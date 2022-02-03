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

        public async Task<IEnumerable<Property>> SearchPropertyAsync(string text, IEnumerable<string> marketKey, int limit)
        {
            var mgmtIndexName = CommonConfigurationsHelper.GetSettings().ElasticMgmtIndex;


            var searchDescriptor = new SearchDescriptor<Property>();
            var queryContainer = new QueryContainer();
            var queryContainerDescriptor = new QueryContainerDescriptor<Property>();

            searchDescriptor.Size(limit);
            searchDescriptor.Index(new[] { IndexName, mgmtIndexName });

            if (!string.IsNullOrEmpty(text))
            {
                queryContainer = queryContainerDescriptor.MultiMatch(m => m.Fields(fs => fs.Field(f => f.city)
                      .Field(f => f.state)
                      .Field(f => f.streetAddress))
                        .Fuzziness(Fuzziness.Auto)
                        .Lenient()
                            .Query(text));

            }

            if (marketKey.Any())
            {
                //queryContainer = queryContainer && queryContainerDescriptor.Terms(m => m
                //    .Field(f => f.market)
                //    .Terms(marketKey.ToArray())); ;

                foreach (string str in marketKey)
                {
                    queryContainer = queryContainer && new QueryContainerDescriptor<Property>().Term(m => m.market, str.ToLower());

                }
            }



            searchDescriptor.Query(q => q
                 .Bool(b => b.Must(queryContainer)));
            var result = _context.GetClient().Search<Property>(s => searchDescriptor);

            return result.Documents;
        }
    }
}