namespace EGAZT.Models.SubmitReportModel
{
    public class ReportResponseModel
    {
        public string TaxEvasionNumber { get; set; }
        public string TaxEvasionGuid { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
