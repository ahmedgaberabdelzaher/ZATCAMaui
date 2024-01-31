using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleasePageView : ContentPage, ContractReleaseInterface
    {
        ContractReleaseViewModel viewModel;

        //public object Loadingbar { get; private set; }

        public ContractReleasePageView()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");

            //App.IsArabic = false;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            On<iOS>().SetUseSafeArea(true);



            viewModel = App.Locator.ContractReleasePageView;
            BindingContext = viewModel;

            viewModel.ResetData();

            Task.Run(async () =>
            {
                viewModel.IsLoading1 = true;
                await GetContractReleaseData();

            });

            viewModel.showInstructionDialog();

            viewModel.contractReleaseInterface = this;

            try
            {
                viewModel.PopulateDataInChips();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                    viewModel.IsHijriCal = false;
                });
            }
            catch (Exception)
            {


            }


        }

        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {



                    case Device.iOS:
                        {
                     

                            NormalCalendar.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            NormalCalendar.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            NormalCalendar.TextStyle.FontFamily = "Somar-SemiBold";


                            HijriCalendar.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            HijriCalendar.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            HijriCalendar.TextStyle.FontFamily = "Somar-SemiBold";

                            EndDateNormalCalendar.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            EndDateNormalCalendar.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            EndDateNormalCalendar.TextStyle.FontFamily = "Somar-SemiBold";

                            EndDateHijriCalendar.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            EndDateHijriCalendar.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            EndDateHijriCalendar.TextStyle.FontFamily = "Somar-SemiBold";
                        }
                        break;
                    case Device.Android:


                        NormalCalendar.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        NormalCalendar.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        NormalCalendar.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        HijriCalendar.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        HijriCalendar.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        HijriCalendar.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        EndDateNormalCalendar.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        EndDateNormalCalendar.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        EndDateNormalCalendar.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        EndDateHijriCalendar.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        EndDateHijriCalendar.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        EndDateHijriCalendar.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";

                        break;
                }
            }
            catch (Exception)
            {


            }


        }

        public async Task GetContractReleaseData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading1 = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.OnPageLoad();

                });
            }
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;


            MainThread.BeginInvokeOnMainThread(() => Padding = safeInsets);

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {
                  
                });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
                OnAppearing();
            });

            MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.PopulateAttachments(arg.results);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
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

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {
                ChipModel selectedReturntype = (ChipModel)e.AddedItem;
                ChipGroup_statusFilter.SelectedItem = selectedReturntype;
                if (selectedReturntype.Text.Equals(AppResources.NDHijri))
                {
                    viewModel.IsHijriCal = true;

                    if (EndDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = HijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.FromDate = year + "/" + month + "/" + day;
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }

                }
                else
                {
                    viewModel.IsHijriCal = false;

                    if (EndDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = NormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.FromDate = year + "/" + month + "/" + day;
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }
                }



            }
            catch (Exception)
            {


            }
        }

        public void setDateFormatFirstTime()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (viewModel.IsHijriCal)
                {
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[1];
                }
                else
                {
                    ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[0];
                }
            });
        }
        private void HandleAmountReleaseTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel._amountToRelease = double.Parse(e.NewTextValue);


            }
            catch (Exception)
            {


            }

            viewModel.MakeCalculations();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!int.TryParse(e.NewTextValue, out int value))
            {
                ContractNumberText.Text = e.OldTextValue;
            }
        }

        private void HandleTotalAmount(object sender, TextChangedEventArgs e)
        {


        }

        private void ContractAttach_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {

        }

        private void invoiceAttachments_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {

        }

        private void RemarksTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Remarks = e.NewTextValue;
            viewModel.charCountRemarksText = RemarksText.Text.Length + "/" + 255;
        }

        private void DetailDescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.DetailDescription = e.NewTextValue;
            viewModel.charCountDetailDescription = DetailDescription.Text.Length + "/" + 132;
        }

        private void ContactPersonNameTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContactPersonName = e.NewTextValue;
            viewModel.EnableDeclarationContinue();
        }

        private void DesignationTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.Designation = e.NewTextValue;
            viewModel.EnableDeclarationContinue();
        }

        private void ContractNameTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContractName = e.NewTextValue;
        }

        private void ContractNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.ContractNumber = e.NewTextValue;
        }

        private void ContractNameFiledUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.ContractName = ContractNameText.Text;
        }

        private void ContractNumberUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.ContractNumber = ContractNumberText.Text;
        }

        private void TotalAmountUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.ContractTotalAmount = double.Parse(ContractTotalAmountText.Text);
                viewModel.MakeCalculations();
                ContractTotalAmountText.Text = string.Format("{0:N}", Convert.ToDouble(ContractTotalAmountText.Text));

            }
            catch (Exception)
            {


            }
        }

        private void AmountToReleaseUnfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (viewModel.ContractTotalAmount < double.Parse(AmountoReleaseTxt.Text))
                {
                    viewModel._dialogService.ShowMessageBox(AppResources.CRTotalAmountRequirdtoReleasemustbelesstotalamountofcontract, AppResources.CRWarning);
                }
                viewModel.AmountToRelease = double.Parse(AmountoReleaseTxt.Text);

                viewModel.MakeCalculations();
                AmountoReleaseTxt.Text = string.Format("{0:N}", Convert.ToDouble(AmountoReleaseTxt.Text));

            }
            catch (Exception)
            {


            }

        }

        private void NormalCalendar_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                HijriCalendar.IsOpen = true;
            }
            else
            {
                NormalCalendar.IsOpen = true;
            }
        }

        private void EndNormalCalendar_Tapped(object sender, EventArgs e)
        {
            if (viewModel.IsHijriCal)
            {
                EndDateHijriCalendar.IsOpen = true;
            }
            else
            {
                EndDateNormalCalendar.IsOpen = true;
            }
        }

        private void NormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (HijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = HijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.FromDate = year + "/" + month + "/" + day;
                    }
                }
                else
                {
                    if (NormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = NormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.FromDate = year + "/" + month + "/" + day;
                    }
                }


            }
            catch (Exception)
            {


            }

        }

        private void EndDateNormalCalendar_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (EndDateHijriCalendar.SelectedItem != null)
                    {
                        var selectedItem = EndDateHijriCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }
                }
                else
                {
                    if (EndDateNormalCalendar.SelectedItem != null)
                    {
                        var selectedItem = EndDateNormalCalendar.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }
                }


            }
            catch (Exception)
            {


            }

        }

        private void NormalCalendar_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {

        }
        private void EndDateNormalCalendar_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {

        }

        private void NormalCalendar_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            
        }
        private void EndDateNormalCalendar_OkButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            
        }
        private void NormalCalendar_CancelButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
            
        }
        private void EndDateNormalCalendar_CancelButtonClicked(object sender, PickerSelectionChangedEventArgs e)
        {
         
        }

        void SubmitClicked(object sender, EventArgs e)
        {
            
        }
    }
}

