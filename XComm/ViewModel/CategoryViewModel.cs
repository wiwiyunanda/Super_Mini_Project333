﻿using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class CategoryViewModel
    {
        public long? Id { get; set; }
        [Required, MaxLength(10)]
        public string Initial { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
