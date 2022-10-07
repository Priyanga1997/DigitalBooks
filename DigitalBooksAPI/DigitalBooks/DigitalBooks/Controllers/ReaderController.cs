using DigitalBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        AuthorContext db = new AuthorContext();
        [HttpGet]
        public IEnumerable<Book> searchBooks(string title, string category, int price, string publisher)
        {
            List<Book> data = (from b in db.Books where (b.Active == "yes" && (b.Title == title || b.Category == category || b.Price == price || b.Publisher == publisher)) select b).ToList();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
