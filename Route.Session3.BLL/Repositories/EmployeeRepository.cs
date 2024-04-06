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
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public IQueryable<Employee> GetEmployeesByAddress(string address)
		{
			return _dbContext.Employees.Where(E => E.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
		}

		public override async Task<IEnumerable<Employee>> GetAllAsync()
			=> await _dbContext.Employees.Include(E => E.Department).ToListAsync();

		public IQueryable<Employee> SearchByName(string name)
			=> _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E => E.Department);

	}
}
