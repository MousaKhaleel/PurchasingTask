using PurchasingTask.Dtos;
using PurchasingTask.Models;

namespace PurchasingTask.Interfaces
{
	public interface IVendorRepository : IGenericRepository<Vendor>
	{
		Task UpdateAsync(Vendor vendor);
		Task GetByEmail(string email);

		Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync();
	}
}
