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