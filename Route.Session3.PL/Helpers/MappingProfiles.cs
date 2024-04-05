using AutoMapper;
using Route.Session3.DAL.Models;
using Route.Session3.PL.ViewModels;

namespace Route.Session3.PL.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
