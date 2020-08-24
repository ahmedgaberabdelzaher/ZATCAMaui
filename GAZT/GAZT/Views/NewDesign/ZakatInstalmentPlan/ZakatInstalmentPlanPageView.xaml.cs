using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    public partial class ZakatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        ZakatInstalmentPlanViewModel viewModel;

        double totalAmount = 0.0;
        #endregion

        public ZakatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanPageView;
                this.BindingContext = viewModel;

                viewModel.showInstructionsDialog();
                 GetZakatInstalmentData();
            }
            catch (Exception ex)
            {

            }





        }

        public async Task GetZakatInstalmentData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.OnPageLoad();

                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
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

        private void Frequncy_Selected(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selectedItem = e.ItemData as InstalmentAgreementFrequencyModel;
            viewModel.updateInstalmentsOnSlider(selectedItem);
        }
        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ZakatInstalmentPlanModel selectedItem = e.AddedItems[0] as ZakatInstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            //            viewModel.ReasonContinueBtnClicked();


            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.IsZakatSelected = true;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.IsVATAmountVisible = false;
            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = true;
                viewModel.IsVATAmountVisible = false;
            }
            else
            //if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 2)
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.IsSubIncomeTaxViewEnabled = false;
                viewModel.IsVATAmountVisible = true;
                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    //await GetVAtInstalmentData();

                });



                //PopupNavigation.Instance.PushAsync(new ZakatInstalmentPlanBottomPopup());
            }
            /* else
             {
                 viewModel.IsZakatSelected = true;
                 viewModel.IsIncomeTaxViewEnabled = true;
                 viewModel.IsVATAmountVisible = false;

             }*/



        }
        //private void BPickerButtonZakat_Clicked(object sender, EventArgs e)
        //{
        //    FZakatPicker.IsOpen = true;
        //}

        //private void FPickerZakat_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    CorrespondenceFiltersModel selectedZakat = (CorrespondenceFiltersModel)e.NewValue;
        //    FZakatPicker.SelectedItem = selectedZakat;
        //    viewModel.SelectedFilterZakat = selectedZakat;//selectedregion
        //    viewModel.SelectedFilterZakatPrev = selectedZakat;//selectedregion
        //    viewModel.TxtSelectedStatusZakat = selectedZakat.Filter;
        //    viewModel.IsSubIncomeTaxViewEnabled = true;
        //}
        //private void FPickerZakat_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        FZakatPicker.SelectedItem = viewModel.SelectedFilterZakatPrev;
        //        viewModel.SelectedFilterZakat = viewModel.SelectedFilterZakatPrev;//selectedregion
        //        if (viewModel.SelectedFilterZakatPrev == null)
        //        {
        //            viewModel.TxtSelectedStatusZakat = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        //private void SubBPickerButtonZakat_Clicked(object sender, EventArgs e)
        //{
        //    SubFZakatPicker.IsOpen = true;
        //}

        //private void SubFPickerZakat_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    CorrespondenceFiltersModel selectedZakat = (CorrespondenceFiltersModel)e.NewValue;
        //    SubFZakatPicker.SelectedItem = selectedZakat;
        //    viewModel.SubSelectedFilterZakat = selectedZakat;//selectedregion
        //    viewModel.SubSelectedFilterZakatPrev = selectedZakat;//selectedregion
        //    viewModel.SubTxtSelectedStatusZakat = selectedZakat.Filter;
        //}
        //private void SubFPickerZakat_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        SubFZakatPicker.SelectedItem = viewModel.SubSelectedFilterZakatPrev;
        //        viewModel.SubSelectedFilterZakat = viewModel.SubSelectedFilterZakatPrev;//selectedregion
        //        if (viewModel.SelectedFilterZakatPrev == null)
        //        {
        //            viewModel.SubTxtSelectedStatusZakat = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        private void DownPayment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            viewModel.DownPaymentAmount = args.NewValue;


        }

        private void Installment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            var newVal = args.NewValue;
            viewModel.NoOfInstalments = Convert.ToInt32(newVal);


        }
        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {


            try
            {
                /*if (SearchItemVAT.Text.Length > 0)
                {*/

                /*
                var itemsSource = viewModel.BillsListVAT.Where(w => w.SadadNo.Contains(SearchItemVAT.Text));
                  BillsVATListVIew.ItemsSource = itemsSource;
                  */


                /*}
                else
                {
                    BillsVATListVIew.ItemsSource = viewModel.BillsListVAT;
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




        }

        private void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
          /*  var dataItem = e.ItemData as EGAZT.Models.ZakatInstalationModels.VATResults4;

            try
            {
                if (viewModel.selectedList.Count > 0)
                {



                    foreach (VATResults4 listItem in viewModel.selectedList)
                    {
                        if (viewModel.selectedList.Contains(listItem))
                        {
                            int index = viewModel.BillsListVAT.IndexOf(listItem);

                            viewModel.BillsListVAT[index].Xsele = "";
                            totalAmount -= Convert.ToDouble(dataItem.Betrh);
                            viewModel.selectedList.Remove(listItem);


                            viewModel.TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " " + dataItem.Waers;
                        }
                        else
                        {
                            int index = viewModel.BillsListVAT.IndexOf(listItem);
                            viewModel.BillsListVAT[index].Xsele = "X";
                            viewModel.selectedList.Add(dataItem);
                            totalAmount += Convert.ToDouble(dataItem.Betrh);
                            viewModel.TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " " + dataItem.Waers;
                        }

                    }

                }
                else
                {
                    viewModel.selectedList.Add(dataItem);
                    viewModel.BillsListVAT[0].Xsele = "X";
                    totalAmount += Convert.ToDouble(dataItem.Betrh);
                    viewModel.TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " " + dataItem.Waers;
                }




            }
            catch (Exception ex)
            {



            }
          */
        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            // PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(viewModel.VatInstalments.d.AttachmentSet.results,WhichAttachment.VATInstalment,viewModel.VatInstalments.d.ReturnIdz));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {

                       // viewModel.VatInstalments.d.AttachmentSet.results = arg.results;
                        viewModel.PopulateAttachments(arg.results);


                    }
                });

            }
            catch (Exception ex)
            {
            }
        }



    }
}
