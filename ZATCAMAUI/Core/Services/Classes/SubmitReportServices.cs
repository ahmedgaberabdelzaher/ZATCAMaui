using System.Threading.Tasks;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.SubmitReportModel;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class SubmitReportServices : ISubmitReportServices
    {
        public async Task<List<ReportTypeModel>> GetReportType()
        {
      
            var lang = App.IsArabic ? "ar" : "en";
            var response = await NewHTTPManger.Get<DATAPowerBaseResponse<ReportTypeList>>($"{PageSettings.ZATCABaseURL}v1/vat/reports/all-tax-types?languageCode={lang}") as DATAPowerBaseResponse<ReportTypeList>;
            var data = response?.data?.reportTaxTypes.GroupBy(c => c.reportTaxTypeName).Select(c=>c.First()).ToList();
            return data;

        }
        public async Task<List<BaseRegionAndCity>> GetLookUps()
        {
            var lang = App.IsArabic ? "ar" : "en";
            var response = await NewHTTPManger.Get<DATAPowerBaseResponse<LookUpsModel>>($"{PageSettings.ZATCABaseURL}/v1/vat/reports/lookups?languageCode={lang}") as DATAPowerBaseResponse<LookUpsModel>;
            return response?.data?.lookups;

        }
        public async Task<List<CategoryDataResponse>> GetReportCategories(string typeId)
        {
        
            var lang = App.IsArabic ? "ar" : "en";
            var response = await NewHTTPManger.Get<DATAPowerBaseResponse<CategoryResponseModel>>($"{PageSettings.ZATCABaseURL}v1/vat/sms/categories?languageCode={lang}&categoryType={typeId}") as DATAPowerBaseResponse<CategoryResponseModel>;
            return response?.data.categories;

        }

        public async Task<List<CategoryDataResponse>> GetReportSubCategories(string CatId)
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<SubCategoryResponseModel>>($"https://vatmobile.zatca.gov.sa/api/Report/GetReportSubCategory?categoryCode={CatId}") as BaseResponseModel<SubCategoryResponseModel>;
              return response?.Result?.Data.subCategoryList;


        }
        public async Task<List<BaseRegionAndCity>> GetCities(string regionId)
        {
            var lang = App.IsArabic ? "ar" : "en";
            var response = await NewHTTPManger.Get<DATAPowerBaseResponse<CitiesListModel>>($"{PageSettings.ZATCABaseURL}v1/vat/sms/cities?languageCode={lang}&region={regionId}") as DATAPowerBaseResponse<CitiesListModel>;

            return response?.data.cities;
        } 
        public async Task<List<BaseRegionAndCity>> GetRegions()
        {
            
            var lang = App.IsArabic ? "ar" : "en";
            var response = await NewHTTPManger.Get<DATAPowerBaseResponse<RegionsListModel>> ($"{PageSettings.ZATCABaseURL}v1/vat/sms/regions?languageCode={lang}") as DATAPowerBaseResponse<RegionsListModel>;
            return response?.data.regions;

        }
        public async Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportDataPowerModel submitReport)
        {
            var res = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}v4/zatca/new-report/submit-form", submitReport);

            var cont = await res.Content.ReadAsStringAsync();
            var response = NewHTTPManger.DeserializeObject<DATAPowerBaseResponseResult<SubmitDataPowerResult>>(cont);
            return response;
        }
        public async Task<BaseResponseModel<string>> CreateZatcaReport(SubmitReportModel submitReport)
        {
         
            var response = await NewHTTPManger.Post<BaseResponseModel<string>>($"{App.VatBaseUrl}/Report/CreateZatcaNewReport",submitReport) as BaseResponseModel<string>;
           
            return response;
        }
    }

    public class portalResponse
    {
        public string ReportCategoryCode { get; set; }
        public string ReportCategoryGuid { get; set; }
        public string ReportCategoryNameAR { get; set; }
        public string ReportCategoryNameEN { get; set; }
        public string ReportTypeGuid { get; set; }
    }
}

