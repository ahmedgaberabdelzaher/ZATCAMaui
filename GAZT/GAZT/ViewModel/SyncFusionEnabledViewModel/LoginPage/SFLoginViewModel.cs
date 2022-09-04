using EGAZT.ViewModel.NewDesignViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;
namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLogin_ViewModel
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SFLoginViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public SFLoginViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
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
                return this.isInvalidEmail;
            }
            set
            {
                if (this.isInvalidEmail == value)
                {
                    return;
                }
                this.isInvalidEmail = value;
                this.RaisePropertyChanged("IsInvalidEmail");
            }
        }
        #endregion
    }
}
