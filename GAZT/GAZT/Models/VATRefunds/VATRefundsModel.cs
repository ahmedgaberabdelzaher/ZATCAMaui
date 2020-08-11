using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models.VATRefunds
{
    public class VATRefundsModel
    {
        public VATRefundsModel()
        {

        }

        public string Title { get; set; }
        public string RefernceNumber { get; set; }
        public string Status { get; set; }
        public string RequestedAmount { get; set; }
        public string ReassesmentAmount { get; set; }
        public string TotalOffset { get; set; }
        public string NetCreditBalance { get; set; }
        public string RequestDate { get; set; }
        public string TotalAmount { get; set; }
        public bool IsNewRequest { get; set; }

        public VATRefundsBankDetailsModel BankDetails { get; set; }
        public ObservableCollection<VATRefundsReturnsModel> VATReturns { get; set; }
    }

    public class VATRefundsBankDetailsModel
    {
        public string BankName { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string IBAN { get; set; }
        public string Icon { get; set; }
    }

    public class VATRefundsReturnsModel
    {
        public string ReturnPeriod { get; set; }
        public string CreditBalance { get; set; }
        public string ReassessedBalance { get; set; }
        public string Offset { get; set; }
        public string NetCreditBalance { get; set; }
        public string Status { get; set; }
        public string LastStatusChange { get; set; }
    }
}
