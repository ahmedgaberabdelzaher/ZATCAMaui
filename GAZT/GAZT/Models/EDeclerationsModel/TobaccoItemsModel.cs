using System;
using System.Collections.Generic;
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

    //new TobacioItems
    public class TobaccoItemsNewModel
    {
        public string MeasureUnitAR { get; set; }
        public string MeasureUnitEN { get; set; }
        public bool HasMeasureUnit { get; set; }
        public string HarmonizedCode { get; set; }
        public string ItemDescription { get; set; }
        public string Productname { get; set; }
        public int MeasureUnit { get; set; }
        public int TaxSequence { get; set; }
        public bool HasWeight { get; set; }
    }

    public class TobaccoItemsNewModelResponse
    {
        public List<TobaccoItemsNewModel> data { get; set; }
        public int Code { get; set; }
        public object Message { get; set; }
        public object DisplayMessageArabic { get; set; }
        public object DisplayMessageEnglish { get; set; }
        public string CorrelationID { get; set; }
        public DateTime TransactionDate { get; set; }
    }


}

