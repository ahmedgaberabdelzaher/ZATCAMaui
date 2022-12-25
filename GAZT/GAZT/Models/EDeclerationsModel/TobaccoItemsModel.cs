using System;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class TobaccoItemsModel
    {
        public int? taxSequence { get; set; }
        public int? measurementUnit { get; set; }
        public string productName { get; set; }
        public string itemDescription { get; set; }
        public string itemCode { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(itemDescription, productName);
            }
        }
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}

