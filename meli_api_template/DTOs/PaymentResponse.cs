namespace meli_api_template.DTOs
{
    public class PaymentResponse
    {
        public required string PreferenceId { get; set; }
        public required string InitPoint { get; set; }
        public required string SandboxInitPoint { get; set; }
    }
}
