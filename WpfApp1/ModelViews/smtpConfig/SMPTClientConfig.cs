namespace Barco.ModelViews.smtpConfig
{
    // object similar to json structure to get data from the json file
    //SMPTClientConfig being the section name in the jsonfile
    //bianca
    public class SMPTClientConfig
    {
        public string Username { get; set; }
        public string SMTPPassword { get; set; }
        public string SMPTHost { get; set; }
    }
}
