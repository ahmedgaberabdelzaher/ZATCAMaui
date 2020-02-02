using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class SalesDetails
    {
        public string SalesType { get; set; }
        public string InformationFromPartie { get; set; }
        public string EstimateSales { get; set; }
        public string NewValue { get; set; }
        public string ChangeReason { get; set; }
        public string AttchamentNumber { get; set; }
        public string AttchamentName { get; set; }
        public string SelectedEditFieldId { get; set; }
        public bool  ComingFromAmendEditMode { get; set; } = false;

    }

}
