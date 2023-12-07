using Licensing.Deviar.Data;
using Licensing.Deviar.Helpers;
using Licensing.Deviar.Models;
using Licensing.Deviar.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace Licensing.Deviar.Controllers
{
    [Route("api/reseller")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResellerController(ApplicationDbContext context,
        UserManager<AppUser> userManager,
        IWebHostEnvironment environment,
        MailService mailService) : Controller
    {
        [HttpGet("list")]
        public async Task<IActionResult> ListSoftware()
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var software = context.ResellerSoftware.Include(x => x.Software).Where(x => x.ResellerId == user.ResellerId).Select(x => x.Software);

            return Ok(software.Select(x => new SoftwareDto
            {
                Id = x.Id,
                Name = x.Name,
                AmountSold = context.ResellerLogs.Count(y => y.ResellerId == user.ResellerId!.Value && y.LicenseKey.SoftwareId == x.Id),
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                StripeProductId = x.StripeProductId!,
                Price = StripeHelper.GetProductPrice(x.StripeProductId!)
            }));
        }

        [HttpGet("start/{softwareId}")]
        public async Task<IActionResult> StartPurchase(int softwareId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

            var reseller = await context.Resellers.FirstOrDefaultAsync(x => x.Id == user.ResellerId);

            if (reseller == null)
            {
                return BadRequest(new
                {
                    Message = "Reseller not found."
                });
            }

            var software = context.Software.FirstOrDefault(x => x.Id == softwareId);

            if (software == null)
            {
                return BadRequest(new
                {
                    Message = "Software not found."
                });
            }

            if (string.IsNullOrEmpty(software.StripeProductId))
                return BadRequest(new
                {
                    Message = "Software doesn't have a Stripe Product ID with it."
                });

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        Price = software.StripeProductId,
                        Quantity = 1,
                    },
                },
                Metadata = new Dictionary<string, string>()
                {
                    { "SoftwareId", software.Id.ToString() },
                    { "Reseller", reseller.Code }
                },
                Mode = "payment",
                SuccessUrl = "https://deviar.net/payment/success"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Ok(new
            {
                Url = session.Url
            });
        }
    }
}
