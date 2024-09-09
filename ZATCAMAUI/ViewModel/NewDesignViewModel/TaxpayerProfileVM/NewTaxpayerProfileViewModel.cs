

using System.Collections.ObjectModel;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.TaxpayerProfile;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{

    public class NewTaxpayerProfileViewModel : BaseViewModel
    {
        #region Variable
        public string UpdatedMobileNumber = string.Empty;
        public string UpdatedEmail = string.Empty;
        ObservableCollection<InternationalMobileData> mobileData = null;
        #endregion

        #region Properties

        private TINStatus _listTINStatus;
        public TINStatus ListTINStatus
        {
            get { return _listTINStatus; }
            set
            {
                if (_listTINStatus == value) return;

                _listTINStatus = value;
                OnPropertyChanged("ListTINStatus");
            }
        }
        private string _TinStatusLabelText;
        public string TinStatusLabelText
        {
            get
            {
                return _TinStatusLabelText;
            }
            set
            {
                if (_TinStatusLabelText == value) return;

                _TinStatusLabelText = value;
                OnPropertyChanged("TinStatusLabelText");
            }
        }
        private string _ResidenceText;
        public string ResidenceText
        {
            get
            {
                return _ResidenceText;
            }
            set
            {
                if (_ResidenceText == value) return;

                _ResidenceText = value;
                OnPropertyChanged("ResidenceText");
            }
        }
        private string _TPProfileNameLbl;
        public string TPProfileNameLbl
        {
            get { return _TPProfileNameLbl; }
            set
            {

                if (_TPProfileNameLbl == value) return;

                _TPProfileNameLbl = value;
                OnPropertyChanged("TPProfileNameLbl");
            }
        }

        private string _TINLabel;
        public string TINLabel
        {
            get { return _TINLabel; }
            set
            {
                if (_TINLabel == value) return;

                _TINLabel = value;
                OnPropertyChanged("TINLabel");
            }
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                if (_MobileNumber == value) return;

                _MobileNumber = value;
                OnPropertyChanged("MobileNumber");
            }
        }

        private string _EmailEntry;
        public string EmailEntry
        {
            get { return _EmailEntry; }
            set
            {
                if (_EmailEntry == value) return;

                _EmailEntry = value;
                OnPropertyChanged("EmailEntry");
            }
        }

        private string _PasswordEntry;
        public string PasswordEntry
        {
            get { return _PasswordEntry; }
            set
            {
                if (_PasswordEntry == value) return;

                _PasswordEntry = value;
                OnPropertyChanged("PasswordEntry");
            }
        }

        private string _ShowHidePasswordImage;
        public string ShowHidePasswordImage
        {
            get { return _ShowHidePasswordImage; }
            set
            {
                if (_ShowHidePasswordImage == value) return;

                _ShowHidePasswordImage = value;
                OnPropertyChanged("ShowHidePasswordImage");
            }
        }
        #endregion

        public NewTaxpayerProfileViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }

        public virtual ICommand OnManagerDetailsCommand
        {
            get
            {
                return new Command(_ =>
                {
                    MopupService.Instance.PushAsync(new UpdateManagerDetailsPopUp());

                });
            }
        }


        public virtual ICommand OnEmailEditCommand
        {
            get
            {
                return new Command(_ =>
                {
                    MopupService.Instance.PushAsync(new UpdateEmailPopUp());

                });
            }
        }

        public virtual ICommand OnPasswordEditCommand
        {
            get
            {
                return new Command(_ =>
                {
                    MopupService.Instance.PushAsync(new UpdatePasswordPopUp());

                });
            }
        }

        public virtual ICommand OnMobileEditCommand
        {
            get
            {
                return new Command(async _ =>
                {
                  

                    try
                    {
                        IsLoading = true;

                        if (mobileData == null)
                            mobileData = await WebServiceManager.GAZTGetMobileRegionDropdown();

                        await MopupService.Instance.PushAsync(new UpdateMobilePopUp(mobileData));

                        IsLoading = false;
                    }

                    catch (Exception)
                    {
                       IsLoading = false;
                    }

                });
            }
        }

        public async Task GetTinStatusDATA()
        {
          

            try
            {
                IsLoading = true;
                string Lang = UtilityManager.GetLanguageParameter();
                ListTINStatus = new TINStatus();
                ListTINStatus = await WebServiceManager.GAZTGetTinStatus(Lang, App.TP.TIN);
                UpdateTinStatus();
                IsLoading = false;
                PopToRootPage();
            }

            catch (Exception )
            {
                IsLoading = false;
                TinStatusLabelText = " - ";


            }
        }

        public void UpdateTinStatus()
        {
            if (ListTINStatus != null)
            {
                if (ListTINStatus.d != null)
                {
                    if (!String.IsNullOrEmpty(ListTINStatus.d.statusDescription))
                        TinStatusLabelText = ListTINStatus.d.statusDescription;
                    else
                        TinStatusLabelText = " - ";
                }
                else
                    TinStatusLabelText = " - ";
            }
            else
                TinStatusLabelText = " - ";
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread( () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }

    }
}
