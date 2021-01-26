using System;
using System.Globalization;
using System.Windows.Input;
using EGAZT.Models.EstablishmentRegistration;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    [Preserve(AllMembers = true)]
    public class RegistrationSuccessfulViewModel: BaseViewModel
    {
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private string _fbnumx = string.Empty;
        public string Fbnumx
        {
            get => _fbnumx;
            private set
            {
                if (_fbnumx == value) return;
                if(value != null)
                {
                    _fbnumx = value;
                    RaisePropertyChanged(nameof(Fbnumx));
                }
            }
        }
        private string _dateText = string.Empty;
        public string DateText
        {
            get => _dateText;
            private set
            {
                if (_dateText == value) return;

                if (value != null)
                {
                    _dateText = value;
                    RaisePropertyChanged(nameof(DateText));
                }
            }
        }
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            private set
            {
                if (_name == value) return;

                if (value != null)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }
        #region commands
        public ICommand GoToDashBoardButtonClick { get; private set; }
        #endregion
        public RegistrationSuccessfulViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToDashBoardButtonClick = new Command(() => BackToDashboard());
        }

        public void BackToDashboard()
        {

        }

        public void OnAppearing()
        {
            Fbnumx = taxPayerDetails?.Fbnumx;
            Name = string.Format("{0} {1}", taxPayerDetails?.NameFirst, taxPayerDetails?.NameLast);
            DateText = DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
        }
    }
}
