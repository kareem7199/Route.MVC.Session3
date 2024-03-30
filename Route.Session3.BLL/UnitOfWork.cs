using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Data;

namespace Route.Session3.BLL
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly ApplicationDbContext applicationDBContext;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepatmentRepository DepartmentRepository { get; set; } = null;
        public UnitOfWork(ApplicationDbContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
            EmployeeRepository = new EmployeeRepository(applicationDBContext);
            DepartmentRepository = new DepartmentRepository(applicationDBContext);
        }

        public int Complete()
        {
            return applicationDBContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationDBContext.Dispose();
        }
    }
}
