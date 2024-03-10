using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;

namespace Thrift_Us.ViewModel
{
    public class SummaryViewModel
    {
        [Key]
        public int RentalId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }

        public Size Size { get; set; }
        public string ImageUrl { get; set; }
        public string Condition { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal RentalPrice { get; set; }


        [Required]
        public int RentalDuration { get; set; }

        public decimal TotalPrice { get; set; }

    }






}
