using System;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.Common
{
    [Preserve(AllMembers = true)]
    public partial class AddNotesPopupPageView : PopupPage
    {
        AddNotePopUpViewModel viewModel;

        public AddNotesPopupPageView(String notes, bool isEdit)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.AddNotesPopupPageViewModel;
                BindingContext = viewModel;
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
                SetLTR();
                viewModel.NoteText = notes;
                viewModel.IsEdit = isEdit;

            }
            catch (Exception e)
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
            MessagingCenter.Send<Object, string>(this, "Notes", viewModel.NoteText);

        }

        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<Object, string>(this, "Notes", viewModel.NoteText);
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {

                Console.Write(ex.StackTrace.ToString());
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void NoteDetail_Unfocused(object sender, FocusEventArgs e)
        {

        }
        #endregion
    }
}
