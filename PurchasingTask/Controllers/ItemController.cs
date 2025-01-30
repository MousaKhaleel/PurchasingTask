using Microsoft.AspNetCore.Mvc;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ItemController : ControllerBase
	{
		private readonly IItemRepository _itemRepository;

		public ItemController(IItemRepository itemRepository)
		{
			_itemRepository = itemRepository;
		}

		//TODO: add item
		[HttpPost]
		public async Task<IActionResult> AddItem(ItemDto itemDto)
		{
			var item = new Item()
			{
				ItemId = itemDto.ItemId,
				ItemCode = itemDto.ItemCode,
				ItemName = itemDto.ItemName,
				Unit = itemDto.Unit,
				Quantity = itemDto.Quantity,
				Price = itemDto.Price,
				CostAmount = itemDto.CostAmount,
			};
			_itemRepository.AddAsync(item);
			_itemRepository.SaveChangesAsync();
			return Ok(item);
		}
	}
}
