using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ContractReleaseViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ContractRelease
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleaseInfoPopup : PopupPage
    {
        public ContractReleaseInfoPopup(ContractReleaseViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = viewModel;
        }
    }
}