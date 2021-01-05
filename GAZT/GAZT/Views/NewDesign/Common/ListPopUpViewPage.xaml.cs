using System;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.Common
{
    [Preserve(AllMembers = true)]

    public partial class ListPopUpViewPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public ListPopUpViewPage(object data)
        {
            InitializeComponent();
            PopupList.ItemsSource = (System.Collections.IEnumerable)data;
            SetLTR();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        async void PopupList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
