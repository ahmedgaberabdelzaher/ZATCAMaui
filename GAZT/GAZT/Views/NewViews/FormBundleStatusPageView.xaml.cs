using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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
            this.BindingContext = viewModel;
            //CPicker_imgtap.IsEnabled = false;
            //tapImg.Tapped += Gesture_Tapped;

            //void Gesture_Tapped(object sender, EventArgs e)
            //{
            //    tapImg.Tapped -= Gesture_Tapped;
            //}
            viewModel.onPageLoad();
          
            SetLTR();
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
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
            ((ListView)sender).SelectedItem = null;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
        {
            //var item = sender as Picker;
            //var selectedItem = item.SelectedItem as FormBundleApplicationNumberModelResult;
            viewModel.populate();


        }
    }
}