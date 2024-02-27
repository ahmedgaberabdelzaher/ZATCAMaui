using System.Web;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using ZATCAMAUI.Views.NewDesign.EDeclaration;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class IAMLoginViewModel : BaseViewModel
    {
        string iAMWbViewSrc;
        public string IAMWbViewSrc { get { return iAMWbViewSrc; } set { iAMWbViewSrc = value; RaisePropertyChanged(); } }

        public int CommingFrom { get; set; }

        string priceText;
        public string PriceText { get { return priceText; } set { priceText = value; RaisePropertyChanged(); } }

        bool isNoUserShowMsg;
        public bool IsNoUserShowMsg { get { return isNoUserShowMsg; } set { isNoUserShowMsg = value; RaisePropertyChanged(); } }

        ZATCAUserRegisterModel User = new ZATCAUserRegisterModel();

        public IAMLoginViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            IAMWbViewSrc = PageSettings.IAMLoginBaseUrl;
        }

        public ICommand OpenIAMRegistrationUrlCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("RegisterZATCAUserPage", CommingFrom);
                    IsNoUserShowMsg = false;

                });
            }
        }

        public void GetIAMToken(string url)
        {
            try
            {


                string token = HttpUtility.ParseQueryString(new Uri(url).Query).Get("token");

                var payload = GetTokenData(token);

                if (payload != null)
                {
                    App.Locator.StateManager.SetItem("IAMLoginPassengerData", payload);

                    IDictionary<string, object> iamLoginPayloadData = payload as IDictionary<string, object>;

                    bool isUserExists = bool.Parse(iamLoginPayloadData["isUserExists"].ToString());

                    if (!isUserExists)
                    {
                        IsNoUserShowMsg = true;
                        MessageTxt = AppResources.IAMUsernNotFoundMSg;
                        HandleUnRegisteredUser(iamLoginPayloadData);
                        return;
                    }

                    var navigation = Application.Current.MainPage.Navigation;

                    var currentPage = navigation.NavigationStack.LastOrDefault();

                    if (CommingFrom == 1)
                    {
                        navigation.InsertPageBefore(new NewDeclarationPage(payload), currentPage);
                        _navigationService.GoBack();
                    }
                    else
                    {
                        navigation.InsertPageBefore(new TransactionReceptionView(payload), currentPage);
                        _navigationService.GoBack();
                    }
                }

            }
            catch (Exception)
            {

            }


        }

        private void HandleUnRegisteredUser(IDictionary<string, object> iamLoginPayloadData)
        {
            bool language = App.IsArabic;

            User.firstName = language ? iamLoginPayloadData["arabicFirstName"].ToString() :
                    iamLoginPayloadData["englishFirstName"].ToString();

            User.secondName = language ? iamLoginPayloadData["arabicFatherName"].ToString() :
                iamLoginPayloadData["englishFatherName"].ToString();

            User.thirdName = language ? iamLoginPayloadData["arabicGrandFatherName"].ToString() :
                iamLoginPayloadData["englishGrandFatherName"].ToString();

            User.fourthName = language ? iamLoginPayloadData["arabicFamilyName"].ToString() :
                iamLoginPayloadData["englishFamilyName"].ToString();

            User.FullName = $"{User.firstName} {User.secondName} {User.thirdName} {User.fourthName}";

            User.nationalityId = int.Parse(iamLoginPayloadData["nationalityCode"].ToString());

            User.gender = iamLoginPayloadData["gender"].ToString() == "Male" ? true : false;

            User.nationalId = iamLoginPayloadData["IdNo"].ToString();

            User.birthDate = iamLoginPayloadData["dob"].ToString();

            User.identityTypeId = int.Parse(iamLoginPayloadData["IdType"].ToString());

            User.cityId = 30;

            User.maritalStatusId = 1;

            User.iqamaExpiryDateHijri = iamLoginPayloadData["iqamaExpiryDateHijri"].ToString();

            User.idExpiryDateHijri = iamLoginPayloadData["idExpiryDateHijri"].ToString();

            User.cardIssueDateHijri = iamLoginPayloadData["cardIssueDateHijri"].ToString();

            User.dateOfBirthHijri = iamLoginPayloadData["dobHijri"].ToString();

            App.Locator.StateManager.SetItem("UnRegisteredUser", User);
        }

    }
}

