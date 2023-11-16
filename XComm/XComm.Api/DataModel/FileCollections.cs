using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XComm.Api.DataModel
{
    [Table("MasterFileCollections")]
    public class FileCollections: BaseSchema
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(200)]
        public string FileName { get; set; }
        public bool Active { get; set; }

    }
}
