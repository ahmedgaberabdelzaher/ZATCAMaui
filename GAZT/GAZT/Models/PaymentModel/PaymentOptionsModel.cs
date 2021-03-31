using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.PaymentModel
{
    [Preserve(AllMembers = true)]
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


    [Preserve(AllMembers = true)]
    public class MadaPayment
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Epaytransid { get; set; }
        public string Vtre2 { get; set; }
        public string Abtyp { get; set; }
        public string Fbtyp { get; set; }
        public string Gpart { get; set; }
        public string Vkont { get; set; }
        public string Vtref { get; set; }
        public string Fbnum { get; set; }
        public string Persl { get; set; }
        public string Amtmd { get; set; }
        public string Oamtmd { get; set; }
        public string Mamtmd { get; set; }
        public string Waers { get; set; }
        public string Messageid { get; set; }
        public string CancelRes { get; set; }
        public DateTime TimestampCr { get; set; }
        public string PayStat { get; set; }
        public string FinalStat { get; set; }
        public string Keyz1 { get; set; }
        public string InStatus { get; set; }
        public object PayDate { get; set; }
        public string PayAmount { get; set; }
        public string PayRef { get; set; }
        public string PymntProcFg { get; set; }
        public string Srcid { get; set; }
        public string Zbranch { get; set; }
        public string PerslTxt { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class MadaPaymentResponse
    {
        public MadaPayment d { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class ApplePayRequestGuid
    {
        public string Fbnum { get; set; }
        public string Tin { get; set; }
        public string Srcid { get; set; }
        public string PymntType { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class UpdateApplePayRequestGuid
    {
        public string Guid { get; set; }
        public string PaymentToken { get; set; }

    }


    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class ApplePayGuidResponse
    {
        public ApplePayGuid d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class Header
    {
        public string ephemeralPublicKey { get; set; }
        public string publicKeyHash { get; set; }
        public string transactionId { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ApplePayToken
    {
        public string Guid { get; set; }
        public string PaymentToken { get; set; }
        public string SrcId { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class ApplePayinfo
    {
        public string Guid { get; set; }
        public string PaymentToken { get; set; }
        public string Gpart { get; set; }
        public string Fbnum { get; set; }
        public string SrcId { get; set; }
        public string Merchantid { get; set; }
        public string Procurl { get; set; }
        public string Amount { get; set; }
        public string PayRef { get; set; }
        public bool Success { get; set; }
        public string PerslTxt { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ApplePayTokenResponse
    {
        public ApplePayinfo d { get; set; }
    }



    [Preserve(AllMembers = true)]
    public class CancelPayment
    {
        public Metadata __metadata { get; set; }
        public string SRCID { get; set; }
        public string GUID { get; set; }
        public string CANC_RES { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CancelPaymentResponse
    {
        public CancelPayment d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class PaymentSucess
    {
        public string Paymentref { get; set; }
        public string Period { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class ValidatePayment
    {
        public string Fbnum { get; set; }
        public string Tin { get; set; }
        public string Srcid { get; set; }
        public string Pymntty { get; set; }
        public string Sadad { get; set; }
        public string Srctile { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class ValidatePaymentResult
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Srcid { get; set; }
        public string Sadad { get; set; }
        public string Amount { get; set; }
        public string Link { get; set; }
        public string Srctile { get; set; }
        public string Guid { get; set; }
        public string Pymntty { get; set; }
        public string Tin { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class ValidatePaymentResponse
    {
        public ValidatePaymentResult d { get; set; }

    }



}
