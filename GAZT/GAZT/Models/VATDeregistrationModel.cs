using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using static Xamarin.Forms.Internals.Profile;

namespace EGAZT.Models
{
    public class VATDeregistrationModel
    {

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }

        public string ActiveOutletDocumentOptions { get; set; }
        public bool ActiveOutletDocumentOptionsIsSelected { get; set; }
    }
    public class VATDeregistrationModelRootObject
    {
        public VATDeregistrationModelDetailD d { get; set; }
    }
    public class VATDeregistrationModelDetailD
    {
        public List<VATDeregistrationModelDetailsResult> results { get; set; }
    }
  
    public class VATDeRegistrationAttachmentDropdownDetails
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("ELGBL_DOCSet")]
        public VatDeregSubItemsSet VatDeregSubItemsSet { get; set; }
        //public VATDeregistrationAttachmentDetailD d { get; set; }
    }
    public partial class VatDeregSubItemsSet
    {
        [JsonProperty("results")]
        public ResultsAttachmentItemForElgblDocSet[] Results { get; set; }
    }
    public class VATDeregistrationSuspendedDateRootObject
    {
        public VATDeregSuspendedDateRootObjectDetailD d { get; set; }
    }
    public class VATDeregSuspendedDateRootObjectDetailD
    {
        [JsonProperty("results")]
        public List<VATDeregSuspendedDateRootObjectDetailsResult> dateResults { get; set; }
    }
    public class VATDeregSuspendedDateRootObjectDetailsResult
    {
        public __metadata __metadata { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? SuspDtfrom { get;
            set;
        }
        public DateTime? SuspDtto { get; set; }
        public DateTime? NextDtfrom { get; set; }
        public DateTime? NextDtto { get; set; }
        public DateTime? Duedate { get; set; }

    }

    public class VATDeregistrationModelDetailsResult
    {
        public __metadata __metadata { get; set; }

        public string Reason { get; set; }
        public string Lang { get; set; }
        public string TxnTp { get; set; }
        public string Rdesc { get; set; }

    }

    public class ResultsAttachmentItemForElgblDocSet
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Spras { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string StartDt { get; set; }
        public string EndDt { get; set; }
        public string Txt50 { get; set; }
    }

    public class VATDeregistrationAttachmentsModel
    {
        public VATDeregistrationAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }

    public class VATDeregistrationSummaryModel
    {
        public VATDeregistrationSummaryModel()
        {
        }

        public string SummaryTitle { get; set; }
        public string SummaryData { get; set; }
        public bool IsEditVisible { get; set; }
    }


}
