using EGAZT.Models.ChangeFillingPeriodModel;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriod
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodPageView : ContentPage
    {

        #region Variable
        ChangeFillingPeriodViewModel viewModel;
        #endregion

        public ChangeFillingPeriodPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                this.BindingContext = viewModel;


            }
            catch (Exception ex)
            {

            }
        }



        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ChangeFillingPeriodModel selectedItem = e.AddedItems[0] as ChangeFillingPeriodModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);

            viewModel.ShowAttachments = true;

            try
            {

                switch (viewModel.OutletDecisionOptions.IndexOf(selectedItem))
                {
                    case 0:
                        {

                            viewModel.SelectedAttachmentText = "Attachment - 2 years monthly return";
                            return;
                        }
                    case 1:
                        {
                            viewModel.SelectedAttachmentText = "Attachment - 12 Months Taxable Revenue";
                            return;
                        }
                    case 2:
                        {
                            viewModel.SelectedAttachmentText = "Attachment - Other Document";
                            return;
                        }

                }
            }
            catch (Exception ex)
            {

            }

            }

            private void SummarybtnContinue_Clicked(object sender, EventArgs e)
        {
            viewModel.EnableMyRequestsView();
            Navigation.PushAsync(new ChangeFillingPeriodSuccessPage());
        }
    }
}