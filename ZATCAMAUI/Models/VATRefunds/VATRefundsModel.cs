using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using System.ComponentModel;
using ZATCAMAUI.Core.Mangers;
using Foundation;

namespace ZATCAMAUI.Models.VATRefunds
{
    [Preserve(AllMembers = true)]
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

    public class VATRefundsBankDetailsModel
    {
        public string BankName { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string IBAN { get; set; }
        public string Icon { get; set; }
    }

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

    [Preserve(AllMembers = true)]
    public partial class VatRefundsListResultModel
    {
        //[JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("taxType")]
        public string TaxType { get; set; }

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

        //[JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        // [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("userType")]
        public string UserTyp { get; set; }

        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }

        [JsonProperty("formProcess")]
        public string Formproc { get; set; }

        //[JsonProperty("OfficerT")]
        public string OfficerT { get; set; }

        [JsonProperty("firstName")]
        public string NameFirst { get; set; }

        [JsonProperty("lastName")]
        public string NameLast { get; set; }

        [JsonProperty("city")]
        public string City1 { get; set; }

        [JsonProperty("periodKey")]
        public string Persl { get; set; }

        // [JsonProperty("Txt50")]
        public string Txt50 { get; set; }

        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }

        [JsonProperty("formBundleTypeDescription")]
        public string FbtText { get; set; }

        [JsonProperty("calendarType")]
        public string CalendarTyp { get; set; }

        [JsonProperty("systemStatusDescription")]
        public string StatusTxt { get; set; }

        [JsonProperty("edit")]
        public string EditFg { get; set; }

        [JsonProperty("warehouseNumber")]
        public string Whno { get; set; }

        [JsonProperty("warehouseName")]
        public string Whfnm { get; set; }

        [JsonProperty("inboundCorrespondenceType")]
        public string Incotyp { get; set; }

        [JsonProperty("inboundCorrespondenceDescription")]
        public string Incotext { get; set; }

        //[JsonProperty("TaxPeriod")]
        public string TaxPeriod { get; set; }

        // [JsonProperty("Abrzu")]
        public object Abrzu { get; set; }

        //[JsonProperty("Abrzo")]
        public object Abrzo { get; set; }

        [JsonProperty("sadadBillNumber1")]
        public string SadadDoc1 { get; set; }

        [JsonProperty("sadadBillNumber2")]
        public string SadadDoc2 { get; set; }

        [JsonProperty("contractNumber")]
        public string Vtref { get; set; }

        // [JsonProperty("DueDt")]
        public object DueDt { get; set; }

        // [JsonProperty("status")]
        public string Stat { get; set; }

        [JsonProperty("serialNumber")]
        public string Euser { get; set; }

        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }

        [JsonProperty("status")]
        public string Statflag { get; set; }

        //[JsonProperty("DueDtC")]
        public string DueDtC { get; set; }

        [JsonProperty("dueAmount")]
        public string Due { get; set; }

        [JsonProperty("sortPeriod")]
        public string Sortperiod { get; set; }

        [JsonProperty("inboundChannel")]
        public string InChannel { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("statusList")]
        public StatusSetResult[] StatusSet { get; set; }

        [JsonProperty("workItems")]
        public WiDtlSetResult[] WiDtlSet { get; set; }

        [JsonProperty("VATRefundSubItems")]
        public VatRefSubItemsSetResult[] VatRefSubItemsSet { get; set; }

        [JsonProperty("VATRefundDetails")]
        public VatRefHeaderSetResult[] VatRefHeaderSet { get; set; }

        //[JsonProperty("VtfrAmtSet")]
        public VtfrAmtSet VAtRefundSET { get; set; }

        public bool IsEditable { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class StatusSet
    {
        [JsonProperty("statusList")]
        public StatusSetResult[] Results { get; set; }
    }

    public partial class StatusSetResult
    {
        //[JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("statusProfile")]
        public string Stsma { get; set; }

        [JsonProperty("userStatus")]
        public string Estat { get; set; }

        [JsonProperty("language")]
        public string Spras { get; set; }

        [JsonProperty("statusCode")]
        public string Txt04 { get; set; }

        [JsonProperty("statusDescriptiont")]
        public string Txt30 { get; set; }

        [JsonProperty("isLongText")]
        public bool Ltext { get; set; }
    }

    public partial class VatRefHeaderSet
    {
        [JsonProperty("VATRefundDetails")]
        public VatRefHeaderSetResult[] Results { get; set; }
    }

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

        [JsonProperty("requestedAmount")]
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

        // [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("refundFormBundleNumber")]
        public string RefundFbnum { get; set; }

        [JsonProperty("refundStatus")]
        public string Status { get; set; }

        [JsonProperty("currency")]
        public string Waers { get; set; }

        [JsonProperty("serialNumber")]
        public long SrNo { get; set; }

        [JsonProperty("reassessAmount")]
        public string ReassessAmt { get; set; }

        [JsonProperty("offsetAmount")]
        public string OffsetAmt { get; set; }

        [JsonProperty("offsetTotal")]
        public string OffsetTot { get; set; }

        [JsonProperty("netCreditBalance")]
        public string NetCreditBal { get; set; }

        private string _formatedReqdt { get; set; }
        public string RefundReqDtString { get; set; }

        // [JsonProperty("RefundReqDt")]
        // public DateTime RefundReqDt { get; set; }

        private string _refundReqDt { get; set; }
        [JsonProperty("refundRequestDate")]
        public string RefundReqDt
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

                    RefundReqDtString = Convert.ToDateTime(value).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = RefundReqDtString.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    RefundReqDtString = date;
                }
            }
        }
    }

    public partial class VatRefSubItemsSet
    {
        [JsonProperty("VATRefundSubItems")]
        public VatRefSubItemsSetResult[] Results { get; set; }
    }

    public partial class VatRefSubItemsSetResult
    {
        // [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("refundFormBundleNumber")]
        public string RefundFbnum { get; set; }

        [JsonProperty("returnPeriod")]
        public string ReturnPeriod { get; set; }

        [JsonProperty("returnFormBundleNumber")]
        public string ReturnFbnum { get; set; }

        [JsonProperty("creditBalance")]
        public string CreditBal { get; set; }

        [JsonProperty("reassessBalalance")]
        public string ReassessBal { get; set; }

        [JsonProperty("offsetAmount")]
        public string Offsets { get; set; }

        [JsonProperty("netCreditBalance")]
        public string NetCreditBal { get; set; }

        [JsonProperty("refundStatus")]
        public string Status { get; set; }

        public string LastStatusDate { get; set; }
        public string _lastChgDt { get; set; }
        [JsonProperty("lastChangeDate")]
        public string LastChgDt
        {
            get => _lastChgDt;
            set
            {
                if (value != null)
                {
                    //string date = UtilityManager.FormatAccordingToDeviceForVAT(value.ToShortDateString());

                    LastStatusDate = Convert.ToDateTime(value).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = LastStatusDate.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    LastStatusDate = date;

                    //LastStatusDate = date;
                }
                _lastChgDt = value;
            }
        }

        [JsonProperty("currency")]
        public string Waers { get; set; }
    }

    public partial class WiDtlSet
    {
        [JsonProperty("workItems")]
        public WiDtlSetResult[] Results { get; set; }
    }

    public partial class WiDtlSetResult
    {
        //[JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }

        [JsonProperty("firstName")]
        public string NameFirst { get; set; }

        [JsonProperty("statusCode")]
        public string Status { get; set; }

        [JsonProperty("language")]
        public string Lang { get; set; }

        [JsonProperty("lastName")]
        public string NameLast { get; set; }

        [JsonProperty("userType")]
        public string UserTyp { get; set; }

        //[JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }

        [JsonProperty("description")]
        public string Txt50 { get; set; }

        [JsonProperty("formBundleTypeDescription")]
        public string FbtText { get; set; }

        [JsonProperty("formProcess")]
        public string Formproc { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("city")]
        public string City1 { get; set; }

        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusTxt { get; set; }

        [JsonProperty("edit")]
        public string EditFg { get; set; }

        [JsonProperty("serialNumber")]
        public string Euser { get; set; }

        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }

        [JsonProperty("warehouseNumber")]
        public string Whno { get; set; }

        [JsonProperty("status")]
        public string Statflag { get; set; }

        [JsonProperty("taxPeriod")]
        public string TaxPeriod { get; set; }

        // [JsonProperty("Due")]
        public string Due { get; set; }

        [JsonProperty("contractNumber")]
        public string Vtref { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        // [JsonProperty("Stat")]
        public string Stat { get; set; }
    }

    public partial class VatRefundDisplayDataResponseModel
    {
        [JsonProperty("data")]
        public VatRefundDisplayDataModel D { get; set; }
    }

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

        [JsonProperty("refundAmount")]
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

        // [JsonProperty("__metadata")]
        public MetadataDisplayModel Metadata { get; set; }

        [JsonProperty("termsAndConditions")]
        public string TcFg { get; set; }

        [JsonProperty("pendingIBANMessage")]
        public string PendingIbanMsg { get; set; }

        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }

        [JsonProperty("CR1645GoLive")]
        public string Cr1645GoliveFg { get; set; }

        [JsonProperty("accountIdType")]
        public string Idtype { get; set; }

        [JsonProperty("IBANCheckBox")]
        public string IbanCb { get; set; }

        [JsonProperty("periodkey")]
        public string Persl { get; set; }

        [JsonProperty("agree")]
        public string Agrfg { get; set; }

        [JsonProperty("accountIdNumber")]
        public string Idnum { get; set; }

        [JsonProperty("calendarType")]
        public string Caltyp { get; set; }

        [JsonProperty("confirm")]
        public string Confirmfg { get; set; }

        private string _ibanC;

        [JsonProperty("IBAN")]
        public string Iban
        {
            get => _ibanC;
            set
            {
                _ibanC = value;
                IbanC = value; // Assign the value of Iban to the IbanC property
            }
        }

        [JsonProperty("taxpayerIdType")]
        public string IdType { get; set; }

        [JsonProperty("addressNumber")]
        public string Addrnumber { get; set; }

        public string IbanC { get; set; }

        [JsonProperty("taxpayerIdNumber")]
        public string Idnumber { get; set; }

        [JsonProperty("authorizationGroup")]
        public string Branchx { get; set; }

        [JsonProperty("buildingCode")]
        public string BuildingNo { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("currency")]
        public string Curr { get; set; }

        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("declarationDate")]
        public string Decdt { get; set; }

        [JsonProperty("declaration")]
        public string Decflg { get; set; }

        [JsonProperty("serialNumber")]
        public string Euser { get; set; }

        [JsonProperty("Evstatus")]
        public string Evstatus { get; set; }

        private string _fbnum;

        [JsonProperty("formBundleNumber")]
        public string Fbnum
        {
            get => _fbnum;
            set
            {
                _fbnum = value;
                Fbnumx = value;
            }
        }

        // [JsonProperty("Fbnumx")]
        public string Fbnumx { get; set; }

        [JsonProperty("Fbstax")]
        public string Fbstax { get; set; }

        [JsonProperty("formBundleStatus")]
        public string Fbustx { get; set; }

        [JsonProperty("contractNumber")]
        public string Fin { get; set; }

        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }

        [JsonProperty("formProcess")]
        public string Formprocx { get; set; }

        [JsonProperty("forward")]
        public string Forwardx { get; set; }

        [JsonProperty("TIN")]
        public string Gpartx { get; set; }

        [JsonProperty("language")]
        public string Langx { get; set; }

        [JsonProperty("systemCode")]
        public string Mandt { get; set; }

        //[JsonProperty("Mandtx")]
        public string Mandtx { get; set; }

        //[JsonProperty("Officerx")]
        public string Officerx { get; set; }

        [JsonProperty("operation")]
        public string Operationx { get; set; }

        [JsonProperty("portalUser")]
        public string PortalUsrx { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCd { get; set; }

        [JsonProperty("quarter")]
        public string Quarter { get; set; }

        [JsonProperty("refundType")]
        public string RefundTp { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }

        [JsonProperty("returnId")]
        public string ReturnIdx { get; set; }

        [JsonProperty("sourceIdentifier")]
        public string Srcidentifyx { get; set; }

        [JsonProperty("statusCode")]
        public string Statusx { get; set; }

        [JsonProperty("stepNumber")]
        public string StepNumberx { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }

        [JsonProperty("TINName")]
        public string TinName { get; set; }

        [JsonProperty("transactionType")]
        public string TxnTpx { get; set; }

        [JsonProperty("userType")]
        public string UserTypx { get; set; }

        [JsonProperty("NotesSet")]
        public Set NotesSet { get; set; }

        [JsonProperty("attachments")]
        public Result[] AttdetSet { get; set; }

        [JsonProperty("banks")]
        public Result[] BankDtlSet { get; set; }

        [JsonProperty("VATAmounts")]
        public VatReffundAmtDetails[] VAtRefundSET { get; set; }
    }
    
    [Preserve(AllMembers = true)]
    public partial class Set
    {
        [JsonProperty("banks")]
        public Result[] Results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public partial class Result
    {
        //[JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }

        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }

        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }

        [JsonProperty("TIN")]
        public string Tin { get; set; }

        [JsonProperty("contractNumber")]
        public string Fin { get; set; }

        [JsonProperty("documentNumber")]
        public string Opbel { get; set; }

        [JsonProperty("itemNumber")]
        public string Opupk { get; set; }

        [JsonProperty("contractAccount")]
        public string Vkont { get; set; }

        [JsonProperty("mainTransaction")]
        public string Hvorg { get; set; }

        [JsonProperty("subTransaction")]
        public string Tvorg { get; set; }

        [JsonProperty("amount")]
        public string Betrh { get; set; }

        [JsonProperty("currency")]
        public string Waers { get; set; }
    }

    public partial class MetadataDisplayModel
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class VarRefundIbanDataResponseModel
    {
        [JsonProperty("d")]
        public VarRefundIbanDataModel D { get; set; }
    }

#pragma MARK CR4914

    public class VtfrAmtSet
    {
        [JsonProperty("VATAmounts")]
        public VatReffundAmtDetails[] results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class VatReffundAmtDetails
    {
        /*ZDP_VAT_NW_RF_SRV*/
        //[JsonProperty("__metadata")]
        public VarRefundIbanDataModelMetadata Metadata { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }

        [JsonProperty("periodKey")]
        public string Persl { get; set; }

        [JsonProperty("periodDescription")]
        public string Perslt { get; set; }

        [JsonProperty("amount")]
        public string Betrw { get; set; }

        [JsonIgnore]
        private string _checkFg { get; set; } = string.Empty;

        [JsonProperty("amountCheck")]
        public string CheckFg
        {

            get => _checkFg;


            set
            {
                if (value.Equals("X"))
                {
                    IsItemSelected = true;
                }
                else
                {
                    IsItemSelected = false;
                }
                _checkFg = value;
            }

        }
        [JsonIgnore]
        public bool IsItemSelected { get; set; }


    }

    [Preserve(AllMembers = true)]
    public partial class VarRefundIbanDataModel
    {
        [JsonProperty("__metadata")]
        public VarRefundIbanDataModelMetadata Metadata { get; set; }

        // [JsonProperty("Mandtz")]
        public string Mandtz { get; set; }

        [JsonProperty("formBundleType")]
        public string Fbtypz { get; set; }

        //[JsonProperty("Fbustz")]
        public string Fbustz { get; set; }

        //[JsonProperty("UserTypz")]
        public string UserTypz { get; set; }

        //[JsonProperty("TransactionTypez")]
        public string TransactionTypez { get; set; }

        [JsonProperty("edit")]
        public string EditFgz { get; set; }

        //[JsonProperty("Mandt")]
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

        // [JsonProperty("Officer")]
        public string Officer { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        [JsonProperty("statusCode")]
        public string Status { get; set; }

        [JsonProperty("userType")]
        public string UserTyp { get; set; }

        //[JsonProperty("TxnTp")]
        public string TxnTp { get; set; }

        [JsonProperty("formProcess")]
        public string Formproc { get; set; }

        // [JsonProperty("OfficerT")]
        public string OfficerT { get; set; }

        [JsonProperty("sourceApplication")]
        public string SrcApp { get; set; }

        [JsonProperty("periodKey")]
        public string Periodkey { get; set; }

        [JsonProperty("destinationCheck")]
        public string DestCheck { get; set; }

        [JsonProperty("buttons")]
        public VarRefundIbanDataModelMetadataResult[] VrUiBtnSet { get; set; }

        [JsonProperty("IBANs")]
        public VarRefundIbanDataModelMetadataResult[] IbanSet { get; set; }
    }

    public partial class NSet
    {
        [JsonProperty("IBANs")]
        public VarRefundIbanDataModelMetadataResult[] Results { get; set; }
    }

    public partial class VarRefundIbanDataModelMetadataResult
    {
        //[JsonProperty("__metadata")]
        public VarRefundIbanDataModelMetadata VarRefundIbanDataModelMetadata { get; set; }

        [JsonProperty("IBAN")]
        public string Iban { get; set; }
    }

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

        [JsonProperty("VtfrAmtSet")]
        public VtfrAmtSet VAtRefundSET { get; set; }
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
