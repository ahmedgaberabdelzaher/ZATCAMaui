using System;
using System.Collections.Generic;
using EGAZT.Helper;

namespace EGAZT.Models.EDeclerationsModel
{
    public class TobaccoItemsModel
    {
        public int taxSequence { get; set; }
        public int measurementUnit { get; set; }
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
        public string measureUnitAr { get; set; }
        public string measureUnitEn { get; set; }
        public bool HasMeasureUnit { get; set; }
        public bool hasWeight { get; set; }
        public string MeasureUnitName
        {
            get
            {
                return NameLocalization.GetLocalizedName(measureUnitAr, measureUnitEn);
            }
        }
    }

    
}

