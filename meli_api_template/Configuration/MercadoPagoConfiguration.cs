namespace meli_api_template.Configuration
{
    public class MercadoPagoConfiguration
    {
        public required string AccessToken { get; set; }
        public required string PublicKey { get; set; }
        public required string BackUrl { get; set; }
    }
}
