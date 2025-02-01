using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;
using PurchasingTask.Repositories;

namespace PurchasingTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	[Authorize(Roles = "Admin")]
	public class AdminController : ControllerBase
	{
		private readonly IVendorRepository _vendorRepository;
		private readonly IOrderRepository _orderRepository;

		private readonly UserManager<Vendor> _userManager;

		public AdminController(IVendorRepository vendorRepository,IOrderRepository orderRepository, UserManager<Vendor> userManager)
		{
			_vendorRepository = vendorRepository;
			_orderRepository = orderRepository;
		}

		[HttpGet("GetAllVendors")]
		public async Task<IActionResult> GetAllVendors()
		{
			var results = await _vendorRepository.GetAllAsync();//TODO: include orderItems
			return Ok(results);
		}

		[HttpGet("GetAllOrders")]
		public async Task<IActionResult> GetAllOrders()
		{
			var results = await _orderRepository.GetAllAsync();
			return Ok(results);
		}

	}
}
