
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Models;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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
            On<iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.DisplayNotesPageView;
                BindingContext = viewModel;
                ChangeAeroIcon();
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    if (vATDeclaration.d.NOTESSet.results.Count != 0)
                    {
                        viewModel.NoteList = vATDeclaration.d.NOTESSet.results;
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
        #region Method
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
        #endregion
    }
}