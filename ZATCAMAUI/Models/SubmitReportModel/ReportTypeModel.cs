namespace ZATCAMAUI.Models.SubmitReportModel
{
    public class ReportTypeModel
    {
        public string reportTaxTypeId { get; set; }
        public string reportTaxTypeCode { get; set; }
        public string reportTaxTypeName { get; set; }
    }
    public class ReportTypeList
    {
        public List<ReportTypeModel> reportTaxTypes { get; set; }
    }
}

