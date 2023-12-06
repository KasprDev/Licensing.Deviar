using SendGrid.Helpers.Mail;
using SendGrid;

namespace Licensing.Deviar.Services
{
	public class MailService(IConfiguration config)
	{
		public async Task Send(string subject, string body, string recipient)
		{
			var client = new SendGridClient(config["SendGrid:Key"]);
			var from = new EmailAddress("licensing@deviar.net", "Deviar Licensing");
			var to = new EmailAddress(recipient, "Customer");
			var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
			await client.SendEmailAsync(msg);
		}
	}
}
