using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace EGAZT.Models
{
    public class TINDeregistrationModel
    {
        public TINDeregistrationModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
        public string OutletOptionIndex { get; set; }

    }

    public class TinDeregestrationAttachmentsModel : INotifyPropertyChanged
    {
        public TinDeregestrationAttachmentsModel()
        {
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
    }

    public partial class TinDeregistrationParentResponseModel
    {
        [JsonProperty("d")]
        public TinDeregistrationResponseModel D { get; set; }
    }

    public partial class TinDeregistrationResponseModel
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Assignme")]
        public string Assignme { get; set; }

        [JsonProperty("Caseid")]
        public string Caseid { get; set; }

        [JsonProperty("Xvoidz")]
        public string Xvoidz { get; set; }

        [JsonProperty("TinInPrcFg")]
        public string TinInPrcFg { get; set; }

        [JsonProperty("Taxpayerz")]
        public string Taxpayerz { get; set; }

        [JsonProperty("Submitz")]
        public string Submitz { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Savez")]
        public string Savez { get; set; }

        [JsonProperty("Rejectz")]
        public string Rejectz { get; set; }

        [JsonProperty("RegIdz")]
        public string RegIdz { get; set; }

        [JsonProperty("PortalUsrz")]
        public string PortalUsrz { get; set; }

        [JsonProperty("PeriodKeyz")]
        public string PeriodKeyz { get; set; }

        [JsonProperty("Operation")]
        public string Operation { get; set; }

        [JsonProperty("OfficerUidz")]
        public string OfficerUidz { get; set; }

        [JsonProperty("Monthz")]
        public string Monthz { get; set; }

        [JsonProperty("LegacyDocNo")]
        public string LegacyDocNo { get; set; }

        [JsonProperty("Langz")]
        public string Langz { get; set; }

        [JsonProperty("FormGuid")]
        public string FormGuid { get; set; }

        [JsonProperty("Fbnumz")]
        public string Fbnumz { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Dflag")]
        public string Dflag { get; set; }

        [JsonProperty("CreateTxAssesz")]
        public string CreateTxAssesz { get; set; }

        [JsonProperty("Cflag")]
        public string Cflag { get; set; }

        [JsonProperty("CaseGuid")]
        public string CaseGuid { get; set; }

        [JsonProperty("Auditorz")]
        public string Auditorz { get; set; }

        [JsonProperty("ATransTin")]
        public string ATransTin { get; set; }

        [JsonProperty("ATitle")]
        public string ATitle { get; set; }

        [JsonProperty("ATinType")]
        public string ATinType { get; set; }

        [JsonProperty("ATin")]
        public string ATin { get; set; }

        [JsonProperty("ATaxpayerName")]
        public string ATaxpayerName { get; set; }

        [JsonProperty("ASubmissionDateH")]
        public string ASubmissionDateH { get; set; }

        [JsonProperty("ASubmissionDateC")]
        public string ASubmissionDateC { get; set; }

        [JsonProperty("ASubmissionDate")]
        public string ASubmissionDate { get; set; }

        [JsonProperty("AStep")]
        public long AStep { get; set; }

        [JsonProperty("Approvez")]
        public string Approvez { get; set; }

        [JsonProperty("AOffOrigin")]
        public string AOffOrigin { get; set; }

        [JsonProperty("AOffAppNo")]
        public string AOffAppNo { get; set; }

        [JsonProperty("ANm7")]
        public string ANm7 { get; set; }

        [JsonProperty("ANm6")]
        public string ANm6 { get; set; }

        [JsonProperty("ANm5")]
        public string ANm5 { get; set; }

        [JsonProperty("ANm4")]
        public string ANm4 { get; set; }

        [JsonProperty("ANm3")]
        public string ANm3 { get; set; }

        [JsonProperty("ANm2")]
        public string ANm2 { get; set; }

        [JsonProperty("ANm1")]
        public string ANm1 { get; set; }

        [JsonProperty("AmdRsnz")]
        public string AmdRsnz { get; set; }

        [JsonProperty("AIdType")]
        public string AIdType { get; set; }

        [JsonProperty("AIdNo")]
        public string AIdNo { get; set; }

        [JsonProperty("AFormStatus")]
        public string AFormStatus { get; set; }

        [JsonProperty("AExpdtH")]
        public string AExpdtH { get; set; }

        [JsonProperty("AExpdtC")]
        public string AExpdtC { get; set; }

        [JsonProperty("AExpdt")]
        public string AExpdt { get; set; }

        [JsonProperty("AEffectiveDtH")]
        public string AEffectiveDtH { get; set; }

        [JsonProperty("AEffectiveDtC")]
        public string AEffectiveDtC { get; set; }

        [JsonProperty("AEffectiveDt")]
        public string AEffectiveDt { get; set; }

        [JsonProperty("ADregReason")]
        public string ADregReason { get; set; }

        [JsonIgnore]
        public string ADeregSelectedReasonValue { get; set; }

        [JsonProperty("ADregOpt")]
        public string ADregOpt { get; set; }

        [JsonProperty("ADocumnt9")]
        public string ADocumnt9 { get; set; }

        [JsonProperty("ADocumnt8")]
        public string ADocumnt8 { get; set; }

        [JsonProperty("ADocumnt7")]
        public string ADocumnt7 { get; set; }

        [JsonProperty("ADocumnt6")]
        public string ADocumnt6 { get; set; }

        [JsonProperty("ADocumnt5Txt")]
        public string ADocumnt5Txt { get; set; }

        [JsonProperty("ADocumnt5")]
        public string ADocumnt5 { get; set; }

        [JsonProperty("ADocumnt4Txt")]
        public string ADocumnt4Txt { get; set; }

        [JsonProperty("ADocumnt4")]
        public string ADocumnt4 { get; set; }

        [JsonProperty("ADocumnt3")]
        public string ADocumnt3 { get; set; }

        [JsonProperty("ADocumnt2")]
        public string ADocumnt2 { get; set; }

        [JsonProperty("ADocumnt14")]
        public string ADocumnt14 { get; set; }

        [JsonProperty("ADocumnt13")]
        public string ADocumnt13 { get; set; }

        [JsonProperty("ADocumnt12")]
        public string ADocumnt12 { get; set; }

        [JsonProperty("ADocumnt11")]
        public string ADocumnt11 { get; set; }

        [JsonProperty("ADocumnt10")]
        public string ADocumnt10 { get; set; }

        [JsonProperty("ADocumnt1")]
        public string ADocumnt1 { get; set; }

        [JsonProperty("ADobH")]
        public string ADobH { get; set; }

        [JsonProperty("ADobC")]
        public string ADobC { get; set; }

        [JsonProperty("ADob")]
        public string ADob { get; set; }

        [JsonProperty("ADegister")]
        public string ADegister { get; set; }

        [JsonProperty("ADecTitle")]
        public string ADecTitle { get; set; }

        [JsonProperty("ADecTelNo")]
        public string ADecTelNo { get; set; }

        [JsonProperty("ADecName")]
        public string ADecName { get; set; }

        [JsonProperty("ADeclarationChkbox")]
        public string ADeclarationChkbox { get; set; }

        [JsonProperty("ADecDesig")]
        public string ADecDesig { get; set; }

        [JsonProperty("ADecDateH")]
        public string ADecDateH { get; set; }

        [JsonProperty("ADecDateC")]
        public string ADecDateC { get; set; }

        [JsonProperty("ADecDate")]
        public string ADecDate { get; set; }

        [JsonProperty("ADateFormat")]
        public string ADateFormat { get; set; }

        [JsonProperty("ABranchTxt")]
        public string ABranchTxt { get; set; }

        [JsonProperty("ABpKind")]
        public string ABpKind { get; set; }

        [JsonProperty("Permit_TableSet")]
        public Set PermitTableSet { get; set; }

        [JsonProperty("Off_notesSet")]
        public Set OffNotesSet { get; set; }

        [JsonProperty("PermitSet")]
        public PermitSet PermitSet { get; set; }

        [JsonProperty("OutletSet")]
        public Set OutletSet { get; set; }

        [JsonProperty("AttDetSet")]
        public AttachmentSet AttDetSet { get; set; }

        [JsonProperty("returnSet")]
        public Set ReturnSet { get; set; }


    }

    public partial class TinDeregistrationSendResponseModel
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Assignme")]
        public string Assignme { get; set; }

        [JsonProperty("Caseid")]
        public string Caseid { get; set; }

        [JsonProperty("Xvoidz")]
        public string Xvoidz { get; set; }

        [JsonProperty("TinInPrcFg")]
        public string TinInPrcFg { get; set; }

        [JsonProperty("Taxpayerz")]
        public string Taxpayerz { get; set; }

        [JsonProperty("Submitz")]
        public string Submitz { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Savez")]
        public string Savez { get; set; }

        [JsonProperty("Rejectz")]
        public string Rejectz { get; set; }

        [JsonProperty("RegIdz")]
        public string RegIdz { get; set; }

        [JsonProperty("PortalUsrz")]
        public string PortalUsrz { get; set; }

        [JsonProperty("PeriodKeyz")]
        public string PeriodKeyz { get; set; }

        [JsonProperty("Operation")]
        public string Operation { get; set; }

        [JsonProperty("OfficerUidz")]
        public string OfficerUidz { get; set; }

        [JsonProperty("Monthz")]
        public string Monthz { get; set; }

        [JsonProperty("LegacyDocNo")]
        public string LegacyDocNo { get; set; }

        [JsonProperty("Langz")]
        public string Langz { get; set; }

        [JsonProperty("FormGuid")]
        public string FormGuid { get; set; }

        [JsonProperty("Fbnumz")]
        public string Fbnumz { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Dflag")]
        public string Dflag { get; set; }

        [JsonProperty("CreateTxAssesz")]
        public string CreateTxAssesz { get; set; }

        [JsonProperty("Cflag")]
        public string Cflag { get; set; }

        [JsonProperty("CaseGuid")]
        public string CaseGuid { get; set; }

        [JsonProperty("Auditorz")]
        public string Auditorz { get; set; }

        [JsonProperty("ATransTin")]
        public string ATransTin { get; set; }

        [JsonProperty("ATitle")]
        public string ATitle { get; set; }

        [JsonProperty("ATinType")]
        public string ATinType { get; set; }

        [JsonProperty("ATin")]
        public string ATin { get; set; }

        [JsonProperty("ATaxpayerName")]
        public string ATaxpayerName { get; set; }

        [JsonProperty("ASubmissionDateH")]
        public string ASubmissionDateH { get; set; }

        [JsonProperty("ASubmissionDateC")]
        public string ASubmissionDateC { get; set; }

        [JsonProperty("ASubmissionDate")]
        public string ASubmissionDate { get; set; }

        [JsonProperty("AStep")]
        public long AStep { get; set; }

        [JsonProperty("Approvez")]
        public string Approvez { get; set; }

        [JsonProperty("AOffOrigin")]
        public string AOffOrigin { get; set; }

        [JsonProperty("AOffAppNo")]
        public string AOffAppNo { get; set; }

        [JsonProperty("ANm7")]
        public string ANm7 { get; set; }

        [JsonProperty("ANm6")]
        public string ANm6 { get; set; }

        [JsonProperty("ANm5")]
        public string ANm5 { get; set; }

        [JsonProperty("ANm4")]
        public string ANm4 { get; set; }

        [JsonProperty("ANm3")]
        public string ANm3 { get; set; }

        [JsonProperty("ANm2")]
        public string ANm2 { get; set; }

        [JsonProperty("ANm1")]
        public string ANm1 { get; set; }

        [JsonProperty("AmdRsnz")]
        public string AmdRsnz { get; set; }

        [JsonProperty("AIdType")]
        public string AIdType { get; set; }

        [JsonProperty("AIdNo")]
        public string AIdNo { get; set; }

        [JsonProperty("AFormStatus")]
        public string AFormStatus { get; set; }

        [JsonProperty("AExpdtH")]
        public string AExpdtH { get; set; }

        [JsonProperty("AExpdtC")]
        public string AExpdtC { get; set; }

        [JsonProperty("AExpdt")]
        public string AExpdt { get; set; }

        [JsonProperty("AEffectiveDtH")]
        public string AEffectiveDtH { get; set; }

        [JsonProperty("AEffectiveDtC")]
        public string AEffectiveDtC { get; set; }

        [JsonProperty("AEffectiveDt")]
        public string AEffectiveDt { get; set; }

        [JsonProperty("ADregReason")]
        public string ADregReason { get; set; }

        [JsonIgnore]
        public string ADeregSelectedReasonValue { get; set; }

        [JsonProperty("ADregOpt")]
        public string ADregOpt { get; set; }

        [JsonProperty("ADocumnt9")]
        public string ADocumnt9 { get; set; }

        [JsonProperty("ADocumnt8")]
        public string ADocumnt8 { get; set; }

        [JsonProperty("ADocumnt7")]
        public string ADocumnt7 { get; set; }

        [JsonProperty("ADocumnt6")]
        public string ADocumnt6 { get; set; }

        [JsonProperty("ADocumnt5Txt")]
        public string ADocumnt5Txt { get; set; }

        [JsonProperty("ADocumnt5")]
        public string ADocumnt5 { get; set; }

        [JsonProperty("ADocumnt4Txt")]
        public string ADocumnt4Txt { get; set; }

        [JsonProperty("ADocumnt4")]
        public string ADocumnt4 { get; set; }

        [JsonProperty("ADocumnt3")]
        public string ADocumnt3 { get; set; }

        [JsonProperty("ADocumnt2")]
        public string ADocumnt2 { get; set; }

        [JsonProperty("ADocumnt14")]
        public string ADocumnt14 { get; set; }

        [JsonProperty("ADocumnt13")]
        public string ADocumnt13 { get; set; }

        [JsonProperty("ADocumnt12")]
        public string ADocumnt12 { get; set; }

        [JsonProperty("ADocumnt11")]
        public string ADocumnt11 { get; set; }

        [JsonProperty("ADocumnt10")]
        public string ADocumnt10 { get; set; }

        [JsonProperty("ADocumnt1")]
        public string ADocumnt1 { get; set; }

        [JsonProperty("ADobH")]
        public string ADobH { get; set; }

        [JsonProperty("ADobC")]
        public string ADobC { get; set; }

        [JsonProperty("ADob")]
        public string ADob { get; set; }

        [JsonProperty("ADegister")]
        public string ADegister { get; set; }

        [JsonProperty("ADecTitle")]
        public string ADecTitle { get; set; }

        [JsonProperty("ADecTelNo")]
        public string ADecTelNo { get; set; }

        [JsonProperty("ADecName")]
        public string ADecName { get; set; }

        [JsonProperty("ADeclarationChkbox")]
        public string ADeclarationChkbox { get; set; }

        [JsonProperty("ADecDesig")]
        public string ADecDesig { get; set; }

        [JsonProperty("ADecDateH")]
        public string ADecDateH { get; set; }

        [JsonProperty("ADecDateC")]
        public string ADecDateC { get; set; }

        [JsonProperty("ADecDate")]
        public string ADecDate { get; set; }

        [JsonProperty("ADateFormat")]
        public string ADateFormat { get; set; }

        [JsonProperty("ABranchTxt")]
        public string ABranchTxt { get; set; }

        [JsonProperty("ABpKind")]
        public string ABpKind { get; set; }

        [JsonProperty("Permit_TableSet")]
        public List<string> PermitTableSet { get; set; }

        [JsonProperty("Off_notesSet")]
        public List<string> OffNotesSet { get; set; }

        [JsonProperty("PermitSet")]
        public Array PermitSet { get; set; }

        [JsonProperty("OutletSet")]
        public Array OutletSet { get; set; }

        [JsonProperty("AttDetSet")]
        public List<string> AttDetSet { get; set; }

        [JsonProperty("returnSet")]
        public List<string> ReturnSet { get; set; }


    }

    public partial class AttachmentSet
    {
        [JsonProperty("results")]
        public List<Attachment> Results { get; set; }
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


    public partial class Set
    {
        [JsonProperty("results")]
        public OutletSetResult[] Results { get; set; }
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

        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("AOutletCompAddr")]
        public string AOutletCompAddr { get; set; }

        [JsonProperty("AOutletNewMainOutnumTb")]
        public string AOutletNewMainOutnumTb { get; set; }

        [JsonProperty("AOutletExpdtTb")]
        public string AOutletExpdtTb { get; set; }

        [JsonProperty("AOutletFlag")]
        public string AOutletFlag { get; set; }

        [JsonProperty("AOutletHouseNoTb")]
        public string AOutletHouseNoTb { get; set; }

        [JsonProperty("AOutletExpdtHTb")]
        public string AOutletExpdtHTb { get; set; }

        [JsonProperty("AOutletMobileNoTb")]
        public string AOutletMobileNoTb { get; set; }

        [JsonProperty("AOutletExpdtCTb")]
        public string AOutletExpdtCTb { get; set; }

        [JsonProperty("AOutletNm6Tb")]
        public string AOutletNm6Tb { get; set; }

        [JsonProperty("AOutletNoTb")]
        public string AOutletNoTb { get; set; }

        [JsonProperty("AOutletTitleTb")]
        public string AOutletTitleTb { get; set; }

        [JsonProperty("OutletInPrcFg")]
        public string OutletInPrcFg { get; set; }

        [JsonProperty("AOutletBuildingNoTb")]
        public string AOutletBuildingNoTb { get; set; }

        [JsonProperty("AOutletComcdFlag")]
        public string AOutletComcdFlag { get; set; }

        [JsonProperty("AOutletEmailTb")]
        public string AOutletEmailTb { get; set; }

        [JsonProperty("AOutletNameTb")]
        public string AOutletNameTb { get; set; }

        [JsonProperty("AOutletNm5Tb")]
        public string AOutletNm5Tb { get; set; }

        [JsonProperty("AOutletNm7Tb")]
        public string AOutletNm7Tb { get; set; }

        [JsonProperty("AOutletMainFlagTb")]
        public string AOutletMainFlagTb { get; set; }

        [JsonProperty("AOutletPoBoxTb")]
        public string AOutletPoBoxTb { get; set; }

        [JsonProperty("AOutletStreet1Tb")]
        public string AOutletStreet1Tb { get; set; }

        [JsonProperty("AOutletToDeregTb")]
        public string AOutletToDeregTb { get; set; }

        [JsonProperty("AOutletDregOptTb")]
        public string AOutletDregOptTb { get; set; }

        [JsonProperty("AOutletStreet2Tb")]
        public string AOutletStreet2Tb { get; set; }

        [JsonProperty("AOutletEffDtTb")]
        public string AOutletEffDtTb { get; set; }

        [JsonProperty("AOutletProvinceTb")]
        public string AOutletProvinceTb { get; set; }

        [JsonProperty("AOutletCityTb")]
        public string AOutletCityTb { get; set; }

        [JsonProperty("AOutletEffDtHTb")]
        public string AOutletEffDtHTb { get; set; }

        [JsonProperty("AOutletEffDtCTb")]
        public string AOutletEffDtCTb { get; set; }

        [JsonProperty("AOutletQuarterTb")]
        public string AOutletQuarterTb { get; set; }

        [JsonProperty("AOutletCountryTb")]
        public string AOutletCountryTb { get; set; }

        [JsonProperty("AOutletTransTinTb")]
        public string AOutletTransTinTb { get; set; }

        [JsonProperty("AOutletIdTypeTb")]
        public string AOutletIdTypeTb { get; set; }

        [JsonProperty("AOutletPostalCodeTb")]
        public string AOutletPostalCodeTb { get; set; }

        [JsonProperty("AOutletIdentificationNoTb")]
        public string AOutletIdentificationNoTb { get; set; }

        [JsonProperty("AOutletIdNoTb")]
        public string AOutletIdNoTb { get; set; }

        [JsonProperty("AOutletNm1Tb")]
        public string AOutletNm1Tb { get; set; }

        [JsonProperty("AOutletNm2Tb")]
        public string AOutletNm2Tb { get; set; }

        [JsonProperty("AOutletNm3Tb")]
        public string AOutletNm3Tb { get; set; }

        [JsonProperty("AOutletNm4Tb")]
        public string AOutletNm4Tb { get; set; }

        [JsonProperty("AOutletDobTb")]
        public string AOutletDobTb { get; set; }

        [JsonProperty("AOutletDobHTb")]
        public string AOutletDobHTb { get; set; }

        [JsonProperty("AOutletDobCTb")]
        public string AOutletDobCTb { get; set; }

        [JsonIgnore]
        private ObservableCollection<PermitSetResult> _permitTypes { get; set; }
        [JsonIgnore]
        public ObservableCollection<PermitSetResult> PermitTypes
        {
            get
            {
                return _permitTypes;
            }
            set
            {
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
                OnPropertyRaised("PermitTypes");
            }
        }
    }

    public class PermitSetResult:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        [JsonProperty("APermitNewMainNoIdTb")]
        public string APermitNewMainNoIdTb { get; set; }

        [JsonProperty("APermitAdrFlag")]
        public string APermitAdrFlag { get; set; }

        [JsonProperty("APermitCbFlag")]
        public string APermitCbFlag { get; set; }

        [JsonProperty("APermitConFlag")]
        public string APermitConFlag { get; set; }

        [JsonProperty("APermitGovFlag")]
        public string APermitGovFlag { get; set; }

        [JsonProperty("APermitMainActFlagTb")]
        public string APermitMainActFlagTb { get; set; }

        [JsonProperty("APermitMainnoTb")]
        public string APermitMainnoTb { get; set; }

        [JsonProperty("APermitNm6Tb")]
        public string APermitNm6Tb { get; set; }

        [JsonProperty("APermitNoTb")]
        public string APermitNoTb { get; set; }

        [JsonProperty("APermitTypTxt")]
        public string APermitTypTxt { get; set; }

        [JsonProperty("PermitInPrcFg")]
        public string PermitInPrcFg { get; set; }

        [JsonProperty("APermitExpdtHTb")]
        public string APermitExpdtHTb { get; set; }

        [JsonProperty("APermitMainActNoTb")]
        public string APermitMainActNoTb { get; set; }

        [JsonProperty("APermitOutletnoTb")]
        public string APermitOutletnoTb { get; set; }

        [JsonProperty("APermitExpdtCTb")]
        public string APermitExpdtCTb { get; set; }

        [JsonProperty("APermitTitleTb")]
        public string APermitTitleTb { get; set; }

        [JsonProperty("APermitNm5Tb")]
        public string APermitNm5Tb { get; set; }

        [JsonProperty("APermitNm7Tb")]
        public string APermitNm7Tb { get; set; }

        [JsonProperty("APermitTypeTb")]
        public string APermitTypeTb { get; set; }

        [JsonProperty("APermitValfrDtTb")]
        public string APermitValfrDtTb { get; set; }

        [JsonProperty("APermitValfrDtHTb")]
        public string APermitValfrDtHTb { get; set; }

        [JsonProperty("APermitValfrDtCTb")]
        public string APermitValfrDtCTb { get; set; }

        [JsonIgnore]
        public string aPermitEffDtTb;

        [JsonProperty("APermitEffDtTb")]
        public string APermitEffDtTb { get { return aPermitEffDtTb; } set
            { if (!string.IsNullOrEmpty(value)) { aPermitEffDtTb = value; OnPropertyRaised("APermitEffDtTb"); } } }

        [JsonProperty("APermitEffDtHTb")]
        public string APermitEffDtHTb { get; set; }

        [JsonIgnore]
        public string APermitDeregDisplayDate { get; set; }

        [JsonProperty("APermitEffDtCTb")]
        public string APermitEffDtCTb { get; set; }

        [JsonProperty("APermitDregRsnTb")]
        public string APermitDregRsnTb { get; set; }

        [JsonProperty("APermitTransTinTb")]
        public string APermitTransTinTb { get; set; }

        /*
         IBANTypesList = new ObservableCollection<IBANType>();
            ObservableCollection<IBANType> IBANTypesDummyList = new ObservableCollection<IBANType>();
            IBANType iBANType = new IBANType();
            iBANType.key = "ZS0001";
            iBANType.Text = AppResources.NationaID;
            IBANTypesDummyList.Add(iBANType);

            IBANType iBANType2 = new IBANType();
            iBANType2.key = "ZS0005";
            iBANType2.Text = AppResources.ZIBANCompanyID;
            IBANTypesDummyList.Add(iBANType2);

            IBANType iBANType3 = new IBANType();
            iBANType3.key = "ZS0002";
            iBANType3.Text = AppResources.ZZIqamaID;
            IBANTypesDummyList.Add(iBANType3);

            IBANType iBANType4 = new IBANType();
            iBANType4.key = "ZS0003";
            iBANType4.Text = AppResources.ZZGCCID;  
            IBANTypesDummyList.Add(iBANType4);*/

            [JsonIgnore]
            public string aPermitIdTypeTb;
            [JsonProperty("APermitIdTypeTb")]
            public string APermitIdTypeTb { get { return aPermitIdTypeTb; } set { aPermitIdTypeTb = value;

                if(aPermitIdTypeTb == "ZS0001")
                {
                    PermitIdTypeName = AppResources.NationaID;
                }
                else if(aPermitIdTypeTb == "ZS0005")
                {
                    PermitIdTypeName = AppResources.ZIBANCompanyID;
                }
                else if(aPermitIdTypeTb == "ZS0002")
                {
                    PermitIdTypeName = AppResources.ZZIqamaID;
                }
                else
                {
                    PermitIdTypeName = AppResources.ZZGCCID;
                }

                OnPropertyRaised("APermitIdTypeTb");
        } }

        [JsonProperty("APermitIdNoTb")]
        public string APermitIdNoTb { get; set; }

        [JsonProperty("APermitNm1Tb")]
        public string APermitNm1Tb { get; set; }

        [JsonProperty("APermitNm2Tb")]
        public string APermitNm2Tb { get; set; }

        [JsonProperty("APermitNm3Tb")]
        public string APermitNm3Tb { get; set; }

        [JsonProperty("APermitNm4Tb")]
        public string APermitNm4Tb { get; set; }

        [JsonProperty("APermitDobTb")]
        public object APermitDobTb { get; set; }

        [JsonProperty("APermitDobHTb")]
        public string APermitDobHTb { get; set; }

        [JsonProperty("APermitDobCTb")]
        public string APermitDobCTb { get; set; }

        [JsonIgnore]
        private string permitIdTypeName;
        [JsonIgnore]
        public string PermitIdTypeName
        { get { return permitIdTypeName; } set {

                permitIdTypeName = value;
                OnPropertyRaised("PermitIdTypeName");

            } }

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
                OnPropertyRaised("PermitTypes");
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
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Partner")]
        public string Partner { get; set; }

        [JsonProperty("Spars")]
        public string Spars { get; set; }

        [JsonProperty("OutletSet")]
        public OutletSet OutletSet { get; set; }

        [JsonProperty("REASONSet")]
        public TinDeregReasonSet ReasonSet { get; set; }
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
        [JsonProperty("results")]
        public TinDeregReasonSetResult[] Results { get; set; }
    }

    public partial class TinDeregReasonSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata MetadataReasonSet { get; set; }

        [JsonProperty("ReasonCd")]
        public string ReasonCd { get; set; }

        [JsonProperty("ReasonDesc")]
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
