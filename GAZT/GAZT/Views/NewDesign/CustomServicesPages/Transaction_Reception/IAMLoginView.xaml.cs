using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class IAMLoginView : BaseContentPage
    {
        IAMLoginViewModel viewModel;
        public IAMLoginView()
        {
             viewModel = App.Locator.IAMLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }     
    }
}

