using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.MyReportsModel;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.MyReportsVM
{
    public class MyReportsViewModel : BaseViewModel
    {
        #region Properties
        private readonly IMyReportsServices _myReportsServices;

        private string reportsCount;
        public string ReportsCount { get { return reportsCount; } set { reportsCount = value; RaisePropertyChanged(); } }

        private MyReportsModel myReports = new MyReportsModel();
        public MyReportsModel MyReports { get { return myReports; } set { myReports = value; RaisePropertyChanged(); } }

        private bool isFilterReportView;
        public bool IsFilterReportView { get { return isFilterReportView; } set { isFilterReportView = value; RaisePropertyChanged(); } }

        ObservableCollection<MyReportsModel> myReportsList = new ObservableCollection<MyReportsModel>();
        public ObservableCollection<MyReportsModel> MyReportsList { get { return myReportsList; } set { myReportsList = value; RaisePropertyChanged(); } }

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
                    IsLoading = true;
                    var result = await _myReportsServices.GetMyReports("0563018294", 1, 25) ?? new List<MyReportsModel>();
                    MyReportsList = new ObservableCollection<MyReportsModel>(result);
                    ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                    IsLoading = false;

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

                    var reportDetails = report as MyReportsModel;
                    if (reportDetails != null)
                    {
                        reportDetails.ReportLocation = $"{reportDetails.latitude},{reportDetails.longitude},{reportDetails.region}";
                        _navigationService.NavigateTo("MyReportDetailsPage", reportDetails);
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
                    int status = -1;
                    switch (selectedFilter ?? "-1")
                    {
                        case "0":
                            status = 0;
                            break;
                        case "1":
                            status = 1;
                            break;
                        case "3":
                            status = 3;
                            break;
                        default:
                            status = -1;
                            break;

                    }

                    IsFilterReportView = false;
                    IsLoading = true;
                    var result = await _myReportsServices.GetMyReports("0563018294", status, 1, 25) ?? new List<MyReportsModel>();
                    MyReportsList = new ObservableCollection<MyReportsModel>(result);
                    ReportsCount = $"{MyReportsList?.Count} {AppResources.Reports}";
                    IsLoading = false;

                });

            }
        }
        #endregion Commands
    }
}

