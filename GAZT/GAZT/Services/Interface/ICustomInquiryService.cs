using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.CustomServices;

namespace EGAZT.Services.Interface
{
    public interface ICustomInquiryService
    {
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther=false);
        Task<Tuple<CarriersModel, bool, string>> GetCarriers(int routCode);
        Task<Tuple<DeclarationTypesModel, bool, string>> GetDeclarationTypes();
        Task<Tuple<DeclarionInformationInquireResponse, bool, string>> GetDcltnBusID(int portNo, int billNo, string date, int dclType);
        Task<Tuple<DeclarationFeesResponse, bool, string>> GetDclFees(int portNo, int billNo, string date, int dclType);
        Task<Tuple<StatmentItemsResponse, bool, string>> GetDclStatmentItems(int portNo, int dcltnISN, int dclType);
        Task<Tuple<InquireByBillInfoResponse, bool, string>> GetDeclarationInfoByBill(int portNo, string billNo, int CarrPrefix);
        Task<Tuple<CustomItemCalcDescription, bool, string>> GetItemsCalcTxt(int portNo, int Dcltype, string DClISn);
            }
}
