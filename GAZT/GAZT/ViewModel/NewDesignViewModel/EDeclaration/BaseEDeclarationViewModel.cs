using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Helper;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using EGAZT.AppConfigurations;
using Xamarin.Forms;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class BaseEDeclarationViewModel : BaseViewModel
    {

        #region Properties
        bool isArrivingPlaneSelected = true;
        public bool IsArrivingPlaneSelected { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }

        bool isYesSelected = true;
        public bool IsYesSelected { get { return isYesSelected; } set { isYesSelected = value; RaisePropertyChanged(); } }

        public static Dictionary<string, object> QAnswereDictionary { get; set; }


        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        EDeclerationSubmitModel _submitModel;
        public EDeclerationSubmitModel SubmitModel { get { return _submitModel; } set { _submitModel = value; RaisePropertyChanged(); } }

        public IDictionary<string, object> IamLoginPayloadData;
        #endregion


        #region Commands
        public ICommand CardSelectionCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    IsArrivingPlaneSelected = e == "1" ? true : false;
                    SubmitModel.travelerDeclaration.travelingType = IsArrivingPlaneSelected ? 1 : 2;
                });
            }
        }
         public ICommand ReviewPreviousLoggedInCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("ListUserRequestsPage");
                });
            }
        }

        public ICommand GoToProductDeclarationCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    SubmitModel.travelerDeclaration.travelingType = IsArrivingPlaneSelected ? 1 : 2;
                    _navigationService.NavigateTo("ChooseQuestionsPage");
                });
            }
        }

        public ICommand GoToEDeclarationTermsPopupPageCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    EDeclarationTermsPopupPage poupWindow = new EDeclarationTermsPopupPage();
                    await PopupNavigation.Instance.PushAsync(poupWindow);

                });
            }
        }
        public ICommand OpenCustomInformationLinkCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        Xamarin.Essentials.Launcher.OpenAsync(PageSettings.GetCustomDeclarationInformationURl());
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }
        #endregion
        public IE_DeclerationServices DeclerationServices;
        public BaseEDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService)
        {
            SubmitModel = App.Locator.EDeclerationSubmitModel;
            DeclerationServices = declerationServices;
        }

        public void SetPassangerData(object data)
        {
            App.Locator.StateManager.SetItem("IAMLoginPassengerData", data);
            IDictionary<string, object> iamLoginPayloadData = data as IDictionary<string, object>;

            SubmitModel.travelerDeclaration.firstName = iamLoginPayloadData["FirstName"].ToString();
            SubmitModel.travelerDeclaration.middleName = iamLoginPayloadData["MiddleName"].ToString();
            SubmitModel.travelerDeclaration.lastName = iamLoginPayloadData["LastName"].ToString();
            SubmitModel.travelerDeclaration.FullName = $"{SubmitModel.travelerDeclaration.firstName} {SubmitModel.travelerDeclaration.lastName}";
            App.Locator.StateManager.SetItem("FullName", SubmitModel.travelerDeclaration.FullName);
            SubmitModel.travelerDeclaration.NationalityName = iamLoginPayloadData["Nationality"].ToString();
            SubmitModel.travelerDeclaration.nationality = int.Parse(iamLoginPayloadData["NationalityId"].ToString());
            // Its source is empty so field with nationality
            SubmitModel.travelerDeclaration.travelIssuerName = iamLoginPayloadData["Nationality"].ToString(); 
            SubmitModel.travelerDeclaration.travelIssuerID = int.Parse(iamLoginPayloadData["NationalityId"].ToString());

            SubmitModel.travelerDeclaration.gender = iamLoginPayloadData["Gender"].ToString() == "Male" ? 1 : 2;
            SubmitModel.travelerDeclaration.travelID = iamLoginPayloadData["NationlId"].ToString();
            App.Locator.StateManager.SetItem("TravelId", SubmitModel.travelerDeclaration.travelID);
            SubmitModel.travelerDeclaration.birthDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData["BirthDate"].ToString());

            SubmitModel.travelerDeclaration.passIssuingDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData["ReleaseDate"].ToString());

            SubmitModel.travelerDeclaration.passExpiryDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData["EndDate"].ToString());
        }
        public object GetTokenData(string token = "")
        {
            try
            {
                string secretKey = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
                var payload = JWT.JsonWebToken.DecodeToObject(token, secretKey);
                SetPassangerData(payload);
                return payload;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

    }
}

