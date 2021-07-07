using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.Models.IBanManagementListModel;

namespace EGAZT.Views.NewDesign.IBanAccountsManagements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class BankAccountAddorUpdateIBANPageView : ContentPage
    {
        private BankAccountAddorUpdateIBANViewModel _viewModel;
        public BankAccountAddorUpdateIBANPageView(IBanAccountManagementResponseModel IBANAccountData)
        {
            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            _viewModel = App.Locator.BankAccountAddOrUpdatePageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;

            _viewModel.IBANAccountData = IBANAccountData;
            AckText.Text = string.Format(AppResources.NDIBANCertifyAck, App.LoginDataRetrieved.NameOrg1);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            //Check for Large Tax payer or not


            if (string.IsNullOrEmpty(App.SelectedIBAN)) {

                _viewModel.SelectedIDType = "";
                _viewModel.SelectedBankName = "";
                _viewModel.SelectedIDNumber = "";
                _viewModel.AccountOwnerName = "";
                _viewModel.IBANValue = "";
            }
            else {


                var selectedIBAN = _viewModel.IBANAccountData.d.IbanListSet.results.Find(selectedValue => (selectedValue.Fbnum == App.SelectedIBAN));

                if (selectedIBAN != null)
                {
                    _viewModel.SelectedIDType = selectedIBAN.IdtypeDesc;
                    _viewModel.SelectedBankName = selectedIBAN.Bkext;
                    _viewModel.SelectedIDNumber = selectedIBAN.Idnumber;
                    _viewModel.AccountOwnerName = selectedIBAN.Koinh;
                    _viewModel.IBANValue = selectedIBAN.Iban;
                }

             
            }


         

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                _viewModel.PickerModel = arg;

                if(arg.PickerId == "IBANIdTypePicker")
                {
                    _viewModel.SelectedIDType = arg.SelectedValue;
                }
                else if (arg.PickerId == "IBANIdNumberPicker")
                {
                    _viewModel.SelectedIDNumber = arg.SelectedValue;
                }
                else if (arg.PickerId == "IBANBankNamePicker")
                {
                    _viewModel.SelectedBankName = arg.SelectedValue;
                }
               
            });

           

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
          
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {

            }

        }

        private void AccountOwnerNameTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.AccountOwnerName = e.NewTextValue;


        }

        private void IBANTextChanged(object sender, TextChangedEventArgs e)
        {

            if(e.NewTextValue.Length > 0) {

                string str = e.NewTextValue.Substring(0, 1);

                if (str.Equals("SA") || str.Equals("S"))
                {
                    _viewModel.IBANValue = e.NewTextValue;
                }
                else {

                    BankAccountIBAN.Text = "";
                    _viewModel.IBANValue = "";
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforSA));

                }

            }
           
        }

        private void IBANFocusChnaged(object sender, TextChangedEventArgs e)
        {

            if ((_viewModel.IBANValue.Length > 0) && (_viewModel.IBANValue.Length < 24))
            {
               
                  PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDIBANValidationforLenght));

            }

        }
    }
}
