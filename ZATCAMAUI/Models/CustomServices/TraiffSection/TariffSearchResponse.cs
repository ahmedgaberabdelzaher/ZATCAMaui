using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.TraiffSection
{

    public class TariffSearch
    {
        public string hrmnzd_code { get; set; }
        public string main_item_code { get; set; }
        public string sub_item_code { get; set; }
        public string locl_item_code { get; set; }
        public string anal_item_code { get; set; }
        public string item_arbc_desc { get; set; }
        public string item_eng_desc { get; set; }
        public int item_rstrct_stat { get; set; }
        public string chpt_code { get; set; }
        public int item_type { get; set; }
        public ObservableCollection<Tariffreleaseprocedure> tariffreleaseprocedures { get; set; }
        public ObservableCollection<SearchTariffdetail> tariffdetails { get; set; }
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
        public string Code
        {
            get
            {
                return chpt_code + main_item_code + " , " + hrmnzd_code;
            }
        }
    }

    public class TariffSearchResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<TariffSearch> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }

    public class SearchTariffdetail
    {
        public string hrmnzd_code { get; set; }
        public DateTime eff_date { get; set; }
        public double duty_rate { get; set; }
        public int duty_type { get; set; }
        public double min_duty_rate { get; set; }
        public string duty_rate_code { get; set; }
        public string min_duty_rate_code { get; set; }
        public int sta_code { get; set; }
        public object min_duty_unit { get; set; }
        public object duty_unit { get; set; }
        public int trf_intl_unit { get; set; }
        public string dutyName
        {
            get
            {
                if (duty_rate == 0)
                {
                    return AppResources.Free;
                }
                else
                {
                    return duty_rate.ToString() + " %";
                }
            }
        }

    }

    public class SearchTariffreleaseprocedure
    {
        public int prcdr_code { get; set; }
        public string rlse_prcdr_note { get; set; }
        public object athrty_cd { get; set; }
        public object athrty_sub_cd { get; set; }
        public int prcdr_type { get; set; }
    }
}
