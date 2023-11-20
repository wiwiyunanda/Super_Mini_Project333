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

        public ResponseResult ChangeStatus(long id, bool status)
        {
            throw new NotImplementedException();
        }

        public OrderHeaderViewModel Create(OrderHeaderViewModel model)
        {
            try
            {
                string newRef = NewReference();

                if (!String.IsNullOrEmpty(newRef))
                {
                    model.Reference = newRef;

                    using var trans = _dbContext.Database.BeginTransaction();

                    OrderHeader entOh = new OrderHeader();

                    entOh.Reference =model.Reference;

                    //entOh.Amount = model.Amount;

                    entOh.CreatedBy = "Ika";
                    entOh.CreatedDate = DateTime.Now;

                    _dbContext.OrderHeaders.Add(entOh);

                    _dbContext.SaveChanges();

                    decimal amount = 0;

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

                        amount += item.Price * item.Quantity;
                    }

                    entOh.Amount = amount;
                    _dbContext.OrderHeaders.Update(entOh);
                    _dbContext.SaveChanges();

                    trans.Commit();
                }
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

        public ResponseResult Update(OrderHeaderViewModel model)
        {
            throw new NotImplementedException();
        }

        private string NewReference()
        {
            //    yyMM increment
            //SLS-2311-0123
            string yearMonth = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("D2"); //2311
            string newRef = "SLS-" + yearMonth + "-"; //SLS-2311-
            try
            {
                var maxRef = _dbContext.OrderHeaders
                    .Where(o => o.Reference.Contains(newRef))
                    .OrderByDescending(o => o.Reference)
                    .FirstOrDefault();

                if (maxRef != null)
                {
                    //SLS-2311-0002
                    string[] oldRef = maxRef.Reference.Split('-'); //['SLS'], ['2311'], ['0002']
                    int newInc = int.Parse(oldRef[2]) + 1; //0003
                    newRef += newInc.ToString("D4"); //SLS-2311-0003
                }
                else
                {
                    newRef += "0001"; //SLS-2311-0001
                }
            }
            catch (Exception)
            {
                newRef= string.Empty;
            }
            return newRef;
        }
    }
}
   