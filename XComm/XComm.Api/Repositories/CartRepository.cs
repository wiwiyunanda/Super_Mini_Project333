using Microsoft.EntityFrameworkCore;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Security;

namespace XComm.Api.Repositories
{
    public class CartRepository
    {
        private XcommDbContext _dbContext; /*= new XCommDbContext();*/
        private ResponseResult _result = new ResponseResult();
        public CartRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            //throw new NotImplementedException();
            List<CartViewModel> result = new List<CartViewModel>();
            try
            {
                // Filter Search
                var query = (from prd in _dbContext.Products
                             join crt in _dbContext.Carts on
                             new { v1 = prd.Id, v2 = ClaimsContext.UserName() } equals
                             new { v1 = crt.ProductId, v2 = crt.CreatedBy } into prdCrt
                             from crt in prdCrt.DefaultIfEmpty()
                             where prd.Initial.Contains(search) || prd.Name.Contains(search) || prd.Description.Contains(search)
                             select new CartViewModel
                             {
                                 Id = prd.Id,
                                 Initial = prd.Initial,
                                 Name = prd.Name,
                                 Description = prd.Description,
                                 Price = prd.Price,
                                 Stock = prd.Stock,
                                 Active = prd.Active,
                                 Base64 = prd.Gallery.Base64Small,
                                 Quantity = crt.Quantity != null ? crt.Quantity : 0
                             });

                int count = query.Count();

                if (count > 0)
                {
                    switch (orderBy)
                    {
                        case "initial":
                            query = sort == Sorting.Ascending ? query.OrderBy(o => o.Initial) :
                                query.OrderByDescending(o => o.Initial);
                            break;
                        case "name":
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
                        .ToList();

                    _result.Pages = (int)Math.Ceiling((decimal)count / (decimal)rows);

                    if (_result.Pages < pageNum)
                    {
                        return Pagination(1, rows, search, orderBy, sort);
                    }
                }
                else
                {
                    _result.Message = "Not Records Found";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _result;
        }

        private decimal GetCartQuantity(long productId)
        {
            decimal quantity = 0;
            Cart cart = _dbContext.Carts
                .Where(o=> o.ProductId == productId && o.CreatedBy == ClaimsContext.UserName())
                .FirstOrDefault();
            if (cart != null)
            {
                return cart.Quantity;
            }
            return quantity;
        }
    }
}
