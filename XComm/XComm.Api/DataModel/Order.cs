using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XComm.Api.DataModel
{
    [Table("TransOrderHeader")]
    public class OrderHeader: BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(15)]
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }

    [Table("TransOrderDetails")]
    public class OrderDetail: BaseSchema
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
       
        [ForeignKey("HeaderId")]
        public virtual OrderHeader Header { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }

    }
}
