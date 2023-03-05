namespace Licensing.Deviar.Models
{
    using Newtonsoft.Json;

    public class SellyPaymentDto
    {
        public string product_id { get; set; }

        public string email { get; set; }

        public string ip_address { get; set; }
    }
}