namespace meli_api_template.DTOs
{
    public class PaymentRequest
    {
        public required string Title { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
        public required string Description { get; set; }
        public required string PayerEmail { get; set; }
    }
}
