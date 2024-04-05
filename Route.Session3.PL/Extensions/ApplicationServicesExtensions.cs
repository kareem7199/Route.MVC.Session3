using Microsoft.Extensions.DependencyInjection;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;
using Route.Session3.PL.Helpers;
using AutoMapper;
using Route.Session3.BLL;

namespace Route.Session3.PL.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
            //services.AddScoped<IDepatmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			return services;
		}
	}
}
