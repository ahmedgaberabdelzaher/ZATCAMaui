using Mopups.Services;
using Syncfusion.Maui.Core.Chips;
using Syncfusion.Maui.ListView;
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
                MopupService.Instance.PopAsync();
                viewModel._navigationService.GoBack();
            });
            MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse", (obj, res) =>
            {
                MopupService.Instance.PopAsync();
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
            MessagingCenter.Subscribe<EstablishmentAmendUpdatePageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
            {
                if (res)
                    Resources["IsInstrunctionCheckedStyle"] = Application.Current.Resources["CheckboxSelectedFontStyle"];
                else
                    Resources["IsInstrunctionCheckedStyle"] = Application.Current.Resources["CheckboxUnselectedFontStyle"];
            });
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



        void dOBDateClicked(object sender, EventArgs e)
        {
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
            if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
            {
                passportExpiryPicker.IsOpen = true;
            }
            else
            {
                passportExpiryHijiriPicker.IsOpen = true;
            }
        }

        void FinacialPeriodSelectionChanged(System.Object sender, ItemSelectionChangedEventArgs e)
        {
            viewModel.isFinaceDetailsChanged = true;

            if (viewModel.SelectedPeriod != null)
            {

                viewModel.TaxDate = viewModel.SelectedPeriod.ConvretedToDate;
            }

        }

        void FinacialMethodSelectionChanged(System.Object sender, ItemSelectionChangedEventArgs e)
        {

            try
            {
                string selectedItem = e.AddedItems[0] as string;

                if (selectedItem == AppResources.NDAccounting)
                {

                    viewModel.IsFinancePeriodVisible = true;
                }
                else
                {
                    viewModel.IsFinancePeriodVisible = false;

                }


            }
            catch (Exception)
            {

            }





            viewModel.isFinaceDetailsChanged = true;

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
            await MopupService.Instance.PushAsync(confirmPopup);
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
            await MopupService.Instance.PushAsync(confirmPopup);
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
                if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
                var dob = viewModel.SelectedDOB.Replace("/", "-");
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
                if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
                if (EstablishmentAmendUpdatePageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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