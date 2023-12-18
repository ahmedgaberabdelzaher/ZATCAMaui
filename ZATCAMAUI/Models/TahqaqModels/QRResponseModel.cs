
namespace ZATCAMAUI.Models.TahqaqModels
{
    public class Pack
    {
        public string ProductDescription { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public string OrganisationName { get; set; }
        public string CountryOfManufacture { get; set; }
        public object CustomsAuthority { get; set; }
        public object CustomersClearanceDate { get; set; }
    }

    public class QRResponseModel
    {
        public bool IsValid { get; set; }
        public bool IsPackCode { get; set; }
        public Pack pack { get; set; }
    }

}
