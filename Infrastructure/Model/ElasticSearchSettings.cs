namespace Infrastructure.Model
{
    public class ElasticSearchSettings
    {
        public string[] Addresses { get; set; }
        public string ElasticDefaultIndex { get; set; }
        public string ElasticMgmtIndex { get; set; }

        public string ElasticUrl { get; set; }
        public bool IsSeedEnable { get; set; }

        public ElasticSearchSettings()
        {
            ElasticUrl = "http://localhost:9200";
        }
    }
}