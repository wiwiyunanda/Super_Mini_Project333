
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
     //request
     public class LoginViewModel
     {
         [Required, MaxLength(50)]
         public string UserName { get; set; }

         [Required, MaxLength(200)]
         public string Password { get; set; }
     }

    //request
    public class RegisterViewModel: LoginViewModel
    {
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(200)]
        public string Email { get; set; }
    }

    //respon
    public class AccountViewModel
    {
        public long? Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public bool Active { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }  
}
