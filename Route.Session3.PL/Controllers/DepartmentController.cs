using Microsoft.AspNetCore.Mvc;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Models;

namespace Route.Session3.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepatmentRepository _departmentRepository;

        public DepartmentController(IDepatmentRepository depatmentRepository)
        {
            _departmentRepository = depatmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // server side validation
            {
                var count = _departmentRepository.Add(department);
                if(count > 0)
                    return RedirectToAction(nameof(Index)); 
            }
            return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if(department is null)
                return NotFound();

            return View(department);
        }
    }
}
