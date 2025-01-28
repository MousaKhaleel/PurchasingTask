using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Models
{
	public class Item
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ItemId { get; set; }

		[Required]
		[MaxLength(10)]
		public string ItemCode { get; set; }

		[Required]
		[MaxLength(50)]
		public string ItemName { get; set; }

		[Required]
		[MaxLength(50)]
		public string Unit { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Quantity { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,4)")]
		public decimal CostAmount { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
