using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignVATReturnUpdatedUIPageView : ContentPage
    {
        #region Variable
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel viewModel;
        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageView(VATDeclaration _vATDeclarationInfo)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.GAZTNewDesignVATReturnUpdatedUIPageView;
                this.BindingContext = viewModel;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                ChangeAeroIcon();
                SetLTR();
               
               
                if (_vATDeclarationInfo.d != null)
                {
                    viewModel.VATDeclarationData = new VATDeclaration();
                    viewModel.VATDeclarationData.d = new VATDeclarationD();
                    viewModel.isBtnVisible = false;
                    viewModel.IsDeclarationCheckedForInstruction = false;
                    viewModel.IsDeclarationCheckedForSummary = false;
                    viewModel.IsCheckedTaxPayerDetailsInfo = false;
                    viewModel.IsRefundButtonEnabled = true;
                    viewModel.IsNavigatedToSubmitted = false;
                    viewModel.IsCarriedForwandReviewMessage = false;
                    viewModel.RefundButtonText = AppResources.ZZZZConfirmAndRefundRequest;
                    viewModel.VATDeclarationData = _vATDeclarationInfo;
                 
                }

                NotesPopUpPageViewModel.NoteString = string.Empty;
                NotesPopUpPageViewModel.NoteCount = 0;
                GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote = true;

                viewModel.IsAmendClicked = false;
                viewModel.IsVoidClicked = false;
                viewModel.IsResetClicked = false;
                checkNewFormorOld();
                
                IntilizeAsync();
            }
            catch(Exception ex)
            {

            }

        }

        #endregion

        public void NavigationtoStep()
        {
            if (viewModel.VATDeclarationData.d.StepNumber == "01" || viewModel.VATDeclarationData.d.StepNumber == "1" || viewModel.VATDeclarationData.d.StepNumber == "0" || viewModel.VATDeclarationData.d.StepNumber == "00")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (viewModel.IsFifteenPercentChange)
                    {
                        viewModel.currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                    }
                    else
                    {
                        viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
                    }
                    viewModel.currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                    viewModel.ManageButtonsNameOnViewModel();
                });
            }
            else if (viewModel.VATDeclarationData.d.StepNumber == "02" || viewModel.VATDeclarationData.d.StepNumber == "2")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Instrunctionsclicked();
                });
            }
            else if (viewModel.VATDeclarationData.d.StepNumber == "03" || viewModel.VATDeclarationData.d.StepNumber == "3")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Instrunctionsclicked();
                });
            }
            else if (viewModel.VATDeclarationData.d.StepNumber == "04" || viewModel.VATDeclarationData.d.StepNumber == "4")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    VatTotalAmountclicked();
                });
            }
        }

        #region Method
        public void SetEnabledProperty()
        {
            if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
            {
                viewModel.ManageEnabledProperty(false);
                viewModel.IsMainButtonEnabled = true;
            }
            else if (App.ICRStatus == "E0001" || App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057")
            {
                viewModel.ManageEnabledProperty(true);
                viewModel.IsMainButtonEnabled = true;
                // viewModel.IsMainButtonVisible = true;
                viewModel.ManageButtonsNameOnViewModel();
            }
        }
         public void Instrunctionsclicked()
        {
            try
            {
                if (viewModel.IsDeclarationCheckedForInstruction)
                {
                    //viewModel.currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
                    //ManageButtonsName();
                    viewModel.IsCheckedTaxPayerDetailsInfo = true;
                    if (viewModel.IsFifteenPercentChange)
                    {
                        viewModel.currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                        viewModel.ManageButtonsNameOnViewModel();
                    }
                    else
                    {
                        viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
                        viewModel.ManageButtonsNameOnViewModel();
                    }
                }
                else
                {
                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void ComeToInstrunctionsclicked()
        {
            try
            {
                if (viewModel.IsDeclarationCheckedForInstruction)
                {
                    viewModel.IsMainButtonEnabled = true;
                }
                else
                {
                    viewModel.IsMainButtonEnabled = false;
                }
                viewModel.currentTab = VATReturnUpdatedUITabEnum.Instrunction;
            }
            catch (Exception ex)
            {

            }
        }

        public void TaxpayerDetailsclicked()
        {
            if (viewModel.IsDeclarationCheckedForInstruction)
            {
                viewModel.IsCheckedTaxPayerDetailsInfo = true;
                if (viewModel.IsFifteenPercentChange)
                {
                    viewModel.currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                    viewModel.ManageButtonsNameOnViewModel();
                }
                else
                {
                    viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
                    viewModel.ManageButtonsNameOnViewModel();
                }
            }
            else
            {

            }
        }

        public void ComeToVatReturnclicked()
        {
            viewModel.currentTab = VATReturnUpdatedUITabEnum.VATReturns;
            viewModel.IsMainButtonEnabled = true;
        }
        public void VatReturnclicked()
        {
            if (viewModel.IsFifteenPersenctVisible || viewModel.IsFivePersenctVisible)
            {
                viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
                viewModel.ManageButtonsNameOnViewModel();
            }
            else
            {

            }
        }
        public void VatSalesclicked()
        {
            bool IsFieldsCheck = CheckSalesMandetoryFields();
           
            if (IsFieldsCheck)
            {
                viewModel.currentTab = VATReturnUpdatedUITabEnum.Purchase;
                viewModel.ManageButtonsNameOnViewModel();
            }
            else
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields;

                headerWithInfos.Add(headerAmountInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
        }

        public void ComeToVatSalesclicked()
        {
            bool IsFieldsCheck = CheckSalesMandetoryFields();

            if (IsFieldsCheck)
            {
                viewModel.IsMainButtonEnabled = true;
                
            }
            else
            {
                viewModel.IsMainButtonEnabled = false;
            }
            viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
        }
        public void VatPurchaseclicked()
        {
            bool IsFieldsCheck = CheckPurchaseMandetoryFields();
           
            if (IsFieldsCheck)
            {
                viewModel.currentTab = VATReturnUpdatedUITabEnum.TotalVat;
                viewModel.ManageButtonsNameOnViewModel();
            }
            else
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields;

                headerWithInfos.Add(headerAmountInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
        }

        public void ComeToVatPurchaseclicked()
        {
            bool IsFieldsCheck = CheckPurchaseMandetoryFields();

            if (IsFieldsCheck)
            {
                viewModel.IsMainButtonEnabled = true;
            }
            else
            {
                viewModel.IsMainButtonEnabled = false;
            }
            viewModel.currentTab = VATReturnUpdatedUITabEnum.Purchase;
        }
        public void VatTotalAmountclicked()
        {
            bool IsFieldsCheck = CheckTotalVATMandetoryFields();

            if (IsFieldsCheck)
            {
                viewModel.currentTab = VATReturnUpdatedUITabEnum.Summery;
                viewModel.ManageButtonsNameOnViewModel();
            }
            else
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields;

                headerWithInfos.Add(headerAmountInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            
        }

        public void ComeToVatTotalAmountclicked()
        {
            bool IsFieldsCheck = CheckTotalVATMandetoryFields();

            if (IsFieldsCheck)
            {
                viewModel.IsMainButtonEnabled = true;
            }
            else
            {
                viewModel.IsMainButtonEnabled = false;
            }
            viewModel.currentTab = VATReturnUpdatedUITabEnum.TotalVat;
        }

        public void Summaryclicked()
        {
            if (viewModel.IsDeclarationCheckedForInstruction)
            {
                //viewModel.currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
            
            }
            else
            {

            }
        }

        public void ManageButtonsName()
        {
            if(viewModel.currentTab == VATReturnUpdatedUITabEnum.Instrunction)
            {
                viewModel.isBtnVisible = false;
                viewModel.IsMainButtonVisible = true;
                viewModel.ContinueText = AppResources.ZZZZContinue;
            }
            //else if(viewModel.currentTab == VATReturnUpdatedUITabEnum.TaxpayerDetails)
            //{
            //    viewModel.isBtnVisible = false;
            //    viewModel.IsMainButtonVisible = true;
            //    viewModel.ContinueText = AppResources.ZZZZContinue; 
            //}
            else if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns)
            {
                viewModel.isBtnVisible = false;
                viewModel.IsMainButtonVisible = true;
                viewModel.ContinueText = AppResources.ZZZZContinue;
            }
            else if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales)
            {
                viewModel.isBtnVisible = false;
                viewModel.IsMainButtonVisible = true;
                viewModel.ContinueText = AppResources.ZZZZContinue;
            }
            else if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                viewModel.isBtnVisible = false;
                viewModel.IsMainButtonVisible = true;
                viewModel.ContinueText = AppResources.ZZZZContinue;
            }
            else if (viewModel.currentTab == VATReturnUpdatedUITabEnum.TotalVat)
            {
                viewModel.isBtnVisible = false;
                viewModel.IsMainButtonVisible = true;
                viewModel.ContinueText = AppResources.ZZZZContinue;
            }
            else if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Summery)
            {
                if (Convert.ToDouble(viewModel.NetdueVat) < 0)
                {
                    viewModel.isBtnVisible = false;
                    viewModel.IsMainButtonVisible = true;
                    viewModel.ContinueText = AppResources.ZZZZConfirmAndCarryForward;
                }
                else
                {
                    viewModel.isBtnVisible = true;
                    viewModel.IsMainButtonVisible = false;
                    viewModel.CreditDetailsText = AppResources.ZZZZConfirmandGenerateSADADBill;
                }
            }
        }


        public void checkNewFormorOld()
        {

            if (App.ICRStatus == "E0001" || App.ICRStatus == "E0013")
            {
                viewModel.IsEnableSwitchToggledFor15PercentChange = true;
            }
            else
            {
                if ((App.ICRStatus == "E0045" || App.ICRStatus == "E0056" || App.ICRStatus == "E0006") && viewModel.VATDeclarationData.d.Yesno == "X")
                {
                    viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                }
                else
                {
                    if (App.ICRStatus == "E0056")
                    {
                        viewModel.IsEnableSwitchToggledFor15PercentChange = true;
                    }
                    else
                    {
                        viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                    }
                }
                if (App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                {
                    viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                }
            }


            if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.GoliveFg == "X")
            {
                viewModel.IsFifteenPercentChange = true;
                viewModel.IsNewReturn = true;
                ShowHideContent(viewModel.IsNewReturn);
                SetNewVATRate();
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.MaxIndex = 6;
                });
            }
            else
            {
                viewModel.IsFifteenPercentChange = false;
                viewModel.IsNewReturn = false;
                ShowHideContent(viewModel.IsNewReturn);
                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.MaxIndex = 5;
                });
            }

        }
        public void ShowHideContent(bool IsNewReturn)
        {
            //Test Checked In VAT15Change
            if (IsNewReturn == true)
            {
                viewModel.IsPrevReturn = false;
                viewModel.IsNewReturn = true;
                if (viewModel.VATDeclarationData.d.Yesno == "X")
                {
                    viewModel.IsFifteenPersenctVisible = true;
                    viewModel.IsFivePersenctVisible = true;
                    viewModel.IsSwitchToggledFor15PercentChange = true;
                    viewModel.IsYesChecked = true;

                    viewModel.YesBackgroundImage = "re_Tile_Background";
                    viewModel.NoBackgroundImage = "re_Property_Tile_Background_White";

                    viewModel.YesLabelColor = Color.White;
                    viewModel.NoLabelColor = Color.FromHex("#232323");
                }
                else
                {
                    viewModel.IsFifteenPersenctVisible = true;
                    viewModel.IsFivePersenctVisible = false;
                    viewModel.IsSwitchToggledFor15PercentChange = false;
                    viewModel.IsNoChecked = true;

                    viewModel.YesBackgroundImage = "re_Property_Tile_Background_White";
                    viewModel.NoBackgroundImage = "re_Tile_Background";

                    viewModel.YesLabelColor = Color.FromHex("#232323");
                    viewModel.NoLabelColor = Color.White;
                }
            }
            else
            {
                viewModel.IsPrevReturn = true;
                viewModel.IsNewReturn = false;
                viewModel.IsFifteenPersenctVisible = false;
                viewModel.IsFivePersenctVisible = false;
            }
        }
        public void SetNewVATRate()
        {

            if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null)
            {
                if (viewModel.VATDeclarationData.d.VATPERITEMSet != null && viewModel.VATDeclarationData.d.VATPERITEMSet.results != null)
                {
                    Result6 Rate002For15Percent = viewModel.VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "002").FirstOrDefault();
                    Result6 Rate003For5Percent = viewModel.VATDeclarationData.d.VATPERITEMSet.results.Where(x => x.Type == "003").FirstOrDefault();

                    if (Rate002For15Percent != null)
                    {
                        viewModel.VATRate002For15Percent = Rate002For15Percent.Rate;
                    }
                    if (Rate003For5Percent != null)
                    {
                        viewModel.VATRate003For5Percent = Rate003For5Percent.Rate;
                    }
                }
            }




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
                            await viewModel.VATSetReturnVoidAsync();
                        }
                        else if(arg == AppResources.ZZZRefundEnableMessage)
                        {
                            viewModel.IsCarriedForwandReviewMessage = false;
                            await PopupNavigation.Instance.PopAsync();
                            viewModel.SetDataForRefundPopup();
                            PopupNavigation.Instance.PushAsync(new RefundAccountPopupPageView(viewModel.VATDeclarationData));
                        }
                    }
               
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getSubmittedFromRefundCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Refundsubmitted", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                      //  viewModel.IsNavigatedToBilled = true;

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel.ManageEnabledProperty(false);
                            viewModel.IsGetSadadNumberEnabled = false;
                            viewModel.IsMainButtonEnabled = false;
                            viewModel.IsMainButtonVisible = false;
                            viewModel.IsRefundButtonEnabled = false;
                            viewModel.IsRefundButtonVisible = false;
                            viewModel.isBtnVisible = false;
                            viewModel.IsEnableSwitchToggledFor15PercentChange = false;
                            viewModel.IsNavigatedToSubmitted = true;
                            //IsMainButtonVisible = false;
                            //IsSwichButtonEnableToTap = false;
                            //IsEnableIBAN = false;
                            //IsEnableCheckedRefund = false;
                            //IsEnableIBANType = false;
                            //IsEnableIBANIdNumber = false;
                            //IsGetAcknowledgementClicked = true;
                            //IsMoreButtonEnabled = false;
                        });
                    }

                });
            }
            catch (Exception ex)
            {

            }
        }
        public void VATViewAttachments()
        {
            try
            {
                PopupNavigation.Instance.PushAsync(new VATDeclarationAttachmentPageView(viewModel.VATDeclarationData));
            }
            catch(Exception ex)
            {

            }
        }
        public async void getRefundClickedCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "RefundClicked", async (sender, arg) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsNewLoading = true;
                    });
                });
            }
            catch (Exception ex)
            {

            }
        }
        public async void getRefundClickedForStopLoaderCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "RefundClickedForStop", async (sender, arg) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        viewModel.IsNewLoading = false;
                    });
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getAddNoteCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "AddNoteForVATDeclaration", async (sender, arg) =>
                {
                    AddNote();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public async void getClearNoteCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "ClearNoteForVATDeclaration", async (sender, arg) =>
                {
                    AddNote();
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

        public async void getActionCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "CommandReceived", async (sender, arg) =>
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
                                    AddNotePopUp();
                                    break;
                                case ArButtons.عرضملاحظات:
                                    DisplayNotePopUp();
                                    break;
                                case ArButtons.المرفقات:
                                    VATViewAttachments();
                                break;
                                case ArButtons.إلغاء:
                                    viewModel.VoidMsg();
                                break;
                                case ArButtons.عادةتعيين:
                                    await viewModel.VATReturnResetAsync();
                                    break;
                                case ArButtons.تعديل:
                                    await viewModel.VATReturnAmendAsync();
                                    break;
                                case ArButtons.حفظكمسودة:
                                    viewModel.IsVATReturnFieldCheckForSaveAsDraft = true;
                                    if (CheckSalesMandetoryFields() && CheckPurchaseMandetoryFields() && CheckTotalVATMandetoryFields())
                                    {
                                        await viewModel.OnSaveDraftClicked();
                                        if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Summery)
                                        {
                                            if (viewModel.IsDeclarationCheckedForSummary == false)
                                            {
                                                viewModel.IsMainButtonEnabled = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                            headerAmountInfo.IsLinkAvailable = false;
                                            headerAmountInfo.Message = AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields;

                                            headerWithInfos.Add(headerAmountInfo);


                                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                                           // await viewModel._dialogService.ShowMessage(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields, AppResources.Information);
                                        });
                                    }
                                    viewModel.IsVATReturnFieldCheckForSaveAsDraft = false;
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
                                    AddNotePopUp();
                                break;
                                case Buttons.DisplayNotes:
                                    DisplayNotePopUp();
                                break;
                                case Buttons.Attachments:
                                    VATViewAttachments();
                                    break;
                                case Buttons.Void:
                                    viewModel.VoidMsg();
                                break;
                                case Buttons.Reset:
                                    await viewModel.VATReturnResetAsync();
                                    break;
                                case Buttons.Amend:
                                    await viewModel.VATReturnAmendAsync();
                                    break;
                                case Buttons.SaveasDraft:
                                    viewModel.IsVATReturnFieldCheckForSaveAsDraft = true;
                                    if (CheckSalesMandetoryFields() && CheckPurchaseMandetoryFields() && CheckTotalVATMandetoryFields())
                                    {
                                        await viewModel.OnSaveDraftClicked();
                                        if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Summery)
                                        {
                                            if (viewModel.IsDeclarationCheckedForSummary == false)
                                            {
                                                viewModel.IsMainButtonEnabled = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {

                                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                            headerAmountInfo.IsLinkAvailable = false;
                                            headerAmountInfo.Message = AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields;

                                            headerWithInfos.Add(headerAmountInfo);


                                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                            //await viewModel._dialogService.ShowMessage(AppResources.ZZGeneralMessage_PleaseCorrectHighlightedFields, AppResources.Information);
                                        });
                                    }
                                    viewModel.IsVATReturnFieldCheckForSaveAsDraft = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                });
            }
            catch(Exception ex)
            {

            }
        }

        public void AddNotePopUp()
        {
            try
            {
                PopupNavigation.Instance.PushAsync(new NotesPopUpPageView(viewModel.VATDeclarationData));
            }
            catch (Exception ex)
            {

            }
        }

        public void DisplayNotePopUp()
        {
            try
            {
                PopupNavigation.Instance.PushAsync(new NotesDescriptionPopUpPageView(viewModel.VATDeclarationData));
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                MessagingCenter.Unsubscribe<object, string>(this, "CommandReceived");
                MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
                MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
                MessagingCenter.Unsubscribe<object, string>(this, "RefundClicked");
                MessagingCenter.Unsubscribe<object, string>(this, "RefundClickedForStop");
                MessagingCenter.Unsubscribe<object, string>(this, "Refundsubmitted");
                MessagingCenter.Unsubscribe<object, string>(this, "AddNoteForVATDeclaration");
                MessagingCenter.Unsubscribe<object, string>(this, "ClearNoteForVATDeclaration");



                Device.BeginInvokeOnMainThread(() =>
                {
                    viewModel.IsNewLoading = false;
                });
            }
            catch(Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                viewModel.IsSwitchVisible = true;
            }
            else
            {
                viewModel.IsSwitchVisible = false;
            }
            getActionCommand();
            getYesCommand();
            getNoCommand();
            getRefundClickedCommand();
            getSubmittedFromRefundCommand();
            getRefundClickedForStopLoaderCommand();
            getAddNoteCommand();
            getClearNoteCommand();


            AddNote();
        }

        public void AddNote()
        {
            try
            {
                if (NotesPopUpPageViewModel.IsComingFromNotePage == true && !string.IsNullOrEmpty(NotesPopUpPageViewModel.NoteString))
                {
                    if (App.ICRStatus == "E0001")
                    {
                        SetNote();
                    }
                    if (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {
                        Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                        if (note != null)
                        {
                            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                            {
                                item.Strline = NotesPopUpPageViewModel.NoteString;
                                item.Tdline = NotesPopUpPageViewModel.NoteString;
                            }
                            NotesPopUpPageViewModel.IsComingFromNotePage = false;
                        }
                        else
                        {
                            SetNoteForDraftModes();
                        }
                    }
                    //if(App.ICRStatus == "E0045" && viewModel.IsAmendClicked==true && AddNotePageViewModel.ClearNoteClicked == false)
                    //{
                    //    int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                    //    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00001").FirstOrDefault();
                    //    if (note != null)
                    //    {
                    //        //foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //        //{
                    //        //    item.DataVersionz = "00001";
                    //        //}
                    //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    //        if (noteForEdited != null)
                    //        {
                    //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //            {
                    //                item.Strline = AddNotePageViewModel.NoteString;
                    //                item.Tdline = AddNotePageViewModel.NoteString;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            SetNoteForBilledAndAmend();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    //        if (noteForEdited != null)
                    //        {
                    //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //            {
                    //                item.Strline = AddNotePageViewModel.NoteString;
                    //                item.Tdline = AddNotePageViewModel.NoteString;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            SetNoteForBilledAndAmend();
                    //        }
                    //    }
                    //}
                    //if (App.ICRStatus == "E0006" && viewModel.IsAmendClicked == true && AddNotePageViewModel.ClearNoteClicked == false)
                    //{
                    //    int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                    //    Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00001").FirstOrDefault();
                    //    if (note != null)
                    //    {
                    //        //foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //        //{
                    //        //    item.DataVersionz = "00001";
                    //        //}
                    //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    //        if (noteForEdited != null)
                    //        {
                    //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //            {
                    //                item.Strline = AddNotePageViewModel.NoteString;
                    //                item.Tdline = AddNotePageViewModel.NoteString;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            SetNoteForBilledAndAmend();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Note noteForEdited = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                    //        if (noteForEdited != null)
                    //        {
                    //            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                    //            {
                    //                item.Strline = AddNotePageViewModel.NoteString;
                    //                item.Tdline = AddNotePageViewModel.NoteString;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            SetNoteForBilledAndAmend();
                    //        }
                    //    }
                    //}
                    if (NotesPopUpPageViewModel.ClearNoteClicked == true)
                    {
                        Note note = viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000").FirstOrDefault();
                        if (note != null)
                        {
                            foreach (var item in viewModel.VATDeclarationData.d.NOTESSet.results.Where(w => w.DataVersionz == "00000"))
                            {
                                item.Strline = NotesPopUpPageViewModel.NoteString;
                                item.Tdline = NotesPopUpPageViewModel.NoteString;
                            }
                            NotesPopUpPageViewModel.IsComingFromNotePage = false;
                            NotesPopUpPageViewModel.NoteString = string.Empty;
                        }
                        NotesPopUpPageViewModel.ClearNoteClicked = false;
                    }
                    NotesPopUpPageViewModel.NoteString = string.Empty;
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void SetNote()
        {
            try
            {
                //if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.NOTESSet != null && viewModel.VATDeclarationData.d.NOTESSet.results != null && viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                //{

                //}
                //else
                //{
                //    viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                //}
                viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                Note objNote = new Note();
                int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                string Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
                objNote.__metadata = new Metadata2();
                objNote.__metadata.id = Url;
                objNote.__metadata.uri = Url;
                objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
                objNote.Notenoz = (count + 1).ToString();
                objNote.DataVersionz = "00000";
                objNote.Refnamez = String.Empty;
                objNote.XInvoicez = String.Empty;
                objNote.XObsoletez = string.Empty;
                objNote.Rcodez = "VATR";
                objNote.ByPusrz = string.Empty;
                objNote.Tdformat = string.Empty;
                objNote.Tdline = string.Empty;
                objNote.Erfusrz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.ByGpartz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.Namez = viewModel.VATDeclarationData.d.Tpnm;
                objNote.AttByz = "TP";
                objNote.Noteno = (count + 1).ToString();
                objNote.Lineno = 1;
                objNote.ElemNo = 0;
                objNote.Strdt = string.Empty;
                objNote.Strtime = string.Empty;
                objNote.Sect = "VAT Return General Note";
                objNote.Strline = NotesPopUpPageViewModel.NoteString;
                objNote.Tdline = NotesPopUpPageViewModel.NoteString;
                viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
                NotesPopUpPageViewModel.IsComingFromNotePage = false;
                //AddNotePageViewModel.NoteString = string.Empty;
            }
            catch(Exception ex)
            {

            }
        }

        public void SetNoteForDraftModes()
        {
            try
            {
                if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.NOTESSet != null && viewModel.VATDeclarationData.d.NOTESSet.results != null && viewModel.VATDeclarationData.d.NOTESSet.results.Count != 0)
                {
                    if (App.ICRStatus == "E0056" || App.ICRStatus == "E0057" || App.ICRStatus == "E0045" || App.ICRStatus == "E0006")
                    {

                    }
                    else
                    {
                        viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                    }
                }
                else
                {
                    viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                }


              //  viewModel.VATDeclarationData.d.NOTESSet.results = new List<Note>();
                Note objNote = new Note();
                int count = viewModel.VATDeclarationData.d.NOTESSet.results.Count;
                string Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_VATR_M_SRV/NOTESSet('00" + (count + 1).ToString() + "')";
                objNote.__metadata = new Metadata2();
                objNote.__metadata.id = Url;
                objNote.__metadata.uri = Url;
                objNote.__metadata.type = "ZDP_VATR_M_SRV.NOTES";
                objNote.Notenoz = (count + 1).ToString();
                objNote.DataVersionz = "00000";
                objNote.Refnamez = String.Empty;
                objNote.XInvoicez = String.Empty;
                objNote.XObsoletez = string.Empty;
                objNote.Rcodez = "VATR";
                objNote.ByPusrz = string.Empty;
                objNote.Tdformat = string.Empty;
                objNote.Tdline = string.Empty;
                objNote.Erfusrz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.ByGpartz = viewModel.VATDeclarationData.d.Gpartz;
                objNote.Namez = viewModel.VATDeclarationData.d.Tpnm;
                objNote.AttByz = "TP";
                objNote.Noteno = (count + 1).ToString();
                objNote.Lineno = 1;
                objNote.ElemNo = 0;
                objNote.Strdt = string.Empty;
                objNote.Strtime = string.Empty;
                objNote.Sect = "VAT Return General Note";
                objNote.Strline = NotesPopUpPageViewModel.NoteString;
                objNote.Tdline = NotesPopUpPageViewModel.NoteString;
                viewModel.VATDeclarationData.d.NOTESSet.results.Add(objNote);
                NotesPopUpPageViewModel.IsComingFromNotePage = false;
                //AddNotePageViewModel.NoteString = string.Empty;
            }
            catch(Exception ex)
            {

            }
        }

        public async Task IntilizeAsync()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsNewLoading = true;
            });

            await Task.Run(async () =>
            {
                try
                {
                    await viewModel.pageLoad();

                    await Task.Run(() =>
                    {
                        NavigationtoStep();
                    });
                    viewModel.ListOfActionButtonsApplicable = new List<string>();
                    await viewModel.SetButtons(viewModel.VATDeclarationData);
                    if (App.ICRStatus == "E0045" || App.ICRStatus == "E0006" || App.ICRStatus == "E0055" || App.ICRStatus == "E0058")
                    {
                        viewModel.ManageEnabledProperty(true);

                        viewModel.IsCheckedTaxPayerDetailsInfo = true;
                        viewModel.IsDeclarationCheckedForSummary = true;
                        viewModel.IsDeclarationCheckedForInstruction = true;
                        //viewModel.ButtonName = AppResources.ZVatDownloadForm;
                        viewModel.IsMainButtonEnabled = false;
                        viewModel.IsMainButtonVisible = false;
                        viewModel.ManageButtonsNameOnViewModel();
                    }
                    if (App.CheckTINStatusPageView != "0045")
                    {
                        await onPageLoadCalculation();
                    }
                    SetEnabledProperty();
                    //viewModel.IsMainButtonVisible = true;
                }
                catch(Exception ex)
                {

                }
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsNewLoading = false;
            });
        }
        private void OnClickedFAQ(object sender, EventArgs e)
        {
            string FaqUrl = string.Empty;
            if (App.IsArabic)
            {
                FaqUrl = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
            }
            else
            {
                FaqUrl = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
            }
            Device.OpenUri(new Uri(FaqUrl));
        }
        public async Task onPageLoadCalculation()
        {
            viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
            viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
            viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
            viewModel.TotalsalesVat = viewModel.ResponseVATDeclarationD.StdsalesVat;
            viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
            if (viewModel.ResponseVATDeclarationD.TpregFg == "X")
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            else
            {
                viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
            }
            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);
            viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);
        }

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
               // MainGrid.Margin = new Thickness(0);
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                //MainGrid.Margin = new Thickness(-36,0,36,0);
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
        #endregion

        private void btnprimary_Clicked(object sender, EventArgs e)
        {

        }

        private void OnStandardRatedTapped(object sender, EventArgs e)
        {
           // PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView());
        }

        private void OnNewStandardRatedSalesAmountClicked(object sender, EventArgs e)
        {
            try
            {

                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipStandardRatedSalesAmount;
                string rate = viewModel.VATRate002.Replace(".00", string.Empty);
                String MessageWithPercent = headerAmountInfo.Message.Replace("5%", rate + "%");
                headerAmountInfo.Message = MessageWithPercent;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }

                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.IsLinkAvailable = false;
                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardRatedSalesAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }
                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

               
                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatStandardRatedSales;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                

                //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
            }
            catch (Exception ex)
            {

            }
        }

        private void OnDomesticRatedTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AdjustmentPopupPageView());
        }

        private void OnInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new InstructionPopUp());
        }

        private void OnSummaryInCTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new SummaryInstruction());
        }

        private void OnYesTapped(object sender, EventArgs e)
        {
            viewModel.IsSwitchToggledFor15PercentChange = true;
           
            viewModel.IsFifteenPersenctVisible = true;
            viewModel.IsFivePersenctVisible = true;
            Clear15And5PercentObject();

            viewModel.YesBackgroundImage = "re_Tile_Background";
            viewModel.NoBackgroundImage = "re_Property_Tile_Background_White";

            viewModel.YesLabelColor = Color.White;
            viewModel.NoLabelColor = Color.FromHex("#232323");
            viewModel.IsMainButtonEnabled = true;
        }

        public bool CheckMandetoryFields()
        {
            bool IsAllEntered = true;
            try
            {
                if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.GoliveFg != "X")
                {
                    if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                    {
                        if (string.IsNullOrEmpty(EntryVatAmount.Text) || EntryVatAmount.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryVatAmountFrame.HasError = true;
                        }
                        else
                        {
                            EntryVatAmountFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) || EntryVatAdjustmentWithSAR.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryVatAdjustmentWithSARFrame.HasError = true;
                        }
                        else
                        {
                            EntryVatAdjustmentWithSARFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryStdsalesVat.Text) || EntryStdsalesVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntrySalesGccAmt.Text) || EntrySalesGccAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntrySalesGccAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntrySalesGccAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntrySalesGccAdj.Text) || EntrySalesGccAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntrySalesGccAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntrySalesGccAdjFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryZerosalesAmt.Text) || EntryZerosalesAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZerosalesAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryZerosalesAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryZerosalesAdj.Text) || EntryZerosalesAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZerosalesAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryZerosalesAdjFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryExportsAmt.Text) || EntryExportsAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExportsAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryExportsAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryExportsAdj.Text) || EntryExportsAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExportsAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryExportsAdjFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text) || EntryExemptsalesAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptsalesAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptsalesAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text) || EntryExemptsalesAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptsalesAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptsalesAdjFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) || EntryStdpurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryStdpurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryStdpurchaseAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) || EntryStdpurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryStdpurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryStdpurchaseAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text) || EntryStdpurchasesVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) || EntryZVatAmountWithSAR.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZVatAmountWithSARFrame.HasError = true;
                        }
                        else
                        {
                            EntryZVatAmountWithSARFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportspaidAdj.Text) || EntryImportspaidAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportspaidAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportspaidAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportspaidVat.Text) || EntryImportspaidVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccAmt.Text) || EntryImportsaccAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportsaccAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportsaccAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccAdj.Text) || EntryImportsaccAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportsaccAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportsaccAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccVat.Text) || EntryImportsaccVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) || EntryZeropurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZeropurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryZeropurchaseAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) || EntryZeropurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZeropurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryZeropurchaseAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) || EntryExemptpurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptpurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptpurchaseAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) || EntryExemptpurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptpurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptpurchaseAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryPreperiodcorr.Text) || EntryPreperiodcorr.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryPreperiodcorrFrame.HasError = true;
                        }
                        else
                        {
                            if (viewModel.IsGreaterThanFiveT == false)
                            {
                                EntryPreperiodcorrFrame.HasError = false;
                            }
                            else
                            {
                                EntryPreperiodcorrFrame.HasError = true;
                            }
                        }
                        if (string.IsNullOrEmpty(EntryCreditVat.Text) || EntryCreditVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (viewModel.IsGreaterThanFiveT == true)
                        {
                            IsAllEntered = false;
                        }
                        //if (string.IsNullOrEmpty(EntryNetdueVat.Text))
                        //{
                        //    IsAllEntered = false;
                        //}
                        if (IsAllEntered == false)
                        {
                            viewModel.IsMainButtonEnabled = false;
                            // BtnNextStep.IsEnabled = false;
                        }
                        else
                        {
                            // viewModel.IsDeclarationCheckedForInstruction = false;
                            viewModel.IsMainButtonEnabled = true;
                            //  BtnNextStep.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    if (viewModel.IsFifteenPercentChange)
                    {
                        bool check = CheckMandetoryFieldsFor15PercentView();
                        if (!check)
                        {
                            IsAllEntered = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return IsAllEntered;
        }

        public bool CheckTotalVATMandetoryFields()
        {
            bool IsAllEntered = true;
            try
            {
                if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null)
                {
                    if (viewModel.currentTab == VATReturnUpdatedUITabEnum.TotalVat)
                    {
                        
                        if (string.IsNullOrEmpty(EntryPreperiodcorr.Text) || EntryPreperiodcorr.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryPreperiodcorrFrame.HasError = true;
                        }
                        else
                        {
                            if (viewModel.IsGreaterThanFiveT == false)
                            {
                                EntryPreperiodcorrFrame.HasError = false;
                            }
                            else
                            {
                                EntryPreperiodcorrFrame.HasError = true;
                            }
                        }
                        if (string.IsNullOrEmpty(EntryCreditVat.Text) || EntryCreditVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (viewModel.IsGreaterThanFiveT == true)
                        {
                            IsAllEntered = false;
                        }
                        //if (string.IsNullOrEmpty(EntryNetdueVat.Text))
                        //{
                        //    IsAllEntered = false;
                        //}
                        if (IsAllEntered == false)
                        {
                            viewModel.IsMainButtonEnabled = false;
                            // BtnNextStep.IsEnabled = false;
                        }
                        else
                        {
                            // viewModel.IsDeclarationCheckedForInstruction = false;
                            viewModel.IsMainButtonEnabled = true;
                            //  BtnNextStep.IsEnabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return IsAllEntered;
        }


        public bool CheckSalesMandetoryFields()
        {
            bool IsAllEntered = true;
            try
            { 
            if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.GoliveFg != "X")
            {
                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales)
                {
                    if (string.IsNullOrEmpty(EntryVatAmount.Text) || EntryVatAmount.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryVatAmountFrame.HasError = true;
                    }
                    else
                    {
                        EntryVatAmountFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) || EntryVatAdjustmentWithSAR.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryVatAdjustmentWithSARFrame.HasError = true;
                    }
                    else
                    {
                        EntryVatAdjustmentWithSARFrame.HasError = false;
                    }
                    if (string.IsNullOrEmpty(EntryStdsalesVat.Text) || EntryStdsalesVat.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                    }
                    if (string.IsNullOrEmpty(EntrySalesGccAmt.Text) || EntrySalesGccAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntrySalesGccAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntrySalesGccAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntrySalesGccAdj.Text) || EntrySalesGccAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntrySalesGccAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntrySalesGccAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryZerosalesAmt.Text) || EntryZerosalesAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZerosalesAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryZerosalesAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryZerosalesAdj.Text) || EntryZerosalesAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryZerosalesAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryZerosalesAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExportsAmt.Text) || EntryExportsAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExportsAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryExportsAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExportsAdj.Text) || EntryExportsAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExportsAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryExportsAdjFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text) || EntryExemptsalesAmt.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptsalesAmtFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptsalesAmtFrame.HasError = false; ;
                    }
                    if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text) || EntryExemptsalesAdj.TextColor == Color.Red)
                    {
                        IsAllEntered = false;
                        EntryExemptsalesAdjFrame.HasError = true;
                    }
                    else
                    {
                        EntryExemptsalesAdjFrame.HasError = false; ;
                    }
                    if (IsAllEntered == false)
                    {
                        viewModel.IsMainButtonEnabled = false;
                        // BtnNextStep.IsEnabled = false;
                    }
                    else
                    {
                        // viewModel.IsDeclarationCheckedForInstruction = false;
                        viewModel.IsMainButtonEnabled = true;
                        //  BtnNextStep.IsEnabled = true;
                    }
                }
            }
            else
            {
                if (viewModel.IsFifteenPercentChange)
                {
                   if(viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales)
                        {
                            bool check = CheckSalesMandetoryFieldsFor15Percent();
                            if (!check)
                            {
                                IsAllEntered = false;
                            }
                        }
                   
                }
            }
        }
            catch (Exception ex)
            {
            }
            return IsAllEntered;
        }

        public bool CheckSalesMandetoryFieldsFor15Percent()
        {
            bool IsAllEntered = true;
            try
            {
                if (string.IsNullOrEmpty(EntryVatAmount.Text) || EntryVatAmount.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAmount15.Text) || EntryVatAmount15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame15.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame15.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryVatAmount5.Text) || EntryVatAmount5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame5.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame5.HasError = false;
                }





                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) || EntryVatAdjustmentWithSAR.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR15.Text) || EntryVatAdjustmentWithSAR15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame15.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR5.Text) || EntryVatAdjustmentWithSAR5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame5.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame5.HasError = false;
                }



                if (string.IsNullOrEmpty(EntryStdsalesVat.Text) || EntryStdsalesVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdsalesVat15.Text) || EntryStdsalesVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntrySalesGccAmt.Text) || EntrySalesGccAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntrySalesGccAmtFrame.HasError = true;
                }
                else
                {
                    EntrySalesGccAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntrySalesGccAdj.Text) || EntrySalesGccAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntrySalesGccAdjFrame.HasError = true;
                }
                else
                {
                    EntrySalesGccAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAmt.Text) || EntryZerosalesAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZerosalesAmtFrame.HasError = true;
                }
                else
                {
                    EntryZerosalesAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAdj.Text) || EntryZerosalesAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZerosalesAdjFrame.HasError = true;
                }
                else
                {
                    EntryZerosalesAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExportsAmt.Text) || EntryExportsAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExportsAmtFrame.HasError = true;
                }
                else
                {
                    EntryExportsAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExportsAdj.Text) || EntryExportsAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExportsAdjFrame.HasError = true;
                }
                else
                {
                    EntryExportsAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text) || EntryExemptsalesAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptsalesAmtFrame.HasError = true;
                }
                else
                {
                    EntryExemptsalesAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text) || EntryExemptsalesAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptsalesAdjFrame.HasError = true;
                }
                else
                {
                    EntryExemptsalesAdjFrame.HasError = false; ;
                }
               
                if (IsAllEntered == false)
                {
                    viewModel.IsMainButtonEnabled = false;
                    // BtnNextStep.IsEnabled = false;
                }
                else
                {
                    // viewModel.IsDeclarationCheckedForInstruction = false;
                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
            }
            catch (Exception e)
            {

            }
            return IsAllEntered;
        }

       

        public bool CheckMandetoryFieldsFor15PercentView()
        {
            bool IsAllEntered = true;
            try
            {
                if (string.IsNullOrEmpty(EntryVatAmount.Text) || EntryVatAmount.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAmount15.Text) || EntryVatAmount15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame15.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame15.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryVatAmount5.Text) || EntryVatAmount5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAmountFrame5.HasError = true;
                }
                else
                {
                    EntryVatAmountFrame5.HasError = false;
                }





                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) || EntryVatAdjustmentWithSAR.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR15.Text) || EntryVatAdjustmentWithSAR15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame15.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryVatAdjustmentWithSAR5.Text) || EntryVatAdjustmentWithSAR5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryVatAdjustmentWithSARFrame5.HasError = true;
                }
                else
                {
                    EntryVatAdjustmentWithSARFrame5.HasError = false;
                }



                if (string.IsNullOrEmpty(EntryStdsalesVat.Text) || EntryStdsalesVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdsalesVat15.Text) || EntryStdsalesVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntrySalesGccAmt.Text) || EntrySalesGccAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntrySalesGccAmtFrame.HasError = true;
                }
                else
                {
                    EntrySalesGccAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntrySalesGccAdj.Text) || EntrySalesGccAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntrySalesGccAdjFrame.HasError = true;
                }
                else
                {
                    EntrySalesGccAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAmt.Text) || EntryZerosalesAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZerosalesAmtFrame.HasError = true;
                }
                else
                {
                    EntryZerosalesAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryZerosalesAdj.Text) || EntryZerosalesAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZerosalesAdjFrame.HasError = true;
                }
                else
                {
                    EntryZerosalesAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExportsAmt.Text) || EntryExportsAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExportsAmtFrame.HasError = true;
                }
                else
                {
                    EntryExportsAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExportsAdj.Text) || EntryExportsAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExportsAdjFrame.HasError = true;
                }
                else
                {
                    EntryExportsAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAmt.Text) || EntryExemptsalesAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptsalesAmtFrame.HasError = true;
                }
                else
                {
                    EntryExemptsalesAmtFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryExemptsalesAdj.Text) || EntryExemptsalesAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptsalesAdjFrame.HasError = true;
                }
                else
                {
                    EntryExemptsalesAdjFrame.HasError = false; ;
                }
                if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) || EntryStdpurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame.HasError = false; ;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) || EntryStdpurchaseAmt15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame15.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame15.HasError = false; ;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) || EntryStdpurchaseAmt5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame5.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame5.HasError = false; ;
                }





                if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) || EntryStdpurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAdj15.Text) || EntryStdpurchaseAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame15.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAdj5.Text) || EntryStdpurchaseAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame5.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text) || EntryStdpurchasesVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchasesVat15.Text) || EntryStdpurchasesVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchasesVat5.Text) || EntryStdpurchasesVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }



                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) || EntryZVatAmountWithSAR.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR15.Text) || EntryZVatAmountWithSAR15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame15.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR5.Text) || EntryZVatAmountWithSAR5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame5.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportspaidAdj.Text) || EntryImportspaidAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportspaidAdj15.Text) || EntryImportspaidAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame15.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportspaidAdj5.Text) || EntryImportspaidAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame5.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame5.HasError = false;
                }




                if (string.IsNullOrEmpty(EntryImportspaidVat.Text) || EntryImportspaidVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidVat15.Text) || EntryImportspaidVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidVat5.Text) || EntryImportspaidVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }




                if (string.IsNullOrEmpty(EntryImportsaccAmt.Text) || EntryImportsaccAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportsaccAmt15.Text) || EntryImportsaccAmt15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame15.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAmt5.Text) || EntryImportsaccAmt5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame5.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame5.HasError = false;
                }



                if (string.IsNullOrEmpty(EntryImportsaccAdj.Text) || EntryImportsaccAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAdj15.Text) || EntryImportsaccAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame15.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAdj5.Text) || EntryImportsaccAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame5.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportsaccVat.Text) || EntryImportsaccVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccVat15.Text) || EntryImportsaccVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccVat5.Text) || EntryImportsaccVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }




                if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) || EntryZeropurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZeropurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryZeropurchaseAmtFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) || EntryZeropurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZeropurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryZeropurchaseAdjFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) || EntryExemptpurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptpurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryExemptpurchaseAmtFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) || EntryExemptpurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptpurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryExemptpurchaseAdjFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryPreperiodcorr.Text) || EntryPreperiodcorr.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryPreperiodcorrFrame.HasError = true;
                }
                else
                {
                    if (viewModel.IsGreaterThanFiveT == false)
                    {
                        EntryPreperiodcorrFrame.HasError = false;
                    }
                    else
                    {
                        EntryPreperiodcorrFrame.HasError = true;
                    }
                }
                if (string.IsNullOrEmpty(EntryCreditVat.Text) || EntryCreditVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (viewModel.IsGreaterThanFiveT == true)
                {
                    IsAllEntered = false;
                }
                //if (string.IsNullOrEmpty(EntryNetdueVat.Text))
                //{
                //    IsAllEntered = false;
                //}
                if (IsAllEntered == false)
                {
                    viewModel.IsMainButtonEnabled = false;
                    // BtnNextStep.IsEnabled = false;
                }
                else
                {
                    // viewModel.IsDeclarationCheckedForInstruction = false;
                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
            }
            catch (Exception e)
            {

            }
            return IsAllEntered;
        }
        public bool CheckPurchaseMandetoryFields()
        {
            bool IsAllEntered = true;
            try
            {
                if (viewModel.VATDeclarationData != null && viewModel.VATDeclarationData.d != null && viewModel.VATDeclarationData.d.GoliveFg != "X")
                {
                    if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                    {
                        if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) || EntryStdpurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryStdpurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryStdpurchaseAmtFrame.HasError = false; ;
                        }
                        if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) || EntryStdpurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryStdpurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryStdpurchaseAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text) || EntryStdpurchasesVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) || EntryZVatAmountWithSAR.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZVatAmountWithSARFrame.HasError = true;
                        }
                        else
                        {
                            EntryZVatAmountWithSARFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportspaidAdj.Text) || EntryImportspaidAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportspaidAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportspaidAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportspaidVat.Text) || EntryImportspaidVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccAmt.Text) || EntryImportsaccAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportsaccAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportsaccAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccAdj.Text) || EntryImportsaccAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryImportsaccAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryImportsaccAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryImportsaccVat.Text) || EntryImportsaccVat.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                        }
                        if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) || EntryZeropurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZeropurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryZeropurchaseAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) || EntryZeropurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryZeropurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryZeropurchaseAdjFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) || EntryExemptpurchaseAmt.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptpurchaseAmtFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptpurchaseAmtFrame.HasError = false;
                        }
                        if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) || EntryExemptpurchaseAdj.TextColor == Color.Red)
                        {
                            IsAllEntered = false;
                            EntryExemptpurchaseAdjFrame.HasError = true;
                        }
                        else
                        {
                            EntryExemptpurchaseAdjFrame.HasError = false;
                        }
                        
                        if (IsAllEntered == false)
                        {
                            viewModel.IsMainButtonEnabled = false;
                            // BtnNextStep.IsEnabled = false;
                        }
                        else
                        {
                            // viewModel.IsDeclarationCheckedForInstruction = false;
                            viewModel.IsMainButtonEnabled = true;
                            //  BtnNextStep.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    if (viewModel.IsFifteenPercentChange)
                    {
                        if (viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                        {
                            bool check = CheckPurchaseMandetoryFieldsFor15Percent();
                            if (!check)
                            {
                                IsAllEntered = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return IsAllEntered;
        }

        public bool CheckPurchaseMandetoryFieldsFor15Percent()
        {
            bool IsAllEntered = true;
            try
            {
                if (string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) || EntryStdpurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame.HasError = false; ;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) || EntryStdpurchaseAmt15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame15.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame15.HasError = false; ;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) || EntryStdpurchaseAmt5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAmtFrame5.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAmtFrame5.HasError = false; ;
                }





                if (string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) || EntryStdpurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAdj15.Text) || EntryStdpurchaseAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame15.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchaseAdj5.Text) || EntryStdpurchaseAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryStdpurchaseAdjFrame5.HasError = true;
                }
                else
                {
                    EntryStdpurchaseAdjFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryStdpurchasesVat.Text) || EntryStdpurchasesVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchasesVat15.Text) || EntryStdpurchasesVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }

                if (string.IsNullOrEmpty(EntryStdpurchasesVat5.Text) || EntryStdpurchasesVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }



                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) || EntryZVatAmountWithSAR.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR15.Text) || EntryZVatAmountWithSAR15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame15.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryZVatAmountWithSAR5.Text) || EntryZVatAmountWithSAR5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZVatAmountWithSARFrame5.HasError = true;
                }
                else
                {
                    EntryZVatAmountWithSARFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportspaidAdj.Text) || EntryImportspaidAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportspaidAdj15.Text) || EntryImportspaidAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame15.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportspaidAdj5.Text) || EntryImportspaidAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportspaidAdjFrame5.HasError = true;
                }
                else
                {
                    EntryImportspaidAdjFrame5.HasError = false;
                }




                if (string.IsNullOrEmpty(EntryImportspaidVat.Text) || EntryImportspaidVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidVat15.Text) || EntryImportspaidVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportspaidVat5.Text) || EntryImportspaidVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }




                if (string.IsNullOrEmpty(EntryImportsaccAmt.Text) || EntryImportsaccAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportsaccAmt15.Text) || EntryImportsaccAmt15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame15.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAmt5.Text) || EntryImportsaccAmt5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAmtFrame5.HasError = true;
                }
                else
                {
                    EntryImportsaccAmtFrame5.HasError = false;
                }



                if (string.IsNullOrEmpty(EntryImportsaccAdj.Text) || EntryImportsaccAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAdj15.Text) || EntryImportsaccAdj15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame15.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame15.HasError = false;
                }

                if (string.IsNullOrEmpty(EntryImportsaccAdj5.Text) || EntryImportsaccAdj5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryImportsaccAdjFrame5.HasError = true;
                }
                else
                {
                    EntryImportsaccAdjFrame5.HasError = false;
                }


                if (string.IsNullOrEmpty(EntryImportsaccVat.Text) || EntryImportsaccVat.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccVat15.Text) || EntryImportsaccVat15.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }
                if (string.IsNullOrEmpty(EntryImportsaccVat5.Text) || EntryImportsaccVat5.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                }




                if (string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) || EntryZeropurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZeropurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryZeropurchaseAmtFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) || EntryZeropurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryZeropurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryZeropurchaseAdjFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) || EntryExemptpurchaseAmt.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptpurchaseAmtFrame.HasError = true;
                }
                else
                {
                    EntryExemptpurchaseAmtFrame.HasError = false;
                }
                if (string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) || EntryExemptpurchaseAdj.TextColor == Color.Red)
                {
                    IsAllEntered = false;
                    EntryExemptpurchaseAdjFrame.HasError = true;
                }
                else
                {
                    EntryExemptpurchaseAdjFrame.HasError = false;
                }
                
                if (IsAllEntered == false)
                {
                    viewModel.IsMainButtonEnabled = false;
                    // BtnNextStep.IsEnabled = false;
                }
                else
                {
                    // viewModel.IsDeclarationCheckedForInstruction = false;
                    viewModel.IsMainButtonEnabled = true;
                    //  BtnNextStep.IsEnabled = true;
                }
            }
            catch (Exception e)
            {

            }
            return IsAllEntered;
        }



        private void OnNoTapped(object sender, EventArgs e)
        {

            viewModel.IsSwitchToggledFor15PercentChange = false;
            viewModel.IsFifteenPersenctVisible = true;
            viewModel.IsFivePersenctVisible = false;
            Clear5PercentObject();

            viewModel.NoBackgroundImage = "re_Tile_Background";
            viewModel.YesBackgroundImage = "re_Property_Tile_Background_White";

            viewModel.NoLabelColor = Color.White;
            viewModel.YesLabelColor = Color.FromHex("#232323");
            viewModel.IsMainButtonEnabled = true;
        }

        public void Clear5PercentObject()
        {
            try
            {
                EntryVatAmount.Text = "0.00";
                EntryVatAdjustmentWithSAR.Text = "0.00";
                EntryStdsalesVat.Text = "0.00";

                EntryStdpurchaseAmt.Text = "0.00";
                EntryStdpurchaseAdj.Text = "0.00";
                EntryStdpurchasesVat.Text = "0.00";

                EntryZVatAmountWithSAR.Text = "0.00";
                EntryImportspaidAdj.Text = "0.00";
                EntryImportspaidVat.Text = "0.00";

                EntryImportsaccAmt.Text = "0.00";
                EntryImportsaccAdj.Text = "0.00";
                EntryImportsaccVat.Text = "0.00";

                EntryVatAmount15.Text = string.Empty;
                EntryVatAdjustmentWithSAR15.Text = string.Empty;
                EntryStdsalesVat15.Text = "0.00";

                EntryVatAmount5.Text = "0.00";
                EntryVatAdjustmentWithSAR5.Text = "0.00";
                EntryStdsalesVat5.Text = "0.00";

                //
                EntryStdpurchaseAmt15.Text = string.Empty;
                EntryStdpurchaseAdj15.Text = string.Empty;
                EntryStdpurchasesVat15.Text = "0.00";

                EntryStdpurchaseAmt5.Text = "0.00";
                EntryStdpurchaseAdj5.Text = "0.00";
                EntryStdpurchasesVat5.Text = "0.00";

                //
                EntryZVatAmountWithSAR15.Text = string.Empty;
                EntryImportspaidAdj15.Text = string.Empty;
                EntryImportspaidVat15.Text = "0.00";

                EntryZVatAmountWithSAR5.Text = "0.00";
                EntryImportspaidAdj5.Text = "0.00";
                EntryImportspaidVat5.Text = "0.00";

                //
                EntryImportsaccAmt15.Text = string.Empty;
                EntryImportsaccAdj15.Text = string.Empty;
                EntryImportsaccVat15.Text = "0.00";

                EntryImportsaccAmt5.Text = "0.00";
                EntryImportsaccAdj5.Text = "0.00";
                EntryImportsaccVat5.Text = "0.00";
            }
            catch (Exception ex)
            {

            }
        }

        public void Clear15And5PercentObject()
        {
            try
            {
                EntryVatAmount.Text = "0.00";
                EntryVatAdjustmentWithSAR.Text = "0.00";
                EntryStdsalesVat.Text = "0.00";

                EntryStdpurchaseAmt.Text = "0.00";
                EntryStdpurchaseAdj.Text = "0.00";
                EntryStdpurchasesVat.Text = "0.00";

                EntryZVatAmountWithSAR.Text = "0.00";
                EntryImportspaidAdj.Text = "0.00";
                EntryImportspaidVat.Text = "0.00";

                EntryImportsaccAmt.Text = "0.00";
                EntryImportsaccAdj.Text = "0.00";
                EntryImportsaccVat.Text = "0.00";

                EntryVatAmount15.Text = string.Empty;
                EntryVatAdjustmentWithSAR15.Text = string.Empty;
                EntryStdsalesVat15.Text = "0.00";

                EntryVatAmount5.Text = string.Empty;
                EntryVatAdjustmentWithSAR5.Text = string.Empty;
                EntryStdsalesVat5.Text = "0.00";

                //

                EntryStdpurchaseAmt15.Text = string.Empty;
                EntryStdpurchaseAdj15.Text = string.Empty;
                EntryStdpurchasesVat15.Text = "0.00";

                EntryStdpurchaseAmt5.Text = string.Empty;
                EntryStdpurchaseAdj5.Text = string.Empty;
                EntryStdpurchasesVat5.Text = "0.00";

                //

                EntryZVatAmountWithSAR15.Text = string.Empty;
                EntryImportspaidAdj15.Text = string.Empty;
                EntryImportspaidVat15.Text = "0.00";

                EntryZVatAmountWithSAR5.Text = string.Empty;
                EntryImportspaidAdj5.Text = string.Empty;
                EntryImportspaidVat5.Text = "0.00";

                //

                EntryImportsaccAmt15.Text = string.Empty;
                EntryImportsaccAdj15.Text = string.Empty;
                EntryImportsaccVat15.Text = "0.00";

                EntryImportsaccAmt5.Text = string.Empty;
                EntryImportsaccAdj5.Text = string.Empty;
                EntryImportsaccVat5.Text = "0.00";
            }
            catch (Exception ex)
            {

            }

        }
        private void EntryVatAmount_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
            //if (EntryVatAmount.TextColor == Color.Red)
            //{
            //    viewModel.IsMainButtonEnabled = false;
            //    EntryVatAmountFrame.HasError = true;
            //}
            //else
            //{
            //    viewModel.IsMainButtonEnabled = true;
            //    EntryVatAmountFrame.HasError = false;
            //}
        }

        private void EntryVatAmountFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAmount.Text == "0.00")
                {
                    EntryVatAmount.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                {
                    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    EntryVatAmount.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAmount_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {

                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                {
                    if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != "." && EntryVatAmount.Text != "," && EntryVatAdjustmentWithSAR.Text != ",")
                    {
                        CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAmount.Text);
                    EntryVatAmount.Text = ValueWithComma;
                    EntryVatAmount.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    //                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmount(object sender, EventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                    //{
                    //    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    //    EntryVatAmount.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                    //{
                    //    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    //    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                    //}
                    CheckSalesMandetoryFields();
                    // char LastChar = ' ';
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (!viewModel.IsFifteenPercentChange)
                            {
                                viewModel.StdsalesVat = viewModel.StandardRatedSalesVatAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.StdsalesAdj);
                                viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                viewModel.TotalsalesVat = viewModel.StdsalesVat;
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAdjustmentWithSAR_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        public bool isCheckArabic(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
        private void EntryVatAdjustmentFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAdjustmentWithSAR.Text == "0.00")
                {
                    EntryVatAdjustmentWithSAR.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                {
                    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        
        private void EntrySalesGccAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntrySalesGccAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntrySalesGccAmt.Text == "0.00")
                {
                    EntrySalesGccAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && EntrySalesGccAmt.Text.Contains(","))
                {
                    EntrySalesGccAmt.Text = EntrySalesGccAmt.Text.Replace(",", "");
                    EntrySalesGccAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntrySalesGccAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntrySalesGccAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntrySalesGccAdj.Text == "0.00")
                {
                    EntrySalesGccAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAdj.Text.Contains(","))
                {
                    EntrySalesGccAdj.Text = EntrySalesGccAdj.Text.Replace(",", "");
                    EntrySalesGccAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryZerosalesAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZerosalesAmt.Text == "0.00")
                {
                    EntryZerosalesAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && EntryZerosalesAmt.Text.Contains(","))
                {
                    EntryZerosalesAmt.Text = EntryZerosalesAmt.Text.Replace(",", "");
                    EntryZerosalesAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryZerosalesAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZerosalesAdj.Text == "0.00")
                {
                    EntryZerosalesAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAdj.Text.Contains(","))
                {
                    EntryZerosalesAdj.Text = EntryZerosalesAdj.Text.Replace(",", "");
                    EntryZerosalesAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryExportsAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExportsAmt.Text == "0.00")
                {
                    EntryExportsAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && EntryExportsAmt.Text.Contains(","))
                {
                    EntryExportsAmt.Text = EntryExportsAmt.Text.Replace(",", "");
                    EntryExportsAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryExportsAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExportsAdj.Text == "0.00")
                {
                    EntryExportsAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAdj.Text.Contains(","))
                {
                    EntryExportsAdj.Text = EntryExportsAdj.Text.Replace(",", "");
                    EntryExportsAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryExemptsalesAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExemptsalesAmt.Text == "0.00")
                {
                    EntryExemptsalesAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && EntryExemptsalesAmt.Text.Contains(","))
                {
                    EntryExemptsalesAmt.Text = EntryExemptsalesAmt.Text.Replace(",", "");
                    EntryExemptsalesAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }
        private void EntryStdpurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryStdpurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryZVatAmountWithSAR_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryImportspaidAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryImportsaccAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryImportsaccAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryZeropurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryZeropurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryExemptpurchaseAmt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryPreperiodcorr_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckTotalVATMandetoryFields();
        }
        private void EntryExemptpurchaseAdj_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }
        private void EntryStdpurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAmt.Text == "0.00")
                {
                    EntryStdpurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && EntryStdpurchaseAmt.Text.Contains(","))
                {
                    EntryStdpurchaseAmt.Text = EntryStdpurchaseAmt.Text.Replace(",", "");
                    EntryStdpurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryStdpurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAdj.Text == "0.00")
                {
                    EntryStdpurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAdj.Text.Contains(","))
                {
                    EntryStdpurchaseAdj.Text = EntryStdpurchaseAdj.Text.Replace(",", "");
                    EntryStdpurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZVatAmountWithSARFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZVatAmountWithSAR.Text == "0.00")
                {
                    EntryZVatAmountWithSAR.Text = String.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                {
                    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    EntryZVatAmountWithSAR.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportspaidAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportspaidAdj.Text == "0.00")
                {
                    EntryImportspaidAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                {
                    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    EntryImportspaidAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAmt.Text == "0.00")
                {
                    EntryImportsaccAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                {
                    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    EntryImportsaccAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAdj.Text == "0.00")
                {
                    EntryImportsaccAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                {
                    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    EntryImportsaccAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZeropurchaseAmt.Text == "0.00")
                {
                    EntryZeropurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && EntryZeropurchaseAmt.Text.Contains(","))
                {
                    EntryZeropurchaseAmt.Text = EntryZeropurchaseAmt.Text.Replace(",", "");
                    EntryZeropurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZeropurchaseAdj.Text == "0.00")
                {
                    EntryZeropurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAdj.Text.Contains(","))
                {
                    EntryZeropurchaseAdj.Text = EntryZeropurchaseAdj.Text.Replace(",", "");
                    EntryZeropurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAmtFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExemptpurchaseAmt.Text == "0.00")
                {
                    EntryExemptpurchaseAmt.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && EntryExemptpurchaseAmt.Text.Contains(","))
                {
                    EntryExemptpurchaseAmt.Text = EntryExemptpurchaseAmt.Text.Replace(",", "");
                    EntryExemptpurchaseAmt.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExemptpurchaseAdj.Text == "0.00")
                {
                    EntryExemptpurchaseAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAdj.Text.Contains(","))
                {
                    EntryExemptpurchaseAdj.Text = EntryExemptpurchaseAdj.Text.Replace(",", "");
                    EntryExemptpurchaseAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryPreperiodcorrFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryPreperiodcorr.Text == "0.00" || EntryPreperiodcorr.Text == "-0.00")
                {
                    EntryPreperiodcorr.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text.Contains(","))
                {
                    EntryPreperiodcorr.Text = EntryPreperiodcorr.Text.Replace(",", "");
                    EntryPreperiodcorr.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAdjustmentWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryVatAmount.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAmount.Text != "." && EntryVatAdjustmentWithSAR.Text != "." && EntryVatAmount.Text != "," && EntryVatAdjustmentWithSAR.Text != ",")
                {
                    CheckOneaOneb(Convert.ToDecimal(EntryVatAmount.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAdjustmentWithSAR.Text);
                    EntryVatAdjustmentWithSAR.Text = ValueWithComma;
                    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckOneaOneb(decimal EntryVatAmount, decimal EntryVatAdjustmentWithSAR)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (EntryVatAmount == 0 && EntryVatAdjustmentWithSAR > 0)
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                        headerAmountInfo.IsLinkAvailable = false;
                        headerAmountInfo.Message = AppResources.ZZValidationMessage02_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                        if (App.IsArabic)
                        {
                            headerAmountInfo.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            headerAmountInfo.FlowDirections = "LeftToRight";
                        }

                        headerWithInfos.Add(headerAmountInfo);


                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                        PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                    }
                    else
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryVatAmount / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryVatAmount) + EntryVatAmount < EntryVatAdjustmentWithSAR)
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            //  PopUp Pop = new PopUp();
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage01_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }
                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntrySalesGccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != "." && EntrySalesGccAmt.Text != "," && EntrySalesGccAdj.Text != ",")
                {
                    CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntrySalesGccAmt.Text);
                    EntrySalesGccAmt.Text = ValueWithComma;
                    EntrySalesGccAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntrySalesGccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && !string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAmt.Text != "." && EntrySalesGccAdj.Text != "." && EntrySalesGccAmt.Text != "," && EntrySalesGccAdj.Text != ",")
                {
                    CheckTwoaTwob(Convert.ToDecimal(EntrySalesGccAmt.Text), Convert.ToDecimal(EntrySalesGccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntrySalesGccAdj.Text);
                    EntrySalesGccAdj.Text = ValueWithComma;
                    EntrySalesGccAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckTwoaTwob(decimal EntrySalesGccAmt, decimal EntrySalesGccAdj)
        {
            try
            {

                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                {
                    try
                    {
                        if (EntrySalesGccAmt == 0 && EntrySalesGccAdj > 0)
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZValidationMessage04_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        }
                        else
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntrySalesGccAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntrySalesGccAmt) + EntrySalesGccAmt < EntrySalesGccAdj)
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage03_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    headerAmountInfo.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    headerAmountInfo.FlowDirections = "LeftToRight";
                                }

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                Xamarin.Forms.Entry Ent = (Xamarin.Forms.Entry)sender;
                string Message = string.Empty;
                if (Ent.Id.ToString() == EntryZerosalesAmt.Id.ToString())
                {
                    IGRTSetResult IGRTSetModel = viewModel.CalculationRateIGRTSet.Where(a => a.GrpNo == viewModel.ResponseVATDeclarationD.GrpNo).FirstOrDefault();
                    if (IGRTSetModel != null && IGRTSetModel.RateTrtmt != null)
                    {
                        if (IGRTSetModel.RateTrtmt != "Z")
                        {
                            if (viewModel.ResponseVATDeclarationD.ZerosalesAmt != "." && !viewModel.ResponseVATDeclarationD.ZerosalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ZerosalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ZerosalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZOurrecordsindicatethatyouarenotapartofthezerorated;
                                    //popUp.IsLinkAvailable = false;
                                    //if (App.IsArabic)
                                    //{
                                    //    popUp.FlowDirections = "RightToLeft";
                                    //}
                                    //else
                                    //{
                                    //    popUp.FlowDirections = "LeftToRight";
                                    //}
                                    //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (viewModel.ResponseVATDeclarationD.GrpNo == "00" || viewModel.ResponseVATDeclarationD.GrpNo == "0")
                        {
                            if (viewModel.ResponseVATDeclarationD.ZerosalesAmt != "." && !viewModel.ResponseVATDeclarationD.ZerosalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ZerosalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ZerosalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZOurrecordsindicatethatyouarenotapartofthezerorated;
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != "." && EntryZerosalesAmt.Text != "," && EntryZerosalesAdj.Text != ",")
                {
                    CheckThreeaThreeb(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZerosalesAmt.Text);
                    EntryZerosalesAmt.Text = ValueWithComma;
                    EntryZerosalesAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZerosalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && !string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAmt.Text != "." && EntryZerosalesAdj.Text != "." && EntryZerosalesAmt.Text != "," && EntryZerosalesAdj.Text != ",")
                {
                    CheckThreeaThreeb(Convert.ToDecimal(EntryZerosalesAmt.Text), Convert.ToDecimal(EntryZerosalesAdj.Text), "");
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZerosalesAdj.Text);
                    EntryZerosalesAdj.Text = ValueWithComma;
                    EntryZerosalesAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckThreeaThreeb(decimal EntryZerosalesAmt, decimal EntryZerosalesAdj, string Massege)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {

                    StringBuilder Masseges = new StringBuilder();
                    if (!string.IsNullOrEmpty(Massege))
                    {
                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;

                        Masseges.Append(Massege);
                        headerAmountInfo.IsLinkAvailable = false;
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZerosalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZerosalesAmt) + EntryZerosalesAmt < EntryZerosalesAdj)
                        {
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(Environment.NewLine);
                            Masseges.Append(string.Format(AppResources.ZZValidationMessage06_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                        }
                        headerAmountInfo.Message = Masseges.ToString();
                        if (headerAmountInfo.Message.Length > 0)
                        {
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                    }
                    else
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZerosalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZerosalesAmt) + EntryZerosalesAmt < EntryZerosalesAdj)
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            // PopUp Pop = new PopUp();
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage06_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExportsAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                Xamarin.Forms.Entry Ent = (Xamarin.Forms.Entry)sender;
                if (Ent.Id.ToString() == EntryExportsAmt.Id.ToString())
                {
                    if (viewModel.ResponseVATDeclarationD.ExporterFg == "0")
                    {
                        if (viewModel.ResponseVATDeclarationD.ExportsAmt != "." && !viewModel.ResponseVATDeclarationD.ExportsAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ExportsAmt))
                        {
                            if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ExportsAmt) > 0)
                            {
                                // PopUp popUp = new PopUp();
                                Message = AppResources.ZZOurrecordsindicatethatyouarenotmainly;
                                //popUp.IsLinkAvailable = false;
                                //if (App.IsArabic)
                                //{
                                //    popUp.FlowDirections = "RightToLeft";
                                //}
                                //else
                                //{
                                //    popUp.FlowDirections = "LeftToRight";
                                //}
                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != "." && EntryExportsAmt.Text != "," && EntryExportsAdj.Text != ",")
                {
                    CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExportsAmt.Text);
                    EntryExportsAmt.Text = ValueWithComma;
                    EntryExportsAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExportsAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                if (viewModel.ResponseVATDeclarationD.ExporterFg == "0")
                {
                    if (EntryExportsAmt.Text != "." && !EntryExportsAmt.Text.Contains("-") && !string.IsNullOrEmpty(EntryExportsAmt.Text))
                    {
                        if (Convert.ToDouble(EntryExportsAmt.Text) > 0)
                        {
                            // PopUp popUp = new PopUp();
                            Message = AppResources.ZZOurrecordsindicatethatyouarenotmainly;
                            //popUp.IsLinkAvailable = false;
                            //if (App.IsArabic)
                            //{
                            //    popUp.FlowDirections = "RightToLeft";
                            //}
                            //else
                            //{
                            //    popUp.FlowDirections = "LeftToRight";
                            //}
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && !string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAmt.Text != "." && EntryExportsAdj.Text != "." && EntryExportsAmt.Text != "," && EntryExportsAdj.Text != ",")
                {
                    CheckFouraFourb(Convert.ToDecimal(EntryExportsAmt.Text), Convert.ToDecimal(EntryExportsAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExportsAdj.Text);
                    EntryExportsAdj.Text = ValueWithComma;
                    EntryExportsAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckFouraFourb(decimal EntryExportsAmt, decimal EntryExportsAdj, string Massege)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        StringBuilder Masseges = new StringBuilder();
                        if (!string.IsNullOrEmpty(Massege))
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            //PopUp Pop = new PopUp();
                            headerAmountInfo.IsLinkAvailable = false;
                            Masseges.Append(Massege);
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntryExportsAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExportsAmt) + EntryExportsAmt < EntryExportsAdj)
                            {
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(string.Format(AppResources.ZZValidationMessage08_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                            }
                            headerAmountInfo.Message = Masseges.ToString();
                            if (headerAmountInfo.Message.Length > 0)
                            {
                                if (App.IsArabic)
                                {
                                    headerAmountInfo.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    headerAmountInfo.FlowDirections = "LeftToRight";
                                }

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                        else
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            // decimal PercentageValue = (EntryExportsAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExportsAmt) + EntryExportsAmt < EntryExportsAdj)
                            {
                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage08_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    headerAmountInfo.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    headerAmountInfo.FlowDirections = "LeftToRight";
                                }

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                               // PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExemptsalesAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                Xamarin.Forms.Entry Ent = (Xamarin.Forms.Entry)sender;
                if (Ent.Id.ToString() == EntryExemptsalesAmt.Id.ToString())
                {
                    IGRTSetResult IGRTSetModel = viewModel.CalculationRateIGRTSet.Where(a => a.GrpNo == viewModel.ResponseVATDeclarationD.GrpNo).FirstOrDefault();
                    if (IGRTSetModel != null && IGRTSetModel.RateTrtmt != null)
                    {
                        if (IGRTSetModel.RateTrtmt != "E")
                        {
                            if (viewModel.ResponseVATDeclarationD.ExemptsalesAmt != "." && !viewModel.ResponseVATDeclarationD.ExemptsalesAmt.Contains("-") && !string.IsNullOrEmpty(viewModel.ResponseVATDeclarationD.ExemptsalesAmt))
                            {
                                if (Convert.ToDouble(viewModel.ResponseVATDeclarationD.ExemptsalesAmt) > 0)
                                {
                                    //PopUp popUp = new PopUp();
                                    Message = AppResources.ZZValidationMessage09_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                                    //popUp.IsLinkAvailable = false;
                                    //if (App.IsArabic)
                                    //{
                                    //    popUp.FlowDirections = "RightToLeft";
                                    //}
                                    //else
                                    //{
                                    //    popUp.FlowDirections = "LeftToRight";
                                    //}
                                    //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != "." && EntryExemptsalesAmt.Text != "," && EntryExemptsalesAdj.Text != ",")
                {
                    CheckFiveaFiveb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptsalesAmt.Text);
                    EntryExemptsalesAmt.Text = ValueWithComma;
                    EntryExemptsalesAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptsalesAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                string Message = string.Empty;
                IGRTSetResult IGRTSetModel = viewModel.CalculationRateIGRTSet.Where(a => a.GrpNo == viewModel.ResponseVATDeclarationD.GrpNo).FirstOrDefault();
                if (IGRTSetModel != null && IGRTSetModel.RateTrtmt != null)
                {
                    if (IGRTSetModel.RateTrtmt != "E")
                    {
                        if (EntryExemptsalesAmt.Text != "." && !EntryExemptsalesAmt.Text.Contains("-") && !string.IsNullOrEmpty(EntryExemptsalesAmt.Text))
                        {
                            if (Convert.ToDouble(EntryExemptsalesAmt.Text) > 0)
                            {
                                //PopUp popUp = new PopUp();
                                Message = AppResources.ZZValidationMessage09_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                                //popUp.IsLinkAvailable = false;
                                //if (App.IsArabic)
                                //{
                                //    popUp.FlowDirections = "RightToLeft";
                                //}
                                //else
                                //{
                                //    popUp.FlowDirections = "LeftToRight";
                                //}
                                //  PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            }
                        }
                    }
                }


                if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && !string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAmt.Text != "." && EntryExemptsalesAdj.Text != "." && EntryExemptsalesAmt.Text != "," && EntryExemptsalesAdj.Text != ",")
                {
                    CheckFiveaFiveb(Convert.ToDecimal(EntryExemptsalesAmt.Text), Convert.ToDecimal(EntryExemptsalesAdj.Text), Message);
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptsalesAdj.Text);
                    EntryExemptsalesAdj.Text = ValueWithComma;
                    EntryExemptsalesAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckFiveaFiveb(decimal EntryExemptsalesAmt, decimal EntryExemptsalesAdj, string Message)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    StringBuilder Masseges = new StringBuilder();
                    if (!string.IsNullOrEmpty(Message))
                    {

                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                        Masseges.Append(Message);
                        if (viewModel.CalculationRateSetVTTH != null)
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            //  decimal PercentageValue = (EntryExemptsalesAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptsalesAmt) + EntryExemptsalesAmt < EntryExemptsalesAdj)
                            {
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(Environment.NewLine);
                                Masseges.Append(string.Format(AppResources.ZZValidationMessage10_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]));
                            }
                            if (Masseges.Length > 0)
                            {
                                headerAmountInfo.Message = Masseges.ToString();
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.HeaderText= AppResources.ZZZInformationNew;
                                if (App.IsArabic)
                                {
                                    headerAmountInfo.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    headerAmountInfo.FlowDirections = "LeftToRight";
                                }

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                    else
                    {
                        if (viewModel.CalculationRateSetVTTH != null)
                        {
                            string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                            //  decimal PercentageValue = (EntryExemptsalesAmt / 100) * Convert.ToDecimal(Percentage);
                            if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptsalesAmt) + EntryExemptsalesAmt < EntryExemptsalesAdj)
                            {

                                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                // PopUp Pop = new PopUp();
                                headerAmountInfo.IsLinkAvailable = false;
                                headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage10_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                                if (App.IsArabic)
                                {
                                    headerAmountInfo.FlowDirections = "RightToLeft";
                                }
                                else
                                {
                                    headerAmountInfo.FlowDirections = "LeftToRight";
                                }

                                headerWithInfos.Add(headerAmountInfo);


                                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckSixaSixb(decimal LabelTotalsalesAmt, decimal LabelTotalsalesAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //   decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalsalesAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage11_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryStdpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && EntryStdpurchaseAmt.Text != "." && LabelTotalsalesAmt.Text != "." && EntryStdpurchaseAmt.Text != "," && LabelTotalsalesAmt.Text != ",")
                {
                    CheckSevenaSixa(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(LabelTotalsalesAmt.Text));
                }
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != "." && EntryStdpurchaseAmt.Text != "," && EntryStdpurchaseAdj.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAmt.Text);
                    EntryStdpurchaseAmt.Text = ValueWithComma;
                    EntryStdpurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryStdpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj.Text) && EntryStdpurchaseAmt.Text != "." && EntryStdpurchaseAdj.Text != "." && EntryStdpurchaseAmt.Text != "," && EntryStdpurchaseAdj.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt.Text), Convert.ToDecimal(EntryStdpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAdj.Text);
                    EntryStdpurchaseAdj.Text = ValueWithComma;
                    EntryStdpurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckSevenaSixa(decimal EntryStdpurchaseAmt, decimal LabelTotalsalesAmt)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                if (EntryStdpurchaseAmt > LabelTotalsalesAmt)
                {

                    List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                    HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                    NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                    headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                    // PopUp Pop = new PopUp();
                    headerAmountInfo.IsLinkAvailable = false;
                    headerAmountInfo.Message = AppResources.ZZValidationMessage12_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                    if (App.IsArabic)
                    {
                        headerAmountInfo.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        headerAmountInfo.FlowDirections = "LeftToRight";
                    }

                    headerWithInfos.Add(headerAmountInfo);


                    newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                    newDesignPopUp.HeaderWithInfos = headerWithInfos;
                    newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                    PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                }
            }
        }
        public void CheckSevenaSevenb(decimal EntryStdpurchaseAmt, decimal EntryStdpurchaseAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryStdpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryStdpurchaseAmt) + EntryStdpurchaseAmt < EntryStdpurchaseAdj)
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                            // PopUp Pop = new PopUp();
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage13_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryZVatAmountWithSAR_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != "." && EntryZVatAmountWithSAR.Text != "," && EntryImportspaidAdj.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZVatAmountWithSAR.Text);
                    EntryZVatAmountWithSAR.Text = ValueWithComma;
                    EntryZVatAmountWithSAR.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportspaidAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryZVatAmountWithSAR.Text != "." && EntryImportspaidAdj.Text != "." && EntryZVatAmountWithSAR.Text != "," && EntryImportspaidAdj.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR.Text), Convert.ToDecimal(EntryImportspaidAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportspaidAdj.Text);
                    EntryImportspaidAdj.Text = ValueWithComma;
                    EntryImportspaidAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckEightaEightb(decimal EntryZVatAmountWithSAR, decimal EntryImportspaidAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryZVatAmountWithSAR / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZVatAmountWithSAR) + EntryZVatAmountWithSAR < EntryImportspaidAdj)
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage14_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);

                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryImportsaccAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != "." && EntryImportsaccAmt.Text != "," && EntryImportsaccAdj.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAmt.Text);
                    EntryImportsaccAmt.Text = ValueWithComma;
                    EntryImportsaccAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryImportsaccAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAmt.Text != "." && EntryImportsaccAdj.Text != "." && EntryImportsaccAmt.Text != "," && EntryImportsaccAdj.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt.Text), Convert.ToDecimal(EntryImportsaccAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAdj.Text);
                    EntryImportsaccAdj.Text = ValueWithComma;
                    EntryImportsaccAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckNineaNineb(decimal EntryImportsaccAmt, decimal EntryImportsaccAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryImportsaccAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryImportsaccAmt) + EntryImportsaccAmt < EntryImportsaccAdj)
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage15_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));


                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryZeropurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != "." && EntryZeropurchaseAmt.Text != "," && EntryZeropurchaseAdj.Text != ",")
                {
                    CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZeropurchaseAmt.Text);
                    EntryZeropurchaseAmt.Text = ValueWithComma;
                    EntryZeropurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryZeropurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && !string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAmt.Text != "." && EntryZeropurchaseAdj.Text != "." && EntryZeropurchaseAmt.Text != "," && EntryZeropurchaseAdj.Text != ",")
                {
                    CheckTenaTenb(Convert.ToDecimal(EntryZeropurchaseAmt.Text), Convert.ToDecimal(EntryZeropurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZeropurchaseAdj.Text);
                    EntryZeropurchaseAdj.Text = ValueWithComma;
                    EntryZeropurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckTenaTenb(decimal EntryZeropurchaseAmt, decimal EntryZeropurchaseAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //   decimal PercentageValue = (EntryZeropurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryZeropurchaseAmt) + EntryZeropurchaseAmt < EntryZeropurchaseAdj)
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage16_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }
                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void EntryExemptpurchaseAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != "." && EntryExemptpurchaseAmt.Text != "," && EntryExemptpurchaseAdj.Text != ",")
                {
                    CheckElevenaElevenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptpurchaseAmt.Text);
                    EntryExemptpurchaseAmt.Text = ValueWithComma;
                    EntryExemptpurchaseAmt.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void EntryExemptpurchaseAdj_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && !string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAmt.Text != "." && EntryExemptpurchaseAdj.Text != "." && EntryExemptpurchaseAmt.Text != "," && EntryExemptpurchaseAdj.Text != ",")
                {
                    CheckElevenaElevenb(Convert.ToDecimal(EntryExemptpurchaseAmt.Text), Convert.ToDecimal(EntryExemptpurchaseAdj.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryExemptpurchaseAdj.Text);
                    EntryExemptpurchaseAdj.Text = ValueWithComma;
                    EntryExemptpurchaseAdj.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckElevenaElevenb(decimal EntryExemptpurchaseAmt, decimal EntryExemptpurchaseAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (EntryExemptpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * EntryExemptpurchaseAmt) + EntryExemptpurchaseAmt < EntryExemptpurchaseAdj)
                        {

                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = string.Format(AppResources.ZZValidationMessage17_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                headerAmountInfo.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                headerAmountInfo.FlowDirections = "LeftToRight";
                            }

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckSixaTweveb(decimal LabelTotalsalesAmt, decimal LabelTotalpurchaseAmt)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                if (viewModel.CalculationRateSetVTTH != null)
                {
                    string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                    //  decimal PercentageValue = (LabelTotalsalesAmt / 100) * Convert.ToDecimal(Percentage);
                    if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalsalesAmt) + LabelTotalsalesAmt < LabelTotalpurchaseAmt)
                    {
                        PopUp Pop = new PopUp();
                        Pop.IsLinkAvailable = false;
                        Pop.Message = AppResources.ZZValidationMessage18_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero;
                        if (App.IsArabic)
                        {
                            Pop.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            Pop.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                    }
                }
            }
        }
        public void CheckTweveaTweveb(decimal LabelTotalpurchaseAmt, decimal LabelTotalpurchaseAdj)
        {

            if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
            {
                try
                {
                    if (viewModel.CalculationRateSetVTTH != null)
                    {
                        string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                        //  decimal PercentageValue = (LabelTotalpurchaseAmt / 100) * Convert.ToDecimal(Percentage);
                        if (((Convert.ToDecimal(Percentage) / 100) * LabelTotalpurchaseAmt) + LabelTotalpurchaseAmt < LabelTotalpurchaseAdj)
                        {
                            PopUp Pop = new PopUp();
                            Pop.IsLinkAvailable = false;
                            Pop.Message = string.Format(AppResources.ZZValidationMessage19_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage.Split('.')[0]);
                            if (App.IsArabic)
                            {
                                Pop.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                Pop.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                        }
                    }
                }
                catch
                {
                }
            }
        }
        public void CheckThirteenaFouteenb(decimal LabelTotaldueVat, decimal EntryPreperiodcorr)
        {
            try
            {
                if (viewModel.CalculationRateSetVTTH != null)
                {
                    string Percentage = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "002").Select(x => x.Percentage).FirstOrDefault();
                    //  decimal PercentageValue = (LabelTotaldueVat / 100) * Convert.ToDecimal(Percentage);
                    if (((Convert.ToDecimal(Percentage) / 100) * LabelTotaldueVat) + LabelTotaldueVat < EntryPreperiodcorr)
                    {
                        //PopUp Pop = new PopUp();
                        //Pop.IsLinkAvailable = false;
                        //Pop.Message = string.Format(AppResources.ZZValidationMessage20_IfThresholdType002AndZTTH_VTTH_PerNotEqualToZero, Percentage);
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                    }
                }
            }
            catch
            {
            }
        }
        private void EntryPreperiodcorr_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (EntryPreperiodcorr.Text.Equals("-.") || EntryPreperiodcorr.Text.Equals("."))
                {
                    EntryPreperiodcorr.Text = "0.00";
                    viewModel.IsSwitchToggled = false;
                    return;
                }
                if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text != "." && EntryPreperiodcorr.Text != "-" && EntryPreperiodcorr.Text != ",")
                {
                    string MinValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MinVal).FirstOrDefault();
                    string MaxValue = viewModel.CalculationRateSetVTTH.Where(a => a.Type == "001").Select(x => x.MaxVal).FirstOrDefault();
                    if (Convert.ToDecimal(EntryPreperiodcorr.Text) <= Convert.ToDecimal(MinValue) || Convert.ToDecimal(EntryPreperiodcorr.Text) >= Convert.ToDecimal(MaxValue))
                    {
                        viewModel._dialogService.ShowMessage(string.Format(AppResources.ZZGeneralMessage_IfCorrectionsGreaterThanEqualToMAxValueAndLessThanEqualToMinValue, MaxValue, MinValue), AppResources.Information);
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryPreperiodcorr.Text);
                    EntryPreperiodcorr.Text = ValueWithComma;
                    EntryPreperiodcorr.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckTotalVATMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch
            {
            }
        }

         private void EntryVatAmount15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }

        private void EntryVatAdjustmentWithSAR15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }

        private void EntryVatAmount15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAmount15.Text == "0.00")
                {
                    EntryVatAmount15.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAmount15.Text) && EntryVatAmount15.Text.Contains(","))
                {
                    EntryVatAmount15.Text = EntryVatAmount15.Text.Replace(",", "");
                    EntryVatAmount15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAdjustment15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAdjustmentWithSAR15.Text == "0.00")
                {
                    EntryVatAdjustmentWithSAR15.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAdjustmentWithSAR15.Text) && EntryVatAdjustmentWithSAR15.Text.Contains(","))
                {
                    EntryVatAdjustmentWithSAR15.Text = EntryVatAdjustmentWithSAR15.Text.Replace(",", "");
                    EntryVatAdjustmentWithSAR15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }


        }

        private void EntryVatAmount15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {

                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                {
                    if (!string.IsNullOrEmpty(EntryVatAmount15.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR15.Text) && EntryVatAmount15.Text != "." && EntryVatAdjustmentWithSAR15.Text != "." && EntryVatAmount15.Text != "," && EntryVatAdjustmentWithSAR15.Text != ",")
                    {
                        CheckOneaOneb(Convert.ToDecimal(EntryVatAmount15.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR15.Text));
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAmount15.Text);
                    EntryVatAmount15.Text = ValueWithComma;
                    EntryVatAmount15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAdjustmentWithSAR15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryVatAmount15.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR15.Text) && EntryVatAmount15.Text != "." && EntryVatAdjustmentWithSAR15.Text != "." && EntryVatAmount15.Text != "," && EntryVatAdjustmentWithSAR15.Text != ",")
                {
                    CheckOneaOneb(Convert.ToDecimal(EntryVatAmount15.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAdjustmentWithSAR15.Text);
                    EntryVatAdjustmentWithSAR15.Text = ValueWithComma;
                    EntryVatAdjustmentWithSAR15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmount15(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                    //{
                    //    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    //    EntryVatAmount.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                    //{
                    //    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    //    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                    //}
                    CheckSalesMandetoryFields();
                    // char LastChar = ' ';
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {


                            viewModel.StdsalesVat15 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATRate002For15Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.StdsalesVat = viewModel.AddTwoAmount(viewModel.StdsalesVat15, viewModel.StdsalesVat5);
                                viewModel.ResponseVATDeclarationD.StdsalesAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAmt);
                                viewModel.ResponseVATDeclarationD.StdsalesAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATNewModelFor5Percent.StdsalesAdj);

                                viewModel.TotalsalesAmt = viewModel.TotalAmountForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                viewModel.TotalsalesAdj = viewModel.TotalAdjustmentForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATNewModelFor5Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                viewModel.TotalsalesVat = viewModel.AddTwoAmount(viewModel.StdsalesVat15, viewModel.StdsalesVat5);
                            }
                            else
                            {
                                viewModel.StdsalesVat = viewModel.GetSingleAmount(viewModel.StdsalesVat15);
                                viewModel.ResponseVATDeclarationD.StdsalesAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt);
                                viewModel.ResponseVATDeclarationD.StdsalesAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdsalesAdj);


                                viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                viewModel.TotalsalesVat = viewModel.StdsalesVat15;
                            }

                            //viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                            //viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                            //viewModel.TotalsalesVat = viewModel.StdsalesVat;
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAmount5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }

        private void EntryVatAmount5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAmount5.Text == "0.00")
                {
                    EntryVatAmount5.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAmount5.Text) && EntryVatAmount5.Text.Contains(","))
                {
                    EntryVatAmount5.Text = EntryVatAmount5.Text.Replace(",", "");
                    EntryVatAmount5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAmount5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {

                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                {
                    if (!string.IsNullOrEmpty(EntryVatAmount5.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR5.Text) && EntryVatAmount5.Text != "." && EntryVatAdjustmentWithSAR5.Text != "." && EntryVatAmount5.Text != "," && EntryVatAdjustmentWithSAR5.Text != ",")
                    {
                        CheckOneaOneb(Convert.ToDecimal(EntryVatAmount5.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR5.Text));
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAmount5.Text);
                    EntryVatAmount5.Text = ValueWithComma;
                    EntryVatAmount5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryVatAdjustmentWithSAR5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckSalesMandetoryFields();
        }

        private void EntryVatAdjustment5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryVatAdjustmentWithSAR5.Text == "0.00")
                {
                    EntryVatAdjustmentWithSAR5.Text = string.Empty;
                }
                if (!String.IsNullOrEmpty(EntryVatAdjustmentWithSAR5.Text) && EntryVatAdjustmentWithSAR5.Text.Contains(","))
                {
                    EntryVatAdjustmentWithSAR5.Text = EntryVatAdjustmentWithSAR5.Text.Replace(",", "");
                    EntryVatAdjustmentWithSAR5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void EntryVatAdjustmentWithSAR5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {

                if (viewModel.currentTab == VATReturnUpdatedUITabEnum.VATReturns || viewModel.currentTab == VATReturnUpdatedUITabEnum.Sales || viewModel.currentTab == VATReturnUpdatedUITabEnum.Purchase)
                {
                    if (!string.IsNullOrEmpty(EntryVatAmount5.Text) && !string.IsNullOrEmpty(EntryVatAdjustmentWithSAR5.Text) && EntryVatAmount5.Text != "." && EntryVatAdjustmentWithSAR5.Text != "." && EntryVatAmount5.Text != "," && EntryVatAdjustmentWithSAR5.Text != ",")
                    {
                        CheckOneaOneb(Convert.ToDecimal(EntryVatAmount5.Text), Convert.ToDecimal(EntryVatAdjustmentWithSAR5.Text));
                    }
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryVatAdjustmentWithSAR5.Text);
                    EntryVatAdjustmentWithSAR5.Text = ValueWithComma;
                    EntryVatAdjustmentWithSAR5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckSalesMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAmt15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAmt15.Text == "0.00")
                {
                    EntryStdpurchaseAmt15.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) && EntryStdpurchaseAmt15.Text.Contains(","))
                {
                    EntryStdpurchaseAmt15.Text = EntryStdpurchaseAmt15.Text.Replace(",", "");
                    EntryStdpurchaseAmt15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAdj15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAdj15.Text == "0.00")
                {
                    EntryStdpurchaseAdj15.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAdj15.Text) && EntryStdpurchaseAdj15.Text.Contains(","))
                {
                    EntryStdpurchaseAdj15.Text = EntryStdpurchaseAdj15.Text.Replace(",", "");
                    EntryStdpurchaseAdj15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAmt15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryStdpurchaseAdj15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryStdpurchaseAmt15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (viewModel.IsNoChecked)
                {
                    if (!string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) && !string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && EntryStdpurchaseAmt15.Text != "." && LabelTotalsalesAmt.Text != "." && EntryStdpurchaseAmt15.Text != "," && LabelTotalsalesAmt.Text != ",")
                    {
                        CheckSevenaSixa(Convert.ToDecimal(EntryStdpurchaseAmt15.Text), Convert.ToDecimal(LabelTotalsalesAmt.Text));
                    }
                }
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj15.Text) && EntryStdpurchaseAmt15.Text != "." && EntryStdpurchaseAdj15.Text != "." && EntryStdpurchaseAmt15.Text != "," && EntryStdpurchaseAdj15.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt15.Text), Convert.ToDecimal(EntryStdpurchaseAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAmt15.Text);
                    EntryStdpurchaseAmt15.Text = ValueWithComma;
                    EntryStdpurchaseAmt15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAdj15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt15.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj15.Text) && EntryStdpurchaseAmt15.Text != "." && EntryStdpurchaseAdj15.Text != "." && EntryStdpurchaseAmt15.Text != "," && EntryStdpurchaseAdj15.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt15.Text), Convert.ToDecimal(EntryStdpurchaseAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAdj15.Text);
                    EntryStdpurchaseAdj15.Text = ValueWithComma;
                    EntryStdpurchaseAdj15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAmt5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAmt5.Text == "0.00")
                {
                    EntryStdpurchaseAmt5.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) && EntryStdpurchaseAmt5.Text.Contains(","))
                {
                    EntryStdpurchaseAmt5.Text = EntryStdpurchaseAmt5.Text.Replace(",", "");
                    EntryStdpurchaseAmt5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAmt5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryStdpurchaseAmt5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (viewModel.IsYesChecked)
                {
                    if (!string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) && !string.IsNullOrEmpty(LabelTotalsalesAmt.Text) && EntryStdpurchaseAmt5.Text != "." && LabelTotalsalesAmt.Text != "." && EntryStdpurchaseAmt5.Text != "," && LabelTotalsalesAmt.Text != ",")
                    {
                        CheckSevenaSixa(Convert.ToDecimal(EntryStdpurchaseAmt5.Text), Convert.ToDecimal(LabelTotalsalesAmt.Text));
                    }
                }
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj5.Text) && EntryStdpurchaseAmt5.Text != "." && EntryStdpurchaseAdj5.Text != "." && EntryStdpurchaseAmt5.Text != "," && EntryStdpurchaseAdj5.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt5.Text), Convert.ToDecimal(EntryStdpurchaseAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAmt5.Text);
                    EntryStdpurchaseAmt5.Text = ValueWithComma;
                    EntryStdpurchaseAmt5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAdj5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryStdpurchaseAdj5.Text == "0.00")
                {
                    EntryStdpurchaseAdj5.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryStdpurchaseAdj5.Text) && EntryStdpurchaseAdj5.Text.Contains(","))
                {
                    EntryStdpurchaseAdj5.Text = EntryStdpurchaseAdj5.Text.Replace(",", "");
                    EntryStdpurchaseAdj5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryStdpurchaseAdj5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryStdpurchaseAdj5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryStdpurchaseAmt5.Text) && !string.IsNullOrEmpty(EntryStdpurchaseAdj5.Text) && EntryStdpurchaseAmt5.Text != "." && EntryStdpurchaseAdj5.Text != "." && EntryStdpurchaseAmt5.Text != "," && EntryStdpurchaseAdj5.Text != ",")
                {
                    CheckSevenaSevenb(Convert.ToDecimal(EntryStdpurchaseAmt5.Text), Convert.ToDecimal(EntryStdpurchaseAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryStdpurchaseAdj5.Text);
                    EntryStdpurchaseAdj5.Text = ValueWithComma;
                    EntryStdpurchaseAdj5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryZVatAmountWithSAR15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZVatAmountWithSAR15.Text == "0.00")
                {
                    EntryZVatAmountWithSAR15.Text = String.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR15.Text) && EntryZVatAmountWithSAR15.Text.Contains(","))
                {
                    EntryZVatAmountWithSAR15.Text = EntryZVatAmountWithSAR15.Text.Replace(",", "");
                    EntryZVatAmountWithSAR15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryZVatAmountWithSAR15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryZVatAmountWithSAR15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR15.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj15.Text) && EntryZVatAmountWithSAR15.Text != "." && EntryImportspaidAdj15.Text != "." && EntryZVatAmountWithSAR15.Text != "," && EntryImportspaidAdj15.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR15.Text), Convert.ToDecimal(EntryImportspaidAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZVatAmountWithSAR15.Text);
                    EntryZVatAmountWithSAR15.Text = ValueWithComma;
                    EntryZVatAmountWithSAR15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportspaidAdj15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportspaidAdj15.Text == "0.00")
                {
                    EntryImportspaidAdj15.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportspaidAdj15.Text) && EntryImportspaidAdj15.Text.Contains(","))
                {
                    EntryImportspaidAdj15.Text = EntryImportspaidAdj15.Text.Replace(",", "");
                    EntryImportspaidAdj15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportspaidAdj15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportspaidAdj15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR15.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj15.Text) && EntryZVatAmountWithSAR15.Text != "." && EntryImportspaidAdj15.Text != "." && EntryZVatAmountWithSAR15.Text != "," && EntryImportspaidAdj15.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR15.Text), Convert.ToDecimal(EntryImportspaidAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportspaidAdj15.Text);
                    EntryImportspaidAdj15.Text = ValueWithComma;
                    EntryImportspaidAdj15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryZVatAmountWithSAR5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryZVatAmountWithSAR5.Text == "0.00")
                {
                    EntryZVatAmountWithSAR5.Text = String.Empty;
                }

                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR5.Text) && EntryZVatAmountWithSAR5.Text.Contains(","))
                {
                    EntryZVatAmountWithSAR5.Text = EntryZVatAmountWithSAR5.Text.Replace(",", "");
                    EntryZVatAmountWithSAR5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryZVatAmountWithSAR5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryZVatAmountWithSAR5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR5.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj5.Text) && EntryZVatAmountWithSAR5.Text != "." && EntryImportspaidAdj5.Text != "." && EntryZVatAmountWithSAR5.Text != "," && EntryImportspaidAdj5.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR5.Text), Convert.ToDecimal(EntryImportspaidAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryZVatAmountWithSAR5.Text);
                    EntryZVatAmountWithSAR5.Text = ValueWithComma;
                    EntryZVatAmountWithSAR5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportspaidAdj5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportspaidAdj5.Text == "0.00")
                {
                    EntryImportspaidAdj5.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportspaidAdj5.Text) && EntryImportspaidAdj5.Text.Contains(","))
                {
                    EntryImportspaidAdj5.Text = EntryImportspaidAdj5.Text.Replace(",", "");
                    EntryImportspaidAdj5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportspaidAdj5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportspaidAdj5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR5.Text) && !string.IsNullOrEmpty(EntryImportspaidAdj5.Text) && EntryZVatAmountWithSAR5.Text != "." && EntryImportspaidAdj5.Text != "." && EntryZVatAmountWithSAR5.Text != "," && EntryImportspaidAdj5.Text != ",")
                {
                    CheckEightaEightb(Convert.ToDecimal(EntryZVatAmountWithSAR5.Text), Convert.ToDecimal(EntryImportspaidAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportspaidAdj5.Text);
                    EntryImportspaidAdj5.Text = ValueWithComma;
                    EntryImportspaidAdj5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAmt15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAmt15.Text == "0.00")
                {
                    EntryImportsaccAmt15.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAmt15.Text) && EntryImportsaccAmt15.Text.Contains(","))
                {
                    EntryImportsaccAmt15.Text = EntryImportsaccAmt15.Text.Replace(",", "");
                    EntryImportsaccAmt15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAmt15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportsaccAmt15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt15.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj15.Text) && EntryImportsaccAmt15.Text != "." && EntryImportsaccAdj15.Text != "." && EntryImportsaccAmt15.Text != "," && EntryImportsaccAdj15.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt15.Text), Convert.ToDecimal(EntryImportsaccAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAmt15.Text);
                    EntryImportsaccAmt15.Text = ValueWithComma;
                    EntryImportsaccAmt15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAdj15Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAdj15.Text == "0.00")
                {
                    EntryImportsaccAdj15.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAdj15.Text) && EntryImportsaccAdj15.Text.Contains(","))
                {
                    EntryImportsaccAdj15.Text = EntryImportsaccAdj15.Text.Replace(",", "");
                    EntryImportsaccAdj15.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAdj15_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportsaccAdj15_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt15.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj15.Text) && EntryImportsaccAmt15.Text != "." && EntryImportsaccAdj15.Text != "." && EntryImportsaccAmt15.Text != "," && EntryImportsaccAdj15.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt15.Text), Convert.ToDecimal(EntryImportsaccAdj15.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAdj15.Text);
                    EntryImportsaccAdj15.Text = ValueWithComma;
                    EntryImportsaccAdj15.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAmt5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAmt5.Text == "0.00")
                {
                    EntryImportsaccAmt5.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAmt5.Text) && EntryImportsaccAmt5.Text.Contains(","))
                {
                    EntryImportsaccAmt5.Text = EntryImportsaccAmt5.Text.Replace(",", "");
                    EntryImportsaccAmt5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAmt5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportsaccAmt5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt5.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj5.Text) && EntryImportsaccAmt5.Text != "." && EntryImportsaccAdj5.Text != "." && EntryImportsaccAmt5.Text != "," && EntryImportsaccAdj5.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt5.Text), Convert.ToDecimal(EntryImportsaccAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAmt5.Text);
                    EntryImportsaccAmt5.Text = ValueWithComma;
                    EntryImportsaccAmt5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAdj5Focused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryImportsaccAdj5.Text == "0.00")
                {
                    EntryImportsaccAdj5.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryImportsaccAdj5.Text) && EntryImportsaccAdj5.Text.Contains(","))
                {
                    EntryImportsaccAdj5.Text = EntryImportsaccAdj5.Text.Replace(",", "");
                    EntryImportsaccAdj5.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryImportsaccAdj5_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckPurchaseMandetoryFields();
        }

        private void EntryImportsaccAdj5_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EntryImportsaccAmt5.Text) && !string.IsNullOrEmpty(EntryImportsaccAdj5.Text) && EntryImportsaccAmt5.Text != "." && EntryImportsaccAdj5.Text != "." && EntryImportsaccAmt5.Text != "," && EntryImportsaccAdj5.Text != ",")
                {
                    CheckNineaNineb(Convert.ToDecimal(EntryImportsaccAmt5.Text), Convert.ToDecimal(EntryImportsaccAdj5.Text));
                }
                if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                {
                    viewModel.IsUnFocusedTextBox = true;
                    string ValueWithComma = UtilityManager.GetCommaSeparatedAmount(EntryImportsaccAdj5.Text);
                    EntryImportsaccAdj5.Text = ValueWithComma;
                    EntryImportsaccAdj5.TextColor = Color.Black;
                }
                else
                {
                    viewModel.IsUnFocusedTextBox = true;
                    viewModel.IsMainButtonEnabled = false;
                    CheckPurchaseMandetoryFields();
                    // UserName.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmount5Percent(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryVatAmount.Text) && EntryVatAmount.Text.Contains(","))
                    //{
                    //    EntryVatAmount.Text = EntryVatAmount.Text.Replace(",", "");
                    //    EntryVatAmount.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryVatAdjustmentWithSAR.Text) && EntryVatAdjustmentWithSAR.Text.Contains(","))
                    //{
                    //    EntryVatAdjustmentWithSAR.Text = EntryVatAdjustmentWithSAR.Text.Replace(",", "");
                    //    EntryVatAdjustmentWithSAR.TextColor = Color.Black;
                    //}
                    CheckSalesMandetoryFields();
                    // char LastChar = ' ';
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {


                            viewModel.StdsalesVat5 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor5Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAdj, viewModel.VATRate003For5Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.StdsalesVat = viewModel.AddTwoAmount(viewModel.StdsalesVat15, viewModel.StdsalesVat5);
                                viewModel.ResponseVATDeclarationD.StdsalesAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAmt);
                                viewModel.ResponseVATDeclarationD.StdsalesAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATNewModelFor5Percent.StdsalesAdj);


                                viewModel.TotalsalesAmt = viewModel.TotalAmountForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                viewModel.TotalsalesAdj = viewModel.TotalAdjustmentForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATNewModelFor5Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                viewModel.TotalsalesVat = viewModel.AddTwoAmount(viewModel.StdsalesVat15, viewModel.StdsalesVat5);
                            }
                            //else
                            //{
                            //    viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                            //    viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                            //    viewModel.TotalsalesVat = viewModel.StdsalesVat15;
                            //}

                            //viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                            //viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                            //viewModel.TotalsalesVat = viewModel.StdsalesVat;
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase15(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.StdpurchasesVat15 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATRate002For15Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.StdpurchasesVat = viewModel.AddTwoAmount(viewModel.StdpurchasesVat15, viewModel.StdpurchasesVat5);

                                viewModel.ResponseVATDeclarationD.StdpurchaseAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt);

                                viewModel.ResponseVATDeclarationD.StdpurchaseAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            else
                            {
                                viewModel.StdpurchasesVat = viewModel.GetSingleAmount(viewModel.StdpurchasesVat15);
                                viewModel.ResponseVATDeclarationD.StdpurchaseAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt);
                                viewModel.ResponseVATDeclarationD.StdpurchaseAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAdj);


                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);


                            }


                            //viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase5(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.StdpurchasesVat5 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATRate003For5Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.StdpurchasesVat = viewModel.AddTwoAmount(viewModel.StdpurchasesVat15, viewModel.StdpurchasesVat5);

                                viewModel.ResponseVATDeclarationD.StdpurchaseAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt);

                                viewModel.ResponseVATDeclarationD.StdpurchaseAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            //else
                            //{
                            //    viewModel.StdpurchasesVat = viewModel.GetSingleAmount(viewModel.StdpurchasesVat15);
                            //    viewModel.ResponseVATDeclarationD.StdpurchaseAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt);
                            //    viewModel.ResponseVATDeclarationD.StdpurchaseAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAdj);


                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);


                            //}


                            //viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms15(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                    //{
                    //    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    //    EntryZVatAmountWithSAR.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                    //{
                    //    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    //    EntryImportspaidAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.ResponseVATDeclarationD != null && viewModel.ResponseVATDeclarationD.TpregFg == "X")
                            {
                                viewModel.ImportspaidVat15 = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATRate002For15Percent);
                            }
                            else
                            {
                                viewModel.ImportspaidVat15 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATRate002For15Percent);
                            }


                            if (viewModel.IsYesChecked)
                            {
                                viewModel.ImportspaidVat = viewModel.AddTwoAmount(viewModel.ImportspaidVat15, viewModel.ImportspaidVat5);

                                viewModel.ResponseVATDeclarationD.ImportspaidAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt);

                                viewModel.ResponseVATDeclarationD.ImportspaidAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            else
                            {
                                viewModel.ImportspaidVat = viewModel.GetSingleAmount(viewModel.ImportspaidVat15);
                                viewModel.ResponseVATDeclarationD.ImportspaidAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.ImportspaidAmt);
                                viewModel.ResponseVATDeclarationD.ImportspaidAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.ImportspaidAdj);


                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);


                            }

                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            //}
                        }

                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms5(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                    //{
                    //    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    //    EntryZVatAmountWithSAR.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                    //{
                    //    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    //    EntryImportspaidAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.ResponseVATDeclarationD != null && viewModel.ResponseVATDeclarationD.TpregFg == "X")
                            {
                                viewModel.ImportspaidVat5 = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATRate003For5Percent);
                            }
                            else
                            {
                                viewModel.ImportspaidVat5 = viewModel.StandardRatedSalesVatAmountForNewChangeRate(viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATRate003For5Percent);
                            }


                            if (viewModel.IsYesChecked)
                            {
                                viewModel.ImportspaidVat = viewModel.AddTwoAmount(viewModel.ImportspaidVat15, viewModel.ImportspaidVat5);

                                viewModel.ResponseVATDeclarationD.ImportspaidAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt);

                                viewModel.ResponseVATDeclarationD.ImportspaidAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            else
                            {
                                //viewModel.ImportspaidVat = viewModel.GetSingleAmount(viewModel.ImportspaidVat15);
                                //viewModel.ResponseVATDeclarationD.ImportspaidAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor5Percent.ImportspaidAmt);
                                //viewModel.ResponseVATDeclarationD.ImportspaidAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor5Percent.ImportspaidAdj);


                                //viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                //viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);


                            }

                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            //}
                        }

                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                   }
               }
            catch (Exception ex)
            {

            }
        }

        private void ClickGestureRecognizer_ClickedForVatAccounted15(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                    //{
                    //    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    //    EntryImportsaccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                    //{
                    //    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    //    EntryImportsaccAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.ImportsaccVat15 = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATRate002For15Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.ImportsaccVat = viewModel.AddTwoAmount(viewModel.ImportsaccVat15, viewModel.ImportsaccVat5);

                                viewModel.ResponseVATDeclarationD.ImportsaccAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt);

                                viewModel.ResponseVATDeclarationD.ImportsaccAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            else
                            {
                                viewModel.ImportsaccVat = viewModel.GetSingleAmount(viewModel.ImportsaccVat15);
                                viewModel.ResponseVATDeclarationD.ImportsaccAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.ImportsaccAmt);
                                viewModel.ResponseVATDeclarationD.ImportsaccAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor15Percent.ImportsaccAdj);


                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }


                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            //}
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAccounted5(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                    //{
                    //    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    //    EntryImportsaccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                    //{
                    //    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    //    EntryImportsaccAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.ImportsaccVat5 = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignatedForNewPercentage(viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.VATRate003For5Percent);

                            if (viewModel.IsYesChecked)
                            {
                                viewModel.ImportsaccVat = viewModel.AddTwoAmount(viewModel.ImportsaccVat15, viewModel.ImportsaccVat5);

                                viewModel.ResponseVATDeclarationD.ImportsaccAmt = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt);

                                viewModel.ResponseVATDeclarationD.ImportsaccAdj = viewModel.AddTwoAmount(viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj);

                                viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);

                                viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            }
                            //else
                            //{
                            //    viewModel.ImportsaccVat = viewModel.GetSingleAmount(viewModel.ImportsaccVat15);
                            //    viewModel.ResponseVATDeclarationD.ImportsaccAmt = viewModel.GetSingleAmount(viewModel.VATNewModelFor5Percent.ImportsaccAmt);
                            //    viewModel.ResponseVATDeclarationD.ImportsaccAdj = viewModel.GetSingleAmount(viewModel.VATNewModelFor5Percent.ImportsaccAdj);


                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);

                            //}


                            //    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            //    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            //}
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Yesrdbuttonclicked(object sender, EventArgs e)
        {
            try
            {
                RadioButton rd = sender as RadioButton;
                if (rd != null)
                {
                    if(rd.IsChecked)
                    {
                        viewModel.IsFifteenPersenctVisible = true;
                        viewModel.IsFivePersenctVisible = true;
                        Clear15And5PercentObject();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void Nordbuttonclicked(object sender, EventArgs e)
        {
            try
            {
                RadioButton rd = sender as RadioButton;
                if (rd != null)
                {
                    if (rd.IsChecked)
                    {
                        viewModel.IsFifteenPersenctVisible = true;
                        viewModel.IsFivePersenctVisible = false;
                        Clear5PercentObject();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void ClickGestureRecognizer_ClickedForAllAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntrySalesGccAmt.Text) && EntrySalesGccAmt.Text.Contains(","))
                    //{
                    //    EntrySalesGccAmt.Text = EntrySalesGccAmt.Text.Replace(",", "");
                    //    EntrySalesGccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryZerosalesAmt.Text) && EntryZerosalesAmt.Text.Contains(","))
                    //{
                    //    EntryZerosalesAmt.Text = EntryZerosalesAmt.Text.Replace(",", "");
                    //    EntryZerosalesAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExportsAmt.Text) && EntryExportsAmt.Text.Contains(","))
                    //{
                    //    EntryExportsAmt.Text = EntryExportsAmt.Text.Replace(",", "");
                    //    EntryExportsAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptsalesAmt.Text) && EntryExemptsalesAmt.Text.Contains(","))
                    //{
                    //    EntryExemptsalesAmt.Text = EntryExemptsalesAmt.Text.Replace(",", "");
                    //    EntryExemptsalesAmt.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckSalesMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.IsFifteenPercentChange)
                            {
                                if (viewModel.IsYesChecked)
                                {
                                    viewModel.TotalsalesAmt = viewModel.TotalAmountForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.VATNewModelFor5Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                }
                                else
                                {
                                    viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                                }
                            }
                            else
                            {
                                viewModel.TotalsalesAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdsalesAmt, viewModel.ResponseVATDeclarationD.SalesGccAmt, viewModel.ResponseVATDeclarationD.ZerosalesAmt, viewModel.ResponseVATDeclarationD.ExportsAmt, viewModel.ResponseVATDeclarationD.ExemptsalesAmt);
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAdjustment(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntrySalesGccAdj.Text) && EntrySalesGccAdj.Text.Contains(","))
                    //{
                    //    EntrySalesGccAdj.Text = EntrySalesGccAdj.Text.Replace(",", "");
                    //    EntrySalesGccAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryZerosalesAdj.Text) && EntryZerosalesAdj.Text.Contains(","))
                    //{
                    //    EntryZerosalesAdj.Text = EntryZerosalesAdj.Text.Replace(",", "");
                    //    EntryZerosalesAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExportsAdj.Text) && EntryExportsAdj.Text.Contains(","))
                    //{
                    //    EntryExportsAdj.Text = EntryExportsAdj.Text.Replace(",", "");
                    //    EntryExportsAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAdj.Text.Contains(","))
                    //{
                    //    EntryExemptsalesAdj.Text = EntryExemptsalesAdj.Text.Replace(",", "");
                    //    EntryExemptsalesAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckSalesMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.IsFifteenPercentChange)
                            {
                                if (viewModel.IsYesChecked)
                                {
                                    viewModel.TotalsalesAdj = viewModel.TotalAdjustmentForSixVar(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.VATNewModelFor5Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                }
                                else
                                {
                                    viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                                }
                            }
                            else
                            {
                                viewModel.TotalsalesAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdsalesAdj, viewModel.ResponseVATDeclarationD.SalesGccAdj, viewModel.ResponseVATDeclarationD.ZerosalesAdj, viewModel.ResponseVATDeclarationD.ExportsAdj, viewModel.ResponseVATDeclarationD.ExemptsalesAdj);
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClickGestureRecognizer_ClickedForVatAmountForPurchase(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (!viewModel.IsFifteenPercentChange)
                            {
                                viewModel.StdpurchasesVat = viewModel.StandardRatedDomesticPurchaseVatAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.StdpurchaseAdj);
                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForVatPaidatcustoms(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZVatAmountWithSAR.Text) && EntryZVatAmountWithSAR.Text.Contains(","))
                    //{
                    //    EntryZVatAmountWithSAR.Text = EntryZVatAmountWithSAR.Text.Replace(",", "");
                    //    EntryZVatAmountWithSAR.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportspaidAdj.Text) && EntryImportspaidAdj.Text.Contains(","))
                    //{
                    //    EntryImportspaidAdj.Text = EntryImportspaidAdj.Text.Replace(",", "");
                    //    EntryImportspaidAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (!viewModel.IsFifteenPercentChange)
                            {
                                if (viewModel.ResponseVATDeclarationD != null && viewModel.ResponseVATDeclarationD.TpregFg == "X")
                                {
                                    viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
                                }
                                else
                                {
                                    viewModel.ImportspaidVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForNonDesignated(viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportspaidAdj);
                                }
                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //   viewModel.ResponseVATDeclarationD.ImportspaidVat=viewModel.
        }
        private void ClickGestureRecognizer_ClickedForVatAccounted(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryImportsaccAmt.Text) && EntryImportsaccAmt.Text.Contains(","))
                    //{
                    //    EntryImportsaccAmt.Text = EntryImportsaccAmt.Text.Replace(",", "");
                    //    EntryImportsaccAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryImportsaccAdj.Text) && EntryImportsaccAdj.Text.Contains(","))
                    //{
                    //    EntryImportsaccAdj.Text = EntryImportsaccAdj.Text.Replace(",", "");
                    //    EntryImportsaccAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            viewModel.ImportsaccVat = viewModel.ImportSubjectToVatPaidAtCustomsVatAmountForDesignated(viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ImportsaccAdj);
                            viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllPurchaseAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZeropurchaseAmt.Text) && EntryZeropurchaseAmt.Text.Contains(","))
                    //{
                    //    EntryZeropurchaseAmt.Text = EntryZeropurchaseAmt.Text.Replace(",", "");
                    //    EntryZeropurchaseAmt.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptpurchaseAmt.Text) && EntryExemptpurchaseAmt.Text.Contains(","))
                    //{
                    //    EntryExemptpurchaseAmt.Text = EntryExemptpurchaseAmt.Text.Replace(",", "");
                    //    EntryExemptpurchaseAmt.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {
                            if (viewModel.IsFifteenPercentChange)
                            {
                                if (viewModel.IsYesChecked)
                                {
                                    viewModel.TotalpurchaseAmt = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor5Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor5Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.VATNewModelFor5Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                }
                                else
                                {
                                    viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.VATNewModelFor15Percent.StdpurchaseAmt, viewModel.VATNewModelFor15Percent.ImportspaidAmt, viewModel.VATNewModelFor15Percent.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                                }
                            }
                            else
                            {
                                viewModel.TotalpurchaseAmt = viewModel.TotalAmount(viewModel.ResponseVATDeclarationD.StdpurchaseAmt, viewModel.ResponseVATDeclarationD.ImportspaidAmt, viewModel.ResponseVATDeclarationD.ImportsaccAmt, viewModel.ResponseVATDeclarationD.ZeropurchaseAmt, viewModel.ResponseVATDeclarationD.ExemptpurchaseAmt);
                            }
                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ClickGestureRecognizer_ClickedForAllPurchaseAdjustment(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryZeropurchaseAdj.Text) && EntryZeropurchaseAdj.Text.Contains(","))
                    //{
                    //    EntryZeropurchaseAdj.Text = EntryZeropurchaseAdj.Text.Replace(",", "");
                    //    EntryZeropurchaseAdj.TextColor = Color.Black;
                    //}
                    //if (!string.IsNullOrEmpty(EntryExemptpurchaseAdj.Text) && EntryExemptpurchaseAdj.Text.Contains(","))
                    //{
                    //    EntryExemptpurchaseAdj.Text = EntryExemptpurchaseAdj.Text.Replace(",", "");
                    //    EntryExemptpurchaseAdj.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabic(senderObj.Text);
                    }
                    if (isArabicChecked)
                    {
                        CheckPurchaseMandetoryFields();
                        if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                        {

                            if (viewModel.IsFifteenPercentChange)
                            {
                                if (viewModel.IsYesChecked)
                                {
                                    viewModel.TotalpurchaseAdj = viewModel.TotalAmountForEightVar(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor5Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor5Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.VATNewModelFor5Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                                }
                                else
                                {
                                    viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.VATNewModelFor15Percent.StdpurchaseAdj, viewModel.VATNewModelFor15Percent.ImportspaidAdj, viewModel.VATNewModelFor15Percent.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                                }
                            }
                            else
                            {
                                viewModel.TotalpurchaseAdj = viewModel.TotalAdjustment(viewModel.ResponseVATDeclarationD.StdpurchaseAdj, viewModel.ResponseVATDeclarationD.ImportspaidAdj, viewModel.ResponseVATDeclarationD.ImportsaccAdj, viewModel.ResponseVATDeclarationD.ZeropurchaseAdj, viewModel.ResponseVATDeclarationD.ExemptpurchaseAdj);
                            }

                        }
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool isCheckArabicWithMinus(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44 || letter == 45))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }

        private void ClickGestureRecognizer_ClickedForAllPurchaseVatAmount(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                //if (!string.IsNullOrEmpty(EntryStdpurchasesVat.Text) && EntryStdpurchasesVat.Text.Contains(","))
                //{
                //    EntryStdpurchasesVat.Text = EntryStdpurchasesVat.Text.Replace(",", "");
                //    EntryStdpurchasesVat.TextColor = Color.Black;
                //}
                //if (!string.IsNullOrEmpty(EntryImportspaidVat.Text) && EntryImportspaidVat.Text.Contains(","))
                //{
                //    EntryImportspaidVat.Text = EntryImportspaidVat.Text.Replace(",", "");
                //    EntryImportspaidVat.TextColor = Color.Black;
                //}
                //if (!string.IsNullOrEmpty(EntryImportsaccVat.Text) && EntryImportsaccVat.Text.Contains(","))
                //{
                //    EntryImportsaccVat.Text = EntryImportsaccVat.Text.Replace(",", "");
                //    EntryImportsaccVat.TextColor = Color.Black;
                //}
                if (!string.IsNullOrEmpty(senderObj.Text))
                {
                    isArabicChecked = isCheckArabicWithMinus(senderObj.Text);
                }

                if (isArabicChecked)
                {
                    CheckPurchaseMandetoryFields();
                    //if (ElevenDotTwoDecimalPlacesAndNoNegativeValue.iSValiedNumber)
                    //{
                    if (viewModel.IsFifteenPercentChange)
                    {
                        if (viewModel.IsYesChecked)
                        {
                            viewModel.TotalpurchaseVat = viewModel.TotalAmountForSixVarForNegative(viewModel.StdpurchasesVat15, viewModel.StdpurchasesVat5, viewModel.ImportspaidVat15, viewModel.ImportspaidVat5, viewModel.ImportsaccVat15, viewModel.ImportsaccVat5);
                        }
                        else
                        {
                            viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat15, viewModel.ImportspaidVat15, viewModel.ImportsaccVat15);
                        }
                    }
                    else
                    {
                        viewModel.TotalpurchaseVat = viewModel.TotalVatAmount(viewModel.StdpurchasesVat, viewModel.ImportspaidVat, viewModel.ImportsaccVat);
                    }
                    //}
                }
                else
                {
                    if (senderObj != null && senderObj.Text.Length > 0)
                        senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryExemptsalesAdjFocused(object sender, FocusEventArgs e)
        {
            try
            {
                viewModel.IsUnFocusedTextBox = false;
                if (EntryExemptsalesAdj.Text == "0.00")
                {
                    EntryExemptsalesAdj.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(EntryExemptsalesAdj.Text) && EntryExemptsalesAdj.Text.Contains(","))
                {
                    EntryExemptsalesAdj.Text = EntryExemptsalesAdj.Text.Replace(",", "");
                    EntryExemptsalesAdj.TextColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EntryPreperiodcorr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                bool isArabicChecked = true;
                var senderObj = (Xamarin.Forms.Entry)sender;
                if (viewModel.IsUnFocusedTextBox == false)
                {
                    //if (!string.IsNullOrEmpty(EntryPreperiodcorr.Text) && EntryPreperiodcorr.Text.Contains(","))
                    //{
                    //    EntryPreperiodcorr.Text = EntryPreperiodcorr.Text.Replace(",", "");
                    //   // EntryPreperiodcorr.TextColor = Color.Black;
                    //}
                    if (!string.IsNullOrEmpty(senderObj.Text))
                    {
                        isArabicChecked = isCheckArabicWithMinus(senderObj.Text);
                    }
                    else
                    {
                        viewModel.IsSwitchToggled = false;
                    }
                    if (isArabicChecked)
                    {
                        CheckTotalVATMandetoryFields();
                    }
                    else
                    {
                        if (senderObj != null && senderObj.Text.Length > 0)
                            senderObj.Text = senderObj.Text.Substring(0, senderObj.Text.Length - 1).ToString();
                    }
                }
                else
                {
                    if (viewModel.IsUnFocusedTextBox == true)
                    {
                        viewModel.IsUnFocusedTextBox = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnNewStandardRatedSalesAmount15Clicked(object sender, EventArgs e)
        {
            try
            {

                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipStandardRatedSalesAmount15;
                string rate = viewModel.VATRate002.Replace(".00", string.Empty);
                String MessageWithPercent = headerAmountInfo.Message.Replace("5%", rate + "%");
                headerAmountInfo.Message = MessageWithPercent;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }

                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardRatedSalesAdjustment15;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }


                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZVatStandardRatedSales15;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch (Exception ex)
            {

            }
        }

        private void OnNewStandardRatedSalesAmount5Clicked(object sender, EventArgs e)
        {
            try
            {

                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipStandardRatedSalesAmount5;
                string rate = viewModel.VATRate003.Replace(".00", string.Empty);
                String MessageWithPercent = headerAmountInfo.Message.Replace("5%", rate + "%");
                headerAmountInfo.Message = MessageWithPercent;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }

                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardRatedSalesAdjustment5;
                string rateAdj = viewModel.VATRate003.Replace(".00", string.Empty);
                String AdjMessageWithPercent = headerAdjustmentInfo.Message.Replace("5%", rateAdj + "%");
                headerAdjustmentInfo.Message = AdjMessageWithPercent;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }


                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZSalessubjecttoVATat5;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch (Exception ex)
            {

            }
        }

        private void OnNewPrivateHealthcareAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipPrivateHealthcareAmount;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZZZZFAQ;
                headerAmountInfo.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipPrivateHealthcareAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = true;
                headerAdjustmentInfo.LinkText = AppResources.ZZZZFAQ; 
                headerAdjustmentInfo.Link = "https://www.uqn.gov.sa/articles/1515222747471373200/";
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }


                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatPrivateHealthcare;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                //PopupNavigation.Instance.PushAsync(new ShowVatInformationForPrivatehealthCare());
                
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewExportsAmountClicked(object sender, EventArgs e)
        {

            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipExportsAmount;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipExportsAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }


                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatExports;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }

        }

        private void OnNewExemptAmountClicked(object sender, EventArgs e)
        {

            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipExemptAmount;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipExemptAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatExemptsales;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewZerorateddomesticsalesAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipZerorateddomesticsalesAmount;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipZerorateddomesticsalesAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatZerorateddomesticsales;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }

        }

        private void OnNewStandardrateddomesticpurchasesAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatStandardrateddomesticpurchases;


                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewStandardrateddomesticpurchasesAmount15Clicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount15;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment15;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZVatStandardrateddomesticpurchases15;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewStandardrateddomesticpurchasesAmount5Clicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAmount5;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipStandardrateddomesticpurchasesAdjustment5;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZPurchasessubjecttoVATat5;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsVatPaidatcustomsAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }


                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatImportsVatPaidatcustoms;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsVatPaidatcustomsAmount15Clicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount15;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment15;
                headerAmountInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZImportssubjecttoVATpaidatcustoms15;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsVatPaidatcustomsAmount5Clicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAmount5;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATpaidatcustomsAdjustment5;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZImportssubjecttoVATpaidatcustoms5;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsSubjectToVatAccountedAmountClickedNew(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();


                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAmount;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatImportsSubjectToVatAccounted;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsSubjectToVatAccountedAmount15ClickedNew(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAmount15;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAdjustment15;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZImportssubjecttoVATaccountedforthroughthereversechargemechanism15;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatImportsSubjectToVatAccountedAmount5ClickedNew(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAmount5;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipImportssubjecttoVATaccountedAdjustment5;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }
                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZImportssubjecttoVATaccountedforthroughthereversechargemechanism5;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatZeroRatedPurchasesAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipZeroratedpurchasesAmount;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipZeroratedpurchasesAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatZeroRatedPurchases;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }

            catch (Exception ex)
            {

            }
        }

        private void OnNewVatExemptPurchasesAmountClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipExemptpurchasesAmount;
                headerAmountInfo.HeaderText = AppResources.ZZZZAmountWithSAR;
                headerAmountInfo.IsLinkAvailable = true;
                headerAmountInfo.LinkText = AppResources.ZVatClickFaqInstructions;
                if (App.IsArabic)
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                else
                {
                    headerAmountInfo.Link = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/FAQArchiveEservices.aspx";
                }
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerAdjustmentInfo.Message = AppResources.ZToolTipExemptpurchasesAdjustment;
                headerAdjustmentInfo.HeaderText = AppResources.ZZZZAdjustmentWithSAR;
                headerAdjustmentInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAdjustmentInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAdjustmentInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);
                headerWithInfos.Add(headerAdjustmentInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatExemptPurchases;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatcreditcarriedforwardClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipcreditcarriedforwardfrompreviousperiod;
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                headerAmountInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZVatcreditcarriedforward;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatcreditcarriedforwardFromPreviousPeriodClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipCorrectionsfrompreviousperiod;
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                String MessageWithPositiveValue = headerAmountInfo.Message.Replace("<5,000>", viewModel.CorrectionPeriodAmount);
                String MessageWithNegativeValue = headerAmountInfo.Message.Replace("<-5,000>", MessageWithPositiveValue);
                headerAmountInfo.Message = MessageWithNegativeValue;
                headerAmountInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = viewModel.CarriedValueString;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private void OnNewVatNetdueClicked(object sender, EventArgs e)
        {
            try
            {
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                HeaderWithInfo headerAdjustmentInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.Message = AppResources.ZToolTipNetVATdue;
                headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                headerAmountInfo.IsLinkAvailable = false;
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }

                headerWithInfos.Add(headerAmountInfo);

                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.ZZZZNetVAT;

                PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
            catch(Exception ex)
            {

            }
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            try
            {
               
                    if (viewModel.ContinueText == AppResources.ZZZZContinue)
                    {
                        if (viewModel.IsFifteenPercentChange == true)
                        {
                            switch (viewModel.currentTab)
                            {
                                case VATReturnUpdatedUITabEnum.Instrunction:
                                    Instrunctionsclicked();
                                    break;

                                //case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                                //    TaxpayerDetailsclicked();
                                //    break;
                                case VATReturnUpdatedUITabEnum.VATReturns:
                                    VatReturnclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.Sales:
                                    VatSalesclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.Purchase:
                                    VatPurchaseclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.TotalVat:
                                    VatTotalAmountclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.Summery:
                                    Summaryclicked();
                                    //                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                                    break;
                            }
                        }
                        else
                        {
                            switch (viewModel.currentTab)
                            {
                                case VATReturnUpdatedUITabEnum.Instrunction:
                                    Instrunctionsclicked();
                                    break;

                                //case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                                //    TaxpayerDetailsclicked();
                                //    break;

                                case VATReturnUpdatedUITabEnum.Sales:
                                    VatSalesclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.Purchase:
                                    VatPurchaseclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.TotalVat:
                                    VatTotalAmountclicked();
                                    break;

                                case VATReturnUpdatedUITabEnum.Summery:
                                    Summaryclicked();
                                    //                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                                    break;
                            }
                        }
                    }
                    else if (viewModel.ContinueText == AppResources.ZZZZConfirmAndCarryForward)
                    {
                   
                        if (viewModel.IsDeclarationCheckedForSummary)
                        {
                            if (viewModel.IsDeclarationCheckedForSummary && (viewModel.IsVoidClicked == false && viewModel.IsResetClicked == false))
                            {
                                if (((App.ICRStatus == "E0045" || App.ICRStatus == "E0006") && (viewModel.IsAmendClicked == true)) || (App.ICRStatus == "E0001" || viewModel.IsCheckedDraftMode()))
                                {
                                if (viewModel.IsCarriedForwandReviewMessage == false)
                                {
                                    viewModel.IsCarriedForwandReviewMessage = true;
                                    decimal FourteenA = 0;
                                    if (!string.IsNullOrEmpty(viewModel.TotaldueVat) && !string.IsNullOrEmpty(viewModel.Preperiodcorr))
                                    {
                                        FourteenA = Convert.ToDecimal(viewModel.TotaldueVat) + Convert.ToDecimal(viewModel.Preperiodcorr);
                                    }
                                    if ((viewModel.IsSwichButtonEnable == false && FourteenA < 5000 && Convert.ToDecimal(viewModel.NetdueVat) < 0) || (viewModel.IsSwichButtonEnable == true && FourteenA < 100000 && Convert.ToDecimal(viewModel.CreditVat) > 0))
                                    {


                                        List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                                        HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                                        NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                                        headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                                        headerAmountInfo.IsLinkAvailable = false;

                                        StringBuilder Masseges = new StringBuilder();
                                        Masseges.Append(AppResources.Pleasereviewthecalculationandsubmitagain);
                                        Masseges.Append(Environment.NewLine);
                                        Masseges.Append(Environment.NewLine);
                                        Masseges.Append(Environment.NewLine);
                                        Masseges.Append(AppResources.CreditReturnMsg);
                                        //  PopUp Pop = new PopUp();
                                        headerAmountInfo.IsLinkAvailable = false;
                                        headerAmountInfo.IsRed = "#ff0000";
                                        headerAmountInfo.IsBold = "Bold";
                                        headerAmountInfo.Message = Masseges.ToString();

                                        headerWithInfos.Add(headerAmountInfo);


                                        newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                                        newDesignPopUp.HeaderWithInfos = headerWithInfos;
                                        newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                                        PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));

                                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(Pop));
                                        //SelectedIndex = 2;
                                        //PageSelectedItem = VatTabbledPageList[2];
                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            viewModel.IsNewLoading = true;
                                        });
                                        await viewModel.SubmitClicked();
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            viewModel.IsNewLoading = false;
                                        });
                                    }

                                }
                                else
                                {
                                        Device.BeginInvokeOnMainThread(() =>
                                            {
                                                viewModel.IsNewLoading = true;
                                            });
                                        await viewModel.SubmitClicked();
                                        Device.BeginInvokeOnMainThread(() =>
                                        {
                                            viewModel.IsNewLoading = false;
                                        });
                                   }
                                }
                            }
                        }
                        else
                        {
                            List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                            HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                            NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                            headerAmountInfo.HeaderText = AppResources.ZZZInformationNew;
                            headerAmountInfo.IsLinkAvailable = false;
                            headerAmountInfo.Message = AppResources.ZZZZPleaseAgreeTandCMsg;

                            headerWithInfos.Add(headerAmountInfo);


                            newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                            newDesignPopUp.HeaderWithInfos = headerWithInfos;
                            newDesignPopUp.MainHeader = AppResources.ZZZInformationNew;

                            PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
                        }
                   

                    //if (viewModel.IsDeclarationCheckedForSummary && (App.ICRStatus == "E0001" || App.ICRStatus == "E0013"))
                    //    {
                    //        await viewModel.SubmitClicked();
                    //    }
                  }
                
            }
            catch(Exception ex)
            {

            }
        }

        public void setSumbmitbuttonVisibility()
        {
            if(viewModel.currentTab == VATReturnUpdatedUITabEnum.Summery)
            {
                viewModel.isBtnVisible = true;
            }
            else
            {
                viewModel.isBtnVisible = false;
            }
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            setSumbmitbuttonVisibility();
            if (viewModel.IsFifteenPercentChange == true)
            {
                switch (viewModel.currentTab)
                {
                    //case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                    //    viewModel.currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                    //    ManageButtonsName();
                    //    break;
                    case VATReturnUpdatedUITabEnum.VATReturns:
                        ComeToInstrunctionsclicked();
                        viewModel.IsCheckedTaxPayerDetailsInfo = false;
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Sales:
                        ComeToVatReturnclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        ComeToVatSalesclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat:
                        ComeToVatPurchaseclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Summery:
                        ComeToVatTotalAmountclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;
                }
            }
            else
            {
                switch (viewModel.currentTab)
                {
                    //case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                    //    viewModel.currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                    //    viewModel.IsCheckedTaxPayerDetailsInfo = false;
                    //    ManageButtonsName();
                    //    break;
                    case VATReturnUpdatedUITabEnum.VATReturns:
                        ComeToInstrunctionsclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Sales:
                        ComeToInstrunctionsclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        ComeToVatSalesclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat:
                        ComeToVatPurchaseclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;

                    case VATReturnUpdatedUITabEnum.Summery:
                        ComeToVatTotalAmountclicked();
                        viewModel.ManageButtonsNameOnViewModel();
                        break;
                }
            }
                
        }

        private void OnEditPurcahseClicked(object sender, EventArgs e)
        {
            viewModel.currentTab = VATReturnUpdatedUITabEnum.Purchase;
            viewModel.ManageButtonsNameOnViewModel();
        }

        private void OnEditSalesClicked(object sender, EventArgs e)
        {
            viewModel.currentTab = VATReturnUpdatedUITabEnum.Sales;
            viewModel.ManageButtonsNameOnViewModel();
        }

        private async void OnAmendButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await viewModel.VATReturnAmendAsync();
            }
            catch(Exception ex)
            {

            }
        }
    }
}