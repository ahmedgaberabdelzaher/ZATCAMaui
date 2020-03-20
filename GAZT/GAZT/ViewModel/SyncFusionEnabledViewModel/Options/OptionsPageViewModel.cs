using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZTeServicesApp.Views.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZTeServicesApp.ViewModels.Options
{
    /// <summary>
    /// ViewModel for Setting page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class OptionsPageViewModel : ViewModelBase
    {

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPageViewModel" /> class
        /// </summary>
        public OptionsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.ChangePasswordCommand = new Command(this.ChangePasswordClicked);
            this.LinkAccountCommand = new Command(this.LinkAccountClicked);
            this.HelpCommand = new Command(this.HelpClicked);
            this.TermsCommand = new Command(this.TermsServiceClicked);
            this.PolicyCommand = new Command(this.PrivacyPolicyClicked);
            this.FAQCommand = new Command(this.FAQClicked);
            this.CloseButtonClicked = new Command(this.CloseClicked);



            this.MyProfileCommand = new Command(this.MyProfileCommandClicked);
            this.EditMobileNumberCommand = new Command(this.EditMobileNumberClicked);
            this.EditPasswordCommand = new Command(this.EditPasswordClicked);
            this.EditEmailCommand = new Command(this.EditEmailClicked);


        }

        #endregion

        #region Commands

        public Command MyProfileCommand { get; set; }
        public Command EditMobileNumberCommand { get; set; }
        public Command EditPasswordCommand { get; set; }
        public Command EditEmailCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the favourite button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the edit profile option is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command ChangePasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the account link option is clicked.
        /// </summary>
        public Command LinkAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the help option is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the terms of service option is clicked.
        /// </summary>
        public Command TermsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the privacy policy option is clicked.
        /// </summary>
        public Command PolicyCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the FAQ option is clicked.
        /// </summary>
        public Command FAQCommand { get; set; }

        public Command CloseButtonClicked { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the back button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void BackButtonClicked(object obj)
        {
            _navigationService.GoBack();
            // Do something
        }

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditProfileClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void ChangePasswordClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void TermsServiceClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void PrivacyPolicyClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        /// 

        private void FAQClicked(object obj)
        {
            // Do something
        }

        private void CloseClicked(object obj)
        {
            _navigationService.GoBack();
            // Do something
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            // Do something
        }


        private void MyProfileCommandClicked(object obj)
        {
            // Do something
        }

        private void EditMobileNumberClicked(object obj)
        {
            // Do something
        }

        private void EditPasswordClicked(object obj)
        {
            // Do something
        }

        private void EditEmailClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}
