using PurchasingTask.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Dtos
{
	public class ItemDto
	{
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string Unit { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal CostAmount { get; set; }

		//public ICollection<OrderItem> OrderItems { get; set; }
	}
}
