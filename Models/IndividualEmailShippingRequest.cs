using EmailsApi.Interfaces;

namespace EmailsApi.Models
{
    public class IndividualEmailShippingRequest : IEmailShippingRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}
