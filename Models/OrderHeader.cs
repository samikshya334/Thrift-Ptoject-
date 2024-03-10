using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thrift_Us.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Application User Id is required.")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Time of Pick is required.")]
        [DataType(DataType.Time)]
        public DateTime TimeofPick { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "Date of Pick is required.")]
        [DataType(DataType.Date)]
        public DateTime DateofPick { get; set; }=DateTime.Now;

        [Required(ErrorMessage = "Order Total is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Order Total must be a positive number.")]
        public decimal OrderTotal { get; set; }

        [Required(ErrorMessage = "Transaction Id is required.")]
        public string TransId { get; set; }

        [Required(ErrorMessage = "Order Status is required.")]
        public OrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = "Payment Status is required.")]
        public PaymentStatus PaymentStatus { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Received
    }

    public enum OrderStatus
    {
        InProcess,
        Shipped
    }
}
