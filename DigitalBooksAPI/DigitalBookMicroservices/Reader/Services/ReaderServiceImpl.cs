using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class ReaderServiceImpl : IReaderService
    {
        AuthorContext db = new AuthorContext();
        public IEnumerable<Book> SearchBooks(string title, string category, int price, string publisher, string author)
        {
            List<Book> data = (from b in db.Books where (b.Active == "yes" && (b.Title == title || b.Category == category || b.Price == price || b.Publisher == publisher || b.Author == author)) select b).ToList();
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
