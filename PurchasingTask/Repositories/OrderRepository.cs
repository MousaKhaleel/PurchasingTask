using PurchasingTask.Data;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Repositories
{
	public class OrderRepository : GenericRepository<Order>, IOrderRepository
	{
		private readonly AppDbContext _context;

		public OrderRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task CreateOrderAsync(OrderDto orderDto)
		{
			var order = new Order()
			{
				VendorId = orderDto.VendorId,
				Description = orderDto.Description,
			};
			await _context.Orders.AddAsync(order);
			await SaveChangesAsync();
			List<int> itemsIds = new List<int>();
			for (int i = 0; i < orderDto.itemsToOrderIds.Count; i++)
			{
				itemsIds.Add(orderDto.itemsToOrderIds[i]);
			}
			foreach (var item in itemsIds)
			{
				var orderItem = new OrderItem()
				{
					ItemId = item,
					OrderId = order.OrderId,
				};
				await _context.OrderItems.AddAsync(orderItem);
				var itemPurchesd = _context.Items.Where(x => x.ItemId == item).FirstOrDefault();
				itemPurchesd.Quantity -= 1;
				await SaveChangesAsync();
			}
			await SaveChangesAsync();
		}
		public async Task AddToOrderAsync(string userId, int orderId, int itemId)
		{
			var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId && x.VendorId == userId);
			if (order == null)
			{
				throw new UnauthorizedAccessException("order does not belong to the user");
			}
			else
			{
				var orderItem = new OrderItem()
				{
					OrderId = orderId,
					ItemId = itemId,
				};
				await SaveChangesAsync();
				var item = _context.Items.Where(x => x.ItemId == itemId).FirstOrDefault();
				item.Quantity -= 1;
				_context.Items.Update(item);
				await SaveChangesAsync();
			}
		}
		public async Task RemoveFromOrderAsync(string userId, int orderId, int itemId)
		{
			var orderItem = _context.OrderItems.FirstOrDefault(x => x.OrderId == orderId && x.ItemId == itemId);
			_context.OrderItems.Remove(orderItem);
			await SaveChangesAsync();

			var item = _context.Items.Where(x => x.ItemId == itemId).FirstOrDefault();
			item.Quantity += 1;
			_context.Items.Update(item);
			await SaveChangesAsync();
		}
	}
}
