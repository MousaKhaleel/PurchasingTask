using Microsoft.AspNetCore.Authorization;
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

		[HttpGet("GetAllItems")]
		public async Task<IActionResult> GetAllItems()
		{
			var allAvaliable = await _itemRepository.GetAllInStockAsync();
			return Ok(allAvaliable);
		}

		//TODO: add item
		[Authorize(Roles = "Admin")]
		[HttpPost("AddItem")]
		public async Task<IActionResult> AddItem(ItemDto itemDto)
		{
			var item = new Item()
			{
				ItemCode = itemDto.ItemCode,
				ItemName = itemDto.ItemName,
				Unit = itemDto.Unit,
				Quantity = itemDto.Quantity,
				Price = itemDto.Price,
				CostAmount = itemDto.CostAmount,
			};
			await _itemRepository.AddAsync(item);
			await _itemRepository.SaveChangesAsync();
			return Ok(item);
		}
	}
}
