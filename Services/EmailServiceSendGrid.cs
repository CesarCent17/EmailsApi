using EmailsApi.Configs;
using EmailsApi.Interfaces;
using EmailsApi.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace EmailsApi.Services
{
    public class EmailServiceSendGrid : IEmailService
    {
        private readonly SendGridConfig _sendGridConfig;

        public EmailServiceSendGrid(IOptions<SendGridConfig> options) 
        { 
            _sendGridConfig = options.Value;
        }
        public async Task<bool> SendIndividualEmailAsync(IndividualEmailShippingRequest shippingRequest)
        {
            try
            {
                var apiKey = _sendGridConfig.ApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_sendGridConfig.From);
                var subject = shippingRequest.Subject;
                var to = new EmailAddress(shippingRequest.To);
                var plainTextContent = shippingRequest.Body;
                var htmlContent = $"<strong>{shippingRequest.Body}</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> SendMultipleEmailAsync(MultipleEmailsShippingRequest shippingRequest)
        {
            return false;
        }
    }
}
