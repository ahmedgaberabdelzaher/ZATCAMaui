using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class UnitsModel
    {
        public string name_en { get; set; }
        public string name_ar { get; set; }
        public int id { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(name_ar, name_en);
            }
        }
    }
}

