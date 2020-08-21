using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class NumberListPopUpPageView : PopupPage
    {
        public delegate void OnItemSelectDelegate(string item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public NumberListPopUpPageView(List<string> data)
        {
            InitializeComponent();
            PopupList.ItemsSource = data;
        }

        async void PopupList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (OnItemSelect != null)
            {
                OnItemSelect(e.CurrentSelection.FirstOrDefault() as string);
            }
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
