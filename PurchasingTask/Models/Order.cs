using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Models
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }

		[ForeignKey(nameof(VendorId))]
		public int VendorId { get; set; }
		public Vendor Vendor { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		public DateTime? DeliveryDate { get; set; }

		[MaxLength(150)]
		public string Description { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
