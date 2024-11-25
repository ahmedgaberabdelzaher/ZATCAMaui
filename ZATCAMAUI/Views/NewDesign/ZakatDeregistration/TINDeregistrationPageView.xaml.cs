using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using Application = Microsoft.Maui.Controls.Application;
using Entry = Microsoft.Maui.Controls.Entry;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using Syncfusion.Maui.ListView;
using System.Text;
using Mopups.Services;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Globalization;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class TINDeregistrationPageView : ContentPage
    {
        TINDeregistrationPageViewModel viewModel;
        OutletSetResult selectedItem;
        public TINDeregistrationPageView(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            InitializeComponent();
            Resources["IsOutletCheckedStyle"] = Microsoft.Maui.Controls.Application.Current.Resources["CheckboxUnselectedFontStyle"];
            Resources["IsDeclarationCheckedStyle"] = Microsoft.Maui.Controls.Application.Current.Resources["CheckboxUnselectedFontStyle"];
            viewModel = App.Locator.TINDeregistrationPageView;

            viewModel.ClearData();
            viewModel.TinDeregistrationData = tinDeregistrationResponseModel;
            BindingContext = viewModel;

            viewModel.LoadReasonSet();
            viewModel.PopulateAttachmentsListViewTemplate();
            if (viewModel.TinDeregistrationData != null)
            {
                if (viewModel.TinDeregistrationData.ADregOpt == "3")
                {
                    viewModel.outletEditIsVisible = true;
                    viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
                }
                else if (viewModel.TinDeregistrationData.ADregOpt == "2")
                {
                    viewModel.outletEditIsVisible = false;
                    viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;

                }
                else
                {
                    viewModel.outletEditIsVisible = false;

                    viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;

                }
            }


            viewModel.IsReasonViewEnabled = true;
            viewModel.IsOutletViewEnabled = false;
            viewModel.IsAttachmentsEnabled = false;
            viewModel.IsDeclarationViewEnabled = false;
            viewModel.IsSummaryViewEnabled = false;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetDate();

            MessagingCenter.Subscribe<TINDeregistrationPageViewModel, bool>(this, "EnableOutletContinueButton", (sender, args) =>
            {
                btnOutletContinue.IsEnabled = true;
            });
            MessagingCenter.Subscribe<TINDeregistrationPageViewModel, bool>(this, "IsOutletChecked", (sender, args) =>
            {
                if (args)
                {
                    Resources["IsOutletCheckedStyle"] = Application.Current.Resources["CheckboxSelectedFontStyle"];
                    viewModel.OutletContinueButtonnBackroundColor = (Color)Application.Current.Resources["Secondary"];
                }
                else
                {
                    Resources["IsOutletCheckedStyle"] = Application.Current.Resources["CheckboxUnselectedFontStyle"];
                    viewModel.OutletContinueButtonnBackroundColor = (Color)Application.Current.Resources["ButtonGray"];
                }
            });
            MessagingCenter.Subscribe<TINDeregistrationPageViewModel, bool>(this, "IsDeclarationChecked", (sender, args) =>
            {
                if (args)
                    Resources["IsDeclarationCheckedStyle"] = Application.Current.Resources["CheckboxSelectedFontStyle"];
                else
                    Resources["IsDeclarationCheckedStyle"] = Application.Current.Resources["CheckboxUnselectedFontStyle"];
            });
            // viewModel.EnableOutletDetaislView();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;

                if (arg.PickerId == "reasonPicker")
                {
                    viewModel.TinDeregistrationData.AttDetSet = new List<Attachment>();
                    if (arg.SelectedValue == string.Empty)
                    {
                        viewModel.TinDeregistrationData.AttDetSet = new List<Attachment>();
                        viewModel.IsOption1Visible = false;
                        FrmDBO.IsVisible = false;
                        CalLabel.IsVisible = false;
                        DateLabel.IsVisible = false;
                        //outletDecisionOptionsListView.IsVisible = false;
                        viewModel.IsOption2Visible = false;
                    }
                    else
                    {

                        FrmDBO.IsVisible = true;
                        CalLabel.IsVisible = true;
                        DateLabel.IsVisible = true;
                        //outletDecisionOptionsListView.IsVisible = true;

                        viewModel.IsOption2Visible = false;
                        viewModel.IsOption1Visible = false;
                        //viewModel.AddOutletDecisionOptions();
                    }
                }

            });

            MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null && arg.results != null && arg.results.Count > 0)
                {
                    viewModel.TinDeregistrationData.AttDetSet = arg.results;
                    if (TINDeregistrationPageViewModel.numberOfAttachmentSentToAttachmentPopUp != arg.results.Count)
                    {
                        viewModel.isSaveAsDraftCalledForAttachment = false;
                        TINDeregistrationPageViewModel.numberOfAttachmentSentToAttachmentPopUp = 0;
                    }
                    var obj = viewModel.AttachmentsListViewData;
                    viewModel.PopulateAttachments(null);

                    foreach (Attachment attachment in viewModel.TinDeregistrationData.AttDetSet)
                    {
                        if (attachment.Dotyp == "DR01")
                        {
                            viewModel.TinDeregistrationData.ADocumnt1 = "1";
                        }
                        if (attachment.Dotyp == "DR02")
                        {
                            viewModel.TinDeregistrationData.ADocumnt2 = "1";
                        }
                        if (attachment.Dotyp == "DR03")
                        {
                            viewModel.TinDeregistrationData.ADocumnt3 = "1";
                        }
                        if (attachment.Dotyp == "DR04")
                        {
                            viewModel.TinDeregistrationData.ADocumnt4 = "1";
                        }
                        if (attachment.Dotyp == "DR05")
                        {
                            viewModel.TinDeregistrationData.ADocumnt10 = "1";
                        }
                        if (attachment.Dotyp == "DR05")
                        {
                            viewModel.TinDeregistrationData.ADocumnt10 = "1";
                        }
                        if (attachment.Dotyp == "DR09")
                        {
                            viewModel.TinDeregistrationData.ADocumnt7 = "1";
                        }
                        if (attachment.Dotyp == "DR08")
                        {
                            viewModel.TinDeregistrationData.ADocumnt5 = "1";
                        }
                        if (attachment.Dotyp == "DR07")
                        {
                            viewModel.TinDeregistrationData.ADocumnt9 = "1";
                        }
                        if (attachment.Dotyp == "DR10")
                        {
                            viewModel.TinDeregistrationData.ADocumnt13 = "1";
                        }
                        if (attachment.Dotyp == "DR11")
                        {
                            viewModel.TinDeregistrationData.ADocumnt8 = "1";
                        }
                        if (attachment.Dotyp == "DR12")
                        {
                            viewModel.TinDeregistrationData.ADocumnt14 = "1";
                        }
                    }
                }
            });

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                if (arg.PickerId == "DeregDatePicker")
                {
                    viewModel.DeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "DOBDateTypePicker")
                {
                    viewModel.SelectedDob = arg.SelectedValue;

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID || viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (!string.IsNullOrEmpty(viewModel.SelectedIdNumber))
                        {
                            viewModel.ValidateIDNumber();
                        }
                    }
                }
            });


            if (viewModel.TinDeregistrationData.ADregOpt == "3")
            {
                viewModel.outletEditIsVisible = true;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            }
            else if (viewModel.TinDeregistrationData.ADregOpt == "2")
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;

            }
            else
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            }

            MessagingCenter.Subscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption", (arg) =>
            {
                GetSelectedDataTemplate();
            });
        }

        private void SetDate()
        {
            viewModel.SetDefaultDate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel, bool>(this, "EnableOutletContinueButton");
            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");
            GC.Collect();
        }




        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            try
            {
                TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
                viewModel.SelectedOutletOption = selectedItem;

                viewModel.SetDataAsitis();//CR3994
                DateLbl2.Text = viewModel.PickerDobToDisplay;

                viewModel.SetDefaultReasonLayout();
                viewModel.SelectedIdtype = string.Empty;
                viewModel.SelectedIdNumber = string.Empty;
                viewModel.TINNumber = string.Empty;
                if (viewModel.IDTypeDataModel != null)
                {
                    viewModel.IDTypeDataModel.name2 = string.Empty;
                    viewModel.FirstNameFromIdType = string.Empty;
                    viewModel.IDTypeDataModel.fatherName = string.Empty;
                    viewModel.IDTypeDataModel.grandfatherName = string.Empty;
                    viewModel.IDTypeDataModel.familyName = string.Empty;
                }

                viewModel.TinDeregistrationData.AttDetSet = new List<Attachment>();
            }
            catch (Exception)
            {
            }
        }

        private void OnSurnameTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
                {
                    viewModel.SurName = e.NewTextValue;
                }
            }
            catch (Exception)
            {

            }
        }
        void GetSelectedDataTemplate(bool isIndex1 = false)
        {


            if (viewModel.SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationCloseOutletsIndividually))
            {
                viewModel.outletEditIsVisible = true;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            }
            else if (viewModel.SelectedOutletOption.CardLabel.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
            }
            else
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            }

        }

        async void attachmentsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            viewModel.SelectedAttachment = e.AddedItems[0] as TinDeregestrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);
            await viewModel.NewAttachmentClicked();
            var view = sender as SfListView;
            view.SelectedItem = null;

        }
        private void EntryMobileNo_Unfocused(object sender, FocusEventArgs e)
        {

            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.TinDeregistrationData.ADecTelNo))
            {
                if (viewModel.TinDeregistrationData.ADecTelNo.Substring(0, 1) != "5")
                {
                    var Message = AppResources.ZZMobilenumberhastostartwithnumber5;
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message));

                }
                else
                {
                    if (viewModel.TinDeregistrationData.ADecTelNo.Length != 10)
                    {
                        if (viewModel.TinDeregistrationData.ADecTelNo.Length < 9)
                        {
                            Messages.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                        }
                        if (Messages.Length > 0)
                        {
                            var Message = Messages.ToString();

                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message));
                        }
                    }
                }


            }

        }
        private async void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var message = string.Empty;
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(viewModel.SelectedIdtype))
                {
                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "1")
                        {
                            message = AppResources.ZZNationalIDstartswith1;

                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));

                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            if (viewModel.SelectedIdNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }
                            if (Messages.Length > 0)
                            {
                                message = Messages.ToString();

                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                                {
                                    await viewModel.ValidateIDNumber(viewModel.PickerDOBDateDisplay);
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "2")
                        {
                            message = AppResources.ZZIqamaIDstartswith2;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            if (viewModel.SelectedIdNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                message = Messages.ToString();

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                                {
                                    await viewModel.ValidateIDNumber(viewModel.PickerDOBDateDisplay);
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            message = AppResources.ZZGCCIDdonotstartwith0;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else if (!(viewModel.SelectedIdNumber.Length <= 15 && viewModel.SelectedIdNumber.Length >= 7))
                        {
                            message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            await viewModel.ValidateIDNumber();
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "7")
                        {
                            //Have to change to neww error message
                            message = AppResources.TinDeregistrationCompanyIDCheck;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else if (viewModel.SelectedIdNumber.Length > 10)
                        {
                            //Have to change to neww error message
                            message = AppResources.CompanyIDlengthis10digit;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            await viewModel.ValidateIDNumber();
                        }
                    }
                }
                else
                {
                    viewModel.FrameIDError = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.SelectedIdNumber))
            {
                viewModel.FrameIDError = false;
            }
            if (viewModel.SelectedIDTypeCode == "ZS0005" && e.NewTextValue.Length < 7)
            {
                FrmDBO1.IsEnabled = false;
                DateEntry23.Text = string.Empty;
            }
            else
            {
                FrmDBO1.IsEnabled = true;
            }

            viewModel.PickerDOBDateDisplay = string.Empty;
        }


        private void HijriCalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsOption1Visible)
                {
                    if (viewModel.IsHijriCal)
                    {


                        if (!string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                        {
                            viewModel.PickerDobToDisplay = UtilityManager.ConvertToHijri(viewModel.PickerDobToDisplay);
                        }
                        else
                        {
                            viewModel.PickerDobToDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);

                        }

                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                        {
                            viewModel.PickerDobToDisplay = UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);
                        }
                        else
                        {
                            viewModel.PickerDobToDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);

                        }

                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void HijriCal2Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsOption2Visible)
                {
                    if (viewModel.IsHijriCal)
                    {
                        if (!string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                        {
                            viewModel.PickerDobToDisplay = UtilityManager.ConvertToHijri(viewModel.PickerDobToDisplay);
                            viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PickerDobToDisplay);
                        }
                        else
                        {
                            viewModel.PickerDobToDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(viewModel.PickerDobToDisplay))
                        {
                            viewModel.PickerDobToDisplay = UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);
                            viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PickerDobToDisplay);
                        }
                        else
                        {
                            viewModel.PickerDobToDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);

                        }
                    }
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                if (viewModel != null)
                {
                    HijriCalSwitch3.IsToggled = viewModel.IsHijriCal;
                }

            }
        }

        private void HijriCal3Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsDOBHijriCal)
                {
                    if (!string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                    {
                        viewModel.PickerDOBDateDisplay = UtilityManager.ConvertToHijri(viewModel.PickerDOBDateDisplay);
                    }
                    else
                    {
                        viewModel.PickerDOBDateDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);

                    }


                }
                else
                {
                    if (!string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                    {
                        viewModel.PickerDOBDateDisplay = UtilityManager.HijriToGreg(viewModel.PickerDOBDateDisplay);
                    }
                    else
                    {
                        viewModel.PickerDOBDateDisplay = string.Empty;// UtilityManager.HijriToGreg(viewModel.PickerDobToDisplay);

                    }

                }

            }
            catch (Exception)
            {


            }
            finally
            {
                HijriCalSwitch1.IsToggled = viewModel.IsDOBHijriCal;
            }
        }

        private void DateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                FrmDBO.HasError = false;
            }

        }
        private void DOBDateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                FrmDBO.HasError = false;
            }

        }
        private void OnDOBClicked(object sender, EventArgs e)
        {

            if (!viewModel.IsHijriCal)
            {
                DpDbo.IsOpen = true;
            }
            else
            {
                DpDboHijri.IsOpen = true;
            }
        }


        private void OnIDDOBClicked(object sender, EventArgs e)
        {

            if (viewModel.IsEnteredTINValid == false)
            {
                if (string.IsNullOrEmpty(idNumber.Text))
                {
                    idNumber.Focus();
                    return;
                }
                if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID && !string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                {
                    return;
                }
                if (!viewModel.IsDOBHijriCal)
                {
                    DpDbo3.IsOpen = true;
                }
                else
                {
                    DpDboHijri3.IsOpen = true;
                }
            }


        }
        private async void DpDbo_Closed(object sender, EventArgs e)
        {
            FrmDBO.HasError = false;
            viewModel.IsDeRegistrationValid = true;
            bool isHIjri;
            DateTime deregDate = new DateTime();
            DateTime permitDate = new DateTime();
            try
            {
                if (viewModel.IsHijriCal)
                {

                    if (DpDboHijri.SelectedItem != null && (DpDboHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = DpDboHijri.SelectedDate.Month.ToString();
                        string day = DpDboHijri.SelectedDate.Day.ToString();
                        string year = DpDboHijri.SelectedDate.Year.ToString();
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && viewModel.SelectedOutletOption.OutletOptionIndex != "1" && !string.IsNullOrEmpty(viewModel.SelectedDob) && DateTime.Parse(date) < DateTime.Parse(viewModel.SelectedDob))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TINDeregDateDOBValidation));
                            viewModel.PickerDOBDateDisplay = "";
                            viewModel.SelectedDob = "";
                        }

                        viewModel.PkrDBO = date;
                        viewModel.DeregistrationDate = new DateTime(DpDboHijri.SelectedDate.Year, DpDboHijri.SelectedDate.Month, DpDboHijri.SelectedDate.Day);
                        viewModel.PickerDobToDisplay = date;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    isHIjri = true;
                }
                else
                {

                    if (DpDbo.SelectedItem != null)
                    {
                        string month = DpDbo.SelectedDate.Month.ToString();
                        string day = DpDbo.SelectedDate.Day.ToString();
                        string year = DpDbo.SelectedDate.Year.ToString();
                        var date = year + "/" + month + "/" + day;

                        if (!string.IsNullOrEmpty(date) && viewModel.SelectedOutletOption.OutletOptionIndex != "1" && !string.IsNullOrEmpty(viewModel.SelectedDob) && DateTime.Parse(date) < DateTime.Parse(viewModel.SelectedDob))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TINDeregDateDOBValidation));

                            viewModel.PickerDOBDateDisplay = "";
                            viewModel.SelectedDob = "";
                        }

                        viewModel.PkrDBO = date;
                        var s = DateTime.Parse(date,new CultureInfo("en-US"));
                        viewModel.DeregistrationDate = new DateTime(DpDbo.SelectedDate.Year, DpDbo.SelectedDate.Month, DpDbo.SelectedDate.Day);

                        viewModel.PickerDobToDisplay = viewModel.PkrDBO; 

                    }
                    isHIjri = false;
                }

                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(viewModel.TinDeregistrationData.PermitSet);
                string sortedDate = string.Empty;
                string datetype = string.Empty;
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    sortedDate = allPermitTypes.OrderBy(x => x.APermitValfrDtHTb).Select(x => x.APermitValfrDtHTb).FirstOrDefault();
                    datetype = permitInfo.APermitValfrDtCTb;
                }
                if(!string.IsNullOrWhiteSpace(datetype) && !string.IsNullOrWhiteSpace(sortedDate))
                {
                    if (datetype.Contains("H") || datetype.Contains("Hijri"))
                    {
                        string convertedSortedDate = UtilityManager.HijriToGreg(sortedDate);
                        permitDate = DateTime.Parse(convertedSortedDate, new CultureInfo("ar-SA"));
                    }
                    else
                    {
                        permitDate = DateTime.Parse(sortedDate, new CultureInfo("en-US"));

                    }
                }

                if (!string.IsNullOrWhiteSpace(viewModel.PkrDBO))
                {
                    if (isHIjri)
                    {
                        string convertedDeregDate = UtilityManager.HijriToGreg(viewModel.PkrDBO);
                        deregDate = DateTime.Parse(convertedDeregDate, new CultureInfo("ar-SA"));
                    }
                    else
                    {
                        deregDate = DateTime.Parse(viewModel.PkrDBO, new CultureInfo("en-US"));
                    }
                }
               


                if (deregDate < permitDate)
                {
                    viewModel.IsDeRegistrationValid = false;
                    FrmDBO.HasError = true;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TinDeregistrationDateValidationMessage));

                }

            }
            catch (Exception ex)
            {


            }

        }

        private async void DpDOB_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsDOBHijriCal)
                {
                    if (DpDboHijri3.SelectedItem != null && (DpDboHijri3.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = DpDboHijri3.SelectedDate.Month.ToString();
                        string day = DpDboHijri3.SelectedDate.Day.ToString();
                        string year = DpDboHijri3.SelectedDate.Year.ToString();
                        var date = year + "/" + month + "/" + day;

                        if (!string.IsNullOrEmpty(date) && DateTime.Parse(date, new CultureInfo("en-US")).Date > viewModel.DeregistrationDate.Date)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TINDeregDateDOBValidation));

                            return;
                        }
                        viewModel.DateOfBirth = date;
                        viewModel.SelectedDob = date;
                        viewModel.PickerDOBDateDisplay = viewModel.DateOfBirth;
                        if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
                            return;
                        await viewModel.ValidateIDNumber(date);

                    }
                }
                else
                {
                    if (DpDbo3.SelectedItem != null && (DpDbo3.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = DpDbo3.SelectedDate.Month.ToString();
                        string day = DpDbo3.SelectedDate.Day.ToString();
                        string year = DpDbo3.SelectedDate.Year.ToString();
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && DateTime.Parse(date, new CultureInfo("en-US")).Date > viewModel.DeregistrationDate.Date)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TINDeregDateDOBValidation));

                            return;
                        }
                        viewModel.DateOfBirth = date;
                        viewModel.SelectedDob = date;
                        viewModel.PickerDOBDateDisplay = viewModel.DateOfBirth;
                        if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
                            return;
                        await viewModel.ValidateIDNumber(date);

                    }
                }


            }
            catch (Exception)
            {


            }

        }


        void BorderlessTINEntry_Unfocused(object sender, FocusEventArgs e)
        {
            var message = string.Empty;
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTIN.Focus();
                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (EntryTIN.Text.Length > 10)
                    {
                        Messages.Append(AppResources.InvalidEntry);
                    }
                    else
                    {
                        Messages.Append(" " + AppResources.ZZTINnumberlengthcannotbelessthan10digits);
                    }
                }
                if (Messages.Length > 0)
                {
                    message = Messages.ToString();
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    viewModel.IsEnteredTINValid = false;
                    viewModel.FrameTinError = false;
                    _ = viewModel.ValidateIdNumberFromApi(EntryTIN.Text);
                }
            }
            else
            {
                message = Messages.ToString();

                EntryTIN.Text = string.Empty;
            }

        }
        private void DOBDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsDOBHijriCal)
            {
                string month = DpDboHijri3.SelectedDate.Month.ToString();
                string day = DpDboHijri3.SelectedDate.Day.ToString();
                string year = DpDboHijri3.SelectedDate.Year.ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.SelectedDob = date;
            }
            else
            {
                string month = DpDbo3.SelectedDate.Month.ToString();
                string day = DpDbo3.SelectedDate.Day.ToString();
                string year = DpDbo3.SelectedDate.Year.ToString();
                string date = year + "/" + month + "/" + day;
                viewModel.SelectedDob = date;
            }
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsHijriCal)
            {

                string month = DpDboHijri.SelectedDate.Month.ToString();
                string day = DpDboHijri.SelectedDate.Day.ToString();
                string year = DpDboHijri.SelectedDate.Year.ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.DeregistrationDate = Convert.ToDateTime(date);


            }
            else
            {
                string month = DpDbo.SelectedDate.Month.ToString();
                string day = DpDbo.SelectedDate.Day.ToString();
                string year = DpDbo.SelectedDate.Year.ToString();
                string date = year + "/" + month + "/" + day;

                viewModel.DeregistrationDate = Convert.ToDateTime(date);


            }
        }
        public void OnDateEntryFocussed(object sender, EventArgs args)
        {
            if (viewModel.IsHijriCal)
            {
                DpDboHijri.IsOpen = true;
            }
            else
            {
                DpDbo.IsOpen = true;
            }

        }


        public void OnDOBDateEntryFocussed(object sender, EventArgs args)
        {
            if (viewModel.IsEnteredTINValid == false)
            {
                DateEntry23.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"];
                if (viewModel.IsDOBHijriCal)
                {

                    if (viewModel.SelectedIDTypeCode == "ZS0005" && idNumber.Text.Length < 7)
                    {
                        DpDboHijri3.IsOpen = false;
                        FrmDBO1.IsEnabled = false;
                        return;
                    }

                    DpDboHijri3.IsOpen = true;
                    FrmDBO1.IsEnabled = true;

                }
                else
                {
                    if (viewModel.SelectedIDTypeCode == "ZS0005" && idNumber.Text.Length < 7)
                    {
                        DpDbo3.IsOpen = false;
                        FrmDBO1.IsEnabled = false;
                        return;
                    }
                    DpDbo3.IsOpen = true;
                    FrmDBO1.IsEnabled = true;

                }

            }
            else
            {
                TINNumber.HasError = true;
                viewModel.IsEnteredTINValid = false;
                DateEntry23.TextColor = Colors.LightGray;
            }

        }
        private void DpDOB_CancelButtonClicked(object sender, EventArgs e)
        {
            viewModel.PkrDBO = viewModel.PkrDBOPrev;
            if (!string.IsNullOrEmpty(viewModel.PkrDBOPrev))
            {
                string[] Date = viewModel.PkrDBOPrev.Split('/');
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                //Select today dates
                todaycollection.Add(Date[2]);
                todaycollection.Add(Date[1]);//day
                todaycollection.Add(Date[0]);

                DpDbo.SelectedItem = todaycollection;
            }
        }
        private void DpDbo_CancelButtonClicked(object sender, EventArgs e)
        {
            viewModel.PkrDBO = viewModel.PkrDBOPrev;
            if (!string.IsNullOrEmpty(viewModel.PkrDBOPrev))
            {
                string[] Date = viewModel.PkrDBOPrev.Split('/');
                ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                //Select today dates
                todaycollection.Add(Date[2]);
                todaycollection.Add(Date[1]);//day
                todaycollection.Add(Date[0]);

                DpDbo.SelectedItem = todaycollection;
            }
        }

        void outletsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {

            try
            {
                if (viewModel.TinDeregistrationData.ADregOpt == "3")
                {
                    viewModel.outletEditIsVisible = true;
                    selectedItem = e.AddedItems[0] as OutletSetResult;
                    viewModel.SelectedOutletForCloseTranser = selectedItem;
                    viewModel.SelectedPermitOutletOptionIndex = viewModel.AllOutlets.IndexOf(selectedItem);

                    viewModel.AddPermitOutletDecisionOptions();
                    viewModel.AddPopUpPage();
                    var view = sender as SfListView;
                    view.SelectedItem = null;
                }
                else
                {
                    viewModel.outletEditIsVisible = false;


                }
            }
            catch (Exception)
            {



            }

        }

        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var result = await viewModel._dialogService.ShowMessage(AppResources.ZZZConfirmationMsg, AppResources.VatDeregistrationVoidMessage, AppResources.ZYes, AppResources.ZNo);
            if (!result)
            {
                if (viewModel.TinDeregistrationData != null)
                {

                    if (viewModel.TinDeregistrationData.Fbnum != string.Empty)
                    {
                        await viewModel.VoidForm();
                    }
                    else
                    {
                        viewModel._navigationService.GoBack();
                    }
                }
            }
        }







        void newName_Clicked(object sender, EventArgs e)
        {
            if (viewModel.TinDeregistrationData.ADregOpt == "3")
            {
                viewModel.outletEditIsVisible = true;
                viewModel.SelectedOutletForCloseTranser = selectedItem;
                viewModel.SelectedPermitOutletOptionIndex = viewModel.AllOutlets.IndexOf(selectedItem);
                viewModel.SingleDeregistrationDate = string.Empty;
                viewModel.AddPermitOutletDecisionOptions();
                viewModel.AddPopUpPage();
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            else
            {
                viewModel.outletEditIsVisible = false;
            }
        }

        private async void attachmentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                viewModel.SelectedAttachment = e.SelectedItem as TinDeregestrationAttachmentsModel;
                viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);
                await viewModel.NewAttachmentClicked();
                var view = sender as SfListView;
                view.SelectedItem = null;
            }
            catch (Exception)
            {


            }
        }

        private void EditOutlet_Tapped(object sender, TappedEventArgs e)
        {
            try
            {

                OutletSetResult selectedOutlet = (OutletSetResult)(e as TappedEventArgs).Parameter;
                if (viewModel.TinDeregistrationData.ADregOpt == "3")
                {
                    viewModel.outletEditIsVisible = true;
                    selectedItem = selectedOutlet;
                    viewModel.SelectedOutletForCloseTranser = selectedItem;
                    viewModel.SelectedPermitOutletOptionIndex = viewModel.AllOutlets.IndexOf(selectedItem);

                    viewModel.AddPermitOutletDecisionOptions();
                    viewModel.AddPopUpPage();
                    var view = sender as SfListView;
                    view.SelectedItem = null;
                }
                else
                {
                    viewModel.outletEditIsVisible = false;


                }
            }
            catch (Exception)
            {



            }
        }

        private async void AddAttachment_Tapped(object sender, EventArgs e)
        {
            try
            {
                var obj1 = viewModel.TinDeregistrationData.AttDetSet;
                TinDeregestrationAttachmentsModel selectedOutlet = (TinDeregestrationAttachmentsModel)(e as TappedEventArgs).Parameter;
                viewModel.SelectedAttachment = selectedOutlet;
                var obj = viewModel.TinDeregistrationData.AttDetSet;
                await viewModel.NewAttachmentClicked();
            }
            catch (Exception)
            {

                return;
            }
        }


        private void DeleteAttachment_Tapped(object sender, EventArgs e)
        {
            try
            {
                Attachment selectedAttachment = (Attachment)(e as TappedEventArgs).Parameter;
                var list = viewModel.AttachmentsListViewData.Where(p => p.AttachmentTypeList.Any(q => q.Filename == selectedAttachment.Filename)).Select(f => f.AttachmentTypeList).FirstOrDefault();
                list.Remove(selectedAttachment);
                list = new List<Attachment>(list);
                string results = UploadAttachementsWebServiceManager.GAZTGenericDeleteAttachment(selectedAttachment.Filename, viewModel.TinDeregistrationData.CaseGuid, "", selectedAttachment.Doguid);
                if (results == "X")
                {
                    foreach (Attachment attachment in viewModel.TinDeregistrationData.AttDetSet)
                    {
                        if (attachment.Doguid.Equals(selectedAttachment.Doguid))
                        {
                            viewModel.TinDeregistrationData.AttDetSet.Remove(attachment);
                            break;
                        }
                    }

                    foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in viewModel.AttachmentsListViewData)
                    {
                        foreach (Attachment attachment in attachmentsModelsTemp.AttachmentTypeList)
                        {
                            if (attachment.Doguid.Equals(selectedAttachment.Doguid))
                            {
                                attachmentsModelsTemp.AttachmentTypeList.Remove(attachment);
                                break;
                            }
                        }
                    }

                    viewModel.AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(viewModel.AttachmentsListViewData);
                }
            }
            catch (Exception)
            {


                return;
            }
        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                StackLayout lblClicked = (StackLayout)sender;
                var item = (TapGestureRecognizer)lblClicked.GestureRecognizers[0];
                var id = item.CommandParameter;

                OutletSetResult selectedOutlet = (OutletSetResult)lblClicked.BindingContext; //(OutletSetResult)(e as TappedEventArgs).Parameter;
                if (viewModel.TinDeregistrationData.ADregOpt == "3")
                {
                    viewModel.outletEditIsVisible = true;
                    selectedItem = selectedOutlet;
                    viewModel.SelectedOutletForCloseTranser = selectedItem;
                    viewModel.SelectedPermitOutletOptionIndex = viewModel.AllOutlets.IndexOf(selectedItem);

                    viewModel.AddPermitOutletDecisionOptions();
                    var viewModel1 = JsonConvert.SerializeObject(viewModel);
                    var list = new List<TINDeregistrationPageViewModel>();
                    foreach (var item1 in viewModel.SelectedOutletForCloseTranser.PermitTypes)
                    {
                        item1.APermitDeregDisplayDobDate = item1.APermitDobHTb;
                    }
                    list.Add(viewModel);
                    viewModel.AddPopUpPage(list);
                    var view = sender as SfListView;
                    view.SelectedItem = null;
                }
                else
                {
                    viewModel.outletEditIsVisible = false;


                }
            }
            catch (Exception)
            {



            }
        }

        private void attachmentsListViewChild_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                int childElements = 0;
                foreach (var item in viewModel.AttachmentsListViewData)
                {
                    childElements += item.AttachmentTypeList != null && item.AttachmentTypeList.Count > 0 ? item.AttachmentTypeList.Count : 0;
                }
                if (((StackLayout)sender).Height > 0)
                    attachmentsListView.HeightRequest = (viewModel.AttachmentsListViewData.Count + childElements) * ((StackLayout)sender).Height;
            }
            catch (Exception)
            {


            }
        }

        private void OnTinEntered(object sender, EventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Entry)sender;
                if (senderObj.Text.Equals(App.LoginDataRetrieved.TIN))
                {
                    TINNumber.HasError = true;
                    viewModel.IsEnteredTINValid = false;
                }
                else
                {
                    TINNumber.HasError = false;
                }

            }
            catch (Exception)
            {


            }
        }

        private async void OnTinRegistrationReasonTapped(object sender, EventArgs e)
        {
            await viewModel.OnTinRegisrtationReasonClicked();
        }

    }
}
