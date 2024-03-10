﻿using System.ComponentModel.DataAnnotations;

namespace Thrift_Us.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
