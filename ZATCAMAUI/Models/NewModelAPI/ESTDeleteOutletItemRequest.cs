using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI
{
    public class ESTDeleteOutletItemRequest
    {
        public string formBundleNumber { get; set; }
        public string activityNumber { get; set; }
        public string portalUser { get; set; }
        public string TIN { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
    }
}
