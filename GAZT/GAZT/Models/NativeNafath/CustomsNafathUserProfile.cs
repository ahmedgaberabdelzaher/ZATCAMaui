using System;
namespace EGAZT.Models.NativeNafath
{
	

    public class CustomsNafathUserProfile
    {
        public int id { get; set; }
        public string nationalid { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public string thirdname { get; set; }
        public string fourthname { get; set; }
        public string address { get; set; }
        public DateTime birthdate { get; set; }
        public int nationalityid { get; set; }
        public string nationalitynamearabic { get; set; }
        public string nationalitynameenglish { get; set; }
        public int cityid { get; set; }
        public bool gender { get; set; }
        public string mobilenumber { get; set; }
        public object landlinenumber { get; set; }
        public string email { get; set; }
        public string mobileprefix { get; set; }
        public string iqamaexpirydatehijri { get; set; }
        public string idexpirydatehijri { get; set; }
        public string cardissuedatehijri { get; set; }
        public string dateofbirthhijri { get; set; }
        public object isfeaturedresidence { get; set; }
        public object featuredresidenceexpirydate { get; set; }
    }

    public class CustomsNafathUserProfileResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object enmessage { get; set; }
        public CustomsNafathUserProfile data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }

}

