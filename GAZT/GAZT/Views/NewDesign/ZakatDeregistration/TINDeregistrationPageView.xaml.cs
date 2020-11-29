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
using GalaSoft.MvvmLight.Ioc;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Cards;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

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
            // ChangeArrowDirection();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel.TinDeregistrationData = tinDeregistrationResponseModel;
            //viewModel.AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>();
            this.BindingContext = viewModel;
            viewModel.LoadReasonSet();
            viewModel.PopulateAttachmentsListViewTemplate();

            //MessagingCenter.Subscribe<TINDeregistrationModel>(this, "selectedOutletOption", (x) =>
            //{
            //    outletDecisionOptionsListView.SelectedItem = x;

            //});
            //Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            //{
            //    if (arg != null)
            //    {
            //        viewModel.PopulateAttachments(arg.results);
            //    }
            //});
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

            SetDatePickerFont();
            SetDateOfBirthPickerFont();
            SetHijriDateOfBirthPickerFont();
            SetHijriDateOfBirth2PickerFont();
            SetTodayDatePickerFont();
            SetTodayDateHijriPickerFont();

            viewModel.IsReasonViewEnabled = true;
            viewModel.IsOutletViewEnabled = false;
            viewModel.IsAttachmentsEnabled = false;
            viewModel.IsDeclarationViewEnabled = false;
            viewModel.IsSummaryViewEnabled = false;
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

            MessagingCenter.Subscribe<TINDeregistrationPageViewModel, bool>(this, "EnableOutletContinueButton", (sender, args) =>
            {
                btnOutletContinue.IsEnabled = args;
            });

            // viewModel.EnableOutletDetaislView();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;

                if (arg.PickerId == "reasonPicker")
                {
                    viewModel.TinDeregistrationData.AttDetSet.Results = new List<Attachment>();
                    if (arg.SelectedValue == string.Empty)
                    {
                        viewModel.TinDeregistrationData.AttDetSet.Results = new List<Attachment>();
                        viewModel.IsOption1Visible = false;
                        FrmDBO.IsVisible = false;
                        CalLabel.IsVisible = false;
                        DateLabel.IsVisible = false;
                        outletDecisionOptionsListView.IsVisible = false;
                        viewModel.IsOption2Visible = false;
                    }
                    else
                    {

                        FrmDBO.IsVisible = true;
                        CalLabel.IsVisible = true;
                        DateLabel.IsVisible = true;
                        outletDecisionOptionsListView.IsVisible = true;

                        viewModel.IsOption2Visible = false;
                        viewModel.IsOption1Visible = false;
                        viewModel.AddOutletDecisionOptions();
                    }
                }


                Console.WriteLine(arg);
              
            });

            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null && arg.results != null && arg.results.Count > 0)
                {
                    viewModel.TinDeregistrationData.AttDetSet.Results = arg.results;
                    viewModel.PopulateAttachments(null);

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

        private async void SetDate()
        {
            await viewModel.SetDefaultDate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            outletDecisionOptionsListView.SelectedItem = null;
            //SimpleIoc.Default.Unregister<TINDeregistrationPageViewModel>();
            //SimpleIoc.Default.Register<TINDeregistrationPageViewModel>();

            Xamarin.Forms.MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel, bool>(this, "EnableOutletContinueButton");
            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");
            GC.Collect();
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
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
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

            //if (viewModel.SelectedOutletOptionIndex == 1)
            //{
            //    viewModel.NationalTypeSelected();
            //}
            if(selectedItem.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                  viewModel.NationalTypeSelected();
            }

            viewModel.IsOption1Visible = index == 0 ? true : false;
            viewModel.IsOption2Visible = index == 1 ? true : false;

            if (viewModel.IsOption2Visible == true)
            {
                viewModel.PickerDobToDisplay = string.Empty;
                viewModel.TINNumber = string.Empty;
                viewModel.SelectedIdtype = string.Empty;
                viewModel.SelectedIdNumber = string.Empty;
                viewModel.PickerDOBDateDisplay = string.Empty;

                if (viewModel.FirstNameFromIdType != null)
                {
                    viewModel.FirstNameFromIdType = string.Empty;
                }

                if (viewModel.IDTypeDataModel == null)
                {
                    viewModel.IDTypeDataModel = new VATSignUpD();
                }
                else
                {
                    viewModel.IDTypeDataModel.Name1 = string.Empty;
                    viewModel.IDTypeDataModel.Name2 = string.Empty;
                    viewModel.IDTypeDataModel.FatherName = string.Empty;
                    viewModel.IDTypeDataModel.GrandfatherName = string.Empty;
                    viewModel.IDTypeDataModel.FamilyName = string.Empty;
                }
            }
            else if (viewModel.IsOption1Visible == true)
            {
                if (viewModel.FirstNameFromIdType != null)
                {
                    viewModel.FirstNameFromIdType = string.Empty;
                }

                viewModel.PickerDobToDisplay = string.Empty;
                //viewModel.TodayDate = new ObservableCollection<object>();
                //viewModel.TodayDateinHijri = new ObservableCollection<object>();
            }
            else
            {
                if (viewModel.FirstNameFromIdType != null)
                {
                    viewModel.FirstNameFromIdType = string.Empty;
                }

                viewModel.PickerDobToDisplay = string.Empty;
                viewModel.TINNumber = string.Empty;
                viewModel.SelectedIdtype = string.Empty;
                viewModel.SelectedIdNumber = string.Empty;
                viewModel.PickerDOBDateDisplay = string.Empty;

                if (viewModel.IDTypeDataModel == null)
                {
                    viewModel.IDTypeDataModel = new VATSignUpD();
                }
                else
                {
                    viewModel.IDTypeDataModel.Name1 = string.Empty;
                    viewModel.IDTypeDataModel.Name2 = string.Empty;
                    viewModel.IDTypeDataModel.FatherName = string.Empty;
                    viewModel.IDTypeDataModel.GrandfatherName = string.Empty;
                    viewModel.IDTypeDataModel.FamilyName = string.Empty;
                }
                viewModel.PickerDobToDisplay = string.Empty;
                //viewModel.TodayDate = new ObservableCollection<object>();
                //viewModel.TodayDateinHijri = new ObservableCollection<object>();
            }
            GetSelectedDataTemplate(selectedItem.ActiveOutletDecisionOptions.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle));
        }
        void GetSelectedDataTemplate(bool isIndex1 = false)
        {
            var captionStyle = Resources["CaptionLabelBlack"] as Style;
            Grid cardView = new Grid() { HeightRequest = 100 };
            Grid grid = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, ColumnSpacing = 20, RowSpacing = 10 };
            Image image = new Image() { Source = ImageSource.FromFile("vat_tile_listofsignup"), Aspect = Aspect.Fill, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            Label label = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Style = captionStyle,
                Text = ((TINDeregistrationModel)outletDecisionOptionsListView.SelectedItem).ActiveOutletDecisionOptions,
                Margin = new Thickness(20, 0, 20, 20),
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Start
            };
            grid.Children.Add(image);
            grid.Children.Add(label);

            cardView.Children.Add(grid);
            outletDecisionOptionsListView.SelectedItemTemplate = new DataTemplate(() => new ViewCell { View = cardView });
            if (viewModel.SelectedOutletOptionIndex == 2)
            {
                viewModel.outletEditIsVisible = true;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
            }
            else if (viewModel.SelectedOutletOptionIndex == 1)
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
            }
            else
            {
                viewModel.outletEditIsVisible = false;
                viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
            }

            //int index = Convert.ToInt16(viewModel.SelectedOutletOptionIndex);
            //if (isIndex1)
            //{
            //    index = 1;
            //}
            if (viewModel.SelectedOutletOption.Equals(AppResources.TinDeregistrationTransferAllOutletsToSingle))
            {
                viewModel.IsOption2Visible = true;
            }
            //viewModel.IsOption1Visible = index == 0 ? true : false;
            //viewModel.IsOption2Visible = index == 1 ? true : false;
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
                                if (!string.IsNullOrEmpty(viewModel.PickerDOBDateDisplay))
                                {
                                    viewModel.ValidateIDNumber();
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
                                    viewModel.ValidateIDNumber();
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
        private void HijriCalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
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
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerDobToDisplay = viewModel.PkrDBO; //DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");



                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }

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
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);

                        viewModel.PickerDobToDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void HijriCal2Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (DpDboHijri2.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerDobToDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }

                }
                else
                {
                    if (DpDbo2.SelectedItem != null)
                    {
                        var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerDobToDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDobToDisplay = string.Empty;

                    }
                }


            }
            catch (Exception ex)
            {
            }
        }

        private void HijriCal3Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsDOBHijriCal)
                {
                    if (DpDboHijri3.SelectedItem != null)
                    {
                        var selectedItem = DpDboHijri.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        viewModel.PkrDBO = year + "/" + month + "/" + day;
                        viewModel.SelectedDob = day + "/" + month + "/" + year;
                        viewModel.PickerDOBDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDOBDateDisplay = string.Empty;

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
                        viewModel.SelectedDob = day + "/" + month + "/" + year;
                        viewModel.PickerDOBDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerDOBDateDisplay = string.Empty;

                    }
                }


            }
            catch (Exception ex)
            {
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
            DateTime permitDate;
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
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(viewModel.SelectedDob) && DateTime.Parse(date) < DateTime.Parse(viewModel.SelectedDob))
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            viewModel.PickerDOBDateDisplay = "";
                            viewModel.SelectedDob = "";
                        }

                        viewModel.PkrDBO = date;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerDobToDisplay = date;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

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
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(viewModel.SelectedDob) && DateTime.Parse(date) < DateTime.Parse(viewModel.SelectedDob))
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            viewModel.PickerDOBDateDisplay = "";
                            viewModel.SelectedDob = "";
                        }

                        viewModel.PkrDBO = date;
                        viewModel.DeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);

                        viewModel.PickerDobToDisplay = viewModel.PkrDBO; //DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    isHIjri = false;
                }

                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(viewModel.TinDeregistrationData.PermitSet.Results);
                string sortedDate = string.Empty;
                string datetype = string.Empty;
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    sortedDate = allPermitTypes.OrderBy(x => x.APermitValfrDtHTb).Select(x => x.APermitValfrDtHTb).FirstOrDefault();
                    datetype = permitInfo.APermitValfrDtCTb;
                }
                if (datetype.Contains("H"))
                {
                    string convertedSortedDate = UtilityManager.HijriToGreg(sortedDate);
                    permitDate = Convert.ToDateTime(convertedSortedDate);
                }
                else
                {
                    permitDate = Convert.ToDateTime(sortedDate);

                }

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
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && viewModel.DeregistrationDate != null && DateTime.Parse(date) > viewModel.DeregistrationDate)
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            return;
                        }
                        viewModel.PkrDBO = date;
                        viewModel.SelectedDob = date;
                        viewModel.PickerDOBDateDisplay = viewModel.PkrDBO;

                        viewModel.ValidateIDNumber();

                    }
                }
                else
                {
                    if (DpDbo3.SelectedItem != null)
                    {
                        var selectedItem = DpDbo3.SelectedItem as ObservableCollection<object>;
                        string month = selectedItem[1].ToString();
                        string day = selectedItem[0].ToString();
                        string year = selectedItem[2].ToString();
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && viewModel.DeregistrationDate != null && DateTime.Parse(date) > viewModel.DeregistrationDate)
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            return;
                        }
                        viewModel.PkrDBO = date;
                        viewModel.SelectedDob = date;
                        viewModel.PickerDOBDateDisplay = viewModel.PkrDBO;
                        viewModel.ValidateIDNumber();

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
                        Messages.Append(" " + AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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
               // viewModel.FrameTinError = true;
                //Messages.Append(AppResources.ZZPleasefillallthemandatoryfields);

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

              //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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

        private void attachmentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.SelectedAttachment = e.SelectedItem as TinDeregestrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);
            viewModel.NewAttachmentClicked();
            var view = sender as SfListView;
            view.SelectedItem = null;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddAttachment_Tapped(object sender, EventArgs e)
        {
            try
            {
                TinDeregestrationAttachmentsModel selectedOutlet = (TinDeregestrationAttachmentsModel)(e as TappedEventArgs).Parameter;
                viewModel.SelectedAttachment = selectedOutlet;
                //viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);
                viewModel.NewAttachmentClicked();
            }
            catch (Exception ex)
            {

                return;
            }
        }

        private async void DeleteAttachment_Tapped(object sender, EventArgs e)
        {
            try
            {
                Attachment selectedAttachment = (Attachment)(e as TappedEventArgs).Parameter;
                var list = viewModel.AttachmentsListViewData.Where(p => p.AttachmentTypeList.Any(q => q.Filename == selectedAttachment.Filename)).Select(f => f.AttachmentTypeList).FirstOrDefault();
                list.Remove(selectedAttachment);
                list = new List<Attachment>(list);
                // viewModel.PopulateAttachments(viewModel.AttachmentTypeList);
                //   viewModel.AttachmentsListViewData = JsonConvert.DeserializeObject<List<TinDeregestrationAttachmentsModel>>(viewModel.attachmentsListViewDataString);
                string results = WebServiceManager.GAZTGenericDeleteAttachment(selectedAttachment.Filename, viewModel.TinDeregistrationData.CaseGuid, "", selectedAttachment.Doguid);
                if (results == "X")
                {
                    foreach (TinDeregestrationAttachmentsModel attachmentsModelsTemp in viewModel.AttachmentsListViewData)
                    {
                        //   UploadedAttachmentFileType = attachmentsModelsTemp.FieldTitle;
                        if (selectedAttachment.Dotyp == attachmentsModelsTemp.DocType && attachmentsModelsTemp.AttachmentTypeList.Any(p => p.Filename == selectedAttachment.Filename && p.Dotyp == selectedAttachment.Dotyp))
                        {
                            var index = attachmentsModelsTemp.AttachmentTypeList.Where(p => p.Filename == selectedAttachment.Filename && p.Dotyp == selectedAttachment.Dotyp).FirstOrDefault();
                            if (index != null)
                                attachmentsModelsTemp.AttachmentTypeList.Remove(index);
                        }
                    }
                    viewModel.AttachmentsListViewData = new List<TinDeregestrationAttachmentsModel>(viewModel.AttachmentsListViewData);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
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

        private void attachmentsListViewChild_BindingContextChanged(object sender, EventArgs e)
        {
            int childElements = 0;
            foreach (var item in viewModel.AttachmentsListViewData)
            {
                childElements += item.AttachmentTypeList != null && item.AttachmentTypeList.Count > 0 ? item.AttachmentTypeList.Count : 0;
            }
            if (((StackLayout)sender).Height > 0)
                attachmentsListView.HeightRequest = (viewModel.AttachmentsListViewData.Count + childElements) * ((StackLayout)sender).Height;
        }
    }
}
