using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception
{
    public partial class SuccessView : BaseContentPage
    {
        TransactionReceptionViewModel viewModel;
        public SuccessView(string refNo)
        {
            viewModel = App.Locator.TransactionReceptionViewModel;
            BindingContext = viewModel;
            
            InitializeComponent();
            RefNoValue.Text = refNo;
        }

    }
}

