using System.ComponentModel.DataAnnotations;

namespace XComm.Api.DataModel
{
    public class BaseSchema
    {
        [Required]
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
