using Microsoft.Extensions.Configuration;

namespace Tests.Infrastructure
{
    public static class ConfigurationProvider
    {
        private static IConfiguration _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                }

                return _configuration;
            }
        }
    }
}