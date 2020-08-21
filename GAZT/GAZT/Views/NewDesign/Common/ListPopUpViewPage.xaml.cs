using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Common
{
 
    public partial class ListPopUpViewPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(string item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public ListPopUpViewPage(List<string> data)
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
