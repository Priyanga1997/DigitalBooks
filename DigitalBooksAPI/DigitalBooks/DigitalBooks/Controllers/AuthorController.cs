using DigitalBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace DigitalBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : ControllerBase
    {
        AuthorContext db = new AuthorContext();

        [HttpGet]
        [Route("getBooksByAuthorId")]
        public IEnumerable<Book> Get(int id)
        {
            List<Book> getBooks= db.Books
                 .Where(x => x.AuthorId == id)
                 .ToList();
            return getBooks;
        }


        [HttpGet]
        [Route("getAllBooks")]
        public IEnumerable<Book> GetAllBooks(int id)
        {
            //List<Book> getBooks = db.Books
            //     .Where(x => x.AuthorId != id)
            //     .ToList();
            return db.Books;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            var response = new { Status = "Success" };
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var data = db.Books.Where(x => x.Id == id).FirstOrDefault();
            data.Title = book.Title;
            data.Category = book.Category;
            data.Image = book.Image;
            data.Price = book.Price;
            data.Publisher = book.Publisher;
            data.Active = book.Active;
            data.Content = book.Content;
            db.Books.Update(data);
            db.SaveChanges();
            var response = new { Status = "Success" };
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = db.Books.Where(x => x.Id == id).FirstOrDefault();
            db.Books.Remove(data);
            db.SaveChanges();
            var response = new { Status = "Success" };
            return Ok(response);
        }
    }
}


