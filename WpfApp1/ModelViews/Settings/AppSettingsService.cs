using System;
using System.IO;
using Barco.ModelViews.smtpConfig;
using Microsoft.Extensions.Configuration;

namespace Barco.ModelViews.Settings
{
    //bianca
   
    public class AppSettingsService<TAppSettings>
    {
       
        public static AppSettingsService<TAppSettings> Instance { get; } = new AppSettingsService<TAppSettings>();

        private IConfigurationRoot _configRoot { get; set; }

        private TAppSettings _appSettings;

        public TAppSettings AppSettings { get => _appSettings; }

        private string _appSettingsBasePath
        {
            get => @"C:\Users\cbian\source\repos\Werkplekleren1\2021WPL1Groep2\ProjectWPL1\WpfApp1\appSettings";
        }

        public AppSettingsService()
        {
            BuildConfigurationRoot();

            _appSettings = _configRoot.Get<TAppSettings>();
        }

        private void BuildConfigurationRoot()
        {
         
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(_appSettingsBasePath);
            AddJSONSettingsFileToBuilder(ref configBuilder, "appSettings.json");
            _configRoot = configBuilder.Build();
        }

   
        private void AddJSONSettingsFileToBuilder(ref ConfigurationBuilder builder, string filename)
        {
            var fullFilePath = Path.Combine(_appSettingsBasePath, filename);
            if (File.Exists(fullFilePath))
            {
                builder.AddJsonFile(filename);
            }
        }

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
