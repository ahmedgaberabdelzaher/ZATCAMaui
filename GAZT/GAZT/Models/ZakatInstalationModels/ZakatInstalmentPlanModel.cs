using System;
using Xamarin.Forms;

namespace EGAZT.Models.ZakatInstalationModels
{
    public class ZakatInstalmentPlanModel
    {
        public ZakatInstalmentPlanModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }

    public class InstalmentAgreementFrequencyModel
    {
        public InstalmentAgreementFrequencyModel()
        {

        }

        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
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

    public class ZakatSelectBillModel
    {
        public ZakatSelectBillModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }
    }

    public class ZakatSummaryViewModel
    {
        public ZakatSummaryViewModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
}
