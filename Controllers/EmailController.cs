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
        private readonly IEmailService _emailServiceMicrosoft;
        private readonly IEmailService _emailServiceSendGrid;

        public EmailController(IServiceProvider serviceProvider)
        {
            _emailServiceMicrosoft = ActivatorUtilities.CreateInstance<EmailServiceMicrosoft>(serviceProvider);
            _emailServiceSendGrid = ActivatorUtilities.CreateInstance<EmailServiceSendGrid>(serviceProvider);
        }

        [HttpPost]
        [Route("SendIndividualEmail/Microsoft")]
        public async Task<ActionResult> PostMicrosoft(IndividualEmailShippingRequest shippingRequest)
        {
            var responseEmail = await _emailServiceMicrosoft.SendIndividualEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new {status= 200, message = "Email enviado con exito!"});
        }

        [HttpPost]
        [Route("SendMultipleEmail/Microsoft")]
        public async Task<ActionResult> PostMicrosoft(MultipleEmailsShippingRequest shippingRequest)
        {
            var responseEmail = await _emailServiceMicrosoft.SendMultipleEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new { status = 200, message = "Emails enviados con exito!" });
        }

        [HttpPost]
        [Route("SendIndividualEmail/SendGrid")]
        public async Task<ActionResult> PostSendGrid(IndividualEmailShippingRequest shippingRequest)
        {
            var responseEmail = await _emailServiceSendGrid.SendIndividualEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new { status = 200, message = "Email enviado con exito!" });
        }

        [HttpPost]
        [Route("SendMultipleEmail/SendGrid")]
        public async Task<ActionResult> PostSendGrid(MultipleEmailsShippingRequest shippingRequest)
        {
            var responseEmail = await _emailServiceSendGrid.SendMultipleEmailAsync(shippingRequest);
            if (responseEmail == false) return BadRequest(new { message = "Error al enviar el email" });
            return Ok(new { status = 200, message = "Emails enviados con exito!" });
        }
    }
}
