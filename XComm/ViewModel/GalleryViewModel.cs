using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GalleryViewModel
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Base64Big { get; set; }
        [Required]
        public string Base64Small { get; set; }
        public bool Active { get; set; }
    }
}
