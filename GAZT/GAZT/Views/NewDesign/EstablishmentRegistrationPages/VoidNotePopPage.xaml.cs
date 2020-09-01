using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class VoidNotePopPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(string note);
        public OnItemSelectDelegate OnVoidSelect { get; set; } = null;
        public VoidNotePopPage()
        {
            InitializeComponent();
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

        async void Void_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            OnVoidSelect?.Invoke(notes.Text);
            await PopupNavigation.Instance.PopAsync();
        }

        async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
