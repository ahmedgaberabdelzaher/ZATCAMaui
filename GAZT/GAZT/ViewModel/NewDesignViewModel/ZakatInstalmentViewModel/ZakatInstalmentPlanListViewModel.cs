using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Models.ZakatInstalmentModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Metadata = EGAZT.Models.ZakatInstalationModels.Metadata;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    [Preserve(AllMembers = true)]
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private bool _isZakatLandingPageVisible = false;
        private bool _createZakatInstalmentBtnVisible = true;
        private bool _isLoading = false;
        private bool _isRevokZakatInstalmentVisible = true;
        private bool _isOTPPageVisible = false;
        private bool _IsZakatSummaryVisible = false;
        private bool _isZakatSummaryRevokeVisible = false;
        private bool _isAttachmentsViewEnabled = false;
        ZakatInstalmentValidateNewRequestModel result = null;

        public Timer otpTimer;
        public int countDownSeconds;

        #endregion

        #region commands
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand OnContinueClick { get; set; }
        public ICommand SummaryContinueBtnTapped { get; set; }
        public ICommand SummaryRevokeBtnTapped { get; set; }
        public Command NoteContinueTapped { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public Command Download_Acknowledgement { get; set; }
        public Command ZDownloadForm { get; set; }

        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
        {

            //IsZakat = Preferences.Get("isZakat", false);

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

            CloseClick = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });


            IsZakat = Preferences.Get("isZakat", false);
            if (IsZakat)
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
            }
            else
            {
                ZakatTitle = AppResources.ZakatInstalmetSelectTypeIncomeTax;
            }

            GoBackClick = new Command(async () =>
            {
                if (IsZakatLandingPageVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (CreateZakatInstalmentBtnVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                    // EnableZakatLandingPage();
                }
                else if (IsNoteViewVisible)
                {
                    EnableZakatInstalmentSummary();
                }
                else if (IsZakatSummaryVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsDueBillsVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsRevokZakatInstalmentVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsOTPPageVisible)
                {
                    EnableNotePage();
                }




            });





            OnContinueClick = new Command(async () =>
            {
                SetOTP();
                if (!string.IsNullOrEmpty(EnteredOTP) && EnteredOTP.Length > 0)
                {
                    await ValidateOTPAsync();
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));

                    // _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

                }
            });


            OnResendOTPClicked = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {

                    await SendOTPToRegisterMobileNumber(SelectedFbNum, "");
                }

            });


            ReqInstalmentBtnTapped = new Command(() =>
            {
                CheckDueInvoices();
                // _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            });
            SummaryContinueBtnTapped = new Command(() =>
            {
                //EnableOTPPage();
            });

            SummaryRevokeBtnTapped = new Command(async () =>
            {
                var result = await App.Current.MainPage.DisplayAlert(AppResources.VatRefundsConfirmationTit, AppResources.ZakatRevokConfirmationText, AppResources.ZZCancel, AppResources.CRContinue);
                if (!result)
                {
                    App.TP = null;
                    EnableNotePage();
                }

            });

            NoteContinueTapped = new Command(async () =>
            {
                await ValidateNoteAndContinueAsync();
            });

            ZDownloadForm = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                if (SelectedFbNum != null)
                {
                    String downloadurl = Constants.ZakatdownloadCoverFormFile + "'" + SelectedFbNum + "')/$value";
                    //await WebServiceManager.FileDownload(downloadurl, "pdf");
                    _navigationService.NavigateTo(App.PdfView, downloadurl);

                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });



            });

            Download_Acknowledgement = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                if (SelectedFbNum != null)
                {
                    String downloadurl = Constants.ZOdownloadAckLetter + "'" + SelectedFbNum + "')/$value";
                    //await WebServiceManager.FileDownload(downloadurl, "pdf");
                    _navigationService.NavigateTo(App.PdfView, downloadurl);



                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });



            });

        }
        public async Task onPageLoad()
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
                        PopToRootPage();



                        result = await ZakatInstallmentPlanWebServiceManager.GAZTGetZakatInstalmentValidateNewReq();
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



                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });





            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

        }


        public ObservableCollection<EvtNotif12SetResult> _DueInvoicesListSet12 { get; set; }
        public ObservableCollection<EvtNotif12SetResult> DueInvoicesListSet12
        {
            get { return _DueInvoicesListSet12; }



            set
            {
                if (_DueInvoicesListSet12 == value)
                {
                    return;
                }


                _DueInvoicesListSet12 = value;
                RaisePropertyChanged("DueInvoicesListSet12");
            }
        }

        public void CheckDueInvoices()
        {
            IsLoading = true;

            // result = await WebServiceManager.GAZTGetZakatInstalmentValidateNewReq();
            DueInvoicesList = null;
            DueInvoicesListSet12 = null;





            var dueInvoicesList = new ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2>();
            var dueInvoicesListSet12 = new ObservableCollection<EvtNotif12SetResult>();
            if (result != null && result.d != null)
            {
                if (result.d.EvtNotif1Set != null && result.d.EvtNotif1Set.results.Count > 0)
                {



                    foreach (ZakatInstalmentValidateNewRequestModel.Result2 result2 in result.d.EvtNotif1Set.results)
                    {



                        DateTime dateStart = new DateTime();
                        DateTime dateStart2 = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + result2.Abrzo + @"""";
                        string apiDate2 = @"""" + result2.Abrzu + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);
                        dateStart2 = JsonConvert.DeserializeObject<DateTime>(apiDate2);



                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);
                        int year2 = hjCalendar.GetYear(dateStart2);
                        int month2 = hjCalendar.GetMonth(dateStart2);
                        int day2 = hjCalendar.GetDayOfMonth(dateStart2);



                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
                        string dateStr2 = string.Format("{0:00}/{1}/{2}", day2, month2, year2);



                        result2.Abrzo = dateStr;
                        result2.Abrzu = dateStr2;



                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = result2.Abrzo.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        result2.Abrzo = dt1;



                        string dt2 = string.Empty;
                        string[] dts2 = null;
                        dts2 = result2.Abrzu.Split('/');
                        dt2 = dts2[0] + "-" + UtilityManager.GetShortMonthName(dts2[1]) + "-" + dts2[2];
                        result2.Abrzu = dt2;

                        result2.Betrh = string.Format("{0:N2}", double.Parse(result2.Betrh.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR;

                        dueInvoicesList.Add(result2);
                    }
                    DueInvoicesList = dueInvoicesList;
                    EnableDueInvoicesPage();
                }
                else if (result.d.EvtNotif12Set != null && result.d.EvtNotif12Set.results.Count > 0)
                {
                    foreach (EvtNotif12SetResult evtNotif12SetResult in result.d.EvtNotif12Set.results)
                    {



                        DateTime dateStart = new DateTime();
                        DateTime dateStart2 = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + evtNotif12SetResult.Faedn + @"""";
                        string apiDate2 = @"""" + evtNotif12SetResult.Cdate + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);
                        dateStart2 = JsonConvert.DeserializeObject<DateTime>(apiDate2);



                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);
                        int year2 = hjCalendar.GetYear(dateStart2);
                        int month2 = hjCalendar.GetMonth(dateStart2);
                        int day2 = hjCalendar.GetDayOfMonth(dateStart2);



                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);
                        string dateStr2 = string.Format("{0:00}/{1}/{2}", day2, month2, year2);



                        evtNotif12SetResult.Faedn = dateStr;
                        evtNotif12SetResult.Cdate = dateStr2;



                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = evtNotif12SetResult.Faedn.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        evtNotif12SetResult.Faedn = dt1;



                        string dt2 = string.Empty;
                        string[] dts2 = null;
                        dts2 = evtNotif12SetResult.Cdate.Split('/');
                        dt2 = dts2[0] + "-" + UtilityManager.GetShortMonthName(dts2[1]) + "-" + dts2[2];
                        evtNotif12SetResult.Cdate = dt2;



                        dueInvoicesListSet12.Add(evtNotif12SetResult);
                    }
                    DueInvoicesListSet12 = dueInvoicesListSet12;
                    EnableDueInvoicesPage();
                }
                else
                {
                    App.selectedZakatItem = "";
                    _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
                }

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

        public void ResetData()
        {
            NumberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;
            App.selectedZakatItem = "";

        }

        private bool _isZakat = false;
        public bool IsZakat
        {
            get
            {
                return _isZakat;
            }
            set
            {
                if (_isZakat == value) return;
                _isZakat = value;
                RaisePropertyChanged("IsZakat");
            }
        }

        private string _zakatTitle = AppResources.ZakatInstalmetSelectTypeZakat;
        public string ZakatTitle
        {
            get
            {
                return _zakatTitle;
            }
            set
            {
                if (_zakatTitle == value) return;

                _zakatTitle = value;
                RaisePropertyChanged("ZakatTitle");
            }
        }

        private ZakatInstalmentPlanRevokeResponse _zakatInstalments;
        public ZakatInstalmentPlanRevokeResponse ZakatInstalments
        {
            get
            {
                return _zakatInstalments;
            }
            set
            {
                if (_zakatInstalments == value) return;

                _zakatInstalments = value;
                RaisePropertyChanged("ZakatInstalments");
            }
        }

        private string _noteEditor;
        public string NoteEditor
        {
            get
            {
                return _noteEditor;
            }
            set
            {
                if (_noteEditor == value) return;

                _noteEditor = value;
                RaisePropertyChanged("NoteEditor");
            }
        }
        private bool _IsDueBillsVisible = false;
        public bool IsDueBillsVisible
        {
            get
            {
                return _IsDueBillsVisible;
            }
            set
            {
                if (_IsDueBillsVisible == value) return;

                _IsDueBillsVisible = value;
                RaisePropertyChanged("IsDueBillsVisible");
            }
        }


        #region Views Enabling
        public void EnableZakatLandingPage()
        {
            AddOutletDecisionOptions();
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = true;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;



        }
        public void EnableDueInvoicesPage()
        {



            IsDueBillsVisible = true;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;



        }
        public void EnableNotePage()
        {
            IsDueBillsVisible = false;
            IsNoteViewVisible = true;
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
        }



        public void EnableOTPPage()
        {
            IsDueBillsVisible = false;
            IsOTPPageVisible = true;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsNoteViewVisible = false;



            // StartOTPTimer();
        }
        public void EnableCreateZakatInstalment()
        {
            IsDueBillsVisible = false;
            IsOTPPageVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = true;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableZakatInstalmentSummary()
        {
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = true;
            IsRevokZakatInstalmentVisible = false;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;
        }
        public void EnableRevokZakatInstalment()
        {
            IsDueBillsVisible = false;
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatSummaryVisible = false;
            IsRevokZakatInstalmentVisible = true;
            IsOTPPageVisible = false;
            IsNoteViewVisible = false;
        }

        #endregion

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
            }
        }

        private string _oTPFirstDigit;
        public string OTPFirstDigit
        {
            get
            {
                return _oTPFirstDigit;
            }
            set
            {
                if (_oTPFirstDigit == value) return;

                _oTPFirstDigit = value;
                if (!string.IsNullOrEmpty(OTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFirstDigit = string.Empty;
                    }
                }

                RaisePropertyChanged("OTPFirstDigit");
            }
        }

        private string _OTPSecondDigit;
        public string OTPSecondDigit
        {
            get
            {
                return _OTPSecondDigit;
            }
            set
            {
                if (_OTPSecondDigit == value) return;

                _OTPSecondDigit = value;
                if (!string.IsNullOrEmpty(OTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPSecondDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("OTPSecondDigit");
            }
        }

        private string _OTPThirdDigit;
        public string OTPThirdDigit
        {
            get
            {
                return _OTPThirdDigit;
            }
            set
            {
                if (_OTPThirdDigit == value) return;

                _OTPThirdDigit = value;
                if (!string.IsNullOrEmpty(OTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPThirdDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("OTPThirdDigit");
            }
        }

        private string _OTPFourthDigit;
        public string OTPFourthDigit
        {
            get
            {
                return _OTPFourthDigit;
            }
            set
            {
                if (_OTPFourthDigit == value) return;

                _OTPFourthDigit = value;
                if (!string.IsNullOrEmpty(OTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFourthDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("OTPFourthDigit");
            }
        }

        private bool CheckOnlyNumber(char letter)
        {
            if ((letter >= 48 && letter <= 57))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _continueButtonEnability = true;
        public bool ContinueButtonEnability
        {
            get
            {
                return _continueButtonEnability;
            }
            set
            {
                if (_continueButtonEnability == value) return;

                _continueButtonEnability = value;

                RaisePropertyChanged(() => ContinueButtonEnability);
            }
        }
        private string _selectedFrequencyName = "";
        public string SelectedFrequencyName
        {
            get
            {
                return _selectedFrequencyName;
            }
            set
            {
                if (_selectedFrequencyName == value) return;

                _selectedFrequencyName = value;
                RaisePropertyChanged("SelectedFrequencyName");
            }
        }

        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                if (_isResendOTPEnabled == value) return;

                _isResendOTPEnabled = value;
                // OnResendOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }


        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            /*if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();*/


            if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else if (countDownSeconds > 60)
            {
                int countDownSecondsL = countDownSeconds - 60;
                LblCountDownTimer = "1:" + countDownSecondsL.ToString();

                if (countDownSecondsL <= 9)
                    LblCountDownTimer = "1:0" + countDownSecondsL.ToString();
            }
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();


            // Stop timer
            if (countDownSeconds == 0)
            {
                ContinueButtonEnability = false;
                IsResendOTPEnabled = true;

                otpTimer.Stop();
            }
        }

        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;
            LblCountDownTimer = "0." + countDownSeconds.ToString();

            otpTimer.Enabled = true;
        }


        public async Task ValidateNoteAndContinueAsync()
        {
            if (NoteEditor.Length > 0)
            {
                await validateZakatRevoke();
            }
            else
            {
                await _dialogService.ShowMessage(AppResources.ZakatRevokCannotLeaveEmpty, AppResources.Information);
            }
        }


        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private bool _isSummaryAttachmentsVisible = false;
        public bool IsSummaryAttachmentsVisible
        {
            get
            {
                return _isSummaryAttachmentsVisible;
            }
            set
            {
                if (_isSummaryAttachmentsVisible == value) return;

                _isSummaryAttachmentsVisible = value;
                RaisePropertyChanged("IsSummaryAttachmentsVisible");
            }
        }

        public bool IsZakatLandingPageVisible
        {
            get
            {
                return _isZakatLandingPageVisible;
            }
            set
            {
                if (_isZakatLandingPageVisible == value) return;

                _isZakatLandingPageVisible = value;
                RaisePropertyChanged("IsZakatLandingPageVisible");
            }
        }


        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _createZakatInstalmentBtnVisible;
            }
            set
            {
                if (_createZakatInstalmentBtnVisible == value) return;

                _createZakatInstalmentBtnVisible = value;
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }


        public bool IsOTPPageVisible
        {
            get
            {
                return _isOTPPageVisible;
            }
            set
            {
                if (_isOTPPageVisible == value) return;

                _isOTPPageVisible = value;
                RaisePropertyChanged("IsOTPPageVisible");
            }
        }
        public bool IsZakatSummaryVisible
        {
            get
            {
                return _IsZakatSummaryVisible;
            }
            set
            {
                if (_IsZakatSummaryVisible == value) return;

                _IsZakatSummaryVisible = value;
                RaisePropertyChanged("IsZakatSummaryVisible");
            }
        }


        public bool IsZakatSummaryRevokeVisible
        {
            get
            {
                return _isZakatSummaryRevokeVisible;
            }
            set
            {
                if (_isZakatSummaryRevokeVisible == value) return;

                _isZakatSummaryRevokeVisible = value;
                RaisePropertyChanged("IsZakatSummaryRevokeVisible");
            }
        }

        public bool IsRevokZakatInstalmentVisible
        {
            get
            {
                return _isRevokZakatInstalmentVisible;
            }
            set
            {
                if (_isRevokZakatInstalmentVisible == value) return;

                _isRevokZakatInstalmentVisible = value;
                RaisePropertyChanged("IsRevokZakatInstalmentVisible");
            }
        }

        public bool _isNoteViewVisible = false;
        public bool IsNoteViewVisible
        {
            get
            {
                return _isNoteViewVisible;
            }
            set
            {
                if (_isNoteViewVisible == value) return;

                _isNoteViewVisible = value;
                RaisePropertyChanged("IsNoteViewVisible");
            }
        }

        public ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2> _DueInvoicesList { get; set; }
        public ObservableCollection<ZakatInstalmentValidateNewRequestModel.Result2> DueInvoicesList
        {
            get { return _DueInvoicesList; }



            set
            {
                if (_DueInvoicesList == value)
                {
                    return;
                }



                _DueInvoicesList = value;
                RaisePropertyChanged("DueInvoicesList");
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
                if (_zakatInstalmentPlanRequestListModel == value) return;

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
                if (_paymentFrequency == value) return;

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
                if (_noOfInstalments == value) return;

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
                if (_instalmentAmount == value) return;

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
                if (_penaltyAmount == value) return;

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
                if (_instalmentsCount == value) return;

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
                if (_totalAmount == value) return;

                _totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }

        private string _selectedFbNum = "";
        public string SelectedFbNum
        {
            get
            {
                return _selectedFbNum;
            }
            set
            {
                if (_selectedFbNum == value) return;

                _selectedFbNum = value;
                RaisePropertyChanged("SelectedFbNum");
            }
        }




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
                if (_numberOfInstalmentPlans == value) return;

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
                if (_reqVatInstalmentPlanResponseList == value) return;

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
                if (_requestForInstalmentPlanList == value) return;

                _requestForInstalmentPlanList = value;
                RaisePropertyChanged("RequestForInstalmentPlanList");
            }
        }
        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> _RevokeListItem;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.Result> RevokeListItem
        {
            get
            {
                return _RevokeListItem;
            }
            set
            {
                if (_RevokeListItem == value) return;

                _RevokeListItem = value;
                RaisePropertyChanged("RevokeListItem");
            }
        }

        private List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.RevokeListResult> _requestForRevokeList;
        public List<Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel.RevokeListResult> RequestForRevokeList
        {
            get
            {
                return _requestForRevokeList;
            }
            set
            {
                if (_requestForRevokeList == value) return;

                _requestForRevokeList = value;
                RaisePropertyChanged("RequestForRevokeList");
            }
        }


        public async void BindVatInstalments()
        {
            if (ReqVatInstalmentPlanResponseList.d.WorklistSet.results != null)
            {
                if (RequestForInstalmentPlanList != null)
                {

                    RequestForInstalmentPlanList.Clear();

                }

                IsZakat = Preferences.Get("isZakat", false);
                if (IsZakat)
                {

                    RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.results.Where(x => x.IptypeFg == "NZ" || x.IptypeFg == "").ToList();
                }
                else
                {
                    RequestForInstalmentPlanList = ReqVatInstalmentPlanResponseList.d.WorklistSet.results.Where(x => x.IptypeFg == "NI" || x.IptypeFg == "").ToList();


                }


                for (int i = 0; i < RequestForInstalmentPlanList.Count; i++)
                {
                    RequestForInstalmentPlanList[i].DpAmt = string.Format("{0:N2}", ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].DpAmt) + " " + AppResources.FORM5SAR;
                    RequestForInstalmentPlanList[i].TotAmt = string.Format("{0:N2}", ReqVatInstalmentPlanResponseList.d.WorklistSet.results[i].TotAmt) + " " + AppResources.FORM5SAR;

                    string submitDate = "";
                    if (RequestForInstalmentPlanList[i].SubmitDt != null)
                    {

                        DateTime dateStart = new DateTime();
                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        string apiDate = @"""" + RequestForInstalmentPlanList[i].SubmitDt + @"""";
                        dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                        RequestForInstalmentPlanList[i].SubmitDt = dateStr;

                        string dt1 = string.Empty;
                        string[] dts = null;
                        dts = RequestForInstalmentPlanList[i].SubmitDt.Split('/');
                        dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                        submitDate = dt1;
                    }

                    var zakatbill = "";
                    if (RequestForInstalmentPlanList[i].IptypeFg == "NZ")
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;
                    }
                    else if (RequestForInstalmentPlanList[i].IptypeFg == "NI")
                    {
                        zakatbill = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                    }
                    else
                    {

                        IsZakat = Preferences.Get("isZakat", false);
                        if (IsZakat)
                        {
                            zakatbill = AppResources.ZakatInstalmetSelectTypeZakat;

                        }
                        else
                        {
                            zakatbill = AppResources.ZakatInstalmetSelectTypeIncomeTax;

                        }


                    }





                    zakatListData.Add(new ZakatListModel()
                    {
                        referanceNumber = RequestForInstalmentPlanList[i].Fbnum,
                        status = RequestForInstalmentPlanList[i].Status,
                        dueamount = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].TotAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        instalmentAmount = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].DueAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        noOfInstalments = RequestForInstalmentPlanList[i].PlanDur,
                        downpayment = string.Format("{0:N2}", Convert.ToDouble(RequestForInstalmentPlanList[i].DpAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                        dateOfSubmission = submitDate,
                        Fbtyp = zakatbill,
                        statusType = RequestForInstalmentPlanList[i].Fbust,
                        fbNum = RequestForInstalmentPlanList[i].Fbnum


                    });

                }



                InstalmentsCount = RequestForInstalmentPlanList.Count + " " + AppResources.ZakatInstalmentRequestCount;

            }

            await GetZakatRevokList();

            if (ZakatListData.Count > 0)
            {
                NumberOfInstalmentPlans = ZakatListData.Count + " " + AppResources.ZakatInstalmetPlan;
            }
        }


        //public ObservableCollection<ZakatRevokeList.Result> _revokList { get; set; }
        //public ObservableCollection<ZakatRevokeList.Result> RevokList
        //{
        //    get
        //    {
        //        return _revokList;
        //    }
        //    set
        //    {
        //        if (_revokList == value)
        //        {
        //            return;
        //        }
        //        _revokList = value;
        //        RaisePropertyChanged("RevokList");
        //    }
        //}
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
                RaisePropertyChanged("SummarySelectedBillsList");
            }
        }

        public ObservableCollection<ZakatListModel> zakatListData { get; set; }
        public ObservableCollection<ZakatListModel> ZakatListData
        {
            get
            {
                return zakatListData;
            }
            set
            {
                if (zakatListData == value)
                {
                    return;
                }
                zakatListData = value;
                RaisePropertyChanged("ZakatListData");
            }
        }


        private Models.ZakatInstalationModels.SummaryDisplayModel _seletedZakatForm;
        public Models.ZakatInstalationModels.SummaryDisplayModel SeletedZakatForm
        {
            get
            {
                return _seletedZakatForm;
            }
            set
            {
                if (_seletedZakatForm == value) return;
                _seletedZakatForm = value;
                RaisePropertyChanged("SeletedZakatForm");
            }
        }
        private string _SummaryNoOfInstalments = "";
        public string SummaryNoOfInstalments
        {
            get
            {
                return _SummaryNoOfInstalments;
            }
            set
            {
                if (_SummaryNoOfInstalments == value) return;

                _SummaryNoOfInstalments = value;
                RaisePropertyChanged("SummaryNoOfInstalments");
            }
        }
        public bool IsAttachmentsViewEnabled
        {
            get
            {
                return _isAttachmentsViewEnabled;
            }
            set
            {
                if (_isAttachmentsViewEnabled == value) return;

                _isAttachmentsViewEnabled = value;
                RaisePropertyChanged("IsAttachmentsViewEnabled");
            }
        }

        public ObservableCollection<SummaryDisplayModel.Result> attachments { get; set; }
        public ObservableCollection<SummaryDisplayModel.Result> Attachments
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
        private string _enteredOTP = "";
        public string EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                if (_enteredOTP == value) return;

                _enteredOTP = value;
                RaisePropertyChanged("EnteredOTP");
            }
        }
        public void SetOTP()
        {

            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;

        }

        public void BindZakatSummaryData(SummaryDisplayModel zakatRequestDisplayModel, ZakatInstalmentInvListModel invoiceResult)
        {



            SummarySelectedBillsList = new ObservableCollection<ZakatSelectBillModel>();

            foreach (var bill in invoiceResult.d.results)
            {




                string submitDate = "";



                if (bill.DueDt != null)
                {
                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                    string apiDate = @"""" + bill.DueDt + @"""";
                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);



                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);



                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);



                    bill.DueDt = dateStr;



                    string dt1 = string.Empty;
                    string[] dts = null;
                    dts = bill.DueDt.Split('/');
                    dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                    submitDate = dt1;
                }

                if (bill.Abtyp.Equals("ITAX"))
                {
                    bill.Abtyp = AppResources.ZakatInstalmetSelectTypeIncomeTax;
                }
                else if (bill.Abtyp.Equals("ZAKT"))
                {
                    bill.Abtyp = AppResources.FORM5Zakat;
                }


                SummarySelectedBillsList.Add(new ZakatSelectBillModel()
                {
                    billNumber = AppResources.Bill + (SummarySelectedBillsList.Count + 1).ToString("00"),
                    amount = bill.DueAmt,
                    saadNumber = bill.InvNo,
                    taxPeriod = bill.DueDt,
                    isSelected = false,
                    //billType = bill.Abtyp
                    billType = IsZakat ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax



                });
            }
            // SummarySelectedBillsList = summarySelectedBillsList;




            Attachments = new ObservableCollection<SummaryDisplayModel.Result>();




            for (int i = 0; i < zakatRequestDisplayModel.d.AttachSet.results.Count; i++)
            {
                Attachments.Add(zakatRequestDisplayModel.d.AttachSet.results[i]);
            }



            // Attachments = attachments;



            if (Attachments != null && Attachments.Count > 0)
            {
                IsSummaryAttachmentsVisible = true;
            }



            TotalAmount = string.Format("{0:N2}", zakatRequestDisplayModel.d.TotAmt) + " SAR";
            InstalmentAmount = string.Format("{0:N2}", zakatRequestDisplayModel.d.DpAmt) + " SAR";
            SummaryNoOfInstalments = zakatRequestDisplayModel.d.PlanDur;



            if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "01")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "02")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "03")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
            }
            else if (zakatRequestDisplayModel.d.PymntFreq != null && zakatRequestDisplayModel.d.PymntFreq == "04")
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
            }
            else
            {
                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;
            }



        }

        #endregion


        #region APIs

        public async Task GetSummaryDetailsClickedAsync(int index)
        {
            // SummarySelectedBillsList = null;

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {

                    IsLoading = true;
                    // DisplayInfoModel _DisplayRequestData = new DisplayInfoModel();
                    try
                    {
                        var item = zakatListData[index];

                        SeletedZakatForm = await ZakatWebServiceManager.GAZTGetZakatRequestDisplayData(item.referanceNumber, item.Fbtyp);


                      //  PopToRootPage();


                        if (SeletedZakatForm != null && SeletedZakatForm.d != null)
                        {

                            var invoiceResult = await ZakatInstallmentPlanWebServiceManager.GAZTGetZakatInstalmentInvData(SeletedZakatForm.d.Fbnum);

                            if (RequestForRevokeList != null && RequestForRevokeList.Count != 0)
                            {

                                var itemsSource = RequestForRevokeList.Where(w => w.Fbnum.Contains(SeletedZakatForm.d.Fbnum)).ToList();
                                if (itemsSource.Count > 0)
                                {
                                    SelectedFbNum = itemsSource[0].Fbnum;
                                    IsZakatSummaryRevokeVisible = true;
                                }
                                else
                                {
                                    IsZakatSummaryRevokeVisible = false;

                                }

                            }

                            NoOfInstalments = item.noOfInstalments;
                            TotalAmount = string.Format("{0:N2}", item.downpayment) + " SAR";
                            InstalmentAmount = string.Format("{0:N2}", item.dueamount) + " SAR";


                            if (item.frequency == "01")
                            {

                                SelectedFrequencyName = AppResources.ZakatInstalmetMonthly;

                            }
                            else if (item.frequency == "02")
                            {

                                SelectedFrequencyName = AppResources.ZakatInstalmetQuarterly;
                            }
                            else if (item.frequency == "03")
                            {

                                SelectedFrequencyName = AppResources.ZakatInstalmetHalfYearly;
                            }
                            else if (item.frequency == "04")
                            {
                                SelectedFrequencyName = AppResources.ZakatInstalmetYearly;
                            }



                            BindZakatSummaryData(SeletedZakatForm, invoiceResult);
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                        rEQVatInstalmentPlanResponse = await ZakatWebServiceManager.GAZTGetZakatInstalmentPlanRequestList("", "", "");
                        ReqVatInstalmentPlanResponseList = rEQVatInstalmentPlanResponse;


                        PopToRootPage();

                        if (ReqVatInstalmentPlanResponseList != null && ReqVatInstalmentPlanResponseList.d != null)
                        {
                            ZakatListData = new ObservableCollection<ZakatListModel>();



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
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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

                        ZakatInstalmentPlanRequestListModel revokResult = await ZakatWebServiceManager.GAZTGetZakatRevokeList("", "", "");

                        PopToRootPage();


                        if (revokResult != null && revokResult.d != null)
                        {

                            if (ZakatListData == null)
                            {
                                ZakatListData = new ObservableCollection<ZakatListModel>();

                            }


                            RequestForRevokeList = revokResult.d.RevokeListSet.results;

                            IsZakat = Preferences.Get("isZakat", false);
                            if (IsZakat)
                            {

                                RevokeListItem = revokResult.d.WorklistSet.results.Where(x => x.IptypeFg == "NZ").ToList();
                            }
                            else
                            {
                                RevokeListItem = revokResult.d.WorklistSet.results.Where(x => x.IptypeFg == "NI").ToList();

                            }


                            for (int i = 0; i < RevokeListItem.Count; i++)
                            {
                                RevokeListItem[i].DpAmt = string.Format("{0:N2}", RevokeListItem[i].DpAmt) + " " + AppResources.FORM5SAR;
                                RevokeListItem[i].TotAmt = string.Format("{0:N2}", RevokeListItem[i].TotAmt) + " " + AppResources.FORM5SAR;

                                string submitDate = "";
                                if (RevokeListItem[i].SubmitDt != null)
                                {

                                    DateTime dateStart = new DateTime();
                                    CultureInfo cultureInfo = new CultureInfo("ar-SA");
                                    string apiDate = @"""" + RevokeListItem[i].SubmitDt + @"""";
                                    dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                                    GregorianCalendar hjCalendar = new GregorianCalendar();
                                    int year = hjCalendar.GetYear(dateStart);
                                    int month = hjCalendar.GetMonth(dateStart);
                                    int day = hjCalendar.GetDayOfMonth(dateStart);

                                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                                    string dt1 = string.Empty;
                                    string[] dts = null;
                                    dts = dateStr.Split('/');
                                    //dt1 = dts[0] + "-" + UtilityManager.GetShortMonthName(dts[1]) + "-" + dts[2];
                                    dt1 = dts[0] + "-" + dts[1] + "-" + dts[2];
                                    submitDate = dt1;
                                }

                                ZakatListData.Add(new ZakatListModel()
                                {
                                    referanceNumber = RevokeListItem[i].Fbnum,
                                    status = RevokeListItem[i].Status,
                                    dueamount = string.Format("{0:N2}", double.Parse(RevokeListItem[i].TotAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                    instalmentAmount = string.Format("{0:N2}", double.Parse(RevokeListItem[i].DueAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                    noOfInstalments = RevokeListItem[i].PlanDur,
                                    downpayment = string.Format("{0:N2}", double.Parse(RevokeListItem[i].DpAmt.Replace("SAR", "").Replace("ريال سعودي", ""))) + " " + AppResources.ZSAR,
                                    dateOfSubmission = submitDate,
                                    Fbtyp = Preferences.Get("isZakat", false) ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax,
                                    frequency = RevokeListItem[i].PymntFreq,
                                    SelectedType = Preferences.Get("isZakat", false) ? AppResources.ZakatInstalmetSelectTypeZakat : AppResources.ZakatInstalmetSelectTypeIncomeTax,
                                    statusType = RevokeListItem[i].Fbust,
                                    fbNum = RevokeListItem[i].Fbnum
                                });

                                if (ZakatListData.Count > 0)
                                {
                                    NumberOfInstalmentPlans = ZakatListData.Count + " " + AppResources.ZakatInstalmetPlan;
                                }

                            }

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
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task validateZakatRevoke()
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

                        ZakatRevokeValidateModel revokeResult = await WebServiceManager.GAZTGetZakatRevokeValidate(SelectedFbNum);

                        PopToRootPage();


                        if (revokeResult != null && revokeResult.d != null)
                        {
                            if (revokeResult.d.Valid)
                            {

                                await SendOTPToRegisterMobileNumber(SelectedFbNum, "");


                            }


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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }




        public async Task getZakatRevokeData()
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
                    ZakatInstalments = null;
                    try
                    {
                        ZakatInstalments = await ZakatInstallmentPlanWebServiceManager.GetZakatInstalmentRevokePostData(SelectedFbNum);


                        if (ZakatInstalments != null)
                        {

                            var isrevoked = await RevokeSubmitClicked();

                            if (isrevoked != null && isrevoked.d != null)
                            {

                                Preferences.Set("IsFromRevok", true);

                                Preferences.Set("RevokeRef", isrevoked.d.Fbnum.ToString());


                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Application.Current.MainPage.Navigation.PushAsync(new ZakatInstalmentPlanSuccessPage());

                                });
                            }


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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async Task<ZakatInstalmentPlanRevokeResponse> RevokeSubmitClicked()
        {
            ZakatInstalmentPlanRevokeResponse response = new ZakatInstalmentPlanRevokeResponse();
            ZakatInstalmentPlanRevokeRequest request = new ZakatInstalmentPlanRevokeRequest();

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                var item = ZakatInstalments.d;


                request.AccMethod = "A";
                request.AltMobNo = item.AltMobNo;
                request.DataVersion = item.DataVersion;
                request.DecCb = item.DecCb;
                request.DpAmt = item.DpAmt.Replace(" SAR", "").Replace(",", "");
                request.Email = item.Email;
                request.Euser = item.Euser;
                request.Fbguid = item.Fbguid;
                request.FbnumIprr = item.FbnumIprr;
                request.Fbnum = item.Fbnum;
                request.FormGuid = item.FormGuid;
                request.InstReqFor = item.InstReqFor;
                request.InstReqReason = item.InstReqReason;
                request.Langz = GetLangZParameter();
                request.MobNo = item.MobNo;
                request.Officer = item.Officer;
                request.OffAmt = item.OffAmt;
                request.OffPlanDur = "";
                request.OffPymntFreq = "";
                request.Formproc = item.Formproc;
                request.PaymtDt = item.PaymtDt;
                request.PenlAmt = item.PenlAmt;
                request.Periodkey = item.Periodkey;
                request.PlanDur = item.PlanDur;
                request.PymntFreq = item.PymntFreq;
                request.ReturnId = "";
                request.Sopbel = item.Sopbel;
                request.Status = "E0045";
                request.StepNumber = item.StepNumber;
                request.SuAmt = item.SuAmt;
                request.SuAmtFg = item.SuAmtFg;
                request.Tin = item.Tin;
                request.TinNm = item.TinNm;
                request.TotAmt = item.TotAmt.Replace(" SAR", "").Replace(",", "");
                request.TxnTp = "CRE_IPRR";
                request.Waers = item.Waers;
                request.AttachSet = new AttachSetResults[0];
                request.FnDtlSet = new Array[0];
                request.insPlan_OffSet = new Array[0];
                request.insPlanSet = new Array[0];
                request.invDtlsSet = new ZakatInvoicesResult[0];
                request.retmsgSet = new Array[0];
                request.UserTyp = "TP";

                request.Operation = "69";

                NotesSet notes = new NotesSet();
                if (NoteEditor != null)
                {

                    notes.Tdline = NoteEditor.ToString();

                }
                else
                {
                    notes.Tdline = "";

                }



                Metadata _metdata = new Metadata();
                _metdata.uri = "HTTP://SAPECCDEV.MYDZIT.GOV.SA:8000/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/NotesSet('001')";
                _metdata.type = "ZDP_IPRF_M_SRV.Notes";
                _metdata.id = "HTTP://SAPECCDEV.MYDZIT.GOV.SA:8000/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/NotesSet('001')";

                notes.__metadata = _metdata;
                notes.AttByz = "TP";
                notes.ByGpartz = App.LoginDataRetrieved.TIN;

                notes.DataVersionz = "00000";
                notes.ElemNo = 0;
                notes.Erfusrz = "";
                notes.Lineno = 1;
                notes.Noteno = "1";
                notes.Notenoz = "1";
                notes.Rcodez = "IPRR_NOTES";
                notes.Refnamez = "";
                notes.Tdformat = "";
                notes.XInvoicez = "";
                notes.XObsoletez = "";

                var noteset = new NotesSet[1];
                noteset[0] = notes;
                request.NotesSet = noteset;

                response = await ZakatInstallmentPlanWebServiceManager.SaveZakatInstalmentRevokeData(request);
                PopToRootPage();
                if (response != null)
                {
                    try
                    {


                        IsLoading = false;
                        return response;

                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        return null;

                    }
                }
                IsLoading = false;
                return response;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();

                });
                return response;
            }

            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                return response;
            }

        }

        #endregion

        private static string GetLangZParameter()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
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

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }
        private async Task SendOTPToRegisterMobileNumber(string fbNumber, string OTPCode)
        {

            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                    Enabled = false;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();
                        ZakatRevokeSendSMSModel revokeOTP = await WebServiceManager.GAZTZakatRevokeSendOTP(fbNumber, OTPCode);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (revokeOTP.d != null)
                        {
                            var OTPItem = revokeOTP.d.results[0];




                            if (OTPCode.Length == 0)
                            {
                                EnableOTPPage();
                                ContinueButtonEnability = true;
                                IsResendOTPEnabled = false;
                                StartOTPTimer();
                            }
                            else
                            {
                                if (OTPItem.ValidSms)
                                {
                                    otpTimer.Stop();
                                    await getZakatRevokeData();

                                }
                                else
                                {


                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessage(AppResources.InvalidOTP, AppResources.Information);

                                    });



                                }


                            }







                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                    // UserIDLayoutVisibility = true;
                });
            }
        }

        public async Task ValidateOTPAsync()
        {

            await SendOTPToRegisterMobileNumber(SelectedFbNum, EnteredOTP);
        }
    }
}
