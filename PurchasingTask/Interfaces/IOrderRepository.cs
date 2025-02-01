using PurchasingTask.Dtos;
using PurchasingTask.Models;

namespace PurchasingTask.Interfaces
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Task CreateOrderAsync(OrderDto orderDto);
		Task AddToOrderAsync(string userId, int orderId, int itemId);
		Task RemoveFromOrderAsync(string userId, int orderId, int itemId);
	}
}
