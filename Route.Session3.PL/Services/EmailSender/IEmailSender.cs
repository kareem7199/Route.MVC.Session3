using System.Threading.Tasks;

namespace Route.Session3.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from , string recipients , string subject , string body);
	}
}
