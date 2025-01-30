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
			signInManager = _signInManager;
			userManager = _userManager;
		}
		//TODO: vendor register and login
		[HttpPost("register")]
		public async Task<IActionResult> Register(VendorRegisterDto vendorRegisterDto)
		{
			Vendor vendor = new()
			{
				UserName = vendorRegisterDto.UserName,
				VendorName = vendorRegisterDto.VendorName,
				Email = vendorRegisterDto.Email,
				PhoneNumber = vendorRegisterDto.VendorAddress,
				PaymentMethodId
			};
			var result = await _userManager.CreateAsync(vendor, vendorRegisterDto.Password);
			_vendorRepository.SaveChangesAsync();

			return Ok("registerd sucssesfuly");
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(VendorLoginDto vendorLoginDto)
		{
			var result = await _signInManager.PasswordSignInAsync(vendorLoginDto.UserName, vendorLoginDto.Password, false, false);
				return Ok("Login successful");
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