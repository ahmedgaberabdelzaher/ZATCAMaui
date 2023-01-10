using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class NewCrPopupView : PopupPage
    {
        TransactionReceptionViewModel viewModel;

        public NewCrPopupView()
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

