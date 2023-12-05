using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Security;

namespace XComm.Api.Repositories
{
    public class ProductsRepository : IRepository<ProductsViewModel>
    {
        private XcommDbContext _dbContext; /*= new XCommDbContext();*/
        private ResponseResult _result = new ResponseResult();
        public ProductsRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductsViewModel ChangeStatus(long id, bool status)
        {
            ProductsViewModel result = new ProductsViewModel();
            try
            {
                Products entity = _dbContext.Products
                    .Where(o => o.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    entity.Active = status;
                    entity.ModifiedBy = ClaimsContext.UserName();
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();

                    result = new ProductsViewModel()
                    {
                        Id = entity.Id,
                        VariantId = entity.VariantId,
                        Initial = entity.Initial,
                        Name = entity.Name,
                        Description = entity.Description,
                        Price = entity.Price,
                        Stock = entity.Stock,
                        Active = status,
                    };
                }
                //else
                //{
                //    _result.Success = false;
                //    _result.Message = "Products Not Found!!";
                //}
            }
            catch (Exception e)
            {
                //_result.Success = false;
                //_result.Message = e.Message;
                //throw;
            }
            return result;
        }

        public ProductsViewModel Create(ProductsViewModel model)
        {
            ProductsViewModel result = new ProductsViewModel();
            try
            {
                Products entity = new Products();
                entity.VariantId = model.VariantId;
                entity.Initial = model.Initial;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;
                entity.Stock = model.Stock;
                entity.Active = model.Active;

                entity.CreatedBy = ClaimsContext.UserName();
                entity.CreatedDate = DateTime.Now;

                _dbContext.Products.Add(entity);
                _dbContext.SaveChanges();

                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public List<ProductsViewModel> GetAll()
        {
            List<ProductsViewModel> result = new List<ProductsViewModel>();
            try
            {
                result = (from o in _dbContext.Products
                          select new ProductsViewModel
                          {
                              Id = o.Id,
                              VariantId = o.VariantId,
                              Variant = new VariantsViewModel()
                              {
                                  Id = o.Variant.Id,
                                  Initial = o.Variant.Initial,
                                  Name = o.Variant.Name,
                                  Active = o.Variant.Active,
                                  Category = new CategoryViewModel()
                                  {
                                      Id = o.Variant.Category.Id,
                                      Initial = o.Variant.Category.Initial,
                                      Name = o.Variant.Category.Name,
                                      Active = o.Variant.Category.Active
                                  },
                              },
                              GalleryId = o.GalleryId,
                              Base64 = o.Gallery.Base64Small,
                              Initial = o.Initial,
                              Name = o.Name,
                              Description = o.Description,
                              Price = o.Price,
                              Stock = o.Stock,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public ProductsViewModel GetById(long id)
        {
            ProductsViewModel result = new ProductsViewModel();
            try
            {
                result = (from o in _dbContext.Products
                          where o.Id == id
                          select new ProductsViewModel
                          {
                              VariantId = o.VariantId,
                              Variant = new VariantsViewModel()
                              {
                                  //Category = new CategoryViewModel()
                                  //{
                                  //    Id = o.Variant.Category.Id,
                                  //    Initial = o.Variant.Category.Initial
                                  //},
                                  Id = o.Variant.Id,
                                  Initial = o.Variant.Initial,
                                  Name = o.Variant.Name,
                                  Active = o.Variant.Active
                              },
                              Id = o.Id,
                              Initial = o.Initial,
                              Name = o.Name,
                              Description = o.Description,
                              Price = o.Price,
                              Stock = o.Stock,
                              Base64 = o.Gallery.Base64Small,
                              Active = o.Active
                          }).FirstOrDefault();
                if (result == null)
                {
                    return new ProductsViewModel();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public List<ProductsViewModel> GetByParentId(long parentId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            //throw new NotImplementedException();
            List<ProductsViewModel> result = new List<ProductsViewModel>();
            try
            {
                // Filter Search
                var query = _dbContext.Products
                    .Where(o => o.Initial.Contains(search) || o.Name.Contains(search) || o.Description.Contains(search));

                int count = query.Count();

                if (count > 0)
                {
                    switch (orderBy)
                    {
                        case "initial":
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Initial) :
                                query.OrderByDescending(o => o.Initial);
                            break;
                        case "name":
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Name) :
                                query.OrderByDescending(o => o.Name);
                            break;
                        default:
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Id) :
                                query.OrderByDescending(o => o.Id);
                            break;
                    }
                    _result.Data = query.Skip((pageNum - 1) * rows)
                        .Take(rows)
                        .Select(o => new ProductsViewModel()
                        {
                            Id = o.Id,
                            Initial = o.Initial,
                            Name = o.Name,
                            Description = o.Description,
                            Price = o.Price,
                            Stock = o.Stock,
                            Base64 = o.Gallery.Base64Small,
                            Active = o.Active,
                            Variant = new VariantsViewModel()
                                    {
                                        Id = o.Variant.Id,
                                        Initial = o.Variant.Initial,
                                        Name = o.Variant.Name,
                                        Active = o.Variant.Active,
                                        Category = new CategoryViewModel()
                                        {
                                            Id = o.Variant.Category.Id,
                                            Initial = o.Variant.Category.Initial,
                                            Name = o.Variant.Category.Name,
                                            Active = o.Variant.Category.Active
                                        }
                                    },                              
                            
                        })
                        .ToList();

                    _result.Pages = (int)Math.Ceiling((decimal)count / (decimal)rows);

                    if (_result.Pages < pageNum)
                    {
                        return Pagination(1, rows, search, orderBy, sort);
                    }
                }
                else
                {
                    _result.Message = "Not Record Found";
                }
            }
            catch (Exception ex)
            {
               
                throw;
            }
            return _result;
        }

        public ProductsViewModel Update(ProductsViewModel model)
        {
            try
            {
                Products entity = _dbContext.Products
                    .Where(o => o.Id == model.Id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    entity.Initial = model.Initial;
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.VariantId = model.VariantId;
                    entity.Price = model.Price;
                    entity.Stock = model.Stock;

                    entity.ModifiedBy = ClaimsContext.UserName();
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();
                    model.Id = entity.Id;
                }
                else
                {
                    model.Id = 0;
                    //_result.Success = false;
                    //_result.Message = "Products Not Found!!";
                    //_result.Data = model;
                }
            }
            catch (Exception e)
            {
                //_result.Success = false;
                //_result.Message = e.Message;
                //throw;
            }
            return model;
        }

        public ProductsViewModel ChangeGallery(long id, long galleryId)
        {
            ProductsViewModel result = new ProductsViewModel();
            try
            {
                Products product = _dbContext.Products
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
                if (product != null)
                {
                    product.GalleryId = galleryId;
                    _dbContext.SaveChanges();

                    result = new ProductsViewModel()
                    {
                        Id = id,
                        GalleryId = galleryId,
                        Initial = product.Initial
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

    }
}
