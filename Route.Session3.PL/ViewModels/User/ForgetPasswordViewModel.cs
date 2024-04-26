using System.ComponentModel.DataAnnotations;

namespace Route.Session3.PL.ViewModels.User
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
