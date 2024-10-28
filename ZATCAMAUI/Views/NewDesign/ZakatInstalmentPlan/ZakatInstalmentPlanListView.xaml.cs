
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{

    public partial class ZakatInstalmentPlanListPageView : ContentPage
    {



        #region Variable
        ZakatInstalmentPlanListViewModel viewModel;


        #endregion

        public ZakatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();


                viewModel = App.Locator.ZakatInstalmentPlanListPageView;
                BindingContext = viewModel;

                _ = viewModel.onPageLoad();

            }
            catch (Exception)
            {


            }

        }
     
     
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                viewModel.ResetData();
                viewModel.EnableCreateZakatInstalment();
                await viewModel.GetZakatInstalmentPlanList();
            }
            catch (Exception)
            {

            }
        }


       
        void OtpFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }

    }
}
