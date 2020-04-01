using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
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
            NavigationPage.SetBackButtonTitle(this, "");
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

                    if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
                    {
                        if (viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                        {
                            viewModel.NoteText = viewModel.VATDeclarationData.d.NOTESSet.results.Where(x => x.DataVersionz == "00000").Select(x => x.Strline).FirstOrDefault();
                        }
                    }
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