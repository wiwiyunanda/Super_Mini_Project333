using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

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
        public async Task<OrderHeaderViewModel> Post(OrderHeaderViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<List<OrderHeaderViewModel>> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<OrderHeaderViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }
    }
}
