using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thrift_Us.Models
{
    public class RentalCart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RentalDuration { get; set; }

        public int Count { get; set; }

      public decimal RefundAmount { get; set; } 
        public decimal TotalPrice { get; set; }

       
    }
}
