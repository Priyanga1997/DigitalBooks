using DigitalBooks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        AuthorContext db = new AuthorContext();
        [HttpPost]
        [Route("postOrder")]
        public IActionResult Post([FromBody] Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            var response = new { Status = "Success" };
            return Ok(response);
        }
        [HttpGet]
        [Route("getOrderDetails")]
        public IEnumerable<Order> Get([FromQuery] string emailId)
        {
            //var data = (from a in db.Logins join b in db.Orders on a.Id equals b.UserId where(a.UserName == emailId) select new{
            //userId = a.Id,
            //bookId = b.BookId,
            //title = b.Title,
            //price = b.Price,
            //quantity = b.Quantity,
            //total = b.PaymentMethod,
            //Active = b.Active
            //}).ToList();
            //return data;
            var data = (from a in db.Orders
                        where (a.EmailId == emailId)
                        select a).ToList();
            return data;
        }

        [HttpGet]
        [Route("getDetailsToViewBook")]
        public IEnumerable<Order> GetDetailsToViewBook([FromQuery] string emailId, int bookId)
        {
            var data = (from a in db.Orders where (a.EmailId == emailId && a.BookId== bookId) select a).ToList();
            return data;
        }

        [HttpPut]
        [Route("cancelOrder")]
        public IActionResult CancelOrder([FromQuery] int orderId)
        {
            try
            {
                var cancelOrder = db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
                cancelOrder.Active = "no";
                db.Orders.Update(cancelOrder);
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

