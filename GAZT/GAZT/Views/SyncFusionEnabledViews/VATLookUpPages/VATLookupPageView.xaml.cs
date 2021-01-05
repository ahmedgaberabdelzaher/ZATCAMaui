using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATLookupPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Application = Xamarin.Forms.Application;
using NavigationPage = Xamarin.Forms.NavigationPage;
namespace EGAZT.Views.SyncFusionEnabledViews.VATLookup
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookupPageView : ContentPage
    {
        private double width = 0;
        private double height = 0;
        bool isMandatoryDataEntered = true;
        VATLookupPageViewModel viewModel;
        public VATLookupPageView()
        {
            viewModel = App.Locator.VATLookupPageView;
            InitializeComponent();
            MainLayout.Padding = new Thickness(0, 0, 0, 0);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            SetLTR();
            SetPickerFont();
            NavigationPage.SetBackButtonTitle(this, "");
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            PPicker.SelectedItem = viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            
        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                PPicker.HeaderFontFamily = "GE SS Two";
                                PPicker.ColumnHeaderFontFamily = "GE SS Two";
                                PPicker.SelectedItemFontFamily = "GE SS Two";
                                PPicker.UnSelectedItemFontFamily = "GE SS Two";
                            }
                            else
                            {
                                PPicker.HeaderFontFamily = "SSTArabic-Medium";
                                PPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                PPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                PPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        PPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        PPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        PPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";

                        PPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        MainLayout.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        MainLayout.Padding = new Thickness(0, 0, 0, 0);
                    }
                }
                //reconfigure layout
            }
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Application.Current.GetType().Assembly);
            }
        }

        private void SelectedParametes_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
            viewModel.SelectedParameterTypePrev = vATParameterType;
            viewModel.SetSelectedParameterTypeData();
            viewModel.TxtSearchParameter = vATParameterType.ParameterType;
        }
        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            isMandatoryDataEntered = true;
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

            await Task.Run(async () =>
            {
                try
                {
                    ValidateFormData();

                    if (isMandatoryDataEntered)
                    {
                        isMandatoryDataEntered = true;
                        string _language = "A"; //UtilityManager.GetLanguageParameter();
                        VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, viewModel.SelectedParameterType.id, viewModel.LookupNumber);
                        if (vatLookUp != null)
                        {
                            if (vatLookUp.d != null)
                            {
                                if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))// Provided condiotion as per Vinay, Description comes null when the there is no error while calling the API
                                {
                                    viewModel.NameOrNoResultLabel = AppResources.Name;
                                    viewModel.Name = vatLookUp.d.results[0].Name;
                                }
                                else
                                {
                                    viewModel.NameOrNoResultLabel = "";
                                    viewModel.Name = "";
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {

                                        if (string.Compare(vatLookUp.d.results[0].Description, "Vat number is not equal to 15") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZVatnumberisnotequalto15, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid VAT number provided") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZInvalidVATnumberprovided, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Tin is not Active") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZTinisnotActive, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid TIN") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZInvalidTinNumber, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "No Data found against given parameters") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZNoDatafoundagainstgivenparameters, AppResources.ZError);
                                        } 
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "No VAT Certificate Found") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZNoVATCertificateFound, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Account is deregistered") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZAccountisderegistered, AppResources.ZError);
                                        }

                                        else
                                        {
                                            await viewModel._dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
                                        }
                                        
                                    });
                                }
                            }
                            else
                            {
                                viewModel.Name = vatLookUp.d.results[0].Name;
                                viewModel.NameOrNoResultLabel = AppResources.Nodataavailable;
                            }
                        }
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

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //viewModel._navigationService.GoBack();
                    });
                }
                catch (HttpRequestException ex)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //  viewModel._navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //  viewModel._navigationService.GoBack();
                    });
                }



            });

            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void ValidateFormData()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    frmLookupNumner.HasError = false;
                    // frmSearchParameter.HasError = false;
                });
                if (viewModel.SelectedParameterType != null)
                {
                    if (viewModel.LookupNumber != null && viewModel.LookupNumber != "")
                    {
                        if (viewModel.SelectedParameterType.id.Equals("3"))
                        {
                            if (viewModel.LookupNumber.Length != 15)
                            {
                                isMandatoryDataEntered = false;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    viewModel._dialogService.ShowMessageBox(AppResources.ZVATNumberisnotequalto15, AppResources.Information);
                                    frmLookupNumner.HasError = true;
                                });

                                return;
                            }
                        }
                        else if (viewModel.SelectedParameterType.id.Equals("2"))
                        {
                            if (viewModel.LookupNumber.Length != 10)
                            {
                                isMandatoryDataEntered = false;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    viewModel._dialogService.ShowMessageBox(AppResources.ZCRNumberisnotequalto10, AppResources.Information);
                                    frmLookupNumner.HasError = true;
                                });
                                return;
                            }
                        }
                        else if (viewModel.SelectedParameterType.id.Equals("4"))
                        {
                            if (viewModel.LookupNumber.Length != 15)
                            {
                                isMandatoryDataEntered = false;
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    viewModel._dialogService.ShowMessageBox(AppResources.ZVATNumberisnotequalto15, AppResources.Information);
                                    frmLookupNumner.HasError = true;
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
                            isMandatoryDataEntered = false;
                            viewModel._dialogService.ShowMessageBox(AppResources.ZZZPleaseentertheIDNumbervalue, AppResources.Information);
                            frmLookupNumner.HasError = true;
                        });

                        return;
                    }
                }
                else
                {
                    isMandatoryDataEntered = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._dialogService.ShowMessageBox(AppResources.ZPleaseselectparametertype, AppResources.Information);
                        //  frmSearchParameter.HasError = true;
                    });
                    return;
                }
            }
            catch (Exception ex)
            {
                isMandatoryDataEntered = false;
            }
        }
        public void OnParameterTypeEntryEntryFocussed(object sender, EventArgs args)
        {
            PPicker.IsOpen = true;
        }
        private void PPicker_btn_Clicked(object sender, EventArgs e)
        {
            PPicker.IsOpen = true;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
                PopUp popUp = new PopUp();//SetPlaceholderText();
                popUp.Message = viewModel.VATACCOrCRNOOrVATCER;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
        }

        private async void ScanCode_btn_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();

            Navigation.PushAsync(scan);

            string id = string.Empty;
            string _language = "A";

            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    entryNumber.Text = result.Text;
                    id = result.Text;
                    viewModel.SelectedParameterType = viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
                    SearchParameterEnter.Text = AppResources.ZVATLookupIDTaxpayerTinType1;
                    try
                    {
                        VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, "3", id);
                        if (vatLookUp != null)
                        {
                            if (vatLookUp.d != null)
                            {
                                if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))// Provided condiotion as per Vinay, Description comes null when the there is no error while calling the API
                                {
                                    viewModel.NameOrNoResultLabel = AppResources.Name;
                                    viewModel.Name = vatLookUp.d.results[0].Name;
                                }
                                else
                                {
                                    viewModel.NameOrNoResultLabel = "";
                                    viewModel.Name = "";

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {

                                        if (string.Compare(vatLookUp.d.results[0].Description, "Vat number is not equal to 15") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZVatnumberisnotequalto15, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid VAT number provided") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZInvalidVATnumberprovided, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Tin is not Active") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZTinisnotActive, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Invalid TIN") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZInvalidTinNumber, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "No Data found against given parameters") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZNoDatafoundagainstgivenparameters, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "No VAT Certificate Found") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZNoVATCertificateFound, AppResources.ZError);
                                        }
                                        else if (string.Compare(vatLookUp.d.results[0].Description, "Account is deregistered") == 0)
                                        {
                                            await viewModel._dialogService.ShowMessageBox(AppResources.ZZZAccountisderegistered, AppResources.ZError);
                                        }

                                        else
                                        {
                                            await viewModel._dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
                                        }


                                    });
                                }
                            }
                            else
                            {
                                viewModel.Name = vatLookUp.d.results[0].Name;
                                viewModel.NameOrNoResultLabel = AppResources.Nodataavailable;
                            }
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

                            await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (HttpRequestException ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.IsLoading = false;

                            await viewModel._dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            //  viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.IsLoading = false;

                            await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //  viewModel._navigationService.GoBack();
                        });

                    }

                });
            };
        }

        private void PPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedParameterType = viewModel.SelectedParameterTypePrev;
            PPicker.SelectedItem = viewModel.SelectedParameterTypePrev;
            if (viewModel.SelectedParameterTypePrev == null)
            {
                viewModel.TxtSearchParameter = string.Empty;
            }
        }
    }
}