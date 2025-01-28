using System.ComponentModel.DataAnnotations.Schema;

namespace PurchasingTask.Models
{
	public class OrderItem
	{
		[ForeignKey(nameof(OrderId))]
		public int OrderId { get; set; }
		public Order Order { get; set; }

		[ForeignKey(nameof(ItemId))]
		public int ItemId { get; set; }
		public Item Item { get; set; }
	}
}
