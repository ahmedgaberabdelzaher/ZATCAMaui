using EGAZT.Views.NewDesign.OnboardingPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.OnBoardingAnimation
{
  
    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
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
        public GAZTNewDesignOnBoardingAnimationPageViewModel(INavigationService navigationService, IDialogService dialogService): base (navigationService, dialogService)
        {
            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);
            // this.ChangeLangCommand = new Command(this.ChangeLanguage);
           
                this.Boardings = new ObservableCollection<Boarding>
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
            foreach (var boarding in this.Boardings)
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
                return this.boardings;
            }

            set
            {
                if (this.boardings == value)
                {
                    return;
                }

                this.boardings = value;
                RaisePropertyChanged("Boardings");
            }
        }

        public string NextButtonText
        {
            get
            {
                return this.nextButtonText;
            }

            set
            {
                if (this.nextButtonText == value)
                {
                    return;
                }

                this.nextButtonText = value;
                RaisePropertyChanged("NextButtonText");
            }
        }

        public string LanguageText
        {
            get
            {
                return this._LanguageText;
            }

            set
            {
                if (this._LanguageText == value)
                {
                    return;
                }

                this._LanguageText = value;
                RaisePropertyChanged("LanguageText");
            }
        }

        public bool IsSkipButtonVisible
        {
            get
            {
                return this.isSkipButtonVisible;
            }

            set
            {
                if (this.isSkipButtonVisible == value)
                {
                    return;
                }

                this.isSkipButtonVisible = value;
                RaisePropertyChanged("IsSkipButtonVisible");
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (this.selectedIndex == value)
                {
                    return;
                }
                if (this.selectedIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
                this.selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
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
            if (this.SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            this.SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip()
        {
            this.MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next()
        {
            var itemCount = Boardings.Count();
            if (this.ValidateAndUpdateSelectedIndex(itemCount))
            {
                this.MoveToNextPage();
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
            this.Boardings = new ObservableCollection<Boarding>
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
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        private void MoveToNextPage()
        {
             _navigationService.NavigateTo("/App.SFLoginPageView", App.GAZTNewDesignDashBoardPageView);
           // _navigationService.NavigateTo("/LoginSelectionView");

            //Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
