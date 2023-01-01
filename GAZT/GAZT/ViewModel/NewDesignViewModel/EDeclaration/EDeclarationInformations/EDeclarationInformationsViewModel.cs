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
using System.Text.RegularExpressions;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.Helper;

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
        private bool isFirstTime = true;
        public bool isPassengerPage  = true;
        public bool isTripPage;
        public bool isContactPage;

        List<CountryModel> countries = new List<CountryModel>();

        string refNo ;
        public string RefNo  { get { return refNo; } set { refNo = value; RaisePropertyChanged(); } }
    

        #endregion


        #region Commands

        public ICommand OnAppearingInfoPagesCommand
        {

            get
            {
                return new Command(async() =>
                {
                    // this condition to load countries only once for
                    // 3 paages
                    if (isFirstTime)
                    {
                        IsLoading = true;
                        var result = await DeclerationServices.GetCountries();
                        countries = result?.Item1?.data?.ToList();
                        isFirstTime = false;
                        IsLoading = false;
                    }
                    if(isPassengerPage)
                    {
                         
                        if (!SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            IDName = AppResources.ZZNationalID;
                            IDNumberPlaceHolder = "0000000000";
                            IDNumberKeyboard = Keyboard.Numeric;
                            if (SubmitModel.travelerDeclaration.travelID !=null)
                            {
                                if (SubmitModel.travelerDeclaration.travelID.ToLower().StartsWith("1"))
                                {
                                    SubmitModel.travelerDeclaration.travelDocumentType = 5; // Citizen
                                }
                                else
                                {
                                    SubmitModel.travelerDeclaration.travelDocumentType = 3; // Resident
                                }
                            }
                           
                        }
                        else // Visitor
                        {
                            //Set Default value for first time only
                            if(SubmitModel.travelerDeclaration.gender == 0)
                                SubmitModel.travelerDeclaration.gender = 1; // Male

                            //Set Default value for first time only
                            if (SubmitModel.travelerDeclaration.travelDocumentType == 0)
                            {
                                IDName = AppResources.Passport;
                                IDNumberPlaceHolder = "XX000000";
                                IDNumberKeyboard = Keyboard.Text;
                                SubmitModel.travelerDeclaration.travelDocumentType = 4; // Visitor Passport => 4
                            }
                                
                        }
                        
                        HeaderTitle = AppResources.PassengerInformation;
                    }
                    else if(isTripPage)
                    {
                        HeaderTitle = AppResources.TripInformation;
                        IsArrivingPlaneSelected = SubmitModel.travelerDeclaration.travelingType == 1 ? true : false;
                    }
                    else if(isContactPage)
                    {
                        HeaderTitle = AppResources.ContactInformation;
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
                            var entry = e as BorderlessEntry;
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
                            SubmitModel.travelerDeclaration.port = int.Parse(e.Id);
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
                        else
                        {
                            SubmitModel.travelerDeclaration.CountryCode =$"+{Regex.Replace(e.Name, @"[^\d]", "")}";
                            HeaderTitle = AppResources.ContactInformation;
                        }

                        IsShowBottomSheet = false;
                        SearchText = string.Empty;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
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

        public ICommand CloseAcknowledgePopUpPageCommand
        {
            get
            {
                return new Command( async() =>
                {
                    SubmitModel.travelerDeclaration.phoneNumber = SubmitModel.travelerDeclaration.phoneNumber.Remove(0, SubmitModel.travelerDeclaration.CountryCode.Length);
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
                    BackMethod();

                });
            }
        }

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command( _ =>
                {
                    _navigationService.NavigateTo("EDeclarationPaymentPage", TravelerDeclarationResponse);
                    
                });
            }
        }
        public void BackMethod()
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
        }
        

        #endregion
        public EDeclarationInformationsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

