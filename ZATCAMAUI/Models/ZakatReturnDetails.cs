

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
 
    public class ZakatReturnDetails
    {
        [JsonProperty("data")]
        public ZakatReturnDetailsD d { get; set; }

        [JsonProperty("result")]
        public ZakatReturnDetailsD result { get; set; }
    }

    
    public class ZakatReturnDetailsReason
    {
        public Metadata2 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("amendmentSource")]
        public string AmdSource { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    
    public class EstimateZakatAttachment
    {
        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }// Comp
        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }
        [JsonProperty("formGUID")]
        public string SchGuid { get; set; }
        [JsonProperty("documentCategory")]
        public string Dotyp { get; set; }
        [JsonProperty("serialNumber")]
        public int Srno { get; set; }
        [JsonProperty("documentId")]
        public string Doguid { get; set; }// Comp
        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }
        [JsonProperty("fileName")]
        public string Filename { get; set; }
        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }
        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }
        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }
        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }//Date

        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("documentURL")]
        public string DocUrl { get; set; }
        [JsonProperty("outletReference")]
        public string OutletRef { get; set; }
    }


    
    public class ZakatAttachment
    {
        public string RetGuid { get; set; }// Comp
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }// Comp
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileImage { get; set; }

        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }//Date
                                         //  public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string UploadededDateToShow { get; set; }
    }

    
    public class ReasonSet
    {
        [JsonProperty("reasons")]
        public List<ZakatReturnDetailsReason> results { get; set; }
    }

    
    public class AttachSet
    {
        [JsonProperty("attachments")]
        public List<EstimateZakatAttachment> results { get; set; }
    }

    
    public class ZakatReturnDetailsInvoice
    {
        public Metadata3 __metadata { get; set; }
        [JsonProperty("undisputedAmount")]
        public string Undisamt { get; set; }
        [JsonProperty("disputedAmount")]
        public string Disamt { get; set; }
        [JsonProperty("totalAmount")]
        public string Totamt { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("sadadId")]
        public string Sadadid { get; set; }
        [JsonProperty("sadadUndisputedAmount")]
        public string Sundisamt { get; set; }
        [JsonProperty("sadadDisputedAmount")]
        public string Sdisamt { get; set; }
        [JsonProperty("sadadTotalAmount")]
        public string Stotamt { get; set; }
    }

    
    public class InvoiceSet
    {
        [JsonProperty("invoices")]
        public List<ZakatReturnDetailsInvoice> results { get; set; }
    }

    
    public class ZakatReturnDetailsThresholdSet
    {
        public Metadata4 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("characteristicsName")]
        public string Chrnm { get; set; }
        [JsonProperty("startDate")]
        public String Begda { get; set; }//Date
        [JsonProperty("endDate")]
        public string Endda { get; set; }//Date
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    
    public class ThresholdSet
    {
        [JsonProperty("thresholds")]
        public List<ZakatReturnDetailsThresholdSet> results { get; set; }
    }

    
    public class ZakatReturnDetailsD
    {
        //public Metadata __metadata { get; set; }
        [JsonProperty("realEstate")]
        public string RestFlg { get; set; }
        [JsonProperty("versionNumberComponent")]
        public string Fsource { get; set; }
        [JsonProperty("informedLaboursNumber")]
        public string LabnoI { get; set; }
        [JsonProperty("zakatAmount")]
        public string Zkamt { get; set; }
        [JsonProperty("madaButton")]
        public string MadabutFg { get; set; }
        [JsonProperty("messageDescription")]
        public string OpenliMsg { get; set; }
        [JsonProperty("estimatedLaboursNumber")]
        public string LabnoE { get; set; }
        [JsonProperty("zakatBaseAmount")]
        public string Zbamt { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("userType")]
        public string UserTypz { get; set; }
        [JsonProperty("userTIN")]
        public string UserTinz { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTpz { get; set; }
        [JsonProperty("totalVATSalesAmendmentReason")]
        public string TvtslResn { get; set; }
        [JsonProperty("informedTotalVATSales")]
        public string TvtslI { get; set; }
        [JsonProperty("estimatedTotalVATSales")]
        public string TvtslE { get; set; }
        [JsonProperty("taxpayerStatus")]
        public string Tpstatus { get; set; }
        [JsonProperty("creationDate")]
        public string TimestampCr { get; set; }//date
        [JsonProperty("changeDate")]
        public string TimestampCh { get; set; }//Date
        [JsonProperty("termsAndConditions")]
        public string Tcflg { get; set; }
        [JsonProperty("contractsSum")]
        public string Sumcnt { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumberz { get; set; }
        [JsonProperty("statusCode")]
        public string Statusz { get; set; }
        [JsonProperty("sourceApplication")]
        public string SrcAppz { get; set; }
        [JsonProperty("sadad")]
        public string SadadFlg { get; set; }
        [JsonIgnore]
        public string ReturnIdz { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("pointOfSaleAmendmentReason")]
        public string PtoslResn { get; set; }
        [JsonProperty("informedPointOfSale")]
        public string PtoslI { get; set; }
        [JsonProperty("purchaseAmendmentReason")]
        public string PramtResn { get; set; }
        [JsonProperty("realEstateAmendmentReason")]
        public string RestResn { get; set; }
        [JsonProperty("informedPurchase")]
        public string PramtI { get; set; }
        [JsonProperty("estimatedPurchase")]
        public string PramtE { get; set; }
        [JsonProperty("informedRealEstate")]
        public string RestI { get; set; }
        [JsonProperty("estimatedRealEstate")]
        public string RestE { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [JsonIgnore]
        public string Periodkeyz { get; set; }
        [JsonProperty("payableAmount")]
        public string Payamt { get; set; }
        [JsonProperty("operation")]
        public string Operationz { get; set; }

        [JsonProperty("userName")]
        public string Officerz { get; set; }
        [JsonIgnore]
        public string OfficerTz { get; set; }
        [JsonProperty("objectionStatus")]
        public string Objst { get; set; }
        [JsonProperty("objection")]
        public string ObjFlag { get; set; }
        [JsonProperty("notification")]
        public string Notfg { get; set; }
        public string Mandtz { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("labourAverageReason")]
        public string LabnoResn { get; set; }
        [JsonProperty("invoice")]
        public string Invflg { get; set; }
        [JsonProperty("inboundCorrespondenceType")]
        public string Incotyp { get; set; }
        [JsonProperty("importValueReason")]
        public string ImpvalResn { get; set; }
        [JsonProperty("informedImportValue")]
        public string ImpvalI { get; set; }
        [JsonProperty("estimatedImportValue")]
        public string ImpvalE { get; set; }
        [JsonIgnore]
        public string Gpartz { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("formProcess")]
        public string Formprocz { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonIgnore]
        public string Fbnumz { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("exportReason")]
        public string ExamtResn { get; set; }
        [JsonProperty("informedExportAmount")]
        public string ExamtI { get; set; }
        [JsonProperty("serialNumber")]
        public string Euser { get; set; }
        [JsonProperty("estimatedReason")]
        public string EtimadResn { get; set; }
        [JsonProperty("informedEstimatedAmount")]
        public string EtimadI { get; set; }
        [JsonProperty("estimatedSalesAmount")]
        public string Estsl { get; set; }
        [JsonProperty("endDate")]
        public object Enddaz { get; set; }
        [JsonProperty("disputedAmount")]
        public string Disamt { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("capitalAmountReason")]
        public string CpamtResn { get; set; }
        [JsonProperty("captialAmount")]
        public string Cpamt { get; set; }
        [JsonProperty("corresondecneType")]
        public string Cotyp { get; set; }
        [JsonProperty("corresondecneKey")]
        public string Cokey { get; set; }
        [JsonProperty("startDate")]
        public object Begdaz { get; set; }
        [JsonProperty("amendmentReason")]
        public string AmdRsn { get; set; }
        [JsonProperty("amendmentDisclaimer")]
        public string Amdflg { get; set; }
        [JsonProperty("periodStartDate")]
        public string Abrzu { get; set; }//Date
        [JsonProperty("periodEndDate")]
        public string Abrzo { get; set; }// Date
        [JsonProperty("reasons")]
        public List<ZakatReturnDetailsReason> ReasonSet { get; set; }
        [JsonProperty("attachments")]
        public List<EstimateZakatAttachment> AttachSet { get; set; }
        [JsonProperty("invoices")]
        public List<ZakatReturnDetailsInvoice> InvoiceSet { get; set; }
        [JsonProperty("thresholds")]
        public List<ZakatReturnDetailsThresholdSet> ThresholdSet { get; set; }
    }
}
