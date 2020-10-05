using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class TINDeregistrationPageView : ContentPage
    {
        TINDeregistrationPageViewModel viewModel;
        OutletSetResult selectedItem;
        public TINDeregistrationPageView(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregistrationPageView;

            ChangeAeroIcon();
            SetLTR();
            ChangeArrowDirection();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel.TinDeregistrationData = tinDeregistrationResponseModel;
            this.BindingContext = viewModel;

            
            MessagingCenter.Subscribe<TINDeregistrationModel>(this, "selectedOutletOption", (x) =>
            {
                outletDecisionOptionsListView.SelectedItem = x;

            });
            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.PopulateAttachments(arg.results);
                }
            });

            SetDatePickerFont();
            SetDateOfBirthPickerFont();
            SetHijriDateOfBirthPickerFont();
            SetHijriDateOfBirth2PickerFont();
            SetTodayDatePickerFont();
            SetTodayDateHijriPickerFont();
        }

        public void ChangeArrowDirection()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ChangeArrowDirection();

            SetDate();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                if (arg.SelectedValue == string.Empty)
                {
                    viewModel.IsOption1Visible = false;
                    FrmDBO.IsVisible = false;
                    CalLabel.IsVisible = false;
                    DateLabel.IsVisible = false;
                    outletDecisionOptionsListView.IsVisible = false;
                    viewModel.IsOption2Visible = false;

                }
                else
                {
                    viewModel.IsOption1Visible = true;
                    FrmDBO.IsVisible = true;
                    CalLabel.IsVisible = true;
                    DateLabel.IsVisible = true;
                    outletDecisionOptionsListView.IsVisible = true;
                    viewModel.IsOption2Visible = true;
                }
                Console.WriteLine(arg);
            });

            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.TinDeregistrationData.AttDetSet.Results = arg.results;
                    viewModel.PopulateAttachments(arg.results);

                    foreach (Attachment attachment in viewModel.TinDeregistrationData.AttDetSet.Results)
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
                        if (!String.IsNullOrEmpty(viewModel.SelectedIdNumber))
                        {
                            viewModel.ValidateIDNumber();
                        }
                    }
                }
                Console.WriteLine(arg);
            });

            viewModel.LoadReasonSet();
            viewModel.PopulateAttachmentsListViewTemplate();

            if (viewModel.TinDeregistrationData.ADregOpt == "3")
            {
                viewModel.outletEditIsVisible = true;
            }
            else
            {
                viewModel.outletEditIsVisible = false;
            }
        }

        private async void SetDate()
        {
            await viewModel.SetDefaultDate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void SfListView_ItemTapped(System.Object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //try
            //{
            //    foreach (TINDeregistrationModel tINDeregistrationModel in viewModel.OutletDecisionOptions)
            //    {
            //        tINDeregistrationModel.ActiveOutletDecisionOptionsIsSelected = false;
            //    }

            //    var dataItem = e.ItemData as TINDeregistrationModel;
            //    dataItem.ActiveOutletDecisionOptionsIsSelected = true;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            int index = Convert.ToInt16(selectedItem.OutletOptionIndex) - 1;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            viewModel.IsOption1Visible = index == 0 ? true : false;
            viewModel.IsOption2Visible = index == 1 ? true : false;
        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            viewModel.SelectedAttachment = e.AddedItems[0] as TinDeregestrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);
            viewModel.NewAttachmentClicked();
            var view = sender as SfListView;
            view.SelectedItem = null;

            //if (viewModel.SelectedAttachment.IsAttachmentAttached == true)
            //{
            //    viewModel.SelectedAttachment.AttachmentName = string.Empty;
            //    viewModel.SelectedAttachment.IsAttachmentAttached = false;
            //}
            //else
            //{
            //    //attachmentsListView.SelectedItems.Clear();
            //    //viewModel.AddAttachmentEx();
            //}
        }
        private void EntryMobileNo_Unfocused(object sender, FocusEventArgs e)
        {

            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.TinDeregistrationData.ADecTelNo))
            {
                if (viewModel.TinDeregistrationData.ADecTelNo.Substring(0, 1) != "5")
                {
                    popUp.Message = AppResources.ZZMobilenumberhastostartwithnumber5;
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

                }
                else
                {
                    if (viewModel.TinDeregistrationData.ADecTelNo.Length != 10)
                    {
                        if (viewModel.TinDeregistrationData.ADecTelNo.Length < 9)
                        {
                           // popUp.Message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
                            Messages.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }

                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                           
                        }
                    }
                }
              

            }

        }
        private void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(viewModel.SelectedIdtype))
                {
                    if (viewModel.SelectedIdtype == AppResources.NationaID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                            //ZZPleaseenteravalidNationalID
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
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.SelectedDob))
                                {
                                    viewModel.ValidateIDNumber();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.ZZIqamaID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.SelectedDob))
                                {
                                    viewModel.ValidateIDNumber();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.ZZGCCID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else if (!(viewModel.SelectedIdNumber.Length <= 15 && viewModel.SelectedIdNumber.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                            //EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumber();
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.ZIBANCompanyID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "7")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.TinDeregistrationCompanyIDCheck;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumber();
                        }
                    }
                }
                else
                {
                    viewModel.FrameIDError = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.SelectedIdNumber))
            {
                viewModel.FrameIDError = false;
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
            if (!viewModel.IsDOBHijriCal)
            {
                DpDbo3.IsOpen = true;
            }
            else
            {
                DpDboHijri3.IsOpen = true;
            }
        }
        private void DpDbo_Closed(object sender, EventArgs e)
        {
            bool isHIjri;
            DateTime deregDate;
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (DpDboHijri.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);

                    }
                    isHIjri = true;
                }
                else
                {
                    if (DpDbo.SelectedItem != null)
                    {
                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.PickerDobToDisplay = day + "/" + month + "/" + year;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);


                    }
                    isHIjri = false;
                }

                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(viewModel.TinDeregistrationData.PermitSet.Results);
                string sortedDate = string.Empty;
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    sortedDate  = allPermitTypes.OrderBy(x => x.APermitValfrDtHTb).Select(x => x.APermitValfrDtHTb).FirstOrDefault();

                }

                string convertedSortedDate = UtilityManager.HijriToGreg(sortedDate);
                DateTime permitDate = Convert.ToDateTime(convertedSortedDate);

                if (isHIjri)
                {
                    string convertedDeregDate = UtilityManager.HijriToGreg(viewModel.PkrDBO);
                     deregDate = Convert.ToDateTime(convertedDeregDate);

                }
                else
                {
                     deregDate = Convert.ToDateTime(viewModel.PkrDBO);


                }


                if (deregDate < permitDate)
                {
                    viewModel._dialogService.ShowMessage(AppResources.TinDeregistrationDateValidationMessage, AppResources.Information);

                }

            }
            catch (Exception ex)
            {
            }

        }
        private void DpDOB_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsDOBHijriCal)
                {
                    if (DpDboHijri3.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri3.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.PickerDOBDateDisplay = day + "/" + month + "/" + year;
                        viewModel.SelectedDob= day + "/" + month + "/" + year;
                    }
                }
                else
                {
                    if (DpDbo3.SelectedItem != null)
                    {
                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.PickerDOBDateDisplay = day + "/" + month + "/" + year;
                        viewModel.SelectedDob = day + "/" + month + "/" + year;


                    }
                }


            }
            catch (Exception ex)
            {
            }

        }

        private void DpDOB_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            // FrmDBO.HasError = false;
            //try
            //{
            //    if (DpDbo.SelectedItem != null)
            //    {
            //        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
            //        string month = selectedItem[1].ToString();
            //        string day = selectedItem[0].ToString();
            //        string year = selectedItem[2].ToString();
            //        viewModel.PkrDBO = year + "/" + month + "/" + day;

            //    }

            //}
            //catch (Exception ex)
            //{
            //}
        }
        private void DpDbo_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            // FrmDBO.HasError = false;
            //try
            //{
            //    if (DpDbo.SelectedItem != null)
            //    {
            //        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
            //        string month = selectedItem[1].ToString();
            //        string day = selectedItem[0].ToString();
            //        string year = selectedItem[2].ToString();
            //        viewModel.PkrDBO = year + "/" + month + "/" + day;

            //    }

            //}
            //catch (Exception ex)
            //{
            //}
        }
    
        void BorderlessTINEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
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
                        Messages.Append(" "+ AppResources.ZZTINnumberlengthcannotbelessthan10digits);
                    }
                }
                if (Messages.Length > 0)
                {
                    popUp.Message = Messages.ToString();
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameTinError = false;
                    viewModel.ValidateIdNumberFromApi(EntryTIN.Text);
                }
            }
            else
            {
                viewModel.FrameTinError = true;
                Messages.Append(AppResources.ZZPleasefillallthemandatoryfields);

                popUp.Message = Messages.ToString();
                popUp.IsLinkAvailable = false;

                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }

                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                EntryTIN.Text = string.Empty;
            }
          
        }
        private void DOBDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsDOBHijriCal)
            {
                var selectedItem = DpDboHijri3.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.SelectedDob = date;


            }
            else
            {
                var selectedItem = DpDbo3.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = year + "/" + month + "/" + day;
                viewModel.SelectedDob = date;


            }
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsHijriCal)
            {
                var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.DeregistrationDate = Convert.ToDateTime(date);


            }
            else
            {
                var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
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
            if (viewModel.IsDOBHijriCal)
            {
                DpDboHijri3.IsOpen = true;
            }
            else
            {
                DpDbo3.IsOpen = true;
            }

        }
        private void DpDOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
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
        private void DpDbo_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
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
        private void DOBpicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //  ValidateIDNumber();
        }
        void outletsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VatDeregistrationVoidMessage, AppResources.ZNo, AppResources.ZYes);
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

        public void SetDatePickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDbo2.HeaderFontFamily = "SSTArabic-Medium";
                            DpDbo2.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDbo2.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDbo2.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDbo2.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo2.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo2.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo2.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                    break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SetDateOfBirthPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDbo.HeaderFontFamily = "SSTArabic-Medium";
                            DpDbo.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDbo.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDbo.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDbo.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SetHijriDateOfBirthPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDboHijri.HeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDboHijri.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDboHijri.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SetHijriDateOfBirth2PickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDboHijri2.HeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri2.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri2.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDboHijri2.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDboHijri2.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri2.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri2.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri2.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SetTodayDatePickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDbo3.HeaderFontFamily = "SSTArabic-Medium";
                            DpDbo3.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDbo3.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDbo3.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDbo3.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo3.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo3.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDbo3.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SetTodayDateHijriPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {
                    case Xamarin.Forms.Device.iOS:
                        {

                            DpDboHijri3.HeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri3.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpDboHijri3.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpDboHijri3.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        {
                            DpDboHijri3.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri3.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri3.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                            DpDboHijri3.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

        void newName_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.TinDeregistrationData.ADregOpt == "3")
            {
                viewModel.outletEditIsVisible = true;
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
    }
}
