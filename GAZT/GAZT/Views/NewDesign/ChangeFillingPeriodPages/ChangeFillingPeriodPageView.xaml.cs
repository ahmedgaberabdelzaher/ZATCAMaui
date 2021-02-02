using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using System;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.GenericPickers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using GAZT.Manager;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodPageView : ContentPage, ChangeFillingInterface
    {
        #region Variable

        ChangeFillingPeriodViewModel viewModel;

        #endregion

        public ChangeFillingPeriodPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                this.BindingContext = viewModel;
                viewModel.cFInterface = this;
                viewModel.ResetData();
                viewModel.GetVATChangeFillingData();
            }
            catch (Exception ex)
            {
            }
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

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            getYesCommand();
            getNoCommand();
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
                                break;
                            case ArButtons.عرضملاحظات:
                                break;
                            case ArButtons.المرفقات:
                                break;
                            case ArButtons.إلغاء:
                                viewModel.isDraftClicked = true;
                                viewModel.VoidMsg();
                                viewModel.isDraftClicked = false;
                                break;
                            case ArButtons.عادةتعيين:
                                break;
                            case ArButtons.تعديل:
                                break;
                            case ArButtons.حفظكمسودة:
                                viewModel.isDraftClicked = true;
                                viewModel.OnSaveDraftClicked();
                                viewModel.isDraftClicked = false;
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
                                break;
                            case Buttons.DisplayNotes:
                                break;
                            case Buttons.Attachments:
                                break;
                            case Buttons.Void:
                                viewModel.isDraftClicked = true;
                                viewModel.VoidMsg();
                                viewModel.isDraftClicked = false;
                                break;
                            case Buttons.Reset:
                                break;
                            case Buttons.Amend:
                                break;
                            case Buttons.SaveasDraft:
                                viewModel.isDraftClicked = true;
                                viewModel.OnSaveDraftClicked();
                                viewModel.isDraftClicked = false;
                                break;
                            default:
                                break;
                        }
                    }
                }
            });

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {

                    viewModel.PickedDate = arg.SelectedValue;
                    viewModel.ValidateIdNumber();
                });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                if (viewModel.selectedPicker == ChangeFillingPeriodViewModel.PickerEnum.EffectiveDate)
                {
                    viewModel.EffectiveDatePickerModel = arg;
                    viewModel.updateEffectiveDatePicker();
                }
                else if (viewModel.selectedPicker == ChangeFillingPeriodViewModel.PickerEnum.IdType)
                {
                    viewModel.IDTypePickerModel = arg;
                    viewModel.updateIdTypePicker();
                }
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

            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");

        }

        public async void getYesCommand()
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
                            viewModel.VATSetReturnVoidAsync();
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
            }
        }

        public async void getNoCommand()
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
                });
            }
            catch (Exception ex)
            {
            }
        }
        void outletDecisionOptionsListView_SelectionChanged(System.Object sender,
            Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ChangeFillingPeriodModel selectedItem = e.AddedItems[0] as ChangeFillingPeriodModel;
            viewModel.ShowAttachments = true;
            try
            {
                switch (viewModel.OutletDecisionOptions.IndexOf(selectedItem))
                {
                    case 0:
                        {
                            viewModel.IsTwoYearsAtachmentsVisible = true;
                            viewModel.IsMonthsAtachmentsVisible = false;
                            viewModel.IsOthersAtachmentsVisible = false;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                    case 1:
                        {

                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = true;
                            viewModel.IsOthersAtachmentsVisible = false;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                    case 2:
                        {
                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = false;
                            viewModel.IsOthersAtachmentsVisible = true;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs focusEventArgs)
        {
            viewModel.ValidateIdNumber();
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel._idNumber = e.NewTextValue;
        }

        //private void OnFrequechCheckChanged(object sender, CheckedChangedEventArgs e)
        //{
        //    viewModel.EnableFrequencyDetails();
        //}

        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclaration();
        }

        private void CheckBox_CheckedChanged(System.Object sender, System.Boolean e)
        {
            viewModel.EnableDeclaration();
        }
        public void SelectDefaultAttachOption(int index)
        {
            AttachmentTypeOption.SelectedItem = viewModel.OutletDecisionOptions[index];
        }

        void OnFrequechCheckChanged(System.Object sender, System.Boolean e)
        {
            viewModel.EnableFrequencyDetails();
        }
    }

    public interface ChangeFillingInterface
    {
        void SelectDefaultAttachOption(int index);
    }

}





