using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages
{
    public partial class TraifSections : ContentPage
    {
        TraifSectionsViewModel viewModel;
        public TraifSections()
        {
            viewModel = App.Locator.traifSectionsViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
       
        protected override bool OnBackButtonPressed()
        {
            viewModel.GoBack();
            if (viewModel.IsSectionView)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                return false;
            }
            
        }
    }
}
