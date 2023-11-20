using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class VariantsViewModel
    {
        public long? Id { get; set; }
        public long CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; } 

        [Required, MaxLength(10)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
