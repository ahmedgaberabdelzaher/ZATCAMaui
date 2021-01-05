using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Models.VATInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddNotePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NotesSet = EGAZT.Models.NotesSet;

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
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                SetLTR();
                ChangeAeroIcon();
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

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
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

            }
        }

        private void NoteDetail_Unfocused(object sender, FocusEventArgs e)
        {

        }
        #endregion
    }
}
