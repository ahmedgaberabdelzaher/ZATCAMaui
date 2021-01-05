using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    public class NewZakatObjectionPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        int currentStep=0;

        public ICommand OnContinueClicked { get; set; }
        public ICommand OnBackStepClicked { get; set; }

        #region Property

        private bool _IsSummaryVisible = false;
        public bool IsSummaryVisible
        {
            get
            {
                return _IsSummaryVisible;
            }
            set
            {
                if (_IsSummaryVisible == value) return;
                _IsSummaryVisible = value;
                RaisePropertyChanged("IsSummaryVisible");
            }
        }

        private bool _IsDeclarationVisible = false;
        public bool IsDeclarationVisible
        {
            get
            {
                return _IsDeclarationVisible;
            }
            set
            {
                if (_IsDeclarationVisible == value) return;

                _IsDeclarationVisible = value;
                RaisePropertyChanged("IsDeclarationVisible");
            }
        }

        private bool _IsAttachmentsVisible = false;
        public bool IsAttachmentsVisible
        {
            get
            {
                return _IsAttachmentsVisible;
            }
            set
            {
                if (_IsAttachmentsVisible == value) return;

                _IsAttachmentsVisible = value;
                RaisePropertyChanged("IsAttachmentsVisible");
            }
        }

        private bool _IsObjectionReasonVisible = false;
        public bool IsObjectionReasonVisible
        {
            get
            {
                return _IsObjectionReasonVisible;
            }
            set
            {
                if (_IsObjectionReasonVisible == value) return;

                _IsObjectionReasonVisible = value;
                RaisePropertyChanged("IsObjectionReasonVisible");
            }
        }

        private bool _IsObjectionDetailVisible = true;
        public bool IsObjectionDetailVisible
        {
            get
            {
                return _IsObjectionDetailVisible;
            }
            set
            {
                if (_IsObjectionDetailVisible == value) return;

                _IsObjectionDetailVisible = value;
                RaisePropertyChanged("IsObjectionDetailVisible");
            }
        }

        private string _HeaderTitle;
        public string HeaderTitle
        {
            get
            {
                return _HeaderTitle;
            }
            set
            {
                if (_HeaderTitle == value) return;

                _HeaderTitle = value;
                RaisePropertyChanged("HeaderTitle");
            }
        }

        private string _subHeaderTitle;
        public string subHeaderTitle
        {
            get
            {
                return _subHeaderTitle;
            }
            set
            {
                if (_subHeaderTitle == value) return;

                _subHeaderTitle = value;
                RaisePropertyChanged("subHeaderTitle");
            }
        }
        #endregion

        #region Constructor
        public NewZakatObjectionPageViewModel(INavigationService navigationService, IDialogService dialogService)
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


            HeaderTitle = "Objection Details";
            subHeaderTitle = "Complete below details";
            
            OnContinueClicked = new Xamarin.Forms.Command(() =>
            {
                switch (currentStep)
                {
                    case 0:IsObjectionDetailVisible = false;
                        IsObjectionReasonVisible = true;
                        HeaderTitle = "Objection Reason";
                        break;
                    case 1:IsObjectionReasonVisible = false;
                        IsAttachmentsVisible = true;
                        HeaderTitle = "Attachments";
                        break;
                    case 2: IsAttachmentsVisible = false;
                        IsDeclarationVisible = true;
                        HeaderTitle = "Declaration";
                        break;
                    case 3:IsDeclarationVisible = false;
                        IsSummaryVisible=true;
                        HeaderTitle = "Summery";
                        subHeaderTitle = "Review the Below Information";
                        break;
                }
                if(currentStep<3)
                    currentStep++;
            });
            OnBackStepClicked = new Xamarin.Forms.Command(() =>
            {
                switch (currentStep)
                {
                    case 3:IsSummaryVisible = false;
                        IsDeclarationVisible = true;
                        HeaderTitle = "Declaration";
                        subHeaderTitle = "Complete below details";
                        break;

                    case 2: IsDeclarationVisible = false;
                        IsAttachmentsVisible = true;
                        HeaderTitle = "Attachments";
                        break;
                    case 1: IsAttachmentsVisible = false;
                        IsObjectionReasonVisible = true;
                        HeaderTitle = "Objection Reason"; 
                        break;
                    case 0:IsObjectionReasonVisible = false;
                        IsObjectionDetailVisible = true;
                        HeaderTitle = "Objection Details"; 
                        break;
                }
                if (currentStep > 0)
                    currentStep--;
            });
            }
        #endregion

            #region Method
            #endregion
        }
}
