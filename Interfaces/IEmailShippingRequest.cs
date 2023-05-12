namespace EmailsApi.Interfaces
{
    public interface IEmailShippingRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
