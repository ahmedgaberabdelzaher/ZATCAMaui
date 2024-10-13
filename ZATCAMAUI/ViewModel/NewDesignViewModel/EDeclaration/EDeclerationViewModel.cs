using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclerationViewModel : BaseEDeclarationViewModel
    {
        /// <summary>
        /// 1 for New Decleration
        /// 2 for Prevous Requests
        /// </summary>
        bool showReviewEntries;
        public bool ShowReviewEntries { get { return showReviewEntries; } set { showReviewEntries = value; OnPropertyChanged(); } }

        /// <summary>
        /// 1 for Visitor
        /// 2 for Citizen
        /// </summary>
        int identityType;
        public int IdentityType { get { return identityType; } set { identityType = value; OnPropertyChanged(); } }

        string _ReferenceNumber;
        public string ReferenceNumber { get { return _ReferenceNumber; } set { _ReferenceNumber = value; OnPropertyChanged(); } }

        string _IDResidencePassportNumber;
        public string IDResidencePassportNumber { get { return _IDResidencePassportNumber; } set { _IDResidencePassportNumber = value; OnPropertyChanged(); } }


        public ICommand ReviewPreviousCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    ShowReviewEntries = !ShowReviewEntries;
                });
            }
        }

        public ICommand SelcectIdentityTypeCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    IdentityType = int.Parse(e);
                    if (IdentityType == 1)
                    {
                        _navigationService.NavigateTo("NewDeclarationPage");
                    }
                    else
                    {
                        _navigationService.NavigateTo("NativeNafathPage", "NewDeclarationPage");
                    }
                    ShowReviewEntries = false;
                });
            }
        }

        public ICommand GoToReviewPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if(string.IsNullOrWhiteSpace(ReferenceNumber)
                        || string.IsNullOrWhiteSpace(IDResidencePassportNumber))
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequiredData;
                            return;
                        }
                        IsLoading = true;
                        var result = await DeclerationServices?.GetInquireDecleration(ReferenceNumber, IDResidencePassportNumber);

                        if (result?.Item1?.header?.status.code == "I000000")
                        {
                            var inquireDecleration = result?.Item1?.data?.travelerDeclaration;
                            if (inquireDecleration != null)
                            {
                                App.Locator.StateManager.SetItem("inquireDeclaration", inquireDecleration);
                              await  _navigationService.NavigateTo("ReviewRequestPage");

                            }
                            IsLoading = false;
                        }
                        else if (!string.IsNullOrWhiteSpace(result?.Item3))
                        {
                            IsShowMsgView = true;
                            MessageTxt = result?.Item3;
                        }
                        else
                        {
                            if (result.Item1?.header.moreInformation != null && result.Item1.header.moreInformation.Errordetails != null && result.Item1?.header.moreInformation.Errordetails.Count > 0)
                            {
                                MessageTxt = result.Item1?.header.moreInformation.Errordetails[0];
                                IsShowMsgView = true;

                                return;
                            }
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
                            IsLoading = false;

                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    BackMethod();

                });
            }
        }

        private void ResetData()
        {
            ReferenceNumber = string.Empty;
            IDResidencePassportNumber = string.Empty;
            ShowReviewEntries = false;
        }

        public void BackMethod()
        {
            ResetData();
            _navigationService.GoBack();
        }
        public EDeclerationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices,INativeNafath nativeNafath) : base(navigationService, dialogService,declerationServices,nativeNafath)

        {
        }


    }


}

