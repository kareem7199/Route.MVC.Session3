using Microsoft.AspNetCore.Mvc;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;

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
            return View();
        }
    }
}
