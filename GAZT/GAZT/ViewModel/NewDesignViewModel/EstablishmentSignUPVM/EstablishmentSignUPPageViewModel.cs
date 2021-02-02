using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using System;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
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
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
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
                RaisePropertyChanged("IndividualBackImg");
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
                RaisePropertyChanged("EstablishmentBackImg");
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
                RaisePropertyChanged("PageTitle");
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
                RaisePropertyChanged("BodyText");
            }
        }
        #endregion

        #region Constructor
        public EstablishmentSignUPPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
        #endregion
    }
}
