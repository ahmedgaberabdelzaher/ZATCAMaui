using System;
using Newtonsoft.Json;

namespace EGAZT.Models
{
	public class ZATCAUserRegisterModel
	{
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string fourthName { get; set; }
        public bool gender { get; set; }
        public string nationalId { get; set; }
        public string birthDate { get; set; }
        public string address { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumber { get; set; }
        public string landlineNumber { get; set; }
        public int nationalityId { get; set; }
        public string PoBox { get; set; }
        public string postalCode { get; set; }
        public string mobilePrefix { get; set; }
        public int identityTypeId { get; set; }
        public int cityId { get; set; }
        public int maritalStatusId { get; set; }
        public string iqamaExpiryDateHijri { get; set; }
        public string idExpiryDateHijri { get; set; }
        public string cardIssueDateHijri { get; set; }
        public string dateOfBirthHijri { get; set; }
    }
}

