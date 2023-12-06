using Stripe;

namespace Licensing.Deviar.Helpers
{
    public class StripeHelper
    {
        public static decimal GetProductPrice(string priceId)
        {
            try
            {
                var priceService = new PriceService();
                var price = priceService.Get(priceId);

                decimal amount = (decimal)price.UnitAmount! / 100; // Convert from cents to dollars

                return amount;
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Error: {ex}");
                // Handle the error as needed
                return 0; // Or throw an exception
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle the error as needed
                return 0; // Or throw an exception
            }
        }
    }
}
