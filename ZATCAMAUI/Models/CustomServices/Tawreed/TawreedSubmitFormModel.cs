
namespace ZATCAMAUI.Models.CustomServices.Tawreed
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
        public long IamRegisteredUserID { get; set; }
        public string mobileNumber { get; set; }
        public Attachement attachement { get; set; }
    }

}

