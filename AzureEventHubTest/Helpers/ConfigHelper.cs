using System.IO;
using Microsoft.Extensions.Configuration;

namespace AzureEventHubTest.Helpers
{
    public class ConfigHelper
    {
        private static IConfigurationRoot _configuration;

        private static IConfigurationRoot SetupConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            return _configuration;
        }

        public static IConfigurationRoot Configuration
        {
            get { return _configuration?? SetupConfig() ; }
            private set { _configuration = value; }
        }
    }
}