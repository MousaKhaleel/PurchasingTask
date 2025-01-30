using PurchasingTask.Dtos;
using PurchasingTask.Models;

namespace PurchasingTask.Interfaces
{
	public interface IVendorRepository : IGenericRepository<Vendor>
	{
		Task UpdateAsync(VendorDto vendorDto);
		Task GetByEmail(string email);
	}
}
