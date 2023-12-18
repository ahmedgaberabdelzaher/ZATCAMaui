using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Core.Chips;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentAmendUpdatePageView : ContentPage
    {
        EstablishmentAmendUpdatePageViewModel viewModel;
        List<Grid> NationalityTileGrids = null;
        public EstablishmentAmendUpdatePageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            viewModel = App.Locator.EstablishmentAmendUpdatePage;
            viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.IsNavigationCompletedToSuccessfulPage = false;
            BindingContext = viewModel;
            if (App.ZAKATType == PageExecutionType.Amend || App.ZAKATType == PageExecutionType.Update)
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
            MessagingCenter.Subscribe<Application>(this, "BackButtonPressed", (args) =>
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
            Padding = safeInsets;
            SetPickerFont();
            viewModel?.OnAppearing();
            MessagingCenter.Subscribe<EstablishmentAmendUpdatePageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
            {
                if (res)
                    Resources["IsInstrunctionCheckedStyle"] = Application.Current.Resources["CheckboxSelectedFontStyle"];
                else
                    Resources["IsInstrunctionCheckedStyle"] = Application.Current.Resources["CheckboxUnselectedFontStyle"];
            });
        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
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

                    case Device.Android:
                        {

                            dobPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";

                            dobHijiriPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";

                            passportIssuePicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";

                            passportIssueHijiriPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";

                            passportExpiryPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";

                            passportExpiryHijiriPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }
            }
            catch (Exception)
            {


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
                FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;
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

        void dOBDateClicked(object sender, EventArgs e)
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

        void PassportIssueDateClicked(object sender, EventArgs e)
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

        void SfChipGroup_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {
                var index = TabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                TabScrollView.ScrollToAsync(TabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }

            catch (Exception)
            {



                {



                }
            }
        }

        async void TapRentDeleteGestureRecognizer_Tapped(object sender, EventArgs e)
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

        async void TapPassportDeleteGestureRecognizer_Tapped(object sender, EventArgs e)
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

        void SfChipGroup_SelectionChanging(object sender, SelectionChangingEventArgs e)
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
            catch (Exception)
            {



                {



                }
            }

            EstablishmentRegistrationTabsEnum getEnumFromChipsLabel(string label)
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

            async void dobPicker_Closed(object sender, EventArgs e)
            {
                ObservableCollection<object> selectedItem = null;

                try
                {
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
                catch (Exception)
                {


                }
            }

            void passportIssuePicker_Closed(object sender, EventArgs e)
            {
                ObservableCollection<object> selectedItem = null;
                try
                {
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
                catch (Exception)
                {


                }
            }

            void passportExpiryPicker_Closed(object sender, EventArgs e)
            {
                ObservableCollection<object> selectedItem = null;
                try
                {
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
                catch (Exception)
                {


                }
            }
        }
    }
}