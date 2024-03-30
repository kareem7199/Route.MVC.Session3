using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Session3.BLL.Repositories;

namespace Route.Session3.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepatmentRepository DepartmentRepository { get; set; }
        public int Complete();
    }
}
