using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    public partial class AddNotesPopupPageView : PopupPage
    {
        AddNotePopUpViewModel viewModel;

        public AddNotesPopupPageView(string notes, bool isEdit)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.AddNotesPopupPageViewModel;
                BindingContext = viewModel;
                NavigationPage.SetBackButtonTitle(this, "");
                NavigationPage.SetHasBackButton(this, false);
                viewModel.NoteText = notes;
                viewModel.IsEdit = isEdit;

            }
            catch (Exception)
            {
            }
        }

        #region Methods
       

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override bool OnBackgroundClicked()
        {
            return true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<object, string>(this, "Notes", viewModel.NoteText);

        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<object, string>(this, "Notes", viewModel.NoteText);
                MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {




            }
        }

        private void NoteDetail_Unfocused(object sender, FocusEventArgs e)
        {

        }
        #endregion
    }
}
