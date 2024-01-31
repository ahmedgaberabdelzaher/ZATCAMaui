using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using ZATCAMAUI.Models.NativeNafath;
using ZATCAMAUI.Views.NewDesign.EDeclaration.PopUpPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
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
                    HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
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
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    SubmitModel.travelerDeclaration.travelingType = IsArrivingPlaneSelected ? 1 : 2;
                    HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
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
                        Launcher.OpenAsync(PageSettings.GetCustomDeclarationInformationURl());
                    }
                    catch (Exception)
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

            try
            {
                if (data == null) return;

                //IDictionary<string, object> iamLoginPayloadData = data as IDictionary<string, object>;
                var iamLoginPayloadData = data as CustomsNafathUserProfile;
                SubmitModel.travelerDeclaration.email = iamLoginPayloadData.email.ToString();
                SubmitModel.travelerDeclaration.phoneNumber = iamLoginPayloadData.mobilenumber.ToString();
                SubmitModel.travelerDeclaration.firstName = iamLoginPayloadData.firstname.ToString();
                SubmitModel.travelerDeclaration.middleName = iamLoginPayloadData.secondname.ToString();
                SubmitModel.travelerDeclaration.lastName = iamLoginPayloadData.thirdname.ToString();
                SubmitModel.travelerDeclaration.FullName = $"{SubmitModel.travelerDeclaration.firstName} {SubmitModel.travelerDeclaration.lastName}";
                App.Locator.StateManager.SetItem("FullName", SubmitModel.travelerDeclaration.FullName);
                SubmitModel.travelerDeclaration.NationalityName = iamLoginPayloadData.nationalitynamearabic ?? iamLoginPayloadData.nationalitynameenglish.ToString();
                SubmitModel.travelerDeclaration.nationality = int.Parse(iamLoginPayloadData.nationalityid.ToString());

                // Its source is empty so it must be KSA as the user maybe resident or citizen.
                SubmitModel.travelerDeclaration.travelIssuerName = App.IsArabic ? "السعودية" : "SAUDI ARABIA";
                SubmitModel.travelerDeclaration.travelIssuerID = 100; // it must be KSA => 100 because the user is resident or citizen

                SubmitModel.travelerDeclaration.gender = iamLoginPayloadData.gender.ToString() == "Male" ? 1 : 2;
                SubmitModel.travelerDeclaration.travelID = iamLoginPayloadData.nationalid.ToString();
                App.Locator.StateManager.SetItem("TravelId", SubmitModel.travelerDeclaration.travelID);
                SubmitModel.travelerDeclaration.birthDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData.birthdate.ToString());

                SubmitModel.travelerDeclaration.passIssuingDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData.cardissuedatehijri.ToString());

                SubmitModel.travelerDeclaration.passExpiryDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData.idexpirydatehijri.ToString());
            }
            catch (Exception)
            {

            }

        }



    }
}

