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

        public AccountViewModel Registration(RegisterViewModel model)
        {
            AccountViewModel result = new AccountViewModel();
            Account exist = _dbContext.Account
                .Where(o => o.UserName == model.UserName)
                .FirstOrDefault();

            if (exist == null)
            {
                Account account = new Account()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    RoleGroupId = 1,
                    Active = true,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                };

                _dbContext.Account.Add(account);
                _dbContext.SaveChanges();

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
