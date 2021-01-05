using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models.VATInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewNotesPopUpPageView : PopupPage
    {
        public ViewNotePopUpViewModel viewModel;

        public ViewNotesPopUpPageView(NotesSet notesSet)
        {
            InitializeComponent();
            //viewModel = App.Locator.NotesDescriptionPopUpPageView;
            //BindingContext = viewModel;
            //SetLTR();


            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            try
            {
                viewModel = App.Locator.ViewNotePopUpViewModel;
                BindingContext = viewModel;
                SetLTR();
                //ChangeAeroIcon();
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
                //viewModel.NoteList = new List<Note>();
                //for (int i=0;i<=5;i++)
                //{
                //    Note note = new Note();
                //    note.Strtime = "10:00:00";
                //    note.Strdt = "11-January-2020";
                //    note.Namez = "Ahmadh Khureshi";
                //    note.Sect = "Section:Test";
                //    note.Strline = "Registered in England. 125, Wharfedale Road, Winnersh Triangle, Wokingham RG41 5RB. Company No. 05872094. VAT No. GB 890 2687 92. Disclaimer: This e-mail and any files transmitted with it are confidential and intended solely for the use of the individual or entity to whom it is addressed. If you have received this e-mail in error, you must not copy, distribute or take any action in reliance on it.";
                //    viewModel.NoteList.Add(note);
                //}
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            }
            catch (Exception e)
            {
            }

        }

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
            catch (Exception ex)
            {

            }
        }
    }
}