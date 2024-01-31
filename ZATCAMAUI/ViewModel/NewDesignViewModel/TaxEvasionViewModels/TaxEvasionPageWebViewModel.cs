using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{

    public class TaxEvasionPageWebViewModel : BaseViewModel
    {
        public string URI { get; set; }
        public bool NeedBackToreport { get; set; }
        public TaxEvasionPageWebViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

    }
}
