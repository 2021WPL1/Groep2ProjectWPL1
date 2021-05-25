using System;
using System.IO;
using Barco.ModelViews.smtpConfig;
using Microsoft.Extensions.Configuration;

namespace Barco.ModelViews.Settings
{
   
    public class AppSettingsService<TAppSettings>
    {
       
        public static AppSettingsService<TAppSettings> Instance { get; } = new AppSettingsService<TAppSettings>();

        private IConfigurationRoot _configRoot { get; set; }

        private TAppSettings _appSettings;

        public TAppSettings AppSettings { get => _appSettings; }

        private string _appSettingsBasePath { get => Path.Combine(Directory.GetCurrentDirectory(), "Settings", "appSettings"); }

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
            // demo GetSection method
            // controleer of de sectie bestaat in de appsettings file
            // section name is de key name in het json bestand, dus als de key bestaat in het json bestand
            if (!_configRoot.GetSection(sectionName).Exists())
            {
                result.Status = QueryStatus.HasError;
                result.Error = new Exception("The sectionname is not found in the settingsfile");
                //exit of method, function
                return result;
            }
            // demo GetSection method, let op de extra Get met Type Parameter
            // Get<TSection>() verzorgt de effectieve binding, anders is het result null !!!
            result.QueryResult = _configRoot.GetSection(sectionName).Get<TSection>();
            return result;
        }
    }
}
