using System;
using System.Collections.Generic;

namespace GAZT.Models
{
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
        public DateTime Begda { get; set; }
        public DateTime Endda { get; set; }
        public string Persl { get; set; }
        public string Waers { get; set; }
        public string PbillsTot { get; set; }
        public string PbillsBetrw { get; set; }
        public string UpbillsTot { get; set; }
        public string UpbillsBetrw { get; set; }
        public string PrbillsTot { get; set; }
        public string PrbillsBetrw { get; set; }
    }


    public class BillReturn
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
                if(_returnTypeProperty == ReturnType.RtnTot)
                {
                    ImagePath = "ic_Check_Mark.png";
                    ReturnTypeName = AppResources.Submitted;
                }
                if (_returnTypeProperty == ReturnType.NrtnTot)
                {
                    ImagePath = "non_submitted.png";
                    ReturnTypeName = AppResources.NonSubmitted;
                }
                if (_returnTypeProperty == ReturnType.PrtnTot)
                {
                    ImagePath = "ic_Check_Mark.png";
                    ReturnTypeName = AppResources.Paid;
                }
                if (_returnTypeProperty == ReturnType.UprtnTot)
                {
                    ImagePath = "ic_Check_Mark.png";
                    ReturnTypeName = AppResources.UnPaid;
                }
                if (_returnTypeProperty == ReturnType.PprtnTot)
                {
                    ImagePath = "partiallay_paid_returns.png";
                    ReturnTypeName = AppResources.PartiallyPaid;
                }
                if (_returnTypeProperty == ReturnType.DueIcr)
                {
                    ImagePath = "overdue_Returns.png";
                    ReturnTypeName = AppResources.OverDue;
                }
            }
        }
        public string ReturnTypeName { get; set; }
        public string ReturnCount { get; set; }
        public string ImagePath { get; set; }
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

    public class BillPaid
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
                if (_billTypeProperty == BillType.PbillsTot)
                {
                    ImagePath = "ic_check_circle.png";
                    BillTypeName = AppResources.Paid;
                }
                if (_billTypeProperty == BillType.PrbillsTot)
                {
                    ImagePath = "ic_loading.png";
                    BillTypeName = AppResources.Partial;
                }
                if (_billTypeProperty == BillType.UpbillsTot)
                {
                    ImagePath = "ic_money.png";
                    BillTypeName = AppResources.UnPaid;
                }
               
               
            }
        }
        public string BillTypeName { get; set; }
        public string BillCount { get; set; }
        public string ImagePath { get; set; }
        public string BillAmount { get; set; }
    }

    //public class D
    //{
    //    public List<DashboardResult> results { get; set; }
    //}



}
