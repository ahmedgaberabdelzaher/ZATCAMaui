using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class TransactionReceptionView : BaseContentPage
    {
        TransactionReceptionViewModel viewModel;
        public TransactionReceptionView(string token)
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;
            viewModel.SetUserData(token);
            InitializeComponent();
        }
    }
}

