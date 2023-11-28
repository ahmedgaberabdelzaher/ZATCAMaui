using System;
using EGAZT.Models.TrackShipment;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public partial class TrackShipmentViewModel
    {
        public bool isExpressCardSelected;

        private void DrawExpressShipping()
        {
            isExpressCardSelected = true;
            HeaderTitle = AppResources.ExpressShipping;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.ExpressShipping}";
            DrawShipmentTrack.ShipmentCardImage = "expressDark.png";
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Express);
        }

        private async Task GetExpressShipping(bool IsAPIForDeclaration)
        {
            try
            {
                if (IsValid(ShipmentCards.Express))
                {
                    IsLoading = true;
                    Tuple<Models.BaseModels.DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result;

                    if (IsAPIForDeclaration)

                        result = await _trackShipment.GetExpressShippingDeclaration(ShipmentDeclarationNumber);
                    else
                        result = await _trackShipment.GetExpressShippingBill(ShipmentBillNumber);

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
    }
}

