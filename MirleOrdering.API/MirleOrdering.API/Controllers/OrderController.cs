using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MirleOrdering.API.ViewModels;
using MirleOrdering.Service.Interfaces;
using MirleOrdering.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MirleOrdering.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IUserService userService, IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        // GET api/order/5
        [HttpGet("{id}", Name = "GetOrder")]
        public OrderViewModel Get(long id)
        {
            return _orderService.GetById(id);
        }
        // GET api/order/today
        [HttpGet("today", Name = "GetTodayOrders")]
        public IEnumerable<OrderViewModel> GetTodayOrders()
        {
            return _orderService.GetTodayOrders();
        }
        // GET api/order/user/5
        [HttpGet("user/{id}", Name = "GetOrdersByUserId")]
        public IEnumerable<OrderViewModel> GetByUserId(long id)
        {
            return _orderService.GetUserOrders(id);
        }
        // GET api/order/user/5/today
        [HttpGet("user/{id}/today", Name = "GetTodayOrdersByUserId")]
        public IEnumerable<OrderViewModel> GetTodayOrdersByUserId(long id)
        {
            return _orderService.GetUserOrders(id, isTodayOnly: true);
        }
        // GET api/order/search
        [HttpGet("search", Name = "SearchOrders")]
        public IEnumerable<OrderViewModel> Search([FromQuery]SearchOrderModel model)
        {
            return _orderService.SearchOrders(model.Term, model.UserId, model.OrderedStartOn, model.OrderedEndOn);
        }

        // POST api/order
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]List<OrderBaseModel> models)
        {
            if (models == null || !models.Any())
            {
                return BadRequest("order is null or empty");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orders = new List<OrderViewModel>();
            int totalCost = 0;
            foreach (var model in models)
            {
                var result = _orderService.Create(model);
                long id = long.Parse(result.Message);
                var order = _orderService.GetViewOrderById(id);
                totalCost += (order.ProductPrice ?? 0) * order.Amount;
                orders.Add(order);
            }
            _userService.AddUserBalance(models.First().UserId, -totalCost);

            return Ok(orders);
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            var product = _productService.GetById(order.ProductId);
            if (product != null)
            {
                var cost = product.Price * order.Amount;
                _userService.AddUserBalance(order.UserId, cost);
            }
            return Json(_orderService.Delete(id));
        }
    }
}
