
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class Correspondance
    {
    }

    
    public class CorrespondenceMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    
    public class CorrespondenceResult
    {
        //public CorrespondenceMetadata __metadata { get; set; }
        [JsonProperty("attachmentURL")]
        public string Pdfurl { get; set; }
        [JsonProperty("isFavourite")]
        public string Zzfav { get; set; }
        [JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("taxType")]
        public string TaxtpFg { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("auditor")]
        public string Auditor { get; set; }
        [JsonProperty("correspondenceRecipient")]
        public string Gpartz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("startDate")]
        public string Begdaz { get; set; }
        [JsonProperty("endDate")]
        public string Enddaz { get; set; }
        [JsonProperty("obligation")]
        public string ObligFlagz { get; set; }
        [JsonProperty("creationDate")]
        public string Cdate { get; set; }
        [JsonProperty("issueTime")]
        public string Ctime { get; set; }
        [JsonProperty("formkey")]
        public string Formkey { get; set; }
        [JsonProperty("formDescription")]
        public string Descript { get; set; }
        [JsonProperty("correspondenceNumber")]
        public string CorrNum { get; set; }
        [JsonProperty("correspondenceKey")]
        public string Cokey { get; set; }
        [JsonProperty("correspondenceType")]
        public string Cotyp { get; set; }
        [JsonProperty("correspondenceName")]
        public string Cotxt { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("contractAccount")]
        public string Vkont { get; set; }
        [JsonProperty("contractReference")]
        public string Vtref { get; set; }
        [JsonProperty("issueDate")]
        public string Coidt { get; set; }
        
        public string Coitm { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("caseId")]
        public string CaseId { get; set; }
        [JsonProperty("letterNumber")]
        public string LetterNum { get; set; }
    }

    
    public class CorrespondenceD
    {
        [JsonProperty("correspondences")]
        public List<CorrespondenceResult> results { get; set; }
        
    }

    
    public class CorrespondenceRootObject
    {
        [JsonProperty("data")]
        public CorrespondenceD d { get; set; }
    }
   
}
