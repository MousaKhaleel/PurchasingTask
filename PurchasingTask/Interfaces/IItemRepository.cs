using PurchasingTask.Models;

namespace PurchasingTask.Interfaces
{
    public interface IItemRepository : IGenericRepository<Item>
	{
		Task<IEnumerable<Item>> GetAllInStockAsync();
	}
}
