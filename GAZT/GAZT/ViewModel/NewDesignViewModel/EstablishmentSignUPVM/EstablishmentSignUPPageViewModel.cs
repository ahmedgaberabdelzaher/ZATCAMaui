using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class EstablishmentSignUPPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Variable
        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.IndividualInformation;
        public EstablishmentSignUPTabEnum currentTab
        {
            get => _currentTab;
            private set
            {
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

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public string _IndividualBackImg = "FP_unselected_tile.png";
        public string IndividualBackImg
        {
            get
            {
                return _IndividualBackImg;
            }
            set
            {
                _IndividualBackImg = value;
                RaisePropertyChanged("IndividualBackImg");
            }
        }

        public string _EstablishmentBackImg = "FP_unselected_tile.png";
        public string EstablishmentBackImg
        {
            get
            {
                return _EstablishmentBackImg;
            }
            set
            {
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
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
        }
        #endregion
    }
}
