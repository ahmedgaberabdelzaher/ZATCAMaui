
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddNotePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AddNotePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePageView : ContentPage
    {
        #region Variable
        AddNotePageViewModel viewModel;
        #endregion
        #region Constructor
        public AddNotePageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                NavigationPage.SetBackButtonTitle(this, "");
                NavigationPage.SetHasBackButton(this, false);
                On<iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();

                viewModel = App.Locator.AddNotePageView;
                BindingContext = viewModel;

                AddNotePageViewModel.IsComingFromNotePage = true;
                if (vATDeclaration != null && vATDeclaration != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                    if (App.ICRStatus == "E0001" && AddNotePageViewModel.NoteCount == 0)
                    {
                        viewModel.NoteText = string.Empty;
                        AddNotePageViewModel.NoteCount++;
                    }
                    else
                    {
                        if (App.ICRStatus == "E0001")
                        {
                            if (viewModel.VATDeclarationData.data.NOTESSet.Count != 0)
                            {
                                viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                viewModel.PreviousNoteText = viewModel.NoteText;
                            }
                        }
                    }
                    if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {
                        if (viewModel.VATDeclarationData.data.NOTESSet.Count != 0)
                        {
                            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                            {
                                if (VATReturnsPageViewModelEX.IsFirstTimeForNote == true)
                                {
                                    viewModel.NoteText = string.Empty;
                                    viewModel.PreviousNoteText = viewModel.NoteText;

                                }
                                else
                                {
                                    viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                    viewModel.PreviousNoteText = viewModel.NoteText;
                                }

                            }
                            else
                            {
                                viewModel.NoteText = viewModel.VATDeclarationData.data.NOTESSet.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                viewModel.PreviousNoteText = viewModel.NoteText;
                            }
                        }
                    }

                }
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