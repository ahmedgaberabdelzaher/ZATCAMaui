using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
            catch (Exception ex)
            {

            }
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
    }
}
