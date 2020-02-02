using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalesDetailsPageView : ContentPage
    {

        #region Variable
        SalesDetailsPageViewModel viewModel;
        ZakatReturnDetailsD ZakatReturnDetail = null;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public SalesDetailsPageView(ZakatReturnDetails ZakatReturnDetail)
        {
            InitializeComponent();
            viewModel = App.Locator.SalesDetailsPageView;
            viewModel.zakatReturnDetailsD = ZakatReturnDetail;
            SetLTR();
            this.BindingContext = viewModel;
            viewModel.onPageLoad();
            viewModel.ZakatReturnDetail = ZakatReturnDetail;
            SalesDetails.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                if (sender is ListView lv) lv.SelectedItem = null;
            };
        }

        #endregion

        #region Method
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (AmendSalesDetailsPageViewModel.SelectedSalesDetails != null && AmendSalesDetailsPageViewModel.SelectedSalesDetails.ComingFromAmendEditMode)
                {
                    SetUpdatedDataToObject();
                }
            }
            catch(Exception ex)
            {

            }
            
            

        }

        private void SetUpdatedDataToObject()
        {
            try
            {
                if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("1"))
                {
                    viewModel.zakatReturnDetailsD.d.TvtslE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("2"))
                {
                    viewModel.zakatReturnDetailsD.d.LabnoE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("3"))
                {
                    viewModel.zakatReturnDetailsD.d.ImpvalE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("4"))
                {
                    viewModel.zakatReturnDetailsD.d.TvtslResn = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("5"))
                {
                    viewModel.zakatReturnDetailsD.d.Estsl = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("6"))
                {
                    viewModel.zakatReturnDetailsD.d.ExamtI = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("7"))
                {
                    viewModel.zakatReturnDetailsD.d.PramtE = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
                else if (AmendSalesDetailsPageViewModel.SelectedSalesDetails.SelectedEditFieldId.Equals("8"))
                {
                    // Missing need to check and assign the value
                    ///viewModel.zakatReturnDetailsD.TvtslResn = AmendSalesDetailsPageViewModel.SelectedSalesDetails.NewValue;
                }
            }
            catch (Exception ex)
            {

            }
            

        }
        protected void OnCheckBoxCheckedChanged(Object sender, EventArgs e)
        {
            checkBox.IsChecked = !checkBox.IsChecked;
            viewModel.CheckBoxStatus = checkBox.IsChecked;
        }

        #endregion


    }
}