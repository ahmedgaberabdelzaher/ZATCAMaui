using System;
using System.Collections.Generic;
using GAZT.Helper;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{	
	public partial class ErrorMessagePopup : PopupPage
    {
        public delegate void OnDoneDelegate();
        public delegate void OnLinkDelegate();

        public OnDoneDelegate OnDone { get; set; } = null;
        public OnLinkDelegate OnLink { get; set; } = null;

        public ErrorMessagePopup(string infromationText)
        {
            InitializeComponent();
            InfromatationText.Text = infromationText;

            SetLTR();
        }


        private void OnCloseTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            OnDone?.Invoke();
            PopupNavigation.Instance.PopAsync();
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                InfromatationText.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                InfromatationText.HorizontalOptions = LayoutOptions.StartAndExpand;
            }
        }

        void Link_Clicked(System.Object sender, System.EventArgs e)
        {
            // OnLink?.Invoke();
            PopupNavigation.Instance.PopAsync();
            if (App.IsArabic)
            {
                Uri uri = new Uri(Constants.ZAtcaContactUsAR);
                OpenBrowser(uri);
            }
            else
            {
                Uri uri = new Uri(Constants.ZAtcaContactUsEN);
                OpenBrowser(uri);
            }
            
        }
        public async void OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }
    }
}