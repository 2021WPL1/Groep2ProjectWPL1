namespace Barco.ModelViews.smtpConfig
{
    public enum MailSendingStatus {Ok, HasError}

    public class MailResult
    {
        public  MailSendingStatus Status { get; set; }
        public string Message { get; set; }
    }
}
