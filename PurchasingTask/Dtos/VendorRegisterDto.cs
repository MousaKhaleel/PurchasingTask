using PurchasingTask.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Dtos
{
	public class VendorRegisterDto
	{
		public string? UserName { get; set; }

		public string VendorName { get; set; }
		public string Email { get; set; }
		public string? Password { get; set; }
		public string VendorAddress { get; set; }

		//public int PaymentMethodId { get; set; }
		//public PaymentMethod PaymentMethod { get; set; }
	}
}
