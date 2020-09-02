using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.VATInstalmentModels;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Models.ZakatInstalmentModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Xamarin.Forms;
using static EGAZT.Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel;
using static EGAZT.Models.ZakatInstalationModels.ZakatRequestDisplayModel;
using static EGAZT.Models.ZakatInstalmentModels.RequestToVATInstallmentPlanDetails;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #endregion

        #region commands
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }

        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
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
                if (IsZakatLandingPageVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (CreateZakatInstalmentBtnVisible)
                {
                    EnableZakatLandingPage();
                }
                else if (IsZakatSummaryVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsRevokZakatInstalmentVisible)
                {
                    EnableZakatLandingPage();
                }


                CloseClick = new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                });

            });

            EnableZakatLandingPage();

            ReqInstalmentBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            });

        }


        #region Views Enabling
        public void EnableZakatLandingPage()
        {
            AddOutletDecisionOptions();

            IsZakatLandingPageVisible = true;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;


        }
        public void EnableCreateZakatInstalment()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = true;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
        }
        public void EnableZakatInstalmentSummary()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = true;
            IsRevokZakatInstalmentVisible = false;
        }
        public void EnableRevokZakatInstalment()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = true;
        }

        #endregion

        #region Observables

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

        private bool _isZakatLandingPageVisible = true;
        public bool IsZakatLandingPageVisible
        {
            get
            {
                return _isZakatLandingPageVisible;
            }
            set
            {
                _isZakatLandingPageVisible = value;
                RaisePropertyChanged("IsZakatLandingPageVisible");
            }
        }

        private bool _createZakatInstalmentBtnVisible = false;
        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _createZakatInstalmentBtnVisible;
            }
            set
            {
                _createZakatInstalmentBtnVisible = value;
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }

        private bool _IsZakatSummaryVisible = false;
        public bool IsZakatSummaryVisible
        {
            get
            {
                return _IsZakatSummaryVisible;
            }
            set
            {
                _IsZakatSummaryVisible = value;
                RaisePropertyChanged("IsZakatSummaryVisible");
            }
        }

        private bool _isRevokZakatInstalmentVisible = true;
        public bool IsRevokZakatInstalmentVisible
        {
            get
            {
                return _isRevokZakatInstalmentVisible;
            }
            set
            {
                _isRevokZakatInstalmentVisible = value;
                RaisePropertyChanged("IsRevokZakatInstalmentVisible");
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

        //        ReqVatInstalmentPlanResponse reqVatInstalmentPlanResponse;
        private Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel _zakatInstalmentPlanRequestListModel;
        public Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel ZakatInstalmentPlanRequestListModel
        {
            get
            {
                return _zakatInstalmentPlanRequestListModel;
            }
            set
            {
                _zakatInstalmentPlanRequestListModel = value;
                RaisePropertyChanged("ZakatInstalmentPlanRequestListModel");
            }
        }
        private string _paymentFrequency = "";
        public string PaymentFrequency
        {
            get
            {
                return _paymentFrequency;
            }
            set
            {
                _paymentFrequency = value;
                RaisePropertyChanged("PaymentFrequency");
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

        private string _instalmentsCount = "0 " + AppResources.ZakatInstalmentRequestCount;
        public string InstalmentsCount
        {
            get
            {
                return _instalmentsCount;
            }
            set
            {
                _instalmentsCount = value;
                RaisePropertyChanged("InstalmentsCount");
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


        #endregion

        #region Lists

        public void AddOutletDecisionOptions()
        {

            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanCreate_Display,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanRevokeZAKAT,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions = outletDecisionOptions;
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


        private Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel _reqVatInstalmentPlanResponseList;
        public Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel ReqVatInstalmentPlanResponseList
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
        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> _requestForInstalmentPlanList;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> RequestForInstalmentPlanList
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

        public void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.WorklistSet.results != null)
            {
                RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.results;
                InstalmentsCount = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmentRequestCount;

                if (RequestForInstalmentPlanList.Count > 0)
                {
                    NumberOfInstalmentPlans = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmetPlan;

                }
            }

        }
        public void BindRevokList(ZakatRevokeList zakatRevokeList)
        {
            RevokList = new ObservableCollection<ZakatRevokeList.Result>();

            if (zakatRevokeList.d.WorklistSet.results != null)
            {
                for (int i = 0; i < zakatRevokeList.d.WorklistSet.results.Count; i++)
                {
                    RevokList.Add(zakatRevokeList.d.WorklistSet.results[i]);
                }
            }
        }

        public ObservableCollection<ZakatRevokeList.Result> _revokList { get; set; }
        public ObservableCollection<ZakatRevokeList.Result> RevokList
        {
            get
            {
                return _revokList;
            }
            set
            {
                if (_revokList == value)
                {
                    return;
                }
                _revokList = value;
                RaisePropertyChanged("RevokList");
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
        public ObservableCollection<ZakatRequestDisplayModel.Result> attachments { get; set; }
        public ObservableCollection<ZakatRequestDisplayModel.Result> Attachments
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

        public void BindZakatSummaryData(ZakatRequestDisplayModel zakatRequestDisplayModel)
        {
            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();
            foreach (var bill in zakatRequestDisplayModel.d.Z_INVOICE_UI5Set.results)
            {
                SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00"),
                    amount = "0.00 SAR",
                    saadNumber = bill.AIvNoTb,
                    taxPeriod = bill.ADueDtTb.ToString(),
                    isSelected = false,
                    billType = AppResources.ZakatInstalmetSelectTypeZakat

                });
            }

            Attachments = new ObservableCollection<ZakatRequestDisplayModel.Result>();

            for (int i = 0; i < zakatRequestDisplayModel.d.AttDetSet.results.Count; i++)
            {
                Attachments.Add(zakatRequestDisplayModel.d.AttDetSet.results[i]);
            }

            NoOfInstalments = zakatRequestDisplayModel.d.ANoOfInstTp;
            PaymentFrequency = zakatRequestDisplayModel.d.APaymentFreq;
            InstalmentAmount = string.Format("{0:N2}", double.Parse(zakatRequestDisplayModel.d.AAppInstAmt)) + " " + AppResources.ZSAR;
            //         PenaltyAmount = string.Format("{0:N2}", double.Parse(zakatRequestDisplayModel.d.p)) + " " + AppResources.ZSAR;
            TotalAmount = string.Format("{0:N2}", double.Parse(zakatRequestDisplayModel.d.ATotalAmt)) + " " + AppResources.ZSAR;
        }

        #endregion


        #region APIs

        public async Task GetSummaryDetailsClickedAsync(int index)
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
                    DisplayInfoModel _DisplayRequestData = new DisplayInfoModel();
                    try
                    {
                        var item = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[index];

                        var itemDetails = await WebServiceManager.GAZTGetZakatRequestDisplayData(item.Fbnum, item.Fbsta);


                        PopToRootPage();


                        if (itemDetails != null && itemDetails.d != null)
                        {
                            BindZakatSummaryData(itemDetails);
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
        public async Task GetZakatInstalmentPlanList()
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
                    Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                    try
                    {
                        rEQVatInstalmentPlanResponse = await WebServiceManager.GAZTGetZakatInstalmentPlanRequestList("", "", "");
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        PopToRootPage();

                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {

                            for (int i = 0; i < ReqVatInstalmentPlanResponseList.d.WorklistSet.results.Count; i++)
                            {
                                DateTime dateStart = new DateTime();
                                CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                string apiDate = @"""" + ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt + @"""";
                                dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                GregorianCalendar hjCalendar = new GregorianCalendar();
                                int year = hjCalendar.GetYear(dateStart);
                                int month = hjCalendar.GetMonth(dateStart);
                                int day = hjCalendar.GetDayOfMonth(dateStart);

                                string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt = dateStr;

                                string dt1 = string.Empty;
                                string[] dts = null;
                                dts = ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt.Split('/');
                                dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].SubmitDt = dt1;

                            }
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

        public async Task GetZakatRevokList()
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
                    //           ReqVatInstalmentPlanResponseList = null;
                    //          Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel rEQVatInstalmentPlanResponse = null;

                    try
                    {

                        ZakatRevokeList revokResult = await WebServiceManager.GAZTGetZakatRevokeList("", "", "");
                        // ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;

                        PopToRootPage();


                        if (revokResult != null && revokResult.d != null)
                        {
                            BindRevokList(revokResult);
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
        #endregion


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

    }
}
