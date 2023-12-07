using System.Net.Http.Headers;
using Licensing.Deviar.Data;
using Licensing.Deviar.Data.Migrations;
using Licensing.Deviar.Helpers;
using Licensing.Deviar.Models;
using Licensing.Deviar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Identity;

namespace Licensing.Deviar.Controllers
{
    [Route("api/software")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SoftwareController(ApplicationDbContext context,
            UserManager<AppUser> userManager,
            IWebHostEnvironment environment,
            MailService mailService)
        : Controller
    {

        [AllowAnonymous]
        [HttpGet("public/list/{userId}")]
        public async Task<IActionResult> ListPublicSoftware(string userId)
        {
            var software = context.Software.Where(x => x.UserId == userId);

            return Ok(software.Select(x => new SoftwareDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = StripeHelper.GetProductPrice(x.StripeProductId ?? "No Product ID"),
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                StripeProductId = x.StripeProductId,
                Version = x.Version,
                UserId = x.UserId
            }));
        }

        [HttpPost("reseller/add")]
        public async Task<IActionResult> AddReseller([FromBody] ResellerDto dto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            if (user!.Email != "contact@deviar.net")
                return BadRequest();

            if (string.IsNullOrEmpty(dto.Email))
                return BadRequest(new
                {
                    Message = "Please enter a valid email."
                });

            var software = await context.Software.FirstOrDefaultAsync(x => x.Id == dto.SoftwareId);
            var resellerAcc = await context.Users.FirstOrDefaultAsync(x => x.ResellerId != null && x.Email == dto.Email);

            // Reseller already exists, just add the new software to their reseller account.
            if (resellerAcc != null)
            {
                var resellerSoftware = new ResellerSoftware
                {
                    SoftwareId = software.Id,
                    ResellerId = resellerAcc.ResellerId!.Value
                };

                context.ResellerSoftware.Add(resellerSoftware);
                await context.SaveChangesAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(dto.Code))
                    return BadRequest(new
                    {
                        Message = "Please enter a valid code."
                    });

                if (dto.Percentage <= 0)
                    return BadRequest(new
                    {
                        Message = "Please enter a valid percentage."
                    });

                var reseller = new Reseller
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Code = dto.Code,
                    SoftwareId = dto.SoftwareId,
                    Added = DateTime.UtcNow,
                    Percentage = dto.Percentage
                };

                context.Resellers.Add(reseller);
                await context.SaveChangesAsync();

                var resellerSoftware = new ResellerSoftware
                {
                    SoftwareId = software.Id,
                    ResellerId = reseller.Id
                };

                context.ResellerSoftware.Add(resellerSoftware);
                await context.SaveChangesAsync();

                resellerAcc = new AppUser()
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Reseller = reseller
                };

                var password = Guid.NewGuid() + "aA";
                var resp = await userManager.CreateAsync(resellerAcc, password);

                if (!resp.Succeeded)
                {
                    return BadRequest(new
                    {
                        Message = resp.Errors.First().Description
                    });
                }

                await mailService.Send($"You've been added as a seller of {software.Name}.", $"Hi, {dto.Name}! You've been added as a reseller of {software.Name} on Deviar! You can access your reseller account <a href=\"https://licensing.deviar.net\">here</a>. <br /><br /><b>Email:</b> {dto.Email}<br /><b>Password:</b> {password}", dto.Email);

                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditSoftware([FromBody] SoftwareDto dto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var software = context.Software.FirstOrDefault(x => x.Id == dto.Id && x.UserId == user.Id);

            if (software == null)
                return BadRequest();

            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest(new
                {
                    Message = "Please enter a name for this software."
                });
            }

            software.StripeProductId = dto.StripeProductId;
            software.Name = dto.Name;
            software.Description = dto.Description;
            software.Version = dto.Version;

            context.Software.Update(software);
            await context.SaveChangesAsync().ConfigureAwait(false);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] SoftwareDto dto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var software = await context.Software.FirstOrDefaultAsync(x => x.Id == dto.Id && user.Id == x.UserId);

            if (software == null)
            {
                return BadRequest(new
                {
                    Message = "Unable to find the specified software."
                });
            }

            context.Software.Remove(software);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SoftwareDto dto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            if (context.Software.Any(x => x.Name == dto.Name))
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

            context.Software.Add(software);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetSoftwareById(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var x = context.Software.AsNoTracking().Include(y => y.LicenseKeys)
                .FirstOrDefault(y => y.UserId == user.Id && y.Id == id);

            if (x == null)
                return BadRequest();

            return Ok(new SoftwareDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                StripeProductId = x.StripeProductId,
                Version = x.Version,
                LicenseKeys = x.LicenseKeys.Select(y => new LicenseKeyDto()
                {
                    ActivatedOn = y.ActivatedOn,
                    CreatedOn = y.CreatedOn,
                    ExpiresOn = y.ExpiresOn,
                    Notes = y.Notes,
                    Email = y.Email,
                    HardwareId = y.HardwareId,
                    Id = y.Id,
                    Key = y.Key,
                    Name = y.Name,
                    LastUsed = y.LastUsed,
                    Locked = y.Locked
                }).ToArray()
            });
        }

        [HttpPost("remove/reseller")]
        public async Task<IActionResult> RemoveReseller([FromBody] ResellerDto dto)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var reseller = await context.Resellers.FirstOrDefaultAsync(x => x.Id == dto.Id);

            var software = await context.ResellerSoftware.FirstOrDefaultAsync(x => x.SoftwareId == dto.SoftwareId && x.ResellerId == reseller.Id);

            context.ResellerSoftware.Remove(software);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("resellers/{id}")]
        public async Task<IActionResult> GetResellers(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            var software = await context.Software
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Resellers)
                .ThenInclude(x => x.Reseller)
                .ThenInclude(reseller => reseller.Logs)
                .ThenInclude(resellerLog => resellerLog.LicenseKey)
                .FirstOrDefaultAsync(x => x.UserId == user!.Id && x.Id == id);

            var resellers = software!.Resellers.Select(x => new ResellerDto
            {
                Id = x.ResellerId,
                Name = x.Reseller.Name,
                Email = x.Reseller.Email,
                Code = x.Reseller.Code,
                UnitsSold = x.Reseller.Logs.Count(y => y.LicenseKey.SoftwareId == x.SoftwareId),
                SoftwareId = x.SoftwareId,
                Added = x.Reseller.Added,
                Percentage = x.Reseller.Percentage
            });

            return Ok(resellers);
        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetSoftware()
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            var software = context.Software.Where(x => x.UserId == user!.Id);

            return Ok(software.Select(x => new SoftwareDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Resellers = x.Resellers.Count(),
                Licenses = x.LicenseKeys.Count(),
                CreatedOn = x.CreatedOn,
                Version = x.Version,
                UserId = x.UserId
            }));
        }
    }
}