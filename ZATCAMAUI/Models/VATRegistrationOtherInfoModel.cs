using System.Runtime.Serialization;

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
        public string Fbtyp { get; set; }
        [DataMember]
        public string Fbust { get; set; }
        [DataMember]
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

    // [Serializable]
  
    //[DataContract]
    public class ELGBL_DOCSetforsubmit
    {
        // [DataMember]
        public List<ResultsItemForDOCSetforsubmit> results { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class VATRegistrationWithOtherInformation
    {
        [DataMember]
        public __metadataForVATREgistrationOtherInfo __metadata { get; set; }
        [DataMember]
        public string Fbtypz { get; set; }
        [DataMember]
        public string Fbustz { get; set; }
        [DataMember]
        public string EditFgz { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string PortalUsr { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        public string StepNumber { get; set; }
        [DataMember]
        public string ReturnId { get; set; }
        [DataMember]
        public string Officer { get; set; }
        [DataMember]
        public string Gpart { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string UserTyp { get; set; }
        [DataMember]
        public string TxnTp { get; set; }
        [DataMember]
        public string Formproc { get; set; }
        [DataMember]
        public VR_UI_BTNSet VR_UI_BTNSet { get; set; }
        [DataMember]
        public ELGBL_DOCSet ELGBL_DOCSet { get; set; }
    }

    [Serializable]
  
    [DataContract]
    public class VATRegistrationOtherDetails
    {
        [DataMember]
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
        public DateTime? VatTaxDt { get; set; }
        [DataMember]
        public string ErrorFg { get; set; }
        [DataMember]
        public string Gpartz { get; set; }
        [DataMember]
        public string TxnTpz { get; set; }
    }

    public class VatCommencementDateFormat
    {
        [DataMember]
        public CommencementModel d { get; set; }
    }
}
