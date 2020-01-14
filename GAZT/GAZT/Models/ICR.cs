using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class ICRListSet
    {
        public string Incotext { get; set; }//VATReturnForm
        public string Persl { get; set; }//TaxPeriodCode
        public string TaxPeriod { get; set; }//TaxPeriodDescription
        public string Txt50 { get; set; }//ReturnPeriod
        public string DueDt { get; set; }//DueDate
        
        public string Status { get; set; }//StatusCode

        private string _statusTxt;
        public string StatusTxt {
            get
            {
                return _statusTxt;
            }
            set
            {
                _statusTxt = value;
                if(!string.IsNullOrEmpty(_statusTxt))
                {
                    if(string.Equals(_statusTxt, "To be filled") || string.Equals(_statusTxt, "In Draft") || string.Equals(_statusTxt, "Draft in Amendment by Taxpayer"))
                    {
                        BorderColour = "#bfbebe";
                    }
                    else if(string.Equals(_statusTxt, "Amended") || string.Equals(_statusTxt, "Billed") || string.Equals(_statusTxt, "GSTC – Escalation Completed"))
                    {
                        BorderColour = "#005e4b";
                    }
                    else if(string.Equals(_statusTxt, "In Additional Clarif. with TP")|| string.Equals(_statusTxt, "Submitted") || string.Equals(_statusTxt, "Draft in Amendment by GAZT") || string.Equals(_statusTxt, "Amendment Submitted") || string.Equals(_statusTxt, "In Supervisor's Pool") || string.Equals(_statusTxt, "For Supervisor's Review") || string.Equals(_statusTxt, "For Officer's Review") || string.Equals(_statusTxt, "GSTC – Escalation In Process"))
                    {
                        BorderColour = "#c49b2d";
                    }
                }
            }
        }//StatusDescription

        public string Fbguid { get; set; }//Fbguidto get information about ICR

        private string _borderColour;
        public  string BorderColour
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
