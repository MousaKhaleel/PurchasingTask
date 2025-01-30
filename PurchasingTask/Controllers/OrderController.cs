using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Interfaces;

namespace PurchasingTask.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IItemRepository _ItemRepository;

		public OrderController(IOrderRepository orderRepository, IItemRepository itemRepository)
		{
			_orderRepository = orderRepository;
			_ItemRepository = itemRepository;
		}

		//TODO: Place order
		[HttpGet(Name = "PlaceOrder")]
		public async Task<IActionResult> PlaceOrder()
		{
			var allItems = await _ItemRepository.GetAllInStockAsync();
			//TODO: implement
			var order = await _orderRepository.CreateOrderAsync(itemsToOrder);
			_orderRepository.SaveChangesAsync();
			return Ok(order);
		}
		//[HttpPost(Name = "PlaceOrder")]

		//TODO: Add/Remove Items from order

		//TODO: Update Order items quantity &amp; price then calculate total cost amount.
		[HttpPost("AddToOrder/{orderId}/{itemId}")]
		public async Task<IActionResult> AddToOrder(int orderId, int itemId)
		{
			//TODO: implement
			var order = await _orderRepository.GetByIdAsync(orderId);


			_orderRepository.SaveChangesAsync();
			return Ok();
		}
		[HttpDelete("RemoveFromOrder/{orderId}/{itemId}")]
		public async Task<IActionResult> RemoveFromOrder(int orderId, int itemId)
		{
			//TODO: implement
			var order = await _orderRepository.GetByIdAsync(orderId);
			var itemToRemove= _ItemRepository.GetByIdAsync(itemId);

			_orderRepository.RemoveFromOrder(itemToRemove);
			itemToRemove.quntte+=1
			_ItemRepository.SaveChangesAsync();
			_orderRepository.SaveChangesAsync();
			return Ok();
		}

	}
}
