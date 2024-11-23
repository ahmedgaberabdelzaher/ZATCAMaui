using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

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
            viewModel = App.Locator.ContractReleasePageView;
            BindingContext = viewModel;


            viewModel.contractReleaseInterface = this;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");
            ContractNumberText.Text = string.Empty;
            ContractTotalAmountText.Text = string.Empty;
            viewModel.ContractName = string.Empty;
            viewModel.ContractNumber = string.Empty;
            viewModel.ContractReleaseAmount = string.Empty;
            viewModel.IsDeclarationEnabled = false;
            viewModel.DeclarationButtonBackGroundColor = (viewModel.IsDeclarationEnabled ? (Color)Application.Current.Resources["Secondary"] : (Color)Application.Current.Resources["ButtonGray"]);
            viewModel.PickedContract = string.Empty;
            viewModel.ContractTotalAmount = default;
            viewModel.AmountToRelease = default;
            viewModel.IsDECCheckBox = false;
        }

        public void setDateFormatFirstTime()
        {
            if (viewModel.IsHijriCal)
            {
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[1];
            }
            else
            {
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist[0];
            }
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
        
        private void ContractNameFiledUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.ContractName = ContractNameText.Text;
        }


        private void TotalAmountUnFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.ContractTotalAmount = double.Parse(ContractTotalAmountText.Text);
                viewModel.MakeCalculations();
                ContractTotalAmountText.Text = UtilityManager.GetCommaSeparatedAmount(ContractTotalAmountText.Text);

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
                        string month = HijriCalendar.SelectedDate.Month.ToString();
                        string day = HijriCalendar.SelectedDate.Day.ToString();
                        string year = HijriCalendar.SelectedDate.Year.ToString();
                        viewModel.FromDate = year + "/" + month + "/" + day;
                    }
                }
                else
                {
                    if (NormalCalendar.SelectedItem != null)
                    {
                        string month = NormalCalendar.SelectedDate.Month.ToString();
                        string day = NormalCalendar.SelectedDate.Day.ToString();
                        string year = NormalCalendar.SelectedDate.Year.ToString();
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
                        string month = EndDateHijriCalendar.SelectedDate.Month.ToString();
                        string day = EndDateHijriCalendar.SelectedDate.Day.ToString();
                        string year = EndDateHijriCalendar.SelectedDate.Year.ToString();
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }
                }
                else
                {
                    if (EndDateNormalCalendar.SelectedItem != null)
                    {
                        string month = EndDateNormalCalendar.SelectedDate.Month.ToString();
                        string day = EndDateNormalCalendar.SelectedDate.Day.ToString();
                        string year = EndDateNormalCalendar.SelectedDate.Year.ToString();
                        viewModel.ToDate = year + "/" + month + "/" + day;
                    }
                }


            }
            catch (Exception)
            {


            }

        }
    }
}

