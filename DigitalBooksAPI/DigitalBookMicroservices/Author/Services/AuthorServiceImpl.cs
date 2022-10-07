using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using SharedModel.Models;

namespace Author.Services
{
    public class AuthorServiceImpl : IAuthorService
    {
        AuthorContext db;
        public AuthorServiceImpl(AuthorContext _db)
        {
            db = _db;
        }

        public IEnumerable<Book> GetCreateBookDetails(string emailId)
        {
            List<Book> getBooks = db.Books
                 .Where(x => x.EmailId == emailId)
                 .ToList();
            return getBooks;
        }
        public IEnumerable<Book> GetAllBookDetails()
        {
            return db.Books;
        }
        public async Task<Book> PostCreateBookDetails(BookModel bookModel, string dbPath)
        {
            Book obj = new Book();
            obj.Title = bookModel.Title;
            obj.Price = Convert.ToInt32(bookModel.Price);
            obj.Category = bookModel.Category;
            obj.Publisher = bookModel.Publisher;
            obj.Active = bookModel.Active;
            obj.Content = bookModel.Content;
            obj.Image = dbPath;
            obj.Author = bookModel.Author;
            obj.EmailId = bookModel.EmailId;
            db.Books.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public async Task<dynamic> UpdateCreateBookDetails(BookModel bookModel, string dbPath)
        {
            try
            {
                var data = db.Books.Where(x => x.Id == bookModel.Id).FirstOrDefault();
                data.Title = bookModel.Title;
                data.Price = Convert.ToInt32(bookModel.Price);
                data.Category = bookModel.Category;
                data.Publisher = bookModel.Publisher;
                data.Active = bookModel.Active;
                data.Content = bookModel.Content;
                data.Image = dbPath;
                data.Author = bookModel.Author;
                data.EmailId = bookModel.EmailId;
                db.Books.Update(data);
                db.SaveChanges();
                return "Book details got updated successfully";
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<dynamic> DeleteCreateBookDetails(int id)
        {
            try
            {
                var data = db.Books.Where(x => x.Id == id).FirstOrDefault();
                db.Books.Remove(data);
                db.SaveChanges();
                return "Book details got deleted successfully";
            }
            catch (Exception ex)
            {
                return ex;
            }

        }

        public async Task<dynamic> BlockBook(int id)
        {
            try
            {
                var blockBook = db.Books.Where(s => s.Id == id).FirstOrDefault();
                blockBook.Active = "no";
                db.Books.Update(blockBook);
                db.SaveChanges();
                return "Book got blocked successfully";
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<dynamic> UnblockBook(int id)
        {
            try
            {
                var unblockBook = db.Books.Where(s => s.Id == id).FirstOrDefault();
                unblockBook.Active = "yes";
                db.Books.Update(unblockBook);
                db.SaveChanges();
                return "Book got unblocked successfully";
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }
}
