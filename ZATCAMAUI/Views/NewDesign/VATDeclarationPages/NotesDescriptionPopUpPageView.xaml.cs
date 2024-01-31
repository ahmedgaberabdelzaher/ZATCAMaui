using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
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
            //viewModel = App.Locator.NotesDescriptionPopUpPageView;
            //BindingContext = viewModel;
            //SetLTR();


            try
            {
                viewModel = App.Locator.NotesDescriptionPopUpPageView;
                BindingContext = viewModel;
                SetLTR();
                //ChangeAeroIcon();
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    if (vATDeclaration.d.NOTESSet.results.Count != 0)
                    {
                        viewModel.NoteList = vATDeclaration.d.NOTESSet.results.OrderBy(X => X.DataVersionz).ToList();
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