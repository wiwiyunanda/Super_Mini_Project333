using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XComm.Api.DataModel
{

    [Table("MasterVariants")]
    public class Variants: BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long CategoryId { get; set; }
        [Required, MaxLength(10)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
