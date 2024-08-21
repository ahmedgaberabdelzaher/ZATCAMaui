using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesDescriptionPopUpPageView : PopupPage
    {
        #region Variable
        public NotesDescriptionPopUpPageViewModel viewModel;
        #endregion

        #region Constructor
        public NotesDescriptionPopUpPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();


            try
            {
                viewModel = App.Locator.NotesDescriptionPopUpPageView;
                BindingContext = viewModel;
                if (vATDeclaration != null && vATDeclaration.data != null)
                {
                    if (vATDeclaration.data.NOTESSet.Count != 0)
                    {
                        viewModel.NoteList = vATDeclaration.data.NOTESSet.OrderBy(X => X.DataVersionz).ToList();
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
        #endregion


        private async void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {


            }
        }
    }
}