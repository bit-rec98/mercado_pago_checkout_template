using meli_api_template.DTOs;

namespace meli_api_template.Services.Interfaces
{
    public interface IMercadoPagoService
    {
        Task<PaymentResponse> CreatePreferenceAsync(PaymentRequest request);
        Task<bool> VerifyPaymentAsync(string paymentId);
    }
}
