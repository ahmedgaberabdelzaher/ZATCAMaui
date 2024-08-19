
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.PaymentModel
{

    public class PaymentOptionsModel
    {
        public PaymentOptionsModel()
        {
        }
        public string CardLabel { get; set; }
        public string UnSelectedCardIcon { get; set; }
        public string SelectedCardIcon { get; set; }
        public bool IsSelectedCardIconVisible { get => true; }
        public bool IsUnSelectedCardIconVisible { get => true; }
        public int IconHeight { get; set; }

    }


    
    public class MadaPayment
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("epayTransactionId")]
        public string Epaytransid { get; set; }
        [JsonProperty("sadadPaymentNumber")]
        public string Vtre2 { get; set; }
        [JsonProperty("revenueType")]
        public string Abtyp { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("contractAccount")]
        public string Vkont { get; set; }
        [JsonProperty("contractNumber")]
        public string Vtref { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("periodkey")]
        public string Persl { get; set; }
        [JsonProperty("applicationZakatAmount")]
        public string Amtmd { get; set; }
        [JsonProperty("DFZakatAmount")]
        public string Oamtmd { get; set; }
        [JsonProperty("MADAAmount")]
        public string Mamtmd { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("messageId")]
        public string Messageid { get; set; }
        [JsonProperty("cancellationReason")]
        public string CancelRes { get; set; }
        [JsonProperty("timeStampCreation")]
        public DateTime TimestampCr { get; set; }
        [JsonProperty("paymentStatus")]
        public string PayStat { get; set; }
        [JsonProperty("finalPaymentStatus")]
        public string FinalStat { get; set; }
        [JsonProperty("paymentLot")]
        public string Keyz1 { get; set; }
        [JsonProperty("incomingStatus")]
        public string InStatus { get; set; }
        [JsonProperty("paymentDate")]
        public object PayDate { get; set; }
        [JsonProperty("paymentAmount")]
        public string PayAmount { get; set; }
        [JsonProperty("paymentReference")]
        public string PayRef { get; set; }
        [JsonProperty("paymentProcess")]
        public string PymntProcFg { get; set; }
        [JsonProperty("paymentSourceId")]
        public string Srcid { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Zbranch { get; set; }
        [JsonProperty("periodDescription")]
        public string PerslTxt { get; set; }
    }
    
    public class MadaPaymentResponse
    {
        [JsonProperty("data")]
        public MadaPayment d { get; set; }
    }
    
    public class MadaPaymentRequest
    {
        public string caseGUID { get; set; }
        public string paymentSourceId { get; set; }
    }

    
    public class CreateMadaResponseRoot
    {

        [JsonProperty("header")]
        public Header header { get; set; }

        [JsonProperty("result")]
        public CreateMadaResponse result { get; set; }

    }
    
    public class CreateMadaResponse
    {
        [JsonProperty("securityAuthorizationKey")]
        public string securityAuthorizationKey { get; set; }

        [JsonProperty("paymentMethod")]
        public string paymentMethod { get; set; }

        [JsonProperty("sessionId")]
        public string sessionId { get; set; }

        [JsonProperty("sourceId")]
        public string sourceId { get; set; }

        [JsonProperty("GUID")]
        public string GUID { get; set; }

        [JsonProperty("success")]
        public string success { get; set; }

        [JsonProperty("merchantId")]
        public long merchantId { get; set; }

        [JsonProperty("processURL")]
        public string processURL { get; set; }

        [JsonProperty("amount")]
        public decimal amount { get; set; }

    }

    
    public class ApplePayRequestGuid
    {
        public string Fbnum { get; set; }
        public string Tin { get; set; }
        public string Srcid { get; set; }
        public string PymntType { get; set; }
    }

    
    public class UpdateApplePayRequestGuid
    {
        public string Guid { get; set; }
        public string PaymentToken { get; set; }

    }


    
    public class ApplePayGuid
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Tin { get; set; }
        public string Guid { get; set; }
        public string Link { get; set; }
        public string Srcid { get; set; }
        public string SadadNo { get; set; }
        public string PymntType { get; set; }
    }
    
    public class ApplePayGuidResponse
    {
        public ApplePayGuid d { get; set; }
    }

    
    public class Header
    {
        public string ephemeralPublicKey { get; set; }
        public string publicKeyHash { get; set; }
        public string transactionId { get; set; }
    }
    
    public class ApplePayToken
    {
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("paymentToken")]
        public string PaymentToken { get; set; }
        [JsonProperty("paymentSourceId")]
        public string SrcId { get; set; }

    }

    
    public class ApplePayinfo
    {
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("paymentToken")]
        public string PaymentToken { get; set; }
        [JsonProperty("paymentSourceId")]
        public string Gpart { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("paymentSourceId")]
        public string SrcId { get; set; }
        [JsonProperty("merchantId")]
        public string Merchantid { get; set; }
        [JsonProperty("processURL")]
        public string Procurl { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("paymentReference")]
        public string PayRef { get; set; }
        [JsonProperty("isSuccess")]
        public bool Success { get; set; }
        [JsonProperty("periodDescription")]
        public string PerslTxt { get; set; }
    }
    
    public class ApplePayTokenResponse
    {
        [JsonProperty("result")]
        public ApplePayinfo d { get; set; }
    }



    
    public class CancelPayment
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("sourceId")]
        public string SRCID { get; set; }
        [JsonProperty("GUID")]
        public string GUID { get; set; }
        [JsonProperty("cancellationReason")]
        public string CANC_RES { get; set; }
    }
    
    public class CancelPaymentResponse
    {
        [JsonProperty("result")]
        public CancelPayment d { get; set; }
    }
    
    public class CancelPaymentRequest
    {
        public string sourceId { get; set; }
        public string GUID { get; set; }
        public string cancellationReason { get; set; }
    }

    
    public class PaymentSucess
    {
        public string Paymentref { get; set; }
        public string Period { get; set; }
    }


    
    public class ValidatePayment
    {
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("sourceId")]
        public string Srcid { get; set; }
        [JsonProperty("paymentType")]
        public string Pymntty { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sadad { get; set; }
        [JsonProperty("sourceTile")]
        public string Srctile { get; set; }

    }

    
    public class ValidatePaymentResult
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("sourceId")]
        public string Srcid { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sadad { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("sourceTile")]
        public string Srctile { get; set; }
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("paymentType")]
        public string Pymntty { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }

    }

    
    public class ValidatePaymentResponse
    {
        [JsonProperty("result")]
        public ValidatePaymentResult d { get; set; }

    }

    
    public class ValidatePaymentRequestModel
    {
        public int amount { get; set; }
        public string formBundleNumber { get; set; }
        public string link { get; set; }
        public string sadadBillNumber { get; set; }
        public string sourceId { get; set; }
        public string sourceTile { get; set; }
        public string GUID { get; set; }
        public string paymentType { get; set; }
        public string TIN { get; set; }


    }

    
    public class CreateMadaPaymentPayload
    {
        public string sourceId { get; set; }
        public string GUID { get; set; }

    }





}
