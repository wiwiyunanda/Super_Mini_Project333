using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;
using XComm.Api.Security;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        private CategoryRepository _repo; /*= new CategoryRepository();*/
        public CategoryController(XcommDbContext dbContext)
        {
            _repo = new CategoryRepository(dbContext);
        }

        [HttpPost]
        [ReadableBodyStream(Roles = "Administrator,Categories")]
        public async Task<CategoryViewModel> Post(CategoryViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<ResponseResult> Get(int pageNum, int rows, string? search = "", string? orderBy = "", Sorting sort = Sorting.Ascending)
        {
            return _repo.Pagination(pageNum, rows, search, orderBy, sort);
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }

        [HttpPut("{id}")]
        [ReadableBodyStream(Roles = "Administrator,Categories")]
        public async Task<CategoryViewModel> Put(long id, CategoryViewModel model)
        {
            model.Id = id;
            return _repo.Update(model);
        }

        [HttpPut("changestatus/{id}/{status}")]
        [ReadableBodyStream(Roles = "Administrator,Categories")]
        public async Task<CategoryViewModel> Put(long id,bool status)
        {
            return _repo.ChangeStatus(id, status);
        }
    }
}
