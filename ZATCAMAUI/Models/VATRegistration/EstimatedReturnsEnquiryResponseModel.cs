using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.VATRegistration
{
    public class EstimatedReturnsEnquiryResponseModel
    {
        public Header header { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public string userTIN { get; set; }
            public string businessPartnerNumber { get; set; }
            public string language { get; set; }
            public string auditor { get; set; }
            public string client { get; set; }
            public string user { get; set; }
            public string registrationStatus { get; set; }
            public int fillingObligation { get; set; }
            public string title { get; set; }
            public string taxType { get; set; }
            public string branchDescription { get; set; }
            public string portNumber { get; set; }
            public string systemName { get; set; }
            public string protocol { get; set; }
            public int referenceNumber { get; set; }
            public int requestNumber { get; set; }
            public int correspondenceNumber { get; set; }
            public int registrationNumber { get; set; }
            public int accountNumber { get; set; }
            public int obligationNumber { get; set; }
            public bool isAuditorReturn { get; set; }
            public bool isAuditorObjection { get; set; }
            public bool isAuditorRequest { get; set; }
            public bool isAuditorRefund { get; set; }
            public string calenderType { get; set; }
            public bool isTileOutlet { get; set; }
            public bool isTilePermit { get; set; }
            public bool isTileTIN { get; set; }
            public bool isBankruptcy { get; set; }
            public string authenticationUser1 { get; set; }
            public string authenticationUser2 { get; set; }
            public string authenticationUser3 { get; set; }
            public string authenticationUser4 { get; set; }
            public string authenticationUser5 { get; set; }
            public string authenticationUser { get; set; }
            public string formBundleGUID { get; set; }
            public bool isAuditorRefundTransaction { get; set; }
            public string portalLink { get; set; }
            public bool isUpdateRegistrationOutlet { get; set; }
            public bool isDisplayShare { get; set; }
            public bool isNotificationLog { get; set; }
            public bool isEnableTile { get; set; }
            public string definedArea { get; set; }
            public string type { get; set; }
            public bool isEnableInstallementPlan { get; set; }
            public int overDue { get; set; }
            public int interest { get; set; }
            public int penaltyAmount { get; set; }
            public string exemptDetail { get; set; }
            public string exemptApplication { get; set; }
            public string hostName { get; set; }
            public int indirectCorrespondenceNumber { get; set; }
            public object closelyDefinedArea { get; set; }
            public string vatDetail { get; set; }
            public string warehouseDetail { get; set; }
            public string @return { get; set; }
            public int returnCountNumber { get; set; }
            public int activeCountNumber { get; set; }
            public int renewalCountNumber { get; set; }
            public int cancellationCountNumber { get; set; }
            public string zakatRegistrationdetail { get; set; }
            public string VATConfig { get; set; }
            public string VATIndividualSignUp { get; set; }
            public string VATEligiblePerson { get; set; }
            public string callService { get; set; }
            public List<List> lists { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        public class List
        {
            public DateTime periodEndDate { get; set; }
            public string error { get; set; }
            public string taxTypeDescription { get; set; }
            public DateTime periodStartDate { get; set; }
            public DateTime date { get; set; }
            public bool isAuditor { get; set; }
            public string auditor { get; set; }
            public DateTime beginDate { get; set; }
            public string branchName { get; set; }
            public string calenderType { get; set; }
            public string combination { get; set; }
            public string display { get; set; }
            public string dueStatus { get; set; }
            public DateTime dueDate { get; set; }
            public string dueDateCharacter { get; set; }
            public string endDate { get; set; }
            public string authenticationUser { get; set; }
            public string authenticationUser1 { get; set; }
            public string authenticationUser2 { get; set; }
            public string authenticationUser3 { get; set; }
            public string authenticationUser4 { get; set; }
            public string authenticationUser5 { get; set; }
            public string formBundleGUID { get; set; }
            public string formBundleNumber { get; set; }
            public string formBundleText { get; set; }
            public string formBundleType { get; set; }
            public string selected { get; set; }
            public string TIN { get; set; }
            public string inboundCorrespondence { get; set; }
            public string inboundCorrespondenceType { get; set; }
            public string informationMessage { get; set; }
            public string language { get; set; }
            public string goLive { get; set; }
            public string month { get; set; }
            public string errorMessage { get; set; }
            public bool objectionFiled { get; set; }
            public string obligation { get; set; }
            public bool open { get; set; }
            public string period { get; set; }
            public string periodkey { get; set; }
            public bool refundFiled { get; set; }
            public string returnGUID { get; set; }
            public string sadadBillNumber1 { get; set; }
            public string sadadBillNumber2 { get; set; }
            public string status2 { get; set; }
            public string Statfg { get; set; }
            public string status { get; set; }
            public string status1 { get; set; }
            public string angularUserStatus { get; set; }
            public string angularSystemStatus { get; set; }
            public string taxPeriod { get; set; }
            public string TaxText { get; set; }
            public string userError { get; set; }
            public string userStatus { get; set; }
            public string userStatusAngular { get; set; }
            public string contract { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }
    }
}
