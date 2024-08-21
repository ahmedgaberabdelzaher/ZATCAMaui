using Mopups.Pages;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPopUpPageView : PopupPage
    {
        #region Variable
        public NotesPopUpPageViewModel viewModel;
        #endregion

        #region Constructor
        public NotesPopUpPageView(VATDeclaration vATDeclaration)
        {

            try
            {
                InitializeComponent();
                viewModel = App.Locator.NotesPopUpPageView;
                BindingContext = viewModel;
                NavigationPage.SetBackButtonTitle(this, "");
                NavigationPage.SetHasBackButton(this, false);
                ChangeAeroIcon();
                //  viewModel.NoteText = string.Empty;
                NotesPopUpPageViewModel.IsComingFromNotePage = true;
                viewModel.NoteText = string.Empty;
                if (vATDeclaration != null && vATDeclaration != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                    if (App.ICRStatus == "E0001" && NotesPopUpPageViewModel.NoteCount == 0)
                    {
                        viewModel.NoteText = string.Empty;
                        NotesPopUpPageViewModel.NoteCount++;
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
                                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
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

        #region Methods

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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override bool OnBackgroundClicked()
        {
            return true;
        }
        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
                MopupService.Instance.PopAsync();
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