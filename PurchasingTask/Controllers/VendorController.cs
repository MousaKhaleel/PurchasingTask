using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VendorController : ControllerBase
	{
		private readonly IVendorRepository _vendorRepository;

		public VendorController(IVendorRepository vendorRepository)
		{
			_vendorRepository = vendorRepository;
		}
		//TODO: vendor register and login
		[HttpPost("register")]
		public async Task<IActionResult> Register(Vendor vendor)
		{
			_vendorRepository.AddAsync(vendor);
			_vendorRepository.SaveChangesAsync();

			return Ok("registerd sucssesfuly");
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login()
		{
			var vendor = await _vendorRepository.GetByEmail(.Email);
			if (vendor.Password != .Password) {
				return BadRequest("Invalid creditntials");
			}
			else
				return Ok("Login successful.");
		}
		//TODO: Update Vendor Information such as payment method or vendor address.

		[Authorize]
		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateVendor(int id, VendorDto vendorUpdatedData)
		{
			var vendor = await _vendorRepository.GetByIdAsync(id);

			vendor.PaymentMethod.PaymentMethodId = ;
			vendor.PaymentMethod.PaymentMethodName = vendorUpdatedData.PaymentMethodName;
			vendor.VendorAddress = vendorUpdatedData.Address;
			await _vendorRepository.UpdateAsync(vendor);

			_vendorRepository.SaveChangesAsync();
			return Ok(vendor);
		}
	}
}