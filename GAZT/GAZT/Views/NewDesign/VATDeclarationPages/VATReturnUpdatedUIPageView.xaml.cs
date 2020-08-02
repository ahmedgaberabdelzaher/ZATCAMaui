using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
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
    public partial class GAZTNewDesignVATReturnUpdatedUIPageView : ContentPage
    {
        #region Variable
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel viewModel;
        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignVATReturnUpdatedUIPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            App.IsArabic = false;
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
        #endregion

        private void btnprimary_Clicked(object sender, EventArgs e)
        {

        }

        private void OnStandardRatedTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView());
        }

        private void OnDomesticRatedTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AdjustmentPopupPageView());
        }
    }
}