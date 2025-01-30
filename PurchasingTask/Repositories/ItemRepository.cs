using Microsoft.EntityFrameworkCore;
using PurchasingTask.Data;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Repositories
{
	public class ItemRepository : GenericRepository<Item>, IItemRepository
	{
		private readonly AppDbContext _context;

		public ItemRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Item>> GetAllInStockAsync()
		{
			 var AllInStock = await _context.Items.Where(item => item.Quantity > 0).ToListAsync();
			return AllInStock;
		}
	}
}
