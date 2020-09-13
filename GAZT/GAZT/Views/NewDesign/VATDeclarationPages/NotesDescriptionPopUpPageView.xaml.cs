using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesDescriptionPopUpPageView : PopupPage
    {
        #region Variable
        public NotesDescriptionPopUpPageViewModel viewModel;
        #endregion
    
        #region Constructor
        public NotesDescriptionPopUpPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.NotesDescriptionPopUpPageView;
            BindingContext = viewModel;
            SetLTR();
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
    }
}