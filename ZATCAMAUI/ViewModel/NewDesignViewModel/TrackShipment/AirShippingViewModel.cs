using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.TrackShipment;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment
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
                    Tuple<DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result;

                    if (IsAPIForDeclaration)

                        result = await _trackShipment.GetAirShippingDeclaration(SelectedPortId, ShipmentDeclarationNumber, DeclarationDateString);
                    else
                        result = await _trackShipment.GetAirShippingBill(SelectedPortId, ShipmentBillNumber);

                    FillDataFromAPI(result);

                    IsLoading = false;
                }


            }
            catch (Exception)
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

