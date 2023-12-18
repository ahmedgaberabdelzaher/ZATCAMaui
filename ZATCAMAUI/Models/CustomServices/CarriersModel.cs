using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices
{
    public class CarriersModel
    {
        public bool Issuccess { get; set; }
        public long Code { get; set; }
        public string Message { get; set; }
        public ObservableCollection<Carrier> Data { get; set; }
        public long Count { get; set; }
        public Guid Correlationid { get; set; }
    }

    public class Carrier
    {
        public int carr_prefix { get; set; }
        public string Carr_Cd { get; set; }
        public long Ctry_Cd { get; set; }
        public object Ctry_Name { get; set; }
        public string carr_name { get; set; }
        public string Carr_Addrs { get; set; }
        public string Carr_Email { get; set; }
        public string Carr_Owner_Name { get; set; }
        public string Carr_Owner_Addrs { get; set; }
        public string Carr_Owner_Email { get; set; }
    }
}
