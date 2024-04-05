using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Session3.DAL.Models
{
    public class Department : ModelBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string Description { get; set; }

        // Navigational Property 
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
