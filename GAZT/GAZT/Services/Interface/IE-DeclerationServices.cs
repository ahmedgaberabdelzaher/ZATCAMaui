using System;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface IE_DeclerationServices
    {
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>, bool, string>> GetTobacoTypes();
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string>> GetTobacoItem(int TobacoTypeID);
    }
}

