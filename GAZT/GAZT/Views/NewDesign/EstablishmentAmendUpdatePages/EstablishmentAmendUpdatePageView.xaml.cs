using EGAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using System.Collections.ObjectModel;
using System.Globalization;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using EGAZT.Views.NewDesign.Common;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Syncfusion.XForms.Cards;
using System.Collections.Generic;
using EGAZT.Views.NewDesign.GenericPickers;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentAmendUpdatePageView : ContentPage
    {
        EstablishmentAmendUpdatePageViewModel viewModel;
       // List<Grid> NationalityTileGrids = null;
        public EstablishmentAmendUpdatePageView()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex) { Console.Write("UpdateAmend exception : "+ex.Message);
                DisplayAlert("UpdateAmend exception", ex.Message, "");
            }
            ChangeAeroIcon();
                SetLTR();
                viewModel = App.Locator.EstablishmentAmendUpdatePage;
                viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
                viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
                viewModel.IsNavigationCompletedToSuccessfulPage = false;
                BindingContext = viewModel;
                if (App.ZAKATType == Enums.PageExecutionType.Amend || App.ZAKATType == Enums.PageExecutionType.Update)
                {
                    fiscalMonth.IsEnabled = false;
                    fiscalDay.IsEnabled = false;
                }
                MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) =>
                {
                    PopupNavigation.Instance.PopAsync();
                    viewModel._navigationService.GoBack();
                });
                MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse", (obj, res) =>
                {
                    PopupNavigation.Instance.PopAsync();
                    viewModel._navigationService.GoBack();
                });
                MessagingCenter.Subscribe<Xamarin.Forms.Application>(this, "BackButtonPressed", (args) =>
                {
                    if (viewModel.IsExceptionPopupVisible)
                    {
                        Navigation.PopAsync();
                    }
                });
                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {

                    viewModel.PickerModel = arg;
                });
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            SetPickerFont();
            viewModel?.OnAppearing();
            MessagingCenter.Subscribe<EstablishmentAmendUpdatePageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
            {
                if (res)
                    Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                else
                    Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
            });
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            dobPicker.HeaderFontFamily = "Somar-SemiBold";
                            dobPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            dobPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            dobPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            dobHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportIssuePicker.HeaderFontFamily = "Somar-SemiBold";
                            passportIssuePicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportIssuePicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportIssuePicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportIssueHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportExpiryPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportExpiryHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {

                            dobPicker.HeaderFontFamily = "Somar-SemiBold";
                            dobPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            dobPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            dobPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            dobHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            dobHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportIssuePicker.HeaderFontFamily = "Somar-SemiBold";
                            passportIssuePicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportIssuePicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportIssuePicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportIssueHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportExpiryPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportExpiryPicker.UnSelectedItemFontFamily = "Somar-SemiBold";

                            passportExpiryHijiriPicker.HeaderFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse");
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse");
            MessagingCenter.Unsubscribe<EstablishmentAmendUpdatePageViewModel, bool>(this, "IsInstrunctionChecked");
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();

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

        public void ChangeAeroIcon()
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

        void dOBDateClicked(System.Object sender, System.EventArgs e)
        {
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                dobPicker.IsOpen = true;
            }
            else
            {
                dobHijiriPicker.IsOpen = true;
            }
        }

        void PassportIssueDateClicked(System.Object sender, System.EventArgs e)
        {
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                passportIssuePicker.IsOpen = true;
            }
            else
            {
                passportIssueHijiriPicker.IsOpen = true;
            }
        }

        void PassportExpiryDateClicked(object sender, EventArgs e)
        {
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                passportExpiryPicker.IsOpen = true;
            }
            else
            {
                passportExpiryHijiriPicker.IsOpen = true;
            }
        }

        void SfChipGroup_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                var index = TabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                TabScrollView.ScrollToAsync(TabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }
            catch (Exception ex) {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

         void FinacialPeriodSelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            viewModel.isFinaceDetailsChanged = true;

            if (viewModel.SelectedPeriod != null)
            {

                viewModel.TaxDate = viewModel.SelectedPeriod.ConvretedToDate;
            }

        }

        void FinacialMethodSelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {

            try {
                string selectedItem = e.AddedItems[0] as string;

                if(selectedItem == AppResources.NDAccounting) {

                    viewModel.IsFinancePeriodVisible = true;
                }
                else {
                    viewModel.IsFinancePeriodVisible = false;

                }


            }
            catch (Exception)
            {

            }





            viewModel.isFinaceDetailsChanged = true;

        }


        async void TapRentDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            Image item = sender as Image;
            Attachment data = item.BindingContext as Attachment;
            string QuestionMark = string.Empty;
            if (App.IsArabic)
            {
                QuestionMark = "؟";
            }
            else
            {
                QuestionMark = "?";
            }
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + " " + data.Filename + QuestionMark);
            confirmPopup.OnSelect = (str) =>
            {
                if (str == "Yes")
                {
                    viewModel.OnRentAttachmentDeleteButtonTapped(data);
                }
            };
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        async void TapPassportDeleteGestureRecognizer_Tapped(Object sender, EventArgs e)
        {
            Image item = sender as Image;
            Attachment data = item.BindingContext as Attachment;
            string QuestionMark = string.Empty;
            if (App.IsArabic)
            {
                QuestionMark = "؟";
            }
            else
            {
                QuestionMark = "?";
            }
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + " " + data.Filename + QuestionMark);
            confirmPopup.OnSelect = (str) =>
            {
                if (str == "Yes")
                {
                    viewModel.OnPassportAttachmentDeleteButtonTapped(data);
                }
            };
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        void SfChipGroup_SelectionChanging(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
        {
            try
            {
                var tt = e.AddedItem as string;
                EstablishmentRegistrationTabsEnum newselectedTab = getEnumFromChipsLabel(tt);
                if (!string.IsNullOrWhiteSpace(tt) && (int)newselectedTab >= (int)viewModel?.currentTab)
                {
                    e.Cancel = true;
                }
                else
                {
                    viewModel.currentTab = newselectedTab;
                }
            }
            catch (Exception ex) {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private EstablishmentRegistrationTabsEnum getEnumFromChipsLabel(string label)
        {
            if (label.Equals(AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel))
            {
                return EstablishmentRegistrationTabsEnum.TaxpayerDetail;
            }
            if (label.Equals(AppResources.ESTPassportDetailsTabTitleLabel))
            {
                return EstablishmentRegistrationTabsEnum.PassportDetails;
            }
            if (label.Equals(AppResources.ESTOutletsTabTitleLabel))
            {
                return EstablishmentRegistrationTabsEnum.Outlets;
            }
            if (label.Equals(AppResources.VATRFinancialDetails))
            {
                return EstablishmentRegistrationTabsEnum.FinancialDetail;
            }
            if (label.Equals(AppResources.ZVatSummary))
            {
                return EstablishmentRegistrationTabsEnum.Declaration;
            }
            return EstablishmentRegistrationTabsEnum.RegistrationType;
        }
        async void dobPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try { 
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                selectedItem = dobPicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplaySelectedDOB = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                viewModel.SelectedDOB = viewModel?.DisplaySelectedDOB;
            }
            else
            {
                selectedItem = dobHijiriPicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplaySelectedDOB = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                DateTime.TryParseExact(viewModel?.DisplaySelectedDOB, "yyyy/MM/dd", new CultureInfo("ar-sa"), DateTimeStyles.None, out DateTime _dob);
                viewModel.SelectedDOB = _dob.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            }
            var dob = viewModel.SelectedDOB.Replace("/", "");
            await viewModel.ValidateIDAndDOB(viewModel.idItem?.Type, viewModel.GCCIDTypeIdNumberValue, dob);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        void passportIssuePicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try { 
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                selectedItem = passportIssuePicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplayPassportIssueDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                viewModel.PassportIssueDate = viewModel?.DisplayPassportIssueDate;
            }
            else
            {
                selectedItem = passportIssueHijiriPicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplayPassportIssueDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                DateTime.TryParseExact(viewModel?.DisplayPassportIssueDate, "yyyy/MM/dd", new CultureInfo("ar-sa"), DateTimeStyles.None, out DateTime _issueDate);
                viewModel.PassportIssueDate = _issueDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        void passportExpiryPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try { 
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "G")
            {
                selectedItem = passportExpiryPicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplayPassportExpireDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                viewModel.PassportExpireDate = viewModel?.DisplayPassportExpireDate;
            }
            else
            {
                selectedItem = passportExpiryHijiriPicker.SelectedItem as ObservableCollection<object>;
                viewModel.DisplayPassportExpireDate = $"{selectedItem[2]}/{selectedItem[1]}/{selectedItem[0]}";
                DateTime.TryParseExact(viewModel?.DisplayPassportExpireDate, "yyyy/MM/dd", new CultureInfo("ar-sa"), DateTimeStyles.None, out DateTime _expiryDate);
                viewModel.PassportExpireDate = _expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}