using GAZT.Manager;
using System;
using System.Globalization;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
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
        public string BETRW
        {
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
                    TestDueAmount = UtilityManager.GetCommaSeparatedAmount(positiveMoney.ToString());
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

        private bool _isPartiallyPaidVisibile = false;
        public bool IsPartiallyPaidVisibile
        {
            get
            {
                if (Status == "I")
                {
                    _isPartiallyPaidVisibile = true;
                    return _isPartiallyPaidVisibile;
                }
                else
                {
                    _isPartiallyPaidVisibile = false;
                    return _isPartiallyPaidVisibile;
                }
            }
            set
            {
                _isPartiallyPaidVisibile = value;
            }
        }

        public bool IsPeriodVisible { get; set; }
        

        private string _paidamt = string.Empty;
        public string Paidamt
        {
            get
            {
                return _paidamt;
            }
            set
            {
                _paidamt = value;
                if (!string.IsNullOrEmpty(_paidamt))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(_paidamt);
                    decimal positiveMoney = d;
                    positiveMoney.ToString(format);  //will return $24,508,975.94
                    TotalPaidAmt = UtilityManager.GetCommaSeparatedAmount(positiveMoney.ToString());

                  
                }
            }
        }

        private string _remainingAmount = string.Empty;
        public string RemainingAmount
        {
            get
            {
                return _remainingAmount;
            }
            set
            {
                _remainingAmount = value;
               
            }
        }

        public string _totalRemainingAmount = String.Empty;
        public string TotalRemainingAmount
        {
            get
            {
                return _totalRemainingAmount;
            }
            set
            {
                _totalRemainingAmount = value;
            }
        }

        public string _totalPaidAmt = String.Empty;
        public string TotalPaidAmt
        {
            get
            {
                return _totalPaidAmt;
            }
            set
            {
                _totalPaidAmt = value;
            }
        }

        private string _Period;
        public string Period
        {
            get
            {
                return _Period;
            }
            set
            {
                _Period = value;
                if (!string.IsNullOrEmpty(_Period))
                {


                    string[] partsofperid = _Period.Split('-');
                    {
                        PeriodPart1 = partsofperid[0];
                        PeriodPart2 = partsofperid[1];



                        if (CalTyp != null)
                        {

                            if (PeriodPart1 != null)
                            {
                                string year = PeriodPart1.Substring(0, 4);
                                string month = PeriodPart1.Substring(4, 2);
                                string day = PeriodPart1.Substring(6, 2);
                                if (CalTyp.Equals("G"))
                                {
                                    PeriodPart1 = UtilityManager.FormatAccordingToDeviceForVAT(day + "/" + month + "/" + year);
                                }
                                else
                                {
                                    PeriodPart1 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                }

                            }
                            if (PeriodPart2 != null)
                            {
                                string year = PeriodPart2.Substring(0, 4);
                                string month = PeriodPart2.Substring(4, 2);
                                string day = PeriodPart2.Substring(6, 2);
                                if (CalTyp.Equals("G"))
                                {
                                    PeriodPart2 = UtilityManager.FormatAccordingToDeviceForVAT(day + "/" + month + "/" + year);
                                }
                                else
                                {
                                    PeriodPart2 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                }

                            }

                        }

                    }
                }
            }
        }


        private string _PeriodPart1;
        public string PeriodPart1 { get; set; }



        private string _PeriodPart2;
        public string PeriodPart2 { get; set; }
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
                            if (App.IsArabic)
                            {
                                string[] dts = _Faednar.Split('/');
                                FormatedFaedn = UtilityManager.FormatAccordingToDeviceHijriEnglish(dts[2] + "-" + dts[1] + "-" + dts[0]);
                            }
                            else
                            {
                                FormatedFaedn = UtilityManager.FormatAccordingToDeviceHijriEnglish(_Faednar);
                            }


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
        public DateTime _faedn;
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
                        string[] dts = FormatedFaedn.Split('-');
                        string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        FormatedFaedn = date;
                    }

                }
            }
        }
        public string FormatedFaedn { get; set; }
        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if (_status == Enum.GetName(typeof(BillStatus), 0))
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
