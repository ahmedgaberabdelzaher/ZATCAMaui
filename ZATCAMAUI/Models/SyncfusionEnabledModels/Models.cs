using System.Globalization;

using Newtonsoft.Json;
using ZATCAMAUI.Core.Mangers;

namespace ZATCAMAUI.Models.SyncfusionEnabledModels
{

    public class ReturnInfo
    {
        public string ReturnTypeName { get; set; }
        public string ReturnCount { get; set; }
        public string iConImagePath { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
        private ReturnType _returnTypeProperty;
        public ReturnType ReturnTypeProperty
        {
            get
            {
                return _returnTypeProperty;
            }
            set
            {
                _returnTypeProperty = value;
            }
        }
    }
    
    public class BillInfo
    {
        private BillType _billTypeProperty;
        public BillType BillTypeProperty
        {
            get
            {
                return _billTypeProperty;
            }
            set
            {
                _billTypeProperty = value;
            }
        }
        public string BillTypeName { get; set; }
        public string BillCount { get; set; }
        public string iConImagePath { get; set; }
        public string BillAmount { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
    }
    
    public class eServiceInfo
    {
        public string eServiceName { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
        public string iConImagePath { get; set; }
        public string OnClickEvents { get; set; }
    }
    
    public class OverduePaymentAndUnSubmittedReturn
    {
        public string _CalendarTyp;
        public string calendarType
        {
            get
            {
                return _CalendarTyp;
            }
            set
            {
                _CalendarTyp = value;
                if (_CalendarTyp != null)
                {
                    if (dueDate != null)
                    {
                        var date = DateTime.Parse(dueDate, new CultureInfo("en-US"));
                        Day = date.Day.ToString();
                        if (calendarType.Equals("G"))
                        {
                            Month = UtilityManager.GetMonthName(date.Month.ToString());
                        }
                        else
                        {
                            Month = UtilityManager.GetMonthNameHijri(date.Month.ToString());
                        }
                        FormatedDuedate = $"{Day}-{Month}-{date.Year}";
                    }


                    App.ACCalType = _CalendarTyp;
                }
            }
        }

        [JsonProperty("message")]
        public string OpenliMsg { get; set; }
        public string revenueType { get; set; }
        public string revenueTaxType { get; set; }
        [JsonProperty("madaPayment")]
        public string MadabutFg { get; set; }
        public string periodStartDate { get; set; }
        public string language { get; set; }
        public string TIN { get; set; }
        public string periodEndDate { get; set; }
        public string inboundCorrespondenceType { get; set; }
        public string inboundCorrespondenceTypeDescription { get; set; }
        public string ICRStatus { get; set; }
        public string sadadBillNumber { get; set; }
        public bool IsFBNumberExist { get; set; }
        public string TaxPeriod { get; set; }
        private string _formatedSingleDueDate;
        public string FormatedSingleDueDate
        {
            get
            {
                return _formatedSingleDueDate;
            }
            set
            {
                _formatedSingleDueDate = value;
            }
        }
        private DateTime _dueDateDateTime;
        public DateTime DueDateDateTime
        {
            get
            {
                return _dueDateDateTime;
            }
            set
            {
                _dueDateDateTime = value;
            }
        }
        public Color ColorCode { get; set; }
        public string _dueDate;
        

        public string FormatedDuedate { get; set; }






        private string _fbnum;
        public string formBundleNumber
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
        public string formBundleType { get; set; }
        public string formBundleDescription { get; set; }
        public string periodDescription { get; set; }
        public string taxTypeDescription { get; set; }
        public string periodkey { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }

        private string _day;
        public string Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
            }
        }
        private string _month;
        public string Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
            }
        }

        public string dueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
                if (dueDate != null)
                {
                    var date = DateTime.Parse(dueDate, new CultureInfo("en-US"));
                    Day = date.Day.ToString();
                    if (App.IsArabic)
                    {
                        Month = UtilityManager.GetMonthName(date.Month.ToString("MMMM", new CultureInfo("en-US")));
                    }
                    else
                    {
                        Month = date.Month.ToString("MMM", new CultureInfo("en-US"));
                    }
                    FormatedSingleDueDate = date.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    DueDateDateTime = date;
                }
            }
        }

        public bool IsPaymentOverdue { get; set; }
        private bool _isUnSubmittedReturn;
        public bool IsUnSubmittedReturn
        {
            get
            {
                return _isUnSubmittedReturn;
            }
            set
            {
                _isUnSubmittedReturn = value;
                if (_isUnSubmittedReturn == true)
                {
                    StatusImage = "sf_ic_Overdue_Returns_Commitments.png";
                    TaxPeriod = periodDescription;
                    if (!string.IsNullOrEmpty(_fbnum))
                    {
                        IsFBNumberExist = true;
                    }
                    else
                    {
                        IsFBNumberExist = false;
                    }
                }
                else
                {
                    StatusImage = "sf_ic_Unpaid_Commitments.png";
                }
            }
        }
        private string _statusImage;
        public string StatusImage
        {
            get
            {
                return _statusImage;
            }
            set
            {
                _statusImage = value;
            }
        }
    }
    
    public enum ReturnType
    {
        RtnTot = 0,
        NrtnTot = 1,
        PrtnTot = 2,
        UprtnTot = 3,
        PprtnTot = 4,
        DueIcr = 5
    }
    
    public enum BillType
    {
        PbillsTot = 0,
        UpbillsTot = 1,
        PrbillsTot = 2,
    }
}
