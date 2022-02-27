using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundAccountPopupPageView : PopupPage
    {
        public RefundAccountPopupPageViewModel viewModel;
        public RefundAccountPopupPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.RefundAccountPopupPageView;
                this.BindingContext = viewModel;
                SetLTR();
                SetPickerFont();
                if (vATDeclaration!=null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationDetails = vATDeclaration;
                    onPageLoad();
                    
                    viewModel.IsSwichButtonEnable = true;
                    viewModel.IsCarriedForwandReviewMessageForRefund = false;
                    viewModel.IsRefundYesMsgDisplayed = false;
                    viewModel.SelectedIBANIDNumber = null;


                    if (App.ICRStatus!="E0001" && App.ICRStatus != "E0013")
                    {
                        if (viewModel.VATDeclarationDetails.d.EstimatedFg == "A")
                        {
                            ManageEnabledProperties(true);
                            //OtherIbanlist.SelectionMode = Syncfusion.ListView.XForms.SelectionMode.Single;
                        }
                        else
                        {
                            ManageEnabledProperties(false);
                           // OtherIbanlist.SelectionMode = Syncfusion.ListView.XForms.SelectionMode.None;
                            OtherIbanlist.SelectionGesture = TouchGesture.Hold;
                        }
                    }
                    else
                    {
                        ManageEnabledProperties(true);
                       // OtherIbanlist.SelectionMode = Syncfusion.ListView.XForms.SelectionMode.Single;
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            IDTypeDropdown.HeaderFontFamily = "Somar-SemiBold";
                            IDTypeDropdown.ColumnHeaderFontFamily = "Somar-SemiBold";
                            IDTypeDropdown.SelectedItemFontFamily = "Somar-SemiBold";
                            IDTypeDropdown.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy


                            IDNumberDropdown.HeaderFontFamily = "Somar-SemiBold";
                            IDNumberDropdown.ColumnHeaderFontFamily = "Somar-SemiBold";
                            IDNumberDropdown.SelectedItemFontFamily = "Somar-SemiBold";
                            IDNumberDropdown.UnSelectedItemFontFamily = "Somar-SemiBold";//dd      

                 }
                        break;
                    case Xamarin.Forms.Device.Android:
                        IDTypeDropdown.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        IDTypeDropdown.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        IDTypeDropdown.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        IDTypeDropdown.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        IDNumberDropdown.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        IDNumberDropdown.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        IDNumberDropdown.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        IDNumberDropdown.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy  

                        break;
                }
            }
            catch (Exception ex)
            {

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
        public void ValidationsForVATRefund()
        {
          
           



              
        }
        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                IDTypeDropdown.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                IDNumberDropdown.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                IDTypeDropdown.BackgroundColor =  (Color)Application.Current.Resources["White"];
                IDNumberDropdown.BackgroundColor =  (Color)Application.Current.Resources["White"];
            }
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
                    await PopupNavigation.Instance.PopAsync();
                    string message = string.Empty;
                    if (arg != null)
                    {
                        message = arg;
                        // firebasemessage = JsonConvert.DeserializeObject<PushnotificationMessage>(arg);
                        if (message == "SA")
                        {
                            if (viewModel.IBANList != null)
                            {
                                viewModel.IBANList.Clear();
                            }
                            viewModel.IBANList = null;
                            viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.d.IBANSet.results);
                           // viewModel.VATDeclarationDetails.d.OptIban = String.Empty;
                            viewModel.NewAccountText = AppResources.ZTERNewAccount;
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                triggerIban(message);
                            });
                        }
                    }

                    // await viewModel.VATSetReturnVoidAsync();

                });
            }
            catch (Exception ex)
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
                    if(viewModel.IBANList==null)
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
                                viewModel.VATDeclarationDetails.d.Iban = message;
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                            }
                            results1D.Add(result);
                        }

                        if (results1D != null && results1D.Count != 0)
                        {
                            //if (viewModel.IBANList != null)
                            //{
                            //    viewModel.IBANList.Clear();
                            //}
                            //viewModel.IBANList = null;
                            
                                viewModel.IBANList = new ObservableCollection<Result2>(results1D);
                                viewModel.SelectedIBAN = viewModel.IBANList.FirstOrDefault();
                           
                        }


                        if (!isExist)
                        {
                            viewModel.VATDeclarationDetails.d.Iban = message;
                            Result2 result2 = new Result2();
                            result2.Iban = message;
                            List<Result2> results = new List<Result2>();
                            results.Add(result2);
                            viewModel.IBANList = new ObservableCollection<Result2>(results);
                            viewModel.SelectedIBAN = viewModel.IBANList.FirstOrDefault();
                            viewModel.NewAccountText = AppResources.VATREditAccount;
                        }
                    }
                    //                viewModel.IsNewAccountClicked = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

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
        public async void onPageLoad()
        {
            try
            {
                viewModel.createIBANType();
                if (App.ICRStatus == "E0001")
                {
                    if (viewModel.VATDeclarationDetails.d.IBANSet.results != null && viewModel.VATDeclarationDetails.d.IBANSet.results.Count() != 0)
                    {
                        viewModel.IBANList = new ObservableCollection<Result2>();
                        viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.d.IBANSet.results);
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
                    if (viewModel.VATDeclarationDetails.d.TcFlg == "1")
                    {
                        viewModel.IsDeclarationCheckedForRefund = true;
                    }
                    else
                    {
                        viewModel.IsDeclarationCheckedForRefund = false;
                    }

                    if (viewModel.VATDeclarationDetails.d.RefundFg == "1")
                    {
                        viewModel.IsSwichButtonEnable = true;

                        //IsVisibleDropdownForRefund = true;
                        //IsVisiblechkRefundDeclaration = true;
                        //  IsDropdownVisibleForIban = true;
                        if (viewModel.VATDeclarationDetails.d.IbanCb == "1")// IbanCb is equal to 1 if there is no data in IBan List as per Vinay
                        {
                            //IsTextBoxVisibleForIban = true;
                            //IsDropdownVisibleForIban = false;
                            //IsCheckedRefund = true;
                            viewModel.IsNewAccountButtonVisible = true;
                            if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.Iban))
                            {
                                viewModel.IbanNumberText = viewModel.VATDeclarationDetails.d.Iban;
                                viewModel.IBANList = new ObservableCollection<Result2>();
                                Result2 result = new Result2();
                                result.Iban = viewModel.VATDeclarationDetails.d.Iban;
                                viewModel.IBANList.Add(result);
                                viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.d.Iban).FirstOrDefault();
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                            }
                            else
                            {
                                viewModel.IBANList = new ObservableCollection<Result2>();
                            }
                        }
                        else
                        {

                            viewModel.IsTextBoxVisibleForIban = false;
                            viewModel.IsDropdownVisibleForIban = true;
                            viewModel.IsNewAccountButtonVisible = false;
                            if (viewModel.VATDeclarationDetails.d.IBANSet.results != null && viewModel.VATDeclarationDetails.d.IBANSet.results.Count() != 0)
                            {
                                viewModel.IBANList = new ObservableCollection<Result2>();
                                viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.d.IBANSet.results);
                            }
                            if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.Iban))
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    viewModel.SelectedIBAN = viewModel.IBANList.Where(x => x.Iban == viewModel.VATDeclarationDetails.d.Iban).FirstOrDefault();

                                });
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
                        if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.Idtype))
                        {
                            viewModel.SelectedIBANType = viewModel.IBANTypesList.Where(x => x.key == viewModel.VATDeclarationDetails.d.Idtype).FirstOrDefault();
                            if (viewModel.SelectedIBANType != null)
                            {
                                await viewModel.SetIBANIdNumber();
                            }
                        }
                        if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.Idnum))
                        {
                            if (viewModel.IBANIDNumberList != null && viewModel.IBANIDNumberList.Count != 0)
                            {
                                viewModel.SelectedIBANIDNumber = viewModel.IBANIDNumberList.Where(x => x.Idnumber == viewModel.VATDeclarationDetails.d.Idnum).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {
                        //viewModel.IsSwichButtonEnableToTap = true;
                        viewModel.IsSwichButtonEnable = false;
                        viewModel.IsDropdownVisibleForIban = false;
                        viewModel.IsVisibleDropdownForRefund = false;
                        viewModel.IsVisiblechkRefundDeclaration = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ManageValidations()
        {
            //Est flag = "A"--Enable
            if(viewModel.VATDeclarationDetails.d.IBANSet!=null && viewModel.VATDeclarationDetails.d.IBANSet.results.Count!= 0)
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
            if(viewModel.IsNewAccountButtonVisible)
            {
                if(!string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && viewModel.SelectedIBAN != null && viewModel.IsDeclarationCheckedForRefund == true)
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
                if (string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber))
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZNoIdNumberMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else if(string.IsNullOrEmpty(viewModel.TxtSelectedIBANType))
                {
                    result = false;
                }
                else if(viewModel.SelectedIBAN == null)
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

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else if(viewModel.IsDeclarationCheckedForRefund == false)
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

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                }
                else
                {
                    result = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber))
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZNoIdNumberMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                    //result = true;
                }
                else if(string.IsNullOrEmpty(viewModel.TxtSelectedIBANType))
                {
                    result = false;
                }
                else if(viewModel.SelectedIBAN == null)
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

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                else if(viewModel.IsDeclarationCheckedForRefund == false)
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

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private async void IDTypeDropdown_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANType selectedIBANType = (IBANType)e.NewValue;
            IDTypeDropdown.SelectedItem = selectedIBANType;
            viewModel.SelectedIBANType = selectedIBANType;
            viewModel.SelectedIBANTypePrev = selectedIBANType;
            viewModel.TxtSelectedIBANType = selectedIBANType.Text;
            await viewModel.SetIBANIdNumber();
            //if (viewModel.IsVisibleSummary == true)
            //{
            //    if (viewModel.IsVisibleDropdownForRefund == true)
            //    {
            //        ValidationsForVATRefund();
            //    }
            //}
        }
        private void IDNumberDropdown_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
            if (selectedIBANIDNumber != null)
            {
                IDNumberDropdown.SelectedItem = selectedIBANIDNumber;
                viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
                viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
            }
            //if (viewModel.IsVisibleSummary == true)
            //{
            //    if (viewModel.IsVisibleDropdownForRefund == true)
            //    {
            //        ValidationsForVATRefund();
            //    }
            //}
        }
        private void IDNumberDropdown_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                IDNumberDropdown.SelectedItem = viewModel.SelectedIBANIDNumberPrev;
                viewModel.SelectedIBANIDNumber = viewModel.SelectedIBANIDNumberPrev;
                if (viewModel.SelectedIBANIDNumberPrev == null)
                {
                    viewModel.TxtSelectedIBANIDNumber = string.Empty;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void IDTypeDropdown_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IDTypeDropdown.SelectedItem = viewModel.SelectedIBANTypePrev;
            viewModel.SelectedIBANType = viewModel.SelectedIBANTypePrev;
            //if (viewModel.SelectedIBANTypePrev == null)
            //{
            //    viewModel.TxtSelectedIBANType = string.Empty;
            //}
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopPage(viewModel.VATDeclarationDetails.d.Iban));
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            IDTypeDropdown.IsOpen = true;
        }

        private void SfButton_Clicked1(object sender, EventArgs e)
        {
            IDNumberDropdown.IsOpen = true;
        }

        private void IDNumber_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
                if (selectedIBANIDNumber != null)
                {
                    IDNumberDropdown.SelectedItem = selectedIBANIDNumber;
                    viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                    viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
                    viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void IDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                IBANType selectedIBANType = (IBANType)e.NewValue;
                if (selectedIBANType != null)
                {
                    IDTypeDropdown.SelectedItem = selectedIBANType;
                    viewModel.SelectedIBANType = selectedIBANType;
                    viewModel.SelectedIBANTypePrev = selectedIBANType;
                    viewModel.TxtSelectedIBANType = selectedIBANType.Text;
                    await viewModel.SetIBANIdNumber();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        await PopupNavigation.Instance.PopAsync();
                        saveRefund();
                    }
                });
            }
            catch (Exception ex)
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
                        await PopupNavigation.Instance.PopAsync();
                        viewModel.IsRefundYesMsgDisplayed = false;
                    }

                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void saveRefund()
        {
            if (CheckValidationsForSubmitButtonWithMsg())
            {

                if (viewModel.IsCarriedForwandReviewMessageForRefund == false)
                {
                    viewModel.IsCarriedForwandReviewMessageForRefund = true;
                    decimal FourteenA = 0;
                    if (!string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.TotaldueVat) && !string.IsNullOrEmpty(viewModel.VATDeclarationDetails.d.Preperiodcorr))
                    {
                        FourteenA = Convert.ToDecimal(viewModel.VATDeclarationDetails.d.TotaldueVat) + Convert.ToDecimal(viewModel.VATDeclarationDetails.d.Preperiodcorr);
                    }
                    if ((viewModel.IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(viewModel.VATDeclarationDetails.d.NetdueVat) < 0) || (viewModel.IsSwichButtonEnable == true && FourteenA < 0))
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

                        await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        //SelectedIndex = 2;
                        //PageSelectedItem = VatTabbledPageList[2];
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel.IsNewLoading = true;
                            this.CloseWhenBackgroundIsClicked = false;
                        });
                        await viewModel.SubmitClicked();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel.IsNewLoading = false;
                            this.CloseWhenBackgroundIsClicked = true;
                        });
                    }

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsNewLoading = true;
                        this.CloseWhenBackgroundIsClicked = false;
                    });
                    await viewModel.SubmitClicked();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsNewLoading = false;
                        this.CloseWhenBackgroundIsClicked = true;
                    });
                }
            }
        }

        private void Confirm_RefundClicked(object sender, EventArgs e)
        {
            
            if (CheckValidationsForSubmitButtonWithMsg())
            {
                if (viewModel.IsRefundYesMsgDisplayed==false)
                {
                    viewModel.IsRefundYesMsgDisplayed = true;
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZConfirmationMsg;
                    headerAmountInfo.IsLinkAvailable = false;
                    if (viewModel.VATDeclarationDetails != null && viewModel.VATDeclarationDetails.d != null && viewModel.VATDeclarationDetails.d.GoliveFg == "X")
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

                    PopupNavigation.Instance.PushAsync(new ShowVatInformationConfirmationPageView(newDesignPopUp));

                }
                else
                {
                    saveRefund();
                }
            }
        }

        private  void IbanChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
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



            await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
        }

        //private void IbanTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        //{
        //    try
        //    {
        //        if (sender != null)
        //        {
        //            if (viewModel.IsIbansEnabled)
        //            {
        //                Result2 selectediban = (Result2)sender;
        //                viewModel.SelectedIBAN = selectediban;
        //            }
        //            else
        //            {
        //               ((SfListView)sender).SelectedItem = null;
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
    }
}