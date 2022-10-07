using Author.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Author.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IAuthorService authorService;
        public AuthorController(IAuthorService _authorService)
        {
            authorService = _authorService;
        }

        [HttpGet]
        [Route("getBooksByAuthorId")]
        public IEnumerable<Book> Get(string emailId)
        {
            return authorService.GetCreateBookDetails(emailId);
        }

        [HttpGet]
        [Route("getAllBooks")]
        public IEnumerable<Book> GetAllBookBetails()
        {
            return authorService.GetAllBookDetails();
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> PostCreateBookDetails([FromForm] BookModel bookModel)
        {
            var file = Request.Form.Files[0];
            var pathToSave = Directory.GetCurrentDirectory();
            if (file.Length > 0)
            {
                try
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var _filename = Path.GetFileNameWithoutExtension(fileName);
                    fileName = _filename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = fileName;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    string connectionstring = "DefaultEndpointsProtocol=https;AccountName=digitalbooksimages;AccountKey=0SWZdFmNHrgIgJCeJtwBd/czrH5Yrcvsr8bh9KNI0ltSAC4PEMIvrwOy0NUOse6sKMm1FcY3hPzJ+ASt6fAyhA==;EndpointSuffix=core.windows.net";
                    string containerName = "images";
                    BlobContainerClient container = new BlobContainerClient(connectionstring, containerName);
                    var blob = container.GetBlobClient(fileName);
                    var blobstream = System.IO.File.OpenRead(fileName);
                    await blob.UploadAsync(blobstream);
                    var URI = blob.Uri.AbsoluteUri;
                    return Ok(new { dbPath });
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            //var file = Request.Form.Files[0];
            //var foldername = "Images";
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
            //var dbPath = string.Empty;
            //if (file.Length > 0)
            //{
            //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    dbPath = Path.Combine(foldername, fileName);
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        file.CopyTo(stream);
            //    }
            //}

            //var createBookDetails = await authorService.PostCreateBookDetails(bookModel, dbPath);
            //if (createBookDetails != null)
            //{
            //    return Ok(new { Status = createBookDetails });
            //}
            //else
            //{
            //    return BadRequest();
            //}

        }


        [HttpPut, DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateCreateBookDetails([FromForm] BookModel bookModel)
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

                var updateBookDetails = await authorService.UpdateCreateBookDetails(bookModel, dbPath);
                var response = new { Status = "Success" };
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCreateBookDetails(int id)
        {
            var deleteBookDetails = await authorService.DeleteCreateBookDetails(id);
            var response = new { Status = "Success" };
            return Ok(response);
        }

        [HttpPut]
        [Route("blockBook")]
        public async Task<IActionResult> BlockBook([FromBody] int id)
        {
            try
            {
                var blockBook = await authorService.BlockBook(id);
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
        public async Task<IActionResult> UnblockBook([FromBody] int id)
        {
            try
            {
                var UnblockBook = await authorService.UnblockBook(id);
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
