using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.TraiffSection
{

    public class MainHarmonizedTariff
    {
        public string hrmnzd_code { get; set; }
        public string main_item_code { get; set; }
        public string sub_item_code { get; set; }
        public string locl_item_code { get; set; }
        public string anal_item_code { get; set; }
        public string item_arbc_desc { get; set; }
        public int item_rstrct_stat { get; set; }
        public string chpt_code { get; set; }
        public int item_type { get; set; }
        public string item_eng_desc { get; set; }
        public object tariffreleaseprocedures { get; set; }
        public object tariffdetails { get; set; }
        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    if (!string.IsNullOrEmpty(item_eng_desc))
                        return item_eng_desc;
                }
                return item_arbc_desc;
            }
        }
        public string Code { get { return main_item_code; } }

    }

    public class MainHarmonizedTariffModel
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<MainHarmonizedTariff> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }


}
