using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductsViewModel
    {
        public long? Id { get; set; }
        public long VariantId { get; set; }
        public VariantsViewModel? Variant { get; set; }
        [Required, MaxLength(10)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public bool Active { get; set; }
    }
}
