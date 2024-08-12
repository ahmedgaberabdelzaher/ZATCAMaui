using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public  class TPUpdateActivityModel
    {
        [JsonProperty("TIN")]
        public string Taxpayer { get; set; }
        [JsonProperty("isMigration")]
        public bool Flag { get; set; }
    }

    [DataContract]
    public class DashBoardUpdateViewResponseModel
    {
        [JsonProperty("data")]
        public DashBoardUpdateViewResponse d { get; set; }
    }

    [DataContract]
    public class DashBoardUpdateViewResponse
    {
        [JsonProperty("activityStatus")]
        public IList<DashBoardUpdateViewResponse> results { get; set; }

        [JsonProperty("TIN")]
        public string Taxpayer { get; set; }

        [JsonProperty("isMigration")]
        public bool Flag { get; set; }

        [JsonProperty("messageDescription")]
        public string Msg { get; set; }

        public string systemCode { get; set; }

    }

    public class DashBoardUpdateResponseModel
    {
        [JsonProperty("result")]
        public DashBoardUpdateResponse d { get; set; }
    }
    public class DashBoardUpdateResponse
    {
        

        [JsonProperty("TIN")]
        public string Taxpayer { get; set; }

        [JsonProperty("isMigration")]
        public bool Flag { get; set; }

        [JsonProperty("messageDescription")]
        public string Msg { get; set; }

        public string systemCode { get; set; }

    }
    /*For change Activity CR and Licence Update*/
    [DataContract]
    public class ActivityUpdateViewResponseModel
    {
        public ActivityUpdateViewResponse d { get; set; }
    }

    [DataContract]
    public class ActivityUpdateViewResponse
    {

        public Metadata __metadata { get; set; }

        public string Taxpayer { get; set; }

        public string Idtype { get; set; }

        public string Idnumber { get; set; }

        public string Activity { get; set; }

        public string MainGrp { get; set; }

        public string SubGrp { get; set; }

        public bool UpdFlg { get; set; }

    }

}
