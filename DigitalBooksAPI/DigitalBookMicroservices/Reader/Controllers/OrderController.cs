using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Reader.Services;
using SharedModel.Models;
using System;
using System.Threading.Tasks;

namespace Reader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBus bus;
        AuthorContext db = new AuthorContext();
        IOrderService orderService;
        public OrderController(IBus _bus, IOrderService _orderService)
        {
            bus = _bus;
            orderService = _orderService;
        }
        [HttpPost]
        [Route("postOrder")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (order != null)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                Uri uri = new Uri("rabbitmq:localhost/readerQueue");
                var endpoint = await bus.GetSendEndpoint(uri);
                Order orderMessage = new Order();
                orderMessage.BookId = order.BookId;
                orderMessage.Title = order.Title;
                orderMessage.Price = order.Price;
                orderMessage.Quantity = order.Quantity;
                orderMessage.Total = order.Total;
                orderMessage.PaymentMethod = order.PaymentMethod;
                orderMessage.EmailId = order.EmailId;
                orderMessage.Active = order.Active;
                orderMessage.Content = order.Content;
                orderMessage.PaymentId = order.PaymentId;
                orderMessage.OrderPlacedTime = order.OrderPlacedTime;
                await endpoint.Send(orderMessage);
                return Ok(new { status = "Your order is placed" });
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getOrderDetails")]
        public async Task<IActionResult> GetOrder([FromQuery] string emailId)
        {
            try
            {
                var data = await orderService.GetOrder(emailId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPut]
        [Route("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)
        {
            try
            {
                var cancelOrder = await orderService.CancelOrder(orderId);
                var response = new { Status = cancelOrder };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
