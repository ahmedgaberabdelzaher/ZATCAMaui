using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GAZT.Models
{
    public class MyBills
    {
        public string Abtypt { get; set; } //TaxType
        public string VTRE2 { get; set; } //SadadPaymentNumber


        private string _BETRW = string.Empty;

        public string BETRW { 
            get
            {
                return _BETRW;
            }
            set
            {
                _BETRW = value;
                if (!string.IsNullOrEmpty(_BETRW))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";


                    decimal d = Convert.ToDecimal(_BETRW);
                    decimal positiveMoney = d;
                    positiveMoney.ToString(format);  //will return $24,508,975.94
                    TestDueAmount = positiveMoney.ToString();
                }


            } 
        } //DueAmount


        public string _TestDueAmount = String.Empty;
        public string TestDueAmount
        {
            get 
            {
                return _TestDueAmount;
            }
            set
            {


                _TestDueAmount = value;

            }

        }
        public string Txt30 { get; set; }
        public string FAEDN { get; set; } //DueDate
        public string StatusImage { get; set; }
        public string Colorcode { get; set; }

        private string _status = string.Empty;
        public string Status {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if(_status==Enum.GetName(typeof(BillStatus),0))
                {
                    StatusImage = "ic_check_circle.png";
                    Colorcode = "#006450";
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 1))
                {
                    StatusImage = "ic_loading.png";
                    Colorcode = "#F36C21";
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 2))
                {
                    StatusImage = "ic_money.png";
                    Colorcode = "#944E23";
                }
            }
        }


       

            
    }
}
