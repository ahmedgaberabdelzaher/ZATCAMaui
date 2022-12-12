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
}

