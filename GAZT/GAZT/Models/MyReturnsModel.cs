using EGAZT.Models;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class MyReturnsModel
    {
    }
    public class MyReturnsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class MyReturnsResult
    {
        public Metadata __metadata { get; set; }
        public string Gpart { get; set; }
        public string Lang { get; set; }
        public string Vkont { get; set; }
        public string Cokey { get; set; }
        public string Augrd { get; set; }
        public string Vtref { get; set; }
        public string Persl { get; set; }
        public string TaxPeriod { get; set; }
        public string Fbtyp { get; set; }
        public string FbtText { get; set; }
        public string Fbsta { get; set; }
        public string Fbust { get; set; }
        private string _CalendarTyp;
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
                    if (!_CalendarTyp.Equals("G"))
                    {

                        if (AbrzuC!=null)
                        {

                            string[] dts = AbrzuC.Split('/');
                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                            FormatedAbrzu = GAZT.Manager.UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                        }
                        if (AbrzoC!=null)
                        {
                            string[] dts = AbrzoC.Split('/');

                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                            FormatedAbrzo = GAZT.Manager.UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                        }
                    }
                }
              
            }
            } 
        public string Msg { get; set; } 
        public bool Open { get; set; }
        public string Due 
        {get;set;}

        private string _StatusTxt;
        public string StatusTxt
        {
            get
            {
                return _StatusTxt;
            }
            set
            {
                _StatusTxt = value;
                try
                {
                    if (_StatusTxt != null)
                    {
                        if (_StatusTxt == "Submitted")
                        {
                            StatusMessage = "submitted";
            
                        }
                        if (_StatusTxt == "Non Submitted")
                        {
                                StatusMessage = "unsubmitted";
                       
                            if (Due != null)
                            { 
                            if (Due.Equals('X'))
                            {
                                StatusMessage = "overdue";
                               
                                }

                            }
                        }
                    }
                }
    
                catch(Exception ex)
                {

                }
            }
        }
   
       public string StatusMessage
        {
            get;set;
        }
        public string Incotyp { get; set; }
        public string Incotext { get; set; }
        private DateTime _abrzu;
        public DateTime Abrzu
        {
            get
            {
                return _abrzu;
            }
            set
            {
                _abrzu = value;
                if (_abrzu != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (CalendarTyp.Equals("G"))
                        {

                            FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts = FormatedAbrzu.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                            FormatedAbrzu = date;
                        }

                    }  
                }
            }
        }
        private string _AbrzuC;
        public string AbrzuC
        {
            get
            {
                return _AbrzuC;
            }
            set
            {
                _AbrzuC = value;
                if (_AbrzuC != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (!CalendarTyp.Equals("G"))
                        {
                            //string[] dts = null;

                            string[] dts = _AbrzuC.Split('/');
                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                            FormatedAbrzu = GAZT.Manager.UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                        }
                    }
                }
            }
        }
        private DateTime _abrzo;
        public DateTime Abrzo
        {
            get
            {
                return _abrzo;
            }
            set
            {
                _abrzo = value;
                if (_abrzo != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (CalendarTyp.Equals("G"))
                        {

                            FormatedAbrzo = _abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts = FormatedAbrzo.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                            FormatedAbrzo = date;
                        }
                        
                    }
                    
                }
            }
        }
        private string _AbrzoC;
        public string AbrzoC
        {
            get
            {
                return _AbrzoC;
            }
            set
            {
                _AbrzoC = value;
                if (_AbrzoC != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (!CalendarTyp.Equals("G"))
                        {
                            //dts = null;

                            string[] dts = _AbrzoC.Split('/');
                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                            FormatedAbrzo = GAZT.Manager.UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                        }
                    }
                }
                }
        }

        public string SadadDoc1 { get; set; }
        public string SadadDoc2 { get; set; }
        private DateTime _dueDt;
        public DateTime DueDt
        { 
            get
            {
                return _dueDt;
            }
            set
            {
                _dueDt = value;
                if (_dueDt != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (CalendarTyp.Equals("G"))
                        {
                            FormatedSingleDueDate = _dueDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts = FormatedSingleDueDate.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                            FormatedSingleDueDate = date;
                        }
                    }
                    
                    
                }
            }
        }
        public string Stat { get; set; }
        public string RetStatTxt { get; set; }
        private string _dueDTC;
        public string DueDtC 
        {
            get 
            {
                return _dueDTC;
            }
            set 
            {
                _dueDTC = value;
                if (_dueDTC != null)
                {
                    if (CalendarTyp != null)
                    {
                        if (!CalendarTyp.Equals("G"))
                        {
                            string[] dts = null;
                           
                                dts = _dueDTC.Split('/');
                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                                FormatedSingleDueDate = GAZT.Manager.UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                        }
                    }
                    
                }
                
            } 
        }
   
        public string Sortperiod { get; set; }
        public string TaxType { get; set; }
        public string Fbnum { get; set; }
        public string Fbguid { get; set; }
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
        private string _formatedAbrzu;
        public string FormatedAbrzu
        {
            get
            {
                return _formatedAbrzu;
            }
            set
            {
                _formatedAbrzu = value;
            }
        }
        private string _formatedAbrzo;
        public string FormatedAbrzo
        {
            get
            {
                return _formatedAbrzo;
            }
            set
            {
                _formatedAbrzo = value;
            }
        }
    }
    public class MyReturnsD
    {
        public List<MyReturnsResult> results { get; set; }
    }
    public class MyReturnsRootObject
    {
        public MyReturnsD d { get; set; }
    }
    public class ReturnTypes
    {
        public string TaxType { get; set; }
        public string Id { get; set; }
    }
    public class ChipModel
    {
        public string TemplateType { get; set; }
        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }
  
    }
}
