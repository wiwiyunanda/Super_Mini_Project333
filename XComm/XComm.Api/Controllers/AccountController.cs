using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ViewModel;
using XComm.Api.DataModel;
using XComm.Api.Repositories;

namespace XComm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private AccountRepository _repo;
        public AccountController(XcommDbContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _repo = new AccountRepository(dbContext);
        }

        [HttpPost("Login")]
        public async Task<AccountViewModel> Post(LoginViewModel model)
        {
            AccountViewModel result = new AccountViewModel();
            if (ModelState.IsValid)
            {
                result= _repo.Authentication(model);
                if(result != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.UserName),
                        new Claim("FistName", result.FirstName),
                        new Claim("LastName", result.LastName)
                    };
                    claims.Add(new Claim(ClaimTypes.Role, "Category"));
                    claims.Add(new Claim(ClaimTypes.Role, "Variant"));
                    claims.Add(new Claim(ClaimTypes.Role, "Product"));

                    var token = GetToken(claims);
                    result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    return null;
                }
            }
            return result;
        }
        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:Expires"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

            return token;
        }

        [HttpPost("Register")]
        public async Task<AccountViewModel> Post(RegisterViewModel model)
        {
            return _repo.Register(model);
        }
    }
}
