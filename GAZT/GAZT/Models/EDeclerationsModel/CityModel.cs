using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class CityModel
    {

        public string cityName_En { get; set; }
        public string cityName_Ar { get; set; }
        public int cityCode { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(cityName_Ar, cityName_En);
            }
        }

    }
}

