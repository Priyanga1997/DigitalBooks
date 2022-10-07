using Microsoft.AspNetCore.Mvc;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author.Services
{
    public interface IAuthorService
    {
        IEnumerable<Book> GetCreateBookDetails(string emailId);
        IEnumerable<Book> GetAllBookDetails();
        Task<Book> PostCreateBookDetails(BookModel bookModel, string dbPath);
        Task<dynamic> UpdateCreateBookDetails(BookModel bookModel, string dbPath);
        Task<dynamic> DeleteCreateBookDetails(int id);
        Task<dynamic> BlockBook(int id);
        Task<dynamic> UnblockBook(int id);
    }
}
