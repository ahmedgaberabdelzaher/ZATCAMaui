using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.GenericPickers;
using Rg.Plugins.Popup.Pages;
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

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
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
            
            viewModel.DataSource.SelectedValue = e.NewValue.ToString();
        }

        void PopupPage_BackgroundClicked(System.Object sender, System.EventArgs e)
        {

            MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
        }
    }
}
