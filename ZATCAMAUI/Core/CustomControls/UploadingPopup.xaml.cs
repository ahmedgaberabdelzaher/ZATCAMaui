using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport;

namespace ZATCAMAUI.Controls
{
    public partial class UploadingPopup : PopupPage
    {
        SubmitReportViewModel viewModel;
        public UploadingPopup()
        {
            InitializeComponent();
            viewModel = App.Locator.SubmitReportViewModel;
            BindingContext = viewModel;
        }

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return true;
        }
    }
}

