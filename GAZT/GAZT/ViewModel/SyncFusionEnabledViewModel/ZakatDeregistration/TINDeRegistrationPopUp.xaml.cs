using System;
using System.Collections.Generic;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public partial class TINDeRegistrationPopUp :PopupPage
    {
        public delegate void OnSaveAsDraftSelectDelegate();
        public OnSaveAsDraftSelectDelegate OnItemSelect { get; set; } = null;

        public TINDeRegistrationPopUp()
        {
            InitializeComponent();
        }

        private async void OnSaveAsDraftClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            OnItemSelect?.Invoke();
        }

    }
}
