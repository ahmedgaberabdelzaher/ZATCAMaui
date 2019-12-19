using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class MyBills
    {
        public string Abtypt { get; set; } //TaxType
        public string VTRE2 { get; set; } //SadadPaymentNumber
        public string BETRW { get; set; } //DueAmount
        public string FAEDN { get; set; } //DueDate
        public string StatusImage { get; set; }


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
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 1))
                {
                    StatusImage = "ic_loading.png";
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 2))
                {
                    StatusImage = "ic_money.png";
                }
            }
        }
            
    }
}
