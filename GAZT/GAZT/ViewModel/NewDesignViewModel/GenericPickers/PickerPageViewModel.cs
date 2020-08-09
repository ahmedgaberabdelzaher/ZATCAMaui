using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.GenericPickers
{
    public class PickerPageViewModel:BaseViewModel
    {
        #region Constructor
        public PickerPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            PickerItemSource = new ObservableCollection<string>();
        }
        #endregion

        private ObservableCollection<string> _pickerItemSource;
        public ObservableCollection<string> PickerItemSource
        {
            get
            {
                return _pickerItemSource;
            }
            set
            {
                _pickerItemSource = value;
                RaisePropertyChanged("PickerItemSource");
            }
        }
    }
}
