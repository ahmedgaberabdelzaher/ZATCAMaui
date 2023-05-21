using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.ZakatyViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Zakaty
{
    public partial class AboutZakatyView : BaseContentPage
    {
        AboutZakatyViewModel viewModel;
        public AboutZakatyView()
        {
            viewModel = App.Locator.AboutZakatyViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

