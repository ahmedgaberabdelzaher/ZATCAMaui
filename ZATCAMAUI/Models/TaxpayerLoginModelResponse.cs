
namespace ZATCAMAUI.Models
{
    public class TaxpayerLoginModelResponse
    {
        public TaxpayerLoginModel result { get; set; }
    }

    public class TaxpayerLoginModel
    {
        public string accessToken { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_code { get; set; }
    }
}
