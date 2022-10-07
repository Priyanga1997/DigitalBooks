using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface IReaderService
    {
       IEnumerable<Book> SearchBooks(string title, string category, int price, string publisher, string author);
    }
}
