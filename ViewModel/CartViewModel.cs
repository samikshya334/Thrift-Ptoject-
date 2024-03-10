using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;


namespace Thrift_Us.ViewModel
{
    public class CartViewModel
    {
        public int Id { get; set; }

  
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Size Size { get; set; }
        public string ImageUrl { get; set; }
        public string Condition { get; set; }

        public string ApplicationUserId { get; set; }
        public string Username { get; set; }

        public int Count { get; set; }

        public decimal TotalPrice
        {
            get { return Price * Count; }
        }
    }
}

