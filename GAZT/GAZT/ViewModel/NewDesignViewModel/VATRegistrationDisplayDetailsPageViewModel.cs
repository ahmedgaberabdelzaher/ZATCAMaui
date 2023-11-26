using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class VATRegistrationDisplayDetailsPageViewModel : ViewModelBase
    {

        string idnumber { get; set; }
        public ICommand OnBackButtonClicked { get; set; }

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public VATRegistrationDisplayDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
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
        private bool _isLoading;
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
        private string _DOB = string.Empty;
        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                if (_DOB == value) return;

                _DOB = value;
                RaisePropertyChanged("DOB");
            }
        }
        private string _IDType = string.Empty;
        public string IDType
        {
            get
            {
                return _IDType;
            }
            set
            {
                if (_IDType == value) return;

                _IDType = value;
                RaisePropertyChanged("IDType");
            }
        }
        private string _IDNumber = string.Empty;
        public string IDNumber
        {
            get
            {
                return _IDNumber;
            }
            set
            {
                if (_IDNumber == value) return;

                _IDNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }
        private string _ContactPersonName = string.Empty;
        public string ContactPersonName
        {
            get
            {
                return _ContactPersonName;
            }
            set
            {
                if (_ContactPersonName == value) return;

                _ContactPersonName = value;
                RaisePropertyChanged("ContactPersonName");
            }
        }

        private string _contactDOB = string.Empty;
        public string ContactDOB
        {
            get
            {
                return _contactDOB;
            }
            set
            {
                if (_contactDOB == value) return;

                _contactDOB = value;
                RaisePropertyChanged("ContactDOB");
            }
        }
        private string _idNumberSR = string.Empty;
        public string IdNumberSR
        {
            get
            {
                return _idNumberSR;
            }
            set
            {
                if (_idNumberSR == value) return;

                _idNumberSR = value;
                RaisePropertyChanged("IdNumberSR");
            }
        }

        private string _firstNameSR = string.Empty;
        public string FirstNameSR
        {
            get
            {
                return _firstNameSR;
            }
            set
            {
                if (_firstNameSR == value) return;

                _firstNameSR = value;
                RaisePropertyChanged("FirstNameSR");
            }
        }
        private string _lastnmFR = string.Empty;
        public string LastnmFR
        {
            get
            {
                return _lastnmFR;
            }
            set
            {
                if (_lastnmFR == value) return;

                _lastnmFR = value;
                RaisePropertyChanged("LastnmFR");
            }
        }

        private string _mobNumberFR = string.Empty;
        public string MobNumberFR
        {
            get
            {
                return _mobNumberFR;
            }
            set
            {
                if (_mobNumberFR == value) return;

                _mobNumberFR = value;
                RaisePropertyChanged("MobNumberFR");
            }
        }
        private string _vatEligibleStartDate = string.Empty;
        public string VatEligibleStartDate
        {
            get
            {
                return _vatEligibleStartDate;
            }
            set
            {
                if (_vatEligibleStartDate == value) return;

                _vatEligibleStartDate = value;
                RaisePropertyChanged("VatEligibleStartDate");
            }
        }
        private string _quesTion3answerSelected = string.Empty;
        public string quesTion3answerSelected
        {
            get
            {
                return _quesTion3answerSelected;
            }
            set
            {
                if (_quesTion3answerSelected == value) return;

                _quesTion3answerSelected = value;

                RaisePropertyChanged("quesTion3answerSelected");
            }
        }
        private string _quesTion4answerSelected = string.Empty;
        public string quesTion4answerSelected
        {
            get
            {
                return _quesTion4answerSelected;
            }
            set
            {
                if (_quesTion4answerSelected == value) return;

                _quesTion4answerSelected = value;

                RaisePropertyChanged("quesTion4answerSelected");
            }
        }
        private string _quesTion1answerSelected = string.Empty;
        public string quesTion1answerSelected
        {
            get
            {
                return _quesTion1answerSelected;
            }
            set
            {
                if (_quesTion1answerSelected == value) return;

                _quesTion1answerSelected = value;

                RaisePropertyChanged("quesTion1answerSelected");
            }
        }
        private string _quesTion2answerSelected = string.Empty;
        public string quesTion2answerSelected
        {
            get
            {
                return _quesTion2answerSelected;
            }
            set
            {
                if (_quesTion2answerSelected == value) return;

                _quesTion2answerSelected = value;

                RaisePropertyChanged("quesTion2answerSelected");
            }
        }
        private string _idnumberFR = string.Empty;
        public string IdnumberFR
        {
            get
            {
                return _idnumberFR;
            }
            set
            {
                if (_idnumberFR == value) return;

                _idnumberFR = value;
                RaisePropertyChanged("IdnumberFR");
            }
        }
        private string _typeFR = string.Empty;
        public string TypeFR
        {
            get
            {
                return _typeFR;
            }
            set
            {
                if (_typeFR == value) return;

                _typeFR = value;
                RaisePropertyChanged("TypeFR");
            }
        }

        private string _firstnmFR = string.Empty;
        public string FirstnmFR
        {
            get
            {
                return _firstnmFR;
            }
            set
            {
                if (_firstnmFR == value) return;

                _firstnmFR = value;
                RaisePropertyChanged("FirstnmFR");
            }
        }
        private string _gpartFR = string.Empty;
        public string GpartFR
        {
            get
            {
                return _gpartFR;
            }
            set
            {
                if (_gpartFR == value) return;

                _gpartFR = value;
                RaisePropertyChanged("GpartFR");
            }
        }
        private string _Iban = string.Empty;
        public string Iban
        {
            get
            {
                return _Iban;
            }
            set
            {
                if (_Iban == value) return;

                _Iban = value;
                RaisePropertyChanged("Iban");
            }
        }
        
        private string _IbanText = string.Empty;
        public string IbanText
        {
            get
            {
                return _IbanText;
            }
            set
            {
                if (_IbanText == value) return;

                _IbanText = value;
                RaisePropertyChanged("IbanText");
            }
        }
        private string _smtpAddrFR = string.Empty;
        public string SmtpAddrFR
        {
            get
            {
                return _smtpAddrFR;
            }
            set
            {
                if (_smtpAddrFR == value) return;

                _smtpAddrFR = value;
                RaisePropertyChanged("SmtpAddrFR");
            }
        }
        

        private string _TxtIDTypeFR = string.Empty;
        public string TxtIDTypeFR
        {
            get
            {
                return _TxtIDTypeFR;
            }
            set
            {
                if (_TxtIDTypeFR == value) return;

                _TxtIDTypeFR = value;
                RaisePropertyChanged("TxtIDTypeFR");
            }
        }
        private string _importExportText = string.Empty;
        public string ImportExportText
        {
            get
            {
                return _importExportText;
            }
            set
            {
                if (_importExportText == value) return;

                _importExportText = value;
                RaisePropertyChanged("ImportExportText");
            }
        }
        private string _AttachmentName = string.Empty;
        public string AttachmentName
        {
            get
            {
                return _AttachmentName;
            }
            set
            {
                if (_AttachmentName == value) return;

                _AttachmentName = value;
                RaisePropertyChanged("AttachmentName");
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
                if (_vATRegistrationDetailsData == value) return;

                _vATRegistrationDetailsData = value;
                RaisePropertyChanged("VATRegistrationDetailsData");
            }
        }
        private List<QuestionNumberWithMinMaxRange> _minMaxRanges;
        public List<QuestionNumberWithMinMaxRange> MinMaxRanges
        {
            get
            {
                return _minMaxRanges;
            }
            set
            {
                if (_minMaxRanges == value) return;

                _minMaxRanges = value;
                RaisePropertyChanged("MinMaxRanges");
            }
        }


        private bool ibanVisibility;
        public bool IbanVisibility
        {
            get { return ibanVisibility; }
            set
            {
                ibanVisibility = value;
                RaisePropertyChanged("IbanVisibility");
            }
        }

        private ObservableCollection<Result2> _ibanList;
        public ObservableCollection<Result2> IbanList
        {
            get
            {
                return _ibanList;
            }
            set
            {
                if (_ibanList == value) return;

                _ibanList = value;
                IbanVisibility=_ibanList.Count > 0;
                RaisePropertyChanged("IbanList");
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
                    VATRegistrationDetailsData = null;

                    VATRegistrationDetails vATRegistration = null;

                    try
                    {
                        vATRegistration = await VatRegistrationWebServiceManager.GAZTGetVATRegistrationDisplayDetailsData();
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (vATRegistration != null && vATRegistration.d != null)
                        {

                            //step 4 and 5 data set
                            if (vATRegistration.d.CONTACT_PERSONSet != null)
                            {
                                GpartFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Gpart;
                                //  VATRegistrationDetailsData.d.CONTACT_PERSONSet.results[0].Type = SelectedIdTypeFR.ID;
                                idnumber = string.Empty;
                                idnumber = vATRegistration.d.CONTACT_PERSONSet.results[0].Idnumber;
                                FirstnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Firstnm;
                                LastnmFR = vATRegistration.d.CONTACT_PERSONSet.results[0].Lastnm;
                                MobNumberFR = vATRegistration.d.CONTACTDTSet.results[0].MobNumber;
                                SmtpAddrFR = vATRegistration.d.CONTACTDTSet.results[0].SmtpAddr;
                                DOB = vATRegistration.d.CONTACT_PERSONSet.results[0].Dobdt;
                                if(vATRegistration.d.CONTACT_PERSONSet.results[0].Type.Equals("ZS0003"))
                                {
                                    TxtIDTypeFR = AppResources.ZZGCCID;
                                }
                                else if(vATRegistration.d.CONTACT_PERSONSet.results[0].Type.Equals("ZS0002"))
                                {
                                    TxtIDTypeFR = AppResources.ZZIqamaID;

                                }
                                else
                                {
                                    TxtIDTypeFR = AppResources.ZZNationalID;

                                }
                            }

                            if(vATRegistration.d.DecidTy.Equals("ZS0003"))
                            {
                                IDType = AppResources.ZZGCCID;
                            }else if (vATRegistration.d.DecidTy.Equals("ZS0002"))
                            {
                                IDType = AppResources.ZZIqamaID;

                            }
                             else
                            {
                                IDType = AppResources.ZZNationalID;

                            }

                           // IDType = vATRegistration.d.DecidTy;
                            IDNumber = vATRegistration.d.DecidNo;
                            ContactPersonName = vATRegistration.d.Decname;
                        }
                        IbanList = new ObservableCollection<Result2>();

                        if (vATRegistration.d.IBANSet != null)
                        {
                            IbanList = new ObservableCollection<Result2>(vATRegistration.d.IBANSet.results);
                            for (int i = 0; i < IbanList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(IbanList.ElementAt(i).Iban))
                                {

                                    Iban = IbanList.FirstOrDefault().Iban;
                                    IbanText = "IBAN";
                                }
                                else
                                {
                                    IbanText = "";
                                }
                            }

                        }
                        if (vATRegistration.d.QUESTIONSSet != null)
                        {
                            List<ResultsItemForQuestion> quest1AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "001" && s.QoptAns == "1").ToList();
                            if (quest1AnsList.Count > 0)
                            {
                                quesTion1answerSelected = quest1AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;
                            }
                            List<ResultsItemForQuestion> quest2AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "002" && s.QoptAns == "1").ToList();
                            if (quest2AnsList.Count > 0)
                            {
                                quesTion2answerSelected = quest2AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;
                            }
                            List<ResultsItemForQuestion> quest3AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList();
                            if (quest3AnsList.Count > 0) { 
                            quesTion3answerSelected = quest3AnsList.FirstOrDefault().QoptTxt; // vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "003" && s.QoptAns == "1").ToList;
                                 }
                            List<ResultsItemForQuestion> quest4AnsList = vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").ToList();
                            if (quest4AnsList.Count > 0)
                            {
                                quesTion4answerSelected = quest3AnsList.FirstOrDefault().QoptTxt;//vATRegistration.d.QUESTIONSSet.results.Where(s => s.QueNo == "004" && s.QoptAns == "1").ToString();
                            }
                         }
                        if (vATRegistration.d.VatTaxDt != null)
                        {
                            string convertedDate = JsonConvert.DeserializeObject<DateTime>(@"""" + vATRegistration.d.VatTaxDt + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            VatEligibleStartDate = Convert.ToDateTime(convertedDate).ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                        }
                        if (vATRegistration.d.QUESCONFIG_MSet.results.Count != 0)
                        {
                            MinMaxRanges = new List<QuestionNumberWithMinMaxRange>();
                            MinMaxRanges = UtilityManager.GetLowAndHighRangeForEachQuestionSet(vATRegistration.d.QUESCONFIG_MSet);
                            //MaximumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MaxRangeValue).FirstOrDefault();
                            //MinimumDisplayValueOfSlider1 = MinMaxRanges.Where(x => x.QueNo == "001").Select(x => x.MinRangeValue).FirstOrDefault();

                            //MaximumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MaxRangeValue).FirstOrDefault();
                            //MinimumDisplayValueOfSlider2 = MinMaxRanges.Where(x => x.QueNo == "002").Select(x => x.MinRangeValue).FirstOrDefault();
                        }
                        if (vATRegistration.d.ATTDETSet != null)
                        { 
                            foreach (Attachment ItemA in vATRegistration.d.ATTDETSet.results)
                            {
                                AttachmentName = ItemA.Filename;
                            }
                         }
                      
                     
                        if (vATRegistration.d.ExFg=="1")
                        {
                            ImportExportText = AppResources.VATRExporter;//"Exporter";
                        }
                        else if(vATRegistration.d.ImFg == "1")
                        {
                            ImportExportText = AppResources.VATRImporter;//"Importer";

                        }

                        IdnumberFR = idnumber;


                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        throw ex;
                    }
                    catch (InternetException ex)
                    {
                        
                        
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            //_navigationService.GoBack();
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
                
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });

            }
            catch (Exception)
            {
                
                
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
           
            }
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

    }
}
