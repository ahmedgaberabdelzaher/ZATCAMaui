using Mopups.Services;
using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using System.Globalization;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentRegistrationPage : ContentPage
    {
        EstablishmentRegistrationPageViewModel viewModel;
        public EstablishmentRegistrationPage()
        {
            EstablishmentRegistrationPageViewModel.taxPayerDetails = null;
            InitializeComponent();
            viewModel = App.Locator.EstablishmentRegistrationPage;
            BindingContext = viewModel;
            viewModel.IsNavigationCompletedToSuccessfulPage = false;

            MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) =>
            {
                MopupService.Instance.PopAsync();
                viewModel._navigationService.GoBack();
            });
            viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            {
                viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
                viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            }
            viewModel?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void dOBDateClicked(object sender, EventArgs e)
        {
            if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
            if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
            if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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

        async void TapRentDeleteGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
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
            catch (Exception)
            {


            }
        }

        async void TapPassportDeleteGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            try
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
            catch (Exception)
            {


            }
        }

        void SfChipGroup_SelectionChanging(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
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

        void dobPicker_Closed(object sender, EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
            try
            {
                if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
                if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
                if (EstablishmentRegistrationPageViewModel.taxPayerDetails?.Caltp == "Gregorian")
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
