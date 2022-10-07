using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? BookId { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }
        public string PaymentMethod { get; set; }
        public string EmailId { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public string PaymentId { get; set; }
        public DateTime? OrderPlacedTime { get; set; }
    }
}
