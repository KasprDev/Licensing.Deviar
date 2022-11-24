using System.Runtime.CompilerServices;
using Licensing.Deviar.Data;
using Licensing.Deviar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Licensing.Deviar.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public ApiController(
            ApplicationDbContext context,
            IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("usage/log")]
        public async Task<IActionResult> LogUsage([FromBody] UsageLogDto dto)
        {
            var license = _context.LicenseKeys.FirstOrDefault(x => x.Key == dto.LicenseKey);

            if (license == null)
            {
                return BadRequest(new
                {
                    Message = "Unable to update usage logs."
                });
            }

            var u = new UsageLog()
            {
                ActionTime = DateTime.UtcNow,
                LicenseKey = license.Key,
                Note = dto.Note
            };

            _context.UsageLogs.Add(u);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("selly/pay")]
        public async Task<IActionResult> ValidateSellyPayment([FromBody] SellyPaymentDto dto)
        {
            // Instagram Bot
            if (dto.product_id != "b22f4aa0") return BadRequest();

            LicenseKey l = new LicenseKey()
            {
                ExpiresOn = DateTime.UtcNow.AddYears(1),
                Email = dto.email,
                Key = Guid.NewGuid().ToString(),
                SoftwareId = Convert.ToInt32(_config["InstagramBotId"])
            };

            _context.LicenseKeys.Add(l);
            await _context.SaveChangesAsync();

            var client = new SendGridClient("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");
            var from = new EmailAddress("licensing@deviar.net", "Deviar Licensing");
            var to = new EmailAddress(dto.email, "");

            var content = $"Your License Key: <b>{l.Key}</b>.";

            var msg = MailHelper.CreateSingleEmail(from, to, $"Software License For \"{_context.Software.First(x => x.Id == Convert.ToInt32(_config["InstagramBotId"])).Name}\"", content, content);

            var response = await client.SendEmailAsync(msg);

            return Ok();
        }

        [Route("license/validate")]
        public async Task<IActionResult> ValidateLicense([FromBody] ValidateLicenseDto dto)
        {
            var license = _context.LicenseKeys.Include(x => x.Software)
                .FirstOrDefault(x => x.Key == dto.Key && x.SoftwareId == dto.SoftwareId);

            if (license == null)
            {
                return BadRequest(new
                {
                    Message = "The specified license key does not exist."
                });
            }

            if (license.Locked)
            {
                return BadRequest(new
                {
                    Message = "Your license key has been suspended. Please contact support."
                });
            }

            if (license.HardwareId != null && license.HardwareId != dto.Fingerprint)
            {
                return BadRequest(new
                {
                    Message = "Registered hardware fingerprint mismatch."
                });
            }

            if (license.ExpiresOn <= DateTime.UtcNow)
            {
                return BadRequest(new
                {
                    Message = "Your license has expired."
                });
            }

            if (license.HardwareId == null)
            {
                license.HardwareId = dto.Fingerprint;
            }

            license.LastUsed = DateTime.UtcNow;

            var u = new UsageLog()
            {
                ActionTime = DateTime.UtcNow,
                LicenseKey = license.Key,
                Note = "Launched the software."
            };

            _context.UsageLogs.Add(u);

            _context.LicenseKeys.Update(license);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
