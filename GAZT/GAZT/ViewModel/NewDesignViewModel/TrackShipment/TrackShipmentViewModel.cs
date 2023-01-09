using System;
using EGAZT.Services.Classes;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
	public class TrackShipmentViewModel : BaseViewModel
    {
        public TrackShipmentViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

