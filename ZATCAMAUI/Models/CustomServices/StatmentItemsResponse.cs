using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices
{
    public class StatmentItems
    {
        public string rv_meaning { get; set; }
        public string inv_date { get; set; }
        public int item_seq { get; set; }
        public int ctry_cd { get; set; }
        public string crty_arbc_name { get; set; }
        public string final_tariff_cd { get; set; }
        public string item_desc { get; set; }
        public double inv_qty { get; set; }
        public double gross_wt { get; set; }
        public double net_wt { get; set; }
        public string msr_arbc_name { get; set; }
        public object isd_chk_val { get; set; }
        public string curcy_intl_cd { get; set; }
        public double inv_cost { get; set; }
        public double duty_rate { get; set; }
        public string duty_type { get; set; }
        public double orgnl_duty { get; set; }
        public object exmtd_duty { get; set; }
        public double cif { get; set; }
        public double paid { get; set; }//Custom
        public string tariff_desc { get; set; }
        public string item_isn { get; set; }
        public string arbc_impr_name { get; set; }
        public string cr_nbr { get; set; }
    }

    public class StatmentItemsResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<StatmentItems> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
