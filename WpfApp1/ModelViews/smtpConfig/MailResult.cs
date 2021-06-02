namespace Barco.ModelViews.smtpConfig
{
    //not needed anymore/replaced with ConfigurationQueryResult
    //bianca
    //  used in the console to check status and generate messages to the user
    public enum MailSendingStatus {Ok, HasError}
    //used to return objects that serve as a transport class between class and console
    public class MailResult
    {
        public  MailSendingStatus Status { get; set; }
        public string Message { get; set; }
    }
}
