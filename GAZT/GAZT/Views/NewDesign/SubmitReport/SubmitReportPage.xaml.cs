using System;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.SubmitReport
{
    public partial class SubmitReportPage : ContentPage
    {
        SubmitReportViewModel viewModel;
        public SubmitReportPage()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.submitReportViewModel;
                BindingContext = viewModel;
            }
            catch(Exception ex)
            {

            }
            

        }

        protected override async void OnAppearing()
        {
            try
            {
                var statusLocationWhenInUse = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                var statusLocationAlways = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();

                if (statusLocationAlways != PermissionStatus.Granted || statusLocationWhenInUse != PermissionStatus.Granted)
                {
                    await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            viewModel.SubmitReport.HasViolationDateError = false;
        }

        private void reportReportDetailsEntry_Focused(object sender, FocusEventArgs e)
        {
            //var element = (GAZTBorderlessEditor)sender;
            //if (element != null && string.IsNullOrWhiteSpace(element.Text))
            //{
            //    Device.BeginInvokeOnMainThread(async () => {
            //        await Task.Delay(1000);
            //        element.Unfocus();
            //    });
            //}
        }
    }
}

