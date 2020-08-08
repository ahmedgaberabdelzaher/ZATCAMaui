using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
//using Syncfusion.BarcodeReader.OPX;
//using Syncfusion.Pdf.Parsing;

namespace EGAZT.Views.NewDesign.VATLookUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
        bool isMandatoryDataEntered = true;
        VATLookUpNewPageViewModel viewModel;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            this.BindingContext = viewModel;

            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = "Search VAT";
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
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
        private void ResetFormData()
        {
            viewModel.Name = "";
            viewModel.IsNameVisible = false;
            viewModel.LookupNumber = "";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
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
                    if (!string.IsNullOrEmpty(viewModel.Name))
                    {
                        ResetFormData();
                        return;
                    }
                    ValidateFormData();

                    if (isMandatoryDataEntered)
                    {
                        isMandatoryDataEntered = true;
                        string _language = "A"; //UtilityManager.GetLanguageParameter();
                        
                        GAZT.Models.VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, viewModel.SelectedParameterType.id, viewModel.LookupNumber);
                        if (vatLookUp != null)
                        {
                            if (vatLookUp.d != null)
                            {
                                if (string.IsNullOrEmpty(vatLookUp.d.results[0].Description))// Provided condiotion as per Vinay, Description comes null when the there is no error while calling the API
                                {
                                    viewModel.NameOrNoResultLabel = AppResources.Name;
                                    viewModel.Name = vatLookUp.d.results[0].Name;
                                    viewModel.IsNameVisible = true;
                                    viewModel.LookUpButtonText = AppResources.ZVATLookUpNewSearchButtonText;
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

        void PPicker_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsNameVisible)
                return;

            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
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

        void PPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
            //PPicker.IsOpen = false;
        }

        private void btnScan_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            ZXingScannerPage scanPage = new ZXingScannerPage();
            Navigation.PushAsync(scanPage);
            //BarcodeReader reader = new BarcodeReader(&quot; Barcode.pdf & quot;, FormatType.PDF);

            string id = string.Empty;
            string _language = "A";
            scanPage.OnScanResult += (result) =>
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
                        GAZT.Models.VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, "3", id);
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
    }
}
