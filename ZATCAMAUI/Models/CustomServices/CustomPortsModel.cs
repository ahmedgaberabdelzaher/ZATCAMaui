using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices
{

    public class CustomPortsModel
    {
        public bool Issuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public ObservableCollection<CustomPort> Data { get; set; }
        public int Count { get; set; }
        public Guid Correlationid { get; set; }
    }

    public class CustomPort
    {
        public int port_cd { get; set; }
        public int PortType { get; set; }
        public string port_name { get; set; }
        public string port_eng_name { get; set; }
        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    if (!string.IsNullOrEmpty(port_eng_name))
                    {
                        return port_eng_name;
                    }
                }
                return port_name;
            }
        }
        public string PortIntlCd { get; set; }
        public string PortStatus { get; set; }
        public int? Filler1 { get; set; }
        public string Filler2 { get; set; }
        public string Filler3 { get; set; }
        public int? Filler4 { get; set; }
        public string Filler5 { get; set; }
    }

}
