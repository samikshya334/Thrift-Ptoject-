using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrift_Us.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, long.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public long Price { get; set; }


        [Required(ErrorMessage = "Rental Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Rental Price must be a non-negative number.")]
        public decimal RentalPrice { get; set; }
        [Required(ErrorMessage = "Size is required.")]
        public Size Size { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        [StringLength(50, ErrorMessage = "Condition must not exceed 50 characters.")]
        public string Condition { get; set; }

        public string ImageUrl { get; set; }
        public double Similarity { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostedOn { get; set; }

        [Required(ErrorMessage = "Application User Id is required.")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual IdentityUser ApplicationUser { get; set; }
        public ICollection<Like> Likes { get; set; }
        public List<Feature> Features { get; set; }

    }
    public enum Size
    {
        Xl,
        L,
        M,
        S,
        XXl,

    }

}
