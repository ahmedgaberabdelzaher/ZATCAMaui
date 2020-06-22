using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class IndividualRegistrationPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

#region Properties
        public Color _BoxColorOne;
        public Color BoxColorOne
        {
            get { return _BoxColorOne; }
            set
            {
                _BoxColorOne = value;
                RaisePropertyChanged("BoxColorOne");
            }
        }
        public Color _BoxColorTwo;
        public Color BoxColorTwo
        {
            get { return _BoxColorTwo; }
            set
            {
                _BoxColorTwo = value;
                RaisePropertyChanged("BoxColorTwo");
            }
        }
        public Color _BoxColorThree;
        public Color BoxColorThree
        {
            get { return _BoxColorThree; }
            set
            {
                _BoxColorThree = value;
                RaisePropertyChanged("BoxColorThree");
            }
        }

        public Color _BoxColorFour;
        public Color BoxColorFour
        {
            get { return _BoxColorFour; }
            set
            {
                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFour");
            }
        }

        public Color _BoxColorFive;
        public Color BoxColorFive
        {
            get { return _BoxColorFive; }
            set
            {
                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFive");
            }
        }
        #endregion

        public IndividualRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
    }
}
