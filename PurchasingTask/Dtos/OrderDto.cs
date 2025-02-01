using PurchasingTask.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchasingTask.Dtos
{
	public class OrderDto
	{
		public string VendorId { get; set; }
		public string Description { get; set; }

		public List<int>? itemsToOrderIds { get; set; }
	}
}
