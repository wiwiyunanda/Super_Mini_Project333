using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class VariantsRepository : IRepository<VariantsViewModel>
    {
        private XcommDbContext _dbContext;// = new XcommDbContext();
        private ResponseResult _result = new ResponseResult();
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

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            try
            {
                //filter by search
                var query = _dbContext.Variants
                    .Where(o =>  o.Initial.Contains(search) || o.Name.Contains(search));

                switch (orderBy)
                {
                    //case "CategoryId":
                    //    _result.Data = query.OrderBy(o => o.CategoryId)
                    //     .Skip((pageNum - 1) * rows)
                    //     .Take(rows)
                    //     .ToList();
                    //    break;

                    case "initial":
                        _result.Data = query.OrderBy(o => o.Initial)
                         .Skip((pageNum - 1) * rows)
                         .Take(rows)
                         .ToList();
                        break;
                    case "name":
                        _result.Data = query.OrderBy(o => o.Name)
                        .Skip((pageNum - 1) * rows)
                        .Take(rows)
                        .ToList();
                        break;
                    default:
                        _result.Data = query.OrderByDescending(o => o.Id)
                        .Skip((pageNum - 1) * rows)
                        .Take(rows)
                        .ToList();
                        break;
                }
                _result.Data = query.Skip((pageNum - 1) * rows)
                    .Take(rows)
                    .Select(o => new VariantsViewModel
                    {
                        Id = o.Id,
                        Initial = o.Initial,
                        Name = o.Name,
                        Active = o.Active
                    }).ToList();

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
