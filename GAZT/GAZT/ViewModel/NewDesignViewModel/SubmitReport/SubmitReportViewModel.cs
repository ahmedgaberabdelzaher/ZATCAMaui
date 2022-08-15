using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.SubmitReport
{
    public class SubmitReportViewModel : BaseViewModel
    {
        private readonly ISubmitReportServices _submitReportServices;
        SubmitReportModel submitReport = new SubmitReportModel();

        public SubmitReportModel SubmitReport { get { return submitReport; } set { submitReport = value; RaisePropertyChanged(); } }

        bool isTherePDFUploaded;
        public bool IsTherePDFUploaded { get { return isTherePDFUploaded; } set { isTherePDFUploaded = value; RaisePropertyChanged(); } }

        bool isShowMapView;
        public bool IsShowMapView { get { return isShowMapView; } set { isShowMapView = value; RaisePropertyChanged(); } }

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        bool hasError;
        public bool HasError { get { return hasError; } set { hasError = value; RaisePropertyChanged(); } }

        bool isNeedRewardError;
        public bool IsNeedRewardError { get { return isNeedRewardError; } set { isNeedRewardError = value; RaisePropertyChanged(); } }

        string reportNumberResult;
        public string ReportNumberResult { get { return reportNumberResult; } set { reportNumberResult = value; RaisePropertyChanged(); } }

        ObservableCollection<ReportFileModel> reportUloadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> ReportUloadedFiles { get { return reportUloadedFiles; } set { reportUloadedFiles = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> bottomSheetList;
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        private bool isReportTypeSelected = false;
        private bool isReportCategorySelected = false;
        private bool isRegionSelected = false;
        private List<BaseRegionAndCity> CitysList;
        private List<BaseRegionAndCity> RegionsList;
        private List<CategoryDataResponse> ReportCategory;

        public ICommand SendReportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (IsValidateReport())
                        {
                            IsLoading = true;
                            //SubmitReport.ViolationDate = Convert.ToDateTime(SubmitReport.ViolationDate ?? "1994-01-01 00:00:00", new CultureInfo("en-US")).ToString("d'/'M'/'yyyy");
                            var date = DateTime.Parse(SubmitReport.ViolationDate);
                            string dt = date.Date.ToString("dd/MM/yyyy");
                            submitReport.ViolationDate = dt;
                            var json = JsonConvert.SerializeObject(SubmitReport);
                            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                            var reportResult = await this._submitReportServices.CreateZatcaNewReport(dictionary, ReportUloadedFiles);
                            IsLoading = false;
                            if (reportResult.Success)
                            {
                                ReportNumberResult = reportResult.Result?.Data;
                                _navigationService.NavigateTo("ReportSuccessPage");
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                    }
                 
                });
            }
        }

        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" });


                });
            }
        }

        public ICommand CloseMapCommand
        {
            get
            {
                return new Command(() => { IsShowMapView = false; });

            }
        }

        public ICommand ClosBottomSheetCommand
        {
            get
            {
                return new Command(() => { IsShowBottomSheet = false; });

            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new Command(() => { IsShowMapView = true; });

            }
        }

        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<ReportFileModel>((e) =>
                {

                    if (e != null && ReportUloadedFiles != null && ReportUloadedFiles.Count > 0)
                    {
                        ReportUloadedFiles.Remove(e);

                        if (ReportUloadedFiles.Count == 0) IsTherePDFUploaded = false;
                    }
                });
            }
        }

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>(async (e) =>
                {
                    IsLoading = true;

                    if (isReportTypeSelected)
                    {
                        SubmitReport.ReportTypeName = e.Name;
                        SubmitReport.ReportTaxType = e.Id;
                        ReportCategory = await this._submitReportServices.GetReportCategories(SubmitReport?.ReportTaxType);
                        var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        isReportTypeSelected = false;
                    }
                    else if (isReportCategorySelected)
                    {
                        SubmitReport.ReportCategoryName = e.Name;
                        SubmitReport.ReportCategory = e.Id;
                        isReportCategorySelected = false;
                    }
                    else if (isRegionSelected)
                    {
                        SubmitReport.Region = e.Name;
                        SubmitReport.RegionCode = e.Id;
                        CitysList = await this._submitReportServices.GetCities(SubmitReport?.RegionCode);
                        isRegionSelected = false;
                    }
                    else
                    {
                        SubmitReport.City = e.Name;
                        SubmitReport.CityCode = e.Id;

                    }

                    IsShowBottomSheet = false;
                    IsLoading = false;

                });
            }
        }

        public ICommand OpenReportTypeCommand
        {
            get
            {

                return new Command(() =>
                {
                    isReportTypeSelected = true;
                    BottomSheetList = new ObservableCollection<BottomSheetModel>()
                    {
                        new BottomSheetModel {Id= "V1", Name = AppResources.VATCertificates},
                        new BottomSheetModel {Id= "V2", Name =AppResources.ExciseCertificates},
                        new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
                    };
                    IsShowBottomSheet = true;
                });

            }
        }

        public ICommand OpenReportCategoryCommand
        {
            get
            {
                return new Command(() =>
                {
                    isReportCategorySelected = true;
                    IsShowBottomSheet = true;
                    if (submitReport.ReportTypeName == null)
                        OpenReportTypeCommand.Execute(null);
                    else
                    {
                        var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    }
                        
                });
            }
        }

        public ICommand OpenRegionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsLoading = true;
                    isRegionSelected = true;
                    if (RegionsList == null)
                        RegionsList = await this._submitReportServices.GetRegions();

                    var result = RegionsList?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    IsLoading = false;
                });
            }
        }

        public ICommand OpenCityCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (submitReport.Region != null)
                    {
                        IsLoading = true;

                        if(CitysList == null)
                            CitysList = await this._submitReportServices.GetCities(SubmitReport?.RegionCode);

                        var result = CitysList?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        IsLoading = false;
                    }
                    else OpenRegionCommand.Execute(null);

                });
            }
        }

        public SubmitReportViewModel(ISubmitReportServices submitReportServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this._submitReportServices = submitReportServices;
        }

        private async Task PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                    {

                        var lenght = new FileInfo(result.FullPath).Length;
                        double LenghtInKb = lenght / 1024;
                        double LenInMb = LenghtInKb / 1024;
                        double size = LenInMb;
                        double filesize = size;

                        if (filesize > 5)
                        {
                            MessageTxt = AppResources.MaximumFileSizeMsg;
                            IsShowMsgView = true;
                        }
                        else if (ReportUloadedFiles != null && ReportUloadedFiles.Count >= 0 && ReportUloadedFiles.Count <= 5)
                        {
                            
                            var stream = await result.OpenReadAsync();
                           // string content = ConvertToBase64(stream);
                            ReportFileModel reportfile = new ReportFileModel();
                            reportfile.filecontentStream = stream;
                            reportfile.filename = result.FileName;
                            reportfile.FileSize = Math.Round(size, 2);
                            reportfile.Id = result.FileName + System.DateTime.Now.Ticks;
                            ReportUloadedFiles.Add(reportfile);
                            IsTherePDFUploaded = true;

                        }
                        else
                        {
                            MessageTxt = AppResources.TINDeregAttachmentsTitleOne;
                            IsShowMsgView = true;
                        }
                    }
                    else
                    {
                        MessageTxt = AppResources.BalaghSupportedFileMsg;
                        IsShowMsgView = true;
                    }

                }
            }
            catch (Exception ex)
            {
            }


        }

        private bool IsValidateReport()
        {
            if (SubmitReport.IsNeedReward)
            {
                if (string.IsNullOrWhiteSpace(SubmitReport.ReporterNameAr)
                    || string.IsNullOrWhiteSpace(SubmitReport.ReporterMobileNumber))
                {
                    IsShowMsgView = true;
                    IsNeedRewardError = true;
                    MessageTxt = AppResources.InvalidValue;
                    return false;

                }

            }
            if (string.IsNullOrWhiteSpace(SubmitReport.ReportTypeName)
                || string.IsNullOrWhiteSpace(SubmitReport.ReportCategoryName)
                || string.IsNullOrWhiteSpace(SubmitReport.CompanyName)
                || string.IsNullOrWhiteSpace(SubmitReport.Region)
                || string.IsNullOrWhiteSpace(SubmitReport.City)
                || string.IsNullOrWhiteSpace(SubmitReport.District)
                || string.IsNullOrWhiteSpace(SubmitReport.Street)
                || string.IsNullOrWhiteSpace(SubmitReport.WorkType)
                || string.IsNullOrWhiteSpace(SubmitReport.CompanyAddress)
                || string.IsNullOrWhiteSpace(SubmitReport.ReportDetails)
                || SubmitReport.HasViolationDateError)
            {
                HasError = true;
                IsShowMsgView = true;
                MessageTxt = AppResources.InvalidValue;
                return false;
            }
            HasError = false;
            return true;

        }

    }
}

