using System.ComponentModel.DataAnnotations;
using Thrift_Us.ViewModel;

namespace Thrift_Us.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public decimal Price { get; set; }
       

       
    }
}
