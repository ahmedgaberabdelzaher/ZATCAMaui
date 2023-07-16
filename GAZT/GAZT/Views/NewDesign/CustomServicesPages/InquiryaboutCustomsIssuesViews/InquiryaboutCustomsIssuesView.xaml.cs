using System;
using System.Collections.Generic;
using EGAZT.AppConfigurations;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.InquiryaboutCustomsIssuesViews
{	
	public partial class InquiryaboutCustomsIssuesView : BaseContentPage
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
	}
}

