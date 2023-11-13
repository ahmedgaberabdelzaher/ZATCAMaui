using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [Preserve(AllMembers = true)]
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
            if (notesInputLayout.HasError || string.IsNullOrWhiteSpace(notes.Text))
            {
                notesInputLayout.HasError = true;
                return;
            }
            OnVoidSelect?.Invoke(notes.Text);
            await PopupNavigation.Instance.PopAsync();
        }

        async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void notes_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try { 
            notesInputLayout.HasError = string.IsNullOrWhiteSpace(notes.Text);
            }
            catch (Exception)
            {
                
                
            }
        }
    }
}
