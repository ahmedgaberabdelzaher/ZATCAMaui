
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ZATCAMAUI.Models
{
    public class Dashboard
    {
        public List<DashboardResult> data { get; set; }
    }
    
    public class DashboardMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }    
    
    public class DashboardResult
    {
        public string calendarType { get; set; }
        public string estimateZakat { get; set; }
        public string TIN { get; set; }
        public string taxpayerType { get; set; }
        public string contractAccountType { get; set; }
        public string returnTotalNumber { get; set; }
        public string nonSubmittedReturnTotalNumber { get; set; }
        public string paidReturnTotalNumber { get; set; }
        public string unpaidReturnTotalNumber { get; set; }
        public string partialReturnTotalNumber { get; set; }
        public string icrTotal { get; set; }
        public string status { get; set; }
        public string accountCategory { get; set; }
        public string accountCategoryDescription { get; set; }
        public string dueTotalNumber { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string periodkey { get; set; }
        public string currency { get; set; }
        public string paidBillsTotalNumber { get; set; }
        public string paidBillsAmount { get; set; }
        public string unpaidBillsTotalNumber { get; set; }
        public string unpaidBillAmount { get; set; }
        public string partialBillsTotalNumber { get; set; }
        public string partialBillsAmount { get; set; }
        public string instructionAction { get; set; }

    }



    
    public class InstalmentPlanResult
    {
        public Metadata __metadata { get; set; }

        [JsonProperty("taxType")]
        public string TaxType { get; set; }
        [JsonProperty("taxTypeDescription")]
        public string TaxTypeDescription { get; set; }
        [JsonProperty("installmentTotalAmount")]
        public string InstallmentTotalAmount { get; set; }
        [JsonProperty("nextInstallmentAmount")]
        public string NextInstallmentAmount { get; set; }
        [JsonProperty("documentDate")]
        public string DocumentDate { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        //public string DayMonth { get; set; }
        private string _dayMonth;
        [JsonProperty("dayMonth")]
        public string DayMonth
        {
            get
            {
                return _dayMonth;
            }
            set
            {
                _dayMonth = value;

                _dayMonth = Regex.Replace(_dayMonth, "['st','nd','rd','th']", "");

            }
        }

        [JsonProperty("totalNumberOfInstallment")]
        public string TotalNumberOfInstallment { get; set; }
        [JsonProperty("totalInstallmentsPaid")]
        public string TotalInstallmentsPaid { get; set; }
        [JsonProperty("totalInstallmentsUnpaid")]
        public string TotalInstallmentsUnpaid { get; set; }

        [JsonIgnore]
        public ObservableCollection<Model> Series { get; set; } = new ObservableCollection<Model>();
        [JsonIgnore]
        public ObservableCollection<Brush> ChartColors { get; set; } = new ObservableCollection<Brush>();
        [JsonIgnore]
        public string DayMonthToDisplay { get; set; }


    }

    
    public class Model
    {
        public Model(string x, double y)
        {
            XValue = x;
            YValue = y;
        }

        public string XValue { get; set; }

        public double YValue { get; set; }
    }

    
    public class INSTPLANItemSet
    {
        public List<InstalmentPlanResult> installmentPlans { get; set; }
    }

    
    public class DashboardInstalmentplan
    {
        public Metadata __metadata { get; set; }
        public string TIN { get; set; }
        public string language { get; set; }
        public string inputChannel { get; set; }
        public List<InstalmentPlanResult> installmentPlans { get; set; }
    }


}
