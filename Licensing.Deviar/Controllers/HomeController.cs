using Licensing.Deviar.Models;
using Microsoft.AspNetCore.Mvc;
using Licensing.Deviar.Data;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Licensing.Deviar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [HttpPost("selly/paid")]
        public async Task<IActionResult> ProcessSellyPayment([FromBody] SellyPaymentDto dto)
        {
            var product = _context.Software.FirstOrDefault(x => x.StripeProductId == dto.product_id);

            if (product == null)
                return BadRequest("Selly Product ID not found.");

            var license = new LicenseKey()
            {
                Email = dto.email,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddYears(10),
                IpAddress = dto.ip_address,
                SoftwareId = product.Id,
                Key = Guid.NewGuid().ToString(),
            };

            _context.LicenseKeys.Add(license);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var client = new SendGridClient(_config["Sendgrid"]);
            var from = new EmailAddress("licensing@deviar.net", $"Deviar Software");
            var to = new EmailAddress(dto.email, dto.email);
            var subject = $"Software License Key for {product.Name}.";
            var htmlContent =
                $"Thank you for purchasing {product.Name}! Your license key to activate the software is <b>{license.Key}</b>. <a href=\"https://discord.gg/kxeSdahKC3\">Join our Discord for the download link & support.</a>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);

            return Ok();
        }
    }
}