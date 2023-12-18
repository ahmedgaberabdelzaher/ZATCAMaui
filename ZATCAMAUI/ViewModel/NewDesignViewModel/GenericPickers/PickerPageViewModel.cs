using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.GenericPickers
{

    public class PickerPageViewModel : BaseViewModel
    {
        #region Constructor
        public PickerPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            PickerItemSource = new List<string>();
            DataSource = new GenericPickerModel();
        }
        #endregion
        //GenericPickerModel
        private GenericPickerModel _dataSource { get; set; }
        public GenericPickerModel DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                RaisePropertyChanged("DataSource");
            }
        }

        private List<string> _pickerItemSource { get; set; }
        public List<string> PickerItemSource
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

        private string _pickerTitle { get; set; }
        public string PickerTitle
        {
            get
            {
                return _pickerTitle;
            }
            set
            {
                _pickerTitle = value;
                RaisePropertyChanged("PickerItemSource");
            }
        }

        private string _selectedItem { get; set; }
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private int _selectedItemIndex { get; set; }
        public int SelectedItemIndex
        {
            get
            {
                return _selectedItemIndex;
            }
            set
            {
                _selectedItemIndex = value;
                RaisePropertyChanged("SelectedItemIndex");
            }
        }
    }
}
