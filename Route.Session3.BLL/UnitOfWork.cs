using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Data;
using Route.Session3.DAL.Models;

namespace Route.Session3.BLL
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly ApplicationDbContext applicationDBContext;

		private Hashtable _repositories;
		public UnitOfWork(ApplicationDbContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
			_repositories = new Hashtable();
		}

		public IGenericRepository<T> Repository<T>() where T : ModelBase
		{
			var key = typeof(T).Name; // Employee

			if (!_repositories.ContainsKey(key))
			{

				if (key == nameof(Employee))
				{
					var repository = new EmployeeRepository(applicationDBContext);
					_repositories.Add(key, repository);

				}
				else
				{
					var repository = new GenericRepository<T>(applicationDBContext);
					_repositories.Add(key, repository);

				}
			}

			return _repositories[key] as IGenericRepository<T>;
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
