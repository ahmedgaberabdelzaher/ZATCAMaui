using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
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

        async void Void_Button_Clicked(object sender, EventArgs e)
        {
            if (notesInputLayout.HasError || string.IsNullOrWhiteSpace(notes.Text))
            {
                notesInputLayout.HasError = true;
                return;
            }
            OnVoidSelect?.Invoke(notes.Text);
            await PopupNavigation.Instance.PopAsync();
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                notesInputLayout.HasError = string.IsNullOrWhiteSpace(notes.Text);
            }
            catch (Exception)
            {


            }
        }
    }
}
