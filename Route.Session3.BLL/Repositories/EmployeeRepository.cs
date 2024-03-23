using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Session3.DAL.Data;
using Route.Session3.DAL.Models;

namespace Route.Session3.BLL.Repositories
{
    internal class EmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Employee entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            ///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            ///if(Employee == null)
            ///    Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
            ///return Employee;
            return _dbContext.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.AsNoTracking().ToList();

        public int Update(Employee entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
