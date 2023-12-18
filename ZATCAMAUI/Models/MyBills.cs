using Newtonsoft.Json;
using System.Globalization;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Models
{

    public class MyBills
    {
        //public string Abtypt { get; set; } //TaxType
        public string VTRE2 { get; set; } //SadadPaymentNumber
        public string MadabutFg { get; set; } //Mada Payment
        public string OpenliMsg { get; set; } //Mada Payment Message
        public string Persl { get; set; } //Mada Payment

        public string _cal_typ = string.Empty;
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

        public string _blart = string.Empty;
        public string Blart
        {
            get
            {

                return _blart;
            }
            set
            {
                _blart = value;
                if (Blart == "IF")
                {
                    BillTitle = AppResources.AcPenality;
                }

            }
        }

        public string _abtypt = string.Empty;
        public string Abtypt
        {
            get
            {

                return _abtypt;
            }
            set
            {
                _abtypt = value;
                if (Blart == "IF")
                {
                    BillTitle = AppResources.AcPenality;
                }

                else
                {
                    BillTitle = _abtypt;
                }


            }
        }

        private string _fbnum;
        public string Fbnum
        {
            get
            {
                return _fbnum;
            }
            set
            {
                _fbnum = value;
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
        public string _TestDueAmount = string.Empty;
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


/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
        public bool IsPeriodVisible { get; set; }
        

        private string _paidamt = string.Empty;
After:
        public bool IsPeriodVisible { get; set; }


        private string _paidamt = string.Empty;
*/
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

        public string _totalRemainingAmount = string.Empty;
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

        public string _totalPaidAmt = string.Empty;
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

        public string _billTitle = string.Empty;
        public string BillTitle
        {
            get
            {
                return _billTitle;
            }
            set
            {
                _billTitle = value;
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

                                    //try{
                                    //    CultureInfo arCI = new CultureInfo("en-US");
                                    //    FormatedFromTaxPeriod = DateTime.ParseExact(year + "/" + month + "/" + day, "yyyy/MM/dd", arCI.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                    //}
                                    //catch (Exception) {

                                    //}



                                }


                                else
                                {
                                    PeriodPart1 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                    //try
                                    //{
                                    //    CultureInfo arCI = new CultureInfo("ar-SA");
                                    //    FormatedFromTaxPeriod = DateTime.ParseExact(year + "/" + month + "/" + day, "yyyy/MM/dd", arCI.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}

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
                                    //try
                                    //{
                                    //    CultureInfo arCI = new CultureInfo("en-US");
                                    //    FormatedToTaxPeriod = DateTime.ParseExact(year + "/" + month + "/" + day, "yyyy/MM/dd", arCI.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}

                                }
                                else
                                {
                                    PeriodPart2 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                    //try
                                    //{
                                    //    CultureInfo arCI = new CultureInfo("ar-SA");
                                    //    FormatedToTaxPeriod = DateTime.ParseExact(year + "/" + month + "/" + day, "yyyy/MM/dd", arCI.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}

                                }

                            }

                        }

                    }
                }
            }
        }



        public string PeriodPart1 { get; set; }
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
                        ACSFormatedFaedn = date;
                    }
                    else
                    {

                        ACSFormatedFaedn = _faedn.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        string[] dts = ACSFormatedFaedn.Split('-');
                        string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        ACSFormatedFaedn = date;
                    }


                }
            }
        }


        [JsonIgnore]
        public string StatusText { get; set; }

        public string FormatedFaedn { get; set; }
        public string ACSFormatedFaedn { get; set; }

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
                    Colorcode = "{StaticResource Primary}";
                    StatusText = AppResources.Paid;

                }
                else if (_status == Enum.GetName(typeof(BillStatus), 1))
                {
                    StatusImage = "ic_loading.png";
                    Colorcode = "{StaticResource Secondary}";
                    StatusText = AppResources.PartiallyPaid;
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 2))
                {
                    StatusImage = "ic_money.png";
                    Colorcode = " #e84941";
                    StatusText = AppResources.UnPaid;
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
