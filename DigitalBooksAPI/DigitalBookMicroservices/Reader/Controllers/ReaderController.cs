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
        public ReaderController(IReaderService _authorService)
        {
            readerService = _authorService;
        }
        [HttpGet]
        public IEnumerable<Book> Get(string title, string category, int price, string publisher, string author)
        {
            return readerService.SearchBooks(title, category, price, publisher, author);
        }
    }
}
