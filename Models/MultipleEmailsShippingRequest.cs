using EmailsApi.Interfaces;

namespace EmailsApi.Models
{
    public class MultipleEmailsShippingRequest : IEmailShippingRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Receiver> Receivers { get; set; }
    }
}
