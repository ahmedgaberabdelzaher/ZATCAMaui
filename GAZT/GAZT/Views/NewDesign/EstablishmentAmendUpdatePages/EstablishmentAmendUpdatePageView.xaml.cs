using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using GAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using System.Collections.ObjectModel;
using System.Globalization;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using EGAZT.Views.NewDesign.Common;
using GAZT.Manager;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentAmendUpdatePageView : ContentPage
    {
        EstablishmentAmendUpdatePageViewModel viewModel;
        public EstablishmentAmendUpdatePageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            viewModel = App.Locator.EstablishmentAmendUpdatePage;
            viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            viewModel.IsNavigationCompletedToSuccessfulPage = false;
            //  viewModel.IsExceptionPopupVisible = false;
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
            MessagingCenter.Subscribe<Application>(this, "BackButtonPressed", (args) =>
             {
                 if (viewModel.IsExceptionPopupVisible)
                 {
                     Navigation.PopAsync();
                 }
             });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            //{
            //    viewModel.currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            //    viewModel.CurrentIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
            //}
            viewModel?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse");
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupBackgroundClickedResponse");
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
                //Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                //Resources["StyleReverseBack"] = App.Current.Resources["Back"];
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

            //if (viewModel?.taxPayerDetails?.Caltp == "G")
            //{
            //    passportIssuePicker.IsOpen = true;
            //}
            //else
            //{
            //    passportIssueHijiriPicker.IsOpen = true;
            //}
        }

        void SfChipGroup_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                var index = TabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                TabScrollView.ScrollToAsync(TabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }
            catch (Exception) { }
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
                    //var item = sender as Image;
                    //var data = item.BindingContext as Attachment;
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
                    //var item = sender as Image;
                    //var data = item.BindingContext as Attachment;
                    viewModel.OnPassportAttachmentDeleteButtonTapped(data);
                }
            };
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        //private void dobPicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.SelectedDOB = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        //private void PassportIssueDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.PassportIssueDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

        //private void PassportExpiryDatePicker_DateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        //{
        //    viewModel.PassportExpireDate = (e.NewValue as DateTime?)?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //}

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
            catch (Exception) { }
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
        void dobPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
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
            viewModel.ValidateIDAndDOB(viewModel.idItem?.Type, viewModel.GCCIDTypeIdNumberValue, dob);
        }

        void passportIssuePicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
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

        void passportExpiryPicker_Closed(System.Object sender, System.EventArgs e)
        {
            ObservableCollection<object> selectedItem = null;
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
    }
}