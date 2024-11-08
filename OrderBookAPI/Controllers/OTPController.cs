using Microsoft.AspNetCore.Mvc;
using OrderBookAPI.Models;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace OrderBookAPI.Controllers
{
    public class OTPController : Controller
    {
        private readonly string _authyApiKey;

        public OTPController(IConfiguration configuration)
        {
            _authyApiKey = configuration["Twilio:ServiceSid"];
            TwilioClient.Init(configuration["Twilio:AccountSid"], configuration["Twilio:AuthToken"]);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            // Brug telefonnummeret fra request-objektet
            var verification = await VerificationResource.CreateAsync(
                to: request.PhoneNumber, // Brug request.PhoneNumber
                channel: "sms",
                pathServiceSid: _authyApiKey
            );

            // Returnér en struktureret JSON-respons
            return Ok(new { status = verification.Status, sid = verification.Sid });
        }


        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: request.PhoneNumber, // Brug request.PhoneNumber
                code: request.Code,      // Brug request.Code
                pathServiceSid: _authyApiKey
            );

            return Ok(new { status = verificationCheck.Status });
        }

    }
}
