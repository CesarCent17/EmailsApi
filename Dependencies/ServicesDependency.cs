using EmailsApi.Interfaces;
using EmailsApi.Services;
using EmailsApi.Configs;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Net;

namespace EmailsApi.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<SmtpConfig>(configuration.GetSection("Smtp"));
            services.AddTransient<SmtpClient>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<SmtpConfig>>().Value;

                var smtpClient = new SmtpClient(options.Server, options.Port)
                {
                    Credentials = new NetworkCredential(options.Username, options.Password),
                    EnableSsl = options.EnableSsl
                };

                return smtpClient;
            });
        }
    }
}
