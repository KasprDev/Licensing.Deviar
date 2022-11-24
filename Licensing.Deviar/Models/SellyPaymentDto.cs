namespace Licensing.Deviar.Models
{
    public class SellyPaymentDto
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string email { get; set; }
        public string ip_address { get; set; }
        public string value { get; set; }
        public string gateway { get; set; }
    }
}
