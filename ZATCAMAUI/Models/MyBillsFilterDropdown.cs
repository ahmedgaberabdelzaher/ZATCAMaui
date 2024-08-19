
namespace ZATCAMAUI.Models
{

    public class MyBillsFilterDropdownResult
    {
        public MyBillsFilterDropdownList d { get; set; }
    }

   
    public class MyBillsFilterDropdownList
    {
        public List<MyBillsFilterDropdown> results { get; set; }

    }
   
    public class MyBillsFilterDropdown
    {
        public Metadata __metadata { get; set; }
        public string language { get; set; }
        public string systemCode { get; set; }
        public string taxType { get; set; }
        public DateTime fromDate { get; set; }
        public string statementFilter { get; set; }
        public string counter { get; set; }
        public string contractObject { get; set; }
        public string revenueType { get; set; }
        public string revenueTypeDescription { get; set; }
        public string toDate { get; set; }
    }
}