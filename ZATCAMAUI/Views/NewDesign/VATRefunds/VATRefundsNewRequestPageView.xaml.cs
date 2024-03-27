using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.NewDesign.VATRefunds
{

    public partial class VATRefundsNewRequestPageView : ContentPage
    {
        VATRefundsNewRequestViewModel viewModel;
        VatRefundsListResultModel DraftsRequestDataModel;

        public VATRefundsNewRequestPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            DraftsRequestDataModel = null;
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
            });
            viewModel.setMoreOptioButtons();

        }

        public VATRefundsNewRequestPageView(VatRefundsListResultModel draftsRequestData)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            DraftsRequestDataModel = draftsRequestData;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
            });
            viewModel.setMoreOptioButtons();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            string message = string.Empty;
            ChangeArrowDirection();
            MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    message = arg;
                    viewModel.AddNewIban(message);
                }
            });
            MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
            {
                await PopupNavigation.Instance.PopAsync();
                if (arg != null)
                {
                    string savemessage = arg.ToString();

                    if (App.IsArabic)
                    {
                        ArButtons buttonId = ArButtons.None;
                        if (!string.IsNullOrEmpty(arg))
                        {
                            arg = arg.Replace(" ", "");
                        }
                        Enum.TryParse(arg, out buttonId);
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
                                await VoidMsg();
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
                        if (!string.IsNullOrEmpty(savemessage))
                        {
                            savemessage = savemessage.Replace(" ", "");
                        }
                        Enum.TryParse(savemessage, out buttonId);
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
                                await VoidMsg();
                                //viewModel.OnVoidBtnClicked();
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

            try
            {
                if (DraftsRequestDataModel == null)
                {
                    await viewModel.ReloadData();
                    if (viewModel.VatRefundsDisplayDataModel.Fbnumx != string.Empty)
                    {

                    }
                }
                else
                {
                    await viewModel.LoadDraftsData(DraftsRequestDataModel);
                }
            }
            catch (GAZTErrorException ex)
            {

            }
            catch (Exception)
            {


            }

        }

        public async Task VoidMsg()
        {
            if (App.IsArabic)
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);
                if (!result)
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            App.DisplayProgressView();
                        });

                        viewModel.OnVoidBtnClicked();



                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

                        viewModel._navigationService.GoBack();
                    }
                    catch (GAZTErrorException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            App.HideProgressView();
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                    catch (InternetException ex)
                    {
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception)
                    {


                    }
                }
            }
            else
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                if (result)
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            App.DisplayProgressView();
                        });

                        viewModel.OnVoidBtnClicked();

                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

                        var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(firstPageToRemove);

                        viewModel._navigationService.GoBack();
                    }
                    catch (GAZTErrorException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            App.HideProgressView();
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                    catch (InternetException ex)
                    {
                        await Task.Run(() =>
                        {
                            App.HideProgressView();
                        });

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception)
                    {


                    }
                }
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
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
        }

        void BankOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            //TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            //viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            // viewModel.IsNewAccountClicked = true;
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView(string.Empty));
        }

        void ContinueButton_Tapped(object sender, EventArgs e)
        {
            try
            {
                viewModel.ContinueBtnClicked();
            }
            catch (Exception)
            {


            }
        }

        public async void VoidButton_Tapped(object sender, EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);

                if (!result)
                {
                    viewModel.OnVoidBtnClicked();
                }
            }
            else
            {
                var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                if (result)
                {
                    viewModel.OnVoidBtnClicked();
                }
            }
        }
    }
}
