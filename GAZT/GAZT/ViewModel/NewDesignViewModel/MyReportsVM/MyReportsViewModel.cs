using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.MyReportsModel;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
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
        #endregion


        public MyReportsViewModel(IMyReportsServices myReportsServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this._myReportsServices = myReportsServices;
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
                        var result = await _myReportsServices.GetMyReports(PhoneNumber) ?? new List<MyReportsModel>();
                        MyReportsList = new ObservableCollection<MyReportsModel>(result);
                        ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                        IsLoading = false;
                    }
                    catch (System.Exception ex)
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
                return new Command(() =>
                {
                    IsFilterReportView = true;
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
                            var result = await _myReportsServices.GetSearcedMyReports(PhoneNumber, SearchValue.ToLower()) ?? new List<MyReportsModel>();
                            MyReportsList = new ObservableCollection<MyReportsModel>(result);
                            ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                            IsLoading = false;
                            SearchValue = string.Empty;
                        }
                    }
                    catch (Exception ex)
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
                    catch (Exception ex)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.Somethingwentwrong;
                    }
                   

                });

            }
        }
        public ICommand ClosBottomSheetCommand
        {
            get
            {
                return new Command(() => { IsFilterReportView = false; });

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
                        int? status = null;
                        switch (selectedFilter)
                        {
                            case "3":
                                status = 3;
                                ReportsResultTitle = AppResources.MyClosedReports;
                                break;
                            case "0":
                                status = 0;
                                ReportsResultTitle = AppResources.MyClosedReports;
                                break;
                            case "1":
                                status = 1;
                                ReportsResultTitle = AppResources.MyOpenedReports;
                                break;
                            case "2":
                                status = 2;
                                ReportsResultTitle = AppResources.ZReportStatusInprogress;
                                break;
                            default:
                                status = null;
                                ReportsResultTitle = AppResources.AllReports;
                                break;

                        }

                        IsFilterReportView = false;
                        IsLoading = true;
                        var result = await _myReportsServices.GetMyReports(PhoneNumber, status) ?? new List<MyReportsModel>();
                        MyReportsList = new ObservableCollection<MyReportsModel>(result);
                        ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                        IsLoading = false;
                    }
                    catch (System.Exception ex)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }
                   

                });

            }
        }
        #endregion Commands
    }
}

