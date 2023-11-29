using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XComm.Api.DataModel
{
    [Table("MasterAccount")]
    public class Account: BaseSchema
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Key, Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(200)]
        public string Email { get; set; }
        public string? Otp { get; set; }
        public int Attempt { get; set; }
        public long RoleGroupId { get; set; }
        public bool Active { get; set; }

        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
