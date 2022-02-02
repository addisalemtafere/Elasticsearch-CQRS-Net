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
            var nameList = new[] { "Abilene", "Abilene" };


            QueryContainer query = new QueryContainerDescriptor<Property>();

            if (!string.IsNullOrEmpty(text))
            {
                query = query && new QueryContainerDescriptor<Property>().MultiMatch(
                                            m => m.Fields(fs => fs.Field(f => f.city)
                                                                  .Field(f => f.state)
                                                                  .Field(f => f.streetAddress))
                                            .Fuzziness(Fuzziness.Auto)
                                            .Lenient()
                                            .Query(text));
            }
            //if (marketKey.Any())
            //{
            //    query = query && new QueryContainerDescriptor<Property>()
            //        .Bool(bq => bq.Filter(fq => fq.Terms(t => t.Field(f => f.market).Terms(new[] { "Abilene", "" }) )));


            //}
            //var filters = new List<Func<QueryContainerDescriptor<Property>, QueryContainer>>();
            //if (nameList.Any())
            //{
            //    //filters.Add(fq => fq.Terms(t => t.Field(f => f.market).Terms("San Francisco")));


            //}
            //var tagsFilter = marketKey.Select(value =>
            //{
            //    Func<QueryContainerDescriptor<Property>, QueryContainer> tagFilter = filter => filter
            //        .Term(term => term
            //            .Field(field => field.market)
            //            .Value(value));

            //    return tagFilter;
            //});

            //var result = _context.GetClient().Search<Property>(s => s
            //   .Index(new[] { IndexName, mgmtIndexName }).
            //   Query(_=>query));


            var sd = new SearchDescriptor<Property>();
            var qc = new QueryContainer();
            var qd = new QueryContainerDescriptor<Property>();

            sd.Size(limit);
            sd.Index(new[] { IndexName, mgmtIndexName });

            if (!string.IsNullOrEmpty(text))
            {
                qc = qd.MultiMatch(m => m.Fields(fs => fs.Field(f => f.city)
                      .Field(f => f.state)
                      .Field(f => f.streetAddress))
                        .Fuzziness(Fuzziness.Auto)
                        .Lenient()
                            .Query(text));

            }

            if (marketKey.Any())
            {
                qc = qd.Terms(m => m
                    .Field(f => f.market)
                    .Terms(marketKey.ToArray()));
            }



            sd.Query(q => q
                 .Bool(b => b.Must(qc)));

            var result = _context.GetClient().Search<Property>(s => s = sd);

            return result.Documents;
        }
    }
}