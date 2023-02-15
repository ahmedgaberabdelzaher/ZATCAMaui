using System;
using Prism.Mvvm;

namespace EGAZT.Models.EDeclerationsModel.FeesCalculators
{
   
    public class FeesCalculatorResponse:BindableBase
    {
        public string tobaccoExciseTaxEquation { get; set; }
        public string tobaccoCustomsTaxEquation { get; set; }
        public double? customsPercentage { get; set; }
        public double? productFinalPrice { get; set; }
        public double totalPayment { get; set; }
        public double vat { get; set; }
        public double? extraFees { get; set; }
        public double excise { get; set; }
        public double? totalDuty { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CustomApiDATA
    {
        public double TotalDuty { get; set; }
        public double Excise { get; set; }
        public double ExtraFees { get; set; }
        public double VAT { get; set; }
        public double TotalPayment { get; set; }
        public double ProductFinalPrice { get; set; }
        public double CustomesPercentage { get; set; }
        public string TobaccoCustomsTaxEquation { get; set; }
        public string TobaccoExciseTaxEquation { get; set; }
    }

    public class CustomApiFeesCalculatorResponse
    {
        public CustomApiDATA data { get; set; }
        public int Code { get; set; }
        public object Message { get; set; }
        public object DisplayMessageArabic { get; set; }
        public object DisplayMessageEnglish { get; set; }
        public string CorrelationID { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

