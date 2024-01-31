using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
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
                SetLTR();
                viewModel.NoteText = notes;
                viewModel.IsEdit = isEdit;

            }
            catch (Exception)
            {
            }
        }

        #region Methods
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
                PopupNavigation.Instance.PopAsync();
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
