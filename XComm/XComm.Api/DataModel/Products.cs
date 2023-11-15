using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XComm.Api.DataModel
{
    [Table("MasterProducts")]
    public class Products: BaseSchema 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long VariantId { get; set; }
        [Required, MaxLength(10)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }   
        public bool Active { get; set; }


            [ForeignKey("VariantId")]
        public virtual Variants Variant { get; set; }
    }
}

