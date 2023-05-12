using EmailsApi.Interfaces;
using EmailsApi.Models;
using System.Net.Mail;


namespace EmailsApi.Services
{
    public class EmailServiceMicrosoft : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;
        public EmailServiceMicrosoft(SmtpClient smtpClient, IConfiguration configuration) 
        {
            _smtpClient = smtpClient;
            _configuration = configuration;
        }
        public async Task<bool> SendIndividualEmailAsync(IndividualEmailShippingRequest shippingRequest)
        {
            try
            {
                //var from = _configuration.GetSection("Smtp")["From"];
                var from = _configuration.GetValue<string>("Smtp:Username");
                var message = new MailMessage();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(shippingRequest.To));
                message.Subject = shippingRequest.Subject;
                message.Body = shippingRequest.Body;
                message.IsBodyHtml = true;

                await _smtpClient.SendMailAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendMultipleEmailAsync(MultipleEmailsShippingRequest shippingRequest)
        {
            try
            {
                var receivers = shippingRequest.Receivers.ToList();
                foreach (var receiver in receivers)
                {
                    var from = _configuration.GetValue<string>("Smtp:Username");
                    var message = new MailMessage();
                    message.From = new MailAddress(from);
                    message.To.Add(new MailAddress(receiver.MailAddress));

                    message.Subject = shippingRequest.Subject;
                    message.Body = shippingRequest.Body;
                    message.IsBodyHtml = true;

                    await _smtpClient.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
