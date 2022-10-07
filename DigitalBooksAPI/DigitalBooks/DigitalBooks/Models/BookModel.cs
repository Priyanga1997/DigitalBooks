using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBooks.Models
{
    public class BookModel
    {

            public int? Id { get; set; }

            public string Title { get; set; }

            public string Category { get; set; }

            public string Author { get; set; }

            public IFormFile Image { get; set; }

            public string Price { get; set; }

            public string Active { get; set; }

            public string Content{ get; set; }

            public string Publisher { get; set; }
            public int AuthorId { get; set; }

    }
    }

