using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModel.Models;

namespace Reader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        IReaderService readerService;
        AuthorContext db = new AuthorContext();
        public ReaderController(IReaderService _authorService)
        {
            readerService = _authorService;
        }
        //[HttpGet]
        //[Route("searchBook")]
        //public async Task<IActionResult> Get(string title, string category, int price, string publisher, string author)
        //{
        //    var data = await readerService.SearchBooks(title, category, price, publisher, author);
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(data);
        //    }
        //}
        [HttpGet]
        [Route("searchBook")]
        public IEnumerable<Book> GetBooksBySearch(string title, string category, int price, string publisher, string author)
        {
            var data = (from b in db.Books where (b.Active == "yes" && (b.Title == title || b.Category == category || b.Price == price || b.Publisher == publisher || b.Author == author)) select b).ToList();
            return data;
        }
    }
}
