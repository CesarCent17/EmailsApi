using EmailsApi.Interfaces;
using EmailsApi.Models;
using EmailsApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmailsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService) 
        {
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendIndividualEmail")]
        public async Task<ActionResult> Post(IndividualEmailShippingRequest shippingRequest)
        {
            var responseEmail = await _emailService.SendIndividualEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new {status= 200, message = "Email enviado con exito!"});
        }

        [HttpPost]
        [Route("SendMultipleEmail")]
        public async Task<ActionResult> Post(MultipleEmailsShippingRequest shippingRequest)
        {
            var responseEmail = await _emailService.SendMultipleEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new { status = 200, message = "Email enviado con exito!" });
        }
    }
}
