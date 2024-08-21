using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    [Serializable]
  
    [DataContract]
    public class __metadataForVATREgistrationOtherInfo
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class ResultsItemForButton
    {
        [DataMember]
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [DataMember]
        [JsonProperty("userStatus")]
        public string Fbust { get; set; }
        [DataMember]
        [JsonProperty("button")]
        public string Button { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class VR_UI_BTNSet
    {
        [DataMember]
        public List<ResultsItemForButton> results { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class ResultsItemForDOCSet
    {
        [DataMember]
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Fbtyp { get; set; }
        [DataMember]
        public string TxnTp { get; set; }
        [DataMember]
        public string DmsTp { get; set; }
        [DataMember]
        public string StartDt { get; set; }
        [DataMember]
        public string EndDt { get; set; }
        [DataMember]
        public string Txt50 { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class ELGBL_DOCSet
    {
        [DataMember]
        public List<ResultsItemForElgblDocSet> results { get; set; }
    }

    
    public class ELGBL_DOCSetforsubmit
    {
       // [DataMember]
        public List<ResultsItemForDOCSetforsubmit> results { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class VATRegistrationWithOtherInformation
    {
        //[DataMember]
      //  public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtypz { get; set; }
        [DataMember]
        public string Fbustz { get; set; }
        [DataMember]
        [JsonProperty("edit")]
        public string EditFgz { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string PortalUsr { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Lang { get; set; }
        [DataMember]
        [JsonProperty("operation")]
        public string Operation { get; set; }
        [DataMember]
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [DataMember]
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [DataMember]
        public string Officer { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [DataMember]
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [DataMember]
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [DataMember]
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [DataMember]
        [JsonProperty("UIButtons")]
        public List<ResultsItemForButton> VR_UI_BTNSet { get; set; }
        [DataMember]
        [JsonProperty("eligibleDocuments")]
        public List<ResultsItemForElgblDocSet> ELGBL_DOCSet { get; set; }
        [JsonProperty("banks")]
        public List<BankItem> Banks { get; set; }
    }

    [Serializable]
    
    [DataContract]
    public class BankItem
    {
        public string systemCode { get; set; }
        public string bankCountry { get; set; }
        public string bankKey { get; set; }
        public string bankName { get; set; }

    }

    [Serializable]
  
    [DataContract]
    public class VATRegistrationOtherDetails
    {
        [DataMember]
        [JsonProperty("data")]
        public VATRegistrationWithOtherInformation d { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class DataToPassTofinancialDetailAttachmentPopup
    {
        [DataMember]
        public VATRegistrationOtherDetails vatRegOthrDetailtoPopup { get; set; }
        [DataMember]
        public VATRegistrationDetails VATRegistrationDetailsDatatoPopup { get; set; }
    }
    
    public class VatCommencementDateFormatModel
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }
    
    public class CommencementModel
    {
        [DataMember]
        public VatCommencementDateFormatModel __metadata { get; set; }
        [DataMember]
        [JsonProperty("VATTaxableDate")]
        public string VatTaxDt { get; set; }
        [DataMember]
        [JsonProperty("error")]
        public string ErrorFg { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Gpartz { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TxnTpz { get; set; }
    }
    
    public class VatCommencementDateFormat
    {
        [DataMember]
        [JsonProperty("data")]
        public CommencementModel d { get; set; }
    }
}
