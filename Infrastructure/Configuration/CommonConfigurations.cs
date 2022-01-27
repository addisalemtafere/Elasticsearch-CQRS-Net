using Infrastructure.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration.ElasticsearchConfiguration
{
    public class CommonConfigurationsHelper
    {
        private static IConfiguration _configuration;

        public static void CommonConfig(IConfiguration config)
        {
            _configuration = config;
        }

        public static string GetDefaultIndexName()
        {
            return _configuration.GetSection("Elastic").Get<ElasticSearchSettings>().ElasticDefaultIndex;
        }

        public static ElasticSearchSettings GetSettings()
        {
            return _configuration.GetSection("Elastic").Get<ElasticSearchSettings>();
        }
    }
}