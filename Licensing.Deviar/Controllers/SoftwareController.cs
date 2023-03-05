using System.Net.Http.Headers;
using Licensing.Deviar.Data;
using Licensing.Deviar.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Licensing.Deviar.Controllers
{
    [Route("api/software")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SoftwareController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public SoftwareController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditSoftware([FromBody] SoftwareDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var software = _context.Software.FirstOrDefault(x => x.Id == dto.Id && x.UserId == user.Id);

            if (software == null)
                return BadRequest();

            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest(new
                {
                    Message = "Please enter a name for this software."
                });
            }

            software.SellyProductId = dto.SellyProductId;
            software.Name = dto.Name;
            software.Description = dto.Description;
            software.Version = dto.Version;
            _context.Software.Update(software);

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SoftwareDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            if (_context.Software.Any(x => x.Name == dto.Name))
            {
                return BadRequest(new
                {
                    Message = "You already have existing software with this name."
                });
            }

            var software = new Software()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = user.Id,
                Name = dto.Name,
                Description = dto.Description,
                Version = 1.0
            };

            _context.Software.Add(software);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetSoftwareById(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var x = _context.Software.AsNoTracking().Include(y => y.LicenseKeys)
                .FirstOrDefault(y => y.UserId == user.Id && y.Id == id);

            if (x == null)
                return BadRequest();

            return Ok(new SoftwareDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                SellyProductId = x.SellyProductId,
                Version = x.Version,
                LicenseKeys = x.LicenseKeys.Select(y => new LicenseKeyDto()
                {
                    ActivatedOn = y.ActivatedOn,
                    CreatedOn = y.CreatedOn,
                    ExpiresOn = y.ExpiresOn,
                    HardwareId = y.HardwareId,
                    Id = y.Id,
                    Key = y.Key,
                    LastUsed = y.LastUsed,
                    Locked = y.Locked
                }).ToArray()
            });
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetSoftware()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            var software = _context.Software.Where(x => x.UserId == user!.Id);

            return Ok(software.Select(x => new SoftwareDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Licenses = x.LicenseKeys.Count(),
                CreatedOn = x.CreatedOn,
                Version = x.Version
            }));
        }
    }
}