using System;
namespace EGAZT.Models.EinvoiceModels
{
    public class EradQRResponseModel
    {
        public TaxpayerRESP Taxpayer_RESP { get; set; }
        public ERROR ERROR { get; set; }

    }

    public class TaxpayerRESP
    {
        public long TIN { get; set; }
        public long VAT_CERT_NO { get; set; }
        public string Name { get; set; }
        public int BLDG_CODE { get; set; }
        public string Street { get; set; }
        public int Post_Code { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public long VAT_ACT_NO { get; set; }
    }
    public class ERROR
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

}

