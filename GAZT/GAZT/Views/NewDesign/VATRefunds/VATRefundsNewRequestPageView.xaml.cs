using System;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATRefunds
{
    [Preserve(AllMembers = true)]
    public partial class VATRefundsNewRequestPageView : ContentPage
    {
        VATRefundsNewRequestViewModel viewModel;
        VatRefundsListResultModel DraftsRequestDataModel;

        public VATRefundsNewRequestPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            DraftsRequestDataModel = null;
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });
            viewModel.setMoreOptioButtons();

        }

        public VATRefundsNewRequestPageView(VatRefundsListResultModel draftsRequestData)
        {
            InitializeComponent();

            viewModel = App.Locator.VATRefundsNewRequestPageView;

            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            DraftsRequestDataModel = draftsRequestData;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
            });
            viewModel.setMoreOptioButtons();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            string message = string.Empty;
            ChangeArrowDirection();
            Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
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
                                VoidMsg();
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
                                VoidMsg();
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
                if(DraftsRequestDataModel == null)
                {
                    viewModel.ReloadData();
                    if (viewModel.VatRefundsDisplayDataModel.Fbnumx != string.Empty)
                    {
                        
                    }
                }
                else
                {
                    viewModel.LoadDraftsData(DraftsRequestDataModel);
                }
            }
            catch(GAZTErrorException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task VoidMsg()
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);
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
                        Device.BeginInvokeOnMainThread(async () =>
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

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

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
                        Device.BeginInvokeOnMainThread(async () =>
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

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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

        void BankOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void VoidButton_Tapped(System.Object sender, System.EventArgs e)
        {
            if (App.IsArabic)
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZNo, AppResources.ZYes);

                if (!result)
                {
                    viewModel.OnVoidBtnClicked();
                }
            }
            else
            {
                var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRefundCancelRefund, AppResources.ZYes, AppResources.ZNo);

                if (result)
                {
                    viewModel.OnVoidBtnClicked();
                }
            }
        }
    }
}
