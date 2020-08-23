using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RefundAccountPopupPageView : PopupPage
    {
        public RefundAccountPopupPageViewModel viewModel;
        public RefundAccountPopupPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.RefundAccountPopupPageView;
                this.BindingContext = viewModel;
                SetLTR();

                if(vATDeclaration!=null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationDetails = vATDeclaration;
                    onPageLoad();
                    viewModel.NewAccountText = AppResources.ZTERNewAccount;
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void ValidationsForVATRefund()
        {
            bool resultForBilledOrNot = false;
           



                //if (viewModel.IsVisibleDropdownForRefund == true)
                //{
                //    if (viewModel.IsCheckedRefund == true)
                //    {
                //        if (!string.IsNullOrEmpty(viewModel.IbanNumberText) && viewModel.SelectedIBANType != null && viewModel.SelectedIBANIDNumber != null && viewModel.IsIBANValid == true)
                //        {
                //            if (!resultForBilledOrNot)
                //            {
                //                viewModel.IsMainButtonEnabled = true;
                //            }
                //            else
                //            {
                //                viewModel.IsMainButtonEnabled = false;
                //            }
                //        }
                //        else
                //        {
                //            viewModel.IsMainButtonEnabled = false;
                //        }
                //    }
                //    else
                //    {
                //        if (!string.IsNullOrEmpty(viewModel.TxtSelectedIBAN) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && !string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber))
                //        {
                //            if (!resultForBilledOrNot)
                //            {
                //                viewModel.IsMainButtonEnabled = true;
                //            }
                //            else
                //            {
                //                viewModel.IsMainButtonEnabled = false;
                //            }
                //        }
                //        else
                //        {
                //            viewModel.IsMainButtonEnabled = false;
                //        }
                //    }
                //}
        }
        protected async override void OnAppearing()
        {
            getIban();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceivedVATDeclaration");
        }
        public async void getIban()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "IbanReceivedVATDeclaration", async (sender, arg) =>
                {
                    await PopupNavigation.Instance.PopAsync();
                    string message = string.Empty;
                    if (arg != null)
                    {
                        message = arg;
                        // firebasemessage = JsonConvert.DeserializeObject<PushnotificationMessage>(arg);
                        if (message == "SA")
                        {
                            if (viewModel.IBANList != null)
                            {
                                viewModel.IBANList.Clear();
                            }
                            viewModel.IBANList = null;
                            viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.d.IBANSet.results);
                           // viewModel.VATDeclarationDetails.d.OptIban = String.Empty;
                            viewModel.NewAccountText = AppResources.ZTERNewAccount;
                        }
                        else
                        {
                            triggerIban(message);
                        }
                    }

                    // await viewModel.VATSetReturnVoidAsync();

                });
            }
            catch (Exception ex)
            {

            }
        }
        public void triggerIban(string messagestring)
        {
            try
            {
                string message = messagestring;
                if (!string.IsNullOrEmpty(message))
                {
                    bool isExist = false;
                    if (!string.IsNullOrEmpty(message))
                    {
                        List<Result2> results1D = new List<Result2>();
                        foreach (var item in viewModel.IBANList)
                        {
                            Result2 result = new Result2();
                            result = item;

                            if (string.IsNullOrEmpty(item.Bkvid))
                            {
                                isExist = true;
                                result.Iban = message;
                                viewModel.VATDeclarationDetails.d.Iban = message;
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                            }
                            results1D.Add(result);
                        }

                        if (results1D != null && results1D.Count != 0)
                        {
                            if (viewModel.IBANList != null)
                            {
                                viewModel.IBANList.Clear();
                            }
                            viewModel.IBANList = null;
                            viewModel.IBANList = new ObservableCollection<Result2>(results1D);
                        }


                        if (!isExist)
                        {
                            viewModel.VATDeclarationDetails.d.Iban = message;
                            Result2 result2 = new Result2();
                            result2.Iban = message;
                            List<Result2> results = new List<Result2>();
                            results.Add(result2);
                            viewModel.IBANList = new ObservableCollection<Result2>(results);
                            viewModel.NewAccountText = AppResources.VATREditAccount;
                        }
                    }
                    //                viewModel.IsNewAccountClicked = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }
        public void onPageLoad()
        {
            viewModel.createIBANType();
            if (viewModel.VATDeclarationDetails.d.IBANSet.results != null && viewModel.VATDeclarationDetails.d.IBANSet.results.Count() != 0)
            {
                viewModel.IBANList = new ObservableCollection<Result2>();
                viewModel.IBANList = new ObservableCollection<Result2>(viewModel.VATDeclarationDetails.d.IBANSet.results);
                viewModel.IsVATRefunCheckedVisible = false;
                viewModel.IsEnableCheckedRefund = false;
                viewModel.SelectedIBAN= viewModel.IBANList.FirstOrDefault();
            }
            else
            {
                viewModel.IsVATRefunCheckedVisible = true;
                viewModel.IsEnableCheckedRefund = true;
            }
        }
        public void ManageValidations()
        {
            //Est flag = "A"--Enable
            if(viewModel.VATDeclarationDetails.d.IBANSet!=null && viewModel.VATDeclarationDetails.d.IBANSet.results.Count!= 0)
            {
                viewModel.IsNewAccountButtonVisible = false;
            }
            else
            {
                viewModel.IsNewAccountButtonVisible = true;
            }
         }

        public bool CheckValidationsForSubmitButton()
        {
            bool result = false;
            if(viewModel.IsNewAccountButtonVisible)
            {
                if(!string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && viewModel.SelectedIban!=null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.TxtSelectedIBANIDNumber) && string.IsNullOrEmpty(viewModel.TxtSelectedIBANType) && viewModel.SelectedIban != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
        private async void IDTypeDropdown_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANType selectedIBANType = (IBANType)e.NewValue;
            IDTypeDropdown.SelectedItem = selectedIBANType;
            viewModel.SelectedIBANType = selectedIBANType;
            viewModel.SelectedIBANTypePrev = selectedIBANType;
            viewModel.TxtSelectedIBANType = selectedIBANType.Text;
            await viewModel.SetIBANIdNumber();
            //if (viewModel.IsVisibleSummary == true)
            //{
            //    if (viewModel.IsVisibleDropdownForRefund == true)
            //    {
            //        ValidationsForVATRefund();
            //    }
            //}
        }
        private void IDNumberDropdown_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
            IDNumberDropdown.SelectedItem = selectedIBANIDNumber;
            viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
            viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
            viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
            //if (viewModel.IsVisibleSummary == true)
            //{
            //    if (viewModel.IsVisibleDropdownForRefund == true)
            //    {
            //        ValidationsForVATRefund();
            //    }
            //}
        }
        private void IDNumberDropdown_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IDNumberDropdown.SelectedItem = viewModel.SelectedIBANIDNumberPrev;
            viewModel.SelectedIBANIDNumber = viewModel.SelectedIBANIDNumberPrev;
            if (viewModel.SelectedIBANIDNumberPrev == null)
            {
                viewModel.TxtSelectedIBANIDNumber = string.Empty;
            }
        }
        private void IDTypeDropdown_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            IDTypeDropdown.SelectedItem = viewModel.SelectedIBANTypePrev;
            viewModel.SelectedIBANType = viewModel.SelectedIBANTypePrev;
            //if (viewModel.SelectedIBANTypePrev == null)
            //{
            //    viewModel.TxtSelectedIBANType = string.Empty;
            //}
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopPage(viewModel.VATDeclarationDetails.d.Iban));
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            IDTypeDropdown.IsOpen = true;
        }

        private void SfButton_Clicked1(object sender, EventArgs e)
        {
            IDNumberDropdown.IsOpen = true;
        }

        private void IDNumber_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                IBANIDNumber selectedIBANIDNumber = (IBANIDNumber)e.NewValue;
                if (selectedIBANIDNumber != null)
                {
                    IDNumberDropdown.SelectedItem = selectedIBANIDNumber;
                    viewModel.SelectedIBANIDNumber = selectedIBANIDNumber;
                    viewModel.SelectedIBANIDNumberPrev = selectedIBANIDNumber;
                    viewModel.TxtSelectedIBANIDNumber = selectedIBANIDNumber.Idnumber;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async void IDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                IBANType selectedIBANType = (IBANType)e.NewValue;
                if (selectedIBANType != null)
                {
                    IDTypeDropdown.SelectedItem = selectedIBANType;
                    viewModel.SelectedIBANType = selectedIBANType;
                    viewModel.SelectedIBANTypePrev = selectedIBANType;
                    viewModel.TxtSelectedIBANType = selectedIBANType.Text;
                    await viewModel.SetIBANIdNumber();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void Confirm_RefundClicked(object sender, EventArgs e)
        {
            if(CheckValidationsForSubmitButton())
            {

            }
        }
    }
}