using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Services.Classes;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using EGAZT.Controls;
using Prism.Mvvm;
using Xamarin.Forms;
using EGAZT.Helper;
using EGAZT.Models.TrackShipment;

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
    public partial class TrackShipmentViewModel : BaseViewModel
    {
        #region Properties
        
        ObservableCollection<ShipmentStatus> shipmentStatusList = new ObservableCollection<ShipmentStatus>();

        public ObservableCollection<ShipmentStatus> ShipmentStatusList { get { return shipmentStatusList; } set { shipmentStatusList = value; RaisePropertyChanged(); } }


        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        DrawShipmentTrack drawShipmentTrack = new DrawShipmentTrack();
        public DrawShipmentTrack DrawShipmentTrack { get { return drawShipmentTrack; } set { drawShipmentTrack = value; RaisePropertyChanged(); } }

        string shipmentDeclarationNumber;
        public string ShipmentDeclarationNumber { get { return shipmentDeclarationNumber; } set { shipmentDeclarationNumber = value; RaisePropertyChanged(); } }

        string declarationDateString;
        public string DeclarationDateString { get { return declarationDateString; } set { declarationDateString = value; RaisePropertyChanged(); } }

        string shipmentBillNumber;
        public string ShipmentBillNumber { get { return shipmentBillNumber; } set { shipmentBillNumber = value; RaisePropertyChanged(); } }

        string shipmentContainerNumber;
        public string ShipmentContainerNumber { get { return shipmentContainerNumber; } set { shipmentContainerNumber = value; RaisePropertyChanged(); } }

        string selectedPortName;
        public string SelectedPortName { get { return selectedPortName; } set { selectedPortName = value; RaisePropertyChanged(); } }

        int selectedPortId;
        public int SelectedPortId { get { return selectedPortId; } set { selectedPortId = value; RaisePropertyChanged(); } }

        TrackShipmentModel trackShipmentResponse;
        public TrackShipmentModel TrackShipmentResponse { get { return trackShipmentResponse; } set { trackShipmentResponse = value; RaisePropertyChanged(); } }

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
                            await GetExpressShipping(DrawShipmentTrack.IsDeclarationSelected);


                        else if (isAirCardSelected)
                            await GetAirShipping(DrawShipmentTrack.IsDeclarationSelected);

                        else if (isSeaCardSelected)
                            await GetSeaShipping(DrawShipmentTrack.IsDeclarationSelected);

                        else if (isLandCardSelected)
                            await GetLandShipping();

                        else
                            await GetTrainShipping(DrawShipmentTrack.IsDeclarationSelected);

                        

                        IsLoading = false;
                    }
                    catch (Exception ex)
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
                return new Command<GAZT.CustomControl.CustomHijriDatePicker>((control) =>
                {
                    try
                    {
                        SetDefaultDate();
                        DeclarationDateString = HijriDateToBeDisplayed;
                        control.IsOpen = true;
                    }
                    catch (Exception ex)
                    {
                        DeclarationDateString = "dd/MM/yyyy";
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
                    catch (Exception ex)
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
                            var entry = e as GAZT.BorderlessEntry;
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
                    catch (Exception ex)
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

                        IsShowBottomSheet = false;
                        SearchText = string.Empty;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception ex)
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
                    _navigationService.NavigateTo("/Home", "0");

                });
            }
        }

        public ICommand SelectedDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (TodayDateinHijri != null && TodayDateinHijri.Count > 0)
                    {
                        string month = TodayDateinHijri[1].ToString();
                        string day; string year;
                        if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                        {
                            day = TodayDateinHijri[0].ToString();
                            year = TodayDateinHijri[2].ToString();
                        }
                        else
                        {
                            day = TodayDateinHijri[2].ToString();
                            year = TodayDateinHijri[0].ToString();
                        }
                        HijriDateToBeDisplayed = day + "/" + month + "/" + year;
                       // DeclarationDateString = HijriDateToBeDisplayed;
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
            catch (Exception ex)
            {
                IsLoading = false;
                IsShowMsgView = true;
                MessageTxt = AppResources.RequestTimeoutDescription;
            }

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

                    foreach (var item in TrackShipmentResponse?.activities)
                    {
                        var status = new ShipmentStatus();

                        status.ShipmentStatusDateString = string.Format("{0:dd/MM/yyyy  hh:mm tt}", item.activityDate);
                        status.ShipmentStatusValue = item.Name;
                        if (trackShipmentResponse?.activities.Count > 1)
                        {
                            // Draw start circle for first item only
                            if (trackShipmentResponse.activities.First() == item)
                            {
                                status.StatusImage = "startCircle.png";
                                status.HasVerticalLine = false;
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
                }

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
                    DrawShipmentTrack.HasBillNumber = false;
                    ShipmentBillNumber = string.Empty;
                }
                else
                {
                    DrawShipmentTrack.HasBillNumber = true;
                    DrawShipmentTrack.HasDeclarationNumber = false;
                    ShipmentDeclarationNumber = string.Empty;
                }
            }


            else if (trackType == ShipmentCards.Air)
            {
                if (DrawShipmentTrack.IsDeclarationSelected)
                {
                    DrawShipmentTrack.HasDeclarationNumber = true;
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
                    if(string.IsNullOrWhiteSpace(ShipmentDeclarationNumber))
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

        public void ResetTrackShipmentData()
        {
            App.Locator.StateManager.SetItem("CardImage", DrawShipmentTrack.ShipmentCardImage);
            DrawShipmentTrack = new DrawShipmentTrack();
            isExpressCardSelected = false;
            isAirCardSelected = false;
            isSeaCardSelected = false;
            isLandCardSelected = false;
            isTrainCardSelected = false;
            ShipmentDeclarationNumber = string.Empty;
            DeclarationDateString = string.Empty;
            ShipmentBillNumber = string.Empty;
            ShipmentContainerNumber = string.Empty;
            SelectedPortName = string.Empty;
            SelectedPortId = 0;
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

                return;
            }

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

