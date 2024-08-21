
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.DisplayNotesPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayNotesPageView : ContentPage
    {
        #region Variable
        DisplayNotesPageViewModel viewModel;
        #endregion

        #region Constructor
        public DisplayNotesPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            try
            {
                viewModel = App.Locator.DisplayNotesPageView;
                BindingContext = viewModel;
                if (vATDeclaration != null && vATDeclaration.data != null)
                {
                    if (vATDeclaration.data.NOTESSet.Count != 0)
                    {
                        viewModel.NoteList = vATDeclaration.data.NOTESSet;
                        viewModel.IsDisplayNoteVisible = true;
                        viewModel.IsNoDataLabelVisible = false;
                    }
                    else
                    {
                        viewModel.IsDisplayNoteVisible = false;
                        viewModel.IsNoDataLabelVisible = true;
                    }
                }
                //viewModel.NoteList =
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}