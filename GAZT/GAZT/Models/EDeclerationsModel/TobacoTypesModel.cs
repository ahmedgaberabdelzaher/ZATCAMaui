using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class TobacoTypesModel
    {
        public string categoryName_English { get; set; }
        public string categoryName_Arabic { get; set; }
        public string typeID { get; set; }
        public string Name  { get {
                return NameLocalization.GetLocalizedName(categoryName_Arabic, categoryName_English);
            } }
    }

}

