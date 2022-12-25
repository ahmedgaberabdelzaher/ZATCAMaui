using System;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using System.Net.Http;
using EGAZT.Models.EDeclerationsModel.SubmitModels;

namespace EGAZT.Services.Interface
{
    public interface IE_DeclerationServices
    {
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>, bool, string>> GetTobacoTypes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string>> GetTobacoItem(int TobacoTypeID);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductTypes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductSubTypes(string productTypeId);
        Task<HttpResponseMessage> FeesCalculator(FeesCalculatorBody body);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PurposeModel>>, bool, string>> GetPurposes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CurrencyModel>>, bool, string>> GetCurrencies();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<UnitsModel>>, bool, string>> GetUnits();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CountryModel>>, bool, string>> GetCountries();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PortsModel>>, bool, string>> GetPorts(int tripType);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<PortsModel>>, bool, string>> GetTravelPurpose();
        Task<HttpResponseMessage> SubmitDecleration(EDeclerationSubmitModel body);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CoinTypesModel>>, bool, string>> GetCoinTypes();
        Task<Tuple<DATAPowerBaseResponse<InquireResponse>, bool, string>> GetInquireDecleration(string referenceNumber, string travelID);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<CountryCodeModel>>, bool, string>> GetCountriesCode();
    }
       
 }

