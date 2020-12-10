using EGAZT.Models;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class ICRListSet
    {
        public string Incotext { get; set; }//VATReturnForm
        public string Persl { get; set; }//TaxPeriodCode
        public string Euser { get; set; }
        public string _TaxPeriod;
        public string TaxPeriod
        {
            get
            {
                return _TaxPeriod;
            }
            set
            {
                _TaxPeriod = value;
                if(!string.IsNullOrEmpty(_TaxPeriod))
                {
                    FormatTaxPeriod = _TaxPeriod;
                    //if (App.IsArabic)
                    //{
                    //    FormatTaxPeriod = UtilityManager.ConvertNumerals(_TaxPeriod);
                    //}
                    //else
                    //{
                    //    FormatTaxPeriod = _TaxPeriod;
                    //}
                }
            }
        }//TaxPeriodDescription
        public string FormatTaxPeriod { get; set; }
        public string _Fbnum;
        public string Fbnum { 
            get
            {
               return _Fbnum;
            }
            set
            {
                _Fbnum = value;
                FormBundleNumber = _Fbnum;
                //if (App.IsArabic)
                //    {
                //        FormBundleNumber = UtilityManager.ConvertNumerals(_Fbnum);
                //    }
                //    else
                //    {
                //        FormBundleNumber = _Fbnum;
                //    }
            }
        }
        public string FormBundleNumber { get; set; }
        private string _txt50;
        public string Txt50 {
            get
            {
                return _txt50;
            }
            set
            {
                _txt50 = value;
                if (_txt50 != null)
                {
                    FormatedDate=UtilityManager.englishDateConversion(_txt50);
                }
            }
        }//ReturnPeriod
        private string _formatedDate;
        public string FormatedDate
        {
            get
            {
                return _formatedDate;
            }
            set
            {
                _formatedDate = value;
            }
        }
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
        private string _dueDate;
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
        public string _dueDT;
        public string DueDt {
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
        private string _status;//StatusCode
        public string Status {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                if(!string.IsNullOrEmpty(_status))
                {
                        //For Border Colour
                        if (string.Equals(_status, "E0001") || string.Equals(_status, "E0013") || string.Equals(_status, "E0056"))
                        {
                            BorderColour = "#bfbebe";
                        }
                        else if (string.Equals(_status, "E0006") || string.Equals(_status, "E0045") || string.Equals(_status, "E0090"))
                        {
                            BorderColour = "#005e4b";
                            StatusImage = "ic_Paid.png";
                        }
                        else if (string.Equals(_status, "E0020") || string.Equals(_status, "E0055") || string.Equals(_status, "E0057") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089"))
                        {
                            BorderColour = "#c49b2d";
                            StatusImage = "ic_Check_golden.png";
                        }
                        //For Image
                        if (string.Equals(_status, "E0001"))
                        {
                            StatusImage = "ic_Check_Gray.png";
                        }
                        else if (string.Equals(_status, "E0013") || string.Equals(_status, "E0056"))
                        {
                            StatusImage = "ic_save_Gray.png";
                        }
                        else if (string.Equals(_status, "E0057"))
                        {
                            StatusImage = "ic_save_golden.png";
                        }
                }
            }
     }
        private string _statusTxt;
        public string StatusTxt
        {
            get
            {
                return _statusTxt;
            }
            set
            {
                _statusTxt = value;
                //if (!string.IsNullOrEmpty(_statusTxt))
                //{
                //    //For Border Colour
                //    if (string.Equals(_statusTxt, "To be filled") || string.Equals(_statusTxt, "In Draft") || string.Equals(_statusTxt, "Draft in Amendment by Taxpayer"))
                //    {
                //        BorderColour = "#bfbebe";
                //    }
                //    else if (string.Equals(_statusTxt, "Amended") || string.Equals(_statusTxt, "Billed") || string.Equals(_statusTxt, "GSTC – Escalation Completed"))
                //    {
                //        BorderColour = "#005e4b";
                //        StatusImage = "ic_Paid.png";
                //    }
                //    else if (string.Equals(_statusTxt, "In Additional Clarif. with TP") || string.Equals(_statusTxt, "Submitted") || string.Equals(_statusTxt, "Draft in Amendment by GAZT") || string.Equals(_statusTxt, "Amendment Submitted") || string.Equals(_statusTxt, "In Supervisor's Pool") || string.Equals(_statusTxt, "For Supervisor's Review") || string.Equals(_statusTxt, "For Officer's Review") || string.Equals(_statusTxt, "GSTC – Escalation In Process"))
                //    {
                //        BorderColour = "#c49b2d";
                //        StatusImage = "ic_Check_golden.png";
                //    }
                //    //For Image
                //    if (string.Equals(_statusTxt, "To be filled"))
                //    {
                //        StatusImage = "ic_Check_Gray.png";
                //    }
                //    else if (string.Equals(_statusTxt, "In Draft") || string.Equals(_statusTxt, "Draft in Amendment by Taxpayer"))
                //    {
                //        StatusImage = "ic_save_Gray.png";
                //    }
                //    else if (string.Equals(_statusTxt, "Draft in Amendment by GAZT"))
                //    {
                //        StatusImage = "ic_save_golden.png";
                //    }
                //}
            }
        }
        //StatusDescription
        public string Fbguid { get; set; }//Fbguidto get information about ICR
        private string _borderColour;
        public string BorderColour
        {
            get
            {
                return _borderColour;
            }
            set
            {
                _borderColour = value;
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
    public class ICR
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string UserTin { get; set; }
        public string PortalUsr { get; set; }
        public string Lang { get; set; }
        public string Operation { get; set; }
        public string StepNumber { get; set; }
        public string ReturnId { get; set; }
        public string Officer { get; set; }
        public string Gpart { get; set; }
        public string Status { get; set; }
        public string UserTyp { get; set; }
        public string TxnTp { get; set; }
        public string Formproc { get; set; }
        public string Persl { get; set; }
        public object Begda { get; set; }
        public object Endda { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public List<ICRListSet> ICR_LISTSet { get; set; }
        public List<ICRStatus> ICR_STATUSSet { get; set; }
        public ICR()
        {
            ICR_LISTSet = new List<ICRListSet>();
            ICR_STATUSSet = new List<ICRStatus>();
        }
    }
    public class ICRStatus
    {
        public Metadata __metadata { get; set; }
        public string Stsma { get; set; }
        public string Estat { get; set; }
        public string Spras { get; set; }
        public string Txt04 { get; set; }
        public string Txt30 { get; set; }
        public string Ltext { get; set; }
    }
}
