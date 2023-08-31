using System;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace EGAZT.Views.NewDesign.CustomServicesPages.CustomFees
{
    public partial class CustomFeesFormView : BaseContentPage
    {
        CustomFeesFormViewModel viewModel;
        public CustomFeesFormView ()
		{
			try
			{
                viewModel = App.Locator.CustomFeesFormViewModel;
                BindingContext = viewModel;
                
                InitializeComponent();
            }
			catch (Exception ex)
			{

			}
			
		}
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

