using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GAZTeServicesBusinessLibrary
{
    public class TaxPayerProfile
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Attempts { get; set; }
        public int CurrAttmps { get; set; }
        public string Langz { get; set; }
        public int Minutes { get; set; }
        public string Result { get; set; }
        public string Otp { get; set; }
        public String Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public String Email { get; set; }

        private String _NewEmail = String.Empty;
        public string NewEmail
        {
            get
            {
                return _NewEmail;
            }
            set
            {
                _NewEmail = value;
            }
        }
        public String Userid { get; set; }
        public String Tin { get; set; }

        private String _Mobile = String.Empty;
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                _Mobile = value;
            }
        }

        private String _NewMobile = String.Empty;
        public string NewMobile
        {
            get
            {
                return _NewMobile;
            }
            set
            {
                _NewMobile = value;
            }
        }
        public String Password { get; set; }
        private String _NewPassword = String.Empty;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                _NewPassword = value;
            }
        }
    }
    public class Dashboard
    {
        public List<DashboardResult> results { get; set; }
    }
    public class DashboardMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class DashboardResult
    {
        public DashboardMetadata __metadata { get; set; }
        public string Caltype { get; set; }
        public string TpType { get; set; }
        public string Tin { get; set; }
        public string Vktyp { get; set; }
        public string RtnTot { get; set; }
        public string NrtnTot { get; set; }
        public string PrtnTot { get; set; }
        public string UprtnTot { get; set; }
        public string PprtnTot { get; set; }
        public string IcrTot { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string Text1 { get; set; }
        public string DueIcr { get; set; }
        public DateTime? Begda { get; set; }
        public DateTime? Endda { get; set; }
        public string Persl { get; set; }
        public string Waers { get; set; }
        public string PbillsTot { get; set; }
        public string PbillsBetrw { get; set; }
        public string UpbillsTot { get; set; }
        public string UpbillsBetrw { get; set; }
        public string PrbillsTot { get; set; }
        public string PrbillsBetrw { get; set; }
    }
    public class ReturnInfo
    {
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
        public string ReturnTypeName { get; set; }
        public string ReturnCount { get; set; }
        public string iConImagePath { get; set; }

        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }

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

        //private ObservableCollection<ChartDataPoint> chartData;
        //public ObservableCollection<ChartDataPoint> ChartData
        //{
        //    get
        //    {
        //        return chartData;
        //    }

        //    set
        //    {
        //        if (chartData == value)
        //        {
        //            return;
        //        }

        //        chartData = value;
        //    }
        //}
    }
    public class eServiceInfo
    {
        public string eServiceName { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
        public string iConImagePath { get; set; }
        public string OnClickEvents { get; set; }
    }

    /// <summary>
    /// Model for the FAQ page.
    /// </summary>
    //[Preserve(AllMembers = true)]
    [DataContract]
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

    #region OverduePaymentsAndUnSubmittedReturns
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class OverduePaymentsAndUnSubmittedReturn
    {
        public Metadata __metadata { get; set; }
        public DateTime Abrzu { get; set; }
        public string Gpartz { get; set; }
        public DateTime Abrzo { get; set; }
        public string Langz { get; set; }
        public string Incotyp { get; set; }
        public string Incotext { get; set; }
        public string IcrStatus { get; set; }
        public string Sopbel { get; set; }
        public DateTime DueDt { get; set; }
        public string Fbnum { get; set; }
        public string CalendarTyp { get; set; }
        public string Fbtyp { get; set; }
        public string FbtText { get; set; }
        public string Txt50 { get; set; }
        public string Persl { get; set; }
        public string Amount { get; set; }
        public string Waers { get; set; }
    }

    //public class D
    //{
    //    public List<OverduePaymentsAndUnSubmittedReturn> results { get; set; }
    //}

    //public class RootObject
    //{
    //    public D d { get; set; }
    //}
    #endregion



}
