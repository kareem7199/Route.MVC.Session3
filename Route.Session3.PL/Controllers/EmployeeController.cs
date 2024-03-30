using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Models;
using Route.Session3.PL.ViewModels;

namespace Route.Session3.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

		public EmployeeController(IMapper mapper , IUnitOfWork unitOfWork, IWebHostEnvironment env)
		{
			_mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
		}
		public IActionResult Index(string searchInput)
		{
			var employees = Enumerable.Empty<Employee>();
			var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

			if (string.IsNullOrEmpty(searchInput))
				employees = employeeRepo.GetAll();
			else
				employees = employeeRepo.SearchByName(searchInput);

			var mappedEmps = _mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeViewModel>>(employees);

			return View(mappedEmps);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(EmployeeViewModel employeeVm)
		{
			if (ModelState.IsValid) // server side validation
			{

				var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.Repository<Employee>().Add(employee);
				var count = _unitOfWork.Complete();

				if (count > 0)
					TempData["Message"] = "Employee is Created Successfully";
				else
					TempData["Message"] = "An Error Has Occured, Employee Not Created :(";

				return RedirectToAction(nameof(Index));
			}
			return View(employeeVm);
		}

		public IActionResult Details(int? id, string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();
			var employee = _unitOfWork.Repository<Employee>().Get(id.Value);

			if (employee is null)
				return NotFound();

			var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

			return View(ViewName, mappedEmp);
		}


		public IActionResult Edit(int? id)
		{
			return Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EmployeeViewModel employeeVm)
		{
		
			if (!ModelState.IsValid)
				return View(employeeVm);

			try
			{
				var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.Repository<Employee>().Update(employee);
				_unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. log Exception
				// 2. Friendly Message
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the employee");

				return View(employeeVm);
			}
		}

		[HttpPost]
		public IActionResult Delete(int? id)
		{
			if (!id.HasValue)
				return BadRequest();

			var employee = _unitOfWork.Repository<Employee>().Get(id.Value);

			if (employee is null)
				return NotFound();

			try
			{
                _unitOfWork.Repository<Employee>().Delete(employee);
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the employee");

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

	}
}
