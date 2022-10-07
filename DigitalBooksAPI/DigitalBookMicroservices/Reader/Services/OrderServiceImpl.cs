using SharedModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Services
{
    public class OrderServiceImpl : IOrderService
    {
        AuthorContext db;
        public OrderServiceImpl(AuthorContext _db)
        {
            db = _db;
        }
        public async Task<dynamic> GetOrder(string emailId)
        {
            var data = (from a in db.Orders
                        where (a.EmailId == emailId)
                        orderby a.OrderId descending
                        select a).ToList();
            return data;
        }
        public async Task<dynamic> CancelOrder(int orderId)
        {
            var cancelOrder = db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
            cancelOrder.Active = "no";
            db.Orders.Update(cancelOrder);
            db.SaveChanges();
            return "Order got cancelled successfully";
        }
    }
}
