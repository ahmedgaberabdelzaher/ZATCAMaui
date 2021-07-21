using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using GAZT.Manager;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.VATRefunds
{ [Preserve(AllMembers = true)]
    public class VATRefundsModel
    {
        public VATRefundsModel()
        {

        }

        public string Title { get; set; }
        public string RefernceNumber { get; set; }
        public string Status { get; set; }
        public string RequestedAmount { get; set; }
        public string ReassesmentAmount { get; set; }
        public string TotalOffset { get; set; }
        public string NetCreditBalance { get; set; }
        public string RequestDate { get; set; }
        public string TotalAmount { get; set; }
        public bool IsNewRequest { get; set; }

        public VATRefundsBankDetailsModel BankDetails { get; set; }
        public ObservableCollection<VATRefundsReturnsModel> VATReturns { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATRefundsBankDetailsModel
    {
        public string BankName { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string IBAN { get; set; }
        public string Icon { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATRefundsReturnsModel
    {
        public string ReturnPeriod { get; set; }
        public string CreditBalance { get; set; }
        public string ReassessedBalance { get; set; }
        public string Offset { get; set; }
        public string NetCreditBalance { get; set; }
        public string Status { get; set; }
        public string LastStatusChange { get; set; }
    }

    //public partial class VatRefundsListResultModel
    //{
    //    [JsonProperty("d")]
    //    public D D { get; set; }
    //}
    [Preserve(AllMembers = true)]
    public partial class VatRefundsListResultModel
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("Mandt")]
        public string Mandt { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("PortalUsr")]
        public string PortalUsr { get; set; }

        [JsonProperty("Lang")]
        public string Lang { get; set; }

        [JsonProperty("Operation")]
        public string Operation { get; set; }

        [JsonProperty("StepNumber")]
        public string StepNumber { get; set; }

        [JsonProperty("ReturnId")]
        public string ReturnId { get; set; }

        [JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("UserTyp")]
        public string UserTyp { get; set; }

        [JsonProperty("TxnTp")]
        public string TxnTp { get; set; }

        [JsonProperty("Formproc")]
        public string Formproc { get; set; }

        [JsonProperty("OfficerT")]
        public string OfficerT { get; set; }

        [JsonProperty("NameFirst")]
        public string NameFirst { get; set; }

        [JsonProperty("NameLast")]
        public string NameLast { get; set; }

        [JsonProperty("City1")]
        public string City1 { get; set; }

        [JsonProperty("Persl")]
        public string Persl { get; set; }

        [JsonProperty("Txt50")]
        public string Txt50 { get; set; }

        [JsonProperty("Fbtyp")]
        public string Fbtyp { get; set; }

        [JsonProperty("FbtText")]
        public string FbtText { get; set; }

        [JsonProperty("CalendarTyp")]
        public string CalendarTyp { get; set; }

        [JsonProperty("StatusTxt")]
        public string StatusTxt { get; set; }

        [JsonProperty("EditFg")]
        public string EditFg { get; set; }

        [JsonProperty("Whno")]
        public string Whno { get; set; }

        [JsonProperty("Whfnm")]
        public string Whfnm { get; set; }

        [JsonProperty("Incotyp")]
        public string Incotyp { get; set; }

        [JsonProperty("Incotext")]
        public string Incotext { get; set; }

        [JsonProperty("TaxPeriod")]
        public string TaxPeriod { get; set; }

        [JsonProperty("Abrzu")]
        public object Abrzu { get; set; }

        [JsonProperty("Abrzo")]
        public object Abrzo { get; set; }

        [JsonProperty("SadadDoc1")]
        public string SadadDoc1 { get; set; }

        [JsonProperty("SadadDoc2")]
        public string SadadDoc2 { get; set; }

        [JsonProperty("Vtref")]
        public string Vtref { get; set; }

        [JsonProperty("DueDt")]
        public object DueDt { get; set; }

        [JsonProperty("Stat")]
        public string Stat { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("Statflag")]
        public string Statflag { get; set; }

        [JsonProperty("DueDtC")]
        public string DueDtC { get; set; }

        [JsonProperty("Due")]
        public string Due { get; set; }

        [JsonProperty("Sortperiod")]
        public string Sortperiod { get; set; }

        [JsonProperty("InChannel")]
        public string InChannel { get; set; }

        [JsonProperty("Flag")]
        public string Flag { get; set; }

        [JsonProperty("STATUSSet")]
        public StatusSet StatusSet { get; set; }

        [JsonProperty("WI_DTLSet")]
        public WiDtlSet WiDtlSet { get; set; }

        [JsonProperty("VatRef_SubItemsSet")]
        public VatRefSubItemsSet VatRefSubItemsSet { get; set; }

        [JsonProperty("VatRef_HeaderSet")]
        public VatRefHeaderSet VatRefHeaderSet { get; set; }

        public bool IsEditable { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class Metadata
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class StatusSet
    {
        [JsonProperty("results")]
        public StatusSetResult[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class StatusSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Stsma")]
        public string Stsma { get; set; }

        [JsonProperty("Estat")]
        public string Estat { get; set; }

        [JsonProperty("Spras")]
        public string Spras { get; set; }

        [JsonProperty("Txt04")]
        public string Txt04 { get; set; }

        [JsonProperty("Txt30")]
        public string Txt30 { get; set; }

        [JsonProperty("Ltext")]
        public bool Ltext { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefHeaderSet
    {
        [JsonProperty("results")]
        public VatRefHeaderSetResult[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefHeaderSetResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _requestedAmt { get; set; }

        [JsonProperty("RequestedAmt")]
        public string RequestedAmt
        {
            get
            {
                return _requestedAmt;
            }
            set
            {
                _requestedAmt = value;
                _requestedAmt = _requestedAmt.Replace("-", string.Empty);
                OnPropertyRaised("RequestedAmt");
            }
        }

        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("RefundFbnum")]
        public string RefundFbnum { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Waers")]
        public string Waers { get; set; }

        [JsonProperty("SrNo")]
        public long SrNo { get; set; }

        [JsonProperty("ReassessAmt")]
        public string ReassessAmt { get; set; }

        [JsonProperty("OffsetAmt")]
        public string OffsetAmt { get; set; }

        [JsonProperty("OffsetTot")]
        public string OffsetTot { get; set; }

        [JsonProperty("NetCreditBal")]
        public string NetCreditBal { get; set; }

        private string _formatedReqdt { get; set; }
        public string RefundReqDtString { get; set; }

        //  [JsonProperty("RefundReqDt")]
        //  public DateTime RefundReqDt { get; set; }

        private DateTime _refundReqDt { get; set; }
        [JsonProperty("RefundReqDt")]
        public DateTime RefundReqDt
        {
            get
            {
                return _refundReqDt;
            }
            set
            {
                _refundReqDt = value;
                if (_refundReqDt != null)
                {
                    //string date = UtilityManager.FormatAccordingToDeviceForVAT(value.ToShortDateString());

                    RefundReqDtString = value.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = RefundReqDtString.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    RefundReqDtString = date;
                }
            }
        }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefSubItemsSet
    {
        [JsonProperty("results")]
        public VatRefSubItemsSetResult[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefSubItemsSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("RefundFbnum")]
        public string RefundFbnum { get; set; }

        [JsonProperty("ReturnPeriod")]
        public string ReturnPeriod { get; set; }

        [JsonProperty("ReturnFbnum")]
        public string ReturnFbnum { get; set; }

        [JsonProperty("CreditBal")]
        public string CreditBal { get; set; }

        [JsonProperty("ReassessBal")]
        public string ReassessBal { get; set; }

        [JsonProperty("Offsets")]
        public string Offsets { get; set; }

        [JsonProperty("NetCreditBal")]
        public string NetCreditBal { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        public string LastStatusDate { get; set; }
        public DateTime _lastChgDt { get; set; }
        [JsonProperty("LastChgDt")]
        public DateTime LastChgDt
        {
            get => _lastChgDt;
            set
            {
                if (value != null)
                {
                    //string date = UtilityManager.FormatAccordingToDeviceForVAT(value.ToShortDateString());

                    LastStatusDate = value.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = LastStatusDate.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    LastStatusDate = date;

                    //LastStatusDate = date;
                }
                _lastChgDt = value;
            }
        }

        [JsonProperty("Waers")]
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class WiDtlSet
    {
        [JsonProperty("results")]
        public WiDtlSetResult[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class WiDtlSetResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("NameFirst")]
        public string NameFirst { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Lang")]
        public string Lang { get; set; }

        [JsonProperty("NameLast")]
        public string NameLast { get; set; }

        [JsonProperty("UserTyp")]
        public string UserTyp { get; set; }

        [JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("TxnTp")]
        public string TxnTp { get; set; }

        [JsonProperty("Txt50")]
        public string Txt50 { get; set; }

        [JsonProperty("FbtText")]
        public string FbtText { get; set; }

        [JsonProperty("Formproc")]
        public string Formproc { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("City1")]
        public string City1 { get; set; }

        [JsonProperty("Fbtyp")]
        public string Fbtyp { get; set; }

        [JsonProperty("StatusTxt")]
        public string StatusTxt { get; set; }

        [JsonProperty("EditFg")]
        public string EditFg { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("Whno")]
        public string Whno { get; set; }

        [JsonProperty("Statflag")]
        public string Statflag { get; set; }

        [JsonProperty("TaxPeriod")]
        public string TaxPeriod { get; set; }

        [JsonProperty("Due")]
        public string Due { get; set; }

        [JsonProperty("Vtref")]
        public string Vtref { get; set; }

        [JsonProperty("Flag")]
        public string Flag { get; set; }

        [JsonProperty("Stat")]
        public string Stat { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefundDisplayDataResponseModel
    {
        [JsonProperty("d")]
        public VatRefundDisplayDataModel D { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefundDisplayDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _rfamt { get; set; }

        [JsonProperty("Rfamt")]
        public string Rfamt
        {
            get
            {
                return _rfamt;
            }
            set
            {
                _rfamt = value;
                OnPropertyRaised("Rfamt");
            }
        }

        [JsonProperty("__metadata")]
        public MetadataDisplayModel Metadata { get; set; }

        [JsonProperty("TcFg")]
        public string TcFg { get; set; }

        [JsonProperty("AdditionalNo")]
        public string AdditionalNo { get; set; }

        [JsonProperty("Cr1645GoliveFg")]
        public string Cr1645GoliveFg { get; set; }

        [JsonProperty("Idtype")]
        public string Idtype { get; set; }

        [JsonProperty("IbanCb")]
        public string IbanCb { get; set; }

        [JsonProperty("Persl")]
        public string Persl { get; set; }

        [JsonProperty("Agrfg")]
        public string Agrfg { get; set; }

        [JsonProperty("Idnum")]
        public string Idnum { get; set; }

        [JsonProperty("Caltyp")]
        public string Caltyp { get; set; }

        [JsonProperty("Confirmfg")]
        public string Confirmfg { get; set; }

        [JsonProperty("Iban")]
        public string Iban { get; set; }

        [JsonProperty("IdType")]
        public string IdType { get; set; }

        [JsonProperty("Addrnumber")]
        public string Addrnumber { get; set; }

        [JsonProperty("IbanC")]
        public string IbanC { get; set; }

        [JsonProperty("Idnumber")]
        public string Idnumber { get; set; }

        [JsonProperty("Branchx")]
        public string Branchx { get; set; }

        [JsonProperty("BuildingNo")]
        public string BuildingNo { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Curr")]
        public string Curr { get; set; }

        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("Decdt")]
        public string Decdt { get; set; }

        [JsonProperty("Decflg")]
        public string Decflg { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Evstatus")]
        public string Evstatus { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Fbnumx")]
        public string Fbnumx { get; set; }

        [JsonProperty("Fbstax")]
        public string Fbstax { get; set; }

        [JsonProperty("Fbustx")]
        public string Fbustx { get; set; }

        [JsonProperty("Fin")]
        public string Fin { get; set; }

        [JsonProperty("FormGuid")]
        public string FormGuid { get; set; }

        [JsonProperty("Formprocx")]
        public string Formprocx { get; set; }

        [JsonProperty("Forwardx")]
        public string Forwardx { get; set; }

        [JsonProperty("Gpartx")]
        public string Gpartx { get; set; }

        [JsonProperty("Langx")]
        public string Langx { get; set; }

        [JsonProperty("Mandt")]
        public string Mandt { get; set; }

        [JsonProperty("Mandtx")]
        public string Mandtx { get; set; }

        [JsonProperty("Officerx")]
        public string Officerx { get; set; }

        [JsonProperty("Operationx")]
        public string Operationx { get; set; }

        [JsonProperty("PortalUsrx")]
        public string PortalUsrx { get; set; }

        [JsonProperty("PostalCd")]
        public string PostalCd { get; set; }

        [JsonProperty("Quarter")]
        public string Quarter { get; set; }

        [JsonProperty("RefundTp")]
        public string RefundTp { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("RegionDesc")]
        public string RegionDesc { get; set; }

        [JsonProperty("ReturnIdx")]
        public string ReturnIdx { get; set; }

        [JsonProperty("Srcidentifyx")]
        public string Srcidentifyx { get; set; }

        [JsonProperty("Statusx")]
        public string Statusx { get; set; }

        [JsonProperty("StepNumberx")]
        public string StepNumberx { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }

        [JsonProperty("TinName")]
        public string TinName { get; set; }

        [JsonProperty("TxnTpx")]
        public string TxnTpx { get; set; }

        [JsonProperty("UserTypx")]
        public string UserTypx { get; set; }

        [JsonProperty("NotesSet")]
        public Set NotesSet { get; set; }

        [JsonProperty("AttdetSet")]
        public Set AttdetSet { get; set; }

        [JsonProperty("BankDtlSet")]
        public Set BankDtlSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class Set
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class Result
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("FormGuid")]
        public string FormGuid { get; set; }

        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("LineNo")]
        public int LineNo { get; set; }

        [JsonProperty("RankingOrder")]
        public string RankingOrder { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }

        [JsonProperty("Fin")]
        public string Fin { get; set; }

        [JsonProperty("Opbel")]
        public string Opbel { get; set; }

        [JsonProperty("Opupk")]
        public string Opupk { get; set; }

        [JsonProperty("Vkont")]
        public string Vkont { get; set; }

        [JsonProperty("Hvorg")]
        public string Hvorg { get; set; }

        [JsonProperty("Tvorg")]
        public string Tvorg { get; set; }

        [JsonProperty("Betrh")]
        public string Betrh { get; set; }

        [JsonProperty("Waers")]
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class MetadataDisplayModel
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VarRefundIbanDataResponseModel
    {
        [JsonProperty("d")]
        public VarRefundIbanDataModel D { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VarRefundIbanDataModel
    {
        [JsonProperty("__metadata")]
        public VarRefundIbanDataModelMetadata Metadata { get; set; }

        [JsonProperty("Mandtz")]
        public string Mandtz { get; set; }

        [JsonProperty("Fbtypz")]
        public string Fbtypz { get; set; }

        [JsonProperty("Fbustz")]
        public string Fbustz { get; set; }

        [JsonProperty("UserTypz")]
        public string UserTypz { get; set; }

        [JsonProperty("TransactionTypez")]
        public string TransactionTypez { get; set; }

        [JsonProperty("EditFgz")]
        public string EditFgz { get; set; }

        [JsonProperty("Mandt")]
        public string Mandt { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("PortalUsr")]
        public string PortalUsr { get; set; }

        [JsonProperty("Lang")]
        public string Lang { get; set; }

        [JsonProperty("Operation")]
        public string Operation { get; set; }

        [JsonProperty("StepNumber")]
        public string StepNumber { get; set; }

        [JsonProperty("ReturnId")]
        public string ReturnId { get; set; }

        [JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("UserTyp")]
        public string UserTyp { get; set; }

        [JsonProperty("TxnTp")]
        public string TxnTp { get; set; }

        [JsonProperty("Formproc")]
        public string Formproc { get; set; }

        [JsonProperty("OfficerT")]
        public string OfficerT { get; set; }

        [JsonProperty("SrcApp")]
        public string SrcApp { get; set; }

        [JsonProperty("Periodkey")]
        public string Periodkey { get; set; }

        [JsonProperty("DestCheck")]
        public string DestCheck { get; set; }

        [JsonProperty("VR_UI_BTNSet")]
        public NSet VrUiBtnSet { get; set; }

        [JsonProperty("IBANSet")]
        public NSet IbanSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class NSet
    {
        [JsonProperty("results")]
        public VarRefundIbanDataModelMetadataResult[] Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VarRefundIbanDataModelMetadataResult
    {
        [JsonProperty("__metadata")]
        public VarRefundIbanDataModelMetadata VarRefundIbanDataModelMetadata { get; set; }

        [JsonProperty("Iban")]
        public string Iban { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VarRefundIbanDataModelMetadata
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class VatRefundSaveDataModel
    {
        [JsonProperty("__metadata")]
        public MetadataVatRefundSaveDataModel Metadata { get; set; }

        [JsonProperty("TcFg")]
        public string TcFg { get; set; }

        [JsonProperty("AdditionalNo")]
        public string AdditionalNo { get; set; }

        [JsonProperty("Idtype")]
        public string Idtype { get; set; }

        [JsonProperty("IbanCb")]
        public long IbanCb { get; set; }

        [JsonProperty("Persl")]
        public string Persl { get; set; }

        [JsonProperty("Agrfg")]
        public string Agrfg { get; set; }

        [JsonProperty("Idnum")]
        public string Idnum { get; set; }

        [JsonProperty("Caltyp")]
        public string Caltyp { get; set; }

        [JsonProperty("Confirmfg")]
        public string Confirmfg { get; set; }

        [JsonProperty("Iban")]
        public string Iban { get; set; }

        [JsonProperty("IdType")]
        public string IdType { get; set; }

        [JsonProperty("Addrnumber")]
        public string Addrnumber { get; set; }

        [JsonProperty("IbanC")]
        public string IbanC { get; set; }

        [JsonProperty("Idnumber")]
        public string Idnumber { get; set; }

        [JsonProperty("Branchx")]
        public string Branchx { get; set; }

        [JsonProperty("BuildingNo")]
        public string BuildingNo { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Curr")]
        public string Curr { get; set; }

        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("Decdt")]
        public object Decdt { get; set; }

        [JsonProperty("Decflg")]
        public string Decflg { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Evstatus")]
        public string Evstatus { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Fbnumx")]
        public string Fbnumx { get; set; }

        [JsonProperty("Fbstax")]
        public string Fbstax { get; set; }

        [JsonProperty("Fbustx")]
        public string Fbustx { get; set; }

        [JsonProperty("Fin")]
        public string Fin { get; set; }

        [JsonProperty("FormGuid")]
        public string FormGuid { get; set; }

        [JsonProperty("Formprocx")]
        public string Formprocx { get; set; }

        [JsonProperty("Forwardx")]
        public string Forwardx { get; set; }

        [JsonProperty("Gpartx")]
        public string Gpartx { get; set; }

        [JsonProperty("Langx")]
        public string Langx { get; set; }

        [JsonProperty("Mandt")]
        public string Mandt { get; set; }

        [JsonProperty("Mandtx")]
        public string Mandtx { get; set; }

        [JsonProperty("Officerx")]
        public string Officerx { get; set; }

        [JsonProperty("Operationx")]
        public string Operationx { get; set; }

        [JsonProperty("PortalUsrx")]
        public string PortalUsrx { get; set; }

        [JsonProperty("PostalCd")]
        public string PostalCd { get; set; }

        [JsonProperty("Quarter")]
        public string Quarter { get; set; }

        [JsonProperty("RefundTp")]
        public string RefundTp { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("RegionDesc")]
        public string RegionDesc { get; set; }

        [JsonProperty("ReturnIdx")]
        public string ReturnIdx { get; set; }

        [JsonProperty("Rfamt")]
        public string Rfamt { get; set; }

        [JsonProperty("Srcidentifyx")]
        public string Srcidentifyx { get; set; }

        [JsonProperty("Statusx")]
        public string Statusx { get; set; }

        [JsonProperty("StepNumberx")]
        public string StepNumberx { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }

        [JsonProperty("TinName")]
        public string TinName { get; set; }

        [JsonProperty("TxnTpx")]
        public string TxnTpx { get; set; }

        [JsonProperty("UserTypx")]
        public string UserTypx { get; set; }

        [JsonProperty("NotesSet")]
        public Array[] NotesSet { get; set; }

        [JsonProperty("AttdetSet")]
        public Array[] AttdetSet { get; set; }

        [JsonProperty("BankDtlSet")]
        public Array[] BankDtlSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class MetadataVatRefundSaveDataModel
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }



}
