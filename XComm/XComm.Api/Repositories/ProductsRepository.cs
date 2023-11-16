using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class ProductsRepository : IRepository<ProductsViewModel>
    {
        private XcommDbContext _dbContext; // = new XcommDbContext();
        public ProductsRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
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
           
    }
}
