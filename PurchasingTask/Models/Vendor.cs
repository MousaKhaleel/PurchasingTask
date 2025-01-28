using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Models
{
	public class Vendor
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int VendorId { get; set; }

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
