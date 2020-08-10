using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.GenericPickers
{
    public partial class CalendarPickerPageView : PopupPage
    {
        CalendarPickerPageViewModel viewModel;
        public CalendarPickerPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.CalendarPickerPageView;
            viewModel.SetDefaultDate();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
        }
    }
}
