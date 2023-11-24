using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XComm.Api.DataModel
{
    [Table("Galleries")]
    public class Gallery :BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Base64Big { get; set; }
        [Required]
        public string Base64Small { get; set;}
        public bool Active { get; set; }
    }
}
