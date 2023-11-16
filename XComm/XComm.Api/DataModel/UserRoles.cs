using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XComm.Api.DataModel
{
    [Table("MasterUserRoles")]
    public class UserRoles: BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Role { get; set; }

        public bool Active { get; set; }

        [ForeignKey("UserName")]
        public virtual Account Account { get; set; }
    }
}
