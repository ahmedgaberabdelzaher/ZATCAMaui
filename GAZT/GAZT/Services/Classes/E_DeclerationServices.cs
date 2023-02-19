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
            // var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/tobacco-items?tobaccoTypeID={TobacoTypeID}").ConfigureAwait(false);
            #region will be deleted when consume this service using DATAPower
            var result = await HttpManager.GetAsync<TobaccoItemsNewModelResponse>($"{PageSettings.CustomPeserviceBaseURl}/Portal/api/Passengers/GetTobaccoItemsv2/{TobacoTypeID}",false).ConfigureAwait(false);
            Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string> response;
            ObservableCollection<TobaccoItemsModel> tobaccoItems = new ObservableCollection<TobaccoItemsModel>(); 
            if (result.Item2)
            {
                foreach (var item in result.Item1.data)
                {
                    tobaccoItems.Add(new TobaccoItemsModel()
                    {
                        itemCode = item.HarmonizedCode,
                        itemDescription = item.ItemDescription,
                        productName = item.Productname,
                        measurementUnit = item.MeasureUnit,
                        taxSequence = item.TaxSequence,
                        HasMeasureUnit = item.HasMeasureUnit,
                        HasWeight = item.HasWeight,
                        MeasureUnitAR = item.MeasureUnitAR,
                        MeasureUnitEN = item.MeasureUnitEN
                    });
                }
                var tobacoresult = new DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>() { data = tobaccoItems };
             response= Tuple.Create(tobacoresult, true, "");
            }
            else
            {
                response = Tuple.Create(new DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>() { data=null, header=new Header() { moreInformation=new MoreInformation() {  backendErrors= result.Item3 } } }, true, "");
            }
            #endregion

            return response;
        }
       public async Task<Tuple<TobaccoItemsNewModelResponse, bool, string>> GetTobacoItemNew(int TobacoTypeID)
        {
            var response = await HttpManager.GetAsync<TobaccoItemsNewModelResponse>($"{PageSettings.CustomPeserviceBaseURl}/Portal/api/Passengers/GetTobaccoItemsv2/{TobacoTypeID}").ConfigureAwait(false);
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
            // var response = await HttpManager.PostAsync<FeesCalculatorBody>($"{PageSettings.ZATCABaseURL}{version}/customs/calculate-fees", body).ConfigureAwait(false);
            var custombody = new FeesCalculatorBodyCustomApi();
            var productItems = new System.Collections.Generic.List<ProductItemCustomApi>();
            var tobacoItems = new System.Collections.Generic.List<TobaccoItemCustomApi>();
            foreach (var item in body.product)
            {
                productItems.Add(new ProductItemCustomApi() { Count = item.Count, HarmonizedCode = item.harmonizedCode, Value = item.value, ID = item.ID });
            }
            foreach (var item in body.tobacco)
            {
                tobacoItems.Add(new TobaccoItemCustomApi() { ID=item.ID, Count=item.count, Value=item.value, HarmonizedCode=item.harmonizedCode, MeasuringUnit=item.MeasuringUnit, Sequence=item.sequence,Wight=item.Wight});
            }
            custombody.TobaccoItems= tobacoItems;
            custombody.ProductItems = productItems;
            var response = await HttpManager.PostAsync<FeesCalculatorBodyCustomApi>($"{PageSettings.CustomPeserviceBaseURl}/Portal/api/Passengers/calculate", custombody).ConfigureAwait(false);
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
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CountryCodeModel>>, bool, string>> GetCountriesCode()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<CountryCodeModel>>>($"{PageSettings.ZATCABaseURL}{version}/references/customs/nibras/phone-country-codes").ConfigureAwait(false);
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
        public async Task<Tuple<string, bool, string>> GetTransactionId()
        {
            var response = await HttpManager.GetStringAsync("https://payments-peservices.zatca.gov.sa/payment/dummy").ConfigureAwait(false);
            return response;
        }
    }
}

