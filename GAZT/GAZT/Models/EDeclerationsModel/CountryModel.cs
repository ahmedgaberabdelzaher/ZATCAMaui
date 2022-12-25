using System;
using EGAZT.Helper;
namespace EGAZT.Models.EDeclerationsModel
{
	public class CountryModel
	{
        public string countryName_En { get; set; }
        public string countryName_Ar { get; set; }
        public int countryCode { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(countryName_Ar, countryName_En);
            }
        }
    }
}

