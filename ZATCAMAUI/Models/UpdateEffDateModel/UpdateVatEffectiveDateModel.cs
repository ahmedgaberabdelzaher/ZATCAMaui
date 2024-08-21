

using Newtonsoft.Json;

namespace ZATCAMAUI.Models.UpdateEffDateModel
{
    
    public class UpdateVatEffectiveDateModel
    {
        [JsonProperty("data")]
        public D d { get; set; }
    }
    
    public class D
    {
        public Metadata Metadata { get; set; }
        [JsonProperty("groupRegistration")]
        public string GrpregFg { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsr { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("userName")]
        public string Officer { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [JsonProperty("userNameT")]
        public string OfficerT { get; set; }
        [JsonProperty("sourceApplication")]
        public string SrcApp { get; set; }
        [JsonProperty("periodkey")]
        public string Periodkey { get; set; }
        [JsonProperty("startDate")]
        public string Begda { get; set; }
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("channel")]
        public string Inpch { get; set; }
        [JsonProperty("formBundleType")]
        public string RegTp { get; set; }
        [JsonProperty("items")]
       // public ItemSet[] ItemSet { get; set; }
        public List<ItemSetResult> ItemSet { get; set; }
    }
    
    public  class ItemSet
    {
        public List<ItemSetResult> results { get; set; }
    }
    
    public partial class ItemSetResult
    {
        public Metadata Metadata { get; set; }
        [JsonProperty("systemCode")]
        public long Mandt { get; set; }
        [JsonProperty("modifyDate")]
        public string ModifyDate { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("effectiveDateBefore")]
        public string EffDtBefore { get; set; }
        [JsonProperty("effectiveDateAfter")]
        public string EffDtAfter { get; set; }
        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty("changeReason")]
        public string ChangeReason { get; set; }
        [JsonProperty("approvalStatus")]
        public string AppStatus { get; set; }
        [JsonProperty("appliedLatePenalty")]
        public string AppLatePen { get; set; }
        [JsonProperty("generatedNewReturn")]
        public string GenNewReturn { get; set; }
        [JsonProperty("userType")]
        public string UserType { get; set; }
    }
    
    public partial class Metadata
    {
        public Uri Id { get; set; }
        public Uri Uri { get; set; }
        public string Type { get; set; }
    }


}
