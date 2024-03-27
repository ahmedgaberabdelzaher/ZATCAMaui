using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewNotesPopUpPageView : PopupPage
    {
        public ViewNotePopUpViewModel viewModel;

        public ViewNotesPopUpPageView(NotesSet notesSet)
        {

            try
            {
                InitializeComponent();
                viewModel = App.Locator.ViewNotePopUpViewModel;
                BindingContext = viewModel;
                if (notesSet != null)
                {
                    if (notesSet.results.Count != 0)
                    {
                        viewModel.NoteList = notesSet.results.OrderBy(X => X.DataVersionz).ToList();
                        viewModel.IsDisplayNoteVisible = true;
                        viewModel.IsNoDataLabelVisible = false;
                    }
                    else
                    {
                        viewModel.IsDisplayNoteVisible = false;
                        viewModel.IsNoDataLabelVisible = true;
                    }
                }

                NavigationPage.SetBackButtonTitle(this, "");
            }
            catch (Exception)
            {


            }

        }

        #region Methods
        #endregion

        private async void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {


            }
        }
    }
}