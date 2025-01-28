﻿using Microsoft.EntityFrameworkCore;
using PurchasingTask.Data;
using PurchasingTask.Interfaces;

namespace PurchasingTask.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _context;
		private readonly DbSet<T> _dbSet;
		public GenericRepository(AppDbContext dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<T>();

		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
