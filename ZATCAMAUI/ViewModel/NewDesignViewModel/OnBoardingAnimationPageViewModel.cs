
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.Views.NewDesign.OnboardingPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
  
    public class GAZTNewDesignOnBoardingAnimationPageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Boarding> boardings;
        private string nextButtonText = AppResources.ZZNext;
        private string _LanguageText = "Set To English";// AppResources.ZZZSetToEnglish;
        private bool isSkipButtonVisible = true;
        private int selectedIndex;

        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 3;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationPageViewModel" /> class.
        /// </summary>
        public GAZTNewDesignOnBoardingAnimationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            SkipCommand = new Command(Skip);
            NextCommand = new Command(Next);
            // this.ChangeLangCommand = new Command(this.ChangeLanguage);

            Boardings = new ObservableCollection<Boarding>
                 {
                new Boarding()
                {
                    ImagePath = AppResources.NDCommitmentsImage,
                    Header = AppResources.NDCommitments,
                    Content = AppResources.NDDontMissObligation,
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding()
                {
                    ImagePath =AppResources.NDSubmisitionsImage,//Correspondence 
                    Header = AppResources.NDReturnSubmission,
                    Content = AppResources.NDNewEasyWayToCommunicate,
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding()
                {
                    ImagePath = AppResources.NDInboxImage, // VAT RETURN
                    Header = AppResources.NDInboxnNotification,
                    Content = AppResources.NDNewEasyWayToCommunicate,
                    RotatorItem = new WalkthroughItemPage()
                },
               };


            // Set bindingcontext to content view.
            foreach (var boarding in Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get
            {
                return boardings;
            }

            set
            {
                if (boardings == value)
                {
                    return;
                }

                boardings = value;
                OnPropertyChanged("Boardings");
            }
        }

        public string NextButtonText
        {
            get
            {
                return nextButtonText;
            }

            set
            {
                if (nextButtonText == value)
                {
                    return;
                }

                nextButtonText = value;
                OnPropertyChanged("NextButtonText");
            }
        }

        public string LanguageText
        {
            get
            {
                return _LanguageText;
            }

            set
            {
                if (_LanguageText == value)
                {
                    return;
                }

                _LanguageText = value;
                OnPropertyChanged("LanguageText");
            }
        }

        public bool IsSkipButtonVisible
        {
            get
            {
                return isSkipButtonVisible;
            }

            set
            {
                if (isSkipButtonVisible == value)
                {
                    return;
                }

                isSkipButtonVisible = value;
                OnPropertyChanged("IsSkipButtonVisible");
            }
        }

        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                if (selectedIndex == value)
                {
                    return;
                }
                if (selectedIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public ICommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Change Language button is clicked.
        /// </summary>
        public ICommand ChangeLangCommand { get; set; }

        #endregion

        #region Methods

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next()
        {
            var itemCount = Boardings.Count();
            if (ValidateAndUpdateSelectedIndex(itemCount))
            {
                MoveToNextPage();
            }
        }


        /// <summary>
        /// Invoked when the Change Language button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ChangeLanguage(object obj)
        {
            // _dialogService.ShowMessage("Language", "Language changed");

            //this.GetFlowDirectionToApply();

            if (App.IsArabic)
            {
                App.IsArabic = false;
                App.changeFontFamily(App.appObj);
            }
            else
            {
                App.IsArabic = true;
                App.changeFontFamily(App.appObj);
            }
        }

        public void test()
        {
            Boardings = new ObservableCollection<Boarding>
                 {
                new Boarding()
                {
                    ImagePath = AppResources.NDCommitmentsImage,
                    Header = AppResources.NDCommitments,
                    Content = AppResources.NDDontMissObligation,
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding()
                {
                    ImagePath =AppResources.NDSubmisitionsImage,//Correspondence 
                    Header = AppResources.NDReturnSubmission,
                    Content = AppResources.NDNewEasyWayToCommunicate,
                    RotatorItem = new WalkthroughItemPage()
                },
                new Boarding()
                {
                    ImagePath = AppResources.NDInboxImage, // VAT RETURN
                    Header = AppResources.NDInboxnNotification,
                    Content = AppResources.NDNewEasyWayToCommunicate,
                    RotatorItem = new WalkthroughItemPage()
                },
               };

            // Set bindingcontext to content view.5
            foreach (var boarding in Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        private void MoveToNextPage()
        {
            _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);

            //Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
