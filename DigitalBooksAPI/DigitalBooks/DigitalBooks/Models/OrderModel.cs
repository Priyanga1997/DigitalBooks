using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBooks.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int? BookId { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }
        public string PaymentMethod { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
    }
}
