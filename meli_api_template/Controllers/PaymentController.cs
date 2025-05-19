using meli_api_template.DTOs;
using meli_api_template.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace meli_api_template.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IMercadoPagoService mercadoPagoService) : ControllerBase
    {
        private readonly IMercadoPagoService _mercadoPagoService = mercadoPagoService;

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            var response = await _mercadoPagoService.CreatePreferenceAsync(request);
            return Ok(response);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] string payment_id, [FromQuery] string status)
        {
            if (status == "approved")
            {
                var verified = await _mercadoPagoService.VerifyPaymentAsync(payment_id);
                if (verified)
                {
                    return Ok(new { Message = "Payment successful" });
                }
            }

            return BadRequest(new { Message = "Payment failed or pending" });
        }
    }
}
