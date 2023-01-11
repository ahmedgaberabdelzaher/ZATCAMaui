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

namespace EGAZT.ViewModel.NewDesignViewModel.TrackShipment
{
    public class TrackShipmentViewModel : BaseViewModel
    {
        #region Properties
        public bool isExpressCardSelected;

        public bool isAirCardSelected;

        public bool isSeaCardSelected;

        public bool isLandCardSelected;

        public bool isTrainCardSelected;

        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        DateTime _MaximumDate = DateTime.Today.AddHours(-24);
        public DateTime MaximumDate { get { return _MaximumDate; } set { _MaximumDate = value; RaisePropertyChanged(); } }

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

        string selectedPort;
        public string SelectedPort { get { return selectedPort; } set { selectedPort = value; RaisePropertyChanged(); } }

        public DateTime DeclarationDate { get; set; }
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

                        bool isValid = false;

                        if (isExpressCardSelected)
                            isValid = IsValid(ShipmentCards.Express);

                        else if (isAirCardSelected)
                            isValid = IsValid(ShipmentCards.Air);

                        else if (isSeaCardSelected)
                            isValid = IsValid(ShipmentCards.Sea);

                        else if (isLandCardSelected)
                            isValid = IsValid(ShipmentCards.Land);

                        else
                            isValid = IsValid(ShipmentCards.Train);

                        if(isValid)
                            //_navigationService.NavigateTo("TrackShipmentPage");

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

        public ICommand SelectedDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {

                        DeclarationDateString = Helper.DateTimeHelper.DateTimeFormater(DeclarationDate);

                    }
                    catch (Exception ex)
                    {
                        DeclarationDateString = "dd/MM/yyyy";
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
                    control?.Focus();

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

                            SelectedPort = e.Name;
                            var portID = int.Parse(e.Id);
                            HeaderTitle = AppResources.AirFreight;
                        }
                        else if (isSeaCardSelected)
                        {
                            SelectedPort = e.Name;
                            var portID = int.Parse(e.Id);
                            HeaderTitle = AppResources.SeaFreight;
                        }
                        else if (isLandCardSelected)
                        {
                            SelectedPort = e.Name;
                            var portID = int.Parse(e.Id);
                            HeaderTitle = AppResources.LandFreight;
                        }
                        else if (isTrainCardSelected)
                        {
                            SelectedPort = e.Name;
                            var portID = int.Parse(e.Id);
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

        private void DrawExpressShipping()
        {
            isExpressCardSelected = true;
            HeaderTitle = AppResources.ExpressShipping;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.ExpressShipping}";
            DrawShipmentTrack.ShipmentCardImage = "expressDark.png";
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Express);
        }

        private void DrawAirShipping()
        {
            isAirCardSelected = true;
            HeaderTitle = AppResources.AirFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.AirFreight}";
            DrawShipmentTrack.ShipmentCardImage = "airDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Air);
        }

        private void DrawSeaShipping()
        {
            isSeaCardSelected = true;
            HeaderTitle = AppResources.SeaFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.SeaFreight}";
            DrawShipmentTrack.ShipmentCardImage = "seaDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Sea);
        }

        private void DrawLandShipping()
        {
            isLandCardSelected = true;
            HeaderTitle = AppResources.LandFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.LandFreight}";
            DrawShipmentTrack.ShipmentCardImage = "landDark.png";
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasDeclarationNumber = true;
            DrawShipmentTrack.HasDeclarationDate = true;
        }

        private void DrawTrainShipping()
        {
            isTrainCardSelected = true;
            HeaderTitle = AppResources.TrainFreight;
            DrawShipmentTrack.ShipmentTrackName = $"{AppResources.Track} {AppResources.TrainFreight}";
            DrawShipmentTrack.ShipmentCardImage = "trainDark.png";
            DrawShipmentTrack.HasSubTitle = true;
            DrawShipmentTrack.HasPortName = true;
            DrawShipmentTrack.HasSearchBy = true;
            DrawShipmentTrack.HasDeclarationCards = true;
            DrawInputsDependingOnCardsOnly(ShipmentCards.Train);
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
                    DeclarationDate = DateTime.Now;
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
                    DeclarationDate = DateTime.Now;
                }
            }
        }

        private bool IsValid(ShipmentCards trackType)
        {
            if (trackType == ShipmentCards.Express)
            {
                if (DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentDeclarationNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
                else if (!DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentBillNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
            }


            else if (trackType == ShipmentCards.Air)
            {
                if (DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                    && string.IsNullOrWhiteSpace(DeclarationDateString))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
                else if (DrawShipmentTrack.IsDeclarationSelected
                        && DeclarationDate > DateTime.Now.Date)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.DateBirthValidation;
                    return false;
                }

                else if (!DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentBillNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
            }


            else if (trackType == ShipmentCards.Sea
                    || trackType == ShipmentCards.Train)
            {
                if (DrawShipmentTrack.IsDeclarationSelected
                   && string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                   && string.IsNullOrWhiteSpace(DeclarationDateString))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
                else if (DrawShipmentTrack.IsDeclarationSelected
                        && DeclarationDate > DateTime.Now.Date)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.DateBirthValidation;
                    return false;
                }
                else if (!DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentBillNumber)
                    && string.IsNullOrWhiteSpace(ShipmentContainerNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
            }

            else
            {
                if (DrawShipmentTrack.IsDeclarationSelected
                    && string.IsNullOrWhiteSpace(ShipmentDeclarationNumber)
                    && string.IsNullOrWhiteSpace(DeclarationDateString))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }
                else if (DrawShipmentTrack.IsDeclarationSelected
                       && DeclarationDate > DateTime.Now.Date)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.DateBirthValidation;
                    return false;
                }
            }

            return true;
        }

        public void ResetData()
        {
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
            SelectedPort = string.Empty;
            DeclarationDate = DateTime.Now.Date;
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
        public TrackShipmentViewModel(INavigationService navigationService, IDialogService dialogService, ICommonServices commonServices) : base(navigationService, dialogService)
        {
            _commonServices = commonServices;
        }
    }

    public enum ShipmentCards
    {
        // This value is related to port type
        Sea = 1,
        Train = 2,
        Land = 3,
        Air = 4,
        Express = 5

    }

    public class DrawShipmentTrack : BindableBase
    {
        bool hasSubTitle;
        public bool HasSubTitle { get { return hasSubTitle; } set { hasSubTitle = value; RaisePropertyChanged(); } }

        bool hasPortName;
        public bool HasPortName { get { return hasPortName; } set { hasPortName = value; RaisePropertyChanged(); } }

        bool hasSearchBy;
        public bool HasSearchBy { get { return hasSearchBy; } set { hasSearchBy = value; RaisePropertyChanged(); } }

        bool hasDeclarationNumber;
        public bool HasDeclarationNumber { get { return hasDeclarationNumber; } set { hasDeclarationNumber = value; RaisePropertyChanged(); } }

        bool hasDeclarationDate;
        public bool HasDeclarationDate { get { return hasDeclarationDate; } set { hasDeclarationDate = value; RaisePropertyChanged(); } }

        bool hasBillNumber;
        public bool HasBillNumber { get { return hasBillNumber; } set { hasBillNumber = value; RaisePropertyChanged(); } }

        bool hasContainerNumber;
        public bool HasContainerNumber { get { return hasContainerNumber; } set { hasContainerNumber = value; RaisePropertyChanged(); } }

        bool hasDeclarationCards;
        public bool HasDeclarationCards { get { return hasDeclarationCards; } set { hasDeclarationCards = value; RaisePropertyChanged(); } }

        bool isDeclarationSelected = true;
        public bool IsDeclarationSelected { get { return isDeclarationSelected; } set { isDeclarationSelected = value; RaisePropertyChanged(); } }

        string shipmentTrackName;
        public string ShipmentTrackName { get { return shipmentTrackName; } set { shipmentTrackName = value; RaisePropertyChanged(); } }

        string shipmentCardImage;
        public string ShipmentCardImage { get { return shipmentCardImage; } set { shipmentCardImage = value; RaisePropertyChanged(); } }
    }
}

