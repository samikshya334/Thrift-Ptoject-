using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thrift_Us.Models
{
    public class RentalOrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public decimal OrderTotal { get; set; }
        public string TransId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Required]
        public int RentalDuration { get; set; }
        public Order_Status OrderStatus { get; set; }
        public Payment_Status PaymentStatus { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<RentalOrderDetails> RentalOrderDetails { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;

    }

    public enum Payment_Status
    {
        Pending,
        Recieved
    }

    public enum Order_Status
    {
        Inprocess,
        Shipped,
        Recieved
    }
}
