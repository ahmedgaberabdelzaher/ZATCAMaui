using System;
namespace EGAZT.Models.NativeNafath
{
	
    public class PremiumResidencytypeData
    {
        public int iqamaType { get; set; }
        public string iqamaTypeDescription { get; set; }
        public string iqamaExpiryDate { get; set; }
    }

    public class PremiumResidencytypeResponseHeader
    {
        public string requestID { get; set; }
        public PremiumResidencytypeStatus status { get; set; }
    }

    public class PremiumResidencytypeResponse
    {
        public PremiumResidencytypeResponseHeader header { get; set; }
        public PremiumResidencytypeData data { get; set; }
    }

    public class PremiumResidencytypeStatus
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}

