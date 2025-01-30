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

		public Task GetByEmail(string email)
		{
			var vendor= _context.Vendors.Where(x=>x.Email==email).FirstOrDefaultAsync();
			return vendor;
		}

		public async Task UpdateAsync(Vendor vendor)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(VendorDto vendorDto)
		{
			throw new NotImplementedException();
		}
	}
}
