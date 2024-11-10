using System.Windows.Input;

using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{

    public class ZakatRegistrationDetailsListPageViewModel : BaseViewModel
    {

        #region Variable
        public ICommand DeregisterTinTapped { get; set; }

        #endregion

        public ZakatRegistrationDetailsListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            IsArabic = App.IsArabic;


            DeregisterTinTapped = new Command(async() => await GetNewTinDeregistrationDataCliked());
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
                if (_zakatDeregResponseData == value) return;
                _zakatDeregResponseData = value;
                OnPropertyChanged("ZakatDeregResponseData");
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
                if (_zakatRegListData == value) return;

                _zakatRegListData = value;
                OnPropertyChanged("ZakatRegListData");
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
                if (_isArabic == value) return;

                _isArabic = value;
                OnPropertyChanged("IsArabic");
            }
        }

        public async Task ZAKATAmendOrUpdateClicked()
        {
            await _navigationService.NavigateTo(App.EstablishmentAmendUpdatePage);
        }
        public void PopulateZakatRegListData()
        {
            try
            {
                List<ZakatDeregistrationDetailsListModel> tempZakatRegListData = new List<ZakatDeregistrationDetailsListModel>();
                string fileImage = string.Empty;
                if (App.IsArabic)
                {
                    fileImage = "arrowLeft.png";
                }
                else
                {
                    fileImage = "arrowRight.png";

                }


                if (App.LoginDataRetrieved.ZkReg == "X" || App.LoginDataRetrieved.VtReg == "X")
                {
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMTaxpayerDetails,
                            ZDImageSource = "user_profile",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMOutlets,
                            ZDImageSource = "establishments_White",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMFinancialDetails,
                            ZDImageSource = "documents_White",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMVATRegistrationDetails,
                            ZDImageSource = "documents_White",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMAmend,
                            ZDImageSource = "registration_w",
                            ArrowImageSource = fileImage
                        });
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMAmendmentOfVATRegistration,
                            ZDImageSource = "registration_w",
                            ArrowImageSource = fileImage
                        });
                    }


                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMVATDeregistration,
                            ZDImageSource = "ic_vatDe",
                            ArrowImageSource = fileImage
                        });
                    }

                    //TODO  Add this after  approval

                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                        {
                            ZDTitle = AppResources.DBSMTINDeregistration,
                            ZDImageSource = "ic_vatDe",
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
                if (App.LoginDataRetrieved.VtReg == "R")
                {
                    tempZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
                    {
                        ZDTitle = AppResources.VatReactivationDashboardTitle,
                        ZDImageSource = "registration_w.png",
                        ArrowImageSource = fileImage
                    });
                }
                ZakatRegListData = new List<ZakatDeregistrationDetailsListModel>(tempZakatRegListData);
            }
            catch (Exception)
            {
            }
        }
        public async Task HandleExceptipon()
        {

            try
            {
                _navigationService.GoBack();
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
            }
            catch (Exception)
            {
            }
        }


        public async Task GetNewTinDeregistrationDataCliked()
        {
            try
            {

                ZakatDeregResponseData = new TinDeregistrationResponseModel();
                ZakatDeregResponseData.Approvez = "";
                ZakatDeregResponseData.Rejectz = "";

                ZakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregResponseData);

                App.HideProgressView();

                if (ZakatDeregResponseData != null && ZakatDeregResponseData.Deregistration_ReasonSet != null)
                {
                    string message = string.Empty;
                    if (ZakatDeregResponseData.Deregistration_ReasonSet.Results.Count > 0)
                    {
                        foreach (var item in ZakatDeregResponseData.Deregistration_ReasonSet.Results)
                        {
                            message += item.Message;
                        }
                    }

                    await _dialogService.ShowMessage(message, AppResources.Information);
                    return;
                }
                else
                {
                    await _navigationService.NavigateTo(App.TINDeregistrationPageView, ZakatDeregResponseData);
                }
            }
            catch (InternetException)
            {
                App.HideProgressView();
                await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
            }
            catch (GAZTErrorException ex)
            {
                App.HideProgressView();
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
                        await _dialogService.ShowMessage(newMessage, AppResources.Information, AppResources.ZYes, AppResources.ZNo, async (bool isConfirmed) =>
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

                                    App.DisplayProgressView();

                                    ZakatDeregResponseData = await TINDeregistrationWebServiceManager.GaztTinDeregistrationNewRequestData(ZakatDeregResponseData);

                                    App.HideProgressView();
                                }
                                catch (InternetException)
                                {
                                    App.HideProgressView();

                                    await _dialogService.ShowMessage(AppResources.ZZInternetConnectionMessage, AppResources.Information);
                                }

                            }
                        });
                    }
                    else
                    {
                        await _dialogService.ShowMessage(message, AppResources.Information);
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
            }
        }

    }
}
