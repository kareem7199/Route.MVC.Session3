using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.Session3.BLL;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.DAL.Models;
using Route.Session3.PL.ViewModels;

namespace Route.Session3.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment env)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_env = env;
		}
		public async Task<IActionResult> Index()
		{
			var departments = await _unitOfWork.Repository<Department>().GetAllAsync();
			var mappedDeps = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

			return View(mappedDeps);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(DepartmentViewModel departmentVm)
		{
			if (ModelState.IsValid) // server side validation
			{
				var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
				_unitOfWork.Repository<Department>().Add(department);
				await _unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(departmentVm);
		}

		public async Task<IActionResult> Details(int? id, string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();

			var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

			if (department is null)
				return NotFound();

			var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);

			return View(ViewName, mappedDep);
		}

		public async Task<IActionResult> Edit(int? id)
			=> await Details(id, "Edit");
		

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DepartmentViewModel departmentVm)
		{

			if (!ModelState.IsValid)
				return View(departmentVm);


			try
			{
				var department = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
				_unitOfWork.Repository<Department>().Update(department);
				await _unitOfWork.Complete();
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

				return View(departmentVm);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue)
				return BadRequest();

			var department = await _unitOfWork.Repository<Department>().GetAsync(id.Value);

			if (department is null)
				return NotFound();

			try
			{
				_unitOfWork.Repository<Department>().Delete(department);
				await _unitOfWork.Complete();
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the department");

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
