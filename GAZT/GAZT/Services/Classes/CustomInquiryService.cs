using System;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class CustomInquiryService: ICustomInquiryService
    {
        public async Task<Tuple<CarriersModel, bool, string>> GetCarriers(int routCode=99)
        {
            var response = await HttpManager.GetAsync<CarriersModel>(App.CustomBaseUrl + $"Common/GetCarriers",true, routCode.ToString()).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther=false)
        {
            var response = await HttpManager.GetAsync<CustomPortsModel>(App.CustomBaseUrl + $"Common/GetCustomPort?Other={isContainOther}",true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<DeclarationTypesModel, bool, string>> GetDeclarationTypes()
        {
            var response = await HttpManager.GetAsync<DeclarationTypesModel>(App.CustomBaseUrl + $"Common/GetCGRefCodes/DCLTN_TYPE",true).ConfigureAwait(false);

            return response;
        }
        public async Task<Tuple<DeclarionInformationInquireResponse, bool, string>> GetDcltnBusID(int portNo,int billNo,string date,int dclType)
        {
            var response = await HttpManager.GetAsync<DeclarionInformationInquireResponse>(App.CustomBaseUrl + $"Declaration/GetDcltnBusID/{portNo}/{billNo}/{date}?dcltnType={dclType}",true,portNo.ToString()).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<DeclarationFeesResponse, bool, string>> GetDclFees(int portNo, int billNo, string date, int dclType)
        {
            var response = await HttpManager.GetAsync<DeclarationFeesResponse>(App.CustomBaseUrl + $"Declaration/GetDeclarationFees/{portNo}/{billNo}/{date}?dcltnType={dclType}", true, portNo.ToString()).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<StatmentItemsResponse, bool, string>> GetDclStatmentItems(int portNo, int dcltnISN, int dclType)
        {
            var response = await HttpManager.GetAsync<StatmentItemsResponse>(App.CustomBaseUrl + $"Declaration/GetAllItemsForDeclaration/{portNo}/{dcltnISN}/{dclType}", true, portNo.ToString()).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<InquireByBillInfoResponse, bool, string>> GetDeclarationInfoByBill(int portNo,string billNo, int CarrPrefix)
        {
            var response = await HttpManager.GetAsync<InquireByBillInfoResponse>(App.CustomBaseUrl + $"Declaration/GetDeclarationInfoByBill/{portNo}/{CarrPrefix}/{billNo}", true, portNo.ToString()).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<CustomItemCalcDescription, bool, string>> GetItemsCalcTxt(int portNo,int Dcltype, string DClISn)
        {
           /* portNo = 31;
            Dcltype = 1;
            DClISn = "4296";*/
            var response = await HttpManager.GetAsync<CustomItemCalcDescription>(App.CustomBaseUrl + $"Declaration/GetItemCalcMthdTxt/{portNo}/{Dcltype}/{DClISn}", true, portNo.ToString()).ConfigureAwait(false);

            return response;
        }

    }
}
