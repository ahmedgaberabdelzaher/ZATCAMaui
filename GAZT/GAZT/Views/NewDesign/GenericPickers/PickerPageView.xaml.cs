using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}
