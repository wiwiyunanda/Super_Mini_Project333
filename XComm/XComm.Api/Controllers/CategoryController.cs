using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

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
        public async Task<CategoryViewModel> Post(CategoryViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<List<CategoryViewModel>> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }
    }
}
