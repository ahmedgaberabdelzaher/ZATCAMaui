using System;
using EGAZT.Models.TrackShipment;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public partial class TrackShipmentViewModel
    {
        public bool isAirCardSelected;

        private async Task GetAirShipping(bool IsAPIForDeclaration)
        {
            try
            {
                if (IsValid(ShipmentCards.Air))
                {
                    IsLoading = true;
                    Tuple<Models.BaseModels.DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result;

                    if (IsAPIForDeclaration)

                        result = await _trackShipment.GetAirShippingDeclaration(SelectedPortId, ShipmentDeclarationNumber, DeclarationDateString);
                    else
                        result = await _trackShipment.GetAirShippingBill(SelectedPortId, ShipmentBillNumber);

                    FillDataFromAPI(result);

                    IsLoading = false;
                }


            }
            catch (Exception ex)
            {
                IsLoading = false;
                IsShowMsgView = true;
                MessageTxt = AppResources.RequestTimeoutDescription;
            }

        }

        private void DrawAirShipping()
        {
            isAirCardSelected = true;
            HeaderTitle = AppResources.AirFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.AirFreight}";
            DrawShipmentTrack.ShipmentCardImage = "airDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Air);
        }
    }
}

