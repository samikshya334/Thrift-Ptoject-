using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Thrift_Us.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

  
        public int Count { get; set; }

        public decimal TotalPrice
        {
            get { return Product?.Price * Count ?? 0; }
        }
    }
}
