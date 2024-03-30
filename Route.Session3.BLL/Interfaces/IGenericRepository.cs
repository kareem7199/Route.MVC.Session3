using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Session3.DAL.Models;

namespace Route.Session3.BLL.Interfaces
{
    public interface IGenericRepository <T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
