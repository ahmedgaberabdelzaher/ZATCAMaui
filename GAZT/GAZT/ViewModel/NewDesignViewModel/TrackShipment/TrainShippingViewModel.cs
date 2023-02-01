using System;
using EGAZT.Models.TrackShipment;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public partial class TrackShipmentViewModel
    {
        public bool isTrainCardSelected;

        private void DrawTrainShipping()
        {
            isTrainCardSelected = true;
            HeaderTitle = AppResources.TrainFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.TrainFreight}";
            DrawShipmentTrack.ShipmentCardImage = "trainDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Train);
        }

        private async Task GetTrainShipping(bool IsAPIForDeclaration)
        {
            try
            {
                if (IsValid(ShipmentCards.Train))
                {
                    IsLoading = true;
                    Tuple<Models.BaseModels.DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result;

                    if (IsAPIForDeclaration)

                        result = await _trackShipment.GetTrainShippingDeclaration(SelectedPortId, ShipmentDeclarationNumber, DeclarationDateString);
                    else
                        result = await _trackShipment.GetTrainShippingBill(SelectedPortId, ShipmentBillNumber, ShipmentContainerNumber);

                    FillDataFromAPI(result);

                    IsLoading = false;

                    _navigationService.NavigateTo("/ShipmentStatusPage");
                }


            }
            catch (Exception ex)
            {
                IsLoading = false;
                IsShowMsgView = true;
                MessageTxt = AppResources.RequestTimeoutDescription;
            }

        }

    }
}

