using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.GenericPickers;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using GAZT.Manager;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodPageView : ContentPage,ChangeFillingInterface
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

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                this.BindingContext = viewModel;
                viewModel.cFInterface = this;
                viewModel.ResetData();

                //viewModel.showInstructionDialog();

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
                                //viewModel.VATReturnAddNote();
                                break;
                            case ArButtons.عرضملاحظات:
                                //  viewModel.VATReturnGetNotes();
                                break;
                            case ArButtons.المرفقات:
                                // viewModel.VATViewAttachments();
                                break;
                            case ArButtons.إلغاء:
                                 viewModel.isDraftClicked = true;
                                 viewModel.VoidMsg();
                                 viewModel.isDraftClicked = false;
                                break;
                            case ArButtons.عادةتعيين:
                                //await viewModel.VATReturnResetAsync();
                                break;
                            case ArButtons.تعديل:
                                // await viewModel.VATReturnAmendAsync();
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
                                //viewModel.VATReturnAddNote();
                                break;
                            case Buttons.DisplayNotes:
                                //viewModel.VATReturnGetNotes();
                                break;
                            case Buttons.Attachments:
                                // viewModel.VATViewAttachments();
                                break;
                            case Buttons.Void:
                                viewModel.isDraftClicked = true;
                                viewModel.VoidMsg();
                                viewModel.isDraftClicked = false;
                                break;
                            case Buttons.Reset:
                                //await viewModel.VATReturnResetAsync();
                                break;
                            case Buttons.Amend:
                                // await viewModel.VATReturnAmendAsync();
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

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                //viewModel.PickerModel = arg;
                //viewModel.updatePicker();
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



                    //await PopupNavigation.Instance.PopAsync();
                    // await viewModel.VATSetReturnVoidAsync();
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


                            //if (viewModel.YearsattachmentsListViewData == null)
                            //{
                            //    viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            //}
                            //else
                            //{
                            //    viewModel.AttachmentsListViewData = viewModel.YearsattachmentsListViewData;
                            //}
                            return;
                        }
                    case 1:
                        {

                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = true;
                            viewModel.IsOthersAtachmentsVisible = false;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;

                            //if (viewModel.MonthsattachmentsListViewData == null)
                            //{
                            //    viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            //}
                            //else
                            //{
                            //    viewModel.AttachmentsListViewData = viewModel.MonthsattachmentsListViewData;
                            //}
                            return;
                        }
                    case 2:
                        {
                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = false;
                            viewModel.IsOthersAtachmentsVisible = true;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            //if (viewModel.OtherAttachmentsListViewData == null)
                            //{
                            //    viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            //}
                            //else
                            //{
                            //    viewModel.AttachmentsListViewData = viewModel.OtherAttachmentsListViewData;
                            //}
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /*private void SummarybtnContinue_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ChangeFillingPeriodSuccessPage());
        }*/

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs focusEventArgs)
        {
            viewModel.ValidateIdNumber();
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel._idNumber = e.NewTextValue;
        }

        private void OnFrequechCheckChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.EnableFrequencyDetails();
        }

        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclaration();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.EnableDeclaration();
        }
        public void SelectDefaultAttachOption(int index)
        {
            AttachmentTypeOption.SelectedItem = viewModel.OutletDecisionOptions[index];
        }
    }

    public interface ChangeFillingInterface
    {
        void SelectDefaultAttachOption(int index);
    }

}





