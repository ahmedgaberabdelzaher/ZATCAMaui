using GalaSoft.MvvmLight;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel
{
   public class DashboardPageViewModel : ViewModelBase
    {
        #region Property

        private List<BillReturn> _billReturn;
        public List<BillReturn> BillReturn
        {
            get
            {
                return _billReturn;
            }
            set
            {
                _billReturn = value;
                RaisePropertyChanged("BillReturn");
            }
        }


        private List<BillPaid> _billPaid;
        public List<BillPaid> BillPaid
        {
            get
            {
                return _billPaid;
            }
            set
            {
                _billPaid = value;
                RaisePropertyChanged("BillPaid");
            }
        }


        

        #endregion


        #region Constructor

        
        #endregion


        #region Method

        public void onPageLoad()
        {
            BillReturn = new List<BillReturn>();

                BillReturn bill = new BillReturn();
                bill.ReturnTypeProperty = ReturnType.RtnTot;
                bill.ReturnCount = "10";
           
                BillReturn.Add(bill);

                BillReturn bill1 = new BillReturn();
                bill1.ReturnTypeProperty = ReturnType.DueIcr;
                bill1.ReturnCount = "30";
                BillReturn.Add(bill1);

                BillReturn bill2 = new BillReturn();
                bill2.ReturnTypeProperty = ReturnType.PrtnTot;
                bill2.ReturnCount = "15";
                BillReturn.Add(bill2);

                BillReturn bill3 = new BillReturn();
                bill3.ReturnTypeProperty = ReturnType.NrtnTot;
                bill3.ReturnCount = "34";
                BillReturn.Add(bill3);

                BillReturn bill4 = new BillReturn();
                bill4.ReturnTypeProperty = ReturnType.UprtnTot;
                bill4.ReturnCount = "22";
                BillReturn.Add(bill4);



            BillPaid = new List<BillPaid>();

            BillPaid billPaid1 = new BillPaid();
            billPaid1.BillTypeProperty = BillType.PbillsTot;
            billPaid1.BillCount = "10";
            BillPaid.Add(billPaid1);

            BillPaid billPaid2 = new BillPaid();
            billPaid2.BillTypeProperty = BillType.PrbillsTot;
            billPaid2.BillCount = "20";
            BillPaid.Add(billPaid2);

            BillPaid billPaid3 = new BillPaid();
            billPaid3.BillTypeProperty = BillType.UpbillsTot;
            billPaid3.BillCount = "40";
            BillPaid.Add(billPaid3);

        }

        #endregion
    }
}
