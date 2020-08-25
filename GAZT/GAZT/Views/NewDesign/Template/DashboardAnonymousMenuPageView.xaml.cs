using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Template
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardAnonymousMenuPageView : ContentPage
    {
        DashboardAnonymousMenuPageViewModel viewModel;
        public DashboardAnonymousMenuPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.DashboardAnonymousMenuPageView;
            BindingContext = viewModel;
            SetLTR();
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}