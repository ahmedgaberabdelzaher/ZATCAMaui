using System.Windows.Input;
using EGAZT.Views.NewDesign.ContractRelease;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ContractReleaseViewModel
{
    public class ContractReleaseViewModel : BaseViewModel
    {
        #region Enums

        enum PagesEnum
        {
            CrReleaseDetailsView,
            CrAttachmentsView,
            CrRemarksAndDescriptionView,
            CrDeclarationView,
            CrSummaryView,
        }

        #endregion

        #region Commands

        public ICommand ReleaseDetailsConBtnTapped { get; set; }
        public ICommand AttachmentsConBtnTapped { get; set; }
        public ICommand RemarksAndDescConBtnTapped { get; set; }
        public ICommand DeclarationConBtnTapped { get; set; }
        public ICommand SummaryConBtnTapped { get; set; }
        public ICommand ContractInstructionsClicked { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand GoBackToReleaseDetails { get; set; }
        public ICommand GoBackToAttachments { get; set; }
        public ICommand GoBackToDeclaration { get; set; }

        public ICommand ContractProfitPercentCommand { get; set; }
        public ICommand ProfitEstimatedContractCommand { get; set; }
        public ICommand EstimatedProfitZakatCommand { get; set; }
        public ICommand EstimatedProfitTaxCommand { get; set; }
        public ICommand ValueofZakatDuesCommand { get; set; }
        public ICommand ValueofTaxDuesCommand { get; set; }
        public ICommand TotalDuesCommand { get; set; }

        #endregion

        private bool _isBackButtonVisible = true;

        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                _isBackButtonVisible = value;
                RaisePropertyChanged("IsBackButtonVisible");
            }
        }

        private bool _isReleaseDetailsVisible = false;

        public bool IsReleaseDetailsVisible
        {
            get { return _isReleaseDetailsVisible; }
            set
            {
                _isReleaseDetailsVisible = value;
                RaisePropertyChanged("IsReleaseDetailsVisible");
            }
        }

        private bool _attachmentsVisible = false;

        public bool AttachmentsVisible
        {
            get { return _attachmentsVisible; }
            set
            {
                _attachmentsVisible = value;
                RaisePropertyChanged("AttachmentsVisible");
            }
        }

        private bool _remarksAndDescVisible = false;

        public bool RemarksAndDescVisible
        {
            get { return _remarksAndDescVisible; }
            set
            {
                _remarksAndDescVisible = value;
                RaisePropertyChanged("RemarksAndDescVisible");
            }
        }

        private bool _declarationVisible = false;

        public bool DeclarationVisible
        {
            get { return _declarationVisible; }
            set
            {
                _declarationVisible = value;
                RaisePropertyChanged("DeclarationVisible");
            }
        }

        private bool _summaryVisible = false;

        public bool SummaryVisible
        {
            get { return _summaryVisible; }
            set
            {
                _summaryVisible = value;
                RaisePropertyChanged("SummaryVisible");
            }
        }

        private string _infoTitle = "";

        public string InfoTitle
        {
            get { return _infoTitle; }
            set
            {
                _infoTitle = value;
                RaisePropertyChanged("InfoTitle");
            }
        }

        private string _infoDesc = "";

        public string InfoDesc
        {
            get { return _infoDesc; }
            set
            {
                _infoDesc = value;
                RaisePropertyChanged("InfoDesc");
            }
        }

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        int selectedPage = (int) PagesEnum.CrReleaseDetailsView;

        public ContractReleaseViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
        {
            _navigationService = navigationService;

            _dialogService = dialogService;

            CloseClick = new Command(async () => { EnableReleaseDetailsView(); });

            GoBackClick = new Command(async () => { BackNavigations(); });

            GoBackToReleaseDetails = new Command(async () => { EnableReleaseDetailsView(); });

            GoBackToAttachments = new Command(async () => { EnableAttachmentsView(); });

            GoBackToDeclaration = new Command(async () => { EnableDeclarationView(); });

            ContractProfitPercentCommand = new Command(async () =>
            {
                InfoTitle = AppResources.CRContractprofitEstimatedRate;
                InfoDesc = AppResources.CRContractprofitEstimatedRateDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ProfitEstimatedContractCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CRProfitEstimatedForContract;
                InfoDesc = AppResources.CRProfitEstimatedForContractDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            EstimatedProfitZakatCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CREstimatedProfitforZakat;
                InfoDesc = AppResources.CREstimatedProfitforZakatDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            EstimatedProfitTaxCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CREstimatedProfitforTax;
                InfoDesc = AppResources.CREstimatedProfitforTaxDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ValueofZakatDuesCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CRTheValueofZakatdues;
                InfoDesc = AppResources.CRTheValueofZakatduesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            ValueofTaxDuesCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CRTheValueTaxDues;
                InfoDesc = AppResources.CRTheValueTaxDuesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });
            TotalDuesCommand =  new Command(async () =>
            {
                InfoTitle = AppResources.CRTotalDues;
                InfoDesc = AppResources.CRTotalDuesDesc;
                await PopupNavigation.Instance.PushAsync(new ContractReleaseInfoPopup(this));
            });


            ReleaseDetailsConBtnTapped = new Command(ReleaseDetailsConBtnClicked);
            AttachmentsConBtnTapped = new Command(AttachmentsConBtnClicked);
            RemarksAndDescConBtnTapped = new Command(RemarksAndDescConBtnClicked);
            DeclarationConBtnTapped = new Command(DeclarationConBtnClicked);
            SummaryConBtnTapped = new Command(SummaryConBtnClicked);
            ContractInstructionsClicked = new Command(InstructionsTapped);

            //EnableReleaseDetailsView();
            PopupNavigation.Instance.PushAsync(new ContractReleaseInstructionsPopUp(this));
        }

        public async void InstructionsTapped()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                EnableReleaseDetailsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void ReleaseDetailsConBtnClicked()
        {
            try
            {
                EnableAttachmentsView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void AttachmentsConBtnClicked()
        {
            try
            {
                EnableRemarksAndDescView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void RemarksAndDescConBtnClicked()
        {
            try
            {
                EnableDeclarationView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void DeclarationConBtnClicked()
        {
            try
            {
                EnableSummaryView();
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void SummaryConBtnClicked()
        {
            try
            {
                _navigationService.NavigateTo(App.ContractReleaseSuccessPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        private void EnableReleaseDetailsView()
        {
            IsBackButtonVisible = false;
            IsReleaseDetailsVisible = true;
            AttachmentsVisible = false;
            RemarksAndDescVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.CrReleaseDetailsView;
        }

        private void EnableAttachmentsView()
        {
            IsBackButtonVisible = true;
            IsReleaseDetailsVisible = false;
            AttachmentsVisible = true;
            RemarksAndDescVisible = false;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.CrAttachmentsView;
        }

        private void EnableRemarksAndDescView()
        {
            IsBackButtonVisible = true;
            IsReleaseDetailsVisible = false;
            AttachmentsVisible = false;
            RemarksAndDescVisible = true;
            DeclarationVisible = false;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.CrRemarksAndDescriptionView;
        }

        private void EnableDeclarationView()
        {
            IsBackButtonVisible = true;
            IsReleaseDetailsVisible = false;
            AttachmentsVisible = false;
            RemarksAndDescVisible = false;
            DeclarationVisible = true;
            SummaryVisible = false;
            selectedPage = (int) PagesEnum.CrDeclarationView;
        }

        private void EnableSummaryView()
        {
            IsBackButtonVisible = true;
            IsReleaseDetailsVisible = false;
            AttachmentsVisible = false;
            RemarksAndDescVisible = false;
            DeclarationVisible = false;
            SummaryVisible = true;
            selectedPage = (int) PagesEnum.CrSummaryView;
        }

        private void BackNavigations()
        {
            switch (selectedPage)
            {
                case (int) PagesEnum.CrAttachmentsView:
                    EnableReleaseDetailsView();
                    break;
                case (int) PagesEnum.CrRemarksAndDescriptionView:
                    EnableAttachmentsView();
                    break;

                case (int) PagesEnum.CrDeclarationView:
                    EnableRemarksAndDescView();
                    break;
                case (int) PagesEnum.CrSummaryView:
                    EnableDeclarationView();
                    break;
            }
        }
    }
}