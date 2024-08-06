
using Newtonsoft.Json;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualRegistrationPageView : ContentPage
    {

        IndividualRegistrationPageViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public IndividualRegistrationPageView(string isGulf)
        {
            try
            {


                InitializeComponent();

                viewModel = App.Locator.IndividualRegistrationPageView;
                BindingContext = viewModel;
                viewModel.IsHijriCal = false;

                if (isGulf == "Gulf")
                {
                    viewModel.IsGulfER = true;
                    viewModel.IsCitizen = false;
                }
                else if (isGulf == "RegisterPageSSO")
                {
                    viewModel.IsGulfER = false;
                    viewModel.IsCitizen = true;
                }
                else
                {
                    viewModel.IsGulfER = true;
                    viewModel.IsCitizen = false;
                }


                viewModel.ClearData();
                Task.Run(async () =>
               {
                   await viewModel.OnPageLoad();
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
                viewModel.PopulateDataInChips();
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                viewModel.IsHijriCal = false;
                viewModel.DOBddyymm = string.Empty;
                viewModel.DOB = string.Empty;
                SetPickerFont();
                 _ = viewModel.GetCaptchImage();

            }
            catch (Exception ex)
            {

            }
        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                //FlowDirection = FlowDirection.LeftToRight;
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                EntryMobileNumber.HorizontalTextAlignment = TextAlignment.Start;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
            }
            else
            {
                //FlowDirection = FlowDirection.RightToLeft;
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                EntryMobileNumber.HorizontalTextAlignment = TextAlignment.End;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);

            }
        }


        #region

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
                    }
                    else
                    {
                        viewModel.FrameIDError = false;
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
                    if (viewModel.IdNumber.Length < 7 || viewModel.IdNumber.Length > 15)
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));

                        viewModel.FrameIDError = true;
                        viewModel.IdNumber = string.Empty;
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
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                        //MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                        //MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                        //MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
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

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            ValidateIDNumber();
        }

        public async void ValidateIDNumber()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
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
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {



                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception)


                        {


                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                }
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void CountryCodes_Clicked(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));

        }

        private void IDType_SelectedIndexChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDType = viewModel.IdTypeList[e.NewValue].Name;
                viewModel.SelectedIdType = viewModel.IdTypeList[e.NewValue];
                viewModel.IdNumber = string.Empty;
                if (!viewModel.SelectedIdType.ID.Equals("ZS0018"))
                {
                    if (string.IsNullOrEmpty(viewModel.DOB) && string.IsNullOrEmpty(viewModel.IdNumber))
                    {
                        ValidateIDNumber();
                    }

                }
            }
            catch (Exception)
            {



            }
        }

        private void IDType_CancelButtonClicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = false;
        }

        private void DDlIDType_OkayButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selected = sender as SfPicker;
                viewModel.TxtIDType = viewModel.IdTypeList[selected.Columns[0].SelectedIndex].Name;
                viewModel.SelectedIdType = viewModel.IdTypeList[selected.Columns[0].SelectedIndex];
                viewModel.IdNumber = string.Empty;
                if (!viewModel.SelectedIdType.ID.Equals("ZS0018"))
                {
                    if (string.IsNullOrEmpty(viewModel.DOB) && string.IsNullOrEmpty(viewModel.IdNumber))
                    {
                        ValidateIDNumber();
                    }

                }

            }
            catch (Exception)
            {



            }
        }

        private void DOB_CancelButtonClicked(object sender, EventArgs e)
        {
            SignUpDOB.IsOpen = false;
        }

        private void GCCCountry_OkayButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selected = sender as SfPicker;
                viewModel.CountryName = viewModel.GCCCountryList[selected.Columns[0].SelectedIndex].CountryName;
                viewModel.SelectedGCCCountry = viewModel.GCCCountryList[selected.Columns[0].SelectedIndex];
                GCCPicker_Country.IsOpen = false;
            }
            catch (Exception)
            {



            }
        }

        private void GCCCountry_CancelButtonClicked(object sender, EventArgs e)
        {
            GCCPicker_Country.IsOpen = false;
        }

        private void Region_OkayButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selected = sender as SfPicker;
                viewModel.SelectedRegion = viewModel.RegionList[selected.Columns[0].SelectedIndex];
                viewModel.Region = viewModel.RegionList[selected.Columns[0].SelectedIndex].Bezei;
                viewModel.CityName = string.Empty;
                Picker_Region.IsOpen = false;
            }
            catch (Exception)
            {



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

        private void Region_CancelButtonClicked(object sender, EventArgs e)
        {
            Picker_Region.IsOpen = false;
        }

        private void City_OkayButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selected = sender as SfPicker;
                viewModel.SelectedCity = viewModel.CityList[selected.Columns[0].SelectedIndex];
                viewModel.CityName = viewModel.CityList[selected.Columns[0].SelectedIndex].CityName;
                Picker_City.IsOpen = false;
            }
            catch (Exception)
            {



            }
        }

        private void City_CancelButtonClicked(object sender, EventArgs e)
        {
            Picker_City.IsOpen = false;
        }

        private void DOB_OkButtonClicked(object sender, EventArgs e)
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
            catch (Exception)
            {

            }

        }


        #endregion

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                idType.Text = "";
                Number.Text = "";
                Birthdt.Text = "";
                Firstname.Text = "";
                if (viewModel.IsCitizen)
                {
                    Task.Run(async () =>
                    {
                        viewModel.modelSSOID = await WebServiceManager.LoginDataSSO();
                        //viewModel.IdNumber = viewModel.modelSSOID.results[0].Idnumber;
                        MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        // EntryIDNumber.Text = viewModel.modelSSOID.results[0].Idnumber;
                                if (viewModel.modelSSOID.results[0].IdType == "ZS0015")
                                {
                                    idType.Text = AppResources.NationaID;
                                }
                                else
                                {
                                    idType.Text = AppResources.VFCIqamaID;
                                    if (viewModel.modelSSOID.results[0].AIqamaType.Length > 0)
                                    {
                                        viewModel.IqamaTypeDesc = viewModel.modelSSOID.results[0].AIqamaDesc;
                                        viewModel.ShowIqamaTypeDesc = true;
                                    }
                                    else
                                    {
                                        viewModel.IqamaTypeDesc = "";
                                        viewModel.ShowIqamaTypeDesc = false;
                                    }

                                }

                        Number.Text = viewModel.modelSSOID.results[0].Idnumber;
                                Birthdt.Text = UtilityManager.FormatDateToYYYYDDMMFromDateTypeString(viewModel.modelSSOID.results[0].Birthdt);
                                viewModel.DOBddyymm = UtilityManager.FormatDateToYYYYDDMMFromDateTypeString(viewModel.modelSSOID.results[0].Birthdt); ;
                                Firstname.Text = viewModel.modelSSOID.results[0].Firstname;

                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });

                    });

                }



                if (Device.RuntimePlatform == Device.Android)
                {
                    DDlIDType.Background = (Color)Application.Current.Resources["PickerBgGray"];
                    GCCPicker_Country.Background = (Color)Application.Current.Resources["PickerBgGray"];
                    Picker_Region.Background = (Color)Application.Current.Resources["PickerBgGray"];
                    Picker_City.Background = (Color)Application.Current.Resources["PickerBgGray"];
                }
                else
                {
                    DDlIDType.Background = (Color)Application.Current.Resources["White"];
                    GCCPicker_Country.Background = (Color)Application.Current.Resources["White"];
                    Picker_Region.Background = (Color)Application.Current.Resources["White"];
                    Picker_City.Background = (Color)Application.Current.Resources["White"];
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
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();
                        viewModel.MobileCountry = mobileData.Where(x => x.Telefto == viewModel.TxtCountryCode).FirstOrDefault().Land1;
                    });

                }
                catch (Exception)
                {



                }
                if (viewModel.currentStep == 5 && App.IsComingFromSleepMode)
                {

                    int timeToExpireOTP = 120;
                    viewModel.TimerStart(timeToExpireOTP);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PickerBtn_Region_Clicked(object sender, TappedEventArgs e)
        {
            Picker_Region.IsOpen = true;

        }

        private void PickerBtn_City_Clicked(object sender, TappedEventArgs e)
        {
            Picker_City.IsOpen = true;
        }

        private void GCCPickerBtn_Country_Clicked(object sender, TappedEventArgs e)
        {
            GCCPicker_Country.IsOpen = true;
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
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                    viewModel.FrameEmailError = true;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameEmailError = false;
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
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
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
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                    viewModel.FrameConfirmEmailError = true;
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
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
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
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
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

        private void ImageSeePassword_Tapped(object sender, TappedEventArgs e)
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

        private void ImageSeeConfirmPassword_Tapped(object sender, TappedEventArgs e)
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
            if (!string.IsNullOrEmpty(viewModel.CityName))
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                        viewModel.PostalCode = string.Empty;
                    }
                }


            }
        }

        private void GCCPicker_Country_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                viewModel.CountryName = viewModel.GCCCountryList[e.NewValue].CountryName;
                viewModel.SelectedGCCCountry = viewModel.GCCCountryList[e.NewValue];
            }
            catch (Exception)
            {



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
            catch (Exception)
            {


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
                DDlIDType.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                DDlIDType.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                DDlIDType.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                DDlIDType.TextStyle.FontFamily = "Somar-SemiBold";

                GCCPicker_Country.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                GCCPicker_Country.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                GCCPicker_Country.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                GCCPicker_Country.TextStyle.FontFamily = "Somar-SemiBold";


                Picker_Region.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                Picker_Region.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                Picker_Region.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                Picker_Region.TextStyle.FontFamily = "Somar-SemiBold";

                Picker_City.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                Picker_City.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                Picker_City.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                Picker_City.TextStyle.FontFamily = "Somar-SemiBold";


                SignUpDOB.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOB.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOB.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOB.TextStyle.FontFamily = "Somar-SemiBold";

                SignUpDOBHijri.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOBHijri.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOBHijri.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                SignUpDOBHijri.TextStyle.FontFamily = "Somar-SemiBold";
            }
            catch (Exception)
            {


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

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
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
            catch (Exception)
            {


            }
        }

        void btnIdType_Clicked(object sender,TappedEventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        void btnDate_Clicked(object sender, TappedEventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                SignUpDOBHijri.IsOpen = true;
            }
            else
            {
                SignUpDOB.IsOpen = true;


            }
        }
        void RefreshCaptchaClicked(System.Object sender, System.EventArgs e)
        {
            viewModel.GetCaptchImage();
            viewModel.Captcha = string.Empty;
        }

    }
}