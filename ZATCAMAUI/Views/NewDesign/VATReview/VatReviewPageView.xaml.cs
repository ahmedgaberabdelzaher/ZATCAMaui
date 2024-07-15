using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewPageView : ContentPage, VatReviewInterface
    {
        private VatReviewViewModel viewModel;

        public VatReviewPageView()
        {
            InitializeComponent();

            ChangeAeroIcon();

            viewModel = App.Locator.VatReviewView;
            this.BindingContext = viewModel;
            viewModel.vRInterface = (ViewModel.NewDesignViewModel.VATReviewViewModel.VatReviewInterface)this;


            viewModel.ResetData();

            _ = viewModel.VatReviewReasonDropDownData();
            viewModel.setMoreOptioButtons();

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
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            getYesCommand();
            getNoCommand();




            MessagingCenter.Unsubscribe<object, int>(this, "draftRequest");
            MessagingCenter.Unsubscribe<object, int>(this, "draftSecurity");


            try
            {
                MessagingCenter.Subscribe<object, int>(this, "draftRequest", (sender, arg) =>
                {
                    DisputeAmountListView.SelectedItem = viewModel.DisputeAmountPaymentOptions[arg];

                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            try
            {
                MessagingCenter.Subscribe<object, int>(this, "draftSecurity", async (sender, arg) =>
                {
                    securityTypeListView.SelectedItem = viewModel.SecurityPaymentOptions[arg];
                    if (arg == 0)
                    {
                        viewModel.EnableSadadSecurityView();
                    }
                    else if (arg == 1)
                    {
                        viewModel.EnablebankGuranteeSecurityView();
                    }
                    viewModel.EnableSecurityPaymentsConButton();


                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }


            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                //viewModel.PickerModel = arg;
                //viewModel.updatePicker();
                viewModel.updatePickerData(arg);
            });
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {
                    try
                    {
                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = arg.SelectedValue.Split('/');
                        dt1 = dts[2] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[0];
                        viewModel.PickedDateFullMonth = dt1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    viewModel.PickedDate = arg.SelectedValue;
                    viewModel.ValidateIdNumber();
                });

            MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.PopulateAttachments(arg.results);
                }
            });

            MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
            {
                await PopupNavigation.Instance.PopAsync();
                if (arg != null)
                {
                    string message = arg;

                    if (App.IsArabic)
                    {
                        ArButtons buttonId = ArButtons.None;
                        if (!string.IsNullOrEmpty(message))
                        {
                            message = message.Replace(" ", "");
                        }
                        Enum.TryParse(message, out buttonId);
                        switch (buttonId)
                        {
                            case ArButtons.إضافةملاحظات:
                                //viewModel.VATReturnAddNote();
                                break;
                            case ArButtons.عرضملاحظات:
                                //  viewModel.VATReturnGetNotes();
                                break;
                            case ArButtons.المرفقات:
                                // viewModel.VATViewAttachments();
                                break;
                            case ArButtons.إلغاء:
                                viewModel.IsDraftClicked = true;
                                viewModel.VoidMsg();
                                viewModel.IsDraftClicked = false;
                                break;
                            case ArButtons.عادةتعيين:
                                //await viewModel.VATReturnResetAsync();
                                break;
                            case ArButtons.تعديل:
                                // await viewModel.VATReturnAmendAsync();
                                break;
                            case ArButtons.حفظكمسودة:
                                viewModel.IsDraftClicked = true;
                                await viewModel.OnSaveDraftClicked();
                                viewModel.IsDraftClicked = false;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Buttons buttonId = Buttons.None;
                        if (!string.IsNullOrEmpty(message))
                        {
                            message = message.Replace(" ", "");
                        }
                        Enum.TryParse(message, out buttonId);
                        switch (buttonId)
                        {
                            case Buttons.CreateNotes:
                                //viewModel.VATReturnAddNote();
                                break;
                            case Buttons.DisplayNotes:
                                //viewModel.VATReturnGetNotes();
                                break;
                            case Buttons.Attachments:
                                // viewModel.VATViewAttachments();
                                break;
                            case Buttons.Void:
                                viewModel.IsDraftClicked = true;
                                viewModel.VoidMsg();
                                viewModel.IsDraftClicked = false;
                                break;
                            case Buttons.Reset:
                                //await viewModel.VATReturnResetAsync();
                                break;
                            case Buttons.Amend:
                                // await viewModel.VATReturnAmendAsync();
                                break;
                            case Buttons.SaveasDraft:
                                viewModel.IsDraftClicked = true;
                                await viewModel.OnSaveDraftClicked();

                                viewModel.IsDraftClicked = false;
                                break;
                            default:
                                break;
                        }
                    }

                }
            });


        }

        public void getYesCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await PopupNavigation.Instance.PopAsync();
                            await viewModel.VATSetReturnVoid();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void getNoCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }

                    //await PopupNavigation.Instance.PopAsync();
                    // await viewModel.VATSetReturnVoidAsync();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");

            MessagingCenter.Unsubscribe<object, int>(this, "draftRequest");
            MessagingCenter.Unsubscribe<object, int>(this, "draftSecurity");

            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");

        }

        private void Report_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel.charCountReportDetails = Report_Details_Tx.Text.Length + "/" + 1000;
                viewModel.ReportDetails = Report_Details_Tx.Text;
                viewModel.EnableReportDetailsConButton();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
            }
        }



        private void SADAD_CheckBox_CheckedChanged(object sender, Boolean e)
        {
            viewModel.EnableSecurityPaymentsConButton();

            if (!string.IsNullOrEmpty(viewModel.SADADNumber))
            {
                return;
            }

            if (e)
            {
                viewModel.ShowSadadGenerateButton();
            }
            else
            {
                viewModel.HideSadadGenerateButton();
            }
        }
        private void Dispute_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.charCountDisputeDetails = Dispute_Details_Tx.Text.Length + "/" + 1000;
        }


        private void Security_Type_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedITem = e.DataItem as VatReviewViewModel.SelectionModel;
                if (selectedITem.SelectionTitle.Equals(AppResources.VRSADAD))
                {
                    viewModel.EnableSadadSecurityView();
                }
                else if (selectedITem.SelectionTitle.Equals(AppResources.VRBANKGURANTEE))
                {
                    viewModel.EnablebankGuranteeSecurityView();

                }

                viewModel.EnableSecurityPaymentsConButton();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        private void Report_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.ReportDetails = Report_Details_Tx.Text;
            viewModel.EnableReportDetailsConButton();
        }
        private void LateFiling_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.LateFlngDetails = LateFiling_Details_Txx.Text;
            viewModel.EnableLateFilingsDetailsConButton();
        }


        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclarationConButton();
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                viewModel._idNumber = e.NewTextValue;
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs e)
        {
            viewModel.ValidateIdNumber();
        }

        private void CheckBox_CheckedChanged(object sender, Boolean e)
        {
            viewModel.EnableSecurityPaymentsConButton();
        }

        private void DecCheckBox_CheckedChanged(object sender, Boolean e)
        {
            viewModel.EnableDeclarationConButton();
        }

        private void DecCheckBox_CheckedChanged1(object sender, Boolean e)
        {
            viewModel.EnableDeclarationConButton();
        }

        private void Dispute_Details_UnFocused(object sender, FocusEventArgs e)
        {
            viewModel.DisputeDetailsDesc = Dispute_Details_Tx.Text;
            viewModel.EnableReviewDetailsConButton();
        }
        private void RRAmountUnfocused(object sender, FocusEventArgs e)
        {
            viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(rrAmountTxt.Text.ToString());

            if (!String.IsNullOrEmpty(viewModel.RequestedReviewAmount) && !String.IsNullOrEmpty(viewModel.TotalTaxLiability) &&
                Double.Parse(viewModel.RequestedReviewAmount) > Double.Parse(viewModel.TotalTaxLiability))
            {
                viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(viewModel.TotalTaxLiability);
            }
            viewModel.EnableReviewDetailsConButton();
            viewModel.FetchSecurityAmount();

        }
        private void Dispute_Amount_ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedITem = e.DataItem as VatReviewViewModel.SelectionModel;
                if (selectedITem.SelectionTitle.Equals(AppResources.VRInfull))
                {
                    viewModel.IsRRAmountEdit = false;
                    viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInfull;
                    viewModel.RequestedReviewAmount = UtilityManager.GetCommaSeparatedAmount(viewModel.TotalTaxLiability.ToString());
                    viewModel.FetchSecurityAmount();
                }
                else if (selectedITem.SelectionTitle.Equals(AppResources.VRInpartial))
                {
                    viewModel.IsRRAmountEdit = true;
                    viewModel.FetchSecurityAmount();
                    viewModel.VRRequesttoReviewtheAmountValue = AppResources.VRInpartial;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine(ex.Message);
            }
        }

        public void SelectDefaultPaymentOption()
        {
            DisputeAmountListView.SelectedItem = viewModel.DisputeAmountPaymentOptions[viewModel.DefaultReq];

            securityTypeListView.SelectedItem = viewModel.SecurityPaymentOptions[viewModel.DefaultSecurity];

            if (viewModel.DefaultSecurity == 0)
            {
                viewModel.EnableSadadSecurityView();
            }
            else if (viewModel.DefaultSecurity == 1)
            {
                viewModel.EnablebankGuranteeSecurityView();
            }
            viewModel.EnableSecurityPaymentsConButton();
        }

        private void LateFiling_Details_Tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.charCountLateFilingDetails = LateFiling_Details_Txx.Text.Length + "/" + 3000;
            viewModel.LateFlngDetails = LateFiling_Details_Txx.Text;

            viewModel.EnableLateFilingsDetailsConButton();


        }

        private async void OnInfoButtonTapped(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatReviewLateFilingInfo));
            }
            catch (Exception)
            {

            }
        }
    }

    public interface VatReviewInterface
    {
        void SelectDefaultPaymentOption();

    }
}