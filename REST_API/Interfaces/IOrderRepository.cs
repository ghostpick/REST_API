using REST_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace REST_API.Interfaces
{
    public interface IOrderRepository : IBaseEntity<Order>
    {
        /// <summary>
        /// Gets the orders of user asynchronous.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        Task<List<OrderProducts>> GetOrdersOfUserAsync(string value, string state);

        /// <summary>
        /// Creates the orders for multiple objects.
        /// </summary>
        /// <param name="orders">The orders.</param>
        /// <returns></returns>
        Task<string> CreateOrdersForMultipleObjects(List<Order> orders);

        /// <summary>
        /// Completes the order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        Task<bool> CompleteOrder(string orderId);
    }
}
