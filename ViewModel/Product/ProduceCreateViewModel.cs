using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Thrift_Us.ViewModel.Category;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Thrift_Us.ViewModel.Product
{
    public class ProduceCreateViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public decimal RentalPrice { get; set; }
        [Required]
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please select an image.")]
        [Display(Name = "Image")]
        public IFormFile ImagePath { get; set; }

        [Required]
        public string Condition { get; set; }

        [ValidateNever] 
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }


      
        public DateTime PostedOn { get; set; }







    }
}
