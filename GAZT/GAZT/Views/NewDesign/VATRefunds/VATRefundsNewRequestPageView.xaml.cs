using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    public partial class VATRefundsNewRequestPageView : ContentPage
    {
        VATRefundsNewRequestViewModel viewModel;
        VatRefundsListResultModel DraftsRequestDataModel;

        public VATRefundsNewRequestPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            DraftsRequestDataModel = null;
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });
        }

        public VATRefundsNewRequestPageView(VatRefundsListResultModel draftsRequestData)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            DraftsRequestDataModel = draftsRequestData;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string message = string.Empty;
            ChangeArrowDirection();
            Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    message = arg;
                    viewModel.AddNewIban(message);
                }
            });

            try
            {
                if(DraftsRequestDataModel == null)
                {
                    viewModel.ReloadData();
                }
                else
                {
                    viewModel.LoadDraftsData(DraftsRequestDataModel);
                }
            }
            catch(GAZTErrorException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
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
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        public void ChangeArrowDirection()
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

        void BankOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            //TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            //viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            // viewModel.IsNewAccountClicked = true;
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView(string.Empty));
        }
        
        void ContinueButton_Tapped(object sender, EventArgs e)
        {
            try
            {
                viewModel.ContinueBtnClicked();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void VoidButton_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.VoidBtnClicked();
        }
    }
}
