using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;
using Thrift_Us.ViewModel.Category;

namespace Thrift_Us.ViewModel.Product
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Size Size { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public long Price { get; set; }
    
        public decimal RentalPrice { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImagePath { get; set; }
       
        public string Condition { get; set; }
        public IEnumerable<CategoryCreateViewModel> Categories { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public DateTime PostedOn { get; set; }



    }
}
