using System;
using System.Collections.Generic;
using EGAZT.AppConfigurations;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class CreateE_Declaration : ContentPage
    {
        E_DeclerationViewModel viewModel;

        public CreateE_Declaration(string title)
        {
            viewModel = App.Locator.eDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            header.TitleText = title;
            if (title == AppResources.eDeclaration)
            {
                wbview.Source = "https://pre-eservices.zatca.gov.sa/sites/sc/ar/pages/Home.aspx";
            }
            else if (title == AppResources.Transactiondescription)
            {
                wbview.Source = PageSettings.GetTawreedLinks();
            }
            else if (title == AppResources.CustomFeesCalculator)
            {
                wbview.Source = PageSettings.GetCustomFeesCalcLink();
            }
        }

        void wbview_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            
        }
    }
}
