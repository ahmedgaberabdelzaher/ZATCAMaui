using System;
using EGAZT.Models.TrackShipment;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public partial class TrackShipmentViewModel
    {
        public bool isSeaCardSelected;

        private async Task GetSeaShipping(bool IsAPIForDeclaration)
        {
            try
            {
                if (IsValid(ShipmentCards.Sea))
                {
                    IsLoading = true;
                    Tuple<Models.BaseModels.DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result;

                    if (IsAPIForDeclaration)

                        result = await _trackShipment.GetSeaShippingDeclaration(SelectedPortId, ShipmentDeclarationNumber, DeclarationDateString);
                    else
                        result = await _trackShipment.GetSeaShippingBill(SelectedPortId, ShipmentBillNumber, ShipmentContainerNumber);

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

        private void DrawSeaShipping()
        {
            isSeaCardSelected = true;
            HeaderTitle = AppResources.SeaFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.SeaFreight}";
            DrawShipmentTrack.ShipmentCardImage = "seaDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Sea);
        }
    }
}

