using Domain.Entity;
using Domain.Interfaces.Repository;
using Infrastructure.Configuration.ElasticsearchConfiguration;
using Infrastructure.Persistance.ElasticsearchContext;

namespace Infrastructure.Persistance.Repository
{
    public class PropertyRepository : ElasticSearchRepositoryBase<Property, Guid>, IPropertyRepository
    {
        public PropertyRepository(IElasticContextProvider context)
            : base(context, CommonConfigurationsHelper.GetDefaultIndexName())
        {
        }

        public async Task<IEnumerable<Property>> GetByTextAsync(string text, int limit)
        {
            //var searchResponse = await _context.GetClient()
            //                    .SearchAsync<Property>(s =>
            //                        s.Index(IndexName)
            //                            .Query(q => q.MultiMatch(
            //                                m => m.Fields(fs => fs.Field(f => f.name)
            //                                                      .Field(f => f.Id)
            //                                                      .Field(f => f.state))
            //                                .Query(text))));

            var searchResponse = this._context.GetClient().Search<Property>(
                     s => s.AllIndices()
                         .Index(new[] { IndexName, "mgmt" })
                         .IgnoreUnavailable().Size(limit)
                       .Query(q => q.MultiMatch(
                                            m => m.Fields(fs => fs.Field(f => f.name)
                                                                  .Field(f => f.Id)
                                                                  .Field(f => f.state))
                                            .Query(text).Lenient())));

            return searchResponse.Documents;
        }
    }
}