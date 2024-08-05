
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    public class SFLoginViewModel : BaseViewModel
    {
        public SFLoginViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        #region Fields
        private bool isInvalidEmail;
        #endregion
        #region Property
        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the email ID from user in the login page.
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether the entered email is valid or invalid.
        /// </summary>
        public bool IsInvalidEmail
        {
            get
            {
                return isInvalidEmail;
            }
            set
            {
                if (isInvalidEmail == value)
                {
                    return;
                }
                isInvalidEmail = value;
                this.OnPropertyChanged("IsInvalidEmail");
            }
        }
        #endregion
    }
}
