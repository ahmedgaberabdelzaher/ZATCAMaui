using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualRegistrationPageView : ContentPage
    {
        private static int CurrentView;
        IndividualRegistrationPageViewModel viewModel;
        public IndividualRegistrationPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.IndividualRegistrationPageView;
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.OnPageLoad();
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

        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {

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
           // ValidateIDNumber();
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
            var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
            string month = selectedItem[1].ToString();
            string day = selectedItem[0].ToString();
            string year = selectedItem[2].ToString();
            viewModel.DOB = year + "/" + month + "/" + day;
            string DOB = year + month + day;
            viewModel.DOBPrev = viewModel.DOB;
            //string DOB = Convert.ToDateTime(DOB.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
            EntryName.IsEnabled = true;
            if (viewModel.SelectedIdType.ID.Equals("ZS0015"))
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumber))
                {
                    try
                    {
                        //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DOB);
                        //IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        //if (SignupIsIDTypeValid.d == null)
                        //{
                        //    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        //    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        //    {
                        //        FrmIDNumber.HasError = true;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //    else
                        //    {
                        //        FrmIDNumber.HasError = false;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //}
                        //else
                        //{
                        //    viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                        //    EntryName.IsEnabled = false;
                        //    FrmIDNumber.HasError = false;
                        //}
                    }
                    catch
                    {
                        try
                        {
                            //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DOB);
                            //IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            //if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            //{
                            //    FrmIDNumber.HasError = true;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
                            //else
                            //{
                            //    FrmIDNumber.HasError = false;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdType.ID.Equals("ZS0017"))
            {
                EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumber))
                {
                    try
                    {
                        //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DOB);
                        //IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        //if (SignupIsIDTypeValid.d == null)
                        //{
                        //    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        //    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        //    {
                        //        FrmIDNumber.HasError = true;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //    else
                        //    {
                        //        FrmIDNumber.HasError = false;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //}
                        //else
                        //{
                        //    //viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                        //    //EntryName.IsEnabled = false;
                        //    //FrmIDNumber.HasError = false;
                        //}
                    }
                    catch
                    {
                        try
                        {
                            //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DOB);
                            //IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            //if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            //{
                            //    FrmIDNumber.HasError = true;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
                            //else
                            //{
                            //    FrmIDNumber.HasError = false;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdType.ID.Equals("ZS0018"))
            {
                EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumber))
                {
                    try
                    {
                        //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DOB);
                        //IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        //if (SignupIsIDTypeValid.d == null)
                        //{
                        //    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                        //    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                        //    {
                        //        FrmIDNumber.HasError = true;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //    else
                        //    {
                        //        FrmIDNumber.HasError = false;
                        //        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        //    }
                        //}
                        //else
                        //{
                        //    //viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                        //    //EntryName.IsEnabled = false;
                        //    //FrmIDNumber.HasError = false;
                        //}
                    }
                    catch
                    {
                        try
                        {
                            //string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DOB);
                            //IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            //if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            //{
                            //    FrmIDNumber.HasError = true;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
                            //else
                            //{
                            //    FrmIDNumber.HasError = false;
                            //    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            //}
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
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
            private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        {

        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            SignUpDOB.IsOpen = true;
        }

        private void LIssuedBy_Clicked(object sender, EventArgs e)
        {

        }

        private void LIssuedByCity_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSubmitNext_Clicked(object sender, EventArgs e)
        {

        }

        private void IDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DOB_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void IDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
            viewModel.SelectedIdType = viewModel.IdTypeList[viewModel.IDTypeIndex];
        }

        private void DOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void Country_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.CountryName = viewModel.CountryList[viewModel.SelectedCountryIndex].Natio;
            viewModel.SelectedCountry = viewModel.CountryList[viewModel.SelectedCountryIndex];
        }

        private void Country_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void GCCCountry_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.CountryName = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex].CountryName;
            viewModel.SelectedGCCCountry = viewModel.GCCCountryList[viewModel.SelectedGCCCountryIndex];
        }

        private void GCCCountry_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
        
        private void Region_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            // viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
        }

        private void Region_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void City_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            // viewModel.TxtIDType = viewModel.IdTypeList[viewModel.IDTypeIndex].Name;
        }

        private void City_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                viewModel.DOBPrev = viewModel.DOB;
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

        //private void DOB_Closed(object sender, EventArgs e)
        //{

        //}
        #endregion
        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
/*            if (CurrentView <= 6)
            {
                CurrentView++;
            }
            switch (CurrentView)
            {
                case 1:
                    break;
                case 2:
                    NationalAddressView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = ContactInformationView.IsVisible = VerificationCodeView.IsVisible =
                    SummeryView.IsVisible = PasswordView.IsVisible = false;
                    await BoxTwo.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 3:
                    ContactInformationView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = NationalAddressView.IsVisible = VerificationCodeView.IsVisible =
                    SummeryView.IsVisible = PasswordView.IsVisible = false;
                    await BoxThree.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 4:
                    VerificationCodeView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = NationalAddressView.IsVisible = ContactInformationView.IsVisible =
                    SummeryView.IsVisible = PasswordView.IsVisible = false;
                    await BoxFour.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 5: 
                    btnContinue.Text = "Confirm";
                    
                    SummeryView.IsVisible = true;
                    IndividualRegistrationView.IsVisible =ContactInformationView.IsVisible= NationalAddressView.IsVisible =
                        VerificationCodeView.IsVisible = PasswordView.IsVisible = false;
                    await BoxFive.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 6:
                    btnContinue.Text = "Continue";
                    PasswordView.IsVisible = true;
                    IndividualRegistrationView.IsVisible = ContactInformationView.IsVisible = NationalAddressView.IsVisible =
                        VerificationCodeView.IsVisible = SummeryView.IsVisible = false;
                   // await BoxFive.TranslateTo(100, 0, 500, Easing.BounceOut);
                    break;
                case 7:
            viewModel._navigationService.NavigateTo(App.RegistrationSuccessfulPageView);
                    break;
            }*/
        }
        // protected override async void OnAppearing()
        // {


        ///*     BoxOne.BackgroundColor = Color.DarkGreen;
        //     await BoxOne.TranslateTo(100,0,500,Easing.BounceOut);
        //     BoxTwo.BackgroundColor = BoxThree.BackgroundColor = BoxFour.BackgroundColor = BoxFive.BackgroundColor = Color.LightGray;

        //     NationalAddressView.IsVisible = ContactInformationView.IsVisible = VerificationCodeView.IsVisible =
        //     SummeryView.IsVisible = PasswordView.IsVisible = false;
        //     CurrentView = 1;*/
        // }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void PickerBtn_Country_Clicked(object sender, EventArgs e)
        {
            Picker_Country.IsOpen = true;
        }

        private void PickerBtn_Region_Clicked(object sender, EventArgs e)
        {
            Picker_Region.IsOpen = true;

        }

        private void PickerBtn_City_Clicked(object sender, EventArgs e)
        {
            Picker_City.IsOpen = true;
        }
    }
}