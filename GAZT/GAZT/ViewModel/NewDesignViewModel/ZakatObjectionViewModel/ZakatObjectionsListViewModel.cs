using EGAZT.Models.ZakatObjectionsModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
   public class ZakatObjectionsListViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #region Commands

        public ICommand ReqInstalmentBtnTapped { get; set; }

        #endregion

        public ZakatObjectionsListViewModel(INavigationService navigationService, IDialogService dialogService)
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


            ReqInstalmentBtnTapped = new Command(this.ReqInstalmentBtnClickedAsync);

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

        public async void ReqInstalmentBtnClickedAsync()
        {
            _navigationService.NavigateTo(App.ZakatObjectionPageView);
        }


        #region API Integration
        public async Task ZAKATObjectionList()
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
                    ZakatObjectionListModel _ZAKATObjectionList = new ZakatObjectionListModel();
                    try
                    {
                        _ZAKATObjectionList = await WebServiceManager.GAZTGetZAKATObjectionList();

                        if (_ZAKATObjectionList != null && _ZAKATObjectionList.d != null)
                        {
                            var ObjectionList = _ZAKATObjectionList.d.ListSet.results.ToList();
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

        public async Task ZakatObjectionData(string fbnum)
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
                    ZAKATObjectionDataModel _ZAKATObjectionData = new ZAKATObjectionDataModel();
                    //ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel _ZAKATObjectionReviewReturn = new ZAKATObjectionReturnModel.ZAKATObjectionReviewReturnModel();
                    //try
                    //{
                    //    _ZAKATObjectionData = await WebServiceManager.GAZTGetZakatObjectionData(fbnum);

                    //    if (_ZAKATObjectionData != null && _ZAKATObjectionData.d != null)
                    //    {
                    //        _ZAKATObjectionReviewReturn.Agree = _ZAKATObjectionData.d.AAgree;
                    //        _ZAKATObjectionReviewReturn.TaxPayerName = _ZAKATObjectionData.d.ATpName;
                    //        _ZAKATObjectionReviewReturn.Branch = _ZAKATObjectionData.d.ABranch;
                    //        //_ZAKATObjectionReviewReturn.Address = _ZAKATObjectionData.d..results[0].BuildingNo + "," +
                    //        //_ZAKATObjectionReviewReturn.ElectronicMail = _ZAKATObjectionData.d.;
                    //        //_ZAKATObjectionReviewReturn.TelephoneNo = _ZAKATObjectionData.d;
                    //        //_ZAKATObjectionReviewReturn.FaxNo = _ZAKATObjectionData.d;
                    //        //_ZAKATObjectionReviewReturn.RegerenceNo = _ZAKATObjectionData.d;
                    //        _ZAKATObjectionReviewReturn.RegerenceNo = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ARefNo;
                    //        _ZAKATObjectionReviewReturn.AssessmentYear = _ZAKATObjectionData.d.zobj_itemsSet.results[0].AAssnmtYr;
                    //        _ZAKATObjectionReviewReturn.PeriodFrom = _ZAKATObjectionData.d.zobj_itemsSet.results[0].APeriodFrom;
                    //        _ZAKATObjectionReviewReturn.PeriodTo = _ZAKATObjectionData.d.zobj_itemsSet.results[0].APeriodTo;
                    //        _ZAKATObjectionReviewReturn.TaxType = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ATaxTy;
                    //        _ZAKATObjectionReviewReturn.Currency = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ACurr;
                    //        _ZAKATObjectionReviewReturn.AssessmentAmountGAZT = _ZAKATObjectionData.d.zobj_itemsSet.results[0].AAssnmtAmt;
                    //        _ZAKATObjectionReviewReturn.RevisedAmount = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ARevAmt;
                    //        _ZAKATObjectionReviewReturn.DisputeAmount = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ADisputeAmt;
                    //        _ZAKATObjectionReviewReturn.ReturnDetails = _ZAKATObjectionData.d.zobj_itemsSet.results[0].ARetDet;
                    //        //_ZAKATObjectionReviewReturn.ObjectionReasons= _ZAKATObjectionData.d.zobj_itemsSet.results[0];
                    //        //_ZAKATObjectionReviewReturn.PaymentAmount= _ZAKATObjectionData.d.;
                    //    }

                    //    else
                    //    {
                    //        Device.BeginInvokeOnMainThread(async () =>
                    //        {
                    //            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //            _navigationService.GoBack();
                    //        });
                    //    }
                    //    IsLoading = false;
                    //}
                    //catch (GAZTVATRegistrationInProcessException ex)
                    //{
                    //    throw ex;
                    //}
                    //catch (InternetException ex)
                    //{
                    //    Device.BeginInvokeOnMainThread(async () =>
                    //    {
                    //        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //        IsLoading = false;
                    //        _navigationService.GoBack();
                    //    });
                    //    //   await Task.Run(() =>
                    //    //   {
                    //    //  });
                    //}
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
    }
}
