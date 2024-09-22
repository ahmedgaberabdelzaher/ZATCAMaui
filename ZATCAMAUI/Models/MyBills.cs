using Newtonsoft.Json;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Mangers;
namespace ZATCAMAUI.Models
{

    public class MyBills
    {
        [JsonProperty("contractNumber")]
        public string VTRE2 { get; set; } //SadadPaymentNumber
        [JsonProperty("MADAButton")]
        public string MadabutFg { get; set; } //Mada Payment
        public string OpenliMsg { get; set; } //Mada Payment Message
        public string Persl { get; set; } //Mada Payment
        public string Opbel { get; set; } //CR1265
        [JsonProperty("paymentStatus")]
        public string PymtStatus { get; set; }


        public Color StatusTextColor { get; set; }
        public Color StatusBackGColor { get; set; }

        private string _calTyp = "";
        [JsonProperty("calendarType")]
        public string CalTyp
        {
            get { return _calTyp; }
            set
            {
                _calTyp = value;
                if (!string.IsNullOrEmpty(Period))
                {
                    string[] partsofperid = Period.Split('-');
                    {
                        PeriodPart1 = partsofperid[0];
                        PeriodPart2 = partsofperid[1];

                        if (_calTyp != null)
                        {

                            if (PeriodPart1 != null)
                            {
                                string year = PeriodPart1.Substring(0, 4);
                                string month = PeriodPart1.Substring(4, 2);
                                string day = PeriodPart1.Substring(6, 2);
                                if (_calTyp.Equals("Gregorian"))
                                {
                                    PeriodPart1 = UtilityManager.FormatAccordingToDeviceForVAT(day + "/" + month + "/" + year);
                                }
                                else if (_calTyp.Equals("Hirji"))
                                {
                                    //if (App.IsArabic)
                                    //{
                                    //    PeriodPart1 = UtilityManager.FormatAccordingToDeviceHijriArabic(day + "/" + month + "/" + year);
                                    //}
                                    //else
                                    //{
                                        PeriodPart1 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                    //}
                                }

                            }
                            if (PeriodPart2 != null)
                            {
                                string year = PeriodPart2.Substring(0, 4);
                                string month = PeriodPart2.Substring(4, 2);
                                string day = PeriodPart2.Substring(6, 2);
                                if (_calTyp.Equals("Gregorian"))
                                {
                                    PeriodPart2 = UtilityManager.FormatAccordingToDeviceForVAT(day + "/" + month + "/" + year);
                                }
                                else if (_calTyp.Equals("Hirji"))
                                {
                                    //if (App.IsArabic)
                                    //{
                                    //    PeriodPart2 = UtilityManager.FormatAccordingToDeviceHijriArabic(day + "/" + month + "/" + year);
                                    //}
                                    //else
                                    //{
                                        PeriodPart2 = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
                                    //}
                                }

                            }

                        }

                    }
                }

                if (Faednar != null)
                {
                    if (CalTyp != null)
                    {
                        if (!CalTyp.Equals("Gregorian"))
                        {
                            FormatedFaedn = UtilityManager.FormatAccordingToDeviceHijriEnglish(Faednar);
                        }
                        else
                        {
                            FormatedFaedn = UtilityManager.FormatAccordingToDeviceForVAT(Faednar);
                        }
                        FormatedFaedn = FormatedFaedn.Replace("T00:00:00", "");
                    }
                }
            }
        }
        public string _blart = String.Empty;
        [JsonProperty("documentType")]
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

        public string _abtypt = String.Empty;
        [JsonProperty("revenueTypeDescription")]
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

        private string _fbnum;//formbundle no
        [JsonProperty("formBundleNumber")]
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
        [JsonProperty("amount")]
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
                    decimal d = decimal.Parse(_BETRW, NumberStyles.Number, CultureInfo.InvariantCulture);
                    // decimal d = Convert.ToDecimal(_BETRW);
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
                if (Status == "Partially Paid")
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
        [JsonProperty("paidAmount")]
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
                    decimal d = decimal.Parse(_paidamt, NumberStyles.Number, CultureInfo.InvariantCulture);
                  //  decimal d = Convert.ToDecimal(_paidamt);
                    decimal positiveMoney = d;
                    positiveMoney.ToString(format);  //will return $24,508,975.94
                    TotalPaidAmt = UtilityManager.GetCommaSeparatedAmount(positiveMoney.ToString());

                }
            }
        }

        private string _remainingAmount = string.Empty;
        [JsonProperty("remainingAmount")]
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

        public string _billTitle = String.Empty;
        [JsonProperty("transactionDescription")]
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
        [JsonProperty("period")]
        public string Period
        {
            get
            {
                return _Period;
            }
            set
            {
                _Period = value;
            }
        }



        public string PeriodPart1 { get; set; }
        public string PeriodPart2 { get; set; }
        [JsonProperty("periodDescription")]
        public string Txt30 { get; set; }

        public string _Faednar;
        [JsonProperty("netDueDateTime")]
        public string Faednar
        {
            get { return _Faednar; }

            set
            {
                _Faednar = value;
               
            }
        } //DueDate
        public string FormattedFaednar { get; set; } //DueDate
        public string StatusImage { get; set; }
        public string Colorcode { get; set; }


        public string _faedn;
        public string Faedn
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
                        FormatedFaedn = _faedn;
                        string formattedDate = ConvertDate(_faedn, "dd/MM/yyyy");
                        ACSFormatedFaedn = formattedDate;

                        // FormatedFaedn = _faedn.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                        //ACSFormatedFaedn = FormatedFaedn;


                        //ReadOnlySpan<char> dateSpan = _faedn.AsSpan();
                        if (DateTime.TryParse(_faedn, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDate))
                        {
                            faednDate = myDate;
                        }
                      

                    }
                    else
                    {

                      // ACSFormatedFaedn = _faedn;
                        
                           
                        ACSFormatedFaedn = ConvertDate(_faedn, "dd/MM/yyyy");
                        if (DateTime.TryParse(_faedn, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDate))
                        {
                            faednDate = myDate;
                        }




                        // ACSFormatedFaedn = _faedn.ToString("dd/MM/yyyy", new CultureInfo("en-US"));

                    }
                }
            }
        }

        public DateTime faednDate { get; set; }

        [JsonIgnore]
        public string StatusText { get; set; }

        public string FormatedFaedn { get; set; }
        [JsonProperty("netDueDate")]
        public string ACSFormatedFaedn { get; set; }

        private string _status = string.Empty;
        [JsonProperty("status")]
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if (_status == "Paid")
                {
                    StatusTextColor = (Color)App.Current.Resources["Success"];
                    StatusBackGColor = (Color)App.Current.Resources["SuccessBg"];
                    StatusText = PymtStatus;

                }
                else if (_status == "Partially Paid")
                {
                    StatusTextColor = (Color)App.Current.Resources["Partial"];
                    StatusBackGColor = (Color)App.Current.Resources["PartialBg"];
                    StatusText = PymtStatus;
                }
                else if (_status == "Open")
                {
                    StatusTextColor = (Color)App.Current.Resources["Error"];
                    StatusBackGColor = (Color)App.Current.Resources["ErrorBg"];
                    StatusText = PymtStatus;
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 3))
                {
                    StatusTextColor = (Color)App.Current.Resources["color"];
                    StatusBackGColor = (Color)App.Current.Resources["gray"];
                    StatusText = PymtStatus;
                }
                else if (_status == Enum.GetName(typeof(BillStatus), 4))
                {
                    StatusTextColor = (Color)App.Current.Resources["color"];
                    StatusBackGColor = (Color)App.Current.Resources["gray"];
                    StatusText = PymtStatus;
                }
            }

        }
        private string ConvertDate(string inputDateString, string type)
        {
            // Parse the input date string to a DateTime object
            DateTime inputDate = DateTime.Parse(inputDateString);

            // Convert the DateTime object to the desired format "yyyy-MM-dd"
            string outputDateString = inputDate.ToString(type);

            // Return the formatted date string
            return outputDateString;
        }
    }

    //public class PendingBills
    //{
    //    public string formBundleGUID { get; set; }
    //    public string serialNumber { get; set; }
    //    public string paidInstallmentAmount { get; set; }
    //    public string contractNumber { get; set; }
    //    public string transactionDescription { get; set; }
    //    private string _status = string.Empty;
    //    public string status
    //    {
    //        get
    //        {
    //            return _status;
    //        }
    //        set
    //        {
    //            _status = value;
    //            if (_status == Enum.GetName(typeof(BillStatus), 0))
    //            {
    //                StatusTextColor = (Color)App.Current.Resources["Success"];
    //                StatusBackGColor = (Color)App.Current.Resources["SuccessBg"];
    //                StatusText = paymentStatus;

    //            }
    //            else if (_status == Enum.GetName(typeof(BillStatus), 1))
    //            {
    //                StatusTextColor = (Color)App.Current.Resources["Partial"];
    //                StatusBackGColor = (Color)App.Current.Resources["PartialBg"];
    //                StatusText = paymentStatus;
    //            }
    //            else if (_status == Enum.GetName(typeof(BillStatus), 2))
    //            {
    //                StatusTextColor = (Color)App.Current.Resources["Error"];
    //                StatusBackGColor = (Color)App.Current.Resources["ErrorBg"];
    //                StatusText = paymentStatus;
    //            }
    //            else if (_status == Enum.GetName(typeof(BillStatus), 3))
    //            {
    //                StatusTextColor = (Color)App.Current.Resources["color"];
    //                StatusBackGColor = (Color)App.Current.Resources["gray"];
    //                StatusText = paymentStatus;
    //            }
    //            else if (_status == Enum.GetName(typeof(BillStatus), 4))
    //            {
    //                StatusTextColor = (Color)App.Current.Resources["color"];
    //                StatusBackGColor = (Color)App.Current.Resources["gray"];
    //                StatusText = paymentStatus;
    //            }
    //        }
    //    }
    //    public Color StatusTextColor { get; set; }
    //    public Color StatusBackGColor { get; set; }
    //    [JsonIgnore]
    //    public string StatusText { get; set; }
    //    public string remainingAmount { get; set; }
    //    public string paymentStatusKey { get; set; }
    //    public string paymentStatus { get; set; }
    //    public string periodDescription { get; set; }
    //    public string periodKey { get; set; }
    //    public string period { get; set; }
    //    public string paidAmount { get; set; }
    //    public string messageDescription { get; set; }
    //    public string documentNumber { get; set; }
    //    public string MADAButton { get; set; }
    //    public string formBundleNumber { get; set; }
    //    public string netDueDate { get; set; }
    //    public string netDueDateTime { get; set; }
    //    public string calendarType { get; set; }
    //    public string documentType { get; set; }
    //    public string amount { get; set; }
    //    public string revenueTypeDescription { get; set; }
    //    public string revenueType { get; set; }
    //}

 
}
