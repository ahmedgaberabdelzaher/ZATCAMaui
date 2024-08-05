using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{

    public class NewZakatObjectionPageViewModel : BaseViewModel
    {
        int currentStep = 0;

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
                OnPropertyChanged("IsSummaryVisible");
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
                OnPropertyChanged("IsDeclarationVisible");
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
                OnPropertyChanged("IsAttachmentsVisible");
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
                OnPropertyChanged("IsObjectionReasonVisible");
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
                OnPropertyChanged("IsObjectionDetailVisible");
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
                OnPropertyChanged("HeaderTitle");
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
                OnPropertyChanged("subHeaderTitle");
            }
        }
        #endregion

        #region Constructor
        public NewZakatObjectionPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


            HeaderTitle = "Objection Details";
            subHeaderTitle = "Complete below details";

            OnContinueClicked = new Command(() =>
            {
                switch (currentStep)
                {
                    case 0:
                        IsObjectionDetailVisible = false;
                        IsObjectionReasonVisible = true;
                        HeaderTitle = "Objection Reason";
                        break;
                    case 1:
                        IsObjectionReasonVisible = false;
                        IsAttachmentsVisible = true;
                        HeaderTitle = "Attachments";
                        break;
                    case 2:
                        IsAttachmentsVisible = false;
                        IsDeclarationVisible = true;
                        HeaderTitle = "Declaration";
                        break;
                    case 3:
                        IsDeclarationVisible = false;
                        IsSummaryVisible = true;
                        HeaderTitle = "Summery";
                        subHeaderTitle = "Review the Below Information";
                        break;
                }
                if (currentStep < 3)
                    currentStep++;
            });
            OnBackStepClicked = new Command(() =>
            {
                switch (currentStep)
                {
                    case 3:
                        IsSummaryVisible = false;
                        IsDeclarationVisible = true;
                        HeaderTitle = "Declaration";
                        subHeaderTitle = "Complete below details";
                        break;

                    case 2:
                        IsDeclarationVisible = false;
                        IsAttachmentsVisible = true;
                        HeaderTitle = "Attachments";
                        break;
                    case 1:
                        IsAttachmentsVisible = false;
                        IsObjectionReasonVisible = true;
                        HeaderTitle = "Objection Reason";
                        break;
                    case 0:
                        IsObjectionReasonVisible = false;
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
