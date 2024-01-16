using System;
namespace EGAZT.Models.CustomServices.Tawreed
{
    
    public class Attachement
    {
        public string fileContent { get; set; }
        public string fileName { get; set; }
    }

    public class TawreedSubmitFormModel
    {
        public string subject { get; set; }
        public string description { get; set; }
        public int departmentTypeId { get; set; }
        public string email { get; set; }
        public string CrNumber { get; set; }
        public string referenceNumber { get; set; }
        public long iamRegisteredUserID { get; set; }
        public string mobileNumber { get; set; }
        public Attachement attachement { get; set; }
        public int crNumber { get; set; }
        public long TIN { get; set; }
        public int buildingNumber { get; set; }
        public string streetNumber { get; set; }
        public string districtNumber { get; set; }
        public string cityName { get; set; }
        public int postCode { get; set; }
        public int additionalNumber { get; set; }
    }

}

