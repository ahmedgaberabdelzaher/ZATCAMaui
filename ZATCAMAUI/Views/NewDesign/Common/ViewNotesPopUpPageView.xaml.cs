using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Models.VATInstalmentModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common
{

   
        [XamlCompilation(XamlCompilationOptions.Compile)]
        public partial class ViewNotesPopUpPageView : PopupPage
        {
            public ViewNotePopUpViewModel viewModel;

            public ViewNotesPopUpPageView(List<NotesSetResult> notesSet)
            {
                InitializeComponent();

                try
                {
                    viewModel = App.Locator.ViewNotePopUpViewModel;
                    BindingContext = viewModel;
                    if (notesSet != null)
                    {
                        if (notesSet.Count != 0)
                        {
                            viewModel.NoteList = notesSet.OrderBy(X => X.DataVersionz).ToList();
                            viewModel.IsDisplayNoteVisible = true;
                            viewModel.IsNoDataLabelVisible = false;
                        }
                        else
                        {
                            viewModel.IsDisplayNoteVisible = false;
                            viewModel.IsNoDataLabelVisible = true;
                        }
                    }

                }
                catch (Exception )
                {
                }

            }

            #region Methods
            #endregion

            private async void OnCloseTapped(object sender, EventArgs e)
            {
                try
                {
                    await MopupService.Instance.PopAsync();
                }
                catch (Exception )
                {
                }
            }
        }
    
}