using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Text;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundAccountPopupPageView : PopupPage
    {
        public RefundAccountPopupPageViewModel viewModel;
        public VATDeclaration ClonedvATDeclaration;
        public IBanListResponseModel IBanListResponse;
        public RefundAccountPopupPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.RefundAccountPopupPageView;
                this.BindingContext = viewModel;
                if (vATDeclaration != null && vATDeclaration.data != null)
                {

                    if (vATDeclaration.data.Cr1645GoliveFg != "")
                    {
                        if (vATDeclaration.data.PendingIbanMsg != "")
                        {
                            MopupService.Instance.PushAsync(new EstimatedZAKATReturnsPages.AttachmentInformationPopUp(vATDeclaration.data.PendingIbanMsg));
                        }
                        vATDeclaration.data.IBANSet.Clear();
                    }

                    viewModel.SelectedIBAN = null;

                    viewModel.IsSwichButtonEnable = true;
                    viewModel.IsCarriedForwandReviewMessageForRefund = false;
                    viewModel.IsRefundYesMsgDisplayed = false;
                    viewModel.SelectedIBANIDNumber = null;

                    viewModel.VATDeclarationDetails = vATDeclaration;

                    if (App.ICRStatus != "E0001" && App.ICRStatus != "E0013")
                    {
                        if (viewModel.VATDeclarationDetails.data.EstimatedFg == "A")
                        {
                            ManageEnabledProperties(true);
                        }
                        else
                        {
                            ManageEnabledProperties(false);
                            OtherIbanlist.SelectionGesture = TouchGesture.LongPress;
                        }
                    }
                    else
                    {
                        ManageEnabledProperties(true);
                    }
                    if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg == "X")
                    {

                        viewModel.IsIdTypeEnabled = false;
                        viewModel.IsIdNumberEnabled = false;
                    }
                    onPageLoad();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void ManageEnabledProperties(bool value)
        {
            viewModel.IsIdTypeEnabled = value;
            viewModel.IsIdNumberEnabled = value;
            viewModel.IsNewAccountEnabled = value;
            viewModel.IsIbansEnabled = value;
            viewModel.IsRefundCheckboxEnabled = value;
            viewModel.IsConfirmRefundButtonEnabled = value;
        }

        protected override void OnAppearing()
        {
            getIban();
            getYesRefundMsgCommand();
            getNoRefundMsgCommand();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceivedVATDeclaration");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceivedForRefundMsg");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceivedForRefundMsg");
        }

        public void getIban()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "IbanReceivedVATDeclaration", async (sender, arg) =>
                {
                    await MopupService.Instance.PopAsync();
                    string message = string.Empty;
                    if (arg != null)
                    {
                        message = arg;
                        if (message == "SA")
                        {
                            if (viewModel.IBANList != null)
                            {
                                viewModel.IBANList.Clear();
                            }
                            viewModel.IBANList = null;
                            viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.data.IBANSet);
                            viewModel.NewAccountText = AppResources.ZTERNewAccount;
                        }
                        else
                        {
                            triggerIban(message);
                        }
                    }
                });
            }
            catch (Exception)
            {
            }
        }

        public void triggerIban(string messagestring)
        {
            try
            {
                string message = messagestring;
                if (!string.IsNullOrEmpty(message))
                {
                    bool isExist = false;
                    if (viewModel.IBANList == null)
                    {
                        viewModel.IBANList = new ObservableCollection<Result2>();
                    }
                    if (!string.IsNullOrEmpty(message))
                    {
                        List<Result2> results1D = new List<Result2>();
                        foreach (var item in viewModel.IBANList)
                        {
                            Result2 result = new Result2();
                            result = item;

                            if (string.IsNullOrEmpty(item.Bkvid))
                            {
                                isExist = true;
                                result.Iban = message;
                                viewModel.VATDeclarationDetails.data.Iban = message;
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                            }
                            results1D.Add(result);
                        }

                        if (results1D != null && results1D.Count != 0)
                        {
                            viewModel.IBANList = new ObservableCollection<Result2>(results1D);
                            viewModel.SelectedIBAN = viewModel.IBANList.FirstOrDefault();

                        }


                        if (!isExist)
                        {
                            viewModel.VATDeclarationDetails.data.Iban = message;
                            Result2 result2 = new Result2();
                            result2.Iban = message;
                            List<Result2> results = new List<Result2>();
                            results.Add(result2);
                            viewModel.IBANList = new ObservableCollection<Result2>(results);
                            viewModel.SelectedIBAN = viewModel.IBANList.FirstOrDefault();
                            viewModel.NewAccountText = AppResources.VATREditAccount;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;

        }

        public bool IsCheckedDraftMode()
        {
            bool value = false;
            if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                value = true;
            }
            return value;
        }

        public async Task GetAllIbanList()
        {
            try
            {
                try
                {
                    Result2 IbanListsResults;
                    IBanListResponse = await WebServiceManager.GetIBanDataForCR1645();
                    if (IBanListResponse != null && IBanListResponse.D != null && IBanListResponse.D.Results != null && IBanListResponse.D.Results.Count > 0)
                    {
                        viewModel.CR1645IBanListModel = IBanListResponse.D.Results;


                        for (int i = 0; i < IBanListResponse.D.Results.Count; i++)
                        {
                            IbanListsResults = new Result2()
                            {
                                Partner = string.Empty,
                                Bkvid = string.Empty,
                                Iban = IBanListResponse.D.Results[i].Iban
                            };
                            viewModel.VATDeclarationDetails.data.IBANSet.Add(IbanListsResults);
                        }
                        if (viewModel.VATDeclarationDetails.data.IBANSet.Count > 0)
                        {
                            viewModel.IBANList = new ObservableCollection<Result2>();
                            viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.data.IBANSet);

                            viewModel.SelectedIBAN = viewModel.VATDeclarationDetails.data.IBANSet.FirstOrDefault();

                           await getAllChecks();
                        }



                        for (int i = 0; i < IBanListResponse.D.Results.Count; i++)
                        {
                            if (IBanListResponse.D.Results[i].IdType == "ZS0005")
                            {
                                viewModel.IBANTypesList.Add(new IBANType
                                {
                                    key = "ZS0005",
                                    Text = AppResources.ZIBANCompanyID
                                });
                            }
                            else if (IBanListResponse.D.Results[i].IdType == "ZS0001")
                            {
                                viewModel.IBANTypesList.Add(new IBANType
                                {
                                    key = "ZS0001",
                                    Text = AppResources.ZIBANNationalID
                                });
                            }
                            else if (IBanListResponse.D.Results[i].IdType == "ZS0003")
                            {
                                viewModel.IBANTypesList.Add(new IBANType
                                {
                                    key = "ZS0003",
                                    Text = AppResources.TinDeregistrationGCCID
                                });
                            }
                            else if (IBanListResponse.D.Results[i].IdType == "BUP002")
                            {
                                viewModel.IBANTypesList.Add(new IBANType
                                {
                                    key = "BUP002",
                                    Text = AppResources.ZIBANCommercialRegistrationID
                                });
                            }

                        }
                        for (int i = 0; i < IBanListResponse.D.Results.Count; i++)
                        {

                            viewModel.IBANIDNumberList.Add(new IBANIDNumber
                            {
                                Idnumber = IBanListResponse.D.Results[i].IdNumber,
                                Type = ""
                            });
                        }
                    }

                    else
                    {
                        viewModel.IsIBANValid = false;

                    }
                }
                catch (InternetException ex)
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                }
            }
            catch (Exception)
            {
            }
        }
        public async Task onPageLoad()
        {
            if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg.Equals("X"))
            {
              await  GetAllIbanList();
            }
            else
            {
              await  getAllChecks();
            }
        }
        public async Task getAllChecks()
        {
            try
            {
                viewModel.createIBANType();
                if (App.ICRStatus == "E0001")
                {
                    if (viewModel.VATDeclarationDetails.data.IBANSet != null && viewModel.VATDeclarationDetails.data.IBANSet.Count() != 0)
                    {
                        viewModel.IBANList = new ObservableCollection<Result2>();
                        viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.data.IBANSet);
                        viewModel.IsVATRefunCheckedVisible = false;
                        viewModel.IsEnableCheckedRefund = false;
                        viewModel.IsNewAccountButtonVisible = false;
                        viewModel.SelectedIBAN = viewModel.IBANList.FirstOrDefault();
                        viewModel.IsDeclarationCheckedForRefund = false;
                    }
                    else
                    {
                        viewModel.IBANList = new ObservableCollection<Result2>();
                        viewModel.IsVATRefunCheckedVisible = true;
                        viewModel.IsEnableCheckedRefund = true;
                        viewModel.IsNewAccountButtonVisible = true;
                        viewModel.NewAccountText = AppResources.ZTERNewAccount;
                        viewModel.IsDeclarationCheckedForRefund = false;
                    }
                }
                bool value = IsCheckedDraftMode();
                if (value || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                {
                    if (viewModel.VATDeclarationDetails.data.TcFlg == "1")
                    {
                        viewModel.IsDeclarationCheckedForRefund = true;
                    }
                    else
                    {
                        viewModel.IsDeclarationCheckedForRefund = false;
                    }

                    if (viewModel.VATDeclarationDetails.data.RefundFg == "1")
                    {
                        viewModel.IsSwichButtonEnable = true;

                        if (viewModel.VATDeclarationDetails.data.IbanCb == "1")// IbanCb is equal to 1 if there is no data in IBan List as per Vinay
                        {
                            viewModel.IsNewAccountButtonVisible = true;

                            if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg == "X")
                            {
                                viewModel.IBANList = new ObservableCollection<Result2>();
                                viewModel.IbanNumberText = viewModel.VATDeclarationDetails.data.IBANSet[0].Iban;
                                viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.data.IBANSet);
                                if (viewModel.VATDeclarationDetails.data.IBANSet != null && viewModel.VATDeclarationDetails.data.IBANSet.Count() != 0)
                                {
                                    viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.data.IBANSet[0].Iban).FirstOrDefault();
                                }
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                                //}
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.Iban))
                                {
                                    viewModel.IbanNumberText = viewModel.VATDeclarationDetails.data.Iban;
                                    viewModel.IBANList = new ObservableCollection<Result2>();
                                    Result2 result = new Result2();
                                    result.Iban = viewModel.VATDeclarationDetails.data.Iban;
                                    viewModel.IBANList.Add(result);
                                    viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.data.Iban).FirstOrDefault();
                                    viewModel.NewAccountText = AppResources.VATREditAccount;
                                }
                                else
                                {
                                    viewModel.IBANList = new ObservableCollection<Result2>();
                                }
                            }
                        }
                        else
                        {

                            viewModel.IsTextBoxVisibleForIban = false;
                            viewModel.IsDropdownVisibleForIban = true;
                            viewModel.IsNewAccountButtonVisible = false;
                            if (viewModel.VATDeclarationDetails.data.IBANSet != null && viewModel.VATDeclarationDetails.data.IBANSet.Count() != 0)
                            {
                                viewModel.IBANList = new ObservableCollection<Result2>();
                                viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.data.IBANSet);
                            }
                            if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg == "X")
                            {
                                if (viewModel.VATDeclarationDetails.data.IBANSet != null && viewModel.VATDeclarationDetails.data.IBANSet.Count() != 0)
                                {
                                    viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.data.IBANSet[0].Iban).FirstOrDefault();
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.Iban))
                                {
                                    viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.data.Iban).FirstOrDefault();
                                }
                            }
                            if (viewModel.IBANList != null && viewModel.IBANList.Count > 0)
                            {
                                viewModel.IsVATRefunCheckedVisible = false;
                            }
                            else
                            {
                                viewModel.IsVATRefunCheckedVisible = true;
                            }
                        }

                        if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg == "X")
                        {
                            viewModel.SelectedIBANType = viewModel.IBANTypesList[0];
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.Idtype))
                            {
                                viewModel.SelectedIBANType = viewModel.IBANTypesList.Where(x => x.key == viewModel.VATDeclarationDetails.data.Idtype).FirstOrDefault();
                                if (viewModel.SelectedIBANType != null)
                                {
                                    await viewModel.SetIBANIdNumber();
                                }
                            }
                        }

                        if (viewModel.VATDeclarationDetails.data.Cr1645GoliveFg == "X")
                        {
                            if (viewModel.IBANIDNumberList != null && viewModel.IBANIDNumberList.Count != 0)
                            {
                                viewModel.SelectedIBANIDNumber = viewModel.IBANIDNumberList[0];
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.Idnum))
                            {
                                if (viewModel.IBANIDNumberList != null && viewModel.IBANIDNumberList.Count != 0)
                                {
                                    viewModel.SelectedIBANIDNumber = viewModel.IBANIDNumberList.Where(x => x.Idnumber == viewModel.VATDeclarationDetails.data.Idnum).FirstOrDefault();
                                }
                            }
                        }


                    }
                    else
                    {
                        viewModel.IsSwichButtonEnable = false;
                        viewModel.IsDropdownVisibleForIban = false;
                        viewModel.IsVisibleDropdownForRefund = false;
                        viewModel.IsVisiblechkRefundDeclaration = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void ManageValidations()
        {
            //Est flag = "A"--Enable
            if (viewModel.VATDeclarationDetails.data.IBANSet != null && viewModel.VATDeclarationDetails.data.IBANSet.Count != 0)
            {
                viewModel.IsNewAccountButtonVisible = false;
            }
            else
            {
                viewModel.IsNewAccountButtonVisible = true;
            }
        }

        public bool CheckValidationsForSubmitButton()
        {
            bool result = false;
            if (viewModel.IsNewAccountButtonVisible)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && viewModel.SelectedIBAN != null && viewModel.IsDeclarationCheckedForRefund == true)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && viewModel.SelectedIBAN != null && viewModel.IsDeclarationCheckedForRefund == true)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public bool CheckValidationsForSubmitButtonWithMsg()
        {
            bool result = false;
            if (viewModel.IsNewAccountButtonVisible)
            {
                if (viewModel.SelectedIBAN == null)
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZPleaseSelectTheIbanMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else if (viewModel.IsDeclarationCheckedForRefund == false)
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else
                {
                    result = true;
                }
            }
            else
            {
                if (viewModel.SelectedIBAN == null)
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZPleaseSelectTheIbanMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                else if (viewModel.IsDeclarationCheckedForRefund == false)
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private async void IDTypeDropdown_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {

                //TODO
                var picker = (SfPicker)sender;
                if (picker == null) return;

                IBANType selectedIBANType = viewModel.IBANTypesList[picker.Columns[0].SelectedIndex];
                viewModel.SelectedIBANType = selectedIBANType;
                viewModel.SelectedIBANTypePrev = selectedIBANType;
                viewModel.TxtSelectedIBANType = selectedIBANType.Text;
                await viewModel.SetIBANIdNumber();
            }
            catch (Exception)
            {


            }
        }
        private void IDNumberDropdown_OkButtonClicked(object sender, EventArgs e)
        {
            var picker = (SfPicker)sender;
            if (picker == null) return;

            IBANIDNumber selectedIBANIDNumber = viewModel.IBANIDNumberList[picker.Columns[0].SelectedIndex];
            if (selectedIBANIDNumber != null)
            {
                viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
                viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
            }
        }
        private void IDNumberDropdown_CancelButtonClicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.SelectedIBANIDNumber = viewModel.SelectedIBANIDNumberPrev;
                if (viewModel.SelectedIBANIDNumberPrev == null)
                {
                    viewModel.TxtSelectedIBANIDNumber = string.Empty;
                }
            }
            catch (Exception)
            {

            }
        }
        private void IDTypeDropdown_CancelButtonClicked(object sender, EventArgs e)
        {
            viewModel.SelectedIBANType = viewModel.SelectedIBANTypePrev;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new NewAccountPopPage(viewModel.VATDeclarationDetails.data.Iban));
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            IDTypeDropdown.IsOpen = true;
        }

        private void SfButton_Clicked1(object sender, EventArgs e)
        {
            IDNumberDropdown.IsOpen = true;
        }

        private void IDNumber_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                IBANIDNumber selectedIBANIDNumber = viewModel.IBANIDNumberList[e.NewValue];
                if (selectedIBANIDNumber != null)
                {
                    viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                    viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
                    viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
                }
            }
            catch (Exception)
            {

            }
        }

        private async void IDType_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                IBANType selectedIBANType = viewModel.IBANTypesList[e.NewValue];
                if (selectedIBANType != null)
                {
                    viewModel.SelectedIBANType = selectedIBANType;
                    viewModel.SelectedIBANTypePrev = selectedIBANType;
                    viewModel.TxtSelectedIBANType = selectedIBANType.Text;
                    await viewModel.SetIBANIdNumber();
                }
            }
            catch (Exception)
            {

            }
        }

        public void getYesRefundMsgCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceivedForRefundMsg", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        await MopupService.Instance.PopAsync();
                       await saveRefund();
                    }
                });
            }
            catch (Exception)
            {
            }
        }
        public void getNoRefundMsgCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceivedForRefundMsg", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        await MopupService.Instance.PopAsync();
                        viewModel.IsRefundYesMsgDisplayed = false;
                    }

                });
            }
            catch (Exception)
            {
            }
        }

        public async Task saveRefund()
        {
            try
            {
                if (CheckValidationsForSubmitButtonWithMsg())
                {

                    if (viewModel.IsCarriedForwandReviewMessageForRefund == false)
                    {
                        viewModel.IsCarriedForwandReviewMessageForRefund = true;
                        decimal FourteenA = 0;
                        if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.TotaldueVat) && !string.IsNullOrEmpty(viewModel.VATDeclarationDetails.data.Preperiodcorr))
                        {
                            FourteenA = Convert.ToDecimal(viewModel.VATDeclarationDetails.data.TotaldueVat) + Convert.ToDecimal(viewModel.VATDeclarationDetails.data.Preperiodcorr);
                        }
                        if ((viewModel.IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(viewModel.VATDeclarationDetails.data.NetdueVat) < 0) || (viewModel.IsSwichButtonEnable == true && FourteenA < 0))
                        {


                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                            headerAmountInfo.IsLinkAvailable = false;

                            StringBuilder Masseges = new StringBuilder();
                            Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(AppResources.CreditReturnMsg);
                            //  PopUp Pop = new PopUp();
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.IsRed = "#e84941";
                            headerAmountInfo.IsBold = "Bold";
                            headerAmountInfo.Message = Masseges.ToString();

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                        else
                        {
                            viewModel.IsLoading = true;
                            this.CloseWhenBackgroundIsClicked = false;
                            var isSuccess = await viewModel.SubmitClicked();
                            viewModel.IsLoading = false;
                            this.CloseWhenBackgroundIsClicked = true;
                            if (isSuccess)
                            {
                                await MopupService.Instance.PopAsync();
                            }
                        }

                    }
                    else
                    {
                        viewModel.IsLoading = true;
                        this.CloseWhenBackgroundIsClicked = false;

                        await viewModel.SubmitClicked();

                        viewModel.IsLoading = false;
                        this.CloseWhenBackgroundIsClicked = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private async void Confirm_RefundClicked(object sender, EventArgs e)
        {

            if (CheckValidationsForSubmitButtonWithMsg())
            {
                if (viewModel.IsRefundYesMsgDisplayed == false)
                {
                    viewModel.IsRefundYesMsgDisplayed = true;
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZConfirmationMsg;
                    headerAmountInfo.IsLinkAvailable = false;
                    if (viewModel.VATDeclarationDetails != null && viewModel.VATDeclarationDetails.data != null && viewModel.VATDeclarationDetails.data.GoliveFg == "X")
                    {
                        headerAmountInfo.Message = AppResources.ZZZRefundYesMsgForFiteenPercent;
                    }
                    else
                    {
                        headerAmountInfo.Message = AppResources.ZZZRefundYesMsg;
                    }

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZConfirmationMsg;

                   await MopupService.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

                }
                else
                {
                   await saveRefund();
                }
            }
        }

        private async void OnRefundInCTapped(object sender, EventArgs e)
        {
            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

            headerAmountInfo.IsLinkAvailable = false;
            headerAmountInfo.Message = AppResources.ZZIacknowledgethattheabovebankaccount;

            headerWithInfos.Add(headerAmountInfo);

            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
            newDesignPopUp.HeaderWithInfos = headerWithInfos;
            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

            await MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
        }

        private async void IBANAccManagementTapped(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
           await viewModel._navigationService.NavigateTo(App.GAZTBankAccountManagementPageView, true);

        }

    }
}