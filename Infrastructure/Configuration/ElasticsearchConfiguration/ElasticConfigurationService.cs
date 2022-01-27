using Infrastructure.Model;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration.ElasticsearchConfiguration
{
    public class ElasticConfigurationService : IElasticConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ElasticConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ElasticSearchSettings GetConnectionString()
        {
            return _configuration.GetSection("Elastic").Get<ElasticSearchSettings>();
        }
    }
}