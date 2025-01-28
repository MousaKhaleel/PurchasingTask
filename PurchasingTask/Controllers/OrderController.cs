using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Interfaces;

namespace PurchasingTask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;

		public OrderController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		//Place order
		[HttpGet(Name = "PlaceOrder")]
		public async Task<IActionResult> PlaceOrder()
		{
			var allOrder = await _orderRepository.GetAllAsync();
			//TODO: implement

			return Ok();
		}
		//[HttpPost(Name = "PlaceOrder")]

		//Add/Remove Items from order

		//Update Order items quantity &amp; price then calculate total cost amount.

		//Update Vendor Information such as payment method or vendor address.

	}
}
