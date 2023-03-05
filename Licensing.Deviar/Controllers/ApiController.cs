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

        [HttpGet]
        [Route("software/{id}")]
        public IActionResult GetSoftwareInfo(int id)
        {
            var software = _context.Software.FirstOrDefault(x => x.Id == id);

            if (software == null)
                return BadRequest();

            return Ok(new
            {
                software.Name,
                software.Description,
                software.Version
            });
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

        [Route("license/validate")]
        public async Task<IActionResult> ValidateLicense([FromBody] ValidateLicenseDto dto)
        {
            var license = _context.LicenseKeys.Include(x => x.Software)
                .FirstOrDefault(x => x.Key == dto.Key && x.SoftwareId == dto.SoftwareId);

            if (license == null) return BadRequest(new { Message = "The specified license key does not exist." });

            if (license.Locked)
                return BadRequest(new { Message = "Your license key has been suspended. Please contact support." });

            if (license.HardwareId != null && license.HardwareId != dto.Fingerprint)
                return BadRequest(new { Message = "Registered hardware fingerprint mismatch." });

            if (license.ExpiresOn <= DateTime.UtcNow)
                return BadRequest(new { Message = "Your license has expired." });

            license.HardwareId ??= dto.Fingerprint;
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