using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GAZT.Models
{
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
        public string CalendarTyp { get; set; }
        public string StatusTxt { get; set; }
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
                    FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

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
                    FormatedAbrzo = _abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

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
                    FormatedSingleDueDate = _dueDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                   
                }

            }
        }
        public string Stat { get; set; }
        public string RetStatTxt { get; set; }
        public string DueDtC { get; set; }
        public string Due { get; set; }
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
}
