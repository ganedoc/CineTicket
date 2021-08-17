using Microsoft.Extensions.Configuration;
using static System.IO.Directory;
using static CineTicket.Common.Constants;

namespace CineTicket.Common
{
    public class Configurations
    {
        public static IConfigurationRoot Configuration;

        static Configurations()
            => Configuration = new ConfigurationBuilder().SetBasePath(GetCurrentDirectory()).AddJsonFile(CONFIG_FILE).Build();
        public static string GetConfigValue(string configKey)
            => Configuration[configKey];
    }
}
