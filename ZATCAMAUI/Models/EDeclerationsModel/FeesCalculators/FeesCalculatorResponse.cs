
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.EDeclerationsModel.FeesCalculators
{

    public class FeesCalculatorResponse : ObservableRecipient
    {
        public string tobaccoExciseTaxEquation { get; set; }
        public string tobaccoCustomsTaxEquation { get; set; }
        public double? customsPercentage { get; set; }
        public double? productFinalPrice { get; set; }
        double? _totalPayment;
        public double? totalPayment { get { return _totalPayment; } set { _totalPayment = value; OnPropertyChanged(); } }
        public double? vat { get; set; }
        public double? extraFees { get; set; }
        public double? excise { get; set; }
        public double totalDuty { get; set; } = 0;
        public double extraFeesMinimumValue { get; set; } = 0;
        public double extraFeesMaximumValue { get; set; } = 0;
        public double extraFeesPercentage { get; set; } = 0;

    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CustomApiDATA
    {
        public double TotalDuty { get; set; } = 0;
        public double? Excise { get; set; }
        public double? ExtraFees { get; set; }
        public double? VAT { get; set; }
        public double? TotalPayment { get; set; }
        public double? ProductFinalPrice { get; set; }
        public double? CustomesPercentage { get; set; }
        public string TobaccoCustomsTaxEquation { get; set; }
        public string TobaccoExciseTaxEquation { get; set; }
    }

    public class CustomApiFeesCalculatorResponse
    {
        public CustomApiDATA data { get; set; }
        public int? Code { get; set; }
        public object Message { get; set; }
        public object DisplayMessageArabic { get; set; }
        public object DisplayMessageEnglish { get; set; }
        public string CorrelationID { get; set; }
    }
}

