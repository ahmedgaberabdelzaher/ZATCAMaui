using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.VATInstalmentModels;
using EGAZT.Models.ZakatInstalationModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using PanCardView.Extensions;
using Xamarin.Forms;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlan;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.VATInstalmentScheduleDetailsModel;
using Application = Xamarin.Forms.Application;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class VATInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        #endregion

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }


        public VATInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                if (IsVATLandingPageVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (IsVATInstalmentPlanVisible)
                {
                    EnableVATLandingPage();
                }
                else if (IsInstalmentSchedulePlanVisible)
                {
                    EnableVAtInstalmentPlan();
                }
                else if (IsDisplayListVisible)
                {
                    EnableVATLandingPage();
                }
                else if (IsDisplayDetailsVisible)
                {
                    EnableDisplayInstalment();
                }

            });

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
            SummaryContinueBtnTapped = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            AddOutletDecisionOptions();



            RequestInstalmentButtonTapped = new Command(this.RequestInstalmentButtonClicked);



        }

        #region ICommand declarations

        public ICommand RequestInstalmentButtonTapped { get; set; }

        #endregion




        #region Visibility


        private bool _isDisplayListVisible = false;
        public bool IsDisplayListVisible
        {
            get
            {
                return _isDisplayListVisible;
            }
            set
            {
                _isDisplayListVisible = value;
                RaisePropertyChanged("IsDisplayListVisible");
            }
        }

        private bool _isDisplayDetailsVisible = false;
        public bool IsDisplayDetailsVisible
        {
            get
            {
                return _isDisplayDetailsVisible;
            }
            set
            {
                _isDisplayDetailsVisible = value;
                RaisePropertyChanged("IsDisplayDetailsVisible");
            }
        }

        private string _numberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;
        public string NumberOfInstalmentPlans
        {
            get
            {
                return _numberOfInstalmentPlans;
            }
            set
            {
                _numberOfInstalmentPlans = value;
                RaisePropertyChanged("NumberOfInstalmentPlans");
            }
        }

        private bool _isVATLandingPageVisible = true;
        public bool IsVATLandingPageVisible
        {
            get
            {
                return _isVATLandingPageVisible;
            }
            set
            {
                _isVATLandingPageVisible = value;
                RaisePropertyChanged("IsVATLandingPageVisible");
            }
        }

        private bool _isVATInstalmentPlanVisible = false;
        public bool IsVATInstalmentPlanVisible
        {
            get
            {
                return _isVATInstalmentPlanVisible;
            }
            set
            {
                _isVATInstalmentPlanVisible = value;
                RaisePropertyChanged("IsVATInstalmentPlanVisible");
            }
        }

        private string _formGuidValue = "";
        public string FormGuidValue
        {
            get
            {
                return _formGuidValue;
            }
            set
            {
                _formGuidValue = value;
                RaisePropertyChanged("FormGuidValue");
            }
        }
        private bool _isInstalmentSchedulePlanVisible = false;
        public bool IsInstalmentSchedulePlanVisible
        {
            get
            {
                return _isInstalmentSchedulePlanVisible;
            }
            set
            {
                _isInstalmentSchedulePlanVisible = value;
                RaisePropertyChanged("IsInstalmentSchedulePlanVisible");
            }
        }

        #endregion


        public VATInstalmentPlanListModel instalmentListModel { get; set; }
        public VATInstalmentPlanListModel InstalmentListModel
        {
            get
            {
                return instalmentListModel;
            }

            set
            {
                if (instalmentListModel == value)
                {
                    return;
                }

                instalmentListModel = value;
                RaisePropertyChanged("InstalmentListModel");
            }
        }

        private int _selectedOutletOptionIndex;
        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                _selectedOutletOptionIndex = value;
                RaisePropertyChanged("SelectedOutletOptionIndex");
            }
        }

        private string _scheduleNoOfMonths = "";
        public string ScheduleNoOfMonths
        {
            get
            {
                return _scheduleNoOfMonths;
            }
            set
            {
                _scheduleNoOfMonths = value;
                RaisePropertyChanged("ScheduleNoOfMonths");
            }
        }

        private string _scheduleMonthlyInstalment = "";
        public string ScheduleMonthlyInstalment
        {
            get
            {
                return _scheduleMonthlyInstalment;
            }
            set
            {
                _scheduleMonthlyInstalment = value;
                RaisePropertyChanged("ScheduleMonthlyInstalment");
            }
        }
        private string _scheduleTotalAmountPaid = "";
        public string ScheduleTotalAmountPaid
        {
            get
            {
                return _scheduleTotalAmountPaid;
            }
            set
            {
                _scheduleTotalAmountPaid = value;
                RaisePropertyChanged("ScheduleTotalAmountPaid");
            }
        }
        private string _scheduleAmountRemaining = "";
        public string ScheduleAmountRemaining
        {
            get
            {
                return _scheduleAmountRemaining;
            }
            set
            {
                _scheduleAmountRemaining = value;
                RaisePropertyChanged("ScheduleAmountRemaining");
            }
        }
        private string _noOfInstalments = "0";
        public string NoOfInstalments
        {
            get
            {
                return _noOfInstalments;
            }
            set
            {
                _noOfInstalments = value;
                RaisePropertyChanged("NoOfInstalments");
            }
        }
        private string _instalmentAmount = "0.00 SAR";
        public string InstalmentAmount
        {
            get
            {
                return _instalmentAmount;
            }
            set
            {
                _instalmentAmount = value;
                RaisePropertyChanged("InstalmentAmount");
            }
        }
        private string _penaltyAmount = "0.00 SAR";
        public string PenaltyAmount
        {
            get
            {
                return _penaltyAmount;
            }
            set
            {
                _penaltyAmount = value;
                RaisePropertyChanged("PenaltyAmount");
            }
        }
        private string _totalAmount = "0.00 SAR";
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }

        private string _totalLiabilityAmount = "0.00 SAR";
        public string TotalLiabilityAmount
        {
            get
            {
                return _totalLiabilityAmount;
            }
            set
            {
                _totalLiabilityAmount = value;
                RaisePropertyChanged("TotalLiabilityAmount");
            }
        }

        private InstalmentPlanModel _selectedOutletOption;
        public InstalmentPlanModel SelectedOutletOption
        {
            get
            {
                return _selectedOutletOption;
            }
            set
            {
                _selectedOutletOption = value;
                RaisePropertyChanged("SelectedOutletOption");
            }
        }

        private List<Result31> _requestForInstalmentPlanList;
        public List<Result31> RequestForInstalmentPlanList
        {
            get
            {
                return _requestForInstalmentPlanList;
            }
            set
            {
                _requestForInstalmentPlanList = value;
                RaisePropertyChanged("RequestForInstalmentPlanList");
            }
        }

        private List<VtiaIaSetResult> _requestForScheduleList;
        public List<VtiaIaSetResult> RequestForScheduleList
        {
            get
            {
                return _requestForScheduleList;
            }
            set
            {
                _requestForScheduleList = value;
                RaisePropertyChanged("RequestForScheduleList");
            }
        }



        private List<VtiaIadtSetResult> _requestForScheduleDetails;
        public List<VtiaIadtSetResult> RequestForScheduleDetails
        {
            get
            {
                return _requestForScheduleDetails;
            }
            set
            {
                _requestForScheduleDetails = value;
                RaisePropertyChanged("RequestForScheduleDetails");
            }
        }




        public ObservableCollection<InstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<InstalmentPlanModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }



        public ObservableCollection<ATTACHMENTSetResults> attachments { get; set; }
        public ObservableCollection<ATTACHMENTSetResults> Attachments
        {
            get
            {
                return attachments;
            }
            set
            {
                if (attachments == value)
                {
                    return;
                }
                attachments = value;
                RaisePropertyChanged("Attachments");
            }
        }
        public ObservableCollection<ZakatSelectBillModel> summarySelectedBillsList { get; set; }
        public ObservableCollection<ZakatSelectBillModel> SummarySelectedBillsList
        {
            get
            {
                return summarySelectedBillsList;
            }
            set
            {
                if (SummarySelectedBillsList == value)
                {
                    return;
                }
                summarySelectedBillsList = value;
                RaisePropertyChanged("SummarySelectedBillsList");
            }
        }


        public void AddOutletDecisionOptions()
        {



            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.VATInstalmentRequestToVatInstalment,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.VATInstalmentRequestToVatDisplayInstalment,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions = outletDecisionOptions;
        }


        public void EnableVATLandingPage()
        {
            AddOutletDecisionOptions();

            IsVATLandingPageVisible = true;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
        }

        public void EnableVAtInstalmentPlan()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = true;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
        }

        public void EnableVAtInstalmentSummary()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = true;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = false;
        }

        public void EnableDisplayInstalment()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = true;
            IsDisplayDetailsVisible = false;
        }

        public void EnableDisplayDetails()
        {
            IsVATLandingPageVisible = false;
            IsVATInstalmentPlanVisible = false;
            IsInstalmentSchedulePlanVisible = false;
            IsDisplayListVisible = false;
            IsDisplayDetailsVisible = true;
        }

        public void ResetData()
        {
            RequestForInstalmentPlanList = null;
            RequestForScheduleDetails = null;
        }


        #region Button Action Declaration

        public async void RequestInstalmentButtonClicked()
        {
            try
            {

                App.selectedVATItem = "";

                // _navigationService.NavigateTo(App.VatInstalmentPlanSuccessPage);

                _navigationService.NavigateTo(App.VatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {



            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        #endregion

        ReqVatInstalmentPlanResponse reqVatInstalmentPlanResponse;
        private ReqVatInstalmentPlanResponse _reqVatInstalmentPlanResponseList;
        public ReqVatInstalmentPlanResponse ReqVatInstalmentPlanResponseList
        {
            get
            {
                return _reqVatInstalmentPlanResponseList;
            }
            set
            {
                _reqVatInstalmentPlanResponseList = value;
                RaisePropertyChanged("ReqVatInstalmentPlanResponseList");
            }
        }

        public void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.ASSLISTSet.results != null)
            {
                //RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.ASSLISTSet.results;
                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.ASSLISTSet.results.Where(w => w.Fbtyp.Contains("VTIA")).ToList();

                if (RequestForInstalmentPlanList.Count > 0)
                {
                    NumberOfInstalmentPlans = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmetPlan;

                }
            }
        }

        #region API Methods

        #region GETVatInstalmentPlan

        public async Task GetVATInstalmentPlanList()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    ReqVatInstalmentPlanResponseList = null;
                    ReqVatInstalmentPlanResponse rEQVatInstalmentPlanResponse = null;
                    try
                    {

                        rEQVatInstalmentPlanResponse = await WebServiceManager.GetRequestToVATInstalmentData();
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        // If seesion Expired it will navigate to Dashboard page
                        PopToRootPage();


                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {
                            BindVatInstalments();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }



                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });

                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }


        #endregion


        public void GetDetailsClicked(int index)
        {
            //For first item list

            Console.WriteLine("Index: " + index);

            GetVATInstalmentPlanDetails(index);

            //  PopulateSummaryReasonData();

        }

        public void GetDisplayDetailsClicked(int index)
        {
            //
            Console.WriteLine("Index: " + index);
            //GetVATDisplaySchedule();

            GetVATDisplayScheduleDetails(index);

        }

        //Static data for Summary
        public void PopulateSummaryReasonData(RequestToVATInstallmentPlanDetails itemDetails)
        {
            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            foreach (var bill in itemDetails.d.VTIASet.results)
            {
                SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00"),
                    amount = "0.00 SAR",
                    saadNumber = bill.SadadNo,
                    taxPeriod = bill.Taxperioddsc,
                    isSelected = false,
                    billType = AppResources.ZakatInstalmetSelectTypeVAT

                });
            }

            Attachments = new ObservableCollection<ATTACHMENTSetResults>();

            foreach (var attach in itemDetails.d.ATTACHMENTSet.results)
            {
                Attachments.Add(attach);
            }
            NoOfInstalments = itemDetails.d.Noofinstallment;
            InstalmentAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.TotInvAmt)) + " " + AppResources.ZSAR;
            PenaltyAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Peneltyamt)) + " " + AppResources.ZSAR;
            TotalAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Totdueamt)) + " " + AppResources.ZSAR;
            TotalLiabilityAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Totliablityamt)) + " " + AppResources.ZSAR;
        }



        #region GETVatInstalmentPlan

        public async Task GetVATInstalmentPlanDetails(int index)
        {

            SummarySelectedBillsList = null;
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    //VatInstalments = null;
                    //VatInstalmentPlanResponse vATInstalment = null;
                    try
                    {

                        var selectedItem = RequestForInstalmentPlanList[index];


                        var selectedItemFormID = await WebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, selectedItem.Fbnum, App.LoginDataRetrieved.TIN, selectedItem.Fbust, "VTIA");
                        //vATInstalment = await WebServiceManager.GAZTGetVATInstalmentData();
                        //VatInstalments = vATInstalment;

                        if (selectedItemFormID.d != null)
                        {

                            var itemDetails = await WebServiceManager.GetRequestToVATInstalmentPlanDetails("", selectedItemFormID.d.Fbguid);

                            if (itemDetails != null && itemDetails.d != null)
                            {
                                PopulateSummaryReasonData(itemDetails);
                            }


                        }


                        PopToRootPage();
                        // If seesion Expired it will navigate to Dashboard page

                        // EnableSlectionView();


                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion

        #region GetVATDisplaySchedule

        public async Task GetVATDisplaySchedule()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;

                    try
                    {


                        var getFormID = await WebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, "", App.LoginDataRetrieved.TIN, "E0045", "VTIA");


                        if (getFormID.d != null)
                        {
                            FormGuidValue = getFormID.d.Fbguid;


                            DisplayInstallmentAgreementSchedulePlan itemDetails = await WebServiceManager.GetDisplayInstallmentAgreementSchedulePlan(formGuid: FormGuidValue);
                            // var itemDetails = await WebServiceManager.GetRequestToVATInstalmentPlanDetails("", getFormID.d.Fbguid);

                            if (itemDetails != null && itemDetails.d != null)
                            {

                                RequestForScheduleList = itemDetails.d.VtiaIahdSet.Results;
                            }


                        }


                        PopToRootPage();
                        // If seesion Expired it will navigate to Dashboard page

                        // EnableSlectionView();


                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion

        #region GetVATDisplayScheduleDetails

        public async Task GetVATDisplayScheduleDetails(int index)
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    //VatInstalments = null;
                    //VatInstalmentPlanResponse vATInstalment = null;
                    try
                    {
                        var selectedItem = RequestForScheduleList[index];


                        var itemDetails = await WebServiceManager.GetDisplayInstallmentScheduleDetails(selectedItem.Opbel, FormGuidValue, "");
                        // var itemDetails = await WebServiceManager.GetRequestToVATInstalmentPlanDetails("", getFormID.d.Fbguid);

                        if (itemDetails != null && itemDetails.d != null)
                        {

                            RequestForScheduleDetails = itemDetails.d.VtiaIadtSet.Results;
                            // RequestForScheduleList = itemDetails.d.VtiaIahdSet.Results;

                            for (int i = 0; i < RequestForScheduleDetails.Count; i++)
                            {
                                DateTime dateStart = new DateTime();

                                string apiDate = @"""" + RequestForScheduleDetails[i].DueDate + @"""";
                                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                //if (App.IsArabic)
                                //{

                                //    dateStart = DateTime.ParseExact(dt.ToString(), "yyyy/MM/dd", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                //}
                                //else
                                //{

                                //    dateStart = DateTime.ParseExact(dt.ToString(), "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                                //}

                                GregorianCalendar hjCalendar = new GregorianCalendar();
                                int year = hjCalendar.GetYear(dateStart);
                                int month = hjCalendar.GetMonth(dateStart);
                                int day = hjCalendar.GetDayOfMonth(dateStart);

                                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                RequestForScheduleDetails[i].DueDate = dateStr;

                                string dt1 = string.Empty;
                                string[] dts = null;
                                dts = RequestForScheduleDetails[i].DueDate.Split('/');
                                //dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                dt1 = dts[0] + "-" + dts[1] + "-" + dts[2];
                                RequestForScheduleDetails[i].DueDate = dt1;

                            }
                            ScheduleNoOfMonths = itemDetails.d.Noofmon;
                            ScheduleAmountRemaining = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalRemAmnt)) + " " + AppResources.ZSAR;
                            ScheduleMonthlyInstalment = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalInstall)) + " " + AppResources.ZSAR;
                            ScheduleTotalAmountPaid = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalAmntPaid)) + " " + AppResources.ZSAR;

                        }


                        PopToRootPage();
                        // If seesion Expired it will navigate to Dashboard page

                        // EnableSlectionView();


                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                //await Task.Run(() =>
                //{

                //});
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion

        #endregion


    }
}
