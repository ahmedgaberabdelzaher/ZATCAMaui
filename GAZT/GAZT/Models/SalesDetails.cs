using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class SalesDetails
    {
        public string SalesType { get; set; }
        public string InformationFromPartie { get; set; }
        public string InformationFromPartieToCompare { get; set; }
        public string EstimateSales { get; set; }
        public string EditImageSource { get; set; } = "";
        public string NewValue { get; set; } = "";
        public string ChangeReason { get; set; } = "";
        public string AttchamentNumber { get; set; }
        public string AttchamentName { get; set; }
        public string SelectedEditFieldId { get; set; }
        public bool  ComingFromAmendEditMode { get; set; } = false;
        public string OldValue { get; set; }
        public bool IsAttachmentRequired { get; set; } = false;
        public bool IsReasonRequird{ get; set; } = false;
        public bool IsOldValueChanged { get; set; } = false;
        public EstimateZakatAttachment estimateZakatAttachment { get; set; }

    }

}
