using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EGAZT.Models
{
   public  class TPUpdateActivityModel
    {
       public string Taxpayer { get; set; }
       public bool Flag { get; set; }
    }

    [DataContract]
    public class DashBoardUpdateViewResponseModel
    {
        [DataMember(Name = "d")]
        public DashBoardUpdateViewResponse d { get; set; }
    }

    [DataContract]
    public class DashBoardUpdateViewResponse
    {

        [DataMember(Name = "results")]
        public IList<DashBoardUpdateViewResponse> results { get; set; }

        [DataMember(Name = "Taxpayer")]
        public string Taxpayer { get; set; }

        [DataMember(Name = "Flag")]
        public bool Flag { get; set; }

        [DataMember(Name = "Msg")]
        public string Msg { get; set; }

    }

    /*For change Activity CR and Licence Update*/
    [DataContract]
    public class ActivityUpdateViewResponseModel
    {
        [DataMember(Name = "d")]
        public ActivityUpdateViewResponse d { get; set; }
    }

    [DataContract]
    public class ActivityUpdateViewResponse
    {

        [DataMember(Name = "__metadata")]
        public Metadata __metadata { get; set; }

        [DataMember(Name = "Taxpayer")]
        public string Taxpayer { get; set; }

        [DataMember(Name = "Idtype")]
        public string Idtype { get; set; }

        [DataMember(Name = "Idnumber")]
        public string Idnumber { get; set; }

        [DataMember(Name = "Activity")]
        public string Activity { get; set; }

        [DataMember(Name = "MainGrp")]
        public string MainGrp { get; set; }

        [DataMember(Name = "SubGrp")]
        public string SubGrp { get; set; }

        [DataMember(Name = "UpdFlg")]
        public bool UpdFlg { get; set; }

    }

}
