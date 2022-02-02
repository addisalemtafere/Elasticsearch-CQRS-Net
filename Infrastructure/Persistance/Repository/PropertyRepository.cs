using Domain.Entity;
using Domain.Interfaces.Repository;
using Infrastructure.Configuration.ElasticsearchConfiguration;
using Infrastructure.Persistance.ElasticsearchContext;
using Nest;

namespace Infrastructure.Persistance.Repository
{
    public class PropertyRepository : ElasticSearchRepositoryBase<Property, Guid>, IPropertyRepository
    {
        public PropertyRepository(IElasticContextProvider context)
            : base(context, CommonConfigurationsHelper.GetDefaultIndexName())
        {
        }

        public async Task<IEnumerable<Property>> GetByTextAsync(string text, IEnumerable<string> marketKey, int limit)
        {
            var mgmtIndexName = CommonConfigurationsHelper.GetSettings().ElasticMgmtIndex;
            var searchResponse = this._context.GetClient().Search<Property>(
                     s => s.AllIndices()
                         .Index(new[] { IndexName, mgmtIndexName })
                         .IgnoreUnavailable().Size(limit)
                       .Query(q => q.MultiMatch(
                                            m => m.Fields(fs => fs.Field(f => f.city)
                                                                  .Field(f => f.state)
                                                                  .Field(f => f.streetAddress))
                                            .Fuzziness(Fuzziness.Auto)
                                            .Lenient()
                                            .Query(text))
                                           && q.Bool(bq => bq
                                     .Filter(fq => fq.Terms(t => t.Field(f => f.market).Terms(marketKey)))

                                            )));

            return searchResponse.Documents;
        }
    }
}