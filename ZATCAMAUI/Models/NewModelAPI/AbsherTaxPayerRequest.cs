using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI
{
    public class AbsherTaxPayerRequest
    {
        public string formBundleGUID { get; set; }
        public string OTPCode { get; set; }
        public string country { get; set; }
        public string taxpayerBirthDate { get; set; }
        public string passExpiryDate { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
    }
}
