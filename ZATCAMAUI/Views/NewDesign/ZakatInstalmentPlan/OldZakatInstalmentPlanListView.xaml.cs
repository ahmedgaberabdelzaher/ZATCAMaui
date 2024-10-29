using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{

    public partial class OldZakatInstalmentPlanListPageView : ContentPage
    {
        #region Variable
        OldZakatInstalmentPlanListViewModel viewModel;
        #endregion

        public OldZakatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();


                viewModel = App.Locator.OldZakatInstalmentPlanListPageView;
                BindingContext = viewModel;
                viewModel.ReqVatInstalmentPlanResponseList = null;

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
