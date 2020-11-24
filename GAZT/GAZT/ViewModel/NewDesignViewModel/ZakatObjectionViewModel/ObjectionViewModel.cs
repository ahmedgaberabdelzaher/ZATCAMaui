using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel
{
    public class ObjectionViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand GoBackClick { get; set; }

        public class SelectionModel
        {
            public SelectionModel()
            {
            }

            public string SelectionTitle { get; set; }
            public bool IsSelected { get; set; }
        }

        public ObservableCollection<SelectionModel> selectionOptions { get; set; }

        public ObservableCollection<SelectionModel> SelectionOptions
        {
            get { return selectionOptions; }

            set
            {
                if (selectionOptions == value)
                {
                    return;
                }

                selectionOptions = value;
                RaisePropertyChanged("SelectionOptions");
            }
        }

        public ObjectionViewModel(INavigationService navigationService, IDialogService dialogService)
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

            GoBackClick = new Command(async () => { _navigationService.GoBack(); });
        }

        public void AddSelectionOptions()
        {
            var outletDecisionOptions = new ObservableCollection<SelectionModel>();
            if (App.LoginDataRetrieved != null && App.LoginDataRetrieved.VtReg == "X")
            {
                outletDecisionOptions.Add(new SelectionModel
                {
                    SelectionTitle = AppResources.DBSMVATObjection,
                    IsSelected = false
                });
            }

            if (App.LoginDataRetrieved != null && App.LoginDataRetrieved.ZkReg == "X")
            {
                outletDecisionOptions.Add(new SelectionModel
                {
                    SelectionTitle = AppResources.DBSMZAKATObjection,
                    IsSelected = false
                });
            }

            SelectionOptions = outletDecisionOptions;
        }
    }
}
