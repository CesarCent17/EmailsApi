namespace EmailsApi.Configs
{
    public class SmtpConfig
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
