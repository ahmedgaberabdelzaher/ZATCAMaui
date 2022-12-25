using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class CoinTypesModel
    {
        public string Name_English { get; set; }
        public string Name_Arabic { get; set; }
        public int ID { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(Name_Arabic, Name_English);
            }
        }
    }
}

