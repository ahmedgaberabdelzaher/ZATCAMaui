using System;
namespace EGAZT.Models
{
    public class MyReportsModel
    {
        public string Id { get; set; }
        public string ReportType { get; set; }
        public string ReportNumber { get; set; }
        public string VatNumber { get; set; }
        public int ReportStatus { get; set; }
        public string FacilityName { get; set; }
        public string FeaturedNumber { get; set; }
        public string CompanyAddress { get; set; }
        public string District { get; set; }
        public string WorkType { get; set; }
        public string Latitude { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Longitude { get; set; }
        public string Details { get; set; }
        public string AddedDate { get; set; }
        //public List<> Attachment { get; set; }
    }
}

