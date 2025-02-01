using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
		private readonly SignInManager<Vendor> _signInManager;
		private readonly UserManager<Vendor> _userManager;

		public VendorController(IVendorRepository vendorRepository, SignInManager<Vendor> signInManager, UserManager<Vendor> userManager)
		{
			_vendorRepository = vendorRepository;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		//TODO: vendor register and login
		[HttpPost("register")]
		public async Task<IActionResult> Register(VendorRegisterDto vendorRegisterDto)
		{
			if (ModelState.IsValid)
			{
				Vendor vendor = new()
				{
					UserName = vendorRegisterDto.UserName,
					VendorName = vendorRegisterDto.VendorName,
					Email = vendorRegisterDto.Email,
					VendorAddress = vendorRegisterDto.VendorAddress,
					PaymentMethodId = vendorRegisterDto.PaymentMethodId,//fix
				};
				var result = await _userManager.CreateAsync(vendor, vendorRegisterDto.Password);
				if (result.Succeeded)
				{
					var roleResult = await _userManager.AddToRoleAsync(vendor, "Vendor");
					if (roleResult.Succeeded)
					{
						await _vendorRepository.SaveChangesAsync();
						return Ok("registerd sucssesfuly");
					}
				}
			}
			return BadRequest("failed" + ModelState);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(VendorLoginDto vendorLoginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(vendorLoginDto.UserName, vendorLoginDto.Password, false, false);
			if (result.Succeeded)
				return Ok("Login successful");
			else return BadRequest("failed");
		}

		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("succses");
		}

		[HttpGet("Profile")]
		public async Task<IActionResult> Profile()
		{
			var userId = _userManager.GetUserId(User);

			var result = await _vendorRepository.GetByIdAsync(userId);
			return Ok(result);
		}

		//TODO: Update Vendor Information such as payment method or vendor address.

		[Authorize]
		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateVendor(string id, VendorDto vendorUpdatedData)
		{
			var vendor = await _vendorRepository.GetByIdAsync(id);

			vendor.PaymentMethod.PaymentMethodId = vendorUpdatedData.PaymentMethodId;
			vendor.PaymentMethod.PaymentMethodName = vendorUpdatedData.PaymentMethodName;
			vendor.VendorAddress = vendorUpdatedData.Address;
			await _vendorRepository.UpdateAsync(vendor);

			await _vendorRepository.SaveChangesAsync();
			return Ok(vendor);
		}

		//move from here
		[HttpGet("GetPaymentMethods")]
		public async Task<IActionResult> GetPaymentMethods()
		{
			var paymentMethods = await _vendorRepository.GetAllPaymentMethodsAsync();
			return Ok(paymentMethods);
		}
	}
}