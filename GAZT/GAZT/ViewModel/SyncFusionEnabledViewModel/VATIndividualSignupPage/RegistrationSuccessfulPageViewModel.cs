using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    public class RegistrationSuccessfulPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private bool isGulf;
        public bool IsGulf
        {
            get
            {
                return isGulf;
            }
            set
            {
                isGulf = value;



                RaisePropertyChanged("IsGulf");
            }
        }
        private bool isCitizen;
        public bool IsCitizen
        {
            get
            {
                return isCitizen;
            }
            set
            {
                isCitizen = value;



                RaisePropertyChanged("IsCitizen");
            }
        }

        private string _tINnumber;
        public string TINnumber
        {
            get
            {
                return _tINnumber; 
            }
            set
            {
                _tINnumber = value;
              
                RaisePropertyChanged("TINnumber");
            }
        }
        public RegistrationSuccessfulPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
      

    }
}