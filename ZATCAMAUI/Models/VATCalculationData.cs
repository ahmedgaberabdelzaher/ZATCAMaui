
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    
    public class VATCalculationData
    {
        [JsonProperty("data")]
        public VATCalculationDataD d { get; set; }
    }
   
    public class VATRateDataWithStringDateType
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
   
    public class VATRateDataWithDateType
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
   
    public class VATCalculationDataDMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class ITUDSetResult
    {
        public Metadata2 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("system")]
        public string SysFlg { get; set; }
       [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("source")]
        public string SourceFg { get; set; }
        [JsonProperty("portalURL")]
        public string UrlPortal { get; set; }
    }
   
    public class ITUDSet
    {
        [JsonProperty("indirectTaxURLs")]
        public List<ITUDSetResult> results { get; set; }
    }
   
    public class VATRSet
    {
        [JsonProperty("VATRates")]
        public List<VATCalculationDataVATRSet> results { get; set; }
    }
   
    public class VATCalculationDataVATRSet
    {
        public Metadata3 __metadata { get; set; }
        [JsonProperty("systemCode")]
      
        public string Mandt { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("penalty")]
        public string Penalty { get; set; }
        [JsonProperty("startDate")]
        public DateTime Begda { get; set; }
        [JsonProperty("endDate")]
        public DateTime Endda { get; set; }
    }
   
    public class VATCalculationDataVTTHSet
    {
        public Metadata5 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Type { get; set; }
        public string MinVal { get; set; }
        public string MaxVal { get; set; }
        public string Percentage { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
    //public class Metadata3
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
   
    public class IBANSetResult
    {
        public Metadata3 __metadata { get; set; }
        [JsonProperty("TIN")]
        public string Partner { get; set; }
        [JsonProperty("bankDetails")]
        public string Bkvid { get; set; }
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
    }
   
    public class VATCalculationDataIBANSet
    {
        [JsonProperty("IBANs")]
        public List<IBANSetResult> results { get; set; }
    }

    //public class Metadata4
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}

   
    public class IGRTSetResult
    {
        public Metadata4 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("groupNumber")]
        public string GrpNo { get; set; }
        [JsonProperty("rateTreatment")]
        public string RateTrtmt { get; set; }
    }

   
    public class IGRTSet
    {
        [JsonProperty("industryGroupsAndRates")]
        public List<IGRTSetResult> results { get; set; }
    }
    //public class Metadata5
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
   
    public class VTTHSetResult
    {
        public Metadata5 __metadata { get; set; }
       [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("minimumValue")]
        public string MinVal { get; set; }
        [JsonProperty("maximumValue")]
        public string MaxVal { get; set; }
        [JsonProperty("fractionalPercentage")]
        public string Percentage { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
   
    public class VTTHSet
    {
        [JsonProperty("VATFormThresholds")]
        public List<VTTHSetResult> results { get; set; }
    }
    public class UIBTNSetResult
    {
        public Metadata6 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbust { get; set; }
        [JsonProperty("button")]
        public string Button { get; set; }
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
    }
   
    public class UIBTNSet
    {
        [JsonProperty("buttons")]
        public List<UIBTNSetResult> results { get; set; }
    }
   
    public class VATCalculationDataD
    {
        public Metadata __metadata { get; set; }
        //[JsonProperty("")]
        public string Mandtz { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtypz { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbustz { get; set; }
      //  [JsonProperty("userType")]
        public string UserTypz { get; set; }
       // [JsonProperty("transactionType")]
        public string TransactionTypez { get; set; }
        [JsonProperty("edit")]
        public string EditFgz { get; set; }
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
       // [JsonProperty("")]
        public string Officer { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
       // [JsonProperty("")]
        public string OfficerT { get; set; }
        [JsonProperty("sourceApplication")]
        public string SrcApp { get; set; }
        [JsonProperty("periodKey")]
        public string Periodkey { get; set; }
       // [JsonProperty("")]
        public object Begda { get; set; }
       // [JsonProperty("")]
        public object Endda { get; set; }
        [JsonProperty("destinationCheck")]
        public string DestCheck { get; set; }
        [JsonProperty("indirectTaxURLs")]
        public List<ITUDSetResult> ITUDSet { get; set; }
        [JsonProperty("VATRates")]
        public List<VATCalculationDataVATRSet> VATRSet { get; set; }
        [JsonProperty("IBANs")]
        public List<IBANSetResult> IBANSet { get; set; }
        [JsonProperty("industryGroupsAndRates")]
        public List<IGRTSetResult> IGRTSet { get; set; }
        [JsonProperty("VATFormThresholds")]
        public List<VTTHSetResult> VTTHSet { get; set; }
        [JsonProperty("buttons")]
        public List<UIBTNSetResult> UI_BTNSet { get; set; }
    }

}
