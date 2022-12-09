using System;
using Prism.Mvvm;

namespace EGAZT.Models.EDeclerationsModel.FeesCalculators
{
   
    public class FeesCalculatorResponse:BindableBase
    {
        public string tobaccoExciseTaxEquation { get; set; }
        public string tobaccoCustomsTaxEquation { get; set; }
        public int customsPercentage { get; set; }
        public int productFinalPrice { get; set; }
        public double totalPayment { get; set; }
        public double vat { get; set; }
        public int extraFees { get; set; }
        public double excise { get; set; }
        public int totalDuty { get; set; }
    }
}

