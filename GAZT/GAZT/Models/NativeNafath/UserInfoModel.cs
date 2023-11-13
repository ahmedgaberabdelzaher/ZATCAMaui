using System;
using EGAZT.Helper;

namespace EGAZT.Models.NativeNafath
{
	public class UserInfoModel
	{
		public int id { get; set; }
		public IdInfoModel idInfo { get; set; }
		public NameModel arName { get; set; }
		public NameModel enName { get; set; }
        public string FullName
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName?.full, enName?.full);
            }
        }
        public string FirstName
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName?.first, enName?.first);
            }
        }
        public string FatherName
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName?.father, enName?.father);
            }
        }
        public string GrandFatherName
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName?.grandfather, enName?.grandfather);
            }
        }
        public string FamilyName
        {
            get
            {
                return NameLocalization.GetLocalizedName(arName?.family, enName?.family);
            }
        }
        public string dateOfBirthH { get; set; }
		public string dateOfBirthG { get; set; }
		public string gender { get; set; }
		public NationalityModel nationality { get; set; }
		public string language { get; set; }
	}
}

