﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductsRepository _repo;// = new VariantsRepository();
        public ProductsController(XcommDbContext dbContext)
        {
            _repo = new ProductsRepository(dbContext);
        }

        [HttpPost]
        public async Task<ProductsViewModel> Post(ProductsViewModel model)
        {
            return _repo.Create(model);
        }

        [HttpGet]
        public async Task<List<ProductsViewModel>> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ProductsViewModel> Get(long id)
        {
            return _repo.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult> Put(long id, ProductsViewModel model)
        {
            model.Id = id;
            return _repo.Update(model);
        }

        [HttpPut("changestatus/{id}")]
        public async Task<ResponseResult> Put(long id, bool status)
        {
            return _repo.ChangeStatus(id, status);
        }
    }
}
