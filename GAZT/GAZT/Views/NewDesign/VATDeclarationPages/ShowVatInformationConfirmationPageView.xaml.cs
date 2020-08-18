using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
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
    public partial class ShowVatInformationConfirmationPageView : PopupPage
    {
        public ShowVatInformationConfirmationPageViewModel viewModel;
        public ShowVatInformationConfirmationPageView(NewDesignPopUp newDesignPopData)
        {
            InitializeComponent();
            viewModel = App.Locator.ShowVatInformationConfirmationPageView;
            this.BindingContext = viewModel;
            SetLTR();
        }

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

        private void YesButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "YesReceived", "Yes");
        }

        private void NoButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<Object, string>(this, "NoReceived", "No");
        }
    }
}