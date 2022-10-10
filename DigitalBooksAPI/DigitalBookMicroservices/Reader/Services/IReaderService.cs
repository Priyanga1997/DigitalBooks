using Microsoft.AspNetCore.Mvc;
using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface IReaderService
    {
        Task<dynamic> SearchBooks(string title, string category, int price, string publisher, string author);
    }
}
