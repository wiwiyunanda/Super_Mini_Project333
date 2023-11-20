﻿using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class ProductsRepository : IRepository<ProductsViewModel>
    {
        private XcommDbContext _dbContext; // = new XcommDbContext();
        private ResponseResult _result = new ResponseResult();
        public ProductsRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ResponseResult ChangeStatus(long id, bool status)
        {
            try
            {
                Products entity = _dbContext.Products
                    .Where(o => o.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    entity.Active = status;

                    entity.ModifiedBy = "Ika";
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();

                    _result.Data = entity;
                }
                else
                {
                    _result.Success = false;
                    _result.Message = "Product not found!";
                }
            }
            catch (Exception e)
            {
                _result.Success = false;
                _result.Message = e.Message;
            }
            return _result;
        }

        public ProductsViewModel Create(ProductsViewModel model)
        {
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

                entity.CreatedBy = "Ika";
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
                              Variants = new VariantsViewModel()
                              {
                                  Id = o.Id,
                                  Initial = o.Initial,
                                  Name = o.Name,                                
                                  Active = o.Active
                              },
                              Id = o.Id,
                              Initial = o.Initial,
                              Name = o.Name,
                              Description = o.Description,
                              Price = o.Price,
                              Stock = o.Stock,
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

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            try
            {
                //filter by search
                var query = _dbContext.Products
                    .Where(o => o.Initial.Contains(search) || o.Name.Contains(search));

                switch (orderBy)
                {
                    case "initial":
                        query = sort == Sorting.Ascending ? query.OrderBy(o => o.Initial) :
                        query.OrderByDescending(o => o.Initial);
                        break;
                    case "nama":
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
                        Active = o.Active
                    })
                    .ToList();

                _result.Pages = (int)Math.Ceiling((decimal)query.Count() / (decimal)rows);

                if (_result.Pages < pageNum)
                {
                    return Pagination(1, rows, search, orderBy, sort);
                }
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.Message = ex.Message;
            }
            return _result;
        }
        public ResponseResult Update(ProductsViewModel model)
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

                    entity.ModifiedBy = "Ika";
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();

                    _result.Data = entity;
                }
                else
                {
                    _result.Success = false;
                    _result.Message = "Product not found!";
                    _result.Data = model;
                }
            }
            catch (Exception e)
            {
                _result.Success = false;
                _result.Message = e.Message;
            }
            return _result;
        }
    }
}
