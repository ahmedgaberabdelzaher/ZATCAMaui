using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{
    [Preserve(AllMembers = true)]
    public class ChangeFillingPeriodListViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }
        public ICommand MyRequestsButtonTapped { get; set; }

        private bool _isBackVisible = false;
        public bool IsBackVisible
        {
            get
            {
                return _isBackVisible;
            }
            set
            {
                if (_isBackVisible == value) return;
                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
            }
        }

        private bool _isDobVisible = false;
        public bool IsDobVisible
        {
            get
            {
                return _isDobVisible;
            }
            set
            {
                if (_isDobVisible == value) return;

                _isDobVisible = value;
                RaisePropertyChanged("IsDobVisible");
            }
        }

        private bool _isMyRequestsViewEnabled = false;
        public bool IsMyRequestsViewEnabled
        {
            get
            {
                return _isMyRequestsViewEnabled;
            }
            set
            {
                if (_isMyRequestsViewEnabled == value) return;

                _isMyRequestsViewEnabled = value;
                RaisePropertyChanged("IsMyRequestsViewEnabled");
            }
        }

        private bool _isSummaryViewEnabled = false;
        public bool IsSummaryViewEnabled
        {
            get
            {
                return _isSummaryViewEnabled;
            }
            set
            {
                if (_isSummaryViewEnabled == value) return;

                _isSummaryViewEnabled = value;
                RaisePropertyChanged("IsSummaryViewEnabled");
            }
        }

        private string _contactPersonName = "";

        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set
            {
                if (_contactPersonName == value) return;

                _contactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        private string _currentFrequency = "";
        public string CurrentFrequency
        {
            get
            {
                return _currentFrequency;
            }
            set
            {
                if (_currentFrequency == value) return;

                _currentFrequency = value;
                RaisePropertyChanged("CurrentFrequency");
            }
        }

        private string _newFrequency = "";
        public string NewFrequency
        {
            get
            {
                return _newFrequency;
            }
            set
            {
                if (_newFrequency == value) return;

                _newFrequency = value;
                RaisePropertyChanged("NewFrequency");
            }
        }

        private string _effectiveDatePicked = "";

        public string EffectiveDatePicked
        {
            get { return _effectiveDatePicked; }
            set
            {
                if (_effectiveDatePicked == value) return;

                _effectiveDatePicked = value;
                RaisePropertyChanged("EffectiveDatePicked");
            }
        }

        private string _idNumber = "";

        public string IDNumber
        {
            get { return _idNumber; }
            set
            {
                if (_idNumber == value) return;

                _idNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }

        private string _pickedDate = "";

        public string PickedDate
        {
            get { return _pickedDate; }
            set
            {
                if (_pickedDate == value) return;

                _pickedDate = value;
                RaisePropertyChanged("PickedDate");
            }
        }

        private string _idType = "";

        public string IDType
        {
            get { return _idType; }
            set
            {
                if (_idType == value) return;

                _idType = value;
                RaisePropertyChanged("IDType");
            }
        }

        public ObservableCollection<VATChangeFillingListModel.ChangeFillingFrequency> _myRequestsListViewData { get; set; }

        public ObservableCollection<VATChangeFillingListModel.ChangeFillingFrequency> MyRequestsListViewData
        {
            get { return _myRequestsListViewData; }

            set
            {
                if (_myRequestsListViewData == value)
                {
                    return;
                }

                _myRequestsListViewData = value;
                RaisePropertyChanged("MyRequestsListViewData");
            }
        }

        public ObservableCollection<Attachment> yearsattachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> YearsattachmentsListViewData
        {
            get { return yearsattachmentsListViewData; }

            set
            {
                if (yearsattachmentsListViewData == value)
                {
                    return;
                }

                yearsattachmentsListViewData = value;
                RaisePropertyChanged("YearsattachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> monthsattachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> MonthsattachmentsListViewData
        {
            get { return monthsattachmentsListViewData; }

            set
            {
                if (monthsattachmentsListViewData == value)
                {
                    return;
                }

                monthsattachmentsListViewData = value;
                RaisePropertyChanged("MonthsattachmentsListViewData");
            }
        }

        public ObservableCollection<Attachment> otherAttachmentsListViewData { get; set; }

        public ObservableCollection<Attachment> OtherAttachmentsListViewData
        {
            get { return otherAttachmentsListViewData; }

            set
            {
                if (otherAttachmentsListViewData == value)
                {
                    return;
                }

                otherAttachmentsListViewData = value;
                RaisePropertyChanged("OtherAttachmentsListViewData");
            }
        }

        private Dictionary<string, string> IDTypeDictionary = new Dictionary<string, string>
        {
            {"ZS0001",AppResources.VFCNationalID},
            {"ZS0002",AppResources.VFCIqamaID},
            {"ZS0003",AppResources.VFCGCCID},
        };

        public ChangeFillingPeriodListViewModel(INavigationService navigationService, IDialogService dialogService) : base(
            navigationService, dialogService)
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
          

            CloseClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });


            GoBackClick = new Command(async () =>
            {
                if (IsMyRequestsViewEnabled)
                {
                    _navigationService.GoBack();
                }
                else
                {
                    EnableFrequencyListView();
                }
            });
            MyRequestsButtonTapped = new Command(async () =>
            {

                App.selectedVatFillingItem = "";
                App.selectedVATItemFbust = "";
                _navigationService.NavigateTo(App.ChangeFillingPeriodPageView);
            });
        }

        private VATChangeFillingSummaryModel.VATChangingSummaryData _vATChangeFillingSummaryData;
        public VATChangeFillingSummaryModel.VATChangingSummaryData vATChangingSummaryData
        {
            get
            {
                return _vATChangeFillingSummaryData;
            }
            set
            {
                if (_vATChangeFillingSummaryData == value) return;

                _vATChangeFillingSummaryData = value;
                RaisePropertyChanged("vATChangingSummaryData");
            }
        }


        private bool _isLoading = false;
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

        public void ResetData()
        {
            EnableFrequencyListView();
        }

        public void EnableFrequencyListView()
        {
            IsMyRequestsViewEnabled = true;
            IsSummaryViewEnabled = false;
            IsBackVisible = false;
        }

        public void EnableSummaryView()
        {
            IsMyRequestsViewEnabled = false;
            IsSummaryViewEnabled = true;
            IsBackVisible = true;
        }

        public async Task GetVATChangeFillingList()
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

                        var resultData = await WebServiceManager.GAZTGetVATChangeFillingList(App.LoginDataRetrieved.TIN);
                        if (resultData != null)
                        {
                            var changeFilingFrequencyDataList = resultData.d.ASSLISTSet.results.Where(x => x.Fbtyp.ToUpper() == "TPCV".ToUpper()).ToList();

                            var myRequestsListViewData = new ObservableCollection<VATChangeFillingListModel.ChangeFillingFrequency>();

                            foreach (var changeFilingFrequencyData in changeFilingFrequencyDataList)
                            {
                                myRequestsListViewData.Add(changeFilingFrequencyData);
                            }

                            MyRequestsListViewData = myRequestsListViewData;

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
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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

        public async Task GetVATChangeFillingSummary(VATChangeFillingListModel.ChangeFillingFrequency item)
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


                        var resultData = await WebServiceManager.GAZTGetVATChangeFillingSummary(item.Fbnum, item.Fbust);
                        if (resultData != null && resultData.d != null)
                        {
                            vATChangingSummaryData = new VATChangeFillingSummaryModel.VATChangingSummaryData();
                            vATChangingSummaryData.Attchk = resultData.d.Attchk;
                            vATChangingSummaryData.CureentF = resultData.d.CureentF;
                            vATChangingSummaryData.FilingF = resultData.d.FilingF;
                            //Persl i.e 21JA means January 2021
                            vATChangingSummaryData.Persl = resultData.d.Persl;
                            vATChangingSummaryData.Decfg = resultData.d.Decfg;
                            vATChangingSummaryData.Decname = resultData.d.Decname;
                            vATChangingSummaryData.DecidNo = resultData.d.DecidNo;
                            vATChangingSummaryData.DecidTy = resultData.d.DecidTy;
                            vATChangingSummaryData.AttachmentList = resultData.d.ATTACHSet;
                            vATChangingSummaryData.Fbnum = resultData.d.Fbnumz;
                            //vATChangingSummaryData.NOTESSet = resultData.d.NOTESSet;

                            CurrentFrequency = vATChangingSummaryData.CureentF;
                            NewFrequency = vATChangingSummaryData.FilingF;
                            EffectiveDatePicked = vATChangingSummaryData.Persl;
                            IDType = IDTypeDictionary[vATChangingSummaryData.DecidTy];
                            if (vATChangingSummaryData.DecidTy == "ZS0001" || vATChangingSummaryData.DecidTy == "ZS0002")
                            {
                                IsDobVisible = true;
                            }
                            else
                            {
                                IsDobVisible = false;
                            }
                            IDNumber = vATChangingSummaryData.DecidNo;
                            PickedDate = vATChangingSummaryData.Decfg;
                            ContactPersonName = vATChangingSummaryData.Decname;

                            PopulateAttachentsListData(resultData.d.ATTACHSet);
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
                    catch (GAZTVATChangeFillingPeriodException ex)
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
            catch (GAZTVATChangeFillingPeriodException ex)
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

        private void PopulateAttachentsListData(VATChangeFillingSummaryModel.ATTACHSet dAttachSet)
        {
            var yearsAttachmentsListViewData = new ObservableCollection<Attachment>();
            var monthsAttachmentsListViewData = new ObservableCollection<Attachment>();
            var othersAttachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (var attachment in dAttachSet.results)
            {
                if (attachment.Dotyp == "ZTPA")
                {
                    yearsAttachmentsListViewData.Add(attachment);
                }
                else if (attachment.Dotyp == "ZTPB")
                {
                    monthsAttachmentsListViewData.Add(attachment);
                }
                else if (attachment.Dotyp == "ZTPC")
                {
                    othersAttachmentsListViewData.Add(attachment);
                }

               
            }

            YearsattachmentsListViewData = yearsAttachmentsListViewData;
            MonthsattachmentsListViewData = monthsAttachmentsListViewData;
            OtherAttachmentsListViewData = othersAttachmentsListViewData;
        }
    }
}

