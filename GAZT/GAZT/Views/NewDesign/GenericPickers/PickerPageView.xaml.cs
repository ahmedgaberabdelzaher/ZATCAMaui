using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.GenericPickers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.GenericPickers
{
    public partial class PickerPageView : PopupPage
    {
        PickerPageViewModel viewModel;
        public PickerPageView(ObservableCollection<string> _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.PickerItemSource = _pickerSource;
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }


        private void SetLTR()
        {
            if (!App.IsArabic)
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

        public PickerPageView(GenericPickerModel _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.DataSource = _pickerSource;
            viewModel.PickerItemSource = viewModel.DataSource.PickerData;
            viewModel.PickerTitle = viewModel.DataSource.PickerTitle;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }

        void genericPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.DataSource.SelectedValue = e.NewValue.ToString();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PopupClose_Clicked(object sender, EventArgs e)
        {
         
                try
                {
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                PopupNavigation.Instance.PopAsync();
            
        }

        void PopupPage_BackgroundClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
