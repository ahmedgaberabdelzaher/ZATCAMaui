using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualRegistrationPageView : ContentPage
    {
      
        IndividualRegistrationPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public IndividualRegistrationPageView()
        {
            InitializeComponent();
            
            viewModel = App.Locator.IndividualRegistrationPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.IsHijriCal = false;

            viewModel.ClearData();
             Task.Run(async() =>
            {
                viewModel.OnPageLoad();
            });
            
            viewModel.IndividualRegistrationView = true;
            SetLTR();

            viewModel.TxtCountryCode = "+966";
            if (Device.RuntimePlatform == Device.Android)
            {
                IntnlCodes.Margin = new Thickness(0);
            }
            else
            {
                IntnlCodes.Margin = new Thickness(10, -8, 10, -8);
            }
            viewModel.currentStep = 1;
            viewModel.NationalAddressView = false;
            viewModel.ContactInformationView = false;
            viewModel.SummeryView = false;
            viewModel.PasswordView = false;
            viewModel.ContinueButtonText = AppResources.ZZZZContinue;
            //viewModel.SetFormVisibility();
            try
            {
                viewModel.PopulateDataInChips();
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                viewModel.IsHijriCal = false;
                viewModel.DOBddyymm = string.Empty;
                viewModel.DOB = string.Empty;
                
            }
            catch(Exception e)
            {

            }
            SetPickerFont();

            _ = viewModel.GetCaptchAndGUID();
        }

        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                //Image_backArrow.Rotation = 0;
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                //Label_MobileInitialAr.IsVisible = false;
                //Label_MobileInitialEng.IsVisible = true;
                EntryMobileNumber.HorizontalTextAlignment = TextAlignment.Start;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                //Image_backArrow.Rotation = 180;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                EntryMobileNumber.HorizontalTextAlignment = TextAlignment.End;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                //Label_MobileInitialAr.IsVisible = true;
                //Label_MobileInitialEng.IsVisible = false;
            }
        }

    /*        private void OnIDTypeTapped(object sender, EventArgs e)
            {
                DDlIDType.IsOpen = true;
            }
            private void OnDOBTapped(object sender, EventArgs e)
            {
                DOB.IsOpen = true;
            }
            private void DDlIDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void IDTypePicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void IDTypePicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DOB_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
            {

            }

            private void DOB_Closed(object sender, EventArgs e)
            {

            }*/
        #region
        private void EntryTIN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.StopTimer = false;
            viewModel.TotalSec = -10;
            
        }
        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.IdNumber))
            {
                if (viewModel.SelectedIdType.ID == "ZS0015")
                {
                    if (viewModel.IdNumber.Length < viewModel.MaxLengthID)
                    {
                        viewModel.FrameIDError = true;
                        //FrmIDNumber.HasError = true;
                    }
                    else
                    {
                        viewModel.FrameIDError = false;

                        //FrmIDNumber.HasError = false;
                    }

                }
                if (viewModel.SelectedIdType.ID == "ZS0017")
                {
                    if (viewModel.IdNumber.Length < viewModel.MaxLengthID)
                    {
                        viewModel.FrameIDError = true;
                    }
                    else
                    {
                        viewModel.FrameIDError = false;
                    }

                }
                if (viewModel.SelectedIdType.ID == "ZS0018")
                {
                    if ((viewModel.IdNumber.Length < 7) || (viewModel.IdNumber.Length > 15))
                    {
                        viewModel.FrameIDError = true;
                    }
                    else
                    {
                        viewModel.FrameIDError = false;
                    }

                }
            }
            else
            {
                viewModel.FrameIDError = false;
            }
        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private async void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.IdNumber))
            {
                if (viewModel.SelectedIdType.ID == "ZS0015")
                {
                    if (viewModel.IdNumber.Substring(0, 1) != "1")
                    {
                        popUp.Message = AppResources.ZZNationalIDstartswith1;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdNumber = string.Empty;
                        //ZZPleaseenteravalidNationalID
                    }
                    else
                    {
                        if (EntryIDNumber.Text.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.AppendLine(AppResources.ZZNationalIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            EntryName.Text = string.Empty;
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.DOB))
                            {
                                ValidateIDNumber(); 
                            }
                                

                        }
                    }


                }
                if (viewModel.SelectedIdType.ID == "ZS0017")
                {
                    if (viewModel.IdNumber.Substring(0, 1) != "2")
                    {
                        popUp.Message = AppResources.ZZIqamaIDstartswith2;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        EntryName.Text = string.Empty;
                    }
                    else
                    {
                        if (viewModel.IdNumber.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.AppendLine(AppResources.ZZIqamaIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.IdNumber = string.Empty;
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.DOB))
                            {
                                ValidateIDNumber();
                            }
                        }
                    }
                    

                }
                if (viewModel.SelectedIdType.ID == "ZS0018")
                {
                    if (viewModel.IdNumber.Substring(0, 1) == "0")
                    {
                        //Have to change to neww error message
                        popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdNumber = string.Empty;
                    }
                    else if (!(viewModel.IdNumber.Length <= 15 && viewModel.IdNumber.Length >= 7))
                    {
                        popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdNumber = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }
                   

                }
            }
            else
            {
               // FrmIDNumber.HasError = false;
                viewModel.FrameIDError = false;
            }



        }
        private void DpDOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            
        }

        private void DpDOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.DOB = viewModel.DOBPrev;
                if (!string.IsNullOrEmpty(viewModel.DOBPrev))
                {
                    string[] Date = viewModel.DOBPrev.Split('/');
                    ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                    //Select today dates
                    todaycollection.Add(Date[2]);
                    todaycollection.Add(Date[1]);//day
                    todaycollection.Add(Date[0]);

                    SignUpDOB.SelectedItem = todaycollection;
                }
            }
            catch(Exception Ex)
            {

            }
           
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
                ValidateIDNumber();
        }


        public async void ValidateIDNumber()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.DOB.Replace("/", "");
            EntryName.IsEnabled = true;
            if (viewModel.SelectedIdType.ID == "ZS0015")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0015", viewModel.IdNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                     //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.Name = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0015", viewModel.IdNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
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
                                viewModel.IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            Console.Write(ex.StackTrace.ToString());
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdType.ID == "ZS0017")
            {
                EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0017", viewModel.IdNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                               // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.Name = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0017", viewModel.IdNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
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
                                viewModel.IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Write(ex.StackTrace.ToString());
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void EntryName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }
        private async void DOB_Closed(object sender, EventArgs e)
        {

        }
        //    private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        //{

        //}

 

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                SignUpDOBHijri.IsOpen = true;
            }
            else
            {
                SignUpDOB.IsOpen = true;
            }
            
            //HijriCal.IsVisible = true;
        }
        private  void CountryCodes_Clicked(object sender, EventArgs e)
        {


            PopupNavigation.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));


        }

        private void LIssuedBy_Clicked(object sender, EventArgs e)
        {

        }

        private void LIssuedByCity_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSubmitNext_Clicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void IDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
                viewModel.SelectedIdType = viewModel.IdTypeList[viewModel.IDTypeIndex];
                viewModel.IdNumber = string.Empty;
                if (!viewModel.SelectedIdType.ID.Equals("ZS0018"))
                {
                    if (string.IsNullOrEmpty(viewModel.DOB) && string.IsNullOrEmpty(viewModel.IdNumber))
                    {
                        ValidateIDNumber();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void DOB_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void IDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void DDlIDType_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
                viewModel.SelectedIdType = viewModel.IdTypeList[viewModel.IDTypeIndex];
                viewModel.IdNumber = string.Empty;
                if (!viewModel.SelectedIdType.ID.Equals("ZS0018"))
                {
                    if (string.IsNullOrEmpty(viewModel.DOB) && string.IsNullOrEmpty(viewModel.IdNumber))
                    {
                        ValidateIDNumber();
                    }
                    
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
                   }

        private void DOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void Country_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                viewModel.CountryName = viewModel.CountryList[viewModel.SelectedCountryIndex].Natio;
                viewModel.SelectedCountry = viewModel.CountryList[viewModel.SelectedCountryIndex];
            }
        }

        private void Country_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void GCCCountry_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.CountryName = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex].CountryName;
                viewModel.SelectedGCCCountry = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
                 }

        private void GCCCountry_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }
        
        private void Region_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
              viewModel.SelectedRegion= viewModel.RegionList[viewModel.SelectedRegionIndex];
              viewModel.Region=viewModel.RegionList[viewModel.SelectedRegionIndex].Bezei;
                viewModel.CityName = string.Empty;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
            // viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;//ZZPleaseenteravalidEmailAddress//ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;//ZZZInvalidEmailAddressMessage
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        // popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                    FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                    FrmEmailAddress.HasError = false;
                }
            }
        }
        public bool IsValid(string emailaddress)
        {
            bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Region_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void City_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

                viewModel.SelectedCity = viewModel.CityList[viewModel.SelectedCityIndex];
                viewModel.CityName = viewModel.CityList[viewModel.SelectedCityIndex].CityName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
            // viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
        }

        private void City_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void DOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    var selectedItem = SignUpDOBHijri.SelectedItem as ObservableCollection<object>;
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;
                }
                else
                {

                    var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;

                }




                ValidateIDNumber();
            }
            catch(Exception ex)
            {

            }
         
        }




        private void ddlLIssuedBy_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedBy_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedByCity_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void ddlLIssuedByCity_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void OnDateEntryFocussed(object sender, FocusEventArgs e)
        {

        }

       
        #endregion
        private async void btnContinue_Clicked(object sender, EventArgs e)
        {

        }
      

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;


            if (Device.RuntimePlatform == Device.Android)
            {
                DDlIDType.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                GCCPicker_Country.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                Picker_Region.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
                Picker_City.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                DDlIDType.BackgroundColor =  (Color)Application.Current.Resources["White"];
                GCCPicker_Country.BackgroundColor =  (Color)Application.Current.Resources["White"];
                Picker_Region.BackgroundColor =  (Color)Application.Current.Resources["White"];
                Picker_City.BackgroundColor =  (Color)Application.Current.Resources["White"];
            }

            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
            {
                IntnlCodes.Text = arg;
                viewModel.TxtCountryCode = arg;
            });
            MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
            {

                viewModel.MobileCountryCode = arg;
            });

            if (Device.RuntimePlatform == Device.Android)
            {
                IntnlCodes.Margin = new Thickness(0);
            }
            else
            {
                IntnlCodes.Margin = new Thickness(10, -8, 10, -8);
            }
            try
            {
                mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
            if (viewModel.currentStep == 5 && App.IsComingFromSleepMode)
            {
                
                int timeToExpireOTP = 120;
                viewModel.TimerStart(timeToExpireOTP);
            }

        }

        private void PickerBtn_Country_Clicked(object sender, EventArgs e)
        {
           // Picker_Country.IsOpen = true;
        }

        private void PickerBtn_Region_Clicked(object sender, EventArgs e)
        {
            Picker_Region.IsOpen = true;

        }

        private void PickerBtn_City_Clicked(object sender, EventArgs e)
        {
            Picker_City.IsOpen = true;
        }

        private void GCCPickerBtn_Country_Clicked(object sender, EventArgs e)
        {
            GCCPicker_Country.IsOpen = true;
        }

        private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        {

        }

        private void EntryEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;//ZZPleaseenteravalidEmailAddress//ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;//ZZZInvalidEmailAddressMessage
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        // popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                    viewModel.FrameEmailError = true;
                    //FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameEmailError = false;
                    //FrmEmailAddress.HasError = false;
                }
            }
        }

        private void EntryConfirmEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryConfirmEmail.Text))
            {
                bool flag = IsValid(EntryConfirmEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;//ZZPleaseenteravalidEmailAddress//ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;//ZZZInvalidEmailAddressMessage
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        // popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                    viewModel.FrameConfirmEmailError = true;
                    //FrmConfirmEmail.HasError = true;
                    EntryConfirmEmail.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameConfirmEmailError = false;
                   // FrmConfirmEmail.HasError = false;
                }
            }
        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {

            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {
                if (EntryMobileNumber.Text.Substring(0, 1) == "0")
                {
                    Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0);
                }
                if (EntryMobileNumber.Text.Length < 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    //FrmMobileNumber.HasError = true;
                    viewModel.FrameMobileNumberError = true;
                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                   
                    //FrmMobileNumber.HasError = false;
                    viewModel.FrameMobileNumberError = false;
                }
            }
            else
            {
                Message.AppendLine(AppResources.EnterMobileNumber);
                popUp.Message = Message.ToString();
                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
            }
        }

        private void EntryConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryConfirmPassword.Text))
            {
                if (viewModel.Password != viewModel.ConfirmPassword)
                {
                    viewModel.FrameConfirmPasswordError = true;
                    //frmCfrmPass.HasError = true;
                }
                else
                {
                    viewModel.FrameConfirmPasswordError = false;
                    //frmCfrmPass.HasError = false;
                }
            }
        }

        private void ImageSeePassword_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsPasswordEncripted)
            {
                viewModel.IsPasswordEncripted = false;
                ImageSeePassword.Source = "showPassword"; 
            }
            else
            {
                viewModel.IsPasswordEncripted = true;
                ImageSeePassword.Source = "hidePassword";
            }
        }

        private void ImageSeeConfirmPassword_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsConfirmPasswordEncripted)
            {
                viewModel.IsConfirmPasswordEncripted = false;
                ImageSeeConfirmPassword.Source = "showPassword";
            }
            else
            {
                viewModel.IsConfirmPasswordEncripted = true;
                ImageSeeConfirmPassword.Source = "hidePassword";
            }
            
        }

        private void ImageSeeOtp_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsOTPEncripted)
            {
                viewModel.IsOTPEncripted = false;
            }
            else
            {
                viewModel.IsOTPEncripted = true;
            }
        }

        private void GccCountryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.CountryName))
            {
                viewModel.FrameGccCountyError = false;
            }
        }

        private void Entry_Region_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.Region))
            {
                viewModel.FrameRegionError = false;
            }
        }

        private void Entry_City_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(viewModel.CityName))
            {
                viewModel.FrameCityError = false;
            }
        }

        private void DateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FrameDOBError = false;
        }

        private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FrameNameError = false;
        }

        private void EntryPostalCode_Unfocused(object sender, FocusEventArgs e)
        {
            StringBuilder Message = new StringBuilder();
            PopUp popUp = new PopUp();
            if (!string.IsNullOrEmpty(viewModel.PostalCode))
            {
                if (viewModel.PostalCode.Length != 5)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.AppendLine(AppResources.ZZZZInvalidPostalCode);
                  
                    if (Message.Length > 0)
                    {
                        popUp.Message = Message.ToString();
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                        //FrmMobileNumber.HasError = true;
                        viewModel.PostalCode = string.Empty;
                    }
                }
                 
         
            }
        }

        private void GCCPicker_Country_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.CountryName = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex].CountryName;
                viewModel.SelectedGCCCountry = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());

            }
        }

        private void SignUpDOB_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    
                        var selectedItem = SignUpDOBHijri.SelectedItem as ObservableCollection<object>;
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;

                }
                else
                {
                    var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;

                }


                ValidateIDNumber();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        //private void HijriCal_OnDateSelected(object sender, CalendarView.DateSelectionArgs e)
        //{
            //var date = HijriCal.DateSelected;
            //Console.WriteLine("Hi Your Hijri Date : " + date);
        //}

        private void HijriCalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (viewModel.IsHijriCal)
            {

                var selectedItem = SignUpDOBHijri.SelectedItem as ObservableCollection<object>;
                if (selectedItem != null)
                {
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;

                }
                else
                {
                    viewModel.DOB =string.Empty;
                    viewModel.DOBddyymm = string.Empty;

                }

            }
            else
            {
                var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                if (selectedItem != null)
                {
                    string month = selectedItem[1].ToString();
                    string day = selectedItem[0].ToString();
                    string year = selectedItem[2].ToString();
                    viewModel.DOB = year + "/" + month + "/" + day;
                    viewModel.DOBddyymm = day + "/" + month + "/" + year;
                    string DOB = year + month + day;
                    viewModel.DOBPrev = viewModel.DOB;

                }
                else
                {
                    viewModel.DOB = string.Empty;
                    viewModel.DOBddyymm = string.Empty;
                }

            }
        }

        private void EntryPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetPasswordValidationConditions();
            bool ValidPassword = UtilityManager.ValidateNewPassword(viewModel.Password);
            if (ValidPassword)
            {
                viewModel.MinEight = "check_oval";
                viewModel.CapsSmall = "check_oval";
                viewModel.MaxSixteen = "check_oval";
                viewModel.NumSymbol = "check_oval";

                // check the new and confirm password condition
            }
            else
            {
                if (UtilityManager.ValidMinEight) { viewModel.MinEight = "check_oval"; }
                if (UtilityManager.ValidSmallL && UtilityManager.ValidCapsL) { viewModel.CapsSmall = "check_oval"; }
                if (UtilityManager.ValidMaxSixteen) { viewModel.MaxSixteen = "check_oval"; }
                if (UtilityManager.ValidNumber && UtilityManager.ValidSymbol) { viewModel.NumSymbol = "check_oval"; }
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

                            DDlIDType.HeaderFontFamily = "Somar-SemiBold";
                            DDlIDType.ColumnHeaderFontFamily = "Somar-SemiBold";
                            DDlIDType.SelectedItemFontFamily = "Somar-SemiBold";
                            DDlIDType.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy


                            GCCPicker_Country.HeaderFontFamily = "Somar-SemiBold";
                            GCCPicker_Country.ColumnHeaderFontFamily = "Somar-SemiBold";
                            GCCPicker_Country.SelectedItemFontFamily = "Somar-SemiBold";
                            GCCPicker_Country.UnSelectedItemFontFamily = "Somar-SemiBold";//dd      


                            Picker_Region.HeaderFontFamily = "Somar-SemiBold";
                            Picker_Region.ColumnHeaderFontFamily = "Somar-SemiBold";
                            Picker_Region.SelectedItemFontFamily = "Somar-SemiBold";
                            Picker_Region.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy

                            Picker_City.HeaderFontFamily = "Somar-SemiBold";
                            Picker_City.ColumnHeaderFontFamily = "Somar-SemiBold";
                            Picker_City.SelectedItemFontFamily = "Somar-SemiBold";
                            Picker_City.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy  SignUpDOB
                            

                            SignUpDOB.HeaderFontFamily = "Somar-SemiBold";
                            SignUpDOB.ColumnHeaderFontFamily = "Somar-SemiBold";
                            SignUpDOB.SelectedItemFontFamily = "Somar-SemiBold";
                            SignUpDOB.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy 

                            SignUpDOBHijri.HeaderFontFamily = "Somar-SemiBold";
                            SignUpDOBHijri.ColumnHeaderFontFamily = "Somar-SemiBold";
                            SignUpDOBHijri.SelectedItemFontFamily = "Somar-SemiBold";
                            SignUpDOBHijri.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        DDlIDType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        DDlIDType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        DDlIDType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        DDlIDType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy 

                        GCCPicker_Country.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        GCCPicker_Country.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        GCCPicker_Country.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        GCCPicker_Country.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy  

                        Picker_Region.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Region.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Region.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_Region.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy

                        Picker_City.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_City.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_City.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        Picker_City.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy

                        SignUpDOB.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOB.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOB.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOB.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy

                        SignUpDOBHijri.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOBHijri.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOBHijri.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        SignUpDOBHijri.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

        }
        void ResetPasswordValidationConditions()
        {
            viewModel.MinEight = "error";
            viewModel.CapsSmall = "error";
            viewModel.MaxSixteen = "error";
            viewModel.NumSymbol = "error";
        }

        private void Btn_Edit_PersonalInformation_Clicked(object sender, EventArgs e)
        {

            viewModel.IndividualRegistrationView = true;
            viewModel.NationalAddressView = false;
            viewModel.ContactInformationView = false;
            viewModel.SummeryView = false;
        }

        private void Btn_Edit_NationalAddress_Clicked(object sender, EventArgs e)
        {
            viewModel.IndividualRegistrationView = false;
            viewModel.NationalAddressView = true;
            viewModel.ContactInformationView = false;
            viewModel.SummeryView = false;
        }

        private void Btn_Edit_ContactInformation_Clicked(object sender, EventArgs e)
        {
            viewModel.IndividualRegistrationView = false;
            viewModel.NationalAddressView = false;
            viewModel.ContactInformationView = true;
            viewModel.SummeryView = false;
        }

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;

                if (selectedReturntype.Text.Equals(AppResources.NDHijri))
                {
                    viewModel.IsHijriCal = true;
                    var selectedItem = SignUpDOBHijri.SelectedItem as ObservableCollection<object>;
                    if (selectedItem != null)
                    {
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.DOB = year + "/" + month + "/" + day;
                        viewModel.DOBddyymm = day + "/" + month + "/" + year;
                        string DOB = year + month + day;
                        viewModel.DOBPrev = viewModel.DOB;
                        
                    }
                    else
                    {
                        viewModel.DOB = string.Empty;
                        viewModel.DOBddyymm = string.Empty;

                    }

                }
                else
                {
                    viewModel.IsHijriCal = false;
                    var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                    if (selectedItem != null)
                    {
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.DOB = year + "/" + month + "/" + day;
                        viewModel.DOBddyymm = day + "/" + month + "/" + year;
                        string DOB = year + month + day;
                        viewModel.DOBPrev = viewModel.DOB;
                        
                    }
                    else
                    {
                        viewModel.DOB = string.Empty;
                        viewModel.DOBddyymm = string.Empty;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}