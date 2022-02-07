namespace Infrastructure.Seed
{
    public class Mgmt
    {
        public int mgmtID { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string state { get; set; }
    }

    public class Root
    {
        public List<string> mgmt { get; set; }
    }
}