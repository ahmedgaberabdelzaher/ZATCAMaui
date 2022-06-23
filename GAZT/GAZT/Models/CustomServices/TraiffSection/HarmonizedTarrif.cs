using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EGAZT.Models.CustomServices.TraiffSection
{
   
    public class HarmonizedTarrif
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
        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    if (!String.IsNullOrEmpty(item_eng_desc))
                        return item_eng_desc;
                }
                return item_arbc_desc;
            }
        }

        public string HsCode
        {
            get
            {
                return hrmnzd_code.Substring(0,4);
            }
        }

        public string Code
        {
            get
            {
                if (item_type==1||item_type==3)
                {
                    return hrmnzd_code;
                }
                else
                {
                    var code = hrmnzd_code.Remove(8, 4);
                    if (!hrmnzd_code.Substring(6, 2).All(c => c == '0'))
                    {
                        return code;
                    }
                    else
                    {
                        if (!hrmnzd_code.Substring(4, 2).All(c => c == '0'))
                        {
                            return hrmnzd_code.Substring(0, 6);
                        }
                       else if (!hrmnzd_code.Substring(2, 2).All(c => c == '0'))
                        {
                            return hrmnzd_code.Substring(0, 4);
                        }
                        else if (!hrmnzd_code.Substring(0, 2).All(c => c == '0'))
                        {
                            return hrmnzd_code.Substring(0, 2);
                        }
                        return hrmnzd_code.Remove(4, 8);
                    }
                }
                //return chpt_code + (main_item_code == "00" ?"" : main_item_code )+ (sub_item_code != "00" ? sub_item_code : "") + (locl_item_code != "00" ? locl_item_code : "");
            }
        }

        public ObservableCollection<Tariffreleaseprocedure> tariffreleaseprocedures { get; set; }
        public ObservableCollection<Tariffdetail> tariffdetails { get; set; }
    }

    public class HarmonizedTarrifResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<HarmonizedTarrif> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }


}
