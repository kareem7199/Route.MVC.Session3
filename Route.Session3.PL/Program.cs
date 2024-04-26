using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.Session3.DAL.Data;
using Route.Session3.DAL.Models;
using Route.Session3.PL.Extensions;

namespace Route.Session3.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webAppBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services
			webAppBuilder.Services.AddControllersWithViews();


			webAppBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseLazyLoadingProxies().UseSqlServer(webAppBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			webAppBuilder.Services.AddApplicationServices();

			webAppBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{

				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 5;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<ApplicationDbContext>()
			  .AddDefaultTokenProviders();

			webAppBuilder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.AccessDeniedPath = "/";
			});

			#endregion

			var app = webAppBuilder.Build();

			#region Configure kestrel middlewares
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
			#endregion

			app.Run();
		}
	}
}
