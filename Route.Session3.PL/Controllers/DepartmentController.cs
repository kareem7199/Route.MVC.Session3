using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Models;

namespace Route.Session3.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepatmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepatmentRepository depatmentRepository, IWebHostEnvironment env)
        {
            _departmentRepository = depatmentRepository;
            _env = env;
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
            if (ModelState.IsValid) // server side validation
            {
                var count = _departmentRepository.Add(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();

            return View(ViewName, department);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id , Department department)
        {

            if(id != department.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _departmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the department");

                return View(department);
            }
        }
    }
}
