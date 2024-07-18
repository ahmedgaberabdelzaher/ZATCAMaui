using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.CustomServices;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class LaboratoryPaymentOfInsuranceFeesViewModel : CommonViewModel
    {

        #region HijriCalender Properities

        public ICommand SelectedRequestDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (TodayDateinHijri != null && TodayDateinHijri.Count > 0)
                    {
                        string month = TodayDateinHijri[1].ToString();
                        string day = TodayDateinHijri[0].ToString();
                        string year = TodayDateinHijri[2].ToString();
                        HijriDateToBeDisplayed = day + "/" + month + "/" + year;

                    }
                });
            }
        }


        string _PickerDeclarationDateToDisplay;
        public string PickerDeclarationDateToDisplay
        {
            get
            {
                return _PickerDeclarationDateToDisplay;
            }
            set
            {
                if (_PickerDeclarationDateToDisplay == value) return;

                _PickerDeclarationDateToDisplay = value;
                RaisePropertyChanged();
            }
        }



        bool isOpenHijriPicker { get; set; }

        public bool IsOpenHijriPicker
        {
            get { return isOpenHijriPicker; }

            set
            {
                isOpenHijriPicker = value;
                RaisePropertyChanged();
            }
        }


        #endregion


        InsuranceCheckListModel insuranceCheckList { get; set; }

        public InsuranceCheckListModel InsuranceCheckList
        {
            get { return insuranceCheckList; }

            set
            {
                insuranceCheckList = value;
                RaisePropertyChanged();
            }
        }

        int? requestNumber { get; set; } = null;

        public int? RequestNumber
        {
            get { return requestNumber; }

            set
            {
                requestNumber = value;
                RaisePropertyChanged();
            }
        }

        string titleText = AppResources.InquireaboutPaymentofInsuranceTitle;

        public string TitleText
        {
            get { return titleText; }

            set
            {
                titleText = value;
                RaisePropertyChanged();
            }
        }

        bool isDetailsView { get; set; }

        public bool IsDetailsView
        {
            get { return isDetailsView; }

            set
            {
                isDetailsView = value;
                RaisePropertyChanged();
            }
        }

        bool isMainView { get; set; } = true;

        public bool IsMainView
        {
            get { return isMainView; }

            set
            {

                isMainView = value;
                RaisePropertyChanged();
            }
        }



        IlaboratoryInsuranseFeesServices _ilaboratoryInsuranseFeesServices;
        public LaboratoryPaymentOfInsuranceFeesViewModel(INavigationService navigationServices, IDialogService dialogService, IlaboratoryInsuranseFeesServices ilaboratoryInsuranseFeesServices, ICommonServices commonServices) : base(navigationServices, dialogService, commonServices)
        {
            SetDefaultDate();
            // _customInquiryService = customInquiryService;
            _ilaboratoryInsuranseFeesServices = ilaboratoryInsuranseFeesServices;
        }
        public async Task GetInssuranceCheckLst()
        {
            try
            {
                IsLoading = true;
                if (SelectedPort != null && RequestNumber != null && !string.IsNullOrEmpty(HijriDateToBeDisplayed))
                {
                    var data = await _ilaboratoryInsuranseFeesServices.CheckLetterSample(SelectedPort.port_cd, RequestNumber.Value, HijriDateToBeDisplayed.Replace("/", "-"));
                    if (data.Item2)
                    {
                        if (data.Item1.code == 404)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestNotFoundMsg;

                        }
                        else
                        {
                            var res = data.Item1;
                            InsuranceCheckList = res;

                            IsMainView = false;
                            IsDetailsView = true;
                            TitleText = AppResources.InquiryResult;
                        }
                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestNotFoundMsg;
                    }
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ZZPleasefillthemandatoryfields;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsPortsPickerSearch)
                    {
                        clear();
                    }
                    else if (IsMainView)
                    {
                        _navigationService.GoBack();
                    }
                    else
                    {
                        IsMainView = true;
                        IsDetailsView = false;
                        TitleText = AppResources.InquireaboutPaymentofInsuranceTitle;
                    }
                    ClearData();

                });
            }
        }


        public ICommand SelectPortCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (Ports != null && Ports.Count > 0)
                    {
                        var res = await ActionSheet.ShowActionSheet("", AppResources.CancelText, "", Ports.Select(c => c.Name).ToArray());
                        if (!string.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            SelectedPort = Ports.First(c => c.Name == res);
                        }
                        //IsPickerOpened =true;
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                });
            }
        }

        public ICommand OpenHijriPickerCommand
        {
            get
            {
                return new Command((sender) =>
                {
                    var date = sender as CustomHijriDatePicker;
                    date.IsOpen = true;
                });
            }
        }

        public ICommand InquireCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetInssuranceCheckLst();

                });
            }
        }

        public ICommand LoadPortsCommand
        {
            get
            {
                return new Command(() =>
                {
                    GetPorts();

                });
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return new Command(() =>
                {
                    ClearData();

                });
            }
        }

        private void ClearData()
        {

            SelectedPort = null;
            RequestNumber = null;
            ResetDate();
        }

    }
}
