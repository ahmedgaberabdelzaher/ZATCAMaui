using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM
{
    [Preserve(AllMembers = true)]
    public class FilterVatEffectiveDatePageViewModel : BaseViewModel
    {

        public ICommand GoBackBtnTapped { get; set; }
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
                RaisePropertyChanged("SelectedDateSortText");
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
                RaisePropertyChanged("SelectedUpdatedSortText");
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
                RaisePropertyChanged("PickerModel");
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
                RaisePropertyChanged("FilterList");
            }
        }

        public FilterVatEffectiveDatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });

            FilterCloseClick = new Command(() =>
            {
                _navigationService.GoBack();
            });

            FilterBtnCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopAsync();
                MessagingCenter.Send<App, List<VatEffectDateFilterModel>>((App)Xamarin.Forms.Application.Current, "filterList", FilterList);
                //MessagingCenter.Unsubscribe<App, List<VatEffectDateFilterModel>>(this, "filterList");

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
            catch (Exception ex)
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
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
                }
                else if (pickerID == 2)
                {
                    try
                    {
                        setUpdatedBySortTypePickerModel();
                        await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }
}
