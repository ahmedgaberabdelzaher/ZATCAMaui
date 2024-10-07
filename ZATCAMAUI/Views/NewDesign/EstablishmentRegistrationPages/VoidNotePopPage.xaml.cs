using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{

    public partial class VoidNotePopPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(string note);
        public OnItemSelectDelegate OnVoidSelect { get; set; } = null;
        public VoidNotePopPage()
        {
            InitializeComponent();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        async void Void_Button_Clicked(object sender, EventArgs e)
        {
            if (notesInputLayout.HasError || string.IsNullOrWhiteSpace(notes.Text))
            {
                notesInputLayout.HasError = true;
                return;
            }
            OnVoidSelect?.Invoke(notes.Text);
            await MopupService.Instance.PopAsync();
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
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
