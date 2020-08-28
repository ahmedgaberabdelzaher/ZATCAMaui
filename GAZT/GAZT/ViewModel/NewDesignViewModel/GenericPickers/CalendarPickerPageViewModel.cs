using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel
{
    public class CalendarPickerPageViewModel:BaseViewModel
    {
        #region Constructor
        public CalendarPickerPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
        #endregion

        public int DefaultMonth;
        private GenericDatePickerModel _dataSource { get; set; }
        public GenericDatePickerModel DataSource
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
        private ObservableCollection<string> _pickerItemSource { get; set; }
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

        private string _datePickerTitle { get; set; }
        public string DatePickerTitle
        {
            get
            {
                return _datePickerTitle;
            }
            set
            {
                _datePickerTitle = value;
                RaisePropertyChanged("DatePickerTitle");
            }
        }

        private bool _isFutureDatePickerVisible = false;
        public bool IsFutureDatePickerVisible
        {
            get
            {
                return _isFutureDatePickerVisible;
            }
            set
            {
                _isFutureDatePickerVisible = value;
                RaisePropertyChanged("IsFutureDatePickerVisible");
            }
        }

        private bool _isCurrentDatePickerVisible = true;
        public bool IsCurrentDatePickerVisible
        {
            get
            {
                return _isCurrentDatePickerVisible;
            }
            set
            {
                _isCurrentDatePickerVisible = value;
                RaisePropertyChanged("IsCurrentDatePickerVisible");
            }
        }

        private ObservableCollection<object> _selectedDate;
        public ObservableCollection<object> SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        public async Task SetDefaultDate()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            //Select today dates

            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            if (DateTime.Now.Date.Month < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Month);
            else
                todaycollection.Add(DateTime.Now.Date.Month.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());
            SelectedDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;
        }
    }
}
