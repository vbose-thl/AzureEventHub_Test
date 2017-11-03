using System.IO;
using Microsoft.Extensions.Configuration;

namespace AzureEventHubTest.Helpers
{
    public class ConfigHelper
    {
        public static IConfigurationRoot Configuration { get; set; }
        
        public ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    }
}