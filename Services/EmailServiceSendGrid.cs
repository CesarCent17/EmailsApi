using EmailsApi.Configs;
using EmailsApi.Interfaces;
using EmailsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace EmailsApi.Services
{
    public class EmailServiceSendGrid : IEmailService
    {
        private readonly IConfiguration _configuration;


        public EmailServiceSendGrid(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        public async Task<bool> SendIndividualEmailAsync([FromForm] IndividualEmailShippingRequest shippingRequest)
        {
            try
            {
                var apiKey = _configuration["ApiKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_configuration["From"]);
                var subject = shippingRequest.Subject;
                var to = new EmailAddress(shippingRequest.To);
                var PdfFile = shippingRequest.PdfFile;
                var plainTextContent = shippingRequest.Body;
                var htmlContent = $"<strong>{shippingRequest.Body}</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                using (var ms = new MemoryStream())
                {
                    PdfFile.CopyTo(ms);
                    var archivoBytes = ms.ToArray();
                    msg.AddAttachment(PdfFile.FileName, Convert.ToBase64String(archivoBytes));
                }

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
            try
            {
                var receivers = shippingRequest.Receivers.ToList();
                var apiKey = _configuration["ApiKey"];

                foreach (var receiver in receivers)
                {
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress(_configuration["From"]);
                    var subject = shippingRequest.Subject;
                    var to = new EmailAddress(receiver.MailAddress);
                    var plainTextContent = shippingRequest.Body;
                    var htmlContent = $"<strong>{shippingRequest.Body}</strong>";
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
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
