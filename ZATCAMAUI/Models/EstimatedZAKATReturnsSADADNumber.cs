using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class EstimatedZAKATReturnsSADADNumber
    {
        [JsonProperty(PropertyName = "data")]
        public EstimatedZAKATReturnsSADADNumberD d { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberDeferred
    {
        public string uri { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberDeferredReasonSet
    {
        public EstimatedZAKATReturnsSADADNumberDeferred __deferred { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberDeferred2
    {
        public string uri { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberAttachSet
    {
        public EstimatedZAKATReturnsSADADNumberDeferred2 __deferred { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberResult
    {
        public Metadata2 __metadata { get; set; }
        [JsonProperty(PropertyName = "undisputedAmount")]
        public string Undisamt { get; set; }
        [JsonProperty(PropertyName = "disputedAmount")]
        public string Disamt { get; set; }
        [JsonProperty(PropertyName = "totalAmount")]
        public string Totamt { get; set; }
        [JsonProperty(PropertyName = "sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty(PropertyName = "sadadId")]
        public string Sadadid { get; set; }
        [JsonProperty(PropertyName = "sadadUndisputedAmount")]
        public string Sundisamt { get; set; }
        [JsonProperty(PropertyName = "sadadDisputedAmount")]
        public string Sdisamt { get; set; }
        [JsonProperty(PropertyName = "sadadTotalAmount")]
        public string Stotamt { get; set; }
        public bool ObjectionInvoiceVisibility { get; set; } = false;
        public bool AmendInvoiceVisibility { get; set; } = false;
        public bool InvoiceVisibility { get; set; } = false;
    }
    
    public class EstimatedZAKATReturnsSADADNumberInvoiceSet
    {
        public List<EstimatedZAKATReturnsSADADNumberResult> results { get; set; }
    }
    
    public class Deferred3
    {
        public string uri { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberThresholdSet
    {
        public Deferred3 __deferred { get; set; }
    }
    
    public class EstimatedZAKATReturnsSADADNumberD
    {
        public Metadata __metadata { get; set; }
        [JsonProperty(PropertyName = "versionNumberComponent")]

        public string Fsource { get; set; }
        [JsonProperty(PropertyName = "informedLaboursNumber")]
        public string LabnoI { get; set; }
        [JsonProperty(PropertyName = "zakatAmount")]
        public string Zkamt { get; set; }
        [JsonProperty(PropertyName = "estimatedLaboursNumber")]
        public string LabnoE { get; set; }
        [JsonProperty(PropertyName = "zakatBaseAmount")]
        public string Zbamt { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string Waers { get; set; }
        [JsonProperty(PropertyName = "userType")]
        public string UserTypz { get; set; }
        [JsonProperty(PropertyName = "userTIN")]
        public string UserTinz { get; set; }
        [JsonProperty(PropertyName = "transactionType")]
        public string TxnTpz { get; set; }
        [JsonProperty(PropertyName = "totalVATSalesAmendmentReason")]
        public string TvtslResn { get; set; }
        [JsonProperty(PropertyName = "informedTotalVATSales")]
        public string TvtslI { get; set; }
        [JsonProperty(PropertyName = "estimatedTotalVATSales")]
        public string TvtslE { get; set; }
        [JsonProperty(PropertyName = "taxpayerStatus")]
        public string Tpstatus { get; set; }
        //public DateTime TimestampCr { get; set; }
        // public DateTime TimestampCh { get; set; }
        [JsonProperty(PropertyName = "termsAndConditions")]
        public string Tcflg { get; set; }
        [JsonProperty(PropertyName = "contractsSum")]
        public string Sumcnt { get; set; }
        [JsonProperty(PropertyName = "stepNumber")]
        public string StepNumberz { get; set; }
        [JsonProperty(PropertyName = "statusCode")]
        public string Statusz { get; set; }
        [JsonProperty(PropertyName = "sourceApplication")]
        public string SrcAppz { get; set; }
        [JsonProperty(PropertyName = "sadad")]
        public string SadadFlg { get; set; }
        [JsonProperty(PropertyName = "returnId")]
        public string ReturnIdz { get; set; }
        public string ReturnId { get; set; }
        [JsonProperty(PropertyName = "pointOfSaleAmendmentReason")]
        public string PtoslResn { get; set; }
        [JsonProperty(PropertyName = "informedPointOfSale")]
        public string PtoslI { get; set; }
        [JsonProperty(PropertyName = "purchaseAmendmentReason")]
        public string PramtResn { get; set; }
        [JsonProperty(PropertyName = "informedPurchase")]
        public string PramtI { get; set; }
        [JsonProperty(PropertyName = "estimatedPurchase")]
        public string PramtE { get; set; }
        [JsonProperty(PropertyName = "portalUser")]
        public string PortalUsrz { get; set; }
        [JsonProperty(PropertyName = "periodKey")]
        public string Persl { get; set; }
        public string Periodkeyz { get; set; }
        [JsonProperty(PropertyName = "payableAmount")]
        public string Payamt { get; set; }
        [JsonProperty(PropertyName = "operation")]
        public string Operationz { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string Officerz { get; set; }
        public string OfficerTz { get; set; }
        [JsonProperty(PropertyName = "objectionStatus")]
        public string Objst { get; set; }
        [JsonProperty(PropertyName = "objection")]
        public string ObjFlag { get; set; }
        [JsonProperty(PropertyName = "notification")]
        public string Notfg { get; set; }
        [JsonProperty(PropertyName = "systemCode")]
        public string Mandtz { get; set; }
        public string Mandt { get; set; }
        [JsonProperty(PropertyName = "language ")]
        public string Langz { get; set; }
        [JsonProperty(PropertyName = "labourAverageReason")]
        public string LabnoResn { get; set; }
        [JsonProperty(PropertyName = "invoice")]
        public string Invflg { get; set; }
        [JsonProperty(PropertyName = "inboundCorrespondenceType")]
        public string Incotyp { get; set; }
        [JsonProperty(PropertyName = "importValueReason")]
        public string ImpvalResn { get; set; }
        [JsonProperty(PropertyName = "informedImportValue")]
        public string ImpvalI { get; set; }
        [JsonProperty(PropertyName = "estimatedImportValue")]
        public string ImpvalE { get; set; }
        [JsonProperty(PropertyName = "TIN")]
        public string Gpartz { get; set; }
        public string Gpart { get; set; }
        [JsonProperty(PropertyName = "formProcess")]
        public string Formprocz { get; set; }
        [JsonProperty(PropertyName = "FormGuid")]
        public string FormGuid { get; set; }
        [JsonProperty(PropertyName = "formBundleNumber")]
        public string Fbnumz { get; set; }
        public string Fbnum { get; set; }
        [JsonProperty(PropertyName = "formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty(PropertyName = "exportReason")]
        public string ExamtResn { get; set; }
        [JsonProperty(PropertyName = "informedExportAmount")]
        public string ExamtI { get; set; }
        [JsonProperty(PropertyName = "serialNumber")]
        public string Euser { get; set; }
        [JsonProperty(PropertyName = "estimatedReason")]
        public string EtimadResn { get; set; }
        [JsonProperty(PropertyName = "informedEstimatedAmount")]
        public string EtimadI { get; set; }
        [JsonProperty(PropertyName = "estimatedSalesAmount")]
        public string Estsl { get; set; }
        //public string Enddaz { get; set; }
        [JsonProperty(PropertyName = "disputedAmount")]
        public string Disamt { get; set; }
        [JsonProperty(PropertyName = "dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty(PropertyName = "capitalAmountReason")]
        public string CpamtResn { get; set; }
        [JsonProperty(PropertyName = "captialAmount")]
        public string Cpamt { get; set; }
        [JsonProperty(PropertyName = "corresondecneType")]
        public string Cotyp { get; set; }
        [JsonProperty(PropertyName = "corresondecneKey")]
        public string Cokey { get; set; }
        //  public string Begdaz { get; set; }
        [JsonProperty(PropertyName = "amendmentReason ")]
        public string AmdRsn { get; set; }
        [JsonProperty(PropertyName = "amendmentDisclaimer")]
        public string Amdflg { get; set; }
        // public DateTime Abrzu { get; set; }
        //public DateTime Abrzo { get; set; }
        public EstimatedZAKATReturnsSADADNumberDeferredReasonSet ReasonSet { get; set; }
        public EstimatedZAKATReturnsSADADNumberAttachSet AttachSet { get; set; }
        //public EstimatedZAKATReturnsSADADNumberInvoiceSet InvoiceSet { get; set; }
        [JsonProperty(PropertyName = "invoices")]
        public List<EstimatedZAKATReturnsSADADNumberResult> results { get; set; }
        public EstimatedZAKATReturnsSADADNumberThresholdSet ThresholdSet { get; set; }
    }

    //public class RootObject
    //{
    //    public EstimatedZAKATReturnsSADADNumberD d { get; set; }
    //}
}
