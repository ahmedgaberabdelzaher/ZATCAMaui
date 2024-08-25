using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Mopups.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATDeRegistration
{
 
    public partial class VATDeregistrationDetailsPage : ContentPage
    {
        VATDeRegistrationDetailsPageViewModel viewModel;


        public VATDeregistrationDetailsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationDetailsPage;
            this.BindingContext = viewModel;


            Task.Run(async () =>
            {
                viewModel.IsLoading = true;
                await GetVatDeRegistrationData();
            });

            viewModel.VoidIsVisible = false;
            ContactName.IsEnabled = true;

        }


        private void MessagingCenterCallBacks()
        {
            MessagingCenter.Subscribe<VATDeRegistrationInstructionsPageViewModel, bool>(this, "SelectedCheckboxItem", (sender, arg) =>
            {
                if (arg)
                {
                    viewModel.IsInstructionChecked = arg;

                }
                //(arg);
            });
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.IsContactPersonEnabled = false;
                if (App.IsArabic)
                {
                    if (arg.PickerTitle.Contains(AppResources.VatDeregIDType))
                    {
                        viewModel.IDType = arg.SelectedValue;

                        if (viewModel.IDType.Contains(AppResources.ZZGCCID))
                        {
                            viewModel.IsContactPersonEnabled = true;
                            viewModel.IsDOBEditorVisible = false;
                            //  IDNumberField.WidthRequest = 320;
                        }
                        else
                        {
                            viewModel.IsDOBEditorVisible = true;
                        }
                    }
                    else
                    {
                        viewModel.ReasonTitle = arg.SelectedValue;

                        if (viewModel.ReasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                        {
                            viewModel.IsOthersEditorVisible = true;
                        }
                        else
                        {
                            viewModel.IsOthersEditorVisible = false;
                        }
                    }
                }
                else
                {
                    if (arg.PickerTitle.Contains(AppResources.VatDeregIDType))
                    {
                        viewModel.IDType = arg.SelectedValue;
                        if (viewModel.IDType.Contains(AppResources.ZZGCCID))
                        {
                            viewModel.IsDOBEditorVisible = false;
                            // IDNumberField.WidthRequest = 320;
                        }
                        else
                        {
                            viewModel.IsDOBEditorVisible = true;
                        }
                    }
                    else
                    {
                        viewModel.ReasonTitle = arg.SelectedValue;
                        if (viewModel.ReasonTitle.Contains(AppResources.VatDeregistrationofReturnReason4))
                        {
                            viewModel.IsOthersEditorVisible = true;
                        }
                        else
                        {
                            viewModel.IsOthersEditorVisible = false;
                        }
                    }
                }
            });

            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", async (sender, arg) =>
            {

                if (App.IsArabic)
                {
                    if (arg.PickerId == "StartDateTypePicker")
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.PickerId == "EndDateTypePicker")
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else
                    {
                        viewModel.DOB = arg.SelectedValue;
                        try
                        {
                            if (viewModel.TxtIDNumber != string.Empty && viewModel.DOB != string.Empty)
                            {
                                ValidateIDNumberContact();
                            }
                        }
                        catch (Exception)
                        {



                        }
                    }
                }
                else
                {

                    if (arg.DatePickerTitle.Contains(AppResources.VatDeregStartDatePickerTitle))
                    {
                        viewModel.FromDate = Convert.ToDateTime(arg.SelectedValue);
                    }
                    else if (arg.DatePickerTitle.Contains(AppResources.VatDeregEndDatePickerTitle))
                    {
                        viewModel.ToDate = Convert.ToDateTime(arg.SelectedValue);

                    }
                    else
                    {
                        viewModel.DOB = arg.SelectedValue;
                        try
                        {
                            if (viewModel.TxtIDNumber != string.Empty && viewModel.DOB != string.Empty)
                            {
                                ValidateIDNumberContact();
                            }
                        }
                        catch (Exception)
                        {



                        }
                    }
                }

                if (arg.DatePickerTitle.Contains(AppResources.VatDeregStartDatePickerTitle) || arg.DatePickerTitle.Contains(AppResources.VatDeregEndDatePickerTitle))
                {
                    bool isValidated = await viewModel.suspendedDateValidation();
                }

            });
        }
        public async Task GetVatDeRegistrationData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.onPageLoad();
                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception)
            {



            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsSummaryViewEnabled = false;
            MessagingCenterCallBacks();

            if (viewModel.VATDeRegistrationDetailsData != null)
            {
                if (viewModel.VATDeRegistrationDetailsData.d != null)
                {
                    if (viewModel.VATDeRegistrationDetailsData.data.Fbnumx == string.Empty)
                    {
                        viewModel.VoidIsVisible = false;
                    }
                    else
                    {
                        viewModel.VoidIsVisible = true;

                    }
                }
            }
            MessagingCenter.Subscribe<object, AttachmentsList>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {

                    //viewModel.VatInstalments.d.AttachmentSet.results = arg.results;
                    viewModel.PopulateAttachments(arg.results);
                    viewModel?.updateattachmentList();

                }
            });
            if (viewModel.IDType != null)
            {
                //MessagingCenterCallBacks();
            }
            //viewModel.PopulateSummaryReasonData();
            viewModel.PopulateSummaryDeclarationData();
            // viewModel.PopulateAttachmentsListViewTemplate();

            // summaryAttachmentsListView.ItemsSource = viewModel.AttachmentsListViewData;  
        }

        public async void ValidateIDNumberContact()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.DOB.Replace("/", "-");

            if (viewModel.IDType == AppResources.NationaID)
            {
                ContactName.IsEnabled = false;

                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.TxtIDNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;

                                //viewModel.FrameContactIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //viewModel.FrameContactIDError = false;
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            //viewModel.ContactPersonName = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                            viewModel.ContactPersonName = vATSignUpData.d.taxpayerFullName; ;
                            ContactName.IsEnabled = false;

                            viewModel.FrameIDError = false;
                            //  viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                // viewModel.FrameContactIDError = true;
                                viewModel.TxtIDNumber = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Write(ex.StackTrace.ToString());

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.IDType == AppResources.ZZIqamaID)
            {
                ContactName.IsEnabled = false;

                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.TxtIDNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                //viewModel.FrameContactIDError = true;
                                viewModel.TxtIDNumber = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                // viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                //   viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.ContactPersonName = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                            ContactName.IsEnabled = false;
                            viewModel.FrameIDError = false;
                            // viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                // viewModel.FrameContactIDError = true;
                                viewModel.TxtIDNumber = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                // viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                //  await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {
                          

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            else
            {
                ContactName.IsEnabled = true;
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        public static double quarterDiff(DateTime first, DateTime second)
        {

            int firstQuarter = getQuarter(first);
            int secondQuarter = getQuarter(second);
            return 1 + Math.Abs(firstQuarter - secondQuarter);
        }

        private static int getQuarter(DateTime date)
        {
            return date.Year * 4 + (date.Month - 1) / 3;
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }



        void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            try
            {
                VATDeregistrationModel RemovedItem = null;
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                    RemovedItem = e.RemovedItems[0] as VATDeregistrationModel;
                VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
                viewModel.ReasonTitle = string.Empty;
                viewModel.OtherField = string.Empty;
                selectedItem.TextCol = Colors.White;
                selectedItem.ImgSource = "vat_tile_listofsignup";
                if (RemovedItem != null)
                {
                    RemovedItem.TextCol = (Color)Application.Current.Resources["Primary"];
                    RemovedItem.ImgSource = "vat_tile_listofsignup_W";
                }
                viewModel.AddOutletDocumentOptions();
            }
            catch (Exception)
            {


            }
        }

        private async void OnAttachmentClicked(object sender, EventArgs e)
        {
            await viewModel.AddAttachmentEx();
        }


        public void onPageLoad(VATDeRegistrationDetails vATDeRegistrationDetails)
        {
            if (vATDeRegistrationDetails != null && vATDeRegistrationDetails.d != null)
            {
                viewModel.VATDeRegistrationDetailsForAttach = vATDeRegistrationDetails;
                SetDocType();
                if (viewModel.VATDeRegistrationDetailsForAttach.d.AttdetSet != null && viewModel.VATDeRegistrationDetailsForAttach.d.AttdetSet != null)
                {
                    if (viewModel.VATDeRegistrationDetailsForAttach.d.AttdetSet.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeRegistrationDetailsForAttach.d.AttdetSet as List<Attachment>);
                        viewModel.VatAttachmentsList = myCollection;

                        try
                        {
                            foreach (var item in viewModel.VatAttachmentsList)
                            {
                                if (item.Erfdt != null && item.Erftm != null)
                                {
                                    viewModel.FileName = item.Filename;
                                    item.Erfdt = JsonConvert.DeserializeObject<DateTime>(@"""" + item.Erfdt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                    item.Erfdt = Convert.ToDateTime(item.Erfdt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                }
                            }
                        }
                        catch (Exception)
                        {


                        }

                        viewModel.filterList();
                        viewModel.CloneAttachmentList(viewModel.VatAttachmentsList);
                    }
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.IsLoading = false;
            outletDecisionOptionsListView.SelectedItem = null;

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");

            MessagingCenter.Unsubscribe<VATDeRegistrationInstructionsPageViewModel, bool>(this, "SelectedCheckboxItem");

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<VATDeRegistrationInstructionsPageViewModel, bool>(this, "SelectedCheckboxItem");

        }
        public void SetDocType()
        {
            viewModel.setDocType();
        }

        public async Task DeleteAttachment(bool result, Attachment attachment)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(() =>
                {
                    if (result)
                    {
                        // int indexToReduceTheSize = viewModel.GetDeletedAttachmentIndex(attachment);
                        string results = VATDeregistrationWebServiceManager.GAZTDeleteVATDeRegistrationAttachment(attachment.Filename, attachment.Doguid, viewModel.DocTypeString);
                        if (results == "X")
                        {


                            Attachment listitem = (from itm in viewModel.VatAttachmentsList
                                                   where itm.Doguid == attachment.Doguid.ToString()
                                                   select itm)
                                              .FirstOrDefault<Attachment>();

                            if (listitem != null)
                                viewModel.VatAttachmentsList.Remove(listitem);


                            viewModel.VATDeRegistrationDetailsForAttach.d.AttdetSet.Remove(listitem);


                            //if (indexToReduceTheSize != -1)
                            // viewModel.ReduceTotalAttachmentSize(indexToReduceTheSize);
                            viewModel.AttachmentCount--;
                            viewModel.filterList();
                            viewModel.CloneAttachmentList(viewModel.VatAttachmentsListtofilter);
                        }
                        viewModel.filterList();
                        viewModel.CloneAttachmentList(viewModel.VatAttachmentsListtofilter);
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


                viewModel.IsLoading = false;
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        void attachmentsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationAttachmentsModel selectedItem = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(selectedItem);
            viewModel.SelectedAttachment = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);

            if (viewModel.SelectedAttachment.IsAttachmentAttached == true)
            {
                viewModel.SelectedAttachment.AttachmentName = string.Empty;
                viewModel.SelectedAttachment.IsAttachmentAttached = false;
            }
        }

        void btnReasonContinue_Clicked(object sender, EventArgs e)
        {
            viewModel.ReasonContinueBtnClicked();
        }

        void Button_Clicked(object sender, EventArgs e)
        {

        }

        void outletDocumentOptionsListView_SelectionChanged(object sender,ItemSelectionChangedEventArgs e)
        {
            try
            {
                ResultsAttachmentItemForElgblDocSet RemovedItem = null;
                if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                    RemovedItem = e.RemovedItems[0] as ResultsAttachmentItemForElgblDocSet;
                ResultsAttachmentItemForElgblDocSet selectedItem = e.AddedItems[0] as ResultsAttachmentItemForElgblDocSet;
                viewModel.SelectedOutletOptionIndex = viewModel.AttachmentTypes.IndexOf(selectedItem);
                viewModel.SelectedDocumentOption = selectedItem;
                viewModel?.updateattachmentList();
                selectedItem.TextCol = Colors.White;
                selectedItem.ImgSource = "vat_tile_listofsignup";
                if (RemovedItem != null)
                {
                    RemovedItem.TextCol = (Color)Application.Current.Resources["Primary"];
                    RemovedItem.ImgSource = "vat_tile_listofsignup_W";
                }
            }
            catch (Exception)
            {


            }
        }
        async void VATDeregStartDateClicked(object sender, EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregStartDatePickerTitle;
            genericDatePickerModel.PickerId = "StartDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }


        }
        async void VATDeregEndDateClicked(object sender, EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregEndDatePickerTitle;
            genericDatePickerModel.PickerId = "EndDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    //await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }


        }
        private async void VATDeregDOBClicked(object sender, EventArgs e)
        {

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "DOBDateTypePicker";
            try
            {
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }


        }
        void VatDeregIdClicked(object sender, EventArgs e)
        {

            List<string> iDTypes = new List<string>();
            iDTypes.Add(AppResources.NationaID);
            iDTypes.Add(AppResources.ZZIqamaID);
            iDTypes.Add(AppResources.ZZGCCID);

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = AppResources.VatDeregIDType;
            genericPickerModel.PickerId = "idTypePicker";

            try
            {
                MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException)
            {

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                    // viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

        }

        private void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    if (viewModel.IDType == AppResources.NationaID)
                    {
                        if (viewModel.TxtIDNumber.Substring(0, 1) != "1")
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
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.TxtIDNumber = string.Empty;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (viewModel.TxtIDNumber.Length != 10)
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
                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel.TxtIDNumber = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                    ValidateIDNumber();
                                }


                            }
                        }


                    }
                    if (viewModel.IDType == AppResources.ZZIqamaID)
                    {
                        if (viewModel.TxtIDNumber.Substring(0, 1) != "2")
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
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.TxtIDNumber = string.Empty;
                        }
                        else
                        {
                            if (viewModel.TxtIDNumber.Length != 10)
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
                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel.TxtIDNumber = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                    ValidateIDNumber();
                                }
                            }
                        }


                    }
                    if (viewModel.IDType == AppResources.ZZGCCID)
                    {

                        if (viewModel.TxtIDNumber.Substring(0, 1) == "0")
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
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.TxtIDNumber = string.Empty;
                        }
                        else if (!(viewModel.TxtIDNumber.Length <= 15 && viewModel.TxtIDNumber.Length >= 7))
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
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.TxtIDNumber = string.Empty;
                            EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }


                    }
                }
                else
                {
                    // FrmIDNumber.HasError = false;
                    viewModel.FrameIDError = true;
                    viewModel.IsDeclarationContinueButtonEnabled = false;
                }


            }
            catch (Exception)
            {




            }


        }
        public async void ValidateIDNumber()
        {
            viewModel.IsLoading = true;

            string dob = viewModel.DOB.Replace("/", "-");
            ContactName.IsEnabled = true;
            if (viewModel.IDType == AppResources.NationaID)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.TxtIDNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.ContactPersonName = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                            //  viewModel.DOB = vATSignUpData.d.Birthdt10;

                            // viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(x => x.ID == vATSignUpData.d.Idtype).FirstOrDefault();

                            ContactName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Write(ex.StackTrace.ToString());

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.IDType == AppResources.ZZIqamaID)
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.TxtIDNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {


                            ContactName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));

                                // viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                // await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
            {
                viewModel.FrameIDError = false;
                viewModel.IsDeclarationContinueButtonEnabled = true;
            }
            else
            {
                viewModel.FrameIDError = true;
                viewModel.IsDeclarationContinueButtonEnabled = false;
            }
        }


        void Button_Clicked_1(object sender, EventArgs e)
        {
            viewModel.EnableReasonView();
        }

        void Button_Clicked_2(object sender, EventArgs e)
        {
            viewModel.EnableAttachmentsView();
        }

        void Button_Clicked_3(object sender, EventArgs e)
        {
            viewModel.EnableDeclarationView();
        }


        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = true;
                    });
                    Image arrowImage = sender as Image;
                    Attachment attachment = (Attachment)arrowImage.BindingContext;
                    //if (!attachment.DeleteImageSource.Equals("ic_Delete_disabled.png"))
                    //{

                    if (attachment != null)
                    {//ZZNotification
                        var result = await this.DisplayAlert(AppResources.ZZNotification, AppResources.ZZDeleteAttachmentConfirmationText + " " + attachment.Filename + "?", AppResources.ZZZOkayText, AppResources.ZZCancel);

                        await DeleteAttachment(result, attachment);
                    }

                    //}
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
                catch (Exception)
                {


                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });

                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsLoading = false;
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        async void voidTapped(object sender, EventArgs e)
        {
            //await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VatDeregistrationVoidMessage));

            var result = await DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VatDeregistrationVoidMessage, AppResources.ZNo, AppResources.ZYes);
            if (!result)
            {

                if (viewModel.VATDeRegistrationDetailsData != null)
                {
                    if (viewModel.VATDeRegistrationDetailsData.d != null)
                    {
                        if (viewModel.VATDeRegistrationDetailsData.data.Fbnumx != string.Empty)
                        {
                            viewModel.setDATA("04");
                            await viewModel.saveAsDraftVoidAPIMethodCall();

                            viewModel.clearData();

                            // _navigationService.GoBack();
                        }
                        else
                        {
                            viewModel._navigationService.GoBack();

                        }
                    }

                }
            }
        }


        void Others_Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //viewModel.ReasonTitle = e.NewTextValue;
            //OthersTxt.HelperText
            try
            {
                if (e.NewTextValue != null)
                {
                    viewModel.SetTextCount(e.NewTextValue.Length);

                }
            }
            catch (Exception)
            {


            }
        }
    }
}
