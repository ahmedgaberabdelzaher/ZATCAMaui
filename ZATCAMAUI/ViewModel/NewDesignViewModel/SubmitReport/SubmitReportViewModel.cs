using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Acr.UserDialogs;

using Maui.GoogleMaps;
using Microsoft.Maui.Handlers;
using Mopups.Services;
using ZATCAMAUI.Controls;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.SubmitReportModel;
using ZATCAMAUI.Views.NewDesign.SubmitReport;
using Map = Maui.GoogleMaps.Map;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport
{
    public class SubmitReportViewModel : BaseViewModel
    {
        #region Properties

        bool _IsReadTermsandCondition = false;
        public bool IsReadTermsandCondition { get { return _IsReadTermsandCondition; } set { _IsReadTermsandCondition = value; OnPropertyChanged(); } }

        bool _IsreporterDataMandatory = false;
        public bool IsreporterDataMandatory { get { return _IsreporterDataMandatory; } set { _IsreporterDataMandatory = value; OnPropertyChanged(); } }


        private readonly ISubmitReportServices _submitReportServices;

        SubmitReportModel submitReport = new SubmitReportModel();
        public SubmitReportModel SubmitReport { get { return submitReport; } set { submitReport = value; OnPropertyChanged(); } }

        public DateTime SelectedDate { get; set; } = DateTime.Now;

        bool isTherePDFUploaded;
        public bool IsTherePDFUploaded { get { return isTherePDFUploaded; } set { isTherePDFUploaded = value; OnPropertyChanged(); } }

        string violationDateDateString;
        public string ViolationDateDateString { get { return violationDateDateString; } set { violationDateDateString = value; OnPropertyChanged(); } }

        bool isReportCategoryShowen;
        public bool IsReportCategoryShowen { get { return isReportCategoryShowen; } set { isReportCategoryShowen = value; OnPropertyChanged(); } }

        bool isMissingFieldShowen;
        public bool IsMissingFieldShowen { get { return isMissingFieldShowen; } set { isMissingFieldShowen = value; OnPropertyChanged(); } }

        bool isSubCategeoryShow;
        public bool IsSubCategeoryShow { get { return isSubCategeoryShow; } set { isSubCategeoryShow = value; OnPropertyChanged(); } }


        bool isCityShowen;
        public bool IsCityShowen { get { return isCityShowen; } set { isCityShowen = value; OnPropertyChanged(); } }

        string reportNumberResult;
        public string ReportNumberResult { get { return reportNumberResult; } set { reportNumberResult = value; OnPropertyChanged(); } }

        string headerTitle = AppResources.Submitareport;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; OnPropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged(); } }

        public Map GoogleMap { get; set; }

        ObservableCollection<ReportFileModel> reportUloadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> ReportUloadedFiles { get { return reportUloadedFiles; } set { reportUloadedFiles = value; OnPropertyChanged(); } }

        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; OnPropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        DateTime _MaximumDate = DateTime.Now.Date.AddHours(-24);
        public DateTime MaximumDate { get { return _MaximumDate; } set { _MaximumDate = value; OnPropertyChanged(); } }

        private bool isReportTypeSelected = false;
        private bool isReportCategorySelected = false;
        private bool isMissingFieldSelected = false;
        private bool isRegionSelected = false;
        private List<BaseRegionAndCity> CitysList;
        private List<BaseRegionAndCity> RegionsList;
        private List<CategoryDataResponse> ReportCategory;
        private List<LookUpsListModel> MissingFieldsList;
        private List<CategoryDataResponse> ReportSubCategory;
        private bool isReportSubCategorySelected = false;
        #endregion

        #region Commands
        public ICommand SendReportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (IsValidateReport() && IsValidateTermsReport())
                        {
                            IsLoading = true;

                            System.Globalization.DateTimeFormatInfo DTFormat;
                            DTFormat = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
                            DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                            DTFormat.ShortDatePattern = "dd/MM/yyyy";
                            SubmitReport.ViolationDate = SelectedDate.Date.ToString(DTFormat).Split(' ').FirstOrDefault();

                            SubmitReport.ReporterNameEn = SubmitReport.ReporterNameAr;
                            SubmitReport.files = ReportUloadedFiles.ToList();
                            List<SubmitReportDataPowerModelAttachement> DATAPowerAttachements = new List<SubmitReportDataPowerModelAttachement>();
                            foreach (var item in SubmitReport.files)
                            {
                                DATAPowerAttachements.Add(new SubmitReportDataPowerModelAttachement() { fileContent = item.fileBase64, fileExtinction = item.fileExtinction, fileName = item.fileFullName + item.fileExtinction });
                            }
                            SubmitReportDataPowerModel model = new SubmitReportDataPowerModel()
                            {
                                city = SubmitReport.City,
                                cityCode = SubmitReport.CityCode,
                                companyAddress = SubmitReport.CompanyAddress,
                                companyName = SubmitReport.CompanyName,
                                CR = SubmitReport.CR,
                                district = SubmitReport.District,
                                isNeedReward = SubmitReport.IsNeedReward,
                                LanguageCode = App.IsArabic ? "ar" : "en",
                                latitude = SubmitReport.Latitude,
                                longitude = SubmitReport.Longitude,
                                missedField = SubmitReport.MissedField,
                                regionCode = SubmitReport.RegionCode,
                                regionName = SubmitReport.Region,
                                reportCategory = SubmitReport.ReportCategory,
                                reportSubCategory = SubmitReport.ReportSubCategory,
                                reportCategoryName = SubmitReport.ReportCategoryName,
                                reportDetails = SubmitReport.ReportDetails,
                                reporterEmail = SubmitReport.ReporterEmail,
                                reporterMobileNumber = SubmitReport.ReporterMobileNumber,
                                reporterName_Arabic = SubmitReport.ReporterNameAr,
                                reporterName_English = SubmitReport.ReporterNameEn,
                                reporterWantToSharePersonalInfo = SubmitReport.ReporterWantToSharePersonalInfo,
                                reportTaxType = SubmitReport.ReportTaxType,
                                reportTypeName = SubmitReport.ReportTypeName,
                                street = SubmitReport.Street,
                                TIN = SubmitReport.TIN,
                                violationDate = SubmitReport.ViolationDate,
                                workType = SubmitReport.ReportTaxType,
                                attachements = DATAPowerAttachements,

                                reporterNationalID = SubmitReport.ReporterNationalId,
                                reportSubCategoryName = submitReport.ReportSubCategoryName,
                                reporterID = SubmitReport.ReporterNationalId,

                            };
                            #region DATA Power Response
                            var reportResult = await this._submitReportServices.CreateZatcaNewReport(model);

                            if (reportResult != null)
                            {
                                if (reportResult.header?.status?.code == "I000000")
                                {
                                    ReportNumberResult = reportResult.result?.referenceNumber;
                                    SubmitReport = new SubmitReportModel();
                                    IsCityShowen = false;
                                    IsReportCategoryShowen = false;
                                    IsMissingFieldShowen = false;
                                    ReportUloadedFiles = new ObservableCollection<ReportFileModel>();
                                    await _navigationService.NavigateTo("/ReportSuccessPage");

                                }
                                else if (!string.IsNullOrWhiteSpace(reportResult?.header?.moreInformation?.backendErrors))
                                {
                                    MessageTxt = reportResult?.header?.moreInformation?.backendErrors;
                                    IsShowMsgView = true;
                                }
                                else if (reportResult.header.status.code == "E999999")
                                {
                                    MessageTxt = reportResult?.header?.status?.description;
                                    IsShowMsgView = true;
                                }
                                else
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = AppResources.RequestTimeoutDescription;
                                }
                            }



                            #endregion
                            IsLoading = false;

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

        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    UploadingPopup poupUploadingWindow = new UploadingPopup();
                    await MopupService.Instance.PushAsync(poupUploadingWindow);

                });
            }
        }

        public override ICommand SelectedUploadLabelCommand
        {
            get
            {
                return new Command<string>(async (selectedLabel) =>
                {
                    await MopupService.Instance.PopAsync(true);
                    await PickAndShow(selectedLabel, new PickOptions() { PickerTitle = "Pick Files" });

                });
            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        await MoveMapToLocation();
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.LocationAccess;
                    }

                });

            }
        }

        public ICommand CopyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Clipboard.SetTextAsync(ReportNumberResult);
                    UserDialogs.Instance.Toast(AppResources.Copied, TimeSpan.FromSeconds(1));
                });
            }
        }

        public ICommand GetCurrentLocationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        var x = await GetCurrentLocation();
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.LocationAccess;
                    }


                });
            }
        }

        public ICommand OpenTermsLinkCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Browser.OpenAsync("https://stgextportal.gazt.gov.sa/ar/RulesRegulations/Taxes/Pages/IncentiveRewards3.aspx", BrowserLaunchMode.SystemPreferred);

                });
            }
        }

        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    try
                    {
                        if (e != null)
                        {
                            var entry = e as GAZTBorderlessEntry;
                            var value = entry.Text.ToLower();
                            if (string.IsNullOrWhiteSpace(value))
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(TempBottomSheetList);
                            else
                            {
                                var result = TempBottomSheetList.Where(s => s.Name.ToLower().Contains(value));
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand GoToMyReportsCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/InquiryAboutMyReportsPage");


                });
            }
        }

        public ICommand BackToHomeCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/Home", "0");

                });
            }
        }

        public ICommand SelectedDateCommand
        {
            get
            {
                return new Command<Entry>((control) =>
                {
                    try
                    {
                        ViolationDateDateString = DateTimeHelper.DateTimeFormater(SelectedDate.Date);

                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand DateClickedCommand
        {
            get
            {
                return new Command<DatePicker>((control) =>
                {
                    try
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
#if ANDROID

                            var handler = control.Handler as IDatePickerHandler;
                                handler.PlatformView.PerformClick();
#endif
#if IOS
                            control?.Focus();
#endif
                        });

                        ViolationDateDateString = DateTimeHelper.DateTimeFormater(SelectedDate.Date);
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand GoToTermsPageCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsValidateReport())
                        _navigationService.NavigateTo("TermsPage");
                });
            }
        }

        public ICommand CloseMapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await MopupService.Instance.PopAsync(true);
                });

            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    BackMethod();

                });
            }
        }

        public ICommand CheckBoxCommand
        {
            get
            {
                return new Command(() =>
                {
                    SubmitReport.IsNeedReward = SubmitReport.IsNeedReward == true ? false : true;
                    if (!SubmitReport.ReportCategory.ToLower().Contains("v"))
                    {
                        IsreporterDataMandatory = SubmitReport.IsNeedReward;
                    }
                    else
                    {

                    }

                });

            }
        }
        public ICommand TermsAndConditionsCheckBoxCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsReadTermsandCondition = IsReadTermsandCondition == true ? false : true;
                });

            }
        }

        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<ReportFileModel>((file) =>
                {

                    if (file != null && ReportUloadedFiles != null && ReportUloadedFiles.Count > 0)
                    {
                        ReportUloadedFiles.Remove(file);

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
                    try
                    {
                        IsLoading = true;

                        if (isReportTypeSelected)
                        {

                            SubmitReport.ReportTypeName = e.Name;
                            SubmitReport.ReportTaxType = e.Id;
                            ReportCategory = await _submitReportServices.GetReportCategories(SubmitReport?.ReportTaxType);
                            var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            isReportTypeSelected = false;
                            SubmitReport.ReportCategoryName = string.Empty;
                            SubmitReport.ReportCategory = string.Empty;
                            SubmitReport.MissedFieldName = string.Empty;
                            SubmitReport.MissedField = string.Empty;
                            IsMissingFieldShowen = false;
                            IsReportCategoryShowen = string.IsNullOrWhiteSpace(SubmitReport.ReportTypeName) ? false : true;

                        }
                        else if (isReportCategorySelected)
                        {
                            SubmitReport.ReportCategoryName = e.Name;
                            SubmitReport.ReportCategory = e.Id;
                            #region GetSubbCategeory
                            ReportSubCategory = await _submitReportServices.GetReportSubCategories(e.Id);
                            if (ReportSubCategory != null && ReportSubCategory.Count > 0)
                            {
                                IsSubCategeoryShow = true;
                            }
                            else
                            {
                                IsSubCategeoryShow = false;
                            }
                            if (SubmitReport.ReportCategory.ToLower().Contains("v"))
                            {
                                IsreporterDataMandatory = true;
                            }
                            else
                            {
                                IsreporterDataMandatory = false;
                            }
                            var result = ReportSubCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);

                            #endregion
                            isReportCategorySelected = false;
                            SubmitReport.MissedFieldName = string.Empty;
                            SubmitReport.MissedField = string.Empty;
                            IsMissingFieldShowen = !string.IsNullOrWhiteSpace(SubmitReport.ReportCategoryName) && SubmitReport.ReportCategory.ToLower().Equals("v36") ? true : false;
                        }
                        else if (isReportSubCategorySelected)
                        {
                            SubmitReport.ReportSubCategoryName = e.Name;
                            SubmitReport.ReportSubCategory = e.Id;
                            isReportSubCategorySelected = false;
                            SubmitReport.MissedFieldName = string.Empty;
                            SubmitReport.MissedField = string.Empty;
                            IsMissingFieldShowen = !string.IsNullOrWhiteSpace(SubmitReport.ReportCategoryName) && SubmitReport.ReportCategory.ToLower().Equals("v36") ? true : false;
                        }
                        else if (isMissingFieldSelected)
                        {
                            SubmitReport.MissedFieldName = e.Name;
                            SubmitReport.MissedField = e.Id;
                            isMissingFieldSelected = false;
                        }
                        else if (isRegionSelected)
                        {
                            SubmitReport.Region = e.Name;
                            SubmitReport.RegionCode = e.Id;
                            CitysList = await _submitReportServices.GetCities(SubmitReport?.RegionCode);
                            isRegionSelected = false;
                            SubmitReport.City = string.Empty;
                            SubmitReport.CityCode = string.Empty;
                            IsCityShowen = string.IsNullOrWhiteSpace(SubmitReport.Region) ? false : true;
                        }
                        else
                        {
                            SubmitReport.City = e.Name;
                            SubmitReport.CityCode = e.Id;

                        }

                        IsShowBottomSheet = false;
                        HeaderTitle = AppResources.Submitareport;
                        SearchText = string.Empty;
                        IsLoading = false;
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

        public ICommand OpenReportTypeCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {

                        IsLoading = true;
                        isReportTypeSelected = true;
                        isReportCategorySelected = false;
                        isMissingFieldSelected = false;
                        isRegionSelected = false;

                        var reportType = await this._submitReportServices.GetReportType();
                        var result = reportType?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result.Distinct());
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ReportType;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });

            }
        }

        public ICommand OpenReportCategoryCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        isReportCategorySelected = true;
                        isReportTypeSelected = false;
                        isMissingFieldSelected = false;
                        isRegionSelected = false;
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ReportCategory;
                        var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenReportSubCategoryCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        isReportSubCategorySelected = true;
                        isReportCategorySelected = false;
                        isReportTypeSelected = false;
                        isMissingFieldSelected = false;
                        isRegionSelected = false;
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ReportCategory;
                        var result = ReportSubCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).Distinct();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenMissingFieldCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isMissingFieldSelected = true;
                        isReportCategorySelected = false;
                        isReportTypeSelected = false;
                        isRegionSelected = false;
                        var reportType = await this._submitReportServices.GetLookUps();
                        var result = reportType?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        HeaderTitle = AppResources.ReportMissingField;
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
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
                    try
                    {
                        IsLoading = true;
                        isRegionSelected = true;
                        isReportCategorySelected = false;
                        isReportTypeSelected = false;
                        isMissingFieldSelected = false;
                        RegionsList = await _submitReportServices.GetRegions();
                        var result = RegionsList?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ZZZZProvinceRegion;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenCityCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isRegionSelected = false;
                        isReportCategorySelected = false;
                        isReportTypeSelected = false;
                        isMissingFieldSelected = false;
                        CitysList = await _submitReportServices.GetCities(SubmitReport?.RegionCode);
                        var result = CitysList?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ReportCity;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }


                });
            }
        }

        #endregion

        public SubmitReportViewModel(ISubmitReportServices submitReportServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _submitReportServices = submitReportServices;
        }


        #region Methods
        private async Task PickAndShow(string selectedLabel, PickOptions options)
        {
            try
            {
                FileResult result;

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    if (selectedLabel.Equals("image"))
                        result = await MediaPicker.PickPhotoAsync();
                    else
                        result = await FilePicker.PickAsync(options);
                }
                else
                {
                    result = await FilePicker.PickAsync(options);
                }

                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase)
                        || result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("docx", StringComparison.OrdinalIgnoreCase)
                        || result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
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
                        else if (ReportUloadedFiles != null && ReportUloadedFiles.Count < 5)
                        {
                            var stream = await result.OpenReadAsync();
                            string content = await ConvertToBase64(stream);
                            ReportFileModel reportfile = new ReportFileModel();
                            reportfile.fileBase64 = content;
                            reportfile.fileFullName = result.FileName;
                            reportfile.fileExtinction = Path.GetExtension(result.FileName);
                            ReportUloadedFiles.Add(reportfile);
                            IsTherePDFUploaded = true;

                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.TINDeregAttachmentsTitleOne;

                        }
                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.BalaghSupportedFileMsg;

                    }

                }
            }
            catch (Exception)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.Somethingwentwrong;

            }


        }

        private async Task<string> ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            await stream.ReadAsync(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }

        private bool IsValidateReport()
        {

            if (string.IsNullOrWhiteSpace(SubmitReport.MissedFieldName)
                && SubmitReport.ReportCategory.ToLower().Equals("v36"))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }

            if (!string.IsNullOrWhiteSpace(SubmitReport.TIN))
            {
                if (!Regex.IsMatch(SubmitReport.TIN, @"^\d{10}$"))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZTINnumberconsistsofnumbersonly + "; " + AppResources.ZZTINnumberlengthcannotbelessthan10digits;
                    return false;

                }

            }

            if (!string.IsNullOrWhiteSpace(SubmitReport.CR))
            {
                if (!Regex.IsMatch(SubmitReport.CR, @"^\d{10}$"))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZCommercialReiterationNumberconsistsofnumbersonly + "; " + AppResources.ZZCommercialReiterationNumbershouddbe10digits;
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
                || string.IsNullOrWhiteSpace(SubmitReport.Location)
                || string.IsNullOrWhiteSpace(SubmitReport.WorkType)
                || SelectedDate.Date > DateTime.Now.Date
                || !IsReadTermsandCondition
                || ReportUloadedFiles.Count == 0)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }

            
            return true;

        }

        private bool IsValidateTermsReport()
        {
            Regex phoneRegex = new Regex(@"^05[0-9]{8}$");
            Regex Email = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            if (SubmitReport.IsNeedReward)
            {
                if (string.IsNullOrWhiteSpace(SubmitReport.ReporterNameAr)
                    || string.IsNullOrWhiteSpace(SubmitReport.ReporterMobileNumber)
                    || string.IsNullOrWhiteSpace(SubmitReport.ReporterEmail))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;

                }
                else if (!Email.IsMatch(SubmitReport.ReporterEmail.ToLower()))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InvalidEmailFormat;
                    return false;
                }
                else if (!phoneRegex.IsMatch(SubmitReport.ReporterMobileNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZMobilenumberhastostartwithnumber05;
                    return false;


                }
            }

            else if (IsreporterDataMandatory)
            {
                if (string.IsNullOrWhiteSpace(SubmitReport.ReporterNameAr)
                    || string.IsNullOrWhiteSpace(SubmitReport.ReporterMobileNumber)
                    || string.IsNullOrWhiteSpace(SubmitReport.ReporterEmail))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;

                }
                else if (!Email.IsMatch(SubmitReport.ReporterEmail.ToLower()))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InvalidEmailFormat;
                    return false;
                }
                else if (!phoneRegex.IsMatch(SubmitReport.ReporterMobileNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZMobilenumberhastostartwithnumber05;
                    return false;


                }
            }

            if (!IsReadTermsandCondition)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }

            return true;

        }

        private async Task<bool> GetCurrentLocation()
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (statusLocationWhenInUse == PermissionStatus.Granted)
                {
                    var location = await Geolocation.GetLocationAsync();
                    if (location == null)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }

                    await SetLocation(location);

                    return true;
                }
                else
                {
                    IsLoading = false;
                    var LastKnownLocation = await Geolocation.GetLastKnownLocationAsync();
                    await SetLocation(LastKnownLocation);
                    return false;

                }
            }
            catch (Exception)
            {
                IsLoading = false;
                return false;
            }

        }

        private async Task SetLocation(Location location)
        {
            SubmitReport.Latitude = location.Latitude;
            SubmitReport.Longitude = location.Longitude;

            Geocoder geoCoder = new Geocoder();

            Position position = new Position(location.Latitude, location.Longitude);

            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

            SubmitReport.CompanyAddress = possibleAddresses.FirstOrDefault();

            SubmitReport.Location = $"{SubmitReport.Latitude},{SubmitReport.Longitude},{possibleAddresses.FirstOrDefault()}";
        }

        private async Task MoveMapToLocation()
        {

            if (await GetCurrentLocation())
            {
                MapPage poupMapWindow = new MapPage();
                await MopupService.Instance.PushAsync(poupMapWindow);
                await Task.Delay(1000);
                var zoomLevel = 10.71; // pick a value between 1 and 18
                var latlongdeg = 360 / Math.Pow(2, zoomLevel);

                GoogleMap?.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(SubmitReport.Latitude, SubmitReport.Longitude), Distance.FromMiles(latlongdeg)), true);
                GoogleMap?.Pins.Clear();
                GoogleMap?.Pins.Add(new Pin()
                {
                    Address = SubmitReport.CompanyAddress,
                    Label = SubmitReport.CompanyAddress,
                    Position = new Position(SubmitReport.Latitude, SubmitReport.Longitude)
                });


            }
            else
            {
                IsLoading = false;
                IsShowMsgView = true;
                MessageTxt = AppResources.LocationAccess;
            }
        }

        public void BackMethod()
        {
            var navigation = Application.Current.MainPage.Navigation;
            var currentPage = navigation.NavigationStack.LastOrDefault();
            if (IsShowBottomSheet)
            {
                IsShowBottomSheet = false;
                HeaderTitle = AppResources.Submitareport;
                return;
            }
            else if (currentPage.GetType().Name == new SubmitReportPage().GetType().Name)
            {
                SubmitReport = new SubmitReportModel();
                IsCityShowen = false;
                IsReportCategoryShowen = false;
                IsMissingFieldShowen = false;
                ReportUloadedFiles = new ObservableCollection<ReportFileModel>();
            }
            _navigationService.GoBack();
        }
        #endregion

    }
}

