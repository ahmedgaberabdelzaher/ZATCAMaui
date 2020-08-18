using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    public partial class VATDeregistrationDetailsPage : ContentPage
    {
        VATDeRegistrationDetailsPageViewModel viewModel;

        public VATDeregistrationDetailsPage()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeregistrationDetailsPage;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            this.FlowDirection = FlowDirection.LeftToRight;

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                viewModel.PickerModel = arg;
                Console.WriteLine(arg);
                OnAppearing();
            });
         

            MessagingCenter.Subscribe<PickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) => {
                viewModel.DatePickerModel = arg;
                Console.WriteLine(arg);
                OnAppearing();
            });

            viewModel.FromDate = "DD/MM/YYYY";
            viewModel.ToDate = "DD/MM/YYYY";
            viewModel.SuspendedStartDate = "DD/MM/YYYY";
            viewModel.SuspendedEndDate = "DD/MM/YYYY";
            viewModel.NextFilingStartDate = "DD/MM/YYYY";
            viewModel.NextFilingEndDate = "DD/MM/YYYY";
            viewModel.NextFilingDueDate = "DD/MM/YYYY";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
               
                if (App.IsArabic)
                {
                    if (arg.PickerTitle.Contains("IDType"))
                    {
                        viewModel.IDType = arg.SelectedValue;


                    }
                    else
                    {
                        viewModel.ReasonTitle = arg.SelectedValue;
                    }
                }
                else
                {
                    if (arg.PickerTitle.Contains("IDType"))
                    {
                        viewModel.IDType = arg.SelectedValue;
                    }
                    else
                    {
                        viewModel.ReasonTitle = arg.SelectedValue;
                    }
                }


            });
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
            {
                
                if (App.IsArabic)
                {
                    if (arg.PickerTitle.Contains("StartDateType"))
                    {
                        viewModel.FromDate = arg.SelectedValue;
                    }
                    else if (arg.PickerTitle.Contains("EndDateType"))
                    {
                        viewModel.ToDate = arg.SelectedValue;

                    }
                    else
                    {
                        viewModel.DOB = arg.SelectedValue;
                    }
                }
                else
                {
                    if (arg.PickerTitle.Contains("StartDateType"))
                    {
                        viewModel.FromDate = arg.SelectedValue;
                    }
                    else if(arg.PickerTitle.Contains("EndDateType")) 
                    {
                        viewModel.ToDate = arg.SelectedValue;

                    }else
                    {
                        viewModel.DOB = arg.SelectedValue;
                    }
                }

                if (!viewModel.FromDate.Contains( "DD/MM/YYYY") && !viewModel.ToDate.Contains
                    ("DD/MM/YYYY"))
                {
                    DateTime startDateTime = Convert.ToDateTime(viewModel.FromDate);
                    DateTime toDateTime = Convert.ToDateTime(viewModel.ToDate);

                    VATDeregistrationSuspendedDateRootObject obj = WebServiceManager.GAZTGETVATDeregReturnFilingDateList(startDateTime, toDateTime);
                    if (obj != null)
                    {
                        if (obj.d.dateResults[0].SuspDtfrom != null)
                        {
                            DateTime date = Convert.ToDateTime(obj.d.dateResults[0].SuspDtfrom.ToString());
                            viewModel.SuspendedStartDate = date.ToString("dd-MM-yyyy", new CultureInfo("en-US"));


                        }
                        if(obj.d.dateResults[0].SuspDtto != null)
                        {
                            DateTime date = (DateTime)obj.d.dateResults[0].SuspDtto;

                            viewModel.SuspendedEndDate = date.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

                        }
                        if (obj.d.dateResults[0].NextDtfrom != null)
                        {
                            DateTime date = Convert.ToDateTime(obj.d.dateResults[0].NextDtfrom.ToString());

                            viewModel.NextFilingStartDate = date.ToString("dd-MM-yyyy", new CultureInfo("en-US"));

                        }
                        if (obj.d.dateResults[0].NextDtfrom != null)
                        {
                            DateTime date = Convert.ToDateTime(obj.d.dateResults[0].NextDtto.ToString());

                            viewModel.NextFilingEndDate = date.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                        }
                        if (obj.d.dateResults[0].Duedate!= null)
                        {
                            DateTime date = Convert.ToDateTime(obj.d.dateResults[0].Duedate.ToString());

                            viewModel.NextFilingDueDate = date.ToString("dd-MM-yyyy", new CultureInfo("en-US"));
                        }
                    }
                }
            });
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            viewModel.AddOutletDocumentOptions();

        }

        void OnDownloadAttachmentClicked()
        {

        }

        void OnDeleteAttachmentClicked()
        {

        }

        void attachmentsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            // VATDeregistrationAttachmentsModel selectedItem = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            // viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(selectedItem);
            viewModel.SelectedAttachment = e.AddedItems[0] as VATDeregistrationAttachmentsModel;
            viewModel.SelectedOutletOptionIndex = viewModel.AttachmentsListViewData.IndexOf(viewModel.SelectedAttachment);

            if (viewModel.SelectedAttachment.IsAttachmentAttached == true)
            {
                viewModel.SelectedAttachment.AttachmentName = string.Empty;
                viewModel.SelectedAttachment.IsAttachmentAttached = false;
            }
            else
            {
                attachmentsListView.SelectedItems.Clear();
                viewModel.AddAttachmentEx();
            }
        }

        void btnReasonContinue_Clicked(System.Object sender, System.EventArgs e)
        {
            viewModel.ReasonContinueBtnClicked();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Clicked event");
        }

        void outletDocumentOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDocumentOptions.IndexOf(selectedItem);
        }
        async void VATDeregStartDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.PickerTitle = "StartDateType";
            genericPickerModel.PickerId = "StartDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

           
        }
        async void VATDeregEndDateClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.PickerTitle = "EndDateType";
            genericPickerModel.PickerId = "EndDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }
        

        }
        private async void VATDeregDOBClicked(System.Object sender, System.EventArgs e)
        {

            GenericDatePickerModel genericPickerModel = new GenericDatePickerModel();
            genericPickerModel.PickerTitle = "DOBDateType";
            genericPickerModel.PickerId = "DOBDateTypePicker";
            try
            {
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                   await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

         
        }
        void VatDeregIdClicked(System.Object sender, System.EventArgs e)
        {

            ObservableCollection<string> iDTypes = new ObservableCollection<string>();
            iDTypes.Add(AppResources.NationaID);
            iDTypes.Add(AppResources.ZZIqamaID);
            iDTypes.Add(AppResources.ZZGCCID);

            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = iDTypes;
            genericPickerModel.PickerTitle = "IDType";
            genericPickerModel.PickerId = "idTypePicker";

            try
            {
                 PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {

            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                     viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                    if (viewModel.IDType == "National ID")
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                    if (viewModel.IDType == "Iqama ID")
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                                PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                    if (viewModel.IDType == "GCC ID")
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.TxtIDNumber = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }


                    }
                }
                else
                {
                    // FrmIDNumber.HasError = false;
                    viewModel.FrameIDError = false;
                }


            }
            catch (Exception ex)
            {


            }


        }
        public async void ValidateIDNumber()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.DOB.Replace("/", "");
            // EntryName.IsEnabled = true;
            if (viewModel.IDType == "National ID")
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.TxtIDNumber, dob);
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
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            //  viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            // viewModel.DOB = vATSignUpData.d.Birthdt10;
                       
                           // viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(x => x.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                          
                            //  EntryName.IsEnabled = false;
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
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
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

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.IDType == "Iqama ID")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.TxtIDNumber, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {

                          
                            //  EntryName.IsEnabled = false;
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
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
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

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private  void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
            {
                viewModel.FrameIDError = false;
            }
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            viewModel.EnableReasonView();
        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            viewModel.EnableAttachmentsView();
        }

        void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            viewModel.EnableDeclarationView();
        }
    }
}
