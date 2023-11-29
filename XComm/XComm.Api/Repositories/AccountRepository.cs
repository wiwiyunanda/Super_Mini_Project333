using Framework.Auth;
using ViewModel;
using XComm.Api.DataModel;

namespace XComm.Api.Repositories
{
    public class AccountRepository
    {
        private XcommDbContext _dbContext;
        public AccountRepository(XcommDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public AccountViewModel Authentication(LoginViewModel model)
        {
            AccountViewModel result = new AccountViewModel();
            result = _dbContext.Account
                .Where(o => o.UserName == model.UserName && o.Password == Encryption.HashSha256(model.Password))
                .Select(o => new AccountViewModel
                {
                    Id = o.Id,
                    UserName = o.UserName,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Email = o.Email,
                    Active = o.Active
                }).FirstOrDefault();               
            return result;
        }

        public AccountViewModel Register(RegisterViewModel model)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                Account entity = new Account();
                entity.UserName = model.UserName;
                entity.Password = model.Password;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;
                entity.CreatedBy = "Ikaa";
                entity.CreatedDate = DateTime.Now;
                _dbContext.Account.Add(entity);
                _dbContext.SaveChanges();

                model.UserName = entity.UserName;

                result = (from o in _dbContext.Account
                          where o.UserName == model.UserName
                          select new AccountViewModel
                          {
                              Id = o.Id,
                              UserName = o.UserName,
                              FirstName = o.FirstName,
                              LastName = o.LastName,
                              Email = o.Email,
                              Active = o.Active
                          }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    }
}
