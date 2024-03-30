using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Models;

namespace Route.Session3.BLL.Interfaces
{
    public interface IUnitOfWork
    {
		IGenericRepository<T> Repository<T>() where T : ModelBase;
		public int Complete();
    }
}
