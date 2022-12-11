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
using System.Linq.Expressions;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using EGAZT.Models.EDeclerationsModel;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel : BaseEDeclarationViewModel
    {
        #region Properties

        private bool isItsSourceSelected;
        private bool isNationalitySelected;
        private bool isComingGoingSelected;
        private bool isPortSelected;
        private bool isTravelPurposeSelected;
        public bool isPassengerPage  = true;
        public bool isTripPage;
        public bool isContactPage;
        public bool isSuccessPage;

        List<CountryModel> countries = new List<CountryModel>();

        #endregion


        #region Commands

        public ICommand OnAppearingInfoPagesCommand
        {

            get
            {
                return new Command(async() =>
                {
                    IsLoading = true;
                    if(isPassengerPage)
                    {
                        
                        if (!SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            //SubmitModel.travelerDeclaration.NationalityName = ;
                            //SubmitModel.travelerDeclaration.travelIssuerName = ;

                            if (SubmitModel.travelerDeclaration.travelID.ToLower().StartsWith("1"))
                            {
                                SubmitModel.travelerDeclaration.travelDocumentType = 5; // Citizen
                            }
                            else
                            {
                                SubmitModel.travelerDeclaration.travelDocumentType = 3; // Resident
                            }
                        }
                        else // Visitor
                        {
                            SubmitModel.travelerDeclaration.gender = 1; // Male
                            SubmitModel.travelerDeclaration.travelDocumentType = 4; // Visitor Passport => 4
                        }
                       
                        HeaderTitle = AppResources.PassengerInformation;
                    }
                    else if(isTripPage)
                    {
                        HeaderTitle = AppResources.TripInformation;
                        IsArrivingPlaneSelected = SubmitModel.travelerDeclaration.travelingType == 1 ? true : false;
                        SubmitModel.travelerDeclaration.tripeType = 1; // Air Trip
                    }
                    else if(isContactPage)
                    {
                        HeaderTitle = AppResources.ContactInformation;
                    }


                    var result = await DeclerationServices.GetCountries();
                    countries = result?.Item1?.data.ToList();

                    
                    IsLoading = false;
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

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    try
                    {
                        IsLoading = true;

                        if (isNationalitySelected)
                        {

                            SubmitModel.travelerDeclaration.NationalityName = e.Name;
                            SubmitModel.travelerDeclaration.nationality = int.Parse(e.Id);
                            isNationalitySelected = false;
                            HeaderTitle = AppResources.PassengerInformation;
                        }
                        else if (isItsSourceSelected)
                        {
                            SubmitModel.travelerDeclaration.travelIssuerID = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.travelIssuerName = e.Name;
                            SubmitModel.travelerDeclaration.passIssuingCountry = int.Parse(e.Id);
                            isItsSourceSelected = false;
                            HeaderTitle = AppResources.PassengerInformation;
                        }
                        else if (isComingGoingSelected)
                        {
                            SubmitModel.travelerDeclaration.arrivingFromDepartingTo = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.arrivingFromDepartingToName = e.Name;
                            isComingGoingSelected = false;
                            HeaderTitle = AppResources.TripInformation;
                        }
                        else if (isPortSelected)
                        {
                            //SubmitModel.travelerDeclaration.port = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.port = 23;
                            SubmitModel.travelerDeclaration.portName = e.Name;
                            isPortSelected = false;
                            HeaderTitle = AppResources.TripInformation;
                        }
                        else if (isTravelPurposeSelected)
                        {
                            SubmitModel.travelerDeclaration.travelPurpose = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.travelPurposeName = e.Name;
                            isTravelPurposeSelected = false;
                            HeaderTitle = AppResources.TripInformation;
                        }

                        IsShowBottomSheet = false;
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

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsShowBottomSheet)
                    {
                        IsShowBottomSheet = false;
                        if (isPassengerPage)
                            HeaderTitle = AppResources.PassengerInformation;

                        else if (isTripPage)
                            HeaderTitle = AppResources.PassengerInformation;

                        else 
                            HeaderTitle = AppResources.ContactInformation;


                        return;
                    }
                  
                    _navigationService.GoBack();

                });
            }
        }

#endregion
public EDeclarationInformationsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

