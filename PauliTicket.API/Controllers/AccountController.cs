using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PauliTicket.Application.Contracts.Identity;
using PauliTicket.Application.Contracts.Infrastructure;
using PauliTicket.Application.Models.Authentication;
using PauliTicket.Application.Models.Mail;

namespace PauliTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
          
        public AccountController(IAuthenticationService authenticationService, IEmailService emailService)
        {
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            var response = await _authenticationService.RegisterAsync(request);
            var callbackUrl = Url.RouteUrl("ConfirmEmail", new { userId = response.UserId, code = response.Code }, protocol: HttpContext.Request.Scheme);
            var email = new Email() { To = "paulooo_rc@hotmail.com", Body = $"Your account was suscesfully created, follow the next link to cconfirm your account: {callbackUrl}", Subject = "Confirm your account!" };
            await _emailService.SendEmail(email);
            return Ok(response);
        }

        [HttpGet("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<ActionResult<AuthenticationResponse>> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound("Invalid Link");
            }

             var result = await _authenticationService.ConfirmEmail(userId, code);

            if (result)
            {
                return Ok("Email confirmed!");
            }

            return Ok();
        }
    }
}

