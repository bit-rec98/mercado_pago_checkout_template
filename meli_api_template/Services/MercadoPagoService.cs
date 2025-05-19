using meli_api_template.Configuration;
using meli_api_template.DTOs;
using meli_api_template.Services.Interfaces;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using Microsoft.Extensions.Options;

namespace meli_api_template.Services
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly MercadoPagoConfiguration _settings;

        public MercadoPagoService(IOptions<MercadoPagoConfiguration> settings)
        {
            _settings = settings.Value;
            MercadoPagoConfig.AccessToken = _settings.AccessToken;
        }

        public async Task<PaymentResponse> CreatePreferenceAsync(PaymentRequest request)
        {
            var client = new PreferenceClient();
            var preference = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = request.Title,
                        Quantity = request.Quantity,
                        UnitPrice = request.Price,
                        Description = request.Description,
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = _settings.BackUrl + "/success",
                    Failure = _settings.BackUrl + "/failure",
                    Pending = _settings.BackUrl + "/pending"
                },
                AutoReturn = "approved",
                Payer = new PreferencePayerRequest
                {
                    Email = request.PayerEmail
                }
            };

            var result = await client.CreateAsync(preference);

            return new PaymentResponse
            {
                PreferenceId = result.Id,
                InitPoint = result.InitPoint,
                SandboxInitPoint = result.SandboxInitPoint
            };
        }

        public async Task<bool> VerifyPaymentAsync(string paymentId)
        {
            try
            {
                // Create payment client
                var client = new MercadoPago.Client.Payment.PaymentClient();

                // Get payment information
                var payment = await client.GetAsync(long.Parse(paymentId));

                // Check if payment status is approved
                return payment.Status == "approved";
            }
            catch (Exception ex)
            {
                // Log error or handle exception
                Console.WriteLine($"Error verifying payment: {ex.Message}");
                return false;
            }
        }
    }
}
