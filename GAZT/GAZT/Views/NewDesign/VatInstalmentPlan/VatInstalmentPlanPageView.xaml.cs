using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.VATInstalationModels;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using VATResults4 = EGAZT.Models.VATInstalationModels.VATResults4;

namespace EGAZT.Views.NewDesign.VatInstalmentPlan
{
    public partial class VatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        VATInstalmentPlanViewModel viewModel;

        double totalAmount = 0.0;
        #endregion

        public VatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.VatInstalmentPlanPageView;
                this.BindingContext = viewModel;

                viewModel.ResetData();
                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    await GetVAtInstalmentData();

                });



            }
            catch (Exception ex)
            {

            }





        }

        public async Task GetVAtInstalmentData()
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
                if (viewModel.InputData.Length > 0)
                {
                    var itemsSource = viewModel.SelectedBillsList.Where(w => w.SadadNo.Contains(viewModel.InputData)).ToList();
                    BillsVATListVIew.ItemsSource = itemsSource;


                    for (int i = 0; i < itemsSource.Count; i++)
                    {
                        var dataItem = itemsSource[i] as VATResults4;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = itemsSource[i];
                        }

                    }


                }
                else
                {
                    BillsVATListVIew.ItemsSource = viewModel.SelectedBillsList;


                    for (int i = 0; i < viewModel.SelectedBillsList.Count; i++)
                    {
                        var dataItem = viewModel.SelectedBillsList[i] as VATResults4;
                        if (viewModel.selectedList.Contains(dataItem))
                        {
                            BillsVATListVIew.SelectedItem = viewModel.SelectedBillsList[i];
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var dataItem = e.ItemData as VATResults4;




            totalAmount = 0.0;



            try
            {
                if (viewModel.selectedList.Count > 0)
                {
                    if (viewModel.selectedList.Contains(dataItem))
                    {
                        // totalAmount -= Convert.ToDouble(dataItem.Betrh);
                        viewModel.selectedList.Remove(dataItem);
                    }
                    else
                    {
                        viewModel.selectedList.Add(dataItem);
                    }





                }
                else
                {
                    viewModel.selectedList.Add(dataItem);
                    //viewModel.BillsListVAT[0].Xsele = "X";
                    // totalAmount += Convert.ToDouble(dataItem.Betrh);
                    // viewModel.TotalAmountSAR = string.Format("{0:N2}", totalAmount) + " " + dataItem.Waers;
                }



                Double dueAmount = 0.0;
                for (int i = 0; i < viewModel.selectedList.Count; i++)
                {
                    if (viewModel.selectedList[i] != null)
                    {
                        dueAmount = dueAmount + Convert.ToDouble(viewModel.selectedList[i].Betrh);
                    }
                    else
                    {
                        viewModel.selectedList.Remove(viewModel.selectedList[i]);
                    }



                }




                viewModel.TotalAmountSAR = string.Format("{0:N2}", dueAmount) + " " + dataItem.Waers;



                viewModel.EnableBillsContinue();

            }


            catch (Exception ex)
            {


            }
        }
        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            // PopupNavigation.Instance.PushAsync(new FilesUploadPopUpPageView(viewModel.VatInstalments.d.AttachmentSet.results,WhichAttachment.VATInstalment,viewModel.VatInstalments.d.ReturnIdz));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, Boolean>(this, "TermsContinue");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, Boolean>(this, "TermsContinueSecond");
            MessagingCenter.Unsubscribe<object, Boolean>(this, "InstructionsContinue");
        }
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                viewModel.FirstTerms = false;
                viewModel.SecondTerms = false;


                Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {

                        //viewModel.VatInstalments.d.AttachmentSet.results = arg.results;
                        viewModel.PopulateAttachments(arg.results);


                    }
                });
                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "TermsContinueSecond", (sender, arg) =>
                {
                    if (arg != null && arg == true)
                    {
                        viewModel.SecondTerms = true;
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "InstructionsContinue", (sender, arg) =>
                {
                    if (arg != null && arg == true)
                    {
                        viewModel.FirstTerms = true;
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, Boolean>(this, "TermsContinue", (sender, arg) =>
                {
                    if (arg != null)
                    {

                        //viewModel.EnableSucessScreenAsync();
                    }
                });
              


                if (Device.RuntimePlatform == Device.iOS)
                {
                    //iOS stuff
                    BillsVATListVIew.IsScrollingEnabled = false;
                    StatementListView.IsScrollingEnabled = false;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {

                }

            }
            catch (Exception ex)
            {
            }
        }



    }
}
