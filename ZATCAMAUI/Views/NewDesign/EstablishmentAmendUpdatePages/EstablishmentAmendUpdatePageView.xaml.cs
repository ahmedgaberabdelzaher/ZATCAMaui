using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Core.Chips;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
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
                            dobPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            dobPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            dobPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            dobPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            dobHijiriPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            dobHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            dobHijiriPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            dobHijiriPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            passportIssuePicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportIssuePicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportIssuePicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            passportIssuePicker.TextStyle.FontFamily = "Somar-SemiBold";

                            passportIssueHijiriPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            passportIssueHijiriPicker.TextStyle.FontFamily = "Somar-SemiBold";


                            passportExpiryPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryPicker.TextStyle.FontFamily = "Somar-SemiBold";

                            passportExpiryHijiriPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            passportExpiryHijiriPicker.TextStyle.FontFamily = "Somar-SemiBold";

                        }
                        break;

                    case Device.Android:
                        {

                            dobPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            dobHijiriPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            dobHijiriPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            passportIssuePicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssuePicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            passportIssueHijiriPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportIssueHijiriPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            passportExpiryPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                            passportExpiryHijiriPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            passportExpiryHijiriPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

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
                //TODO
                var view = (Element)TabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index);
                TabScrollView.ScrollToAsync(view, ScrollToPosition.MakeVisible, true);
            }

            catch (Exception)
            {
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