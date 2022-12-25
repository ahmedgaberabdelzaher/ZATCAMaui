using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class CurrencyModel
    {
        public string currencyName_English { get; set; }
        public string currencyName_Arabic { get; set; }
        public int currencyCode { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(currencyName_Arabic, currencyName_English);
            }
        }
    }
}

