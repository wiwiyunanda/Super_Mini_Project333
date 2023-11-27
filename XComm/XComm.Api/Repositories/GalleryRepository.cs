using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class GalleryRepository : IRepository<GalleryViewModel>
    {
        private XcommDbContext _dbContext;
        private ResponseResult _result = new ResponseResult();
        public GalleryRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GalleryViewModel ChangeStatus(long id, bool status)
        {
            throw new NotImplementedException();
        }

        public GalleryViewModel Create(GalleryViewModel model)
        {
            try
            {
                Gallery entity = new Gallery();
                entity.Title = model.Title;
                entity.Base64Big = model.Base64Big;
                entity.Base64Small = model.Base64Small;
                entity.Active = model.Active;

                entity.CreatedBy = "Ika";
                entity.CreatedDate = DateTime.Now;

                _dbContext.Galleries.Add(entity);
                _dbContext.SaveChanges();

                model.Id = entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public List<GalleryViewModel> GetAll()
        {
            List<GalleryViewModel> result = new List<GalleryViewModel>();
            try
            {
                result = (from o in _dbContext.Galleries
                          select new GalleryViewModel
                          {
                              Id = o.Id,
                              Title = o.Title,
                              Base64Big = o.Base64Big,
                              Base64Small = o.Base64Small,
                              Active = o.Active
                          }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public GalleryViewModel GetById(long id)
        {
            throw new NotImplementedException();
        }

        public List<GalleryViewModel> GetByParentId(long parentId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            List<GalleryViewModel> result = new List<GalleryViewModel>();
            try
            {
                //filtering by search
                var query = _dbContext.Galleries
                    .Where(o => o.Title.Contains(search));

                int count = query.Count();
                if (count > 0)
                {
                    switch (orderBy)
                    {
                        case "title":
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Title) : query.OrderByDescending(o => o.Title);
                            break;

                        default:
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id);
                            break;
                    }
                    _result.Data = query.Skip((pageNum - 1) * rows)
                    .Take(rows)
                    .Select(o => new GalleryViewModel
                    {
                        Id = o.Id,
                        Title = o.Title,
                        Base64Big = o.Base64Big,
                        Base64Small = o.Base64Small,
                        Active = o.Active
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
                    _result.Message = "No record data";
                }


            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.Message = ex.Message;
            }
            return _result;
        }

        public GalleryViewModel Update(GalleryViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
