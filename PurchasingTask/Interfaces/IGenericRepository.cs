using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using PurchasingTask.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

namespace PurchasingTask.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();

		Task SaveChangesAsync();
	}
}
