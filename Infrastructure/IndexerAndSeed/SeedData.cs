using Infrastructure.Persistance.ElasticsearchContext;

namespace Infrastructure.Seed
{
    public class SeedData
    {
        public static async Task SeedAsync(IElasticContextProvider elasticContext)

        {
            if ((await elasticContext.GetClient().Indices.ExistsAsync("properties")).Exists)
                await elasticContext.GetClient().Indices.DeleteAsync("properties");

            var createMoviesIndexResponse = await elasticContext.GetClient().Indices.CreateAsync("properties", c => c
               .Settings(s => s
                   .Analysis(a => a
                       .TokenFilters(tf => tf
                           .Stop("english_stop", st => st
                               .StopWords("_english_")
                           )
                           .Stemmer("english_stemmer", st => st
                               .Language("english")
                           )
                           .Stemmer("light_english_stemmer", st => st
                               .Language("light_english")
                           )
                           .Stemmer("english_possessive_stemmer", st => st
                               .Language("possessive_english")
                           )
                           .Synonym("synonyms", st => st
                             
                               .Synonyms(
                                   "NY,New York",
                                   "CA,California")
                           )
                       )
                       .Analyzers(aa => aa
                           .Custom("light_english", ca => ca
                               .Tokenizer("standard")
                               .Filters("light_english_stemmer", "english_possessive_stemmer", "lowercase", "asciifolding")
                           )
                           .Custom("full_english", ca => ca
                               .Tokenizer("standard")
                               .Filters("english_possessive_stemmer",
                                       "lowercase",
                                       "english_stop",
                                       "english_stemmer",
                                       "asciifolding")
                           )
                           .Custom("full_english_synopsis", ca => ca
                               .Tokenizer("standard")
                               .Filters("synonyms",
                                       "english_possessive_stemmer",
                                       "lowercase",
                                       "english_stop",
                                       "english_stemmer",
                                       "asciifolding")
                           )
                       )
                   )
               )
           );

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