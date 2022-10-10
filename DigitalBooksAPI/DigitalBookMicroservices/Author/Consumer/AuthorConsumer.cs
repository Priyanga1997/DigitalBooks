using MassTransit;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author.Consumer
{
    public class AuthorConsumer : IConsumer<Order>
    {
        AuthorContext db;
        public AuthorConsumer(AuthorContext _db)
        {
            db = _db;
        }
        public Task Consume(ConsumeContext<Order> context)
        {
            var data = context.Message;
            var bookDetails = db.Books.Where(x => x.Id == data.BookId).FirstOrDefault();
            bookDetails.Title = bookDetails.Title;
            db.Books.Update(bookDetails);
            db.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
