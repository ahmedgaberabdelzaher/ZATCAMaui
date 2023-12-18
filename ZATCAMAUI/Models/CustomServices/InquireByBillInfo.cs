namespace ZATCAMAUI.Models.CustomServices
{
    public class InquireByBillInfo
    {
        public int stmt_isn { get; set; }
        public int impr_nbr { get; set; }
        public int dcltn_type_cd { get; set; }
        public int dcltn_nbr { get; set; }
        public string dcltn_dt { get; set; }
        public string impr_mobile_no { get; set; }
    }

    public class InquireByBillInfoResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public InquireByBillInfo data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
