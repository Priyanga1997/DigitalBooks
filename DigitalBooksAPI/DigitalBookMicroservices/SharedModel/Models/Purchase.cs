using System;
using System.Collections.Generic;

#nullable disable

namespace SharedModel.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int? BookId { get; set; }
    }
}
