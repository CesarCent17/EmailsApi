using System.Diagnostics.Contracts;
using EmailsApi.Models;

namespace EmailsApi.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendIndividualEmailAsync(IndividualEmailShippingRequest shippingRequest);
        Task<bool> SendMultipleEmailAsync(MultipleEmailsShippingRequest shippingRequest);

    }
}
