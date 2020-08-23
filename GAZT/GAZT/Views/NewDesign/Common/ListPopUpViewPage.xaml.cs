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
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public ListPopUpViewPage(object data)
        {
            InitializeComponent();
            PopupList.ItemsSource = (System.Collections.IEnumerable)data;
        }

        async void PopupList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
