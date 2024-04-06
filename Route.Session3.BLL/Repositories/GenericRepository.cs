using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Session3.BLL.Interfaces;
using Route.Session3.DAL.Data;
using Route.Session3.DAL.Models;

namespace Route.Session3.BLL.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _dbContext;
		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Add(T entity)
			=> _dbContext.Add(entity);


		public void Delete(T entity)
			=> _dbContext.Remove(entity);


		public async Task<T> GetAsync(int id)
			=> await _dbContext.FindAsync<T>(id);


		public virtual async Task<IEnumerable<T>> GetAllAsync()
			=> await _dbContext.Set<T>().AsNoTracking().ToListAsync();

		public void Update(T entity)
			=> _dbContext.Update(entity);
	}
}
