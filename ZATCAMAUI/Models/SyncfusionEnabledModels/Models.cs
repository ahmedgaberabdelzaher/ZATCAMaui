using System.Globalization;
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


        public DateTime? Abrzu { get; set; }
        public string Gpartz { get; set; }
        public string Abtyp { get; set; }
        public string MadabutFg { get; set; }
        public string OpenliMsg { get; set; } //Mada Payment Message



        public string _CalendarTyp;
        public string CalendarTyp
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

                    if (DueDtC != null)
                    {

                        string formatedDate = string.Format(DueDtC?.ToString("dd/MM/yyyy", new CultureInfo("en-US")));

                        if (CalendarTyp.Equals("G"))
                        {

                            string[] dts1 = formatedDate.Split('/');

                            FormatedDuedate = dts1[0] + " " + UtilityManager.GetMonthName(dts1[1]) + " " + dts1[2];

                        }
                        else if (CalendarTyp.Equals("H"))
                        {

                            string[] dts1 = formatedDate.Split('/');

                            FormatedDuedate = dts1[0] + " " + UtilityManager.GetMonthNameHijri(dts1[1]) + " " + dts1[2];

                        }

                    }


                    App.ACCalType = _CalendarTyp;
                }
            }
        }



        public string Abtypt { get; set; }
        public DateTime? Abrzo { get; set; }
        public string Langz { get; set; }
        public string Incotyp { get; set; }
        public string Incotext { get; set; }
        public string IcrStatus { get; set; }
        public string Sopbel { get; set; }
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
        public string _dueDT;
        public string DueDt
        {
            get
            {
                return _dueDT;
            }
            set
            {
                _dueDT = value;
                if (_dueDT != null)
                {
                    if (_dueDT.Contains("T"))
                    {
                        string[] _dueDate = new string[2];
                        _dueDate = _dueDT.Split('T');
                        DueDate = _dueDate[0];
                    }
                }
            }
        }


        public string FormatedDuedate { get; set; }




        public DateTime? _dueDtC;
        public DateTime? DueDtC
        {
            get
            {
                return _dueDtC;
            }
            set
            {
                _dueDtC = value;
                if (_dueDtC != null)
                {
                    if (CalendarTyp != null)
                    {

                        string formatedDate = string.Format(_dueDtC?.ToString("dd/MM/yyyy", new CultureInfo("en-US")));

                        if (CalendarTyp.Equals("G"))
                        {

                            string[] dts1 = formatedDate.Split('/');

                            FormatedDuedate = dts1[0] + " " + UtilityManager.GetMonthName(dts1[1]) + " " + dts1[2];

                        }
                        else if (CalendarTyp.Equals("H"))
                        {

                            string[] dts1 = formatedDate.Split('/');

                            FormatedDuedate = dts1[0] + " " + UtilityManager.GetMonthNameHijri(dts1[1]) + " " + dts1[2];

                        }


                        // Month = Convert.ToDateTime(_dueDate).ToString("MMM", new CultureInfo("en-US"));
                        FormatedSingleDueDate = Convert.ToDateTime(_dueDtC).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        DueDateDateTime = Convert.ToDateTime(_dueDtC);
                    }
                    //else {


                    //    string formatedDate = string.Format(_dueDtC?.ToString("dd/MM/yyyy", new CultureInfo("en-US")));

                    //    string[] dts1 = formatedDate.Split('/');

                    //    FormatedDuedate = dts1[0] + " " + UtilityManager.GetMonthName(dts1[1]) + " " + dts1[2];

                    //}


                }




                //    }


            }
        }

        //DueDate
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
        public string Fbtyp { get; set; }
        public string FbtText { get; set; }
        public string Txt50 { get; set; }
        public string Persl { get; set; }
        public string Amount { get; set; }
        public string Waers { get; set; }
        private string _dueDate;

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

        public string DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
                if (_dueDate != null)
                {
                    Day = Convert.ToDateTime(_dueDate).ToString("dd", new CultureInfo("en-US"));
                    if (App.IsArabic)
                    {
                        Month = UtilityManager.GetMonthName(Convert.ToDateTime(_dueDate).ToString("MMMM", new CultureInfo("en-US")));
                    }
                    else
                    {
                        Month = Convert.ToDateTime(_dueDate).ToString("MMM", new CultureInfo("en-US"));
                    }
                    // Month = Convert.ToDateTime(_dueDate).ToString("MMM", new CultureInfo("en-US"));
                    FormatedSingleDueDate = Convert.ToDateTime(_dueDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    DueDateDateTime = Convert.ToDateTime(_dueDate);
                    //if (App.IsArabic)
                    //{
                    //    string date = Convert.ToDateTime(_dueDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    //    FormatedSingleDueDate = UtilityManager.ToArabicDate(date);
                    //    DueDateDateTime = Convert.ToDateTime(_dueDate);
                    //}
                    //else
                    //{
                    //    FormatedSingleDueDate = Convert.ToDateTime(_dueDate).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    //    DueDateDateTime = Convert.ToDateTime(_dueDate);
                    //}
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
                if (_isUnSubmittedReturn != null)
                {
                    if (_isUnSubmittedReturn == true)
                    {
                        StatusImage = "sf_ic_Overdue_Returns_Commitments.png";
                        TaxPeriod = Txt50;
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
