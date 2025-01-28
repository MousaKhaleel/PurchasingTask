using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Models
{
	public class PaymentMethod
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PaymentMethodId { get; set; }

		[Required]
		[MaxLength(20)]
		public string PaymentMethodName { get; set; }

		public ICollection<Vendor> Vendors { get; set; }
	}
}
