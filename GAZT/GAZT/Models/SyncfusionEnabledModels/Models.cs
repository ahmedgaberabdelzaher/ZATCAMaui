using EGAZT;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class eServiceInfo
    {
        public string eServiceName { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
        public string iConImagePath { get; set; }
        public string OnClickEvents { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OverduePaymentAndUnSubmittedReturn
    {
        public DateTime? Abrzu { get; set; }
        public string Gpartz { get; set; }
        public string Abtyp { get; set; }
        public string MadabutFg { get; set; }
        
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
                        string[] _dueDate = new String[2];
                        _dueDate = _dueDT.Split('T');
                        DueDate = _dueDate[0];
                    }
                }
            }
        }//DueDate
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
        public string CalendarTyp { get; set; }
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
                    Day= Convert.ToDateTime(_dueDate).ToString("dd", new CultureInfo("en-US"));
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
                if(_isUnSubmittedReturn!=null)
                {
                    if(_isUnSubmittedReturn==true)
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
    [Preserve(AllMembers = true)]
    public enum ReturnType
    {
        RtnTot = 0,
        NrtnTot = 1,
        PrtnTot = 2,
        UprtnTot = 3,
        PprtnTot = 4,
        DueIcr = 5
    }
    [Preserve(AllMembers = true)]
    public enum BillType
    {
        PbillsTot = 0,
        UpbillsTot = 1,
        PrbillsTot = 2,
    }
    [Preserve(AllMembers = true)]
    public class FAQ
    {
        #region Properties
        /// <summary>
        /// Gets or sets the question for FAQ.
        /// </summary>
        [DataMember(Name = "question")]
        public string Question { get; set; }
        /// <summary>
        /// Gets or sets the answer for FAQ.
        /// </summary>
        [DataMember(Name = "answer")]
        public List<string> Answer { get; set; }
        #endregion
    }
}
