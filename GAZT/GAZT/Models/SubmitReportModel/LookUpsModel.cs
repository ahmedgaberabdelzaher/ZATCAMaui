using System;
using System.Collections.Generic;

namespace EGAZT.Models.SubmitReportModel
{
    public class LookUpsModel
    {
        public string lookupId { get; set; }
        public string lookupName { get; set; }
    }
    public class LookUpsListModel
    {
        public List<LookUpsModel> lookUpList { get; set; }
    }
}

