using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    [Preserve(AllMembers = true)]
    public class TaxEvasionPageWebViewModel : BaseViewModel
    {
        public string URI { get; set; }
        public TaxEvasionPageWebViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

    }
}
