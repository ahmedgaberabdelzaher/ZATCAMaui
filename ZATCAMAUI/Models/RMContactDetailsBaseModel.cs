
using System.Runtime.Serialization;

namespace ZATCAMAUI.Models
{
    public class RMContactDetailsBaseModel
    {
        [DataMember(Name = "d")]
        public RMSUPContactDetailsModel d { get; set; }
    }
    public class RMSUPContactDetailsModel
    {
        [DataMember(Name = "__metadata")]
        public Metadata __metadata { get; set; }

        [DataMember(Name = "Gpart")]
        public string Gpart { get; set; }

        [DataMember(Name = "TpEntityDesc")]
        public string TpEntityDesc { get; set; }

        [DataMember(Name = "Lang")]
        public string Lang { get; set; }

        [DataMember(Name = "ServiceName")]
        public string ServiceName { get; set; }

        [DataMember(Name = "Code")]
        public string Code { get; set; }

        [DataMember(Name = "Category")]
        public string Category { get; set; }

        [DataMember(Name = "TpEntity")]
        public string TpEntity { get; set; }

        [DataMember(Name = "TpMobile")]
        public string TpMobile { get; set; }

        [DataMember(Name = "RmName")]
        public string RmName { get; set; }

        [DataMember(Name = "RmMobile")]
        public string RmMobile { get; set; }

        [DataMember(Name = "RmEmail")]
        public string RmEmail { get; set; }

        [DataMember(Name = "SupName")]
        public string SupName { get; set; }

        [DataMember(Name = "SupMobile")]
        public string SupMobile { get; set; }

        [DataMember(Name = "SupEmail")]
        public string SupEmail { get; set; }

        [DataMember(Name = "UrlValidate")]
        public string UrlValidate { get; set; }

        [DataMember(Name = "SurveyId")]
        public string SurveyId { get; set; }

        [DataMember(Name = "Url")]
        public string Url { get; set; }
    
    }

    [DataContract]
    public class CheckVocAvailabilityModel
    {
        [DataMember(Name = "surId")]
        public string surId { get; set; }

        [DataMember(Name = "eData")]
        public VocEData eData { get; set; }

        [DataMember(Name = "timeFilter")]
        public VocTimeFilter timeFilter { get; set; }
    }

    [DataContract]
    public class VocEData
    {

        [DataMember(Name = "TIN")]
        public string TIN { get; set; }

        [DataMember(Name = "phone")]
        public string phone { get; set; }
    }

    [DataContract]
    public class VocTimeFilter
    {

        [DataMember(Name = "amount")]
        public int amount { get; set; }

        [DataMember(Name = "period")]
        public string period { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }
    }

}
