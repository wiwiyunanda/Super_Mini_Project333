﻿using Microsoft.EntityFrameworkCore;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Security;

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


        public VariantsViewModel ChangeStatus(long id, bool status)
        {
            VariantsViewModel result = new VariantsViewModel();
            try
            {
                Variants entity = _dbContext.Variants
                    .Where(o => o.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    entity.Active = status;

                    entity.ModifiedBy = ClaimsContext.UserName();
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();

                    result = new VariantsViewModel()
                    {
                        Id = entity.Id,
                        CategoryId = entity.CategoryId,
                        Initial = entity.Initial,
                        Name = entity.Name,
                        Active = status
                    };
                }
                //else
                //{
                //    _result.Success = false;
                //    _result.Message = "Variant not found!";
                //}
            }
            catch (Exception e)
            {
                //_result.Success = false;
                //_result.Message = e.Message;
            }
            return result;
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

                entity.CreatedBy = ClaimsContext.UserName();
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
                              Id = o.Id,
                              CategoryId = o.CategoryId,                            
                              Category = new CategoryViewModel()
                              {
                                  Id = o.Category.Id,
                                  Initial = o.Category.Initial,
                                  Name = o.Category.Name,
                                  Active = o.Category.Active
                              },
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

        public List<VariantsViewModel> GetByParentId(long parentId)
        {
            List<VariantsViewModel> result = new List<VariantsViewModel>();
            try
            {
                result = (from o in _dbContext.Variants
                          where o.CategoryId == parentId
                          select new VariantsViewModel
                          {
                              Id = o.Id,
                              CategoryId = o.CategoryId,
                              Category = new CategoryViewModel()
                              {
                                  Id = o.Category.Id,
                                  Initial = o.Category.Initial,
                                  Name = o.Category.Name,
                                  Active = o.Category.Active
                              },
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

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            try
            {
                //filter by search
                var query = _dbContext.Variants
                    .Where(o => o.Initial.Contains(search) || o.Name.Contains(search));

                switch (orderBy)
                {
                    case "initial":
                        query = sort == Sorting.Ascending ? query.OrderBy(o => o.Initial) :
                        query.OrderByDescending(o => o.Initial);
                        break;
                    case "nama":
                        query = sort == Sorting.Ascending ? query.OrderBy(o => o.Name) :
                        query.OrderByDescending(o => o.Name);
                        break;
                    default:
                        query = sort == Sorting.Ascending ? query.OrderBy(o => o.Id) :
                        query.OrderByDescending(o => o.Id);
                        break;
                }
                _result.Data = query.Skip((pageNum - 1) * rows)
                    .Take(rows)
                    .Select(o => new VariantsViewModel()
                    {
                        Id = o.Id,
                        CategoryId = o.CategoryId,
                        Category = new CategoryViewModel()
                        {
                            Id = o.Category.Id,
                            Initial = o.Category.Initial,
                            Name = o.Category.Name,
                            Active = o.Category.Active
                        },
                        Initial = o.Initial,
                        Name = o.Name,
                        Active = o.Active
                    })
                    .ToList();

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

        public VariantsViewModel Update(VariantsViewModel model)
        {
            try
            {
                Variants entity = _dbContext.Variants
                    .Where(o => o.Id == model.Id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    entity.Initial = model.Initial;
                    entity.Name = model.Name;

                    entity.ModifiedBy = ClaimsContext.UserName();
                    entity.ModifiedDate = DateTime.Now;

                    _dbContext.SaveChanges();
                    model.Id = entity.Id;

                }
                else
                {
                    model.Id = 0;
                    //_result.Success = false;
                    //_result.Message = "Variant not found!";
                    //_result.Data = model;
                }
            }
            catch (Exception e)
            {
                //_result.Success = false;
                //_result.Message = e.Message;
            }
            return model;
        }
    }
}
