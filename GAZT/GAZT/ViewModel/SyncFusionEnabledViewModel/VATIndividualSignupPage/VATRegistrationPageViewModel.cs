using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class VATRegistrationPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static IsComeFromForAttachment IsComeFromForAttachment;

        #region Properties
        private bool _isInstrunctionVisible = false;
        public bool IsInstrunctionVisible
        {
            get
            {
                return _isInstrunctionVisible;
            }
            set
            {
                _isInstrunctionVisible = value;
                RaisePropertyChanged("IsInstrunctionVisible");
            }
        }
        private bool _isTaxPayersVisible = false;
        public bool IsTaxPayersVisible
        {
            get
            {
                return _isTaxPayersVisible;
            }
            set
            {
                _isTaxPayersVisible = value;
                RaisePropertyChanged("IsTaxPayersVisible");
            }
        }
        private bool _isSalesVisible = false;
        public bool IsSalesVisible
        {
            get
            {
                return _isSalesVisible;
            }
            set
            {
                _isSalesVisible = value;
                RaisePropertyChanged("IsSalesVisible");
            }
        }
        private bool _isExpensesVisible = false;
        public bool IsExpensesVisible
        {
            get
            {
                return _isExpensesVisible;
            }
            set
            {
                _isExpensesVisible = value;
                RaisePropertyChanged("IsExpensesVisible");
            }
        }
        private bool _isFinancialVisible = false;
        public bool IsFinancialVisible
        {
            get
            {
                return _isFinancialVisible;
            }
            set
            {
                _isFinancialVisible = value;
                RaisePropertyChanged("IsFinancialVisible");
            }
        }
        private bool _isSummaryVisible = false;
        public bool IsSummaryVisible
        {
            get
            {
                return _isSummaryVisible;
            }
            set
            {
                _isSummaryVisible = value;
                RaisePropertyChanged("IsSummaryVisible");
            }
        }

        private String _currentStep;
        public String CurrentStep
        {
            get
            {
                return _currentStep;
            }
            set
            {
                _currentStep = value;
                RaisePropertyChanged("CurrentStep");
            }
        }

        private bool _isLoading;
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

        private VATRegistrationDetails _vATRegistrationDetailsData;
        public VATRegistrationDetails VATRegistrationDetailsData
        {
            get
            {
                return _vATRegistrationDetailsData;
            }
            set
            {
                _vATRegistrationDetailsData = value;
                RaisePropertyChanged("VATRegistrationDetailsData");
            }
        }

        private VATRegistrationOtherDetails _vATRegistrationOtherDetails;
        public VATRegistrationOtherDetails VATRegistrationOtherDetails
        {
            get
            {
                return _vATRegistrationOtherDetails;
            }
            set
            {
                _vATRegistrationOtherDetails = value;
                RaisePropertyChanged("VATRegistrationOtherDetails");
            }
        }

        private bool _isInstrunctionChecked;
        public bool IsInstrunctionChecked
        {
            get
            {
                return _isInstrunctionChecked;
            }
            set
            {
                _isInstrunctionChecked = value;
                RaisePropertyChanged("IsInstrunctionChecked");
            }
        }

        private bool _isDeclarationChecked;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }

        private ResultsItem _aDDRESSSetData;
        public ResultsItem ADDRESSSetData
        {
            get
            {
                return _aDDRESSSetData;
            }
            set
            {
                _aDDRESSSetData = value;
                RaisePropertyChanged("ADDRESSSetData");
            }
        }

        private String _addressLineOne;
        public String AddressLineOne
        {
            get
            {
                return _addressLineOne;
            }
            set
            {
                _addressLineOne = value;
                RaisePropertyChanged("AddressLineOne");
            }
        }

        private String _addressLineTwo;
        public String AddressLineTwo
        {
            get
            {
                return _addressLineTwo;
            }
            set
            {
                _addressLineTwo = value;
                RaisePropertyChanged("AddressLineTwo");
            }
        }

        private String _vatEligibleStartDate=string.Empty;
        public String VatEligibleStartDate
        {
            get
            {
                return _vatEligibleStartDate;
            }
            set
            {
                _vatEligibleStartDate = value;
                RaisePropertyChanged("VatEligibleStartDate");
            }
        }

        private String _gpartFR = string.Empty;
        public String GpartFR
        {
            get
            {
                return _gpartFR;
            }
            set
            {
                _gpartFR = value;
                RaisePropertyChanged("GpartFR");
            }
        }
        private String _typeFR = string.Empty;
        public String TypeFR
        {
            get
            {
                return _typeFR;
            }
            set
            {
                _typeFR = value;
                RaisePropertyChanged("TypeFR");
            }
        }

        private String _idnumberFR = string.Empty;
        public String IdnumberFR
        {
            get
            {
                return _idnumberFR;
            }
            set
            {
                _idnumberFR = value;
                RaisePropertyChanged("IdnumberFR");
            }
        }

        private String _firstnmFR = string.Empty;
        public String FirstnmFR
        {
            get
            {
                return _firstnmFR;
            }
            set
            {
                _firstnmFR = value;
                RaisePropertyChanged("FirstnmFR");
            }
        }

        private String _lastnmFR = string.Empty;
        public String LastnmFR
        {
            get
            {
                return _lastnmFR;
            }
            set
            {
                _lastnmFR = value;
                RaisePropertyChanged("LastnmFR");
            }
        }

        private String _mobNumberFR = string.Empty;
        public String MobNumberFR
        {
            get
            {
                return _mobNumberFR;
            }
            set
            {
                _mobNumberFR = value;
                RaisePropertyChanged("MobNumberFR");
            }
        }

        private String _idNumberSR = string.Empty;
        public String IdNumberSR
        {
            get
            {
                return _idNumberSR;
            }
            set
            {
                _idNumberSR = value;
                RaisePropertyChanged("IdNumberSR");
            }
        }

        private String _firstNameSR = string.Empty;
        public String FirstNameSR
        {
            get
            {
                return _firstNameSR;
            }
            set
            {
                _firstNameSR = value;
                RaisePropertyChanged("FirstNameSR");
            }
        }

        private String _smtpAddrFR = string.Empty;
        public String SmtpAddrFR
        {
            get
            {
                return _smtpAddrFR;
            }
            set
            {
                _smtpAddrFR = value;
                RaisePropertyChanged("SmtpAddrFR");
            }
        }

        private List<SignUpIdType> _idTypeListFR;
        public List<SignUpIdType> IdTypeListFR
        {
            get
            {
                return _idTypeListFR;
            }
            set
            {
                _idTypeListFR = value;
                RaisePropertyChanged("IdTypeListFR");
            }
        }

        private List<SignUpIdType> _idTypeListSR;
        public List<SignUpIdType> IdTypeListSR
        {
            get
            {
                return _idTypeListSR;
            }
            set
            {
                _idTypeListSR = value;
                RaisePropertyChanged("IdTypeListSR");
            }
        }

        public int _iDTypeIndexFR = 0;
        public int IDTypeIndexFR
        {
            get
            {
                return _iDTypeIndexFR;
            }
            set
            {
                _iDTypeIndexFR = value;
                RaisePropertyChanged("IDTypeIndexFR");
            }
        }

        public int _iDTypeIndexSR = 0;
        public int IDTypeIndexSR
        {
            get
            {
                return _iDTypeIndexSR;
            }
            set
            {
                _iDTypeIndexSR = value;
                RaisePropertyChanged("IDTypeIndexSR");
            }
        }
        private string _txtIDType = string.Empty;
        public string TxtIDType
        {
            get
            {
                return _txtIDType;
            }
            set
            {
                _txtIDType = value;
                RaisePropertyChanged("TxtIDType");
            }
        }

        private string _txtIDTypeFR = string.Empty;
        public string TxtIDTypeFR
        {
            get
            {
                return _txtIDTypeFR;
            }
            set
            {
                _txtIDTypeFR = value;
                RaisePropertyChanged("TxtIDTypeFR");
            }
        }

        private string _txtIDTypeSR = string.Empty;
        public string TxtIDTypeSR
        {
            get
            {
                return _txtIDTypeSR;
            }
            set
            {
                _txtIDTypeSR = value;
                RaisePropertyChanged("TxtIDTypeSR");
            }
        }

        

        private SignUpIdType _selectedIdTypeFR = null;
        public SignUpIdType SelectedIdTypeFR
        {
            get
            {
                return _selectedIdTypeFR;
            }
            set
            {
                _selectedIdTypeFR = value;
                if (_selectedIdTypeFR != null)
                {

                }
                RaisePropertyChanged("SelectedIdTypeFR");
            }
        }

        private SignUpIdType _selectedIdTypeSR = null;
        public SignUpIdType SelectedIdTypeSR
        {
            get
            {
                return _selectedIdTypeSR;
            }
            set
            {
                _selectedIdTypeSR = value;
                if (_selectedIdTypeSR != null)
                {

                }
                RaisePropertyChanged("SelectedIdTypeSR");
            }
        }

        private string _importerImageSource = null;
        public string ImporterImageSource
        {
            get
            {
                return _importerImageSource;
            }
            set
            {
                _importerImageSource = value;
                RaisePropertyChanged("ImporterImageSource");
            }
        }

        private Color _importerTextColor;
        public Color ImporterTextColor
        {
            get
            {
                return _importerTextColor;
            }
            set
            {
                _importerTextColor = value;
                RaisePropertyChanged("ImporterTextColor");
            }
        }

        private Color _exporterTextColor;
        public Color ExporterTextColor
        {
            get
            {
                return _exporterTextColor;
            }
            set
            {
                _exporterTextColor = value;
                RaisePropertyChanged("ExporterTextColor");
            }
        }



        private string _exporterImageSource = null;
        public string ExporterImageSource
        {
            get
            {
                return _exporterImageSource;
            }
            set
            {
                _exporterImageSource = value;
                RaisePropertyChanged("ExporterImageSource");
            }
        }

        private string _selectedIban ;
        public string SelectedIban
        {
            get
            {
                return _selectedIban;
            }
            set
            {
                _selectedIban = value;
                RaisePropertyChanged("SelectedIban");
            }
        }

        private ObservableCollection<string> _ibanList;
        public ObservableCollection<string> IbanList
        {
            get
            {
                return _ibanList;
            }
            set
            {
                _ibanList = value;
                RaisePropertyChanged("IbanList");
            }
        }





        #endregion

        public VATRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

        }

        #region Method

        public void SetVisibility()
        {
            IsInstrunctionVisible = false;
            IsTaxPayersVisible = false;
            IsSalesVisible = false;
            IsExpensesVisible = false;
            IsFinancialVisible = false;
            IsSummaryVisible = false;
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
                    GetSignUpIdType();
                    setIban();
                    VATRegistrationDetailsData = null;
                    VATRegistrationOtherDetails = null;
                    ADDRESSSetData = null;
                    VATRegistrationDetails vATRegistration = null;
                    VATRegistrationOtherDetails vATRegistrationOther = null;
                    try
                    {
                        
                        vATRegistration = await WebServiceManager.GAZTGetVATRegistrationData();
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (vATRegistration != null && vATRegistration.d != null)
                        {
                            VATRegistrationDetailsData = vATRegistration;
                            if (VATRegistrationDetailsData.d.ADDRESSSet != null && VATRegistrationDetailsData.d.ADDRESSSet.results.Count!=0)
                            {
                                ADDRESSSetData = VATRegistrationDetailsData.d.ADDRESSSet.results[0];
                                AddressLineOne = ADDRESSSetData.BuildingNo + " " + ADDRESSSetData.Street + " " + ADDRESSSetData.Quarter;
                                AddressLineTwo = ADDRESSSetData.RegionDesc + " " + ADDRESSSetData.City + " " + ADDRESSSetData.PostalCd;
                            }
                            if(VATRegistrationDetailsData.d.VatTaxDt !=null)
                            {
                                string convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + VATRegistrationDetailsData.d.VatTaxDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                VatEligibleStartDate = Convert.ToDateTime(convertedDate).ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                            }
                            if(VATRegistrationDetailsData.d.ImFg=="1")
                            {
                                ImporterImageSource = "vat_tile_IbanCard_background.png";
                                ImporterTextColor = Color.White;
                            }
                            else
                            {
                                ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ImporterTextColor = Color.Black;
                            }
                            if (VATRegistrationDetailsData.d.ExFg == "1")
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background.png";
                                ExporterTextColor = Color.White;
                            }
                            else
                            {
                                ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                                ExporterTextColor = Color.Black;
                            }

                            vATRegistrationOther = await WebServiceManager.GAZTGetVATRegistrationDataWithButtons(vATRegistration.d.Fbnumz, vATRegistration.d.Officerz, vATRegistration.d.Statusz, vATRegistration.d.TxnTpz, "ZTAX_VT_REG");
                            PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if(vATRegistrationOther != null && vATRegistrationOther.d != null)
                            {
                                VATRegistrationOtherDetails = vATRegistrationOther;
                            }
                        }
                  
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
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
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void setIban()
        {
            IbanList = new ObservableCollection<string>();
            IbanList.Add("1111111111111");
            IbanList.Add("1111111111111");
            IbanList.Add("1111111111111");
            IbanList.Add("1111111111111");
            IbanList.Add("1111111111111");
        }
        public void GetSignUpIdType()
        {
            try
            {
                List<SignUpIdType> signUpIdTypeList = new List<SignUpIdType>{
           new SignUpIdType {ID = "ZS0015",Name = AppResources.NationaID},
                      new SignUpIdType {ID = "ZS0017",Name = AppResources.ZZIqamaID},
                                            new SignUpIdType {ID = "ZS0018",Name = AppResources.ZZGCCID},


            };
                List<SignUpIdType> lst = new List<SignUpIdType>();
                lst = signUpIdTypeList;
                IdTypeListFR = signUpIdTypeList;
                IdTypeListSR = signUpIdTypeList;
                IDTypeIndexFR = 0;
                IDTypeIndexSR = 0;
                TxtIDType = AppResources.ZZNationalID;

                TxtIDTypeFR = IdTypeListFR[IDTypeIndexFR].Name;
                TxtIDTypeSR = IdTypeListSR[IDTypeIndexSR].Name;
                SelectedIdTypeFR = IdTypeListFR[IDTypeIndexFR];
                SelectedIdTypeSR = IdTypeListSR[IDTypeIndexSR];
            }
            catch(Exception ex)
            {

            }
        }

        #endregion

    }
}
