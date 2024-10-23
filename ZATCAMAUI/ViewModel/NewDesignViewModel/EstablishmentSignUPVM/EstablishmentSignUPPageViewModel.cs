
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.Nafat;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{

    public class EstablishmentSignUPPageViewModel : BaseViewModel
    {

        #region Variable
        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.IndividualInformation;
        public EstablishmentSignUPTabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                if (_currentTab == value) return;
                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 4;
        #endregion

        #region Propetry


        public string _IndividualBackImg = "vat_tile_listofsignup_W.png";
        public string IndividualBackImg
        {
            get
            {
                return _IndividualBackImg;
            }
            set
            {
                if (_IndividualBackImg == value) return;

                _IndividualBackImg = value;
                OnPropertyChanged("IndividualBackImg");
            }
        }

        public string _EstablishmentBackImg = "vat_tile_listofsignup_W.png";
        public string EstablishmentBackImg
        {
            get
            {
                return _EstablishmentBackImg;
            }
            set
            {
                if (_EstablishmentBackImg == value) return;

                _EstablishmentBackImg = value;
                OnPropertyChanged("EstablishmentBackImg");
            }
        }
        public string _PageTitle = AppResources.ZTERNewAccount;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                if (_PageTitle == value) return;

                _PageTitle = value;
                OnPropertyChanged("PageTitle");
            }
        }

        public string _BodyText = AppResources.ZZZZSelectthetypeofEntity;
        public string BodyText
        {
            get
            {
                return _BodyText;
            }
            set
            {
                if (_BodyText == value) return;

                _BodyText = value;
                OnPropertyChanged("BodyText");
            }
        }
        #endregion

        #region Constructor
        public EstablishmentSignUPPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #endregion


        public ICommand OnEstablishmentCommand
        {

            get
            {
                return new Command(async() =>
                {
                    IndividualBackImg = "vat_tile_listofsignup_W.png";
                    EstablishmentBackImg = "vat_tile_listofsignup.png";
                    await _navigationService.NavigateTo(App.SignUpForEstablishmentPageView);
                });
            }
        }

        public ICommand OnIndividualCommand
        {

            get
            {
                return new Command(async() =>
                {
                    IndividualBackImg = "vat_tile_listofsignup.png";
                    EstablishmentBackImg = "vat_tile_listofsignup_W.png";
                    await MopupService.Instance.PushAsync(new NafathPopUpPage());
                });
            }
        }

        public ICommand OnAppearingEstablishmentSignUPCommand
        {

            get
            {
                return new Command(() =>
                {
                    IndividualBackImg = "vat_tile_listofsignup_W.png";
                    EstablishmentBackImg = "vat_tile_listofsignup_W.png";
                });
            }
        }
    }
}
