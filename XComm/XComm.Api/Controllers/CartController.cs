using Microsoft.AspNetCore.Authorization;
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
    public class CartController : ControllerBase
    {
        private CartRepository _repo;
        public CartController(XcommDbContext dbContext)
        {
            _repo = new CartRepository(dbContext);
        }

        [HttpGet]
        [ReadableBodyStream]
        public async Task<ResponseResult> Get(int pageNum, int rows, string? search= "", string? orderBy = "", Sorting sort = Sorting.Ascending)
        {
            return _repo.Pagination(pageNum, rows, search, orderBy, sort);
        }

    }
}
