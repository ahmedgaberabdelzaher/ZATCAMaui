using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATLookupPage_ViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Application = Xamarin.Forms.Application;
using NavigationPage = Xamarin.Forms.NavigationPage;
namespace EGAZT.Views.SyncFusionEnabledViews.VATLookup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookupPageView : ContentPage
    {
        private double width = 0;
        private double height = 0;
        bool isMendatoryDataEntered = true;
        VATLookupPageViewModel viewModel;
        int LanguageToolBarCount = 0;
        public VATLookupPageView()
        {
            viewModel = App.Locator.VATLookupPageView;
            InitializeComponent();
            MainLayout.Padding = new Thickness(0, 0, 0, 0);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
            //PickerResourceManager.Manager = new ResourceManager("GAZT.Resources.Syncfusion.SfPicker.XForms", Application.Current.GetType().Assembly);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            SetLTR();
            viewModel.IsTooltipEnableVisible = false;
            NavigationPage.SetBackButtonTitle(this, "");
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            PPicker.SelectedItem = viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
            //ToolbarItem toolbarItem1 = new ToolbarItem
            //{
            //};
            //if (LanguageToolBarCount == 0)
            //{
            //    LanguageToolBarCount = 1;
            //    this.ToolbarItems.Add(toolbarItem1);
            //}
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
                PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Application.Current.GetType().Assembly);
            }
        }
        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (viewModel.SelectedParameterType != null)
        //        {
        //            PPicker.Focus();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //}
        private void btn1_Clicked(object sender, EventArgs e)
        {
            // PPicker.IsOpen = true;
        }
        private void SelectedParametes_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
            viewModel.SelectedParameterTypePrev = vATParameterType;
            viewModel.SetSelectedParameterTypeData();
            viewModel.TxtSearchParameter = vATParameterType.ParameterType;
            //viewModel.SelectedSignUpUsing = signUpUsing;
            //viewModel.TxtIDNumber = signUpUsing.SUType;
        }
        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                isMendatoryDataEntered = true;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        ValidateFormData();//isMendatoryDataEntered
                        if (isMendatoryDataEntered)
                        {
                            isMendatoryDataEntered = true;
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
                                            await viewModel._dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
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
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel._dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                    // IsLoading = true;
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                try
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel._dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    });
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
                catch (Exception a)
                {
                }
            }
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
                                isMendatoryDataEntered = false;
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
                                isMendatoryDataEntered = false;
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
                                isMendatoryDataEntered = false;
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
                        isMendatoryDataEntered = false;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            isMendatoryDataEntered = false;
                            viewModel._dialogService.ShowMessageBox(AppResources.ZPleaseenterthecorrespondingnumber, AppResources.Information);
                            frmLookupNumner.HasError = true;
                        });
                        return;
                    }
                }
                else
                {
                    isMendatoryDataEntered = false;
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
                isMendatoryDataEntered = false;
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
        private void SfButton_Clicked(object sender, EventArgs e)
        {
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
                    //entryNumber.IsEnabled = false;
                    ///PPicker_btn.IsEnabled = false;
                    //PPicker.IsEnabled = false; SearchParameterEnter.IsEnabled = false;
                   viewModel.SelectedParameterType=viewModel.ParameterTypeList.Where(x => x.id == "3").FirstOrDefault();
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
                                        await viewModel._dialogService.ShowMessageBox(vatLookUp.d.results[0].Description, AppResources.ZError);
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
                    catch (InternetException ex)
                    {
                        try
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                viewModel._dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                            });
                            await Task.Run(() =>
                            {
                                viewModel.IsLoading = false;
                            });
                        }
                        catch (Exception a)
                        {
                        }
                    }
                });
            };
        }
        private void PPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedParameterType = viewModel.SelectedParameterTypePrev;
            PPicker.SelectedItem = viewModel.SelectedParameterTypePrev;
            if(viewModel.SelectedParameterTypePrev == null)
            {
                viewModel.TxtSearchParameter = string.Empty;
            }
        }
    }
}