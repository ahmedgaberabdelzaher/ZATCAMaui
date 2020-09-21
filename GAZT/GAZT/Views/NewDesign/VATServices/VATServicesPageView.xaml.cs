using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATServices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATServicesPageView : ContentPage
    {
        #region Variable
        GAZTNewDesignDashBoardPageViewModel viewModel;
        #endregion
        public VATServicesPageView()
        {
            InitializeComponent();
        }
    }
}