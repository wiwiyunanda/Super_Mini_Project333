using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private GalleryRepository _repo;

        public GalleryController(XcommDbContext dbContext) 
        {
            _repo = new GalleryRepository(dbContext);
        }

        [HttpPost]
        public async Task<GalleryViewModel> Post(GalleryViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<ResponseResult>Get(int pageNum, int rows, string? search ="", string orderBy = "", Sorting sort = Sorting.Ascending)
        {
            return _repo.Pagination(pageNum, rows, search, orderBy, sort);
        }
    }
}
