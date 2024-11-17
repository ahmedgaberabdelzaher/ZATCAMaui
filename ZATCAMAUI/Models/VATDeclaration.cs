using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Mangers;
using Font = Microsoft.Maui.Font;

namespace ZATCAMAUI.Models
{

    [Serializable]
   
    [DataContract]
    public class VATDeclarationsMetadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    //[Serializable]

    //[DataContract]
    public class Metadata2
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }
    public class Note
    {
        // public Metadata2 __metadata { get; set; }
        [DataMember]
        [JsonProperty("noteNumber")]
        public string Notenoz { get; set; }
        [DataMember]
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }
        [DataMember]
        [JsonProperty("displayOnAssessment")]
        public string XInvoicez { get; set; }
        [DataMember]
        [JsonProperty("completed")]
        public string XObsoletez { get; set; }
        [DataMember]
        [JsonProperty("processingReason")]
        public string Rcodez { get; set; }
        [DataMember]
        [JsonProperty("userName")]
        public string Erfusrz { get; set; }
        [DataMember]
        [JsonProperty("entryDate")]
        public string Erfdtz { get; set; }
        [DataMember]
        [JsonProperty("createdAt")]
        public string Erftmz { get; set; }
        [DataMember]
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string ByPusrz { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        [DataMember]
        [JsonProperty("name")]
        public string Namez { get; set; } //Name
        [DataMember]
        public string Noteno { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int Lineno { get; set; }
        [DataMember]
        [JsonProperty("elementNumber")]
        public int ElemNo { get; set; }
        [DataMember]
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [DataMember]
        [JsonProperty("notesLine")]
        public string Tdline { get; set; }
        [DataMember]
        [JsonProperty("section")]
        public string Sect { get; set; } //Section
        [DataMember]
        [JsonProperty("startDate")]
        public string Strdt { get; set; } //date
        [DataMember]
        [JsonProperty("startTime")]
        public string Strtime { get; set; } //time
        [DataMember]
        [JsonProperty("notesDescription")]
        public string Strline { get; set; }   //note
    }

    // [Serializable]
   
    // [DataContract]
    public class NOTESSet
    {
        // [DataMember]
        [JsonProperty("notes")]
        public List<Note> results { get; set; }
    }

    // [Serializable]
   
    //[DataContract]
    public class Metadata3
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class Result2
    {
        [DataMember]
        public Metadata3 __metadata { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Partner { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("bankDetails")]
        public string Bkvid { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class IBANSet
    {
        [DataMember]
        [JsonProperty("IBANs")]
        public List<Result2> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Metadata4
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
    public class Result3
    {
        [DataMember]
        public Metadata4 __metadata { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("creditAmount")]
        public string CreditAmt { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("documentNumber")]
        public string DocNo { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [DataMember]
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class CFSet
    {
        [DataMember]
        [JsonProperty("credits")]
        public List<Result3> results { get; set; }
    }

    // [Serializable]
   
    // [DataContract]
    public class Metadata5
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class Attachment
    {
        // public Metadata5 __metadata { get; set; }
        [DataMember]
        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }
        [DataMember]
        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }
        [DataMember]
        [JsonProperty("formGuid")]
        public string SchGuid { get; set; }
        [DataMember]
        [JsonProperty("documentCategory")]
        public string Dotyp { get; set; }
        [DataMember]
        [JsonProperty("serialNumber")]
        public int Srno { get; set; }
        [DataMember]
        [JsonProperty("documentId")]
        public string Doguid { get; set; }

        [DataMember]
        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }
        [DataMember]
        [JsonProperty("fileName")]
        public string Filename { get; set; }
        [DataMember]
        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }
        [DataMember]
        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }
        [DataMember]
        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }
        [DataMember]
        [JsonProperty("createdAt")]
        public string Erftm { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("documentURL")]
        public string DocUrl { get; set; }
        [DataMember]
        [JsonProperty("outletReference")]
        public string OutletRef { get; set; }
        [DataMember]
        [JsonProperty("enableEdit")]
        public string Enbedit { get; set; }
        [DataMember]
        [JsonProperty("enableDelete")]
        public string Enbdele { get; set; }
        [DataMember]
        [JsonProperty("visibleEdit")]
        public string Visedit { get; set; }
        [DataMember]
        [JsonProperty("visibleDelete")]
        public string Visdel { get; set; }
         [DataMember]
        public Color ColorOf { get; set; }
        public bool showDelete { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATAttachment
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [DataMember]
        public string RetGuid { get; set; }
        [DataMember]
        public string Seqno { get; set; }
        [DataMember]
        public string SchGuid { get; set; }
        [DataMember]
        public string Dotyp { get; set; }
        [DataMember]
        public int Srno { get; set; }
        [DataMember]
        public string Doguid { get; set; }
        [DataMember]
        public string AttBy { get; set; }
        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public string FileExtn { get; set; }
        [DataMember]
        public string Mimetype { get; set; }
        [DataMember]
        public string ByPusr { get; set; }
        [DataMember]
        public string Erfdt { get; set; }
        [DataMember]
        public string Erftm { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string DocUrl { get; set; }
        [DataMember]
        public string OutletRef { get; set; }
        [DataMember]
        public string Enbedit { get; set; }
        [DataMember]
        public string Enbdele { get; set; }
        [DataMember]
        public string Visedit { get; set; }
        [DataMember]
        public string Visdel { get; set; }
        [DataMember]
        public string DeleteImageSource { get; set; }

        [DataMember]
        public Color ColorOf { get; set; }
        [DataMember]
        public bool ShowDelete { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class ATTACHSet
    {
        [DataMember]
        [JsonProperty("attachments")]
        public List<Attachment> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Metadata6
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
    public class Result5
    {
        [DataMember]
        public Metadata6 __metadata { get; set; }
        [DataMember]
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
        [DataMember]
        [JsonProperty("addressType")]
        public string AddrType { get; set; }
        [DataMember]
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        [DataMember]
        [JsonProperty("startDate")]
        public object Begda { get; set; }
        [DataMember]
        [JsonProperty("endDate")]
        public object Endda { get; set; }
        [DataMember]
        [JsonProperty("addressNumber")]
        public string Addrnumber { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City { get; set; }
        [DataMember]
        [JsonProperty("quarter")]
        public string Quarter { get; set; }
        [DataMember]
        [JsonProperty("postalCode")]
        public string PostalCd { get; set; }
        [DataMember]
        [JsonProperty("street")]
        public string Street { get; set; }
        [DataMember]
        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }
        [DataMember]
        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }
        [DataMember]
        [JsonProperty("region")]
        public string Region { get; set; }
        [DataMember]
        [JsonProperty("sizeUnit")]
        public string SizUn { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class ADRSet
    {
        [DataMember]
        [JsonProperty("addresses")]
        public List<Result5> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATRMSGSet
    {
        [DataMember]
        [JsonProperty("messages")]
        public List<object> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class AttachmentRootOject
    {
        [DataMember]
        [JsonProperty("result")]
        public Attachment d { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclarationD
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        [DataMember]
        public VATDeclarationsMetadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("goLive")]
        public string GoliveFg { get; set; }
        [DataMember]
        [JsonProperty("CR3487GoLive")]
        public string Cr3487golive { get; set; }
        [DataMember]
        [JsonProperty("CR1317GoLive")]
        public string Cr1317golive { get; set; }
        [DataMember]
        [JsonProperty("CR2215GoLive")]
        public string Cr2215 { get; set; }
        [DataMember]
        [JsonProperty("governmentalSuppliesRateChange")]
        public string GovsupYesno { get; set; }

        [DataMember]
        [JsonProperty("reviewNotAvailableMessage")]
        public string ReviewNaMsg { get; set; }
        [DataMember]
        [JsonProperty("CR1645GoLive")]
        public string Cr1645GoliveFg { get; set; }
        [DataMember]

        [JsonProperty("pendingIBANMessage")]
        public string PendingIbanMsg { get; set; }
        [DataMember]
        [JsonProperty("rateChange")]
        public string Yesno { get; set; }
        [DataMember]
        [JsonProperty("termsAndConditionsStep4")]
        public string TcFlg { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string Idtype { get; set; }
        [DataMember]
        [JsonProperty("payableAmount")]
        public string Betrh { get; set; }
        [DataMember]
        [JsonProperty("importer")]
        public string ImporterFg { get; set; }
        [DataMember]
        [JsonProperty("idNumber")]
        public string Idnum { get; set; }
        [DataMember]
        [JsonProperty("groupNumber")]
        public string GrpNo { get; set; }
        [DataMember]
        [JsonProperty("submit")]
        public string SubmitFg { get; set; }
        //[DataMember]
        [JsonProperty("taxpayerIdType")]
        public string IdType { get; set; }
        [DataMember]
        [JsonProperty("idNumber2ndStep")]
        public string Idnumber { get; set; }
        [DataMember]
        [JsonProperty("totalSalesAmount")]    
        public string TotalsalesAmt { get; set; }
        [DataMember]
        [JsonProperty("estimated")]
        public string EstimatedFg { get; set; }
        [DataMember]
        [JsonProperty("totalSalesAdjustment")]
        public string TotalsalesAdj { get; set; }
        [DataMember]
        [JsonProperty("formBundleStatus")]
        public string Fbust { get; set; }
        [DataMember]
        [JsonProperty("totalPurchaseAmount")]
        public string TotalpurchaseAmt { get; set; }
        [DataMember]
        [JsonProperty("totalPurchaseAdjustment")]
        public string TotalpurchaseAdj { get; set; }
        [DataMember]
        [JsonProperty("taxpayerRegistration")]
        public string TpregFg { get; set; }
        [DataMember]
        [JsonProperty("creditVATRefund")]
        public string CreditVatRef { get; set; }
        [DataMember]
        [JsonProperty("VATPost")]
        public string VatPost { get; set; }
        [DataMember]
        [JsonProperty("IBANCheckBox")]
        public string IbanCb { get; set; }
        [DataMember]
        [JsonProperty("VATAmount")]
        public string LfpVat { get; set; }
        [DataMember]
        [JsonProperty("inboundCorrespondenceDescription")]
        public string Incotext { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandtz { get; set; }
        //[DataMember]
        //[JsonProperty("periodKey")]
        //public string Periodkeyz { get; set; }
        [DataMember]
        [JsonProperty("periodDescription")]
        public string Perslt { get; set; }
        [DataMember]
        [JsonProperty("confirmStep2")]
        public string ConfStp2 { get; set; }
        [DataMember]
        [JsonProperty("totalDueVAT")]
        public string TotaldueVat { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Langz { get; set; }
        [DataMember]
        [JsonProperty("operation")]
        public string Operationz { get; set; }
        [DataMember]
       // [JsonProperty("stepNumber")]
        public string StepNumberz { get; set; }
        [DataMember]
        [JsonProperty("returnId")]
        public string ReturnIdz { get; set; }
        [DataMember]
        [JsonProperty("officer")]
        public string Officerz { get; set; }
        //[DataMember]
        //[JsonProperty("TIN")]
        //public string Gpartz { get; set; }
        [DataMember]
        [JsonProperty("statusCode")]
        public string Statusz { get; set; }
        [DataMember]
        [JsonProperty("userType")]
        public string UserTypz { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TxnTpz { get; set; }
        [DataMember]
        [JsonProperty("formProcess")]
        public string Formprocz { get; set; }
        [DataMember]
        public string OfficerTz { get; set; }
        [DataMember]
        [JsonProperty("sourceApplication")]
        public string SrcAppz { get; set; }
        [DataMember]
        //[JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [DataMember]
        [JsonProperty("VATReturn")]
        public string Vrtaxret { get; set; }
        [DataMember]
        [JsonProperty("inboundCorrespondenceType")]
        public string Incotyp { get; set; }
        [DataMember]
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [DataMember]
        [JsonProperty("periodStartDate")]
        public string Abrzu { get; set; }
        [DataMember]
        [JsonProperty("periodEndDate")]
        public string Abrzo { get; set; }
        [DataMember]
        [JsonProperty("contractNumber")]
        public string Fin { get; set; }
        [DataMember]
        [JsonProperty("taxpayerName")]
        public string Tpnm { get; set; }
        [DataMember]
        [JsonProperty("termsAndConditionsStep1")]
        public string TcFg { get; set; }
        [DataMember]
        [JsonProperty("standardSalesAmount")]
        public string StdsalesAmt { get; set; }
        [DataMember]
        [JsonProperty("standardSalesAdjustment")]
        public string StdsalesAdj { get; set; }
        [DataMember]
        [JsonProperty("standardSalesVAT")]
        public string StdsalesVat { get; set; }
        [DataMember]
        [JsonProperty("salesGCCAmount")]
        public string SalesGccAmt { get; set; }
        [DataMember]
        [JsonProperty("salesGCCAdjustment")]
        public string SalesGccAdj { get; set; }
        [DataMember]
        [JsonProperty("zeroSalesAmount")]
        public string ZerosalesAmt { get; set; }
        [DataMember]
        [JsonProperty("zeroSalesAdjustment")]
        public string ZerosalesAdj { get; set; }
        [DataMember]
        [JsonProperty("exportsAmount")]
        public string ExportsAmt { get; set; }
        [DataMember]
        [JsonProperty("exportsAdjustment")]
        public string ExportsAdj { get; set; }
        [DataMember]
        [JsonProperty("exemptSalesAmount")]
        public string ExemptsalesAmt { get; set; }
        [DataMember]
        [JsonProperty("exemptSalesAdjustment")]
        public string ExemptsalesAdj { get; set; }
        [DataMember]
        [JsonProperty("totalSalesVAT")]
        public string TotalsalesVat { get; set; }
        [DataMember]
        [JsonProperty("standardPurchaseAmount")]
        public string StdpurchaseAmt { get; set; }
        [DataMember]
        [JsonProperty("standardPurchaseAdjustment")]
        public string StdpurchaseAdj { get; set; }
        [DataMember]
        [JsonProperty("standardPurchasesVAT")]
        public string StdpurchasesVat { get; set; }
        [DataMember]
        [JsonProperty("importsPaidAmount")]
        public string ImportspaidAmt { get; set; }
        [DataMember]
        [JsonProperty("importsPaidAdjustment")]
        public string ImportspaidAdj { get; set; }
        [DataMember]
        [JsonProperty("importsPaidVAT")]
        public string ImportspaidVat { get; set; }
        [DataMember]
        [JsonProperty("importsAccountAmount")]
        public string ImportsaccAmt { get; set; }
        [DataMember]
        [JsonProperty("importsAccountAdjustment")]
        public string ImportsaccAdj { get; set; }
        [DataMember]
        [JsonProperty("importsAccountVAT")]
        public string ImportsaccVat { get; set; }
        [DataMember]
        [JsonProperty("zeroPurchaseAmount")]
        public string ZeropurchaseAmt { get; set; }
        [DataMember]
        [JsonProperty("zeroPurchaseAdjustment")]
        public string ZeropurchaseAdj { get; set; }
        [DataMember]
        [JsonProperty("exemptPurchaseAmount")]
        public string ExemptpurchaseAmt { get; set; }
        [DataMember]
        [JsonProperty("exemptPurchaseAdjustment")]
        public string ExemptpurchaseAdj { get; set; }
        [DataMember]
        [JsonProperty("totalPurchaseVAT")]
        public string TotalpurchaseVat { get; set; }
        [DataMember]
        [JsonProperty("preperiodCorrection")]
        public string Preperiodcorr { get; set; }
        [DataMember]
        [JsonProperty("creditVAT")]
        public string CreditVat { get; set; }
        [DataMember]
        [JsonProperty("netDueVAT")]
        public string NetdueVat { get; set; }
        [DataMember]
        [JsonProperty("correctionPenalty")]
        public string CorrPen { get; set; }
        [DataMember]
        [JsonProperty("finalDueVAT")]
        public string FinaldueVat { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [DataMember]
        [JsonProperty("refund")]
        public string RefundFg { get; set; }
        [DataMember]
        [JsonProperty("exporter")]
        public string ExporterFg { get; set; }
        [DataMember]
        [JsonProperty("declaration")]
        public string DecFg { get; set; }
        [DataMember]
        [JsonProperty("recieptDate")]
        public string ReceiptDt { get; set; }
        [DataMember]
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [DataMember]
        [JsonProperty("calendarType")]
        public string Caltp { get; set; }
        [DataMember]
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [DataMember]
        [JsonProperty("hotlineNumber")]
        public string HotlineNo { get; set; }
        [DataMember]
        [JsonProperty("returnSource")]
        public string RetSource { get; set; }
        [DataMember]
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [DataMember]
        [JsonProperty("industrySector")]
        public string Activity { get; set; }
        [DataMember]
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [DataMember]
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [DataMember]
        [JsonProperty("totalSales")]
        public string ToSflg { get; set; }
        [DataMember]
        [JsonProperty("declarationMode")]
        public string DmodeFlg { get; set; }
        [DataMember]
        [JsonProperty("block")]
        public string Block { get; set; }
        [DataMember]
        [JsonProperty("fieldName")]
        public string FldNm { get; set; }
        [DataMember]
        [JsonProperty("VATItems")]
        public List<Result6> VATPERITEMSet { get; set; }
        [DataMember]
        [JsonProperty("notes")]
        public List<Note> NOTESSet { get; set; }
        [DataMember]
        [JsonProperty("IBANs")]
        public List<Result2> IBANSet { get; set; }
        [DataMember]
        [JsonProperty("credits")]
        public List<Result3> CFSet { get; set; }
        [DataMember]
        [JsonProperty("attachments")]
        public List<Attachment> ATTACHSet { get; set; }
        [DataMember]
        [JsonProperty("addresses")]
        public List<Result5> ADRSet { get; set; }
        [DataMember]
        [JsonProperty("messages")]
        public List<object> VATR_MSGSet { get; set; }
        [DataMember]
        [JsonProperty("madaButton")]
        public string MadabutFg { get; set; }
        [DataMember]
        [JsonProperty("messageDescription")]
        public string OpenliMsg { get; set; }
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

    [Serializable]
   
    [DataContract]
    public class VATAttachments
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string Size { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class CreditCarried
    {
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public string ReturnReferenceNumber { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public string Amount { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclaration
    {
        [DataMember]
        [JsonProperty("data")]
        public VATDeclarationD data { get; set; }
        [JsonProperty("result")]
        public VATDeclarationD data1 { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclarationTabbedPageName : INotifyPropertyChanged
    {
        [DataMember]
        Color textColor = (Color)Application.Current.Resources["White"];
        [DataMember]
        Font _font = Font.Default;
        [DataMember]
        Font fontnew;
        //public VATDeclarationTabbedPageName()
        //{
        //    //_font.FontFamily=
        //}
        [DataMember]
        public string pageName { get; set; }
        [DataMember]
        public Color TextColor
        {
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
            get
            {
                return textColor;
            }
        }
        [DataMember]
        public Font Font
        {
            set
            {
                if (_font != value)
                {
                    _font = value;
                    OnPropertyChanged(nameof(Font));
                }
            }
            get
            {
                return _font;
            }
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
    [Serializable]
   
    [DataContract]

    public class ApplicableButton
    {
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [DataMember]
        [JsonProperty("formBundleStatus")]
        public string Fbust { get; set; }
        [DataMember]
        [JsonProperty("button")]
        public string Button { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
        [DataMember]
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public Buttons buttonEnumId = Buttons.None;
    }


    #region New Added models for VAT 15%change

    [Serializable]
   
    [DataContract]
    public class VATPERITEMSet
    {
        [DataMember]
        [JsonProperty("VATItems")]
        public List<Result6> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Result6
    {
        [DataMember]
        public VATDeclarationsMetadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("VATType")]
        public string Type { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("rate")]
        public string Rate { get; set; }
        [DataMember]
        [JsonProperty("standardSalesAmount")]
        public string StdsalesAmt { get; set; }
        [DataMember]
        [JsonProperty("standardSalesAdjustment")]
        public string StdsalesAdj { get; set; }
        [DataMember]
        [JsonProperty("standardSalesVAT")]
        public string StdsalesVat { get; set; }
        [DataMember]
        [JsonProperty("standardPurchaseAmount")]
        public string StdpurchaseAmt { get; set; }
        [DataMember]
        [JsonProperty("standardPurchaseAdjustment")]
        public string StdpurchaseAdj { get; set; }
        [DataMember]
        [JsonProperty("standardPurchasesVAT")]
        public string StdpurchasesVat { get; set; }
        [DataMember]
        [JsonProperty("importsPaidAmount")]
        public string ImportspaidAmt { get; set; }
        [DataMember]
        [JsonProperty("importsPaidAdjustment")]
        public string ImportspaidAdj { get; set; }
        [DataMember]
        [JsonProperty("importsPaidVAT")]
        public string ImportspaidVat { get; set; }
        [DataMember]
        [JsonProperty("importsAccountAmount")]
        public string ImportsaccAmt { get; set; }
        [DataMember]
        [JsonProperty("importsAccountAdjustment")]
        public string ImportsaccAdj { get; set; }
        [DataMember]
        [JsonProperty("importsAccountVAT")]
        public string ImportsaccVat { get; set; }
        [DataMember]
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [DataMember]
        [JsonProperty("timeStampCreation")]
        public string TimestampCr { get; set; }
        [DataMember]
        [JsonProperty("timeStampChange")]
        public string TimestampCh { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        [JsonProperty("governmentSuppliesSalesAdjustment")]
        public string GovsupsalesAdj { get; set; }
        [DataMember]
        [JsonProperty("governmentSuppliesSalesVAT")]
        public string GovsupsalesVat { get; set; }
        [DataMember]
        [JsonProperty("governmentSuppliesSalesAmount")]
        public string GovsupsalesAmt { get; set; }
    }

    #endregion


}
