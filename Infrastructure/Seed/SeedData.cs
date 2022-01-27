using Infrastructure.Persistance.ElasticsearchContext;

namespace Infrastructure.Seed
{
    public class SeedData
    {
        public static async Task SeedAsync(IElasticContextProvider elasticContext)

        {
            var mgtData = JsonParser.MgmtJsonParser();
            var indexManyResponse = elasticContext.GetClient().Bulk(b => b.Index("mgmt").IndexMany(mgtData));

            if (indexManyResponse.Errors)
            {
                foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
                {
                    Console.WriteLine($"Failed to index document {itemWithError.Id}: {itemWithError.Error}");
                }
            }
            var propertyData = JsonParser.PropertyJsonParser();

            var propertyIndexManyResponse = elasticContext.GetClient().Bulk(b => b.Index("properties").IndexMany(mgtData));

            if (propertyIndexManyResponse.Errors)
            {
                foreach (var itemWithError in propertyIndexManyResponse.ItemsWithErrors)
                {
                    Console.WriteLine($"Failed to index document {itemWithError.Id}: {itemWithError.Error}");
                }
            }
        }
    }
}