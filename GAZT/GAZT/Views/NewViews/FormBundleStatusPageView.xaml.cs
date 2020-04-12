using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormBundleStatusPageView : ContentPage
    {
        FormBundleStatusPageViewModel viewModel;
        public FormBundleStatusPageView()
        {
            viewModel = App.Locator.FormBundleStatusPageView;
            InitializeComponent();
            viewModel.FormBundleList = null;
            viewModel.FormBundleApplicatioNumberList = null;
            viewModel.SelectedFormBindleFbtyp = null;
            viewModel.SelectedFormBindleFbnum = null;
            viewModel.ListFormBudles = null;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            this.BindingContext = viewModel;
            OnPageLoad();
            //CPicker_imgtap.IsEnabled = false;
            //tapImg.Tapped += Gesture_Tapped;

            //void Gesture_Tapped(object sender, EventArgs e)
            //{
            //    tapImg.Tapped -= Gesture_Tapped;
            //}


            SetLTR();
        }


        public async void OnPageLoad()
        {
            Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });


            await Task.Run(async () =>
            {
                await viewModel.onPageLoad();
                
            });
            Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
            try
            {
                viewModel.SelectedFormBindleFbtypCancel = (FormBundleResult)DDlIDType.SelectedItem;
            }
            catch (Exception ex)
            {

            }
            //BPicker.Focus();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {if (viewModel.SelectedFormBindleFbtyp != null)
            {
               CPicker.IsOpen = true;
                //CPicker.Focus();
            }
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {

                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);

            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
        }

        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            
            //EntryIDNumber.IsEnabled = true;
            
          
        }

        private void CPicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
          

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            viewModel.SelectedFormBindleFbnum = null;
            viewModel.SelectedFormBindleFbtyp = null;
            viewModel.TxtFBnum = string.Empty;
            viewModel.TxtFBtype = string.Empty;

        }

        private void CPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {//SelectedFormBindleFbnum

            FormBundleApplicationNumberModelResult selectedfbnum = (FormBundleApplicationNumberModelResult)e.NewValue;
            CPicker.SelectedItem = selectedfbnum;//Fbnum
            viewModel.SelectedFormBindleFbnum = selectedfbnum;
            viewModel.TxtFBnum = selectedfbnum.Fbnum;

            //var item = sender as Picker;
            //var selectedItem = item.SelectedItem as FormBundleApplicationNumberModelResult;
            viewModel.populate();


        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            DDlIDType.SelectedItem = viewModel.SelectedFormBindleFbtypCancel;
            viewModel.SelectedFormBindleFbtyp = viewModel.SelectedFormBindleFbtypCancel;
        }

        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {//SelectedFormBindleFbtyp
            FormBundleResult selectedfbtyp = (FormBundleResult)e.NewValue;
            DDlIDType.SelectedItem = selectedfbtyp;
            viewModel.SelectedFormBindleFbtyp = selectedfbtyp;
            viewModel.TxtFBtype = selectedfbtyp.Txt50;

        }
    }
}