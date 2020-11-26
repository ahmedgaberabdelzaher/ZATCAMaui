using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using EGAZT.Views.NewDesign.GenericPickers;
using GAZT.Models;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ContractReleasePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractReleasePageView : ContentPage, ContractReleaseInterface
    {
        ContractReleaseViewModel viewModel;

        public ContractReleasePageView()
        {
            InitializeComponent();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            //App.IsArabic = false;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ContractReleasePageView;
            this.BindingContext = viewModel;

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

                Task.Run(async () =>
                {
                    await viewModel.PopulateDataInChips();

                });
                ChipGroup_statusFilter.SelectedItem = viewModel.ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                viewModel.IsHijriCal = false;



            }
            catch (Exception e)
            {



            }


        }

        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {



                    case Xamarin.Forms.Device.iOS:
                        {
                            NormalCalendar.HeaderFontFamily = "SSTArabic-Medium";
                            NormalCalendar.SelectedItemFontFamily = "SSTArabic-Medium";
                            NormalCalendar.UnSelectedItemFontFamily = "SSTArabic-Medium";



                            HijriCalendar.HeaderFontFamily = "SSTArabic-Medium";
                            HijriCalendar.SelectedItemFontFamily = "SSTArabic-Medium";
                            HijriCalendar.UnSelectedItemFontFamily = "SSTArabic-Medium";



                            EndDateNormalCalendar.HeaderFontFamily = "SSTArabic-Medium";
                            EndDateNormalCalendar.SelectedItemFontFamily = "SSTArabic-Medium";
                            EndDateNormalCalendar.UnSelectedItemFontFamily = "SSTArabic-Medium";



                            EndDateHijriCalendar.HeaderFontFamily = "SSTArabic-Medium";
                            EndDateHijriCalendar.SelectedItemFontFamily = "SSTArabic-Medium";
                            EndDateHijriCalendar.UnSelectedItemFontFamily = "SSTArabic-Medium";
                        }
                        break;
                    case Xamarin.Forms.Device.Android:



                        NormalCalendar.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        NormalCalendar.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        NormalCalendar.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";



                        HijriCalendar.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        HijriCalendar.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        HijriCalendar.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";



                        EndDateNormalCalendar.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        EndDateNormalCalendar.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        EndDateNormalCalendar.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";



                        EndDateHijriCalendar.HeaderFontFamily = "GAZT_FONT_MEDIUM";
                        EndDateHijriCalendar.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";
                        EndDateHijriCalendar.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";



                        break;
                }
            }
            catch (Exception ex)
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
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {
                    if (viewModel.fromDatePicker)
                    {
                        // viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else
                    {
                        // viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                viewModel.updatePicker();
                Console.WriteLine(arg);
                OnAppearing();
            });

            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
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
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void ChipGroup_statusFilter_SelectionChanged(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
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
            catch (Exception ex)
            {
            }
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
        private void HandleAmountReleaseTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel._amountToRelease = Double.Parse(e.NewTextValue);


            }
            catch (Exception ex)
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
                viewModel.ContractTotalAmount = Double.Parse(ContractTotalAmountText.Text);
                viewModel.MakeCalculations();
                ContractTotalAmountText.Text = String.Format("{0:N}", Convert.ToDouble(ContractTotalAmountText.Text));

            }
            catch (Exception ex)
            {

            }
        }

        private void AmountToReleaseUnfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (viewModel.ContractTotalAmount < Double.Parse(AmountoReleaseTxt.Text))
                {
                    viewModel._dialogService.ShowMessageBox(AppResources.CRTotalAmountRequirdtoReleasemustbelesstotalamountofcontract, AppResources.CRWarning);
                }
                viewModel.AmountToRelease = Double.Parse(AmountoReleaseTxt.Text);

                viewModel.MakeCalculations();
                AmountoReleaseTxt.Text = String.Format("{0:N}", Convert.ToDouble(AmountoReleaseTxt.Text));

            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
            }

        }

        private void NormalCalendar_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
        private void EndDateNormalCalendar_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void NormalCalendar_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //  ValidateIDNumber();
        }
        private void EndDateNormalCalendar_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //  ValidateIDNumber();
        }
        private void NormalCalendar_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            /* viewModel.PkrDBO = viewModel.PkrDBOPrev;
             if (!string.IsNullOrEmpty(viewModel.PkrDBOPrev))
             {
                 string[] Date = viewModel.PkrDBOPrev.Split('/');
                 ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                 //Select today dates
                 todaycollection.Add(Date[2]);
                 todaycollection.Add(Date[1]);//day
                 todaycollection.Add(Date[0]);

                 DpDbo.SelectedItem = todaycollection;
             }*/
        }
        private void EndDateNormalCalendar_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            /* viewModel.PkrDBO = viewModel.PkrDBOPrev;
             if (!string.IsNullOrEmpty(viewModel.PkrDBOPrev))
             {
                 string[] Date = viewModel.PkrDBOPrev.Split('/');
                 ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                 //Select today dates
                 todaycollection.Add(Date[2]);
                 todaycollection.Add(Date[1]);//day
                 todaycollection.Add(Date[0]);

                 DpDbo.SelectedItem = todaycollection;
             }*/
        }
    }

    public interface ContractReleaseInterface
    {
        void setDateFormatFirstTime();
    }
}

