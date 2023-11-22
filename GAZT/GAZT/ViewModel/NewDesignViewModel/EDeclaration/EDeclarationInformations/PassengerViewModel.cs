using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Controls;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.ObjectModel;
using EGAZT.Helper;
using System.Text.RegularExpressions;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel
    {

        bool isMaleSelected = true;
        public bool IsMaleSelected { get { return isMaleSelected; } set { isMaleSelected = value; RaisePropertyChanged(); } }

        string iDName = AppResources.Passport;
        public string IDName { get { return iDName; } set { iDName = value; RaisePropertyChanged(); } }

        string _ReleaseDateString;
        public string ReleaseDateString { get { return _ReleaseDateString; } set { _ReleaseDateString = value; RaisePropertyChanged(); } }

        string _EndDateString;
        public string EndDateString { get { return _EndDateString; } set { _EndDateString = value; RaisePropertyChanged(); } }

        string _BirthDateString;
        public string BirthDateString { get { return _BirthDateString; } set { _BirthDateString = value; RaisePropertyChanged(); } }

        string iDNumberPlaceHolder = "XX000000";
        public string IDNumberPlaceHolder { get { return iDNumberPlaceHolder; } set { iDNumberPlaceHolder = value; RaisePropertyChanged(); } }

        Keyboard iDNumberKeyboard = Keyboard.Text;
        public Keyboard IDNumberKeyboard { get { return iDNumberKeyboard; } set { iDNumberKeyboard = value; RaisePropertyChanged(); } }

      


        public ICommand IDSelectionCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    try
                    {

                        if (SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            if (IsYesSelected)
                            {
                                IsYesSelected = false;
                                IDName = AppResources.GccNationalID;
                                IDNumberKeyboard = Keyboard.Numeric;
                                IDNumberPlaceHolder = "0000000000";
                                SubmitModel.travelerDeclaration.travelID = string.Empty;
                                SubmitModel.travelerDeclaration.travelDocumentType = int.Parse(e); // Visitor GCC=> 16

                            }
                            else
                            {
                                IsYesSelected = true;
                                IDName = AppResources.Passport;
                                IDNumberKeyboard = Keyboard.Text;
                                IDNumberPlaceHolder = "XX000000";
                                SubmitModel.travelerDeclaration.travelID = string.Empty;
                                SubmitModel.travelerDeclaration.travelDocumentType = int.Parse(e); // Visitor Passport => 4
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }

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

                        if (control.ClassId.ToLower().Equals("releasedateentry"))
                        {
                            ReleaseDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passIssuingDate);
                        }
                        else if (control.ClassId.ToLower().Equals("enddateentry"))
                        {
                            EndDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passExpiryDate);
                        }
                        else
                        {
                            BirthDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.birthDate);
                        }

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
                        control?.Focus();

                        if (control.ClassId.ToLower().Equals("releasedateentry") &&
                            SubmitModel.travelerDeclaration.passIssuingDate.Date == DateTime.Now.Date.AddHours(-24))
                            ReleaseDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passIssuingDate);

                        else if (control.ClassId.ToLower().Equals("enddateentry") &&
                            SubmitModel.travelerDeclaration.passExpiryDate.Date == DateTime.Now.Date)
                            EndDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passExpiryDate);

                        else
                            BirthDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.birthDate);
                    }
                    catch (Exception)
                    {

                    }
                   
                });
            }
        }

        public ICommand OpenNationalityCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        if (SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            IsLoading = true;
                            isNationalitySelected = true;
                            isItsSourceSelected = false;
                            isPortSelected = false;
                            isComingGoingSelected = false;
                            isTravelPurposeSelected = false;
                            isPlatesCountrySelected = false;
                            isPlatesCitySelected = false;
                            var result = countries?.Select(c => new BottomSheetModel() { Id = c.countryCode.ToString(), Name = c.Name });
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            IsShowBottomSheet = true;
                            HeaderTitle = AppResources.ESTNationalityLabel;
                            TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                            IsLoading = false;

                        }
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
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
                    if (SubmitModel.travelerDeclaration.Isvisitor)
                    {
                        if (IsMaleSelected)
                        {
                            IsMaleSelected = false;
                            SubmitModel.travelerDeclaration.gender = int.Parse(g); // Male => 1
                        }
                        else
                        {
                            IsMaleSelected = true;
                            SubmitModel.travelerDeclaration.gender = int.Parse(g); // Female =>2
                        }
                    }

                });
            }
        }

        public ICommand OpenItsSourceCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            isNationalitySelected = false;
                            isItsSourceSelected = true;
                            isPortSelected = false;
                            isComingGoingSelected = false;
                            isTravelPurposeSelected = false;
                            isPlatesCountrySelected = false;
                            isPlatesCitySelected = false;
                            var result = countries?.Select(c => new BottomSheetModel() { Id = c.countryCode.ToString(), Name = c.Name });
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            IsShowBottomSheet = true;
                            HeaderTitle = AppResources.ItsSource;
                            TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);

                        }
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
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
                    // in case visitor (navigation => Passenger, Trip, contact & payment)
                    if (SubmitModel.travelerDeclaration.Isvisitor)
                    {
                        if (IsValidatePassenger())
                        {
                            isTripPage = true;
                            _navigationService.NavigateTo("TripInformationPage");
                        }
                    }

                    // in case loggedIn (navigation => come from trip to show passenger, contact & payment)
                    else
                    {
                        if (IsValidatePassenger())
                        {
                            isContactPage = true;
                            _navigationService.NavigateTo("ContactInformationPage");
                        }
                    }

                });
            }
        }

        private bool IsValidatePassenger()
        {
            try
            {
                if (SubmitModel.travelerDeclaration.Isvisitor)
                {

                    if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.firstName)
                        || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.middleName)
                        || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.lastName)
                        || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.NationalityName)
                        || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.travelID)
                        || (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.travelIssuerName) && SubmitModel.travelerDeclaration.Isvisitor)
                        || string.IsNullOrWhiteSpace(ReleaseDateString)
                        || string.IsNullOrWhiteSpace(EndDateString)
                        || string.IsNullOrWhiteSpace(BirthDateString))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }

                    else if (SubmitModel.travelerDeclaration.birthDate.Date > DateTime.Now.Date)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.DateBirthValidation;
                        return false;
                    }
                    else if (SubmitModel.travelerDeclaration.passIssuingDate.Date > DateTime.Now.Date)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ReleaseDateValidation;
                        return false;
                    }
                    else if (SubmitModel.travelerDeclaration.passExpiryDate.Date < DateTime.Now.Date)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.EndDateValidation;
                        return false;
                    }
                   

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}

