using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;
using System;
using System.Collections.Generic;

namespace MirleOrdering.Service.Interfaces
{
    public interface IOrderService : IGenericService<Order, OrderViewModel, OrderBaseModel>
    {
        ReturnViewModel DeleteTodayListByUser(long userId);
        OrderViewModel GetViewOrderById(long orderId);
        IEnumerable<OrderViewModel> GetTodayOrders();
        IEnumerable<OrderViewModel> GetUserOrders(long userId, bool isTodayOnly = false);
        IEnumerable<OrderViewModel> SearchOrders(string term, long? userId, DateTime? orderedStartOn, DateTime? orderedEndOn);
    }
}
