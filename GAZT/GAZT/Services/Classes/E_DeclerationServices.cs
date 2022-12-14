using System;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.BaseModels;
using System.Collections.ObjectModel;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.AppConfigurations;
using System.Net.Http;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using System.Security.Cryptography.Xml;

namespace EGAZT.Services.Classes
{
    public class E_DeclerationServices: IE_DeclerationServices
    {
        static string version = "v1";
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>, bool, string>> GetTobacoTypes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/tobacco-category").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string>> GetTobacoItem(int TobacoTypeID)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/tobacco-items?tobaccoTypeID={TobacoTypeID}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductTypes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/good-types").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductSubTypes(string productTypeId)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/good-sub-types?goodTypeID={productTypeId}").ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> FeesCalculator(FeesCalculatorBody body)
        {
            var response = await HttpManager.PostAsync<FeesCalculatorBody>($"{PageSettings.ZATCABaseURL}{version}/customs/calculate-fees",body).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PurposeModel>>, bool, string>> GetPurposes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<PurposeModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/purposes").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CurrencyModel>>, bool, string>> GetCurrencies()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<CurrencyModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/currencies").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<UnitsModel>>, bool, string>> GetUnits()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<UnitsModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/units").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CountryModel>>, bool, string>> GetCountries()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<CountryModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/countries").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PortsModel>>, bool, string>> GetTravelPurpose()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<PortsModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/travel-purposes").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PortsModel>>, bool, string>> GetPorts(int tripType)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<PortsModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/ports?tripTypeID={tripType}").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<InquireResponse>, bool, string>> GetInquireDecleration(string referenceNumber, string travelID)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<InquireResponse>>($"{PageSettings.ZATCABaseURL}{version}/zatca/customs/declaration/inquire-declaration?declarationID={referenceNumber}&travelID={travelID}").ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> SubmitDecleration(EDeclerationSubmitModel body)
        {
            var response = await HttpManager.PostAsync<EDeclerationSubmitModel>($"{PageSettings.ZATCABaseURL}{version}/zatca/customs/declaration/submit-declaration", body).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CoinTypesModel>>, bool, string>> GetCoinTypes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<CoinTypesModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/coin-types").ConfigureAwait(false);
            return response;
        }
    }
}

