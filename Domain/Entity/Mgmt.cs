using Domain.Common;

namespace Domain.Entity
{
    public class Mgmt : Message
    {
        public int mgmtID { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string state { get; set; }
    }
}