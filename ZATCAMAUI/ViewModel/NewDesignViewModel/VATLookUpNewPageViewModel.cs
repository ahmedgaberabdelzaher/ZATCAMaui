using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class VATLookUpNewPageViewModel : BaseViewModel
    {

        bool _IsVatLookUp = true;
        public bool IsVatLookUp { get { return _IsVatLookUp; } set { _IsVatLookUp = value; OnPropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value; OnPropertyChanged(); } }

        bool _IsShowScanView;
        public bool IsShowScanView { get { return _IsShowScanView; } set { _IsShowScanView = value; OnPropertyChanged(); } }

        bool _IsMainView = true;
        public bool IsMainView { get { return _IsMainView; } set { _IsMainView = value; OnPropertyChanged(); } }


        string vatNumber;
        public string VatNumber { get { return vatNumber; } set { vatNumber = value; OnPropertyChanged(); } }

        string tIN;
        public string TIN { get { return tIN; } set { tIN = value; OnPropertyChanged(); } }

        string vATCertificateNumber;
        public string VATCertificateNumber { get { return vATCertificateNumber; } set { vATCertificateNumber = value; OnPropertyChanged(); } }

        string region;
        public string Region { get { return region; } set { region = value; OnPropertyChanged(); } }


        bool isMandatoryDataEntered = true;
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnSearchButtonClicked { get; set; }
        public ICommand OnScanButtonClicked { get; set; }

        #region proprety
        private List<VATParameterType> _parameterTypeList;
        public List<VATParameterType> ParameterTypeList
        {
            get
            {
                return _parameterTypeList;
            }
            set
            {
                _parameterTypeList = value;
                OnPropertyChanged("ParameterTypeList");
            }
        }
        private VATParameterType _selectedParameterType = null;
        public VATParameterType SelectedParameterType
        {
            get
            {
                return _selectedParameterType;
            }
            set
            {
                _selectedParameterType = value;

                if (_selectedParameterType != null)
                {
                    SetSelectedParameterTypeData();
                    TxtSearchParameter = _selectedParameterType.ParameterType;
                    SetPlaceholderText();
                }
                OnPropertyChanged("SelectedParameterType");
            }
        }
        private bool _isTooltipEnableVisible = false;
        public bool IsTooltipEnableVisible
        {
            get
            {
                return _isTooltipEnableVisible;
            }
            set
            {
                _isTooltipEnableVisible = value;
                OnPropertyChanged("IsTooltipEnableVisible");
            }
        }

        private string _parameter;
        public string Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
                OnPropertyChanged("Parameter");
            }
        }
        private string _name = "";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _nameOrNoResultLabel = "";
        public string NameOrNoResultLabel
        {
            get
            {
                return _nameOrNoResultLabel;
            }
            set
            {
                _nameOrNoResultLabel = value;
                OnPropertyChanged("NameOrNoResultLabel");
            }
        }

        private string _VATACCOrCRNOOrVATCER = "";// AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
        public string VATACCOrCRNOOrVATCER
        {
            get
            {
                return _VATACCOrCRNOOrVATCER;
            }
            set
            {
                _VATACCOrCRNOOrVATCER = value;
                OnPropertyChanged("VATACCOrCRNOOrVATCER");
            }
        }
        private string _lookupNumber = "";
        public string LookupNumber
        {
            get
            {
                return _lookupNumber;
            }
            set
            {
                _lookupNumber = value;
                OnPropertyChanged("LookupNumber");
            }
        }
        private string _maxDigids = "15";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                _maxDigids = value;
                OnPropertyChanged("MaxDigids");
            }
        }
        private string _txtSearchParameter = string.Empty;
        public string TxtSearchParameter
        {
            get
            {
                return _txtSearchParameter;
            }
            set
            {
                _txtSearchParameter = value;
                OnPropertyChanged("TxtSearchParameter");
            }
        }
        private bool _isNameVisible = false;
        public bool IsNameVisible
        {
            get
            {
                return _isNameVisible;
            }
            set
            {
                _isNameVisible = value;
                OnPropertyChanged("IsNameVisible");
            }
        }
        private string _lookUpButtonText = "";
        public string LookUpButtonText
        {
            get
            {
                return _lookUpButtonText;
            }
            set
            {
                _lookUpButtonText = value;
                OnPropertyChanged("LookUpButtonText");
            }
        }
        #endregion
        public VATLookUpNewPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            IsMainView = true;
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnBackButtonClicked = new Command(() =>
            {
                if (IsShowScanView || IsShowRsltView)
                {
                    IsShowScanView = IsShowRsltView = false;
                    return;
                }
                ResetFormData();
                _navigationService.GoBack();
            });
            OnSearchButtonClicked = new Command(async () =>
            {
                isMandatoryDataEntered = true;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        // ResetFormData();
                        Name = string.Empty;
                        IsNameVisible = false;
                    }
                    ValidateFormData();
                    if (isMandatoryDataEntered)
                    {
                        getBarcodeData();
                    }

                });


            });
            OnScanButtonClicked = new Command(() =>
            {
            });

        }

        #region Methods
        public void OnPageLoad()
        {
            try
            {
               
                VATACCOrCRNOOrVATCER = string.Empty;
                List<VATParameterType> VATParameterList = new List<VATParameterType>
            {
               new VATParameterType{ id = "3" , ParameterType = AppResources.ZZZTaxRegistrationNumber},
                new VATParameterType{ id = "2" , ParameterType = AppResources.ZVATLookupCRNumberType3},
                new VATParameterType{ id = "4" , ParameterType = AppResources.ZVATLookupIDVatCertificateNumberType2}
            };

                ParameterTypeList = new List<VATParameterType>();
                ParameterTypeList = VATParameterList;
                SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            }
            catch (Exception)
            {


            }
        }
        public void SetSelectedParameterTypeData()
        {

            if (SelectedParameterType != null)
            {
                TxtSearchParameter = SelectedParameterType.ParameterType;
                IsTooltipEnableVisible = true;
                SetPlaceholderText();
            }
        }
        private void SetPlaceholderText()
        {
            IsTooltipEnableVisible = true;
            if (SelectedParameterType.id.Equals("3"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheVATAccountNocomposedof15digits;
                MaxDigids = "15";
            }
            else if (SelectedParameterType.id.Equals("2"))
            {
                VATACCOrCRNOOrVATCER = AppResources.ZPleaseentertheCRcomposedof10digits;
                MaxDigids = "10";
            }
            else
            {
                VATACCOrCRNOOrVATCER = AppResources.PleaseentertheVATCertificateNocomposedof15digits;
                MaxDigids = "15";
            }
        }
        public void ValidateFormData()
        {
            try
            {
                IsLoading = true;
                if (SelectedParameterType != null)
                {
                    if (!string.IsNullOrWhiteSpace(LookupNumber))
                    {
                        if (SelectedParameterType.id.Equals("3"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                isMandatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseentertheVATAccountNocomposedof15digits));
                                });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("2"))
                        {
                            if (LookupNumber.Length != 10)
                            {
                                isMandatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseentertheCRcomposedof10digits));
                                });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("4"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                isMandatoryDataEntered = false;
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATCertificateNumberValidation));
                                });
                                return;
                            }
                        }
                    }
                    else
                    {
                        isMandatoryDataEntered = false;
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
                            isMandatoryDataEntered = false;
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp($"{AppResources.PleaseEnter} {SelectedParameterType.ParameterType}"));
                        });
                        return;
                    }
                }
                else
                {
                    isMandatoryDataEntered = false;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseselectparametertype));
                    });
                    return;
                }
            }
            catch (Exception)

            {


                IsLoading = false;
                isMandatoryDataEntered = false;
            }
        }
        public void ResetFormData()
        {
            Name = "";
            IsNameVisible = false;
            LookupNumber = "";
            LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
        }
        public async void getBarcodeData(string LookUpNo = "")
        {

            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(LookUpNo))
                {
                    if (LookUpNo.Length != 15)
                    {
                        IsLoading = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.InValidCode));
                        return;
                    }
                    LookupNumber = LookUpNo;
                }

                //isMandatoryDataEntered = true;
                string _language = "A"; //UtilityManager.GetLanguageParameter();

                VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, SelectedParameterType.id, LookupNumber);
                if (vatLookUp != null)
                {
                    if (vatLookUp.d != null)
                    {
                        if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description)) // Provided condiotion as per Vinay, Description comes null when the there is no error while calling the API
                        {
                            IsVatLookUp = true;
                            IsLoading = false;
                            IsShowScanView = false;
                            IsMainView = false;
                            IsShowRsltView = true;

                            // NameOrNoResultLabel = AppResources.Name;
                            Name = vatLookUp.d.results[0].Name;
                            TIN = vatLookUp.d.results[0].Tin;
                            Region = vatLookUp.d.results[0].Region;
                            VatNumber = vatLookUp.d.results[0].Idnumber;
                            VATCertificateNumber = vatLookUp.d.results[0].VatCertNo;

                            //IsNameVisible = true;
                            LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
                            return;
                        }
                        else
                        {
                            IsVatLookUp = false;
                            IsLoading = false;
                            IsShowScanView = false;
                            IsMainView = false;
                            IsShowRsltView = true;
                            Name = AppResources.UnregisteredFacility;
                            VatNumber = AppResources.Incorrect;
                            TIN = "--";
                            Region = "--";
                            VATCertificateNumber = "--";
                            LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
                            return;
                        }
                      
                    }
                    else
                    {
                        IsLoading = false;
                        Name = vatLookUp.d.results[0].Name;
                        NameOrNoResultLabel = AppResources.Nodataavailable;
                    }

                }
                IsMainView = true;
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (HttpRequestException)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (Exception)
            {


                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            finally
            {
                IsShowScanView = false;
            }



        }
        #endregion


        public void onDissapear()
        {
       
        }
        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (IsShowRsltView || IsShowScanView)
                    {
                        IsShowRsltView = IsShowScanView = false;
                        IsMainView = true;
                        return;
                    }
                    _navigationService.GoBack();

                });
            }
        }

    }
}
