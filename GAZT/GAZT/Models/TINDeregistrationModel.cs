using System;
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

    }

    public class TinDeregestrationAttachmentsModel: INotifyPropertyChanged
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
        public long AOffOrigin { get; set; }

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
        public object AExpdt { get; set; }

        [JsonProperty("AEffectiveDtH")]
        public string AEffectiveDtH { get; set; }

        [JsonProperty("AEffectiveDtC")]
        public string AEffectiveDtC { get; set; }

        [JsonProperty("AEffectiveDt")]
        public object AEffectiveDt { get; set; }

        [JsonProperty("ADregReason")]
        public string ADregReason { get; set; }

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
        public object ADob { get; set; }

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
        public Set PermitSet { get; set; }

        [JsonProperty("OutletSet")]
        public Set OutletSet { get; set; }

        [JsonProperty("AttDetSet")]
        public Set AttDetSet { get; set; }

        [JsonProperty("returnSet")]
        public Set ReturnSet { get; set; }
    }

    public partial class Set
    {
        [JsonProperty("results")]
        public OutletSetResult[] Results { get; set; }
    }

    public partial class OutletSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("AOutletCompAddr", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletCompAddr { get; set; }

        [JsonProperty("AOutletNewMainOutnumTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNewMainOutnumTb { get; set; }

        [JsonProperty("AOutletExpdtTb")]
        public object AOutletExpdtTb { get; set; }

        [JsonProperty("AOutletFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletFlag { get; set; }

        [JsonProperty("AOutletHouseNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletHouseNoTb { get; set; }

        [JsonProperty("AOutletExpdtHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletExpdtHTb { get; set; }

        [JsonProperty("AOutletMobileNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletMobileNoTb { get; set; }

        [JsonProperty("AOutletExpdtCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletExpdtCTb { get; set; }

        [JsonProperty("AOutletNm6Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm6Tb { get; set; }

        [JsonProperty("AOutletNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNoTb { get; set; }

        [JsonProperty("AOutletTitleTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletTitleTb { get; set; }

        [JsonProperty("OutletInPrcFg", NullValueHandling = NullValueHandling.Ignore)]
        public string OutletInPrcFg { get; set; }

        [JsonProperty("AOutletBuildingNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletBuildingNoTb { get; set; }

        [JsonProperty("AOutletComcdFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletComcdFlag { get; set; }

        [JsonProperty("AOutletEmailTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletEmailTb { get; set; }

        [JsonProperty("AOutletNameTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNameTb { get; set; }

        [JsonProperty("AOutletNm5Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm5Tb { get; set; }

        [JsonProperty("AOutletNm7Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm7Tb { get; set; }

        [JsonProperty("AOutletMainFlagTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletMainFlagTb { get; set; }

        [JsonProperty("AOutletPoBoxTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletPoBoxTb { get; set; }

        [JsonProperty("AOutletStreet1Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletStreet1Tb { get; set; }

        [JsonProperty("AOutletToDeregTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletToDeregTb { get; set; }

        [JsonProperty("AOutletDregOptTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletDregOptTb { get; set; }

        [JsonProperty("AOutletStreet2Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletStreet2Tb { get; set; }

        [JsonProperty("AOutletEffDtTb")]
        public object AOutletEffDtTb { get; set; }

        [JsonProperty("AOutletProvinceTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletProvinceTb { get; set; }

        [JsonProperty("AOutletCityTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletCityTb { get; set; }

        [JsonProperty("AOutletEffDtHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletEffDtHTb { get; set; }

        [JsonProperty("AOutletEffDtCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletEffDtCTb { get; set; }

        [JsonProperty("AOutletQuarterTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletQuarterTb { get; set; }

        [JsonProperty("AOutletCountryTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletCountryTb { get; set; }

        [JsonProperty("AOutletTransTinTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletTransTinTb { get; set; }

        [JsonProperty("AOutletIdTypeTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletIdTypeTb { get; set; }

        [JsonProperty("AOutletPostalCodeTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletPostalCodeTb { get; set; }

        [JsonProperty("AOutletIdentificationNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletIdentificationNoTb { get; set; }

        [JsonProperty("AOutletIdNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletIdNoTb { get; set; }

        [JsonProperty("AOutletNm1Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm1Tb { get; set; }

        [JsonProperty("AOutletNm2Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm2Tb { get; set; }

        [JsonProperty("AOutletNm3Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm3Tb { get; set; }

        [JsonProperty("AOutletNm4Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletNm4Tb { get; set; }

        [JsonProperty("AOutletDobTb")]
        public object AOutletDobTb { get; set; }

        [JsonProperty("AOutletDobHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletDobHTb { get; set; }

        [JsonProperty("AOutletDobCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string AOutletDobCTb { get; set; }

        [JsonProperty("APermitNewMainNoIdTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNewMainNoIdTb { get; set; }

        [JsonProperty("APermitAdrFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitAdrFlag { get; set; }

        [JsonProperty("APermitCbFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitCbFlag { get; set; }

        [JsonProperty("APermitConFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitConFlag { get; set; }

        [JsonProperty("APermitExpdtTb")]
        public object APermitExpdtTb { get; set; }

        [JsonProperty("APermitGovFlag", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitGovFlag { get; set; }

        [JsonProperty("APermitMainActFlagTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitMainActFlagTb { get; set; }

        [JsonProperty("APermitMainnoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitMainnoTb { get; set; }

        [JsonProperty("APermitNm6Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm6Tb { get; set; }

        [JsonProperty("APermitNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNoTb { get; set; }

        [JsonProperty("APermitTypTxt", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitTypTxt { get; set; }

        [JsonProperty("PermitInPrcFg", NullValueHandling = NullValueHandling.Ignore)]
        public string PermitInPrcFg { get; set; }

        [JsonProperty("APermitExpdtHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitExpdtHTb { get; set; }

        [JsonProperty("APermitMainActNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitMainActNoTb { get; set; }

        [JsonProperty("APermitOutletnoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitOutletnoTb { get; set; }

        [JsonProperty("APermitExpdtCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitExpdtCTb { get; set; }

        [JsonProperty("APermitTitleTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitTitleTb { get; set; }

        [JsonProperty("APermitNm5Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm5Tb { get; set; }

        [JsonProperty("APermitNm7Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm7Tb { get; set; }

        [JsonProperty("APermitTypeTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitTypeTb { get; set; }

        [JsonProperty("APermitValfrDtTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitValfrDtTb { get; set; }

        [JsonProperty("APermitValfrDtHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitValfrDtHTb { get; set; }

        [JsonProperty("APermitValfrDtCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitValfrDtCTb { get; set; }

        [JsonProperty("APermitEffDtTb")]
        public object APermitEffDtTb { get; set; }

        [JsonProperty("APermitEffDtHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitEffDtHTb { get; set; }

        [JsonProperty("APermitEffDtCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitEffDtCTb { get; set; }

        [JsonProperty("APermitDregRsnTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitDregRsnTb { get; set; }

        [JsonProperty("APermitTransTinTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitTransTinTb { get; set; }

        [JsonProperty("APermitIdTypeTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitIdTypeTb { get; set; }

        [JsonProperty("APermitIdNoTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitIdNoTb { get; set; }

        [JsonProperty("APermitNm1Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm1Tb { get; set; }

        [JsonProperty("APermitNm2Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm2Tb { get; set; }

        [JsonProperty("APermitNm3Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm3Tb { get; set; }

        [JsonProperty("APermitNm4Tb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitNm4Tb { get; set; }

        [JsonProperty("APermitDobTb")]
        public object APermitDobTb { get; set; }

        [JsonProperty("APermitDobHTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitDobHTb { get; set; }

        [JsonProperty("APermitDobCTb", NullValueHandling = NullValueHandling.Ignore)]
        public string APermitDobCTb { get; set; }
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
