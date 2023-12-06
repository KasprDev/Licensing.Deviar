using Licensing.Deviar.Data;
using Licensing.Deviar.Models;
using Licensing.Deviar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace Licensing.Deviar.Controllers
{
    public class StripeController(IConfiguration config, ApplicationDbContext context, MailService mailService) : Controller
    {
        [HttpPost("purchase/start")]
        public async Task<IActionResult> CreateCharge([FromBody] CreateCustomerDto dto)
        {
            var software = context.Software.FirstOrDefault(x => x.Id == dto.SoftwareId);

            if (software == null)
            {
                return BadRequest(new
                {
                    Message = "Unable to find the specified software."
                });
            }

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
					{ "SoftwareId", software.Id.ToString() }
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

        [HttpPost("stripe/webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
	        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

	        await mailService.Send("payload", json, "contact@deviar.net");

	        try
	        {
		        var stripeEvent = EventUtility.ConstructEvent(
			        json,
			        Request.Headers["Stripe-Signature"],
			        config["Stripe:WebhookSecret"]);

		        if (stripeEvent.Type != Events.CheckoutSessionCompleted) return BadRequest("Payment didn't succeed.");

				// The data isn't PaymentIntent data.
		        if (stripeEvent.Data.Object is not Session data) return BadRequest("Not payment intent data.");

		        // The Software ID isn't present in the Stripe response.
		        if (!data.Metadata.TryGetValue("SoftwareId", out var value)) return BadRequest("Software Id not in the metadata.");

		        var software = await context.Software.FirstOrDefaultAsync(x => x.Id == Convert.ToInt32(value));

		        if (software == null)
			        return BadRequest("Unable to find the specified Software ID.");

		        var key = new LicenseKey
		        {
			        Email = data.CustomerDetails.Email,
					Name = data.CustomerDetails.Name,
			        ActivatedOn = DateTime.UtcNow,
			        CreatedOn = DateTime.UtcNow,
			        ExpiresOn = DateTime.UtcNow.AddYears(1),
			        Key = Guid.NewGuid().ToString(),
			        TransactionId = data.Id,
			        SoftwareId = software.Id,
		        };

		        context.LicenseKeys.Add(key);
		        await context.SaveChangesAsync().ConfigureAwait(false);

		        await mailService.Send($"Your License Key for {software.Name}.",
			        $"Hi there! Your license key is <b>{key.Key}</b>", data.CustomerDetails.Email);

		        return Ok();
	        }
	        catch (StripeException e)
	        {
		        // Handle Stripe exceptions
		        return BadRequest($"Webhook error: {e.Message}");
	        }
	        catch (Exception ex)
	        {
		        // Handle other exceptions
		        return StatusCode(500, $"Internal server error: {ex.Message}");
	        }
		}
    }
}
