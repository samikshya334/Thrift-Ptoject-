using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;

namespace Thrift_Us.ViewModels
{
    public class RentalViewModel
    {
        [Key]
        public int RentalId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
   
        public Size Size { get; set; }
        public string ImageUrl { get; set; }
        public string Condition { get; set; }

      
        public string Username { get; set; }

   
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal RentalPrice { get; set; }
        public decimal Price { get; set; }

        public int Count { get; set; }
        [Required]
        public int RentalDuration { get; set; }
        public decimal TotalPrice { get; set; }
        
        


        public Product Product { get; set; }
        public IdentityUser ApplicationUser { get; set; }
    }


}
