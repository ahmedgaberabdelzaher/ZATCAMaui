using System;
using System.Collections.Generic;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.HomePages
{
    public partial class ContactUs : BaseContentPage
    {
        ContactUsPageViewModel viewModel;
        public ContactUs()
        {
            viewModel = App.Locator.ContactUsPageView;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
