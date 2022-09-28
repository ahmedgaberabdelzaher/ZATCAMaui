using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.MyReports;
using EGAZT.Views.NewDesign.ReportOTP;
using EGAZT.Views.NewDesign.SubmitReport;
using GalaSoft.MvvmLight.Views;
using GAZT;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Maps;
using Distance = Xamarin.Forms.GoogleMaps.Distance;
using Geocoder = Xamarin.Forms.GoogleMaps.Geocoder;
using Map = Xamarin.Forms.GoogleMaps.Map;
using MapSpan = Xamarin.Forms.GoogleMaps.MapSpan;
using Pin = Xamarin.Forms.GoogleMaps.Pin;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace EGAZT.ViewModel.NewDesignViewModel.SubmitReport
{
    public class SubmitReportViewModel : BaseViewModel
    {
        #region Properties
        private readonly ISubmitReportServices _submitReportServices;
        SubmitReportModel submitReport = new SubmitReportModel();

        public SubmitReportModel SubmitReport { get { return submitReport; } set { submitReport = value; RaisePropertyChanged(); } }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        bool isTherePDFUploaded;
        public bool IsTherePDFUploaded { get { return isTherePDFUploaded; } set { isTherePDFUploaded = value; RaisePropertyChanged(); } }

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        bool isOpenDatePicker;
        public bool IsOpenDatePicker { get { return isOpenDatePicker; } set { isOpenDatePicker = value; RaisePropertyChanged(); } }

        bool isReportCategoryShowen;
        public bool IsReportCategoryShowen { get { return isReportCategoryShowen; } set { isReportCategoryShowen = value; RaisePropertyChanged(); } }

        bool isMissingFieldShowen;
        public bool IsMissingFieldShowen { get { return isMissingFieldShowen; } set { isMissingFieldShowen = value; RaisePropertyChanged(); } }

        bool isCityShowen;
        public bool IsCityShowen { get { return isCityShowen; } set { isCityShowen = value; RaisePropertyChanged(); } }

        string reportNumberResult;
        public string ReportNumberResult { get { return reportNumberResult; } set { reportNumberResult = value; RaisePropertyChanged(); } }

        string headerTitle = AppResources.Submitareport;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        public Map GoogleMap { get; set; }

        ObservableCollection<ReportFileModel> reportUloadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> ReportUloadedFiles { get { return reportUloadedFiles; } set { reportUloadedFiles = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> bottomSheetList;
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> tempBottomSheetList;
        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get { return tempBottomSheetList; } set { tempBottomSheetList = value; RaisePropertyChanged(); } }

        private bool isReportTypeSelected = false;
        private bool isReportCategorySelected = false;
        private bool isMissingFieldSelected = false;
        private bool isRegionSelected = false;
        private List<BaseRegionAndCity> CitysList;
        private List<BaseRegionAndCity> RegionsList;
        private List<CategoryDataResponse> ReportCategory;
        private List<LookUpsListModel> MissingFieldsList;

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
                        if (IsValidateTermsReport())
                        {
                            IsLoading = true;

                            System.Globalization.DateTimeFormatInfo DTFormat;
                            DTFormat = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
                            DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                            DTFormat.ShortDatePattern = "dd/MM/yyyy";
                            SubmitReport.ViolationDate = SelectedDate.Date.ToString(DTFormat).Split(' ').FirstOrDefault();

                            SubmitReport.ReporterNameEn = SubmitReport.ReporterNameAr;
                            var json = JsonConvert.SerializeObject(SubmitReport);
                            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json); 
                            var reportResult = await this._submitReportServices.CreateZatcaNewReport(dictionary, ReportUloadedFiles);
                            if (reportResult.Success)
                            {
                                ReportNumberResult = reportResult.Result?.Data;
                                _navigationService.NavigateTo("/ReportSuccessPage");
                                SubmitReport = new SubmitReportModel();
                            }
                            IsLoading = false;

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

        public ICommand ShowMapCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await MoveMapToLocation();
                    }
                    catch (Exception ex)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.Somethingwentwrong;
                    }
                   
                });

            }
        }

        public ICommand GetCurrentLocationCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        _ = await GetCurrentLocation();
                    }
                    catch (Exception ex)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.Somethingwentwrong;
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
                    if (e != null)
                    {

                        var entry = e as BorderlessEntry;
                        var value = entry.Text.ToLower();
                        if (string.IsNullOrWhiteSpace(value))
                            BottomSheetList = TempBottomSheetList;
                        else
                        {
                            var result = BottomSheetList.Where(s => s.Name.Contains(value)).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        }


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
        public ICommand OpenDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsOpenDatePicker = true;

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
                    await PopupNavigation.Instance.PopAsync(true);
                });

            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    var navigation = Application.Current.MainPage.Navigation;
                    var currentPage = navigation.NavigationStack.LastOrDefault();
                    if (IsShowBottomSheet)
                    {
                        IsShowBottomSheet = false;
                        HeaderTitle = AppResources.Submitareport;
                        return;
                    }
                    else if(currentPage.GetType().Name == new SubmitReportPage().GetType().Name)
                    {
                        SubmitReport = new SubmitReportModel();
                    }
                    _navigationService.GoBack();

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
                            ReportCategory = await this._submitReportServices.GetReportCategories(SubmitReport?.ReportTaxType);
                            var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            isReportTypeSelected = false;
                            SubmitReport.ReportCategoryName = string.Empty;
                            SubmitReport.ReportCategory = string.Empty;

                            IsReportCategoryShowen = string.IsNullOrWhiteSpace(SubmitReport.ReportTypeName) ? false : true;
                        }
                        else if (isReportCategorySelected)
                        {
                            SubmitReport.ReportCategoryName = e.Name;
                            SubmitReport.ReportCategory = e.Id;
                            isReportCategorySelected = false;
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
                            CitysList = await this._submitReportServices.GetCities(SubmitReport?.RegionCode);
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
                        TempBottomSheetList = BottomSheetList;
                        IsLoading = false;
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

        public ICommand OpenReportTypeCommand
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    isReportTypeSelected = true;
                    var reportType = await this._submitReportServices.GetReportType();
                    var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ReportType;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
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
                    HeaderTitle = AppResources.ReportCategory;
                    var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    TempBottomSheetList = BottomSheetList;

                });
            }
        }
        public ICommand OpenMissingFieldCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsLoading = true;
                    isMissingFieldSelected = true;
                    var reportType = await this._submitReportServices.GetLookUps();
                    var result = reportType?.lookUpList?.Select(c => new BottomSheetModel() { Id = c.lookupId, Name = c.lookupName }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    TempBottomSheetList = BottomSheetList;
                    HeaderTitle = AppResources.ReportMissingField;
                    IsLoading = false;

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
                    HeaderTitle = AppResources.ZZZZProvinceRegion;
                    TempBottomSheetList = BottomSheetList;
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
                    IsLoading = true;

                    if (CitysList == null)
                        CitysList = await this._submitReportServices.GetCities(SubmitReport?.RegionCode);

                    var result = CitysList?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ReportCity;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;

                });
            }
        }

        #endregion

        public SubmitReportViewModel(ISubmitReportServices submitReportServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this._submitReportServices = submitReportServices;
        }


        #region Methods
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
                        else if (ReportUloadedFiles != null && ReportUloadedFiles.Count < 5)
                        {
                          
                            var stream = await result.OpenReadAsync();
                            // string content = ConvertToBase64(stream);
                            ReportFileModel reportfile = new ReportFileModel();
                            reportfile.filecontentStream = stream;
                            reportfile.filename = result.FileName;
                            reportfile.FileSize = Math.Round(size, 2);
                            reportfile.Id = result.FileName + System.DateTime.Now.Ticks;
                            reportfile.paramFileStream= File.ReadAllBytes(result.FullPath);
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
            catch (Exception ex)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.Somethingwentwrong;

            }


        }

        private bool IsValidateReport()
        {

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
                || ReportUloadedFiles.Count == 0)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }

            if (string.IsNullOrWhiteSpace(SubmitReport.MissedFieldName)
                && SubmitReport.ReportCategory.ToLower().Equals("v36"))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }
            if(string.IsNullOrWhiteSpace(SubmitReport.TIN))
            { 
                if(!Regex.IsMatch(SubmitReport.TIN, @"^\d{10}$"))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZTINnumberconsistsofnumbersonly +"; "+ AppResources.ZZTINnumberlengthcannotbelessthan10digits;
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
                else if (!Email.IsMatch(SubmitReport.ReporterEmail))
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
            return true;

        }

        private async Task<bool> GetCurrentLocation()
        {
            var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (statusLocationAlways == PermissionStatus.Granted || statusLocationWhenInUse == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                SubmitReport.Latitude = location.Latitude;
                SubmitReport.Longitude = location.Longitude;

                Geocoder geoCoder = new Geocoder();

                Position position = new Position(location.Latitude, location.Longitude);

                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                SubmitReport.Street = possibleAddresses.FirstOrDefault();

                SubmitReport.Location = $"{SubmitReport.Latitude},{SubmitReport.Longitude},{possibleAddresses.FirstOrDefault()}";

                return true;
            }
            else
            {
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
               return false;

            }
        }

        private async Task MoveMapToLocation()
        {

            if (await GetCurrentLocation())
            {
                MapPage poupMapWindow = new MapPage();
                await PopupNavigation.Instance.PushAsync(poupMapWindow);
                await Task.Delay(1000);
                var zoomLevel = 10.71; // pick a value between 1 and 18
                var latlongdeg = 360 / (Math.Pow(2, zoomLevel));

                GoogleMap?.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(SubmitReport.Latitude, SubmitReport.Longitude), Distance.FromMiles(latlongdeg)), true);
                GoogleMap?.Pins.Clear();
                GoogleMap?.Pins.Add(new Pin()
                {
                    Address = SubmitReport.Street,
                    Label = SubmitReport.Street,
                    Position = new Position(SubmitReport.Latitude, SubmitReport.Longitude)
                });


            }
            else
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.LocationAccess;
            }
        }
        #endregion

    }
}

