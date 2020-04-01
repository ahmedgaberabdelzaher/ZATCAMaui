using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookupPageView : ContentPage
    {
        bool isMendatoryDataEntered = true;
        VATLookupPageViewModel viewModel;
        int LanguageToolBarCount = 0;
        public VATLookupPageView()
        {
            viewModel = App.Locator.VATLookupPageView;
            InitializeComponent();
            //CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
            //PickerResourceManager.Manager = new ResourceManager("GAZT.Resources.Syncfusion.SfPicker.XForms", Application.Current.GetType().Assembly);
            this.BindingContext = viewModel;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            SetLTR();
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
        private void SetLTR()
        {
            if (App.IsArabic)
            {

                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.TestPicker", Application.Current.GetType().Assembly);

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
            viewModel.SetSelectedParameterTypeData();
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
                    ValidateFormData();//isMendatoryDataEntered
                    if (isMendatoryDataEntered)
                    {
                        isMendatoryDataEntered = true;
                        string _language = "A"; //UtilityManager.GetLanguageParameter();
                        VATLookUp vatLookUp = await WebServiceManager.GAZTGetVATLookUp(_language, viewModel.SelectedParameterType.id, viewModel.LookupNumber);
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

                    // IsLoading = true;
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await viewModel._dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
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

                                    viewModel._dialogService.ShowMessageBox(AppResources.ZVATCerNumberisnotequalto15, AppResources.Information);
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
    }
}
