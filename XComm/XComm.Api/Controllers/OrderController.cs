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
    public class OrderController : ControllerBase
    {
        private OrderRepository _repo;
        public OrderController(XcommDbContext dbContext)
        {
            _repo = new OrderRepository(dbContext);
        }

        [HttpPost]
        [ReadableBodyStream(Roles = "Administrator,Orders")]
        public async Task<OrderHeaderViewModel> Post(OrderHeaderViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<ResponseResult> Get(int pageNum, int rows, string? search = "", string? orderBy = "", Sorting sort = Sorting.Ascending)
        {
            return _repo.Pagination(pageNum, rows, search, orderBy, sort);
        }

        [HttpGet("{id}")]
        public async Task<OrderHeaderViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }
    }
}
