using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class CategoryRepository : IRepository<CategoryViewModel>
    {
        private XcommDbContext _dbContext; // = new XcommDbContext();
        public CategoryRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CategoryViewModel Create(CategoryViewModel model)
        {
            try
            {
                Category entity = new Category();
                entity.Initial = model.Initial;
                entity.Name = model.Name;
                entity.Active = model.Active;

                entity.CreatedBy = "Ika";
                entity.CreatedDate = DateTime.Now;

                _dbContext.Categories.Add(entity);
                _dbContext.SaveChanges();

                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return model;
        }
        public List<CategoryViewModel> GetAll()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            try
            {
                result = (from o in _dbContext.Categories
                          select new CategoryViewModel
                          {
                              Id = o.Id,
                              Initial = o.Initial,
                              Name = o.Name,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public CategoryViewModel GetById(long id)
        {
                CategoryViewModel result = new CategoryViewModel();
                try
                {
                    result = (from o in _dbContext.Categories
                              where o.Id == id
                              select new CategoryViewModel
                              {
                                  Id = o.Id,
                                  Initial = o.Initial,
                                  Name = o.Name,
                                  Active = o.Active
                              }).FirstOrDefault();
                    if (result == null)
                    {
                        return new CategoryViewModel();
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
