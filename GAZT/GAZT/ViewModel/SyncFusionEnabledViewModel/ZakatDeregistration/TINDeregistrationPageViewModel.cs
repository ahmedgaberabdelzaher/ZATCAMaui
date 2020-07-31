using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class TINDeregistrationPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion

        public TINDeregistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            TinDeregistrationModel = new TINDeregistrationModel();
            AddOutletDecisionOptions();
        }

        public TINDeregistrationModel tinDeregistrationModel { get; set; }
        public TINDeregistrationModel TinDeregistrationModel
        {
            get
            {
                return tinDeregistrationModel;
            }

            set
            {
                if (tinDeregistrationModel == value)
                {
                    return;
                }

                tinDeregistrationModel = value;
                RaisePropertyChanged("TinDeregistrationModel");
            }
        }

        public ObservableCollection<TINDeregistrationModel> outletDecisionOptions { get; set; }
        public ObservableCollection<TINDeregistrationModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        public ObservableCollection<TINDeregistrationModel> c { get; set; }


        public void AddOutletDecisionOptions()
        {
            OutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>();
            OutletDecisionOptions.Add(new TINDeregistrationModel
            {
                ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseAllOutlets,
                ActiveOutletDecisionOptionsIsSelected = true
            });
            OutletDecisionOptions.Add(new TINDeregistrationModel
            {
                ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions.Add(new TINDeregistrationModel
            {
                ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOutletsIndividually,
                ActiveOutletDecisionOptionsIsSelected = false
            });
        }
    }
}
