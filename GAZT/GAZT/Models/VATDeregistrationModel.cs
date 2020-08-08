using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models
{
    public class VATDeregistrationModel
    {
        public VATDeregistrationModel()
        {

        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }

        public string ActiveOutletDocumentOptions { get; set; }
        public bool ActiveOutletDocumentOptionsIsSelected { get; set; }
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
