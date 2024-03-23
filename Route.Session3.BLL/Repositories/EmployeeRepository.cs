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
    public class IEmployeeRepository : GenericRepository<Employee>, Interfaces.IEmployeeRepository
    {
        public IEmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
    }
}
