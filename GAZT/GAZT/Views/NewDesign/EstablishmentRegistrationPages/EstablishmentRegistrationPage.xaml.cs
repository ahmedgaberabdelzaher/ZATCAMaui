using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentRegistrationPage : ContentPage
    {
        EstablishmentRegistrationPageViewModel viewModel;
        public EstablishmentRegistrationPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.EstablishmentRegistrationPage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ListView_Correspondance_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ETaxableIncomeSourceTypeListModel taxableIncomeSourceTypeSelected = ((Xamarin.Forms.ListView)sender).SelectedItem as ETaxableIncomeSourceTypeListModel;

            if (taxableIncomeSourceTypeSelected.IsSelectedType == false)
            {
                taxableIncomeSourceTypeSelected.IsSelectedType = true;

            }
         //  viewModel.ShowCorrespondenceDetails(Correspondence);
         ((Xamarin.Forms.ListView)sender).SelectedItem = null;
        }

        private void TappedOnicon(object sender, EventArgs e)
        {

        }
    }
}
