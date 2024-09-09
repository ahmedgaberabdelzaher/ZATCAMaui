using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using static ZATCAMAUI.Models.EscalatedGstcModel;
namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{

    public class GeneralServicesViewModel : BaseViewModel
    {
        public List<GeneralServicesListModel> generalServicesListData = new List<GeneralServicesListModel>();
        string fileImage = string.Empty;
        public class GeneralServicesListModel
        {
            public string ZDTitle { get; set; }
            public string ZDImageSource { get; set; }
            public string ArrowImageSource { get; set; }
        }

        public ObservableCollection<GeneralServicesListModel> _generalServicesList { get; set; }
        public ObservableCollection<GeneralServicesListModel> GeneralServicesList
        {
            get
            {
                return _generalServicesList;
            }

            set
            {
                _generalServicesList = value;
                OnPropertyChanged("GeneralServicesList");
            }
        }
        public ObservableCollection<GeneralServicesListModel> _refundRequestMenuList { get; set; }
        public ObservableCollection<GeneralServicesListModel> RefundRequestMenuList
        {
            get
            {
                return _refundRequestMenuList;
            }

            set
            {
                _refundRequestMenuList = value;
                OnPropertyChanged("RefundRequestMenuList");
            }
        }
        public ObservableCollection<GeneralServicesListModel> _fillingFrquencyMenuList { get; set; }
        public ObservableCollection<GeneralServicesListModel> FillingFrquencyMenuList
        {
            get
            {
                return _fillingFrquencyMenuList;
            }

            set
            {
                _fillingFrquencyMenuList = value;
                OnPropertyChanged("FillingFrquencyMenuList");
            }
        }
        public ObservableCollection<CaseDetailsResultSet> _caseDetailedListViewData { get; set; }

        public ObservableCollection<CaseDetailsResultSet> CaseDetailedListViewData
        {
            get { return _caseDetailedListViewData; }

            set
            {
                if (_caseDetailedListViewData == value)
                {
                    return;
                }

                _caseDetailedListViewData = value;
                OnPropertyChanged("CaseDetailedListViewData");
            }
        }
        public ICommand GoBackBtnTapped { get; set; }

        public GeneralServicesViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        public void PopulateGeneralServicesListData()
        {
            if (App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            generalServicesListData.Add(new GeneralServicesListModel
            {
                ZDTitle = AppResources.NDVATRegistrationVerification,
                ZDImageSource = "request_verification.png",
                ArrowImageSource = fileImage
            });

            generalServicesListData.Add(new GeneralServicesListModel
            {
                ZDTitle = AppResources.NDBankAccManagement,
                ZDImageSource = "tax_evasion_green.png",
                ArrowImageSource = fileImage
            });

            generalServicesListData.Add(new GeneralServicesListModel
            {
                ZDTitle = AppResources.NDTaxEvasionReport,
                ZDImageSource = "taxEvasion.png",
                ArrowImageSource = fileImage
            });
            if (App.LoginDataRetrieved.TpMpVip.Equals("X"))
            {
                generalServicesListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.NDRelationContact,
                    ZDImageSource = "details.png",
                    ArrowImageSource = fileImage
                });
            }

            //TODO CR7420 - Changes Commented for Golive
            //generalServicesListData.Add(new GeneralServicesListModel
            //{
            //    ZDTitle = AppResources.ZakatExemptionRequest,
            //    ZDImageSource = "tax_evasion_green.png",
            //    ArrowImageSource = fileImage
            //});
            GeneralServicesList = new ObservableCollection<GeneralServicesListModel>(generalServicesListData);
        }

        public void PopulateRefundRequestMenuListData()
        {
            List<GeneralServicesListModel> refundRequestMenuListData = new List<GeneralServicesListModel>();
            string fileImage = string.Empty;
            if (App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            if (App.LoginDataRetrieved.VtReg == "X")
            {
                refundRequestMenuListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.VATRefundsRequest,
                    ZDImageSource = "ic_RefundR.png",
                    ArrowImageSource = fileImage
                });
            }

            RefundRequestMenuList = new ObservableCollection<GeneralServicesListModel>(refundRequestMenuListData);
        }

        public void PopulateFillingFrequencyListData()
        {
            List<GeneralServicesListModel> fillingFrequencyListData = new List<GeneralServicesListModel>();
            string fileImage = string.Empty;
            if (App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            if (App.LoginDataRetrieved.VtReg == "X")
            {
                fillingFrequencyListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.ChangeVatFillingFrequency,
                    ZDImageSource = "ic_vat_return.png",
                    ArrowImageSource = fileImage
                });
            }

            FillingFrquencyMenuList = new ObservableCollection<GeneralServicesListModel>(fillingFrequencyListData);
        }
        public async Task GetGstcCaseDetailSet()
        {
            IsLoading = true;
            try
            {
                var escalatedGstcModel = await EscalatedCasesWebserviceManager.GAZTGetCaseDetailSet();

                if (escalatedGstcModel != null && escalatedGstcModel != null && escalatedGstcModel.d != null && escalatedGstcModel.d.Code != null)
                {
                    if (escalatedGstcModel.d.Code.Equals("001"))
                    {
                        generalServicesListData.Add(new GeneralServicesListModel
                        {
                            ZDTitle = AppResources.GSTCEscalatedCasesGSTC,
                            ZDImageSource = "tax_evasion_green.png",
                            ArrowImageSource = fileImage
                        });
                    }
                    else if (escalatedGstcModel.d.Code.Equals("002"))
                    {
                        var item = generalServicesListData.FirstOrDefault(i => i.ZDTitle == "Escalated cases in GSTC");

                        if (item != null)
                        {
                            generalServicesListData.Remove(item);
                        }
                    }
                }
                GeneralServicesList = new ObservableCollection<GeneralServicesListModel>(generalServicesListData);


                if (escalatedGstcModel != null && escalatedGstcModel?.d?.CaseDetailSet?.results?.Count > 0)
                {
                    CaseDetailedListViewData = new ObservableCollection<CaseDetailsResultSet>(escalatedGstcModel.d.CaseDetailSet.results);

                }
                else
                {

                }


                IsLoading = false;
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                IsLoading = false;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }

        }

    }
}