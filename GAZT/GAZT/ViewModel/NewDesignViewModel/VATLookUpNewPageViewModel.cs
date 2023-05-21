using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ZXing.Net.Mobile.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class VATLookUpNewPageViewModel : BaseViewModel
    {

        bool _IsVatLookUp = true;
        public bool IsVatLookUp { get { return _IsVatLookUp; } set { _IsVatLookUp = value; RaisePropertyChanged(); } }

        bool _IsShowRsltView;
        public bool IsShowRsltView { get { return _IsShowRsltView; } set { _IsShowRsltView = value; RaisePropertyChanged(); } }

        bool _IsShowScanView;
        public bool IsShowScanView { get { return _IsShowScanView; } set { _IsShowScanView = value; RaisePropertyChanged(); } }

        bool _IsMainView = true;
        public bool IsMainView { get { return _IsMainView; } set { _IsMainView = value; RaisePropertyChanged(); } }


        string vatNumber;
        public string VatNumber { get { return vatNumber; } set { vatNumber = value; RaisePropertyChanged(); } }

        string tIN;
        public string TIN { get { return tIN; } set { tIN = value; RaisePropertyChanged(); } }

        string vATCertificateNumber;
        public string VATCertificateNumber { get { return vATCertificateNumber; } set { vATCertificateNumber = value; RaisePropertyChanged(); } }

        string region;
        public string Region { get { return region; } set { region = value; RaisePropertyChanged(); } }


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
                RaisePropertyChanged("ParameterTypeList");
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
                RaisePropertyChanged("SelectedParameterType");
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
                RaisePropertyChanged("IsTooltipEnableVisible");
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
                RaisePropertyChanged("Parameter");
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
                RaisePropertyChanged("Name");
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
                RaisePropertyChanged("NameOrNoResultLabel");
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
                RaisePropertyChanged("VATACCOrCRNOOrVATCER");
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
                RaisePropertyChanged("LookupNumber");
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
                RaisePropertyChanged("MaxDigids");
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
                RaisePropertyChanged("TxtSearchParameter");
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
                RaisePropertyChanged("IsNameVisible");
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
                RaisePropertyChanged("LookUpButtonText");
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
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                if (IsShowScanView || IsShowRsltView)
                {
                    IsShowScanView = IsShowRsltView = false;
                    return;
                }
                ResetFormData();
                _navigationService.GoBack();
            });
            OnSearchButtonClicked = new Xamarin.Forms.Command(async () =>
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
            OnScanButtonClicked = new Xamarin.Forms.Command(() =>
            {
                //try
                //{
                //    SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                //}
                //catch
                //{ 
                //}
                //IsNameVisible = false;
                //ZXingScannerPage scanPage = new ZXingScannerPage();
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    await Application.Current.MainPage.Navigation.PushAsync(scanPage);
                //});

                //string id = string.Empty;
                //string _language = "A";
                //scanPage.OnScanResult += (result) =>
                //{
                //    Device.BeginInvokeOnMainThread(async () =>
                //    {
                //        await Application.Current.MainPage.Navigation.PopAsync();
                //        LookupNumber = result.Text;
                //        id = result.Text;
                //        SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                //      //  TxtSearchParameter = SelectedParameterType.ParameterType.First();
                //        getBarcodeData();

                //    });
                //};
            });

            /*  MessagingCenter.Subscribe<VATLookUpNewPageViewModel, string>(this, "ScanData", (sender, arg) =>
              {
                  SelectedParameterType = ParameterTypeList?.Where(x => x.id == "3")?.FirstOrDefault();
                 // LookupNumber = arg;
                  getBarcodeData();
              });
            */

        }

        #region Methods
        public void OnPageLoad()
        {
            try
            {
                //EnteredCaptchaValue = string.Empty;
                //LookupNumber = string.Empty;
                //Name = string.Empty;
                VATACCOrCRNOOrVATCER = string.Empty;
                List<VATParameterType> VATParameterList = new List<VATParameterType>
            {//ZZZTaxRegistrationNumber
               //new VATParameterType{ id = "3" , ParameterType = AppResources.ZVATLookupIDTaxpayerTinType1},
               new VATParameterType{ id = "3" , ParameterType = AppResources.ZZZTaxRegistrationNumber},
                new VATParameterType{ id = "2" , ParameterType = AppResources.ZVATLookupCRNumberType3},
                new VATParameterType{ id = "4" , ParameterType = AppResources.ZVATLookupIDVatCertificateNumberType2}
            };

                ParameterTypeList = new List<VATParameterType>();
                ParameterTypeList = VATParameterList;
                SelectedParameterType = ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
        public void SetSelectedParameterTypeData()
        {
            //EnteredCaptchaValue = string.Empty;
            //LookupNumber = string.Empty;
            //Name = string.Empty;
            //StringBuilder captcha = GetCaptcha();
            //Captcha = captcha.ToString();
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
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    // _dialogService.ShowMessageBox(AppResources.ZVATNumberisnotequalto15, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseentertheVATAccountNocomposedof15digits));
                                });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("2"))
                        {
                            if (LookupNumber.Length != 10)
                            {
                                isMandatoryDataEntered = false;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    //   _dialogService.ShowMessageBox(AppResources.ZCRNumberisnotequalto10, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseentertheCRcomposedof10digits));
                                });
                                return;
                            }
                        }
                        else if (SelectedParameterType.id.Equals("4"))
                        {
                            if (LookupNumber.Length != 15)
                            {
                                isMandatoryDataEntered = false;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                    //  _dialogService.ShowMessageBox(AppResources.ZVATCerNumberisnotequalto15, AppResources.Information);
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATCertificateNumberValidation));
                                });
                                return;
                            }
                        }
                    }
                    else
                    {
                        isMandatoryDataEntered = false;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
                            isMandatoryDataEntered = false;
                            //  _dialogService.ShowMessageBox(AppResources.ZPleaseenterthecorrespondingnumber, AppResources.Information);
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp($"{AppResources.PleaseEnter} {SelectedParameterType.ParameterType}"));
                        });
                        return;
                    }
                }
                else
                {
                    isMandatoryDataEntered = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        // _dialogService.ShowMessageBox(AppResources.ZPleaseselectparametertype, AppResources.Information);
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZPleaseselectparametertype));
                    });
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                IsLoading = false;
                isMandatoryDataEntered = false;
            }
            //_dialogService.ShowMessageBox(AppResources.ZVATLookupDialogue, AppResources.Information);
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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.InValidCode));
                        return;
                    }
                    LookupNumber = LookUpNo;
                }

                //isMandatoryDataEntered = true;
                string _language = "A"; //UtilityManager.GetLanguageParameter();

                GAZT.Models.VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, SelectedParameterType.id, LookupNumber);
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
                        //else
                        //{
                        //    NameOrNoResultLabel = "";
                        //    Name = "";
                        //    IsNameVisible = false;
                        //    LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
                        //    Device.BeginInvokeOnMainThread(async () =>
                        //    {

                        //        if (string.Compare(vatLookUp.d.results[0].Description, "Vat number is not equal to 15", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            //  await _dialogService.ShowMessageBox(AppResources.ZZZVatnumberisnotequalto15, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZVatnumberisnotequalto15));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid VAT number provided", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            // await _dialogService.ShowMessageBox(AppResources.ZZZInvalidVATnumberprovided, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZInvalidVATnumberprovided));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "Tin is not Active", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            // await _dialogService.ShowMessageBox(AppResources.ZZZTinisnotActive, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZTinisnotActive));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid TIN", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            // await _dialogService.ShowMessageBox(AppResources.ZInvalidTinNumber, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZInvalidTinNumber));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "No Data found against given parameters", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            // await _dialogService.ShowMessageBox(AppResources.ZZZNoDatafoundagainstgivenparameters, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZNoDatafoundagainstgivenparameters));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "No VAT Certificate Found", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            //await _dialogService.ShowMessageBox(AppResources.ZZZNoVATCertificateFound, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZNoVATCertificateFound));
                        //        }
                        //        else if (string.Compare(vatLookUp.d.results[0].Description, "Account is deregistered", true) == 0)
                        //        {
                        //            IsLoading = false;
                        //            // await _dialogService.ShowMessageBox(AppResources.ZZZAccountisderegistered, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZAccountisderegistered));
                        //        }

                        //        else
                        //        {
                        //            IsLoading = false;
                        //            //await _dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
                        //            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(vatLookUp.d.results[0].Description));
                        //        }

                        //    });
                        //}
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (HttpRequestException)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    // await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    //  viewModel._navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    //  await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    //  viewModel._navigationService.GoBack();
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
            // MessagingCenter.Unsubscribe<VATLookUpNewPageViewModel, string>(this, "ScanData");
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
