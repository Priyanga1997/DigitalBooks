using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Services
{
    public interface IOrderService
    {
        Task<dynamic> GetOrder(string emailId);
        Task<dynamic> CancelOrder(int orderId);
    }
}
