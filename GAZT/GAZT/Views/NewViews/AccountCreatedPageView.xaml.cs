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
    public partial class AccountCreatedPageView : ContentPage
    {
        AccountCreatedPageViewModel viewModel;
        public AccountCreatedPageView()
        {
            viewModel = App.Locator.AccountCreatedPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
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