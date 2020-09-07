using System;
using System.Collections.Generic;
using System.Text;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatDeregistration
{
    public partial class TINDeregistrationCloseIndividualOutletsPageView : PopupPage
    {
        TINDeregistrationPageViewModel viewModel;

        public TINDeregistrationCloseIndividualOutletsPageView( string reasonDesc)
        {
            InitializeComponent();

            viewModel = App.Locator.TINDeregistrationPageView;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
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

                    if (viewModel.SelectedIdtype == AppResources.NationaID || viewModel.SelectedIdtype == AppResources.ZZIqamaID)
                    {
                        if (!String.IsNullOrEmpty(viewModel.SelectedIdNumber))
                        {
                            //viewModel.ValidateIDNumber();
                        }
                    }
                }

                Console.WriteLine(arg);
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

        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.SelectedIdNumber))
            {
                viewModel.FrameIDError = false;
            }
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
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
                EntryTIN.Text = string.Empty;
            }
        }
    }
}
