using Licensing.Deviar.Data;
using Licensing.Deviar.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Licensing.Deviar.Controllers
{
    [Route("api/key")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LicenseKeyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public LicenseKeyController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("unlock")]
        public async Task<IActionResult> UnlockLicense([FromBody] LicenseKeyDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var license = _context.LicenseKeys.Include(x => x.Software)
                .FirstOrDefault(x => x.Key == dto.Key && x.Software.UserId == user!.Id);

            if (license == null)
                return BadRequest();

            license.HardwareId = null;
            _context.LicenseKeys.Update(license);

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditLicense([FromBody] LicenseKeyDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var license = _context.LicenseKeys.Include(x => x.Software)
                .FirstOrDefault(x => x.Key == dto.Key && x.Software.UserId == user!.Id);

            if (license == null)
                return BadRequest();

            license.ExpiresOn = dto.ExpiresOn;
            license.Name = dto.Name;
            license.Notes = dto.Notes;
            license.Email = dto.Email;

            _context.LicenseKeys.Update(license);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("suspend")]
        public async Task<IActionResult> SuspendLicense([FromBody] LicenseKeyDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            var license = _context.LicenseKeys.FirstOrDefault(x => x.Key == dto.Key);

            if (license == null)
                return BadRequest();

            license.Locked = !license.Locked;
            _context.LicenseKeys.Update(license);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateKey([FromBody] LicenseKeyDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            if (!_context.Software.Any(x => x.Id == dto.SoftwareId))
            {
                return BadRequest(new
                {
                    Message = "Unable to find the specified software."
                });
            }

            if (dto.ExpiresOn < DateTime.UtcNow)
            {
                return BadRequest(new
                {
                    Message = "Expiry date must be a date later than today."
                });
            }

            var key = new LicenseKey()
            {
                SoftwareId = dto.SoftwareId,
                Key = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = dto.ExpiresOn,
                Email = dto.Email,
                Notes = dto.Notes
            };

            _context.LicenseKeys.Add(key);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(new
            {
                Message = key.Key
            });
        }

        [HttpGet]
        [Route("usage/logs/{key}")]
        public async Task<IActionResult> GetUsageLogs(string key)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var l = _context.LicenseKeys.Include(x => x.Software).FirstOrDefault(x => x.Key == key);

            if (l == null)
                return BadRequest();

            var usage = _context.UsageLogs.Where(x => x.LicenseKey == key).OrderByDescending(x => x.ActionTime)
                .Take(200);

            return Ok(usage.Select(x => new UsageLogDto()
            {
                Note = x.Note,
                ActionTime = x.ActionTime
            }));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteKey([FromBody] LicenseKeyDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var key = _context.LicenseKeys.Include(x => x.Software).FirstOrDefault(x => x.Key == dto.Key);

            if (key == null)
            {
                return BadRequest(new
                {
                    Message = "Unable to find the specified key."
                });
            }

            if (key.Software.UserId != user!.Id)
                return Unauthorized();

            _context.LicenseKeys.Remove(key);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }
    }
}