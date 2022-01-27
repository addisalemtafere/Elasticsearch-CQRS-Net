using Infrastructure.Model;

namespace Infrastructure.Configuration.ElasticsearchConfiguration
{
    public interface IElasticConfigurationService
    {
        ElasticSearchSettings GetConnectionString();
    }
}