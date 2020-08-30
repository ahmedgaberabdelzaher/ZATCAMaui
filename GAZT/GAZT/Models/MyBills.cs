using GAZT.Manager;
using System;
using System.Globalization;

namespace EGAZT.Models
{
    public class MyBills
    {
        public string Abtypt { get; set; } //TaxType
        public string VTRE2 { get; set; } //SadadPaymentNumber
        public string _cal_typ = String.Empty;
        public string CalTyp
        {
            get
            {

                return _cal_typ;
            }
            set
            {
                _cal_typ = value;
            }
        }
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
                    TestDueAmount =  UtilityManager.GetCommaSeparatedAmount(positiveMoney.ToString());
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
        public string Period { get; set; }
        public string Txt30 { get; set; }
        public string _Faednar;
        public string Faednar
        {
            get { return _Faednar; }

            set
            {
                _Faednar = value;
                if (_Faednar != null)
                {
                    if (CalTyp != null)
                    {
                        if (!CalTyp.Equals("G"))
                        {
                            FormatedFaedn = UtilityManager.FormatAccordingToDeviceHijriEnglish(_Faednar);
                        }
                      
                    }
                   
                }
            } 
        } //DueDate
                                            // public string Faedn { get; set; } //DueDate
        public string FormattedFaednar { get; set; } //DueDate
        public string StatusImage { get; set; }
        public string Colorcode { get; set; }
       // FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        public DateTime _faedn ;
        public DateTime Faedn
        {
            get
            {

                return _faedn;
            }
            set
            {
                _faedn = value;
                if (_faedn != null)
                {
                    if (CalTyp.Equals("G"))
                    {
                        FormatedFaedn = _faedn.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                    
                }
            }
        }
        public string FormatedFaedn { get; set; }
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
                    Colorcode = "#D99A29";
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 2))
                {
                    StatusImage = "ic_money.png";
                    Colorcode = " #AA0C19";
                   
                }
            }
        }

        //public string _cal_typ = String.Empty;
        //public string CalTyp
        //{
        //    get
        //    {
                
        //        return _cal_typ;
        //    }
        //    set
        //    {
        //        _cal_typ = value;
        //    }
        //}
    }
}
