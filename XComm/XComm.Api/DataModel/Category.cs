using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XComm.Api.DataModel
{
    //Inheritance
    //Entity Category
    [Table("MasterCategories")]
    public class Category: BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(50)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
