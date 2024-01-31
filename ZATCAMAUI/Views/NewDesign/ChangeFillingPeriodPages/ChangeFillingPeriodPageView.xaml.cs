using NavigationPage = Microsoft.Maui.Controls.NavigationPage;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using RGPopup.Maui.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ChageFillingPeriodModel;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
{

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

                NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
                SetLTR();
                On<iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                BindingContext = viewModel;
                viewModel.cFInterface = (Core.Interfaces.IChangeFillingInterface)this;
                viewModel.ResetData();
                _ = viewModel.GetVATChangeFillingData();
            }
            catch (Exception)
            {
            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
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
            Padding = safeInsets;

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

            MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
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
                            viewModel.VATSetReturnVoidAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await PopupNavigation.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {
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
                });
            }
            catch (Exception)
            {
            }
        }
        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
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
            catch (Exception)
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


        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclaration();
        }

        private void CheckBox_CheckedChanged(object sender, bool e)
        {
            viewModel.EnableDeclaration();
        }
        public void SelectDefaultAttachOption(int index)
        {
            AttachmentTypeOption.SelectedItem = viewModel.OutletDecisionOptions[index];
        }

        void OnFrequechCheckChanged(object sender, bool e)
        {
            viewModel.EnableFrequencyDetails();
        }
    }

    public interface ChangeFillingInterface
    {
        void SelectDefaultAttachOption(int index);
    }

}





