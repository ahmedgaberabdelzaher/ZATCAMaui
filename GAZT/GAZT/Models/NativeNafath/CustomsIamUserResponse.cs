using System;
namespace EGAZT.Models.NativeNafath
{
    public class CustomsIamUser
    {
        public int id { get; set; }
        public string nationalId { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string fourthName { get; set; }
        public string address { get; set; }
        public DateTime birthDate { get; set; }
        public int nationalityId { get; set; }
        public string nationalityNameArabic { get; set; }
        public string nationalityNameEnglish { get; set; }
        public int cityId { get; set; }
        public bool gender { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string iqamaExpiryDateHijri { get; set; }
        public string idExpiryDateHijri { get; set; }
        public string cardIssueDateHijri { get; set; }
        public string dateOfBirthHijri { get; set; }
    }

    public class IamHeader
    {
        public string requestID { get; set; }
        public IamStatus status { get; set; }
    }

    public class CustomsIamUserResponse
    {
        public IamHeader header { get; set; }
        public CustomsIamUser data { get; set; }
    }

    public class IamStatus
    {
        public string code { get; set; }
        public string description { get; set; }
    }

}

