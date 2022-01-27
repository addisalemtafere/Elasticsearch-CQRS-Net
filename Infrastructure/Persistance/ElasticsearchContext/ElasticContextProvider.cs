using Elasticsearch.Net;
using Infrastructure.Configuration.ElasticsearchConfiguration;
using Nest;

namespace Infrastructure.Persistance.ElasticsearchContext
{
    public class ElasticContextProvider : IElasticContextProvider
    {
        private const string DEFAULT_CONNECTION = "http://localhost:9200/";
        private readonly IElasticConfigurationService _configuracaoService;
        private IElasticClient Client { get; set; }

        public ElasticContextProvider(IElasticConfigurationService configuracaoService)
        {
            _configuracaoService = configuracaoService;
        }

        public IElasticClient GetClient()
        {
            if (Client == null)
            {
                var hosts = _configuracaoService.GetConnectionString().Addresses ?? new[] { DEFAULT_CONNECTION };

                if (hosts.Length > 1)
                {
                    var connectionPool = new SniffingConnectionPool(hosts.Select(a => new Uri(a)));
                    var settings = new ConnectionSettings(connectionPool);
                    Client = new ElasticClient(settings);
                }
                else
                {
                    var settings = new ConnectionSettings(new Uri(hosts[0]));
                    Client = new ElasticClient(settings);
                }
            }

            return Client;
        }
    }
}