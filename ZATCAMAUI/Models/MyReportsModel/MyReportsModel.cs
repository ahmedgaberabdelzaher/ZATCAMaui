using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.MyReportsModel
{
    public class MyReportsModel : ObservableRecipient
    {
        public string id { get; set; }
        public string reportType { get; set; }
        public string reportNumber { get; set; }
        public string vatNumber { get; set; }
        public int reportStatus { get; set; }
        public string facilityName { get; set; }
        public string featuredNumber { get; set; }
        public string companyAddress { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string workType { get; set; }
        public string latitude { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string longitude { get; set; }
        public string details { get; set; }
        public string addedDate { get; set; }
        public string cr { get; set; }
        public string tin { get; set; }
        //public List<> attachment { get; set; }

        public string ReportDate { get { return DateTime.Parse(addedDate).Date.ToString("dd-MM-yyyy", new CultureInfo("en-US")); } }

        private string reportLocation;
        public string ReportLocation { get { return reportLocation; } set { reportLocation = value; OnPropertyChanged(); } }
    }

    public class ReportsResult
    {
        public List<MyReportsModel> reportTaxTypes { get; set; }
        public int totalCount { get; set; }
        public int pagesCount { get; set; }
    }










}

