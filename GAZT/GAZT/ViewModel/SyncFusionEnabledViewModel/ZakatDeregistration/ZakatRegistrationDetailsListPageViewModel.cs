using System;
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

            PopulateZakatRegListData();
            DeregisterTinTapped = new Command(GetNewTinDeregistrationDataCliked);
        }

        public TinDeregistrationResponseModel _zakatDeregDataData { get; set; }
        public TinDeregistrationResponseModel ZakatDeregDataData
        {
            get
            {
                return _zakatDeregDataData;
            }

            set
            {
                _zakatDeregDataData = value;
                RaisePropertyChanged("ZakatDeregDataData");
            }
        }

        public ObservableCollection<ZakatDeregistrationDetailsListModel> _zakatRegListData { get; set; }
        public ObservableCollection<ZakatDeregistrationDetailsListModel> ZakatRegListData
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

        public void PopulateZakatRegListData()
        {
            ZakatRegListData = new ObservableCollection<ZakatDeregistrationDetailsListModel>();
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.ZZTaxPayerDetails,
                ZDImageSource = "vat_ic_taxpayerDetail",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.TinDeregistrationRegistrationOutlets,
                ZDImageSource = "establishments",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.ZZZZVATREFinancialDetails,
                ZDImageSource = "details",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.TinDeregistration,
                ZDImageSource = "deregistration",
            });
        }

        public async void GetNewTinDeregistrationDataCliked()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                ZakatDeregDataData = new TinDeregistrationResponseModel();
                ZakatDeregDataData.Approvez = "";
                ZakatDeregDataData.Rejectz = "";

                ZakatDeregDataData = await WebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregDataData);

                //VatRefundsIbanDataModel = await WebServiceManager.GAZTGetVATRefundGetIbanData("");
                //IbanData = new ObservableCollection<VarRefundIbanDataModelMetadataResult>(VatRefundsIbanDataModel.IbanSet.Results);
                //VatRefundsDisplayDataModel.Rfamt = VatRefundsDisplayDataModel.Rfamt.Replace("-", string.Empty);
               
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });

                _navigationService.NavigateTo(App.TINDeregistrationPageView);
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
                    if(message.Contains("206"))
                    {
                        if(ZakatRegListData == null)
                        {
                            ZakatDeregDataData = new TinDeregistrationResponseModel();
                            ZakatDeregDataData.Approvez = "";
                            ZakatDeregDataData.Rejectz = "X";
                        }
                        else
                        {
                            ZakatDeregDataData.Approvez = "";
                            ZakatDeregDataData.Rejectz = "X";
                        }
                        message = AppResources.TinDeregistrationChangeApplicationInDraftError;
                      
                        await _dialogService.ShowMessage(message, AppResources.Information, AppResources.ZYes, AppResources.ZNo, (async(bool isConfirmed) =>
                        {
                            if(isConfirmed == true)
                            {
                                ZakatDeregDataData = await WebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregDataData);
                                _navigationService.NavigateTo(App.TINDeregistrationPageView);
                            }
                        }));
                    }
                    else if(message.Contains("112"))
                    {
                        if (ZakatRegListData == null)
                        {
                            ZakatDeregDataData = new TinDeregistrationResponseModel();
                            ZakatDeregDataData.Approvez = "X";
                            ZakatDeregDataData.Rejectz = "";
                        }
                        else
                        {
                            ZakatDeregDataData.Approvez = "X";
                            ZakatDeregDataData.Rejectz = "";
                        }
                        message = AppResources.TinDeregistrationChangeApplicationInDraftError;

                        await _dialogService.ShowMessage(message, AppResources.Information, AppResources.ZYes, AppResources.ZNo, (async (bool isConfirmed) =>
                        {
                            if (isConfirmed == true)
                            {
                                ZakatDeregDataData = await WebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregDataData);
                                _navigationService.NavigateTo(App.TINDeregistrationPageView);
                            }
                        }));
                    }
                    await _dialogService.ShowMessage(message, AppResources.Information);
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
