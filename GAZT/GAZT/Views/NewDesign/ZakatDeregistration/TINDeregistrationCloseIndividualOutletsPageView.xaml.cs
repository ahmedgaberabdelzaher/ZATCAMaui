using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class TINDeregistrationCloseIndividualOutletsPageView : PopupPage
    {
        TINDeregistrationPageViewModel viewModel;

        public TINDeregistrationCloseIndividualOutletsPageView(string reasonDesc, TINDeregistrationPageViewModel tINDeregistrationPageViewModel)
        {
            InitializeComponent();
            tINDeregistrationPageViewModel.SelectedPermitTypeOutletOption = null;
            tINDeregistrationPageViewModel.IsMultiplePermitsVisible = false;
            viewModel = tINDeregistrationPageViewModel;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            // viewModel.TinDeregistrationData = tinDeregistrationResponseModel;
            this.BindingContext = viewModel;
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
        void GetSelectedDataTemplate()
        {
            var captionStyle = Resources["CaptionLabelBlack"] as Style;
            Grid cardView = new Grid() { HeightRequest = 100 };
            Grid grid = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, ColumnSpacing = 20, RowSpacing = 10 };
            Image image = new Image() { Source = ImageSource.FromFile("vat_tile_listofsignup"), Aspect = Aspect.Fill, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            Label label = new Label() { HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.EndAndExpand, Style = captionStyle, Text = ((TINDeregistrationModel)outletDecisionOptionsListView.SelectedItem).ActiveOutletDecisionOptions, Margin = new Thickness(20, 0, 20, 20), TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Start };
            grid.Children.Add(image);
            grid.Children.Add(label);

            cardView.Children.Add(grid);
            outletDecisionOptionsListView.SelectedItemTemplate = new DataTemplate(() => new ViewCell { View = cardView });
            if (viewModel.SelectedPermitOutletOptionIndex == 2)
            {
                viewModel.outletEditIsVisible = true;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            }
            else if (viewModel.SelectedPermitOutletOptionIndex == 1)
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
            }
            else
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            }

            int index = Convert.ToInt16(viewModel.SelectedPermitOutletOptionIndex);
            viewModel.IsPermitOption1Visible = index == 0 ? true : false;
            viewModel.IsPermitOption2Visible = index == 1 ? true : false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });

            
            MessagingCenter.Subscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption", (arg) =>
            {
                GetSelectedDataTemplate();
            });
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
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel = null;

            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            GC.Collect(1);
        }
        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel?.SelectedIdNumber))
            {
                viewModel.FrameIDError = false;
            }
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            viewModel.SelectedPermitOutletOptionIndex = viewModel.PermitOutletDecisionOptions.IndexOf(selectedItem);

            if (viewModel.SelectedPermitOutletOptionIndex == 2)
            {
                if (viewModel.SelectedOutletForCloseTranser != null)
                {
                    if (viewModel.SelectedOutletForCloseTranser.PermitTypes != null)
                    {
                        if (viewModel.SelectedOutletForCloseTranser.PermitTypes.Count > 0)
                        {
                            viewModel.IsNodataAvailableVisible = false;

                            viewModel.IsMultiplePermitsVisible = true;
                        }
                        else
                        {
                            viewModel.IsNodataAvailableVisible = true;

                        }
                    }
                    else
                    {
                        viewModel.IsNodataAvailableVisible = true;
                    }
                }
            }
            else
            {
                viewModel.IsNodataAvailableVisible = false;
                viewModel.IsMultiplePermitsVisible = false;
                if (viewModel.SelectedPermitOutletOptionIndex == 1)
                {
                    viewModel.FirstNameLbl = AppResources.ZZZVATRFirstName;
                    viewModel.SurnameNameLbl = AppResources.TinDeregistrationSurName;
                }
            }
            GetSelectedDataTemplate();

        }

        private void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(viewModel.SelectedIdtype))
                {
                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID)
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
                                    viewModel.ValidateIDNumberForIndiviualOutlets();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
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
                                    viewModel.ValidateIDNumberForIndiviualOutlets();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
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
                            viewModel.ValidateIDNumberForIndiviualOutlets();
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationCompanyID)
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
                        else if (viewModel.SelectedIdNumber.Length > 10)
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.CompanyIDlengthis10digit;
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
                            viewModel.ValidateIDNumberForIndiviualOutlets();
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

        void BorderlessEntryPermittype_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();

            var cell = outletsListView.Children.FirstOrDefault();
            var entry = (Xamarin.Forms.Entry)cell.FindByName("EntryTINPermitType");

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (entry.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    entry.Focus();
                }
                if (entry.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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
                    entry.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameTinError = false;
                    viewModel.ValidateIdNumberForPermitTypes(entry.Text);
                }
            }
            else
            {
                viewModel.FrameTinError = true;
                Messages.Append(AppResources.AccountUnlockedCompleteRequiedFields);

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
                entry.Text = string.Empty;
            }
        }

        async void BorderlessTINEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
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
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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

                    await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameTinError = false;
                    await viewModel.ValidateIdNumberFromApi(EntryTIN.Text);
                }
            }
            else
            {
                viewModel.FrameTinError = true;
                Messages.Append(AppResources.AccountUnlockedCompleteRequiedFields);

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

                await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                EntryTIN.Text = string.Empty;
            }
        }

        async void TapGestureRecognizerSingleDeregDate_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregOutletSingleDatePicker";

                try
                {
                    datepickermessagecenter();
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {

                }
                catch (InternetException ex)
                {

                }
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                });
            }
        }

        private void datepickermessagecenter()
        {
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (senderDate, arg) =>
            {
                if (arg.PickerId == "DeregDatePicker")
                {
                    viewModel.DeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "DeregPermitOutletDatePicker")
                {
                    viewModel.SingleOutletDeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "PermitTypeDobPickerDateTypePicker")
                {
                    viewModel.PermitDob = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "_DOBDateTypePicker")
                {
                    viewModel.SelectedDob = arg.SelectedValue;
                    viewModel.PkrDBO = arg.SelectedValue;
                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID || viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (!String.IsNullOrEmpty(viewModel.SelectedIdNumber))
                        {
                            //await Task.Delay(700);
                            viewModel.ValidateIDNumberForIndiviualOutlets();
                        }
                    }
                }

                if (arg.PickerId == "DeregOutletSingleDatePicker")
                {
                    viewModel.SingleDeregistrationDate = arg.SelectedValue;//Convert.ToDateTime(arg.SelectedValue).ToString("dd/MM/yyyy");
                }

                MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            });
        }

        async void TapGestureRecognizerSelectSingleOutleDate_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregPermitOutletDatePicker";

                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeDeRegisrtationReasonDateTapped.Execute(parameterVal);
                }

                try
                {
                    datepickermessagecenter();
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {

                }
                catch (InternetException ex)
                {

                }
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                });
            }
        }

        async void TapGestureRecognizerSelectPermitType_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                List<string> reasonData = new List<string>();
                reasonData.Add(AppResources.TinDeregistrationClosed);
                reasonData.Add(AppResources.TinDeregistrationTransfer);

                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeReasonTapped.Execute(parameterVal);
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = reasonData;
                genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "permitTypeReasonPicker";

                await PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InternetException ex)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            viewModel.OnIdTypeClicked();
        }

        async void IndiviualTinRegistrationDOBTapped(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(idNumber.Text))
            {
                idNumber.Focus();
                return;
            }
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "_DOBDateTypePicker";
            try
            {
                datepickermessagecenter();
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException)
            {

            }
        }

        async void OnIndiviualOutletTinRegistrationDOBTapped(System.Object sender, System.EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "PermitTypeDobPickerDateTypePicker";
            try
            {
                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeDeRegisrtationReasonDateTapped.Execute(parameterVal);
                }
                datepickermessagecenter();
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }

        }
    }
}
