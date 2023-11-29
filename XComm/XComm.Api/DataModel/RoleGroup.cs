using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XComm.Api.DataModel
{
    [Table("RoleGroups")]
    public class RoleGroup: BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string GroupName { get; set; } //Customer, Kasir, Admin

        public ICollection<AuthorizationGroup> AuthorizationGroups { get; set; }
    }
}
