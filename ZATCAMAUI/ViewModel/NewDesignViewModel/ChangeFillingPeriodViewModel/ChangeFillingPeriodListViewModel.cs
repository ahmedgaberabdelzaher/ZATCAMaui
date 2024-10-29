using System.Collections.ObjectModel;
using System.Windows.Input;

using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ChageFillingPeriodModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{

    public class ChangeFillingPeriodListViewModel : BaseViewModel
    {
        public ICommand GoBackClick { get; set; }
        public ICommand MyRequestsButtonTapped { get; set; }
        public ICommand RequestItemTapCommand { get; set; }
        public ICommand DownloadAcknowledgementCommand { get; set; }
        public ICommand OnAppearingChangeFillingPeriodListCommand { get; set; }

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
                OnPropertyChanged("IsBackVisible");
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
                OnPropertyChanged("IsDobVisible");
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
                OnPropertyChanged("IsMyRequestsViewEnabled");
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
                OnPropertyChanged("IsSummaryViewEnabled");
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
                OnPropertyChanged("ContactPersonName");
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
                OnPropertyChanged("CurrentFrequency");
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
                OnPropertyChanged("NewFrequency");
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
                OnPropertyChanged("EffectiveDatePicked");
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
                OnPropertyChanged("IDNumber");
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
                OnPropertyChanged("PickedDate");
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
                OnPropertyChanged("IDType");
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
                OnPropertyChanged("MyRequestsListViewData");
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
                OnPropertyChanged("YearsattachmentsListViewData");
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
                OnPropertyChanged("MonthsattachmentsListViewData");
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
                OnPropertyChanged("OtherAttachmentsListViewData");
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


            GoBackClick = new Command(() =>
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
                IsLoading = true;
                App.selectedVatFillingItem = "";
                App.selectedVATItemFbust = "";
                await _navigationService.NavigateTo(App.ChangeFillingPeriodPageView);
                IsLoading = false;
            });

            DownloadAcknowledgementCommand = new Command(async () =>
            {
                if (vATChangingSummaryData.Fbnum != null)
                {

                    string downloadurl = ZATCAConstants.downloadFile + vATChangingSummaryData.Fbnum;
                    await _navigationService.NavigateTo(App.PdfView, downloadurl);
                }

            });

            OnAppearingChangeFillingPeriodListCommand = new Command(async () =>
            {
                try
                {
                    ResetData();
                    await GetVATChangeFillingList();
                }
                catch (Exception)
                {

                }

            });

            RequestItemTapCommand = new Command<object>(async (obj) =>
            {

                try
                {
                    var item = (obj as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as VATChangeFillingListModel.ChangeFillingFrequency;

                    if (item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013")
                    {
                        App.selectedVatFillingItem = item.Fbnum;
                        App.selectedVATItemFbust = item.Fbust;
                        await _navigationService.NavigateTo(App.ChangeFillingPeriodPageView);
                    }
                    else
                    {

                        await GetVATChangeFillingSummary(item);

                        if (vATChangingSummaryData != null)
                        {

                            EnableSummaryView();

                        }

                    }
                }
                catch (Exception)
                {


                }
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
                OnPropertyChanged("vATChangingSummaryData");
            }
        }


        private bool _isLoading = false;
        public new bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;

                _isLoading = value;
                OnPropertyChanged("IsLoading");
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
                IsLoading = true;
                try
                {

                    var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingList(App.LoginDataRetrieved.TIN);
                    if (resultData != null)
                    {
                        var changeFilingFrequencyDataList = resultData.d.ASSLISTSet.Where(x => x.Fbtyp.ToUpper() == "TPCV".ToUpper()).ToList();

                        var myRequestsListViewData = new ObservableCollection<VATChangeFillingListModel.ChangeFillingFrequency>();

                        foreach (var changeFilingFrequencyData in changeFilingFrequencyDataList)
                        {
                            myRequestsListViewData.Add(changeFilingFrequencyData);
                        }

                        MyRequestsListViewData = myRequestsListViewData;

                    }
                    else
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    IsLoading = false;
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();

                }
                IsLoading = false;

            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();

            }
            catch (Exception)

            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        public async Task GetVATChangeFillingSummary(VATChangeFillingListModel.ChangeFillingFrequency item)
        {
            try
            {
                IsLoading = true;
                var resultData = await VATChangeFillingWebServiceManager.GAZTGetVATChangeFillingSummary(item.Fbnum, item.Fbust);
                if (resultData != null && resultData.d != null)
                {
                    vATChangingSummaryData = new VATChangeFillingSummaryModel.VATChangingSummaryData();
                    vATChangingSummaryData.Attchk = resultData.d.Attchk;
                    vATChangingSummaryData.CureentF = resultData.d.CureentF;
                    vATChangingSummaryData.FilingF = resultData.d.FilingF;
                    //Persl i.e 21JA means January 2021
                    vATChangingSummaryData.Persl = resultData.d.CPersl;
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
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                }
                IsLoading = false;
            }
            catch (GAZTVATChangeFillingPeriodException ex)
            {

                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();

            }
            catch (Exception)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            }
        }

        private void PopulateAttachentsListData(List<Attachment> dAttachSet)
        {
            var yearsAttachmentsListViewData = new ObservableCollection<Attachment>();
            var monthsAttachmentsListViewData = new ObservableCollection<Attachment>();
            var othersAttachmentsListViewData = new ObservableCollection<Attachment>();

            foreach (var attachment in dAttachSet)
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

