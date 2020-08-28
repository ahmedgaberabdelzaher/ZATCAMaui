using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.ChageFillingPeriodModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel
{
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
                _isBackVisible = value;
                RaisePropertyChanged("IsBackVisible");
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
                _isSummaryViewEnabled = value;
                RaisePropertyChanged("IsSummaryViewEnabled");
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
            GoBackClick = new Command(async () =>
            {
                EnableFrequencyListView();
            });

            CloseClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            MyRequestsButtonTapped = new Command(async () =>
            {
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
                        if (resultData != null && resultData.d.ASSLISTSet.results.Count > 0)
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
                            vATChangingSummaryData.NOTESSet = resultData.d.NOTESSet;


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
    }
}

