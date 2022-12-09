using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using EGAZT.Controls;
using GAZT;
using System.Linq;
using System.Collections.Generic;
using EGAZT.Services.Interface;
using EGAZT.Models.EDeclerationsModel;
using System.Linq.Expressions;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclarationInformationsViewModel : BaseEDeclarationViewModel
    {
        #region Properties

        private bool isItsSourceSelected;
        private bool isNationalitySelected;
        private bool isComingGoingSelected;
        private bool isPortSelected;
        private bool isTravelPurposeSelected;

        PassengerModel passenger = new PassengerModel();
        public PassengerModel Passenger { get { return passenger; } set { passenger = value; } }

        TripCardModel tripCard = new TripCardModel();
        public TripCardModel TripCard { get { return tripCard; } set { tripCard = value; } }

        TripInfoModel tripInfo = new TripInfoModel();
        public TripInfoModel TripInfo { get { return tripInfo; } set { tripInfo = value; } }
        #endregion


        #region Commands
        public ICommand IDSelectionCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsYesSelected)
                    {
                        IsYesSelected = false;

                        if (passenger.travelID.ToLower().StartsWith("1"))
                        {
                            // passenger.travelDocumentType =; 
                        }
                        else if (passenger.travelID.ToLower().StartsWith("2"))
                        {

                        }
                        else
                        {
                            // Passport
                        }
                    }
                    else
                    {
                        IsYesSelected = true;
                    }
                });
            }
        }

        public ICommand GenderSelectionCommand
        {
            get
            {
                return new Command<string>((g) =>
                {
                    if (IsYesSelected)
                    {
                        IsYesSelected = false;
                        Passenger.gender = int.Parse(g); // Male
                    }
                    else
                    {
                        IsYesSelected = true;
                        Passenger.gender = int.Parse(g); // Female
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

        public ICommand OpenItsSourceCommand
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    isItsSourceSelected = true;
                    //var reportType = await this._submitReportServices.GetReportType();
                    //var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                    //BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ItsSource;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
                });

            }
        }

        public ICommand OpenNationalityCommand
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    isNationalitySelected = true;
                    //var reportType = await this._submitReportServices.GetReportType();
                    //var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                    //BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ESTNationalityLabel;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
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

                        //if (isNationalitySelected)
                        //{

                        //    SubmitReport.ReportTypeName = e.Name;
                        //    SubmitReport.ReportTaxType = e.Id;
                        //    ReportCategory = await this._submitReportServices.GetReportCategories(SubmitReport?.ReportTaxType);
                        //    var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                        //    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        //    isNationalitySelected = false;
                        //}
                        //else if (isItsSourceSelected)
                        //{
                        //    SubmitReport.ReportCategoryName = e.Name;
                        //    SubmitReport.ReportCategory = e.Id;
                        //    isItsSourceSelected = false;
                        //    SubmitReport.MissedFieldName = string.Empty;
                        //    SubmitReport.MissedField = string.Empty;
                        //    IsMissingFieldShowen = !string.IsNullOrWhiteSpace(SubmitReport.ReportCategoryName) && SubmitReport.ReportCategory.ToLower().Equals("v36") ? true : false;
                        //}

                        //IsShowBottomSheet = false;
                        //HeaderTitle = AppResources.Submitareport;
                        //SearchText = string.Empty;
                        //TempBottomSheetList = BottomSheetList;
                        //IsLoading = false;
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

        public ICommand GoToTripInfoCommand
        {
            get
            {
                return new Command(_ =>
                {
                    _navigationService.NavigateTo("TripInformationPage");
                });
            }
        }
        public ICommand OpenComingGoingCommand
        {
            get
            {
                return new Command(_ =>
                {
                    IsArrivingPlaneSelected = IsArrivingPlaneSelected == true ? false : true;
                });
            }
        }
        public ICommand TripCardCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    var selectedTrip = int.Parse(e);

                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        TripCard.AirImage = "QSelected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.White;
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        TripCard.IsAirTripSelected = true;
                    }

                    else if (selectedTrip == (int)TripName.LandTrip)
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QSelected.png";

                        TripCard.AirTextColor = Color.FromHex("#002447");
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.White;

                        TripCard.IsAirTripSelected = false;
                    }
                    else
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QSelected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.FromHex("#002447");
                        TripCard.SeaTextColor = Color.White;
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        TripCard.IsAirTripSelected = false;
                    }

                });
            }
        }

        #endregion
        public EDeclarationInformationsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

