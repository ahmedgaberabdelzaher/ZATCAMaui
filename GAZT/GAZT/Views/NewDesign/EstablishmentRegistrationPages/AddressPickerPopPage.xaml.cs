using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EGAZT.Models.EstablishmentRegistration;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class AddressPickerPopPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;

        public AddressPickerPopPage(IEnumerable addressess)
        {
            InitializeComponent();
            addressList.ItemsSource = addressess;
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
        async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        async void AddressList_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
