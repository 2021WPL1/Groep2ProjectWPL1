using Barco.ModelViews.smtpConfig;
namespace Barco.ModelViews.Settings
{
    public class AppSettings
    {
        //bianca
       // mapping the configuration to a POCO class.
       public SMPTClientConfig SmptClientConfig { get; set; }
        public EmailAdresses EmailAdresses { get; set; }
    }
}
