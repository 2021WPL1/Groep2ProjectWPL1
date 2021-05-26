using System;
using System.IO;
using Barco.ModelViews.smtpConfig;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IO;
using System.Reflection;

namespace Barco.ModelViews.Settings
{
    //bianca
   
    public class AppSettingsService<TAppSettings>
    {
       
        public static AppSettingsService<TAppSettings> Instance { get; } = new AppSettingsService<TAppSettings>();

        private IConfigurationRoot _configRoot { get; set; }

        private TAppSettings _appSettings;

        public TAppSettings AppSettings { get => _appSettings; }

        private string _appSettingsBasePath =>
            // still need to be fixed 
            //Path.Combine(Directory.GetCurrentDirectory(), "Barco");
            //Directory.GetCurrentDirectory();
            //@"C:\Users\laure\Documents\Documents\vives\werkplekleren\Barco\WpfApp1\appSettings";
            Path.Combine(System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp3.1", ""), "appSettings");
            

       
        public AppSettingsService()
        {
            BuildConfigurationRoot();

            _appSettings = _configRoot.Get<TAppSettings>();
        }

        //bianca ->  building the configuration root based of the json file
        private void BuildConfigurationRoot()
        {
         
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(_appSettingsBasePath);
            AddJSONSettingsFileToBuilder(ref configBuilder, "appSettings.json");
            _configRoot = configBuilder.Build();
        }

   //bianca 
   private void AddJSONSettingsFileToBuilder(ref ConfigurationBuilder builder, string filename)
        {
            var fullFilePath = Path.Combine(_appSettingsBasePath, filename);
            if (File.Exists(fullFilePath))
            {
                builder.AddJsonFile(filename);
            }
        }


        // Laurent-Bianca
        public ConfigurationQueryResult<TSection> GetConfigurationSection<TSection>(string sectionName)
        {
            var result = new ConfigurationQueryResult<TSection>() { Status = QueryStatus.Ok };
            if (!_configRoot.GetSection(sectionName).Exists())
            {
                result.Status = QueryStatus.HasError;
                result.Error = new Exception("The sectionname is not found in the settingsfile");
                return result;
            }
  
            result.QueryResult = _configRoot.GetSection(sectionName).Get<TSection>();
            return result;
        }
    }
}
