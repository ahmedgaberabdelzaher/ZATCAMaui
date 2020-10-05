using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class ZakatRegistrationTaxPayerDetailsPageViewModel: EstablishmentRegistrationPageViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        public ZakatRegistrationTaxPayerDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }

        

        public async Task LoadDataTaxPayerDetails()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            try
            {
                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.RegistrationType);
                });
                
            }
            catch(Exception ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }

            try
            {
                await Task.Run(async () =>
                {
                    await FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum.TaxpayerDetail);
                }); 
            }
            catch(Exception ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            await Task.Run(() =>
            {
                App.HideProgressView();
            });
        }
    }
}
