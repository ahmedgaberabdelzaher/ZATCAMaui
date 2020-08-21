using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class NumberListPopUpPageView : PopupPage
    {
        public NumberListPopUpPageView(List<string> data)
        {
            InitializeComponent();
            PopupList.ItemsSource = data;
        }

        async void PopupList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
