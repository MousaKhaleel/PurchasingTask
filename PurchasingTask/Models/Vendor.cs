using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PurchasingTask.Models
{
	public class Vendor : IdentityUser
	{
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int VendorId { get; set; }
		//used the identiy id
		[Required]
		[MaxLength(60)]
		public string VendorName { get; set; }
		[Required]
		[MaxLength(40)]
		public string Email { get; set; }

		[Required]
		[MaxLength(200)]
		public string VendorAddress { get; set; }

		[ForeignKey(nameof(PaymentMethodId))]
		public int PaymentMethodId { get; set; }
		public PaymentMethod PaymentMethod { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
