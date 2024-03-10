using Thrift_Us.Models;

namespace Thrift_Us.ViewModel
{
    public class OrderDetailsViewModel
    {
   
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public RentalOrderHeader RentalOrderHeader { get; set; }
        public IEnumerable<RentalOrderDetails> RentalOrderDetails { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public int RentalDuration { get; set; }

    }
}
