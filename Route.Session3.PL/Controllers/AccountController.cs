using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Route.Session3.DAL.Models;
using Route.Session3.PL.Services.EmailSender;
using Route.Session3.PL.ViewModels.User;

namespace Route.Session3.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IEmailSender emailSender,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_configuration = configuration;
		}

		#region Sign Up

		public IActionResult SignUp()
			=> View();

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
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

					var result = await _userManager.CreateAsync(user, model.Password);

					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}

				ModelState.AddModelError(string.Empty, "this username is already in use for another account");

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

		#region Forget Password
		public IActionResult ForgetPassword()
			=> View();

		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

				if (user is not null)
				{
					var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = resetPasswordToken } , "https" , "localhost:5001");
					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"],
						recipients: model.Email,
						subject: "Reset Your Password",
						body: resetPasswordUrl
						);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "There is no account with this Email!!");
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
			=> View();
		#endregion

		#region Reset Password

		public IActionResult ResetPassword(string email , string token)
		{
			TempData["Email"] = email;
			TempData["token"] = token;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{

			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
				{
					await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					return RedirectToAction(nameof(SignIn));
				}

				ModelState.AddModelError(string.Empty, "Url is not valid");
			}

			return View(model);
		}

		#endregion
	}

}
