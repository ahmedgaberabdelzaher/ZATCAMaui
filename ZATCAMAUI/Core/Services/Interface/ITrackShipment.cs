using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.TrackShipment;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ITrackShipment
    {
        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetAirShippingBill(int portCode, string billNumber);
        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetAirShippingDeclaration(int portCode, string declarationNumber, string declarationDate);

        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetExpressShippingBill(string billNumber);
        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetExpressShippingDeclaration(string declarationNumber, string importYear);

        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetLandShippingDeclaration(int portCode, string declarationNumber, string declarationDate);

        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetSeaShippingBill(int portCode, string billNumber, string containerNumber);
        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetSeaShippingDeclaration(int portCode, string declarationNumber, string declarationDate);

        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetTrainShippingBill(int portCode, string billNumber, string containerNumber);
        Task<Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string>> GetTrainShippingDeclaration(int portCode, string declarationNumber, string declarationDate);
    }
}

