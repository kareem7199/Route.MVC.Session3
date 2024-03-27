using Route.Session3.DAL.Models;
using System.Collections.Generic;
using System;

namespace Route.Session3.PL.ViewModels
{
	public class DepartmentViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Code { get; set; }
		public DateTime DateOfCreation { get; set; }

		public string Description { get; set; }
	}
}
