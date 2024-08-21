using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Models
{
    public class TINDeregistrationModel
    {
        public TINDeregistrationModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
        public string OutletOptionIndex { get; set; }
        [JsonIgnore]
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        [JsonIgnore]
        public string UnSelectedCardIcon { get; set; } = "vat_tile_IbanCard_background_white";
        [JsonIgnore]
        public string SelectedCardIcon { get; set; } = "vat_tile_IbanCard_background";

    }

    public class TinDeregestrationAttachmentsModel : ObservableRecipient
    {
        public TinDeregestrationAttachmentsModel()
        {
        }

        private List<Attachment> _attachmentTypeList { get; set; }
        public List<Attachment> AttachmentTypeList
        {
            get
            {
                return _attachmentTypeList;
            }
            set
            {
                _attachmentTypeList = value;
                OnPropertyChanged("AttachmentTypeList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _attachmentName { get; set; }
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                OnPropertyRaised("AttachmentName");
            }
        }

        private bool _isAttachmentAttached { get; set; }
        public bool IsAttachmentAttached
        {
            get
            {
                return _isAttachmentAttached;
            }
            set
            {
                _isAttachmentAttached = value;
                OnPropertyRaised("IsAttachmentAttached");
            }
        }

        private bool _isMandatory { get; set; }
        public bool IsMandatory
        {
            get
            {
                return _isMandatory;
            }
            set
            {
                _isMandatory = value;
                OnPropertyRaised("IsMandatory");
            }
        }

        private string _docType { get; set; }
        public string DocType
        {
            get
            {
                return _docType;
            }
            set
            {
                _docType = value;
                OnPropertyRaised("DocType");
            }
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
    }
   
    public class TINDeregistrationSummaryModel
    {
        public TINDeregistrationSummaryModel()
        {
        }

        public string SummaryTitle { get; set; }
        public string SummaryData { get; set; }
        public bool IsEditVisible { get; set; }
    }


   
    public class ZakatDeregistrationDetailsListModel
    {
        public string ZDTitle { get; set; }
        public string ZDImageSource { get; set; }
        public string ArrowImageSource { get; set; }
    }
   
    public partial class TinDeregistrationParentResponseModel
    {
        [JsonProperty("result")]
        public TinDeregistrationResponseModel D { get; set; }
    }
   
    public partial class TinDeregistrationResponseModel
    {
        public Metadata Metadata { get; set; }

        [JsonProperty("CR2021Popup")]
        public string Cr2021popup { get; set; }

        [JsonProperty("assignToMe")]
        public string Assignme { get; set; }

        [JsonProperty("caseId")]
        public string Caseid { get; set; }

        [JsonProperty("void")]
        public string Xvoidz { get; set; }

        [JsonProperty("TINInProcessing")]
        public string TinInPrcFg { get; set; }

        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }

        [JsonProperty("submit")]
        public string Submitz { get; set; }

        [JsonProperty("SEZTaxpayer")]
        public string SezTpFlag { get; set; }

        [JsonProperty("deregistartion")]
        public string BgDregFlg { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("save")]
        public string Savez { get; set; }

        [JsonProperty("reject")]
        public string Rejectz { get; set; }

        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; }

        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }

        [JsonProperty("periodKey")]
        public string PeriodKeyz { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("userName")]
        public string OfficerUidz { get; set; }

        [JsonProperty("month")]
        public string Monthz { get; set; }

        [JsonProperty("legacyDocumentNumber")]
        public string LegacyDocNo { get; set; }

        [JsonProperty("language")]
        public string Langz { get; set; }

        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }
        [JsonProperty("userStatus")]
        public string Fbust { get; set; }

        [JsonProperty("display")]
        public string Dflag { get; set; }

        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; }

        [JsonProperty("change")]
        public string Cflag { get; set; }

        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }

        [JsonProperty("auditor")]
        public string Auditorz { get; set; }

        [JsonProperty("transactionTIN")]
        public string ATransTin { get; set; }

        [JsonProperty("title")]
        public string ATitle { get; set; }

        [JsonProperty("TINType")]
        public string ATinType { get; set; }

        [JsonProperty("ATin")]
        public string ATin { get; set; }

        [JsonProperty("taxpayerName")]
        public string ATaxpayerName { get; set; }

        [JsonProperty("submissionDateHijri")]
        public string ASubmissionDateH { get; set; }

        [JsonProperty("submissionDateCalendar")]
        public string ASubmissionDateC { get; set; }

        [JsonProperty("submissionDate")]
        public string ASubmissionDate { get; set; }

        [JsonProperty("step")]
        public long AStep { get; set; }

        [JsonProperty("approve")]
        public string Approvez { get; set; }

        [JsonProperty("officialUseOrigin")]
        public string AOffOrigin { get; set; }

        [JsonProperty("officialApplicationNumber")]
        public string AOffAppNo { get; set; }

        [JsonProperty("firstName7")]
        public string ANm7 { get; set; }

        [JsonProperty("firstName6")]
        public string ANm6 { get; set; }

        [JsonProperty("firstName5")]
        public string ANm5 { get; set; }

        [JsonProperty("firstName4")]
        public string ANm4 { get; set; }

        [JsonProperty("firstName3")]
        public string ANm3 { get; set; }

        [JsonProperty("firstName2")]
        public string ANm2 { get; set; }

        [JsonProperty("firstName1")]
        public string ANm1 { get; set; }

        [JsonProperty("amendmentReason")]
        public string AmdRsnz { get; set; }

        [JsonProperty("idType")]
        public string AIdType { get; set; }

        [JsonProperty("idNumber")]
        public string AIdNo { get; set; }

        [JsonProperty("formStatus")]
        public string AFormStatus { get; set; }

        [JsonProperty("expiryDateHijri")]
        public string AExpdtH { get; set; }

        [JsonProperty("expiryDateCalendar")]
        public string AExpdtC { get; set; }

        [JsonProperty("expiryDate")]
        public string AExpdt { get; set; }

        [JsonProperty("effectiveDateHijri")]
        public string AEffectiveDtH { get; set; }

        [JsonProperty("effectiveDateCalendar")]
        public string AEffectiveDtC { get; set; }

        [JsonProperty("effectiveDate")]
        public string AEffectiveDt { get; set; }

        [JsonProperty("deregistrationReason")]
        public string ADregReason { get; set; }

        [JsonIgnore]
        public string ADeregSelectedReasonValue { get; set; }

        [JsonProperty("deregistrationOption")]
        public string ADregOpt
        {
            get;
            set;
        }

        [JsonProperty("document9")]
        public string ADocumnt9 { get; set; }

        [JsonProperty("document8")]
        public string ADocumnt8 { get; set; }

        [JsonProperty("document7")]
        public string ADocumnt7 { get; set; }

        [JsonProperty("document6")]
        public string ADocumnt6 { get; set; }

        [JsonProperty("document5Description")]
        public string ADocumnt5Txt { get; set; }

        [JsonProperty("document5")]
        public string ADocumnt5 { get; set; }

        [JsonProperty("document4Description")]
        public string ADocumnt4Txt { get; set; }

        [JsonProperty("document4")]
        public string ADocumnt4 { get; set; }

        [JsonProperty("document3")]
        public string ADocumnt3 { get; set; }

        [JsonProperty("document2")]
        public string ADocumnt2 { get; set; }

        [JsonProperty("document14")]
        public string ADocumnt14 { get; set; }

        [JsonProperty("document13")]
        public string ADocumnt13 { get; set; }

        [JsonProperty("document12")]
        public string ADocumnt12 { get; set; }

        [JsonProperty("document11")]
        public string ADocumnt11 { get; set; }

        [JsonProperty("document10")]
        public string ADocumnt10 { get; set; }

        [JsonProperty("document1")]
        public string ADocumnt1 { get; set; }

        [JsonProperty("birthDateHijri")]
        public string ADobH { get; set; }

        [JsonProperty("birthDateCalendar")]
        public string ADobC { get; set; }

        [JsonProperty("birthDate")]
        public string ADob { get; set; }

        [JsonProperty("deregister")]
        public string ADegister { get; set; }

        [JsonProperty("declarationTitle")]
        public string ADecTitle { get; set; }

        [JsonProperty("declarationTelephoneNumber")]
        public string ADecTelNo { get; set; }

        [JsonProperty("declarationName")]
        public string ADecName { get; set; }

        [JsonProperty("declarationCheckbox")]
        public string ADeclarationChkbox { get; set; }

        [JsonProperty("declarationDesignation")]
        public string ADecDesig { get; set; }

        [JsonProperty("declarationDateHijri")]
        public string ADecDateH { get; set; }

        [JsonProperty("declarationDateCalendar")]
        public string ADecDateC { get; set; }

        [JsonProperty("declarationDate")]
        public string ADecDate { get; set; }

        [JsonProperty("date")]
        public string ADateFormat { get; set; }

        [JsonProperty("branchDescription")]
        public string ABranchTxt { get; set; }

        [JsonProperty("partnerType")]
        public string ABpKind { get; set; }

        [JsonProperty("permitsTable")]
        public ArrayList PermitTableSet { get; set; }

        [JsonProperty("offNotes")]
        public ArrayList OffNotesSet { get; set; }

        [JsonProperty("permits")]
        public PermitSetResult[] PermitSet { get; set; }
       // public PermitSet PermitSet { get; set; }

        [JsonProperty("outlets")]
        public OutletSetResult[] OutletSet;
        //public Set OutletSet { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> AttDetSet { get; set; }
       // public AttachmentSet AttDetSet { get; set; }

        [JsonProperty("returns")]
        public ArrayList ReturnSet { get; set; }

        [JsonProperty("deregistration_reasonSet")]
        public DeregistrationSet Deregistration_ReasonSet {get; set;}

    }

    public class DeregistrationSet
    {
        [JsonProperty("results")]
        public List<DeregistrationSetResult> Results { get; set; }
    }

    public class DeregistrationSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("FieldName")]
        public string FieldName { get; set; }

        [JsonProperty("LineNo")]
        public int LineNo { get; set; }

        [JsonProperty("Msgty")]
        public string Msgty { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

    }

    
    public partial class TinDeregistrationSendResponseModel
    {
        public Metadata Metadata { get; set; }

        [JsonProperty("assignToMe")]
        public string Assignme { get; set; }

        [JsonProperty("caseId")]
        public string Caseid { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("void")]
        public string Xvoidz { get; set; }

        [JsonProperty("TINInProcessing")]
        public string TinInPrcFg { get; set; }

        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }

        [JsonProperty("submit")]
        public string Submitz { get; set; }

        [JsonProperty("SEZTaxpayer")]
        public string SezTpFlag { get; set; }

        [JsonProperty("deregistartion")]
        public string BgDregFlg { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("save")]
        public string Savez { get; set; }

        [JsonProperty("reject")]
        public string Rejectz { get; set; }

        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; }

        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }

        [JsonProperty("periodKey")]
        public string PeriodKeyz { get; set; }

        [JsonProperty("userName")]
        public string OfficerUidz { get; set; }

        [JsonProperty("month")]
        public string Monthz { get; set; }

        [JsonProperty("legacyDocumentNumber")]
        public string LegacyDocNo { get; set; }

        [JsonProperty("language")]
        public string Langz { get; set; }

        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }

      //  [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }
        [JsonProperty("userStatus")]
        public string Fbust { get; set; }

        [JsonProperty("display")]
        public string Dflag { get; set; }

        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; }

        [JsonProperty("change")]
        public string Cflag { get; set; }

        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }

        [JsonProperty("auditor")]
        public string Auditorz { get; set; }

        [JsonProperty("transactionTIN")]
        public string ATransTin { get; set; }

        [JsonProperty("title")]
        public string ATitle { get; set; }

        [JsonProperty("TINType")]
        public string ATinType { get; set; }

        //[JsonProperty("TIN")]
        public string ATin { get; set; }

        [JsonProperty("taxpayerName")]
        public string ATaxpayerName { get; set; }

        [JsonProperty("submissionDateHijri")]
        public string ASubmissionDateH { get; set; }

        [JsonProperty("submissionDateCalendar")]
        public string ASubmissionDateC { get; set; }

        [JsonProperty("submissionDate")]
        public string ASubmissionDate { get; set; }

        [JsonProperty("step")]
        public string AStep { get; set; }

        [JsonProperty("approve")]
        public string Approvez { get; set; }

        [JsonProperty("officialUseOrigin")]
        public string AOffOrigin { get; set; }

        [JsonProperty("officialApplicationNumber")]
        public string AOffAppNo { get; set; }

        [JsonProperty("firstName7")]
        public string ANm7 { get; set; }

        [JsonProperty("firstName6")]
        public string ANm6 { get; set; }

        [JsonProperty("firstName5")]
        public string ANm5 { get; set; }

        [JsonProperty("firstName4")]
        public string ANm4 { get; set; }

        [JsonProperty("firstName3")]
        public string ANm3 { get; set; }

        [JsonProperty("firstName2")]
        public string ANm2 { get; set; }

        [JsonProperty("firstName1")]
        public string ANm1 { get; set; }

        [JsonProperty("amendmentReason")]
        public string AmdRsnz { get; set; }

        [JsonProperty("idType")]
        public string AIdType { get; set; }

        [JsonProperty("idNumber")]
        public string AIdNo { get; set; }

        [JsonProperty("formStatus")]
        public string AFormStatus { get; set; }

        [JsonProperty("expiryDateHijri")]
        public string AExpdtH { get; set; }

        [JsonProperty("expiryDateCalendar")]
        public string AExpdtC { get; set; }

        [JsonProperty("expiryDate")]
        public string AExpdt { get; set; }

        [JsonProperty("effectiveDateHijri")]
        public string AEffectiveDtH { get; set; }

        [JsonProperty("effectiveDateCalendar")]
        public string AEffectiveDtC { get; set; }

        [JsonProperty("effectiveDate")]
        public string AEffectiveDt { get; set; }

        [JsonProperty("deregistrationReason")]
        public string ADregReason { get; set; }

        [JsonIgnore]
        public string ADeregSelectedReasonValue { get; set; }

        [JsonProperty("deregistrationOption")]
        public string ADregOpt { get; set; }

        [JsonProperty("document9")]
        public string ADocumnt9 { get; set; }

        [JsonProperty("document8")]
        public string ADocumnt8 { get; set; }

        [JsonProperty("document7")]
        public string ADocumnt7 { get; set; }

        [JsonProperty("document6")]
        public string ADocumnt6 { get; set; }

        [JsonProperty("document5Description")]
        public string ADocumnt5Txt { get; set; }

        [JsonProperty("document5")]
        public string ADocumnt5 { get; set; }

        [JsonProperty("document4Description")]
        public string ADocumnt4Txt { get; set; }

        [JsonProperty("document4")]
        public string ADocumnt4 { get; set; }

        [JsonProperty("document3")]
        public string ADocumnt3 { get; set; }

        [JsonProperty("document2")]
        public string ADocumnt2 { get; set; }

        [JsonProperty("document14")]
        public string ADocumnt14 { get; set; }

        [JsonProperty("document13")]
        public string ADocumnt13 { get; set; }

        [JsonProperty("document12")]
        public string ADocumnt12 { get; set; }

        [JsonProperty("document11")]
        public string ADocumnt11 { get; set; }

        [JsonProperty("document10")]
        public string ADocumnt10 { get; set; }

        [JsonProperty("document1")]
        public string ADocumnt1 { get; set; }

        [JsonProperty("birthDateHijri")]
        public string ADobH { get; set; }

        [JsonProperty("birthDateCalendar")]
        public string ADobC { get; set; }

        [JsonProperty("birthDate")]
        public string ADob { get; set; }

        [JsonProperty("deregister")]
        public string ADegister { get; set; }

        [JsonProperty("declarationTitle")]
        public string ADecTitle { get; set; }

        [JsonProperty("declarationTelephoneNumber")]
        public string ADecTelNo { get; set; }

        [JsonProperty("declarationName")]
        public string ADecName { get; set; }

        [JsonProperty("declarationCheckbox")]
        public string ADeclarationChkbox { get; set; }

        [JsonProperty("declarationDesignation")]
        public string ADecDesig { get; set; }

        [JsonProperty("declarationDateHijri")]
        public string ADecDateH { get; set; }

        [JsonProperty("declarationDateCalendar")]
        public string ADecDateC { get; set; }

        [JsonProperty("declarationDate")]
        public string ADecDate { get; set; }

        [JsonProperty("date")]
        public string ADateFormat { get; set; }

        [JsonProperty("branchDescription")]
        public string ABranchTxt { get; set; }

        [JsonProperty("partnerType")]
        public string ABpKind { get; set; }

        [JsonProperty("permitsTable")]
        public List<string> PermitTableSet { get; set; }

        [JsonProperty("offNotes")]
        public List<string> OffNotesSet { get; set; }

        [JsonProperty("permits")]
        public Array PermitSet { get; set; }

        [JsonProperty("outlets")]
        public Array OutletSet { get; set; }

        [JsonProperty("attachments")]
        public List<string> AttDetSet { get; set; }

        [JsonProperty("returns")]
        public List<string> ReturnSet { get; set; }


    }
   
    public partial class AttachmentSet : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {

            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));

            }

        }

        [JsonIgnore]

        public List<Attachment> _results { get; set; }



        [JsonProperty("results")]

        public List<Attachment> Results

        {

            get

            {

                return _results;

            }

            set

            {

                _results = value;

                OnPropertyRaised(nameof(Results));

            }

        }

    }
   
    public partial class PermitSet
    {
        [JsonProperty("results")]
        public PermitSetResult[] Results { get; set; }
    }
   
    public partial class AttDetSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("DocUrl")]
        public Uri DocUrl { get; set; }

        [JsonProperty("RetGuid")]
        public string RetGuid { get; set; }

        [JsonProperty("Seqno")]
        public string Seqno { get; set; }

        [JsonProperty("SchGuid")]
        public string SchGuid { get; set; }

        [JsonProperty("Dotyp")]
        public string Dotyp { get; set; }

        [JsonProperty("Srno")]
        public long Srno { get; set; }

        [JsonProperty("Doguid")]
        public string Doguid { get; set; }

        [JsonProperty("AttBy")]
        public string AttBy { get; set; }

        [JsonProperty("Filename")]
        public string Filename { get; set; }

        [JsonProperty("FileExtn")]
        public string FileExtn { get; set; }

        [JsonProperty("Mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("Erfdt")]
        public string Erfdt { get; set; }

        [JsonProperty("Erftm")]
        public string Erftm { get; set; }

        [JsonProperty("Enbedit")]
        public string Enbedit { get; set; }

        [JsonProperty("Enbdele")]
        public string Enbdele { get; set; }

        [JsonProperty("Visedit")]
        public string Visedit { get; set; }

        [JsonProperty("Visdel")]
        public string Visdel { get; set; }


    }

   
    public partial class Set : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        [JsonIgnore]
        public OutletSetResult[] _results { get; set; }

        [JsonProperty("results")]
        public OutletSetResult[] Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyRaised(nameof(Results));
            }
        }
    }
   
    public partial class OutletSetResult : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        [JsonIgnore]
        private bool showPermit { get; set; }

        [JsonIgnore]
        public bool ShowPermit
        {
            get { return showPermit; }
            set
            {
                showPermit = value;
                OnPropertyRaised("ShowPermit");
            }
        }

        public Metadata Metadata { get; set; }

        [JsonProperty("address")]
        public string AOutletCompAddr { get; set; }

        [JsonProperty("newMainNumber")]
        public string AOutletNewMainOutnumTb { get; set; }

        [JsonProperty("expiryDate")]
        public string AOutletExpdtTb { get; set; }

        [JsonProperty("outlet")]
        public string AOutletFlag { get; set; }

        [JsonProperty("houseNumber")]
        public string AOutletHouseNoTb { get; set; }

        [JsonProperty("expiryDateHijri")]
        public string AOutletExpdtHTb { get; set; }

        [JsonProperty("mobileNumber")]
        public string AOutletMobileNoTb { get; set; }

        [JsonProperty("expiryDateCalendar")]
        public string AOutletExpdtCTb { get; set; }

        [JsonProperty("name6")]
        public string AOutletNm6Tb { get; set; }

        [JsonProperty("number")]
        public string AOutletNoTb { get; set; }

        [JsonProperty("title")]
        public string AOutletTitleTb { get; set; }

        [JsonProperty("inProcessing")]
        public string OutletInPrcFg { get; set; }

        [JsonProperty("buildingNumber")]
        public string AOutletBuildingNoTb { get; set; }

        [JsonProperty("outletCommencement")]
        public string AOutletComcdFlag { get; set; }

        [JsonProperty("email")]
        public string AOutletEmailTb { get; set; }

        [JsonProperty("name")]
        public string AOutletNameTb { get; set; }

        [JsonProperty("name5")]
        public string AOutletNm5Tb { get; set; }

        [JsonProperty("name7")]
        public string AOutletNm7Tb { get; set; }

        [JsonProperty("mainOutlet")]
        public string AOutletMainFlagTb { get; set; }

        [JsonProperty("POBox")]
        public string AOutletPoBoxTb { get; set; }

        [JsonProperty("street1")]
        public string AOutletStreet1Tb { get; set; }

        [JsonProperty("toDeregister")]
        public string AOutletToDeregTb { get; set; }

        [JsonProperty("deregisterOption")]
        public string AOutletDregOptTb { get; set; }

        [JsonProperty("street2")]
        public string AOutletStreet2Tb { get; set; }

        [JsonProperty("effectiveDate")]
        public string AOutletEffDtTb { get; set; }

        [JsonProperty("province")]
        public string AOutletProvinceTb { get; set; }

        [JsonProperty("city")]
        public string AOutletCityTb { get; set; }

        [JsonProperty("effectiveDateHijri")]
        public string AOutletEffDtHTb { get; set; }

        [JsonProperty("effectiveDateCalendar")]
        public string AOutletEffDtCTb { get; set; }

        [JsonProperty("quarter")]
        public string AOutletQuarterTb { get; set; }

        [JsonProperty("country")]
        public string AOutletCountryTb { get; set; }

        [JsonProperty("transferTIN")]
        public string AOutletTransTinTb { get; set; }

        [JsonProperty("idType")]
        public string AOutletIdTypeTb { get; set; }

        [JsonProperty("postalCode")]
        public string AOutletPostalCodeTb { get; set; }

        [JsonProperty("identificationNumber")]
        public string AOutletIdentificationNoTb { get; set; }

        [JsonProperty("idNumber")]
        public string AOutletIdNoTb { get; set; }

        [JsonProperty("name1")]
        public string AOutletNm1Tb { get; set; }

        [JsonProperty("name12")]
        public string AOutletNm2Tb { get; set; }

        [JsonProperty("name13")]
        public string AOutletNm3Tb { get; set; }

        [JsonProperty("name14")]
        public string AOutletNm4Tb { get; set; }

        [JsonProperty("birthDate")]
        public string AOutletDobTb { get; set; }

        [JsonProperty("birthDateHijri")]
        public string AOutletDobHTb { get; set; }

        [JsonProperty("birthDateCalendar")]
        public string AOutletDobCTb { get; set; }

        [JsonIgnore]
        private List<PermitSetResult> _permitTypes { get; set; }
        [JsonIgnore]
        public List<PermitSetResult> PermitTypes
        {
            get
            {
                return _permitTypes;
            }
            set
            {
                if (_permitTypes == value) return;
                _permitTypes = value;
                OnPropertyRaised("PermitTypes");
            }
        }


        [JsonIgnore]
        private string _reasonDescription { get; set; }
        [JsonIgnore]
        public string ReasonDescription
        {
            get
            {
                return _reasonDescription;
            }
            set
            {
                _reasonDescription = value;
                OnPropertyRaised("ReasonDescription");
            }
        }
    }
   
    public class PermitSetResult : INotifyPropertyChanged
    {
        public PermitSetResult()
        {
            PopulateIdTypeTypeFromList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        [JsonProperty("newMainIdNumber")]
        public string APermitNewMainNoIdTb { get; set; }

        [JsonProperty("address")]
        public string APermitAdrFlag { get; set; }

        [JsonProperty("comboBox")]
        public string APermitCbFlag { get; set; }

        [JsonProperty("permitCommencement")]
        public string APermitConFlag { get; set; }

        [JsonProperty("government")]
        public string APermitGovFlag { get; set; }

        [JsonProperty("mainActivity")]
        public string APermitMainActFlagTb { get; set; }

        [JsonProperty("mainNumber")]
        public string APermitMainnoTb { get; set; }

        private string _aPermitNm6Tb;
        [JsonProperty("name6")]
        public string APermitNm6Tb
        {
            get => _aPermitNm6Tb;
            set
            {
                _aPermitNm6Tb = value;
                OnPropertyRaised(nameof(APermitNm6Tb));
            }
        }

        [JsonProperty("number")]
        public string APermitNoTb { get; set; }

        [JsonProperty("typeDescription")]
        public string APermitTypTxt { get; set; }

        [JsonProperty("inProcessing")]
        public string PermitInPrcFg { get; set; }

        [JsonProperty("expiryDateHijri")]
        public string APermitExpdtHTb { get; set; }

        [JsonProperty("mainActivityNumber")]
        public string APermitMainActNoTb { get; set; }

        [JsonProperty("outletNumber")]
        public string APermitOutletnoTb { get; set; }

        [JsonProperty("expiryDateCalendar")]
        public string APermitExpdtCTb { get; set; }

        [JsonProperty("title")]
        public string APermitTitleTb { get; set; }

        private string _aPermitNm5Tb;
        [JsonProperty("name5")]
        public string APermitNm5Tb
        {
            get => _aPermitNm5Tb;
            set
            {
                _aPermitNm5Tb = value;
                OnPropertyRaised(nameof(APermitNm5Tb));
            }
        }

        private string _aPermitNm7Tb;
        [JsonProperty("name7")]
        public string APermitNm7Tb
        {
            get => _aPermitNm7Tb;
            set
            {
                _aPermitNm7Tb = value;
                OnPropertyRaised(nameof(APermitNm7Tb));
            }
        }

        [JsonProperty("type")]
        public string APermitTypeTb { get; set; }

        [JsonProperty("validFromDate")]
        public string APermitValfrDtTb { get; set; }

        [JsonProperty("validFromDateHijri")]
        public string APermitValfrDtHTb { get; set; }

        [JsonProperty("validFromDateCalendar")]
        public string APermitValfrDtCTb { get; set; }

        [JsonIgnore]
        public string aPermitEffDtTb;

        [JsonProperty("effectiveDate")]
        public string APermitEffDtTb
        {
            get { return aPermitEffDtTb; }
            set
            { if (!string.IsNullOrEmpty(value)) { aPermitEffDtTb = value; OnPropertyRaised(nameof(APermitEffDtTb)); } }
        }

        [JsonProperty("effectiveDateHijri")]
        public string APermitEffDtHTb { get; set; }

        [JsonIgnore]
        public string aPermitDeregDisplayDate { get; set; }

        [JsonIgnore]
        public string APermitDeregDisplayDate
        {
            get { return aPermitDeregDisplayDate; }
            set
            { { aPermitDeregDisplayDate = value; OnPropertyRaised(nameof(APermitDeregDisplayDate)); } }
        }

        [JsonIgnore]
        private bool aPermitIsReasonSelected { get; set; }

        [JsonIgnore]
        public bool APermitIsReasonSelected
        {
            get { return aPermitIsReasonSelected; }
            set
            {
                aPermitIsReasonSelected = value;
                OnPropertyRaised(nameof(APermitIsReasonSelected));
            }
        }

        [JsonIgnore]
        private bool isHijiri { get; set; }

        [JsonIgnore]
        public bool IsHijiri
        {
            get { return isHijiri; }
            set
            {
                try
                {
                    if (isHijiri == value) return;
                    if (value)
                    {
                        if (!string.IsNullOrEmpty(APermitDeregDisplayDate))
                            APermitDeregDisplayDate = UtilityManager.ConvertToHijri(APermitDeregDisplayDate);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(APermitDeregDisplayDate))
                            APermitDeregDisplayDate = UtilityManager.HijriToGreg(APermitDeregDisplayDate);

                    }
                    if (!string.IsNullOrEmpty(APermitDeregDisplayDate))
                        APermitEffDtTb = UtilityManager.ConvertDateFormat(APermitDeregDisplayDate);
                    isHijiri = value;
                    OnPropertyRaised("IsHijiri");
                }
                catch (Exception)
                {

                }

            }
        }
        [JsonIgnore]
        private bool isDOBHijiri { get; set; }

        [JsonIgnore]
        public bool IsDOBHijiri
        {
            get { return isDOBHijiri; }
            set
            {
                if (isDOBHijiri == value) return;

                if (value)
                    APermitDeregDisplayDobDate = UtilityManager.ConvertToHijri(APermitDeregDisplayDobDate);
                else
                    APermitDeregDisplayDobDate = UtilityManager.HijriToGreg(APermitDeregDisplayDobDate);
                if (!string.IsNullOrEmpty(APermitDeregDisplayDobDate))
                    APermitDobTb = UtilityManager.ConvertDateFormat(APermitDeregDisplayDobDate);
                isDOBHijiri = value;
                OnPropertyRaised("IsDOBHijiri");
            }
        }
        [JsonIgnore]
        public string aPermitDeregDisplayDobDate { get; set; }

        [JsonIgnore]
        public string APermitDeregDisplayDobDate
        {
            get { return aPermitDeregDisplayDobDate; }
            set
            { /*if (!string.IsNullOrEmpty(value)) {*/ aPermitDeregDisplayDobDate = value; OnPropertyRaised(nameof(APermitDeregDisplayDobDate)); /*}*/ }
        }

        [JsonIgnore]
        public string aPermitDisplayReason { get; set; }

        [JsonIgnore]
        public string APermitDisplayReason
        {
            get { return aPermitDisplayReason; }
            set
            { if (!string.IsNullOrEmpty(value)) { aPermitDisplayReason = value; OnPropertyRaised(nameof(APermitDisplayReason)); } }
        }

        [JsonIgnore]
        public string aPermitDregRsnTb { get; set; }

        [JsonProperty("deregistration")]
        public string APermitDregRsnTb
        {
            get
            {
                if (aPermitDregRsnTb == null)
                {
                    return string.Empty;
                }
                else
                    return aPermitDregRsnTb;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    aPermitDregRsnTb = value;
                    if (!string.IsNullOrEmpty(aPermitDregRsnTb))
                    {
                        APermitIsReasonSelected = true;
                        if (value == "1")
                        {
                            APermitDisplayReason = AppResources.TinDeregistrationClosed;
                            ReasonDescription = AppResources.TinDeregistrationClosed;
                        }
                        else
                        {
                            APermitDisplayReason = AppResources.TinDeregistrationTransfer;
                            ReasonDescription = AppResources.TinDeregistrationTransfer;
                        }
                    }
                    OnPropertyRaised(nameof(APermitDregRsnTb));
                }
            }
        }

        [JsonProperty("effectiveDateCalendar")]
        public string APermitEffDtCTb { get; set; }

        [JsonIgnore]
        public string aPermitIdNoTb { get; set; }

        [JsonProperty("idNumber")]
        public string APermitIdNoTb
        {
            get
            {
                if (aPermitIdNoTb == null)
                {
                    return string.Empty;
                }

                return aPermitIdNoTb;
            }
            set
            {
                aPermitIdNoTb = value;
                if (!string.IsNullOrEmpty(value))
                {
                    Task.Run(async () =>
                    {

                        string resultData = await VATChangeFillingWebServiceManager.GAZTGetTInNumberData(value);
                        string _responseData = JObject.Parse(resultData)["d"].ToString();
                        VATSignUpD IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                        if (_responseData != null)
                        {


                            IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();
                            if (idType != null)
                            {
                                APermitDobHTb = IDTypeDataModel.TaxpDob;
                                APermitIdTypeTb = idType.key;
                                APermitTransTinTb = IDTypeDataModel.TIN;
                            }

                        }

                    });
                }
                OnPropertyRaised(nameof(APermitIdNoTb));
            }
        }

        [JsonIgnore]
        private string aPermitTransTinTb = string.Empty;

        [JsonProperty("transferTIN")]
        public string APermitTransTinTb
        {
            get
            {

                return aPermitTransTinTb;
            }
            set
            {
                aPermitTransTinTb = value;
                if (!string.IsNullOrEmpty(value.Trim()))
                {

                    if (value?.Trim().Length == 10 && !string.IsNullOrWhiteSpace(aPermitIdNoTb))
                    {
                        Task.Run(async () =>
                        {
                            string resultData = await VATChangeFillingWebServiceManager.GAZTGetTInNumberData(aPermitIdNoTb);
                            string _responseData = JObject.Parse(resultData)["d"].ToString();
                            VATSignUpD IDTypeDataModel = JsonConvert.DeserializeObject<VATSignUpD>(_responseData);
                            if (_responseData != null)
                            {
                                //VATSignUpD IDTypeDataModel = new VATSignUpD();
                                //IDTypeDataModel = resultData.d;

                                IBANType idType = IBANTypesList.Where(m => m.key == IDTypeDataModel.Idtype).FirstOrDefault();
                                if (idType != null)
                                {
                                    APermitIdNoTb = IDTypeDataModel.Idnum;
                                    APermitDobHTb = IDTypeDataModel.TaxpDob;
                                    APermitIdTypeTb = idType.key;
                                }

                            }
                        });
                    }


                }
                OnPropertyRaised(nameof(APermitTransTinTb));
            }
        }

        [JsonIgnore]
        private List<IBANType> _iBANTypesList;
        [JsonIgnore]
        public List<IBANType> IBANTypesList
        {
            get
            {
                return _iBANTypesList;
            }
            set
            {
                _iBANTypesList = value;

                OnPropertyRaised("IBANTypesList");
            }
        }

        public void PopulateIdTypeTypeFromList()
        {
            IBANTypesList = new List<IBANType>();
            List<IBANType> IBANTypesDummyList = new List<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.TinDeregistrationNationalID;
            IBANTypesDummyList.Add(iBANType);

            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.TinDeregistrationCompanyID;
            IBANTypesDummyList.Add(iBANType2);

            IBANType iBANType3 = new IBANType();
            iBANType3.key = "ZS0002";
            iBANType3.Text = AppResources.TinDeregistrationIQAMANumber;
            IBANTypesDummyList.Add(iBANType3);

            IBANType iBANType4 = new IBANType();
            iBANType4.key = "ZS0003";
            iBANType4.Text = AppResources.TinDeregistrationGCCID;
            IBANTypesDummyList.Add(iBANType4);

            IBANTypesList = IBANTypesDummyList;
        }

        [JsonIgnore]
        public string aPermitIdTypeTb;
        [JsonProperty("idType")]
        public string APermitIdTypeTb
        {
            get
            {
                return aPermitIdTypeTb;
            }
            set
            {
                aPermitIdTypeTb = value;

                if (aPermitIdTypeTb == "ZS0001")
                {
                    PermitIdTypeName = AppResources.TinDeregistrationNationalID;
                    APermitIsCompanyId = false;
                }
                else if (aPermitIdTypeTb == "ZS0005")
                {
                    PermitIdTypeName = AppResources.TinDeregistrationCompanyID;
                    APermitIsCompanyId = true;
                }
                else if (aPermitIdTypeTb == "ZS0002")
                {
                    PermitIdTypeName = AppResources.TinDeregistrationIQAMANumber;
                    APermitIsCompanyId = false;
                }
                else if (aPermitIdTypeTb == "ZS0003")
                {
                    PermitIdTypeName = AppResources.TinDeregistrationGCCID;
                    APermitIsCompanyId = false;
                }

                OnPropertyRaised(nameof(APermitIdTypeTb));
            }
        }

        private bool _aPermitEditable = false;
        [JsonIgnore]
        public bool APermitEditable
        {
            get => _aPermitEditable;
            set
            {
                _aPermitEditable = value;
                OnPropertyRaised(nameof(APermitEditable));
            }
        }

        private bool _aPermitIsCompanyId = false;
        [JsonIgnore]
        public bool APermitIsCompanyId
        {
            get => _aPermitIsCompanyId;
            set
            {
                _aPermitIsCompanyId = value;
                OnPropertyRaised(nameof(APermitIsCompanyId));
            }
        }

        [JsonProperty("name1")]
        public string APermitNm1Tb { get; set; }

        [JsonProperty("name2")]
        public string APermitNm2Tb { get; set; }

        private string _aPermitNm3Tb;
        [JsonProperty("name3")]
        public string APermitNm3Tb
        {
            get => _aPermitNm3Tb;
            set
            {
                _aPermitNm3Tb = value;
                OnPropertyRaised(nameof(APermitNm3Tb));
            }
        }

        private string _aPermitNm4Tb;
        [JsonProperty("name4")]
        public string APermitNm4Tb
        {
            get => _aPermitNm4Tb;
            set
            {
                _aPermitNm4Tb = value;
                OnPropertyRaised(nameof(APermitNm4Tb));
            }
        }

        [JsonProperty("birthDate")]
        public string APermitDobTb { get; set; }

        [JsonProperty("birthDatHijri")]
        public string APermitDobHTb { get; set; }

        [JsonProperty("birthDateCalendar")]
        public string APermitDobCTb { get; set; }

        [JsonIgnore]
        private string permitIdTypeName;
        [JsonIgnore]
        public string PermitIdTypeName
        {
            get { return permitIdTypeName; }
            set
            {

                permitIdTypeName = value;
                OnPropertyRaised("PermitIdTypeName");

            }
        }

        [JsonIgnore]
        private string _reasonDescription { get; set; }
        [JsonIgnore]
        public string ReasonDescription
        {
            get
            {
                return _reasonDescription;
            }
            set
            {
                _reasonDescription = value;
                OnPropertyRaised("ReasonDescription");
            }
        }
    }
   
    public partial class TinDeregistrationReasonSet
    {
        [JsonProperty("d")]
        public TinDeregistrationReasonSetDataModel D { get; set; }
    }
   
    public partial class TinDeregistrationReasonSetDataModel
    {
       // [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("TIN")]
        public string Partner { get; set; }

        [JsonProperty("language")]
        public string Spars { get; set; }

        //[JsonProperty("OutletSet")]
        public OutletSet OutletSet { get; set; }

        [JsonProperty("reasons")]
        public TinDeregReasonSetResult[] ReasonSet { get; set; }
        [JsonProperty("deregistrationReasons")]
        public TinDeregReasonSetResult[] Reasons { set { ReasonSet = value; } }
    }
   
    public partial class OutletSet
    {
        [JsonProperty("__deferred")]
        public Deferred Deferred { get; set; }
    }
   
    public partial class Deferred
    {
        [JsonProperty("uri")]
        public Uri Uri { get; set; }
    }
   
    public partial class TinDeregReasonSet
    {
        [JsonProperty("deregistrationReasons")]
        public TinDeregReasonSetResult[] Results { get; set; }
    }
   
    public partial class TinDeregReasonSetResult
    {
       // [JsonProperty("__metadata")]
        public Metadata MetadataReasonSet { get; set; }

        [JsonProperty("reasonCode")]
        public string ReasonCd { get; set; }

        [JsonProperty("reasonDescription")]
        public string ReasonDesc { get; set; }
    }
   
    public partial class FieldValidations : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _fieldName { get; set; }
        public string FieldName
        {
            get
            {
                return _fieldName;
            }
            set
            {
                _fieldName = value;
                OnPropertyRaised("FieldName");
            }
        }

        private string _fieldValue { get; set; }
        public string FieldValue
        {
            get
            {
                return _fieldValue;
            }
            set
            {
                _fieldValue = value;
                OnPropertyRaised("FieldValue");
            }
        }

        private bool _isMandatory { get; set; }
        public bool IsMandatory
        {
            get
            {
                return _isMandatory;
            }
            set
            {
                _isMandatory = value;
                OnPropertyRaised("IsMandatory");
            }
        }

        private bool _isVisible { get; set; }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyRaised("IsVisible");
            }
        }

        private bool _isEditable { get; set; }
        public bool IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                OnPropertyRaised("IsEditable");
            }
        }
    }
}
