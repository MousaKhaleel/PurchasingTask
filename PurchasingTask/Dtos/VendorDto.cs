using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Dtos
{
	public class VendorDto
	{
		public int PaymentMethodId { get; set; }
		[Required]//?
		public string PaymentMethodName { get; set; }
		public string Address { get; set; }
	}
}
