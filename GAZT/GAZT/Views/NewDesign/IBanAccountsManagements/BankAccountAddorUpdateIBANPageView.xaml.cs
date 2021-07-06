using System;
using System.Collections.Generic;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            //Check for Large Tax payer or not

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


                Console.WriteLine(arg);
                OnAppearing();
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
            _viewModel.IBANValue = e.NewTextValue;
        }
    }
}
