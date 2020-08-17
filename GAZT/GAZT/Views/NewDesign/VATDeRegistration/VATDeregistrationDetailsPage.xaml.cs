using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.GenericPickers;
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


            });
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            VATDeregistrationModel selectedItem = e.AddedItems[0] as VATDeregistrationModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);

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
                     viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    viewModel._navigationService.GoBack();
                });
            }

            if (viewModel.StartDate != null && viewModel.EndDate != null)
            {
                VATDeregistrationSuspendedDateRootObject obj = WebServiceManager.GAZTGETVATDeregReturnFilingDateList(viewModel.StartDate, viewModel.EndDate);
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
                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView());
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
        private async void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                try
                {
                    if (viewModel.DOB != null)
                    {
                        string month = string.Empty, day = string.Empty, year = string.Empty;
 
                        // int a = DateTime.Compare(viewModel.TodayDate, selecteDate);
                       
                        string _month = DateTime.Now.Month.ToString();
                        string _day = DateTime.Now.Day.ToString();
                        string _year = DateTime.Now.Year.ToString();
                        string DBO = year + month + day;
                        //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
                        if (!string.IsNullOrEmpty(EntryIDNumber.Text))
                        {
                            if (viewModel.IDType.Contains("National ID"))
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "1")
                                {
                                    // FrmIDNumber.HasError = true;
                                     //EntryName.Text = string.Empty;
                                }
                                else
                                {
                                    //FrmIDNumber.HasError = false;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = string.Empty;
                                            if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(DateTime.Now.Date.Month) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                            {
                                                Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                                IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                                                if (SignupIsIDTypeValid.d == null)
                                                {
                                                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                    {
                                                        //FrmIDNumber.HasError = true;
                                                        //EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                        //  viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        // FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    //viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                    //FrmIDNumber.HasError = false;
                                                    //EntryName.IsEnabled = false;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = string.Empty;
                                                if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(DateTime.Now.Date.Month) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                                {
                                                    Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                    {
                                                        // FrmIDNumber.HasError = true;
                                                        //EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                        // viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        // FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
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
                                                    //viewModel.IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    viewModel._navigationService.GoBack();
                                                });
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                            }
                            else if (viewModel.IDType.Contains("Iqama ID"))
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "2")
                                {
                                    // FrmIDNumber.HasError = true;
                                    //EntryName.Text = string.Empty;
                                }
                                else
                                {
                                    // FrmIDNumber.HasError = false;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = string.Empty; ;
                                            if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(DateTime.Now.Date.Month) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                            {
                                                Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                                IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                                                if (SignupIsIDTypeValid.d == null)
                                                {
                                                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                    {
                                                        // FrmIDNumber.HasError = true;
                                                        // EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                        //  viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        // FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    //viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                    //EntryName.IsEnabled = false;
                                                    //FrmIDNumber.HasError = false;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = string.Empty;
                                                if (!(Convert.ToUInt16(_month) == Convert.ToUInt16(DateTime.Now.Date.Month) && Convert.ToUInt16(_day) == Convert.ToUInt16(day) && Convert.ToUInt16(year) == Convert.ToUInt16(_year)))
                                                {
                                                    Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                    {
                                                        //FrmIDNumber.HasError = true;
                                                        //EntryName.Text = string.Empty;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                        // viewModel.TxtIDNumber = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        //FrmIDNumber.HasError = false;
                                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    }
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
                                                    // viewModel.IsLoading = false;

                                                    await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                                    viewModel._navigationService.GoBack();
                                                });
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                            }
                            else
                            {
                                // FrmIDNumber.HasError = false;
                            }
                        }
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
                        // viewModel.IsLoading = false;

                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
}
