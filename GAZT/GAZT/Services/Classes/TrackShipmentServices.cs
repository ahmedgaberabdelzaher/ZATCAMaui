using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Models.TrackShipment;
using EGAZT.Models.BaseModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class TrackShipmentServices : ITrackShipment
    {
        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetAirShippingBill(int portCode, string billNumber)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/air-shipping-bill?portCode={portCode}&billNumber={billNumber}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetAirShippingDeclaration(int portCode, string declarationNumber, string declarationDate)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/air-shipping-declaration?portCode={portCode}&declarationNumber={declarationNumber}&declarationDate={declarationDate}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetExpressShippingBill(string billNumber)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/express-shipping-bill?billNumber={billNumber}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetExpressShippingDeclaration(string declarationNumber)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/express-shipping-declaration?declarationNumber={declarationNumber}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetLandShippingDeclaration(int portCode, string declarationNumber, string declarationDate)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/land-shipping-declaration?portCode={portCode}&declarationNumber={declarationNumber}&declarationDate={declarationDate}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetSeaShippingBill(int portCode, string billNumber, string containerNumber)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/sea-shipping-bill?portCode={portCode}&billNumber={billNumber}&containerNumber={containerNumber}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetSeaShippingDeclaration(int portCode, string declarationNumber, string declarationDate)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/sea-shipping-declaration?portCode={portCode}&declarationNumber={declarationNumber}&declarationDate={declarationDate}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetTrainShippingBill(int portCode, string billNumber, string containerNumber)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/train-shipping-bill?portCode={portCode}&billNumber={billNumber}&containerNumber={containerNumber}").ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetTrainShippingDeclaration(int portCode, string declarationNumber, string declarationDate)
        {
            var response = await Helper.HttpManager.GetAsync<DATAPowerBaseResponse<TrackShipmentModel>>($"{PageSettings.ZATCABaseURL}v1/references/customs/shipment-tracking/train-shipping-declaration?portCode={portCode}&declarationNumber={declarationNumber}&declarationDate={declarationDate}").ConfigureAwait(false);
            return response;
        }
    }
}

