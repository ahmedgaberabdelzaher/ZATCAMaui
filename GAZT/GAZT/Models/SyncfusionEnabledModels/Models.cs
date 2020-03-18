using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GAZT.Models
{
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
    public class OverduePaymentsAndUnSubmittedReturn
    {
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
