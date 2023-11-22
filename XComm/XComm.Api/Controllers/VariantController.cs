using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController : ControllerBase
    {
        private VariantsRepository _repo;// = new VariantsRepository();
        public VariantController(XcommDbContext dbContext)
        {
            _repo = new VariantsRepository(dbContext);
        }

        [HttpPost]
        public async Task<VariantsViewModel> Post(VariantsViewModel model)
        {
            return _repo.Create(model);
        }
        [HttpGet]
        public async Task<ResponseResult> Get(int pageNum, int rows, string? search = "", string? orderBy = "", Sorting sort = Sorting.Ascending)
        {
            return _repo.Pagination(pageNum, rows, search, orderBy, sort);
        }


        [HttpGet("{id}")]
        public async Task<VariantsViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<VariantsViewModel> Put(long id, VariantsViewModel model)
        {
            model.Id = id;
            return _repo.Update(model);
        }

        [HttpPut("changestatus/{id}")]
        public async Task<VariantsViewModel> Put(long id, bool status)
        {
            return _repo.ChangeStatus(id, status);
        }
    }
}
