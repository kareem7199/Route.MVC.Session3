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
    public class DepartmentRepository : GenericRepository<Department> , IDepatmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
