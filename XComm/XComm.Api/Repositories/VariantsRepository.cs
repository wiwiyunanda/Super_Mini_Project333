using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class VariantsRepository : IRepository<VariantsViewModel>
    {
        private XcommDbContext _dbContext;// = new XcommDbContext();
        public VariantsRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public VariantsViewModel Create(VariantsViewModel model)
        {
            try
            {
                Variants entity = new Variants();
                entity.CategoryId = model.CategoryId;
                entity.Initial = model.Initial;
                entity.Name = model.Name;
                entity.Active = model.Active;

                entity.CreatedBy = "Ika";
                entity.CreatedDate = DateTime.Now;

                _dbContext.Variants.Add(entity);
                _dbContext.SaveChanges();

                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }

            return model;
        }
        public List<VariantsViewModel> GetAll()
        {
            List<VariantsViewModel> result = new List<VariantsViewModel>();
            try
            {
                result = (from o in _dbContext.Variants
                          select new VariantsViewModel
                          {
                              CategoryId = o.CategoryId,
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

        public VariantsViewModel GetById(long id)
        {
            VariantsViewModel result = new VariantsViewModel();
            try
            {
                result = (from o in _dbContext.Variants
                          where o.Id == id
                          select new VariantsViewModel
                          {
                              CategoryId = o.CategoryId,
                              Category = new CategoryViewModel()
                              {
                                  Id = o.Id,
                                  Initial = o.Initial,
                                  Name = o.Name,
                                  Active = o.Active
                              },
                              Id = o.Id,
                              Initial = o.Initial,
                              Name = o.Name,
                              Active = o.Active
                          }).FirstOrDefault();
                if (result == null)
                {
                    return new VariantsViewModel();
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
