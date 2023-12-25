using System;
using System.Collections.Generic;
using EGAZT.AppConfigurations;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.InquiryaboutCustomsIssuesViews
{	
	public partial class InquiryaboutCustomsIssuesView : ContentPage
    {

		InquiryaboutCustomsIssuesViewModel viewModel;
        public InquiryaboutCustomsIssuesView ()
		{
            viewModel = App.Locator.InquiryaboutCustomsIssuesViewModel;
            BindingContext = viewModel;
        var url= PageSettings.GetCustomsIssueUrl();

            InitializeComponent ();
            webc.Source = url;
        }

        void BackTapped(System.Object sender, System.EventArgs e)
        {
             if (webc.CanGoBack)
         {
             webc.GoBack();
                var url = PageSettings.GetCustomsIssueUrl();
                webc.Source = url;
                return;
         }
         viewModel._navigationService.GoBack();
        }
    }
}

