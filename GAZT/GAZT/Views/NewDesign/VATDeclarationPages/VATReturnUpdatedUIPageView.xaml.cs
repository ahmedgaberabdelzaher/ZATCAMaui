using EGAZT.ViewModel.NewDesignViewModel;
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
    public partial class VATReturnUpdatedUIPageView : ContentPage
    {
        #region Variable
        public VATReturnUpdatedUIPageViewModel viewModel;
        #endregion

        #region Constructor
        public VATReturnUpdatedUIPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATReturnUpdatedUIPageView;
            this.BindingContext = viewModel;
            SetLTR();

        }

        #endregion

        #region Method
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