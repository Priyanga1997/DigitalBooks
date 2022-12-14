using System;
using System.Collections.Generic;

#nullable disable

namespace DigitalBooks.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int? Price { get; set; }
        public string Publisher { get; set; }
        public string Active { get; set; }
        public string Content { get; set; }
        public int? AuthorId { get; set; }
    }
}
