using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.ChangeFillingPeriodModel
{
    public class ChangeFillingPeriodModel
    {
        public ChangeFillingPeriodModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }


    public class InstalmentAgreementAttachmentsModel
    {
        public InstalmentAgreementAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }


    public class MyRequestsListModel
    {
        public MyRequestsListModel()
        {
        }

        public string Title { get; set; }
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public string CurrentFrequency { get; set; }
        public string NewFrequency { get; set; }
        public string EffectiveDate { get; set; }
        public string ReleaseDate { get; set; }
    }


}
