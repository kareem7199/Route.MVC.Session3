using Microsoft.Extensions.DependencyInjection;
using Route.Session3.BLL.Interfaces;
using Route.Session3.BLL.Repositories;

namespace Route.Session3.PL.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IDepatmentRepository, DepartmentRepository>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();

			return services;
		}
	}
}
