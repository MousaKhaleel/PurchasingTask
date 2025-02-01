using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IItemRepository _ItemRepository;

		private readonly UserManager<Vendor> _userManager;


		public OrderController(IOrderRepository orderRepository, IItemRepository itemRepository, UserManager<Vendor> userManager)
		{
			_orderRepository = orderRepository;
			_ItemRepository = itemRepository;
			_userManager = userManager;
		}

		//TODO: Place order
		[HttpPost("PlaceOrder")]
		public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
		{
			await _orderRepository.CreateOrderAsync(orderDto);

			await _orderRepository.SaveChangesAsync();
			return Ok("Order made succsefully");
		}

		//TODO: Add/Remove Items from order

		//TODO: Update Order items quantity &amp; price then calculate total cost amount.


		[HttpPost("AddToOrder/{orderId}/{itemId}")]
		public async Task<IActionResult> AddToOrder(int orderId, int itemId)
		{
			try
			{
				var userId = _userManager.GetUserId(User);

				await _orderRepository.AddToOrderAsync(userId, orderId, itemId);
				await _orderRepository.SaveChangesAsync();
				return Ok("added");
			}
			catch (UnauthorizedAccessException e)
			{
				return Unauthorized(e.Message);
			}
		}
		[HttpDelete("RemoveFromOrder/{orderId}/{itemId}")]
		public async Task<IActionResult> RemoveFromOrder(int orderId, int itemId)
		{
			try
			{
				var userId = _userManager.GetUserId(User);

				await _orderRepository.RemoveFromOrderAsync(userId, orderId, itemId);

				await _ItemRepository.SaveChangesAsync();
				await _orderRepository.SaveChangesAsync();
				return Ok("removed");
			}
			catch (UnauthorizedAccessException e)
			{
				return Unauthorized(e.Message);
			}
		}



	}
}
