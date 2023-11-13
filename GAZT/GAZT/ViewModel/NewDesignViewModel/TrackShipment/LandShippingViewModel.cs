using System;
using EGAZT.Models.TrackShipment;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public partial class TrackShipmentViewModel
    {

        public bool isLandCardSelected;



        private async Task GetLandShipping()
        {
            try
            {
                if (IsValid(ShipmentCards.Land))
                {
                    IsLoading = true;

                    var result = await _trackShipment.GetLandShippingDeclaration(SelectedPortId, ShipmentDeclarationNumber, DeclarationDateString);

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


        private void DrawLandShipping()
        {
            isLandCardSelected = true;
            HeaderTitle = AppResources.LandFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.LandFreight}";
            DrawShipmentTrack.ShipmentCardImage = "landDark.png";
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasDeclarationNumber = true;
            DrawShipmentTrack.HasDeclarationDate = true;
        }


    }
}

