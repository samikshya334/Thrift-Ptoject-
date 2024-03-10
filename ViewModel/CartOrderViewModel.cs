using System.ComponentModel.DataAnnotations;
using Thrift_Us.Models;
using Thrift_Us.ViewModels;

namespace Thrift_Us.ViewModel
{
    public class CartOrderViewModel
    {
   
        public List<Cart> ListOfCart { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public RentalOrderHeader RentalOrderHeader { get; set; }

        public List<RentalCart> ListOfRentalCart { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
       
    }

}
