using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Interfaces;
using System.Windows.Input;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM
{
    public class FilterVatEffectiveDatePageViewModel : BaseViewModel
    {

        public ICommand FilterCloseClick { get; set; }
        public ICommand FilterBtnCommand { get; set; }
        public ICommand FiltersTapped { get; set; }
        public ICommand ShowDateSortTypePicker { get; set; }
        public ICommand ShowUpdatedByTypePicker { get; set; }


        private string _selectedDateSortText = string.Empty;
        public string SelectedDateSortText
        {
            get { return _selectedDateSortText; }
            set
            {
                if (_selectedDateSortText == value) return;

                _selectedDateSortText = value;
                OnPropertyChanged("SelectedDateSortText");
            }
        }




        private string _selectedUpdatedSortText = string.Empty;
        public string SelectedUpdatedSortText
        {
            get { return _selectedUpdatedSortText; }
            set
            {
                if (_selectedUpdatedSortText == value) return;

                _selectedUpdatedSortText = value;
                OnPropertyChanged("SelectedUpdatedSortText");
            }
        }


        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                OnPropertyChanged("PickerModel");
            }
        }

        private List<VatEffectDateFilterModel> _filterList = new List<VatEffectDateFilterModel>();
        public List<VatEffectDateFilterModel> FilterList
        {
            get { return _filterList; }
            set
            {
                if (_filterList == value) return;

                _filterList = value;
                OnPropertyChanged("FilterList");
            }
        }

        public FilterVatEffectiveDatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
           

            FilterCloseClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            FilterBtnCommand = new Command(() =>
            {
                try
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                    MessagingCenter.Send<App, List<VatEffectDateFilterModel>>((App)
                        Application.Current, "filterList", FilterList);


                }
                catch (Exception )
                {
                }


            });
            FiltersTapped = new Command(() =>
            {
                FiltersClicked();
            });

            ShowDateSortTypePicker = new Command(() => { showPickerDialog(2); });
            ShowUpdatedByTypePicker = new Command(() => { showPickerDialog(1); });
        }



        private void FiltersClicked()
        {
            _navigationService.NavigateTo(App.AccountStatementsNewFilterPageView);
        }

        private void setUpdatedBySortTypePickerModel()
        {
            if (PickerModel != null)
            {
                PickerModel = null;
            }
            var list = new List<string>();
            try
            {
                list.Add(AppResources.VatEffTaxPayer);
                list.Add(AppResources.VatEffGazt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "UpdatedBySortTypePicker";
            PickerModel = genericPickerModel;
        }

        public class VatEffectDateFilterModel
        {
            public int filterId { get; set; }
            public string filterName { get; set; }
            public string filterType { get; set; }

        }

        private void setDateSortTypePickerModel()
        {

            if (PickerModel != null)
            {
                PickerModel = null;
            }
            var list = new List<string>();
            try
            {
                list.Add(AppResources.EffectSortfromOldToNew);
                list.Add(AppResources.EffectSortfromNewToOld);
            }
            catch (Exception )
            {

            }
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = list;
            genericPickerModel.PickerTitle = "";
            genericPickerModel.PickerId = "DateSortTypePicker";
            PickerModel = genericPickerModel;
        }

        private async void showPickerDialog(int pickerID)
        {
            try
            {

                if (pickerID == 1)
                {

                    setDateSortTypePickerModel();
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
                }
                else if (pickerID == 2)
                {
                    try
                    {
                        setUpdatedBySortTypePickerModel();
                        await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }
}


