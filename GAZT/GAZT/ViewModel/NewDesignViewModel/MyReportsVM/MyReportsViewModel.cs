using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.BaseModels;
using EGAZT.Models.MyReportsModel;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.MyReportsVM
{
    public class MyReportsViewModel : BaseViewModel
    {
        #region Properties
        private readonly IMyReportsServices _myReportsServices;

        private string reportsCount= $"0 {AppResources.Reports}";
        public string ReportsCount { get { return reportsCount; } set { reportsCount = value; RaisePropertyChanged(); } }

        private string searchValue;
        public string SearchValue { get { return searchValue; } set { searchValue = value; RaisePropertyChanged(); } }

        private string reportsResultTitle = AppResources.AllReports;
        public string ReportsResultTitle { get { return reportsResultTitle; } set { reportsResultTitle = value; RaisePropertyChanged(); } }

        private MyReportsModel myReports = new MyReportsModel();
        public MyReportsModel MyReports { get { return myReports; } set { myReports = value; RaisePropertyChanged(); } }

        private bool isFilterReportView;
        public bool IsFilterReportView { get { return isFilterReportView; } set { isFilterReportView = value; RaisePropertyChanged(); } }

        private bool isSearching;
        public bool IsSearching { get { return isSearching; } set { isSearching = value; RaisePropertyChanged(); } }

        ObservableCollection<MyReportsModel> myReportsList = new ObservableCollection<MyReportsModel>();
        public ObservableCollection<MyReportsModel> MyReportsList { get { return myReportsList; } set { myReportsList = value; RaisePropertyChanged(); } }

        public string PhoneNumber;
        private int? status = null;
        private int pageNumber = 1;
        private ReportsResult reportsAPIResult = new ReportsResult ();
        GenericPickerModel genericPickerModel = new GenericPickerModel();
        #endregion


        public MyReportsViewModel(IMyReportsServices myReportsServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this._myReportsServices = myReportsServices;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                SelectedFilterItemCommand.Execute(arg.SelectedValue);

            });
        }


        #region Commands
        public ICommand GetMyReportsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        reportsAPIResult = await _myReportsServices.GetMyReports(PhoneNumber) ?? new ReportsResult();
                        pageNumber = 1;
                        MyReportsList = new ObservableCollection<MyReportsModel>(reportsAPIResult?.reportTaxTypes);
                        ReportsResultTitle = AppResources.AllReports;
                        ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                        IsLoading = false;
                    }
                    catch (System.Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }
                   

                });

            }
        }
        public ICommand LoadMoreMyReportsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (pageNumber >= reportsAPIResult?.pagesCount) return;
                        IsLoading = true;
                        reportsAPIResult = await _myReportsServices.GetMyReports(PhoneNumber,status,pageNumber: ++pageNumber) ?? new ReportsResult();

                        foreach (var item in reportsAPIResult?.reportTaxTypes)
                        {
                            MyReportsList.Add(item);
                        }

                        ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                        IsLoading = false;
                    }
                    catch (System.Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }
                   

                });

            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return new Command(async() =>
                {
                    
                    List<string> filterData = new List<string>() {
                        AppResources.AllReports, AppResources.MyOpenedReports,
                        /*AppResources.ZReportStatusInprogress,*/ AppResources.MyClosedReports};

                    genericPickerModel.PickerData = filterData;
                    genericPickerModel.PickerTitle = string.Empty;
                    genericPickerModel.PickerId = "filterMyReportsDataPicker";
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                });

            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(() =>
                {
                   
                    IsSearching = IsSearching == true ? false : true;
                });

            }
        }
        public ICommand SearchReportCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        if (IsSearching && !string.IsNullOrWhiteSpace(SearchValue))
                        {
                            IsSearching = false;
                            IsLoading = true;
                            reportsAPIResult = await _myReportsServices.GetMyReports(PhoneNumber,status,SearchValue.ToLower()) ?? new ReportsResult();
                            pageNumber = 1;
                            MyReportsList = new ObservableCollection<MyReportsModel>(reportsAPIResult?.reportTaxTypes);
                            ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                            IsLoading = false;
                            SearchValue = string.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;

                    }
                   
                });

            }
        }
        public ICommand AddNewReportCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("SubmitReportPage");
                    
                });

            }
        }
        public ICommand SelectedReportItemCommand
        {
            get
            {
                return new Command((report) =>
                {
                    try
                    {
                        var reportDetails = report as MyReportsModel;
                        if (reportDetails != null)
                        {
                            reportDetails.ReportLocation = $"{reportDetails.latitude},{reportDetails.longitude},{reportDetails.region}";
                            _navigationService.NavigateTo("MyReportDetailsPage", reportDetails);
                        }
                    }
                    catch (Exception)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.Somethingwentwrong;
                    }
                   

                });

            }
        }

        public ICommand SelectedFilterItemCommand
        {
            get
            {
                return new Command(async (selectedFilter) =>
                {
                    try
                    {
                        var value = selectedFilter as string;
                        checkReportStatus(value);
                        IsLoading = true;
                        reportsAPIResult = await _myReportsServices.GetMyReports(PhoneNumber, status) ?? new ReportsResult();
                        pageNumber = 1;
                        MyReportsList = new ObservableCollection<MyReportsModel>(reportsAPIResult?.reportTaxTypes);
                        ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                        IsLoading = false;
                    }
                    catch (System.Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }
                   

                });

            }
        }
        #endregion Commands

        private void checkReportStatus(string selectedFilter)
        {
            if (selectedFilter.Equals(AppResources.MyClosedReports))
            {
                status = 3;
                ReportsResultTitle = AppResources.MyClosedReports;
            }
            else if (selectedFilter.Equals(AppResources.MyOpenedReports))
            {
                status = 1;
                ReportsResultTitle = AppResources.MyOpenedReports;
            }
            else if (selectedFilter.Equals(AppResources.ZReportStatusInprogress))
            {
                status = 2;
                ReportsResultTitle = AppResources.ZReportStatusInprogress;
            }
            else
            {
                status = null;
                ReportsResultTitle = AppResources.AllReports;
            }
        }
    }
}

