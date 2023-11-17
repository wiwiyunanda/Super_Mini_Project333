using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class OrderRepository : IRepository<OrderHeaderViewModel>
    {
        private XcommDbContext _dbContext;
        public OrderRepository(XcommDbContext dbContext)
        {
            
            _dbContext = dbContext;
        }

        public OrderHeaderViewModel Create(OrderHeaderViewModel model)
        {
            try
            {
                using var trans = _dbContext.Database.BeginTransaction();

                OrderHeader entOh = new OrderHeader();
                entOh.Reference = model.Reference;
                entOh.Amount = model.Amount;

                entOh.CreatedBy = "Ika";
                entOh.CreatedDate = DateTime.Now;

                _dbContext.OrderHeaders.Add(entOh);
                _dbContext.SaveChanges();

                foreach (var item in model.Details)
                {
                    OrderDetail entOd = new OrderDetail();
                    entOd.HeaderId = entOh.Id;
                    entOd.ProductId = item.ProductId;
                    entOd.Price = item.Price;
                    entOd.Quantity = item.Quantity;

                    entOd.CreatedBy = "Ika";
                    entOd.CreatedDate = DateTime.Now;

                    _dbContext.OrderDetails.Add(entOd);
                }
                _dbContext.SaveChanges();

                trans.Commit();
            }
            catch (Exception)
            {
                throw;
            }

            return model;
        
        }

        public List<OrderHeaderViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderHeaderViewModel GetById(long id)
        {
            throw new NotSupportedException();
        }

        public ResponseResult Pagination(int pageNum, int rows, string search, string orderBy, Sorting sort)
        {
            throw new NotImplementedException();
        }
    }
}
   