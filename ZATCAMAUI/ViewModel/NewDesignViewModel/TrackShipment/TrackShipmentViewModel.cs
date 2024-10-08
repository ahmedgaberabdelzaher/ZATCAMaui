
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

#if ANDROID
using Microsoft.Maui.Handlers;
#endif
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.TrackShipment;
using ZATCAMAUI.Views.NewDesign.TrackShipment;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment
{
    public partial class TrackShipmentViewModel : BaseViewModel
    {
        #region Properties
        
        ObservableCollection<ShipmentStatus> shipmentStatusList = new ObservableCollection<ShipmentStatus>();

        public ObservableCollection<ShipmentStatus> ShipmentStatusList { get { return shipmentStatusList; } set { shipmentStatusList = value; OnPropertyChanged(); } }


        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; OnPropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; OnPropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; OnPropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged(); } }

        DrawShipmentTrack drawShipmentTrack = new DrawShipmentTrack();
        public DrawShipmentTrack DrawShipmentTrack { get { return drawShipmentTrack; } set { drawShipmentTrack = value; OnPropertyChanged(); } }

        string shipmentDeclarationNumber;
        public string ShipmentDeclarationNumber { get { return shipmentDeclarationNumber; } set { shipmentDeclarationNumber = value; OnPropertyChanged(); } }

        string declarationDateString= "dd-MM-yyyy";
        public string DeclarationDateString { get { return declarationDateString; } set { declarationDateString = value; OnPropertyChanged(); } }

        string shipmentBillNumber;
        public string ShipmentBillNumber { get { return shipmentBillNumber; } set { shipmentBillNumber = value; OnPropertyChanged(); } }

        string shipmentContainerNumber;
        public string ShipmentContainerNumber { get { return shipmentContainerNumber; } set { shipmentContainerNumber = value; OnPropertyChanged(); } }

        string selectedPortName;
        public string SelectedPortName { get { return selectedPortName; } set { selectedPortName = value; OnPropertyChanged(); } }

        int selectedPortId;
        public int SelectedPortId { get { return selectedPortId; } set { selectedPortId = value; OnPropertyChanged(); } }

        TrackShipmentModel trackShipmentResponse;
        public TrackShipmentModel TrackShipmentResponse { get { return trackShipmentResponse; } set { trackShipmentResponse = value; OnPropertyChanged(); } }

        string statusTitle;
        public string StatusTitle { get { return statusTitle; } set { statusTitle = value; OnPropertyChanged(); } }

        bool isExpressShipment;
        public bool IsExpressShipment { get { return isExpressShipment; } set { isExpressShipment = value; OnPropertyChanged(); } }

        bool hasExciseTax;
        public bool HasExciseTax { get { return hasExciseTax; } set { hasExciseTax = value; OnPropertyChanged(); } }

        string shipmentImporterYear;
        public string ShipmentImporterYear { get { return shipmentImporterYear; } set { shipmentImporterYear = value; OnPropertyChanged(); } }

        ObservableCollection<string> yearsList = new ObservableCollection<string>();
        public ObservableCollection<string> YearsList { get { return yearsList; } set { yearsList = value; OnPropertyChanged(); } }

        #endregion Properties

        #region Commands
        public ICommand CardSelectionCommand
        {
            get
            {
                return new Command<string>((card) =>
                {
                    

                    if (card.Equals(((int)ShipmentCards.Express).ToString()))
                    {
                        DrawExpressShipping();
                    }


                    else if (card.Equals(((int)ShipmentCards.Air).ToString()))
                    {
                        DrawAirShipping();
                    }


                    else if (card.Equals(((int)ShipmentCards.Sea).ToString()))
                    {
                        DrawSeaShipping();
                    }

                    else if (card.Equals(((int)ShipmentCards.Land).ToString()))
                    {
                        DrawLandShipping();
                    }

                    else
                    {
                        DrawTrainShipping();
                    }
                    _navigationService.NavigateTo("TrackShipmentPage");
                });
            }
        }

        public ICommand GoToTShipmentStatusCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;

                        if (isExpressCardSelected)
                        {
                            StatusTitle = DrawShipmentTrack.IsDeclarationSelected ? $"{AppResources.DeclarationNumber}: {ShipmentDeclarationNumber}" : $"{AppResources.ExpressBillNumber}: {ShipmentBillNumber}";
                            await GetExpressShipping(DrawShipmentTrack.IsDeclarationSelected);
                        }
                            


                        else if (isAirCardSelected)
                        {
                            StatusTitle = DrawShipmentTrack.IsDeclarationSelected ? $"{AppResources.DeclarationNumber}: {ShipmentDeclarationNumber}" : $"{AppResources.Billofladingnumber}: {ShipmentBillNumber}";
                            await GetAirShipping(DrawShipmentTrack.IsDeclarationSelected);

                        }

                        else if (isSeaCardSelected)
                        {
                            StatusTitle = DrawShipmentTrack.IsDeclarationSelected ? $"{AppResources.DeclarationNumber}: {ShipmentDeclarationNumber}" : $"{AppResources.Billofladingnumber}: {ShipmentBillNumber}";
                            await GetSeaShipping(DrawShipmentTrack.IsDeclarationSelected);
                        }
                            

                        else if (isLandCardSelected)
                        {
                            StatusTitle = $"{AppResources.DeclarationNumber}: {ShipmentDeclarationNumber}";
                            await GetLandShipping();
                        }
                            

                        else
                        {
                            StatusTitle = DrawShipmentTrack.IsDeclarationSelected ? $"{AppResources.DeclarationNumber}: {ShipmentDeclarationNumber}" : $"{AppResources.Billofladingnumber}: {ShipmentBillNumber}";
                            await GetTrainShipping(DrawShipmentTrack.IsDeclarationSelected);
                        }
                            

                        

                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand DeclarationSelectionCommand
        {
            get
            {
                return new Command<string>((d) =>
                {
                    DrawShipmentTrack.IsDeclarationSelected = !DrawShipmentTrack.IsDeclarationSelected;

                    if (isExpressCardSelected)
                        DrawInputsDependingOnCardsOnly(ShipmentCards.Express);

                    else if (isAirCardSelected)
                        DrawInputsDependingOnCardsOnly(ShipmentCards.Air);

                    else if (isSeaCardSelected)
                        DrawInputsDependingOnCardsOnly(ShipmentCards.Sea);

                    else if (isTrainCardSelected)
                        DrawInputsDependingOnCardsOnly(ShipmentCards.Train);

                });
            }
        }

        public ICommand DateClickedCommand
        {
            get
            {
                return new Command<CustomHijriDatePicker>((control) =>
                {
                    try
                    {
                        control.IsOpen = true;

                    }
                    catch (Exception)
                    {
                        DeclarationDateString = "dd-MM-yyyy";
                    }
                    

                });
            }
        }

        public ICommand OpenPortCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {

                        if (isAirCardSelected)
                        {

                            await GetPorts(ShipmentCards.Air);
                        }
                        else if (isSeaCardSelected)
                        {
                            await GetPorts(ShipmentCards.Sea);
                        }
                        else if (isLandCardSelected)
                        {
                            await GetPorts(ShipmentCards.Land);
                        }
                        else if (isTrainCardSelected)
                        {
                            await GetPorts(ShipmentCards.Train);
                        }

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

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

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    try
                    {
                        if (isAirCardSelected)
                        {

                            SelectedPortName = e.Name;
                            SelectedPortId = int.Parse(e.Id);
                            HeaderTitle = AppResources.AirFreight;
                        }
                        else if (isSeaCardSelected)
                        {
                            SelectedPortName = e.Name;
                            SelectedPortId = int.Parse(e.Id);
                            HeaderTitle = AppResources.SeaFreight;
                        }
                        else if (isLandCardSelected)
                        {
                            SelectedPortName = e.Name;
                            SelectedPortId = int.Parse(e.Id);
                            HeaderTitle = AppResources.LandFreight;
                        }
                        else if (isTrainCardSelected)
                        {
                            SelectedPortName = e.Name;
                            SelectedPortId = int.Parse(e.Id);
                            HeaderTitle = AppResources.TrainFreight;
                        }

                        else if (HeaderTitle == AppResources.ImporterYear)
                        {
                            ShipmentImporterYear = e.Name;
                            HeaderTitle = AppResources.ExpressShipping;
                        }

                        IsShowBottomSheet = false;
                        SearchText = string.Empty;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                    }


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

        public ICommand BackToHomeCommand
        {
            get
            {
                return new Command(() =>
                {
                    ResetDate();
                    DeclarationDateString = string.Empty;
                    ResetTrackShipmentData();
                    ResetTrackStatus();
                    _navigationService.NavigateTo("/Home", "0");

                });
            }
        }

        public ICommand SelectedDateCommand
        {
            get
            {
                return new Command<CustomHijriDatePicker>((date) =>
                {
                    try
                    {
                        var dateTime = date.SelectedDate;
                        DeclarationDateString = DateTimeHelper.DateTimeFormater(dateTime,"dd-MM-yyyy");

                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand OpenYearsCommand
        {
            get
            {
                return new Command( _ =>
                {
                    try
                    {
                        GetYears();
                       

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }
        #endregion Commands

        #region Methods

        private async Task GetPorts(ShipmentCards portType)
        {
            try
            {
                IsLoading = true;
                var result = await _commonServices.GetCustomPorts((int)portType);
                if (result?.Item1?.Code == 200)
                {
                    var ports = result?.Item1.Data.ToList();
                    var bottom = ports?.Select(p => new BottomSheetModel() { Id = p.port_cd.ToString(), Name = p.Name });
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.Port;
                    TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);

                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequestTimeoutDescription;
                }
                IsLoading = false;

            }
            catch (Exception)
            {
                IsLoading = false;
                IsShowMsgView = true;
                MessageTxt = AppResources.RequestTimeoutDescription;
            }

        }

        private void GetYears()
        {
            UmAlQuraCalendar hijriCalendar = new UmAlQuraCalendar();
            BottomSheetList = new ObservableCollection<BottomSheetModel>();
            for (int i = hijriCalendar.GetYear(DateTime.Today); i >=  1349 ; i--)
            {
                BottomSheetList.Add(new BottomSheetModel()
                {
                    Name = i.ToString()
                });

            }
            HeaderTitle = AppResources.ImporterYear;
            IsShowBottomSheet = true;
            TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
        }

        private void FillDataFromAPI(Tuple<Models.BaseModels.DATAPowerBaseResponse<TrackShipmentModel>, bool, string> result)
        {
            if (result?.Item1?.header?.status.code == "I000000")
            {
                TrackShipmentResponse = result?.Item1?.data;
                if (TrackShipmentResponse != null)
                {
                    TrackShipmentResponse.VAT = Math.Round(TrackShipmentResponse.VAT, 2);
                    TrackShipmentResponse.customs = Math.Round(TrackShipmentResponse.customs, 2);
                    TrackShipmentResponse.others = Math.Round(TrackShipmentResponse.others, 2);
                    TrackShipmentResponse.CIF = Math.Round(TrackShipmentResponse.CIF, 2);
                    TrackShipmentResponse.totalFees = Math.Round(TrackShipmentResponse.totalFees, 2);
                    HasExciseTax = TrackShipmentResponse.excisetax == 0 ? false : true;

                    foreach (var item in TrackShipmentResponse?.activities)
                    {
                        var status = new ShipmentStatus();


                        CultureInfo currentCulture = CultureInfo.GetCultureInfo(CultureInfo.CurrentCulture.ToString());
                        CultureInfo myLanguage = CultureInfo.GetCultureInfo("en-US");
                        CultureInfo.CurrentUICulture = myLanguage;
                        Thread.CurrentThread.CurrentCulture = myLanguage;
                        // The date in the format that we need
                        var mydatw = DateTime.Parse(item.activityDate, myLanguage);
                        var statusDate = mydatw.ToString("dd/MM/yyyy hh:mm tt").ToString(myLanguage);


                        status.ShipmentStatusDateString = statusDate;
                        status.ShipmentStatusValue = item.Name.Replace("تم تحويل البيان الجمركي للتحصيل", "تم اصدار الفاتورة");
                        if (trackShipmentResponse?.activities.Count > 1)
                        {
                            // Draw start circle for first item only
                            if (trackShipmentResponse.activities.Last() == item)
                            {
                                status.StatusImage = "fillCircle.png";
                                status.HasVerticalLine = false;
                                ShipmentStatusList?.Add(status);
                                continue;
                            }
                            status.StatusImage = "fillCircle.png";
                            status.HasVerticalLine = true;
                        }

                        // Draw start circle if items is one item only
                        else
                        {
                            status.StatusImage = "startCircle.png";
                            status.HasVerticalLine = false;
                        }

                        ShipmentStatusList?.Add(status);
                    }
                    var navigation = Application.Current.MainPage.Navigation;
                    var currentPage = navigation.NavigationStack.LastOrDefault();
                    navigation.InsertPageBefore(new ShipmentStatusPage(), currentPage);
                    _navigationService.GoBack();
                }

            }
            else if(result?.Item1?.header?.status.code == "E260401")
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.DeclarationDisclaimer;

            }
            else
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequestTimeoutDescription;

            }
            
        }

        private void DrawInputsDependingOnCardsOnly(ShipmentCards trackType)
        {
            if (trackType == ShipmentCards.Express)
            {
                if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    DrawShipmentTrack.HasDeclarationNumber = true;
                    DrawShipmentTrack.HasImportYear = true;
                    DrawShipmentTrack.HasBillNumber = false;
                    ShipmentBillNumber = string.Empty;
                }
                else
                {
                    DrawShipmentTrack.HasBillNumber = true;
                    DrawShipmentTrack.HasImportYear = false;
                    DrawShipmentTrack.HasDeclarationNumber = false;
                    ShipmentDeclarationNumber = string.Empty;
                    ShipmentImporterYear = string.Empty;
                }
            }


            else if (trackType == ShipmentCards.Air)
            {
                if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    DrawShipmentTrack.HasDeclarationNumber = true;
                    DrawShipmentTrack.HasImportYear = false;
                    DrawShipmentTrack.HasDeclarationDate = true;
                    DrawShipmentTrack.HasBillNumber = false;
                    ShipmentBillNumber = string.Empty;
                }
                else
                {
                    DrawShipmentTrack.HasBillNumber = true;
                    DrawShipmentTrack.HasDeclarationNumber = false;
                    DrawShipmentTrack.HasDeclarationDate = false;
                    ShipmentDeclarationNumber = string.Empty;
                    DeclarationDateString = string.Empty;
                }
            }


            else if (trackType == ShipmentCards.Sea
                    || trackType == ShipmentCards.Train)
            {
                if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    DrawShipmentTrack.HasDeclarationNumber = true;
                    DrawShipmentTrack.HasDeclarationDate = true;
                    DrawShipmentTrack.HasBillNumber = false;
                    DrawShipmentTrack.HasContainerNumber = false;
                    ShipmentBillNumber = string.Empty;
                    ShipmentContainerNumber = string.Empty;
                }
                else
                {
                    DrawShipmentTrack.HasBillNumber = true;
                    DrawShipmentTrack.HasContainerNumber = true;
                    DrawShipmentTrack.HasDeclarationNumber = false;
                    DrawShipmentTrack.HasDeclarationDate = false;
                    ShipmentDeclarationNumber = string.Empty;
                    DeclarationDateString = string.Empty;
                }
            }
        }

        private bool IsValid(ShipmentCards trackType)
        {
            if (trackType == ShipmentCards.Express)
            {
                if(DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                        || string.IsNullOrWhiteSpace(ShipmentImporterYear))

                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(ShipmentBillNumber))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
            }


            else if (trackType == ShipmentCards.Air)
            {
                if (string.IsNullOrWhiteSpace(SelectedPortName))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
                else if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                         || string.IsNullOrWhiteSpace(DeclarationDateString))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }

                else if (!DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentBillNumber))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
            }


            else if (trackType == ShipmentCards.Sea
                    || trackType == ShipmentCards.Train)
            {
                if (string.IsNullOrWhiteSpace(SelectedPortName))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }

                else if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                         || string.IsNullOrWhiteSpace(DeclarationDateString))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
                else if (!DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentBillNumber)
                        ||string.IsNullOrWhiteSpace(ShipmentContainerNumber))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
            }

            else
            {
                if (string.IsNullOrWhiteSpace(SelectedPortName))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }

                else if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    if (string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                         || string.IsNullOrWhiteSpace(DeclarationDateString))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }
            }

            return true;
        }

        private void ResetTrackShipmentData()
        {
            DrawShipmentTrack = new DrawShipmentTrack();
            isExpressCardSelected = IsExpressShipment = false;
            isAirCardSelected = false;
            isSeaCardSelected = false;
            isLandCardSelected = false;
            isTrainCardSelected = false;
            ShipmentDeclarationNumber = string.Empty;
            ShipmentImporterYear = string.Empty;
            DeclarationDateString = string.Empty;
            ShipmentBillNumber = string.Empty;
            ShipmentContainerNumber = string.Empty;
            SelectedPortName = string.Empty;
            SelectedPortId = 0;
        }

        private void ResetTrackStatus()
        {
            if (ShipmentStatusList == null || ShipmentStatusList.Count == 0)
                return;
            ShipmentStatusList = new System.Collections.ObjectModel.ObservableCollection<Models.TrackShipment.ShipmentStatus>();
            TrackShipmentResponse = new Models.TrackShipment.TrackShipmentModel();
        }

        public void BackMethod()
        {
            if (IsShowBottomSheet)
            {
                IsShowBottomSheet = false;
                if (isAirCardSelected)
                    HeaderTitle = AppResources.AirFreight;

                else if (isSeaCardSelected)
                    HeaderTitle = AppResources.SeaFreight;

                else if (isLandCardSelected)
                    HeaderTitle = AppResources.LandFreight;

                else if (isTrainCardSelected)
                    HeaderTitle = AppResources.TrainFreight;

                else if (HeaderTitle == AppResources.ImporterYear)
                    HeaderTitle = AppResources.ExpressShipping;

                return;
            }
           ResetDate();
           ResetTrackShipmentData();
           ResetTrackStatus();
           _navigationService.GoBack();
        }
        #endregion Methods


        ICommonServices _commonServices;
        ITrackShipment _trackShipment;
        public TrackShipmentViewModel(INavigationService navigationService, IDialogService dialogService, ICommonServices commonServices, ITrackShipment trackShipment) : base(navigationService, dialogService)
        {
            _commonServices = commonServices;
            _trackShipment = trackShipment;
           

        }
    }

    
}

