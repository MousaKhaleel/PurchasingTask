using Microsoft.EntityFrameworkCore;
using PurchasingTask.Data;
using PurchasingTask.Dtos;
using PurchasingTask.Interfaces;
using PurchasingTask.Models;

namespace PurchasingTask.Repositories
{
	public class VendorRepository : GenericRepository<Vendor>, IVendorRepository
	{
		private readonly AppDbContext _context;

		public VendorRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
		{
			return await _context.PaymentMethods.ToListAsync();
		}

		public Task GetByEmail(string email)
		{
			var vendor= _context.Vendors.Where(x=>x.Email==email).FirstOrDefaultAsync();
			return vendor;
		}

		public async Task UpdateAsync(Vendor vendor)
		{
			var findVendor = await _context.Vendors.FindAsync(vendor.Id);
			findVendor.VendorAddress = vendor.VendorAddress;
			findVendor.PaymentMethod.PaymentMethodId = vendor.PaymentMethodId;
			findVendor.PaymentMethod.PaymentMethodName = vendor.PaymentMethod.PaymentMethodName;
			_context.Vendors.Update(findVendor);
		}
	}
}
