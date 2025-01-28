using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Interfaces;

namespace PurchasingTask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class VendorController : ControllerBase
	{
		private readonly IVendorRepository _vendorRepository;

		public VendorController(IVendorRepository vendorRepository)
		{
			_vendorRepository = vendorRepository;
		}
	}
}
