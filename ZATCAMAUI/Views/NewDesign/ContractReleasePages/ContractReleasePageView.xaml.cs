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
    }
}

