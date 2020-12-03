using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using static GAZT.ErrorMessage;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class ZakatRegistrationDetailsListPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand DeregisterTinTapped { get; set; }

        #endregion

        public ZakatRegistrationDetailsListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            IsArabic = App.IsArabic;

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            DeregisterTinTapped = new Command(GetNewTinDeregistrationDataCliked);
        }

        public TinDeregistrationResponseModel _zakatDeregResponseData { get; set; }
        public TinDeregistrationResponseModel ZakatDeregResponseData
        {
            get
            {
                return _zakatDeregResponseData;
            }

            set
            {
                _zakatDeregResponseData = value;
                RaisePropertyChanged("ZakatDeregResponseData");
            }
        }

        public List<ZakatDeregistrationDetailsListModel> _zakatRegListData { get; set; }
        public List<ZakatDeregistrationDetailsListModel> ZakatRegListData
        {
            get
            {
                return _zakatRegListData;
            }

            set
            {
                _zakatRegListData = value;
                RaisePropertyChanged("ZakatRegListData");
            }
        }

        private bool _isArabic = true;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                _isArabic = value;
                RaisePropertyChanged("IsArabic");
            }
        }

        public void ZAKATAmendOrUpdateClicked()
        {
            _navigationService.NavigateTo(App.EstablishmentAmendUpdatePage);
        }
        public void PopulateZakatRegListData()
        {
            List<ZakatDeregistrationDetailsListModel> tempZakatRegListData = new List<ZakatDeregistrationDetailsListModel>();
            string fileImage = string.Empty;
            if(App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            
            if (App.LoginDataRetrieved.ZkReg == "X"||App.LoginDataRetrieved.VtReg == "X")
            {   
                    if (App.LoginDataRetrieved.ZkReg == "X") {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMTaxpayerDetails,
                        ZDImageSource = "vat_ic_taxpayerDetail",
                        ArrowImageSource = fileImage
                    });
                    }

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMOutlets,
                            ZDImageSource = "establishments",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMFinancialDetails,
                            ZDImageSource = "details",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMVATRegistrationDetails,
                        ZDImageSource = "details",
                        ArrowImageSource = fileImage
                    });
                }

                if (App.LoginDataRetrieved.ZkReg == "X")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMAmend,
                        ZDImageSource = "registration.png",
                        ArrowImageSource = fileImage
                    });
                }

                if (App.LoginDataRetrieved.VtReg == "X")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMAmendmentOfVATRegistration,
                        ZDImageSource = "registration.png",
                        ArrowImageSource = fileImage
                    });
                }
                /*tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                {
                    ZDTitle = AppResources.Registrations,
                    ZDImageSource = "registration.png",
                    ArrowImageSource = fileImage
                });*/

                if (App.LoginDataRetrieved.VtReg == "X")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMVATDeregistration,
                        ZDImageSource = "deregistration",
                        ArrowImageSource = fileImage
                    });
                }

                //TODO  Add this after  approval

                if (App.LoginDataRetrieved.ZkReg == "X")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.DBSMTINDeregistration,
                        ZDImageSource = "deregistration",
                        ArrowImageSource = fileImage
                    });
                }

            }
            else if (App.LoginDataRetrieved.ZkReg == "U")
            {
                tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                {
                    ZDTitle = AppResources.TPUpdate,
                    ZDImageSource = "sf_ic_Paid.png",
                    ArrowImageSource = fileImage

                });
            }
            ZakatRegListData = new List<ZakatDeregistrationDetailsListModel>(tempZakatRegListData);
        }

        public async void GetNewTinDeregistrationDataCliked()
        {
            try
            {

                ZakatDeregResponseData = new TinDeregistrationResponseModel();
                ZakatDeregResponseData.Approvez = "";
                ZakatDeregResponseData.Rejectz = "";

                ZakatDeregResponseData = await WebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregResponseData);

                //VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                //IbanData = new List<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);
                //VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);

                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                _navigationService.NavigateTo(App.TINDeregistrationPageView, ZakatDeregResponseData);
            }
            catch (InternetException ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                    });

                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (GAZTErrorException ex)
            {
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                string message = ex.Message;

                try
                {
                    if (message.Contains("112") || message.Contains("206"))
                    {
                        string newMessage = AppResources.TinDeregistrationChangeApplicationInDraftError;

                        if (message.Contains("112"))
                        {
                            newMessage = AppResources.TinDeregistrationPermitApplicationInDraftError;
                        }
                        else if (message.Contains("206"))
                        {
                            newMessage = AppResources.TinDeregistrationChangeApplicationInDraftError;
                        }

                        await _dialogService.ShowMessage(newMessage, AppResources.Information, AppResources.ZYes, AppResources.ZNo, (async (bool isConfirmed) =>
                         {
                             if (isConfirmed == true)
                             {
                                 try
                                 {
                                     if (ZakatDeregResponseData == null)
                                     {
                                         ZakatDeregResponseData = new TinDeregistrationResponseModel();
                                     }

                                     if (message.Contains("206"))
                                     {
                                         ZakatDeregResponseData.Approvez = "";
                                         ZakatDeregResponseData.Rejectz = "X";
                                     }
                                     else if (message.Contains("112"))
                                     {

                                         ZakatDeregResponseData.Approvez = "X";
                                         ZakatDeregResponseData.Rejectz = "";
                                     }

                                     await Task.Run(() =>
                                     {
                                         App.DisplayProgressView();
                                     });

                                     ZakatDeregResponseData = await WebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregResponseData);
                                     //_navigationService.NavigateTo(App.TINDeregistrationPageView, ZakatDeregResponseData);

                                     await Task.Run(() =>
                                     {
                                         App.HideProgressView();
                                     });
                                 }
                                 catch (InternetException iex)
                                 {
                                     await Task.Run(() =>
                                     {
                                         App.HideProgressView();
                                     });

                                     try
                                     {
                                         Device.BeginInvokeOnMainThread(async () =>
                                         {
                                             await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                                         });

                                     }
                                     catch (Exception mex)
                                     {
                                         Console.WriteLine(mex.Message);
                                     }
                                 }

                             }
                         }));
                    }
                    else
                    {
                        await _dialogService.ShowMessage(message, AppResources.Information);
                    }
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    });
                }
                catch (Exception mex)
                {
                    Console.WriteLine(mex.Message);
                }
            }
        }

    }
}
