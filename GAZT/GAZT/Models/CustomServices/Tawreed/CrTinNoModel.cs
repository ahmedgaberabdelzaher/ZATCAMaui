using System;
using EGAZT.Helper;

namespace EGAZT.Models.CustomServices.Tawreed
{
	public class CrTinNoModel
	{

        public string englishName { get; set; }
        public string arabicName { get; set; }
        public string TINNumber { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(arabicName, englishName);
            }
        }
    }
}

