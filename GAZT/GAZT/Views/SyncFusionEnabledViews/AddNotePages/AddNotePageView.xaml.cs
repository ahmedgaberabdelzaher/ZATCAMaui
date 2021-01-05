using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddNotePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.AddNote
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePageView : ContentPage
    {
        #region Variable
        AddNotePageViewModel viewModel;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public AddNotePageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();
            ChangeAeroIcon();
            try
            {
                viewModel = App.Locator.AddNotePageView;
                this.BindingContext = viewModel;
                InitializeComponent();
              //  viewModel.NoteText = string.Empty;
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
                        if(App.ICRStatus == "E0001")
                        {
                            if (viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                            {
                                viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                viewModel.PreviousNoteText = viewModel.NoteText;
                            }
                        }
                    }
                    if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {
                        if (viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
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
                                    viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                    viewModel.PreviousNoteText = viewModel.NoteText;
                                }

                            }
                            else
                            {
                                viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                                viewModel.PreviousNoteText = viewModel.NoteText;
                            }
                        }
                    }
                    //if(App.ICRStatus=="E0045" && AddNotePageViewModel.NoteCount == 0)
                    //{
                    //    viewModel.NoteText = string.Empty;
                    //    AddNotePageViewModel.NoteCount++;
                    //}
                    //else
                    //{
                    //    if (App.ICRStatus == "E0045")
                    //    {
                    //        if (viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                    //        {
                    //            viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                    //            viewModel.PreviousNoteText = viewModel.NoteText;
                    //        }
                    //    }
                    //}
                    //if (App.ICRStatus == "E0006" && AddNotePageViewModel.NoteCount == 0)
                    //{
                    //    viewModel.NoteText = string.Empty;
                    //    AddNotePageViewModel.NoteCount++;
                    //}
                    //else
                    //{
                    //    if (App.ICRStatus == "E0006")
                    //    {
                    //        if (viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                    //        {
                    //            viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                    //            viewModel.PreviousNoteText = viewModel.NoteText;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
            }
        }
        #endregion
        #region Method
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
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion
    }
}