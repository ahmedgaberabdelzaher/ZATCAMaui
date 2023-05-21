using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages
{
    public partial class LaboratoryPaymentOfInsuranceFees : ContentPage
    {
        LaboratoryPaymentOfInsuranceFeesViewModel viewModel;
        public LaboratoryPaymentOfInsuranceFees()
        {
            viewModel = App.Locator.LaboratoryPaymentOfInsuranceFeesViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
