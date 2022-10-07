using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DigitalBooks.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DigitalBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        AuthorContext db = new AuthorContext();
        public static IWebHostEnvironment webHostEnvironment;
        private IConfiguration _config;
        [BindProperty]
        public BookModel bookModel { get; set; }
        public HomeController(IWebHostEnvironment environment, IConfiguration config)
        {
            webHostEnvironment = environment;
            _config = config;

        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post([FromForm] BookModel bookModel)
        {
            var file = Request.Form.Files[0];
            var foldername = "Images";
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(foldername, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Book obj = new Book();
                obj.Title = bookModel.Title;
                obj.Price = Convert.ToInt32(bookModel.Price);
                obj.Category = bookModel.Category;
                obj.Publisher = bookModel.Publisher;
                obj.Active = bookModel.Active;
                obj.Content = bookModel.Content;
                obj.Image = dbPath;
                var data = db.Logins.Where(x => x.Id == bookModel.AuthorId).FirstOrDefault();
                obj.AuthorId = bookModel.AuthorId;
                db.Books.Add(obj);
                db.SaveChanges();
                var response = new { Status = "Success" };
                return Ok(response);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut, DisableRequestSizeLimit]
        public IActionResult Put(int id, [FromForm] BookModel bookModel)
        {
            var file = Request.Form.Files[0];
            var foldername = "Images";
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(foldername, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var data = db.Books.Where(x => x.Id == bookModel.AuthorId).FirstOrDefault();
                data.Title = bookModel.Title;
                data.Price = Convert.ToInt32(bookModel.Price);
                data.Category = bookModel.Category;
                data.Publisher = bookModel.Publisher;
                data.Active = bookModel.Active;
                data.Content = bookModel.Content;
                data.Image = dbPath;
                data.AuthorId = bookModel.AuthorId;
                db.Books.Update(data);
                db.SaveChanges();
                var response = new { Status = "Success" };
                return Ok(response);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("blockBook")]
        public IActionResult BlockBook([FromBody] int id)
        {
            try
            {
                var blockBook = db.Books.Where(s => s.Id == id).FirstOrDefault();
                blockBook.Active = "no";
                db.Books.Update(blockBook);
                db.SaveChanges();
                var response = new { Status = "Success" };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPut]
        [Route("unblockBook")]
        public IActionResult UnblockBook([FromBody] int id)
        {
            try
            {
                var unblockBook = db.Books.Where(s => s.Id == id).FirstOrDefault();
                unblockBook.Active = "yes";
                db.Books.Update(unblockBook);
                db.SaveChanges();
                var response = new { Status = "Success" };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}

