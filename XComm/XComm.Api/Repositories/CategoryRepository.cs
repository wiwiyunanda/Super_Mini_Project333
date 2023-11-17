using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class CategoryRepository : IRepository<CategoryViewModel>
    {
        private XcommDbContext _dbContext; // = new XcommDbContext();
        private ResponseResult _result = new ResponseResult();

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

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            //List<CategoryViewModel> result = new List<CategoryViewModel>();
            try
            {
                //filter by search
                var query = _dbContext.Categories
                    .Where(o => o.Initial.Contains(search) || o.Name.Contains(search));

                switch (orderBy)
                {
                    case "initial":
                        query = sort == Sorting.Ascending? query.OrderBy(o => o.Initial):
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
                _result.Data = query.Skip((pageNum-1) * rows)
                    .Take(rows)
                    .Select(o => new CategoryViewModel()
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
    }
}
