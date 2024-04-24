using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Session3.DAL.Models;
using Route.Session3.PL.ViewModels.User;

namespace Route.Session3.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Sign Up

		public IActionResult SignUp()
			=> View();

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);	

				if (user is null)
				{
					user = new ApplicationUser()
					{
						UserName = model.Username,
						FName = model.FirstName,
						LName = model.LastName,
						Email = model.Email,
						IsAgree = model.IsAgree
					};

					var result = await _userManager.CreateAsync(user , model.Password);
					
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach(var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				} 

				ModelState.AddModelError(string.Empty , "this username is already in use for another account");

			}
			return View(model);
		}

		#endregion

		#region Sign In
		public IActionResult SignIn()
			=> View();

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);

					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your account is locked!!");

						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your account is not confirmed yet!!");

						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");

					}

				}

				ModelState.AddModelError(string.Empty, "Incorrect  Email or Password");
			}

			return View(model);

		}
		#endregion

		#region Sign Out

		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}


		#endregion

		public IActionResult ForgetPassword()
			=> View();

		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if(user is not null)
				{

				}
				ModelState.AddModelError(string.Empty, "There is no account with this Email!!");
			}
			return View(model);
		}
	}
}
