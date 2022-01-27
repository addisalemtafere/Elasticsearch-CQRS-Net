using Nest;

namespace Infrastructure.Persistance.ElasticsearchContext
{
    public interface IElasticContextProvider
    {
        IElasticClient GetClient();
    }
}