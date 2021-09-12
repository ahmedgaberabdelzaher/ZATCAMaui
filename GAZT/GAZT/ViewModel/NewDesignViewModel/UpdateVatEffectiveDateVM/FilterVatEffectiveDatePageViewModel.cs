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

        public ObservableCollection<ASFilters> _filterList = null;
        public ObservableCollection<ASFilters> FilterList
        {
            get
            {
                return _filterList;
            }
            set
            {
                if (_filterList == value) return;
                _filterList = value;
                RaisePropertyChanged("FilterList");
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
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AcFilterEmptyState));
               /* if (string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) && string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) && string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                {



                }
                else
                {

                    isFromFilter = true;
                    FilterIfTypeAndStausFilterSelected(false);
                }*/




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
                        setDateSortTypePickerModel();
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
