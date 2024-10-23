using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndividualRegistrationPageView : ContentPage
    {

        IndividualRegistrationPageViewModel viewModel;
        

        public IndividualRegistrationPageView(string isGulf)
        {
            try
            {


                InitializeComponent();

                viewModel = App.Locator.IndividualRegistrationPageView;
                BindingContext = viewModel;

                if (isGulf == "Gulf")
                {
                    viewModel.IsGulfER = true;
                    viewModel.IsCitizen = false;
                }
                else if (isGulf == "RegisterPageSSO")
                {
                    viewModel.IsGulfER = false;
                    viewModel.IsCitizen = true;
                }
                else
                {
                    viewModel.IsGulfER = true;
                    viewModel.IsCitizen = false;
                }


            }
            catch (Exception)
            {

            }
        }


        #region

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.StopTimer = false;
            viewModel.TotalSec = -10;
        }

       

        private void PickerBtn_Region_Clicked(object sender, TappedEventArgs e)
        {
            Picker_Region.IsOpen = true;

        }

        private void PickerBtn_City_Clicked(object sender, TappedEventArgs e)
        {
            Picker_City.IsOpen = true;
        }

        private void GCCPickerBtn_Country_Clicked(object sender, TappedEventArgs e)
        {
            GCCPicker_Country.IsOpen = true;
        }
        

        void btnDate_Clicked(object sender, TappedEventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                SignUpDOBHijri.IsOpen = true;
            }
            else
            {
                SignUpDOB.IsOpen = true;


            }
        }

        #endregion

    }
}