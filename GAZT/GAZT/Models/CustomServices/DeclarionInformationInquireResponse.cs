using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models.CustomServices
{
   
    public class DeclarionInformationInquire
    {
        public int port_cd { get; set; }
        public string port_name { get; set; }
        public string impr_nbr { get; set; }
        public string dcltn_isn { get; set; }
        public int dcltn_sta_cd { get; set; }
        public string dcltn_sta_desc { get; set; }
        public int dcltn_type_cd { get; set; }
        public int bus_id_type_cd { get; set; }
        public string bus_id { get; set; }
        public string bus_id_brnch { get; set; }
        public int ctry_cd { get; set; }
        public int impr_type_cd { get; set; }
        public string arbc_impr_name { get; set; }
        public string type_name { get; set; }
        public string brkr_name { get; set; }
        public string lic_nbr { get; set; }
        public string mobile_nbr { get; set; }
        public object fasahdate { get; set; }
        
        public string InferentialNumber { get
            {
                if (!String.IsNullOrEmpty(bus_id_brnch))
                {
                    return bus_id_brnch;
                }
                return bus_id;
            } }
    }

    public class DeclarionInformationInquireResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<DeclarionInformationInquire> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
