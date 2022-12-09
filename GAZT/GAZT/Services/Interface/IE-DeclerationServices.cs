using System;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using System.Net.Http;

namespace EGAZT.Services.Interface
{
    public interface IE_DeclerationServices
    {
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>, bool, string>> GetTobacoTypes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string>> GetTobacoItem(int TobacoTypeID);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductTypes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductSubTypes(string productTypeId);
        Task<HttpResponseMessage> FeesCalculator(FeesCalculatorBody body);
    }
}

