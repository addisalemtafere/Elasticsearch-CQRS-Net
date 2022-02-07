using Domain.Entity;
using Newtonsoft.Json;

namespace Infrastructure.Seed
{
    public static class JsonParser
    {
        public static List<Mgmt> MgmtJsonParser()
        {
            List<Mgmt> mmgmtLlst = new List<Mgmt>();
            List<Dictionary<string, Mgmt>> list = JsonConvert.DeserializeObject<List<Dictionary<string, Mgmt>>>(value: File.ReadAllText(@"SampleData\\mgmt.json"));

            foreach (Dictionary<string, Mgmt> dict in list)
            {
                KeyValuePair<string, Mgmt> keyValuePair = dict.First();
                Mgmt m = new Mgmt();
                m.mgmtID = keyValuePair.Value.mgmtID;
                m.name = keyValuePair.Value.name;
                m.market = keyValuePair.Value.market;
                m.state = keyValuePair.Value.state;
                mmgmtLlst.Add(m);
            }
            return mmgmtLlst;
        }

        public static List<Property> PropertyJsonParser()
        {
            List<Property> properties = new List<Property>();
            List<Dictionary<string, Property>> list = JsonConvert.DeserializeObject<List<Dictionary<string, Property>>>(value: File.ReadAllText(@"SampleData\\mgmt.json"));

            foreach (Dictionary<string, Property> dict in list)
            {
                KeyValuePair<string, Property> keyValuePair = dict.First();
                Property m = new Property();
                m.propertyID = keyValuePair.Value.propertyID;
                m.name = keyValuePair.Value.name;
                m.formerName = keyValuePair.Value.formerName;
                m.streetAddress = keyValuePair.Value.streetAddress;
                m.city = keyValuePair.Value.city;
                m.market = keyValuePair.Value.market;
                m.state = keyValuePair.Value.state;
                m.lat = keyValuePair.Value.lat;
                m.lng = keyValuePair.Value.lng;
                properties.Add(m);
            }
            return properties;
        }
    }
}