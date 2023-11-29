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
            Account account = _dbContext.Account
                .Where(o => o.UserName == model.UserName && o.Password == Encryption.HashSha256(model.Password)).FirstOrDefault();

            if (account != null)
            {
                result = new AccountViewModel()
                {
                    Id = account.Id,
                    UserName = account.UserName,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    Active = account.Active
                };

                result.Roles = Roles(account.RoleGroupId);
            }
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
                              Roles = Roles(o.RoleGroupId),
                              Active = o.Active
                          }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private List<string> Roles(long roleGroupId)
        {
            List<string> result = new List<string>();
            var list= _dbContext.AuthorizationGroups
                .Where(o => o.RoleGroupId == roleGroupId)
                .ToList();
            foreach (var group in list)
            {
                result.Add(group.Role);
            }
            return result;
        }

    }
}
