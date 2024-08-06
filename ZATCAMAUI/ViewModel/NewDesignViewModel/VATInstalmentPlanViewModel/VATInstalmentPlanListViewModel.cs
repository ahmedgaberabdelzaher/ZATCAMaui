using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;


using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.VATInstalmentModels;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.VATInstalmentScheduleDetailsModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class VATInstalmentPlanListViewModel : BaseViewModel
    {
        #region Variable
        
        #endregion

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }


        public VATInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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



            RequestInstalmentButtonTapped = new Command(RequestInstalmentButtonClicked);



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
                if (_isDisplayListVisible == value) return;

                _isDisplayListVisible = value;
                OnPropertyChanged("IsDisplayListVisible");
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
                if (_isDisplayDetailsVisible == value) return;

                _isDisplayDetailsVisible = value;
                OnPropertyChanged("IsDisplayDetailsVisible");
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
                if (_numberOfInstalmentPlans == value) return;

                _numberOfInstalmentPlans = value;
                OnPropertyChanged("NumberOfInstalmentPlans");
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
                if (_isVATLandingPageVisible == value) return;

                _isVATLandingPageVisible = value;
                OnPropertyChanged("IsVATLandingPageVisible");
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
                if (_isVATInstalmentPlanVisible == value) return;

                _isVATInstalmentPlanVisible = value;
                OnPropertyChanged("IsVATInstalmentPlanVisible");
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
                if (_formGuidValue == value) return;

                _formGuidValue = value;
                OnPropertyChanged("FormGuidValue");
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
                if (_isInstalmentSchedulePlanVisible == value) return;

                _isInstalmentSchedulePlanVisible = value;
                OnPropertyChanged("IsInstalmentSchedulePlanVisible");
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
                OnPropertyChanged("InstalmentListModel");
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
                if (_selectedOutletOptionIndex == value) return;

                _selectedOutletOptionIndex = value;
                OnPropertyChanged("SelectedOutletOptionIndex");
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
                if (_scheduleNoOfMonths == value) return;

                _scheduleNoOfMonths = value;
                OnPropertyChanged("ScheduleNoOfMonths");
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
                if (_scheduleMonthlyInstalment == value) return;

                _scheduleMonthlyInstalment = value;
                OnPropertyChanged("ScheduleMonthlyInstalment");
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
                if (_scheduleTotalAmountPaid == value) return;

                _scheduleTotalAmountPaid = value;
                OnPropertyChanged("ScheduleTotalAmountPaid");
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
                if (_scheduleAmountRemaining == value) return;

                _scheduleAmountRemaining = value;
                OnPropertyChanged("ScheduleAmountRemaining");
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
                if (_noOfInstalments == value) return;

                _noOfInstalments = value;
                OnPropertyChanged("NoOfInstalments");
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
                if (_instalmentAmount == value) return;

                _instalmentAmount = value;
                OnPropertyChanged("InstalmentAmount");
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
                if (_penaltyAmount == value) return;

                _penaltyAmount = value;
                OnPropertyChanged("PenaltyAmount");
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
                if (_totalAmount == value) return;

                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
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
                if (_totalLiabilityAmount == value) return;

                _totalLiabilityAmount = value;
                OnPropertyChanged("TotalLiabilityAmount");
            }
        }

        private string _AgreementNumber = "";
        public string AgreementNumber
        {
            get
            {
                return _AgreementNumber;
            }
            set
            {
                if (_AgreementNumber == value) return;

                _AgreementNumber = value;
                OnPropertyChanged("AgreementNumber");
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
                if (_selectedOutletOption == value) return;

                _selectedOutletOption = value;
                OnPropertyChanged("SelectedOutletOption");
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
                if (_requestForInstalmentPlanList == value) return;

                _requestForInstalmentPlanList = value;
                OnPropertyChanged("RequestForInstalmentPlanList");
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
                if (_requestForScheduleList == value) return;

                _requestForScheduleList = value;
                OnPropertyChanged("RequestForScheduleList");
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
                if (_requestForScheduleDetails == value) return;

                _requestForScheduleDetails = value;
                OnPropertyChanged("RequestForScheduleDetails");
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
                OnPropertyChanged("OutletDecisionOptions");
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
                if (attachments == value) return;

                attachments = value;
                OnPropertyChanged("Attachments");
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
                if (summarySelectedBillsList == value) return;

                summarySelectedBillsList = value;
                OnPropertyChanged("SummarySelectedBillsList");
            }
        }



        private bool _isAttachmentsListVisible = false;
        public bool IsAttachmentsListVisible
        {
            get
            {
                return _isAttachmentsListVisible;
            }
            set
            {
                if (_isAttachmentsListVisible == value) return;

                _isAttachmentsListVisible = value;
                OnPropertyChanged("IsAttachmentsListVisible");
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
            VATInstalmentList = new ObservableCollection<Result31>();
            RequestForInstalmentPlanList = null;
            RequestForScheduleDetails = null;

        }

        public void SummaryData()
        {

            Attachments = null;
            SummarySelectedBillsList = null;


            NoOfInstalments = "";
            InstalmentAmount = "";
            PenaltyAmount = "";
            TotalAmount = "";
            TotalLiabilityAmount = "";
        }



        #region Button Action Declaration

        public void RequestInstalmentButtonClicked()
        {
            try
            {

                App.selectedVATItem = "";
                App.selectedVATItemFbust = "";

                // _navigationService.NavigateTo(App.VatInstalmentPlanSuccessPage);

                _navigationService.NavigateTo(App.VatInstalmentPlanPageView);
            }
            catch (GAZTUnlockAccountException ex)
            {
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        #endregion


        private ReqVatInstalmentPlanResponse _reqVatInstalmentPlanResponseList;
        public ReqVatInstalmentPlanResponse ReqVatInstalmentPlanResponseList
        {
            get
            {
                return _reqVatInstalmentPlanResponseList;
            }
            set
            {
                if (_reqVatInstalmentPlanResponseList == value) return;

                _reqVatInstalmentPlanResponseList = value;
                OnPropertyChanged("ReqVatInstalmentPlanResponseList");
            }
        }

        private ObservableCollection<Result31> _vATInstalmentList { get; set; }
        public ObservableCollection<Result31> VATInstalmentList
        {
            get
            {
                return _vATInstalmentList;
            }
            set
            {
                if (_vATInstalmentList == value) return;

                _vATInstalmentList = value;
                OnPropertyChanged("VATInstalmentList");
            }
        }

        public void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.ASSLISTSet.results != null)
            {
                var VATInstalmentListData = new ObservableCollection<Result31>();


                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.ASSLISTSet.results.Where(w => w.Fbtyp.Contains("VTIA")).ToList();

                foreach (var instalmentListModel in RequestForInstalmentPlanList)
                {

                    VATInstalmentListData.Add(instalmentListModel);
                }

                VATInstalmentList = VATInstalmentListData;

                if (VATInstalmentList.Count > 0)
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

                        rEQVatInstalmentPlanResponse = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentData();
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        // If seesion Expired it will navigate to Dashboard page
                        PopToRootPage();


                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {
                            BindVatInstalments();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
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
                        MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }


        #endregion


        public async Task GetDetailsClicked(int index)
        {
            //For first item list

            //("Index: " + index);

            await GetVATInstalmentPlanDetails(index);

            //  PopulateSummaryReasonData();

        }

        public async Task GetDisplayDetailsClicked(int index)
        {

            await GetVATDisplayScheduleDetails(index);

        }

        //Static data for Summary

        public void PopulateSummaryReasonData(RequestToVATInstallmentPlanDetails itemDetails)
        {
            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            foreach (var bill in itemDetails.d.VTIASet.results)
            {
                if (bill.Xsele == "X")
                {

                    SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                    {
                        billNumber = AppResources.Bill + " " + (SummarySelectedBillsList.Count + 1).ToString("00") + ":",
                        amount = bill.Betrh,
                        saadNumber = bill.SadadNo,
                        taxPeriod = bill.Taxperioddsc,
                        isSelected = false,
                        billType = AppResources.ZakatInstalmetSelectTypeVAT
                    });

                }



            }



            var attachments = new ObservableCollection<ATTACHMENTSetResults>();

            foreach (var attach in itemDetails.d.ATTACHMENTSet.results)
            {
                if (string.IsNullOrEmpty(attach.Filename))
                {
                    attach.Filename = DateTime.Now.ToString("yyyy/MM/dd");
                }
                attachments.Add(attach);
            }

            Attachments = attachments;

            if (Attachments.Count == 0)
            {
                IsAttachmentsListVisible = false;
            }
            else
            {
                IsAttachmentsListVisible = true;
            }

            NoOfInstalments = itemDetails.d.Noofinstallment;
            InstalmentAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.TotInvAmt)) + " " + AppResources.ZSAR;
            PenaltyAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Peneltyamt));
            TotalAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.Totdueamt));
            TotalLiabilityAmount = string.Format("{0:N2}", double.Parse(itemDetails.d.VTISSet.results[0].Betrw));

        }


        #region GETVatInstalmentPlan

        public async Task GetVATInstalmentPlanDetails(int index)
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

                        var selectedItem = RequestForInstalmentPlanList[index];


                        var selectedItemFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, selectedItem.Fbnum, App.LoginDataRetrieved.TIN, selectedItem.Fbust, "VTIA");
                        

                        if (selectedItemFormID.d != null)
                        {

                            var itemDetails = await VATInstalationPlanWebServiceManager.GetRequestToVATInstalmentPlanDetails("", selectedItemFormID.d.Fbguid);

                            if (itemDetails != null && itemDetails.d != null)
                            {
                                PopulateSummaryReasonData(itemDetails);
                            }


                        }


                        PopToRootPage();


                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {


                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {



                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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


                        var getFormID = await VATInstalationPlanWebServiceManager.GAZTGetFbGuidDetailsInputData(App.LoginDataRetrieved.FbGuid, "", App.LoginDataRetrieved.TIN, "E0045", "VTIA");


                        if (getFormID.d != null)
                        {
                            FormGuidValue = getFormID.d.Fbguid;


                            DisplayInstallmentAgreementSchedulePlan itemDetails = await VATInstalationPlanWebServiceManager.GetDisplayInstallmentAgreementSchedulePlan(formGuid: FormGuidValue);
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
                        MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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

                        AgreementNumber = selectedItem.AgreementNo;

                        var itemDetails = await VATInstalationPlanWebServiceManager.GetDisplayInstallmentScheduleDetails(selectedItem.Opbel, FormGuidValue, "");
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
                            ScheduleAmountRemaining = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalRemAmnt));
                            ScheduleMonthlyInstalment = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalInstall));
                            ScheduleTotalAmountPaid = string.Format("{0:N2}", double.Parse(itemDetails.d.TotalAmntPaid));

                        }


                        PopToRootPage();

                        IsLoading = false;
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        #endregion

        #endregion


    }
}
