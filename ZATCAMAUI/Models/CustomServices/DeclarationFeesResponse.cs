using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices
{

    public class DeclarationFees
    {
        public string acct_desc { get; set; }
        public string aid_type { get; set; }
        public double amt { get; set; }
        public bool isHideSeperatorLine { get; set; } = true;
    }

    public class DeclarationFeesResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<DeclarationFees> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }


}
