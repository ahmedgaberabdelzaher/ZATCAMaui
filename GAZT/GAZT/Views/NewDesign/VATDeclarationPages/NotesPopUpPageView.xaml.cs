using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
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
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                SetLTR();
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
                                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
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
            return true ;
        }
        protected override bool OnBackgroundClicked()
        {
            return true;
        }
        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<Object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
                PopupNavigation.Instance.PopAsync();
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