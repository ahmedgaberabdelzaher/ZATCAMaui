using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using GAZT;
using Xamarin.Essentials;
//using Prism.Commands;
//using Prism.Mvvm;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class InquiryAboutCustomsDeclarationViewModel : BaseViewModel
    {
        InquireTypes selectedInquireThrougTypes { get; set; }

        public InquireTypes SelectedInquireThrougTypes
        {
            get { return selectedInquireThrougTypes; }

            set
            {
                selectedInquireThrougTypes = value;
                RaisePropertyChanged();
            }
        }
        bool isPickerOpened { get; set; }

        public bool IsPickerOpened
        {
            get { return isPickerOpened; }

            set
            {
                isPickerOpened = value;
                RaisePropertyChanged();
            }
        }

        bool isPortsPickerSearch { get; set; }

        public bool IsPortsPickerSearch
        {
            get { return isPortsPickerSearch; }

            set
            {
                isPortsPickerSearch = value;
                
                RaisePropertyChanged();
            }
        }


        bool isMainPage { get; set; } = true;
        public bool IsMainPage
        {
            get { return isMainPage; }

            set
            {
                isMainPage = value;
                RaisePropertyChanged();
            }
        }

        double totalFees { get; set; }

        public double TotalFees
        {
            get { return totalFees; }

            set
            {
                totalFees = value;
                RaisePropertyChanged();
            }
        }


        int? declarationNumber { get; set; } = null;

        public int? DeclarationNumber
        {
            get { return declarationNumber; }

            set
            {
                declarationNumber = value;
                RaisePropertyChanged();
            }
        }

        string billInfoNumber { get; set; }

        public string BillInfoNumber
        {
            get { return billInfoNumber; }

            set
            {
                billInfoNumber = value;
                RaisePropertyChanged();
            }
        }

        bool isDeclarationTypeOpened { get; set; }

        public bool IsDeclarationTypeOpened
        {
            get { return isDeclarationTypeOpened; }

            set
            {
                isDeclarationTypeOpened = value;
                RaisePropertyChanged();
            }
        }

        bool isCarrierOpened { get; set; }

        public bool IsCarrierOpened
        {
            get { return isCarrierOpened; }

            set
            {
                isCarrierOpened = value;
                RaisePropertyChanged();
            }
        }

        bool isStatmentDetailsVisible { get; set; }

        public bool IsStatmentDetailsVisible
        {
            get { return isStatmentDetailsVisible; }

            set
            {
                isStatmentDetailsVisible = value;
                RaisePropertyChanged();
            }
        }

        bool isDetailsVisible { get; set; }

        public bool IsDetailsVisible
        {
            get { return isDetailsVisible; }

            set
            {
                isDetailsVisible = value;
                RaisePropertyChanged();
            }
        }

        bool isOTPView { get; set; }

        public bool IsOTPView
        {
            get { return isOTPView; }

            set
            {
                isOTPView = value;
                RaisePropertyChanged();
            }
        }


        bool isFilterByDeclarationInformation = true;

        public bool IsFilterByDeclarationInformation
        {
            get { return isFilterByDeclarationInformation; }

            set
            {
                isFilterByDeclarationInformation = value;
                RaisePropertyChanged();
            }
        }


        bool isFilterByBusInformation;

        public bool IsFilterByBusInformation
        {
            get { return isFilterByBusInformation; }

            set
            {
                isFilterByBusInformation = value;
                RaisePropertyChanged();
            }
        }


        #region HijriCalender Properities

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

        bool isShowMsgView { get; set; }

        public bool IsShowMsgView
        {
            get { return isShowMsgView; }

            set
            {
                isShowMsgView = value;
                RaisePropertyChanged();
            }
        }

        string messageTxt { get; set; }

        public string MessageTxt
        {
            get { return messageTxt; }

            set
            {
                messageTxt = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        ObservableCollection<InquireTypes> inquireThrougTypes { get; set; }

        public ObservableCollection<InquireTypes> InquireThrougTypes
        {
            get { return inquireThrougTypes; }

            set
            {
                if (inquireThrougTypes == value)
                {
                    return;
                }

                inquireThrougTypes = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<DeclarionInformationInquire> declarionByInformationInquireLst { get; set; }

        public ObservableCollection<DeclarionInformationInquire> DeclarionByInformationInquireLst
        {
            get { return declarionByInformationInquireLst; }

            set
            {

                declarionByInformationInquireLst = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<DeclarationFees> declarionFeesLst { get; set; }

        public ObservableCollection<DeclarationFees> DeclarionFeesLst
        {
            get { return declarionFeesLst; }

            set
            {

                declarionFeesLst = value;
                RaisePropertyChanged();
            }
        }

        StatmentItems selectedStatmentItems { get; set; }

        public StatmentItems SelectedStatmentItems
        {
            get { return selectedStatmentItems; }

            set
            {

                selectedStatmentItems = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<StatmentItems> statmentItemsLst { get; set; }

        public ObservableCollection<StatmentItems> StatmentItemsLst
        {
            get { return statmentItemsLst; }

            set
            {

                statmentItemsLst = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<InquireByBillInfo> decByBillingInfoLst { get; set; }

        public ObservableCollection<InquireByBillInfo> DecByBillingInfoLst
        {
            get { return decByBillingInfoLst; }

            set
            {

                decByBillingInfoLst = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<CustomPort> ports { get; set; }

        public ObservableCollection<CustomPort> Ports
        {
            get { return ports; }

            set
            {
                if (ports == value)
                {
                    return;
                }

                ports = value;
                RaisePropertyChanged();
            }
        }

      
        public static ObservableCollection<CustomPort> PortsStaticLst{ get;set;}
        public static ObservableCollection<DeclarationType> DeclartionTpesStaticLst { get; set; }
        public static ObservableCollection<Carrier> CarriersStaticLst { get; set; }

        ObservableCollection<Carrier> carriers { get; set; }

        public ObservableCollection<Carrier> Carriers
        {
            get { return carriers; }

            set
            {

                carriers = value;
                RaisePropertyChanged();
            }
        }

        Carrier selectedCarrier { get; set; } = null;

        public Carrier SelectedCarrier
        {
            get { return selectedCarrier; }

            set
            {

                selectedCarrier = value;
                RaisePropertyChanged();
            }
        }

        CustomItemCalcDescription feesDescription ;

        public CustomItemCalcDescription FeesDescription
        {
            get { return feesDescription; }

            set
            {

                feesDescription = value;
                RaisePropertyChanged();
            }
        }



        CustomPort selectedPort { get; set; } = null;

        public CustomPort SelectedPort
        {
            get { return selectedPort; }

            set
            {
                selectedPort = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<DeclarationType> declarationTypes { get; set; }

        public ObservableCollection<DeclarationType> DeclarationTypes
        {
            get { return declarationTypes; }

            set
            {
                declarationTypes = value;
                RaisePropertyChanged();
            }
        }

        DeclarationType selectedDeclarationType { get; set; }

        public DeclarationType SelectedDeclarationType
        {
            get { return selectedDeclarationType; }

            set
            {
                selectedDeclarationType = value;
                RaisePropertyChanged();
            }
        }
       
        ICustomInquiryService _customInquiryService;
        ICommonServices _commonServices;
        public InquiryAboutCustomsDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, ICustomInquiryService customInquiryService, ICommonServices commonServices) :base(navigationService,dialogService)
        {
            _customInquiryService = customInquiryService;
            _commonServices = commonServices;
            SetDefaultDate();
           InquireThrougTypes = new ObservableCollection<InquireTypes>()
            {
 new InquireTypes()
 {
     Name=AppResources.InQuerywithstatementinformation,Id=1
 }  ,
  new InquireTypes()
 {
     Name=AppResources.Inquiryforpolicyinformation,Id=2
 }  ,
            };
        }


        public ICommand CloseMsgViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowMsgView = false;
                });
            }
        }

        bool isFeesDescriptionVisible { get; set; }

        public bool IsFeesDescriptionVisible
        {
            get { return isFeesDescriptionVisible; }

            set
            {
                isFeesDescriptionVisible = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CloseisFeesDescriptionCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsFeesDescriptionVisible = false;
                });
            }
        }

        public ICommand OpenFeesDescriptionCommand
        {
            get
            {
                return new Command(async() =>
                {
                    IsFeesDescriptionVisible = true;
                    await GetCustomFeesDescription();
                });
            }
        }


        public ICommand OpenInquiryFilterCommand
        {
            get
            {
                return new Command(async() =>
                {

                    if (PortsStaticLst != null && PortsStaticLst.Count > 0)
                    {
                        /*var res = await ActionSheet.ShowActionSheet(null, AppResources.CancelText, null, Ports.Select(c => c.Name).ToArray());
                        if (!String.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            SelectedPort = Ports.First(c => c.Name == res);
                        }*/
                        Ports = PortsStaticLst;
                        IsPortsPickerSearch = true;
                         IsPickerOpened =true;
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                });
            }
        }

        public ICommand OpenDeclarationTypesLstCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (DeclartionTpesStaticLst != null && DeclartionTpesStaticLst.Count > 0)
                    {
                        /*var res = await ActionSheet.ShowActionSheet(null, AppResources.CancelText, null, DeclarationTypes.Select(c => c.Value).ToArray());
                        if (!String.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            SelectedDeclarationType = DeclarationTypes.First(c => c.Value == res);
                        }*/
                        DeclarationTypes = DeclartionTpesStaticLst;
                        IsPortsPickerSearch = true;     IsDeclarationTypeOpened = true;
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                }
                );
            }
        }
        public StringBuilder GetCaptcha()
        {
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    SelectedParameterType = ParameterTypeList[0];
            //});
            StringBuilder Captcha;
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                //Session["captcha"] = captcha.ToString();
                //imgCaptcha.ImageUrl = "~/Captcha/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                Captcha = captcha;
            }
            catch
            {
                throw;
            }
            return Captcha;
        }

        public ICommand OpenCarrierLstCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {

                    IsLoading = true;
                    if (SelectedPort!=null)
                    {
  //await GetCarriers(SelectedPort.port_cd);
                    if (CarriersStaticLst!=null&& CarriersStaticLst.Count>0)
                    {

                        /*         var res = await ActionSheet.ShowActionSheet(null, AppResources.CancelText, null, Carriers.Select(c => c.carr_name).ToArray());
                                 if (!String.IsNullOrEmpty(res) && res != AppResources.CancelText)
                                 {
                                     SelectedCarrier = Carriers.First(c => c.carr_name == res);
                                 }*/
                     Carriers = CarriersStaticLst;
                     IsPortsPickerSearch=IsCarrierOpened = true;
                                
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                    }
                 else
                    {
                        MessageTxt = AppResources.SelectPortFirst;
                        IsShowMsgView = true;
                    }
                    

                    }
                    catch
                    {

                    }
                    finally { IsLoading = false; }
                });
            }
        }


        public ICommand CloseInquiryFilterCommand
        {
            get
            {
                return new Command(() =>
                {

                    IsPortsPickerSearch = IsCarrierOpened = IsDeclarationTypeOpened = IsPickerOpened = false;
                    SearchTxt = "";
                });
            }
        }

        public ICommand OpenHijriPickerCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsOpenHijriPicker = true;
                });
            }
        }
        public ICommand CloseHijriPickerCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsOpenHijriPicker = false;
                });
            }
        }
        public ICommand ChangeFilterByCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsFilterByDeclarationInformation = !IsFilterByDeclarationInformation;
                });
            }
        }

        public ICommand FilterByDeclarationInfoCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsFilterByDeclarationInformation =true;
                    IsFilterByBusInformation = false;
                    SelectedPort = null;
                });
            }
        }

        public ICommand FilterByBusInfoCommand
        {
            get
            {
                return new Command(() =>
                {
                    
                    IsFilterByDeclarationInformation = false;
                    IsFilterByBusInformation = true;
                    SelectedPort = null;
                });
            }
        }

        public async Task GetPorts()
        {
            try
            {
                IsLoading = true;
                var data =await _customInquiryService.GetCustomPorts();
                if (data.Item2)
                {
                    Ports = data.Item1.Data;
                    PortsStaticLst = Ports;
                   // SelectedPort = null;
                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }

        public async Task GetCustomFeesDescription()
        {
            try
            {
                IsLoading = true;
                var data = await _customInquiryService.GetItemsCalcTxt(DeclarionByInformationInquireLst[0].port_cd, DeclarionByInformationInquireLst[0].dcltn_type_cd, SelectedStatmentItems.item_isn);
                if (data.Item2)
                {
                    FeesDescription = data.Item1;
                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }


        public async Task GetDeclarationTypes()
        {
            try
            {
                var data = await _customInquiryService.GetDeclarationTypes();
                if (data.Item2)
                {
                    DeclarationTypes = data.Item1.Data;
                    DeclartionTpesStaticLst = DeclarationTypes;
                }
            }
            catch (Exception ex)
            {

            }
        }

         public async Task GetCarriers(int RouteCode=99)
        {
            try
            {
                IsLoading = true;
                var data = await _customInquiryService.GetCarriers(RouteCode);
                if (data.Item2)
                {
                    Carriers = data.Item1.Data;
                     CarriersStaticLst=Carriers;
                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }
        bool IsFromBillInfo;
        string Date;
        int? DeclarationType;
        public async Task GetDeclarationsByInformation(bool isFromBillInfo=false,string date="",int? no=null,int? declarationType=null)
        {
            try
            {
                IsLoading = true;
                IsFromBillInfo = isFromBillInfo;
                
                Tuple<DeclarionInformationInquireResponse, bool, string> data;
                if (isFromBillInfo)
                {
                    data = await _customInquiryService.GetDcltnBusID(selectedPort.port_cd, no.Value, date.Replace("/", "-"), declarationType.Value);
                    DeclarationNumber = no;
                    Date = date;
                }
                else
                {
                    data = await _customInquiryService.GetDcltnBusID(selectedPort.port_cd, DeclarationNumber.Value, HijriDateToBeDisplayed.Replace("/", "-"), selectedDeclarationType.Key);
                }
                DeclarationType = declarationType;
                    if (data.Item2)
                {
                    if (data.Item1.code!=200)
                    {
                        //await _dialogService.ShowMessage(AppResources.DeclarationNotAvailableMsg,"");
                        MessageTxt = AppResources.DeclarationNotAvailableMsg;
                        IsShowMsgView = true;
                        IsMainPage = true;
                    }
                    else
                    {
                        DeclarionByInformationInquireLst = data.Item1.data;
                        IsMainPage = false;
                         await SendOtpSMS(DeclarionByInformationInquireLst[0].mobile_nbr);
                       IsOTPView = true;
                      // await LoadInquiryDetails();
                        /*
                        IsDetailsVisible = true;
                        if (isFromBillInfo)
                        {
                            GetDeclarationFees(no.Value, selectedPort.port_cd,date,declarationType.Value);
                           await GetDeclarationStatmentItems(declarationType.Value);
                        }
                        else
                        {
                            
                            GetDeclarationFees(DeclarationNumber.Value, selectedPort.port_cd, HijriDateToBeDisplayed, SelectedDeclarationType.Key);
                           await GetDeclarationStatmentItems(SelectedDeclarationType.Key);
                        }
                      */
                    }
                }
                 else
                    {
                    // await _dialogService.ShowMessage(AppResources.DeclarationNotAvailableMsg,"");
                    IsShowMsgView = true;
                    MessageTxt = AppResources.DeclarationNotAvailableMsg;
                    IsMainPage = true;
                    }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }


        public async Task LoadInquiryDetails()
        {
            IsOTPView = false;
            IsDetailsVisible = true;
            if (IsFromBillInfo)
            {
                GetDeclarationFees(DeclarationNumber.Value, SelectedPort.port_cd, Date, DeclarationType.Value);
                await GetDeclarationStatmentItems(DeclarationType.Value);
            }
            else
            {

                GetDeclarationFees(DeclarationNumber.Value, selectedPort.port_cd, HijriDateToBeDisplayed, SelectedDeclarationType.Key);
                await GetDeclarationStatmentItems(SelectedDeclarationType.Key);
            }
          
        }

        public async Task GetDeclarationFees(int declarationNumber,int port,string date,int typeCode)
        {
            try
            {
               // IsLoading = true;

                //var data = await _customInquiryService.GetDclFees(selectedPort.port_cd, DeclarationNumber, HijriDateToBeDisplayed.Replace("/", "-"), selectedDeclarationType.Key);
                var data = await _customInquiryService.GetDclFees(port, declarationNumber, date.Replace("/", "-"), typeCode);

                if (data.Item2)
                {
                  
                    DeclarionFeesLst = data.Item1.data;
                    double total = 0;
                    for (int i = 0; i < DeclarionFeesLst.Count; i++)
                    {
                        total += DeclarionFeesLst[i].amt;
                        if (i== DeclarionFeesLst.Count-1)
                        {
                            DeclarionFeesLst[i].isHideSeperatorLine = false;
                        }
                    }
                    TotalFees = total;
                }
            }
            catch (Exception ex)
            {

            }
            finally {  }
        }

        public async Task GetDeclarationStatmentItems(int declType)
        {
            try
            {
                if (DeclarionByInformationInquireLst!=null&&DeclarionByInformationInquireLst.Count>0)
                {

                    
                    var dcltn_isn = DeclarionByInformationInquireLst[0].dcltn_isn;
              //  IsLoading = true;
                var data = await _customInquiryService.GetDclStatmentItems(selectedPort.port_cd, int.Parse(dcltn_isn.Replace(".0","")), declType);
                if (data.Item2)
                {
                    StatmentItemsLst = data.Item1.data;
                       // StatmentItemsLst.Add(data.Item1.data.First());


                }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
        }

        public async Task GetDeclarationsByBillInformation()
        {
            try
            {
                IsLoading = true;
                var data = await _customInquiryService.GetDeclarationInfoByBill(selectedPort.port_cd, BillInfoNumber, SelectedCarrier.carr_prefix);
                if (data.Item2)
                {
                    if (data.Item1.code != 200)
                    {
                        //await _dialogService.ShowMessage(AppResources.DeclarationNotAvailableMsg, "");
                        MessageTxt = AppResources.DeclarationNotAvailableMsg;

                        IsShowMsgView = true;
                        IsMainPage = true;
                    }
                    else
                    {
                        var res = data.Item1.data;
                      await  GetDeclarationsByInformation(true, res.dcltn_dt, res.dcltn_nbr,res.dcltn_type_cd);
                    }
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.DeclarationNotAvailableMsg;
                    IsMainPage = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }


        public ICommand SubmitInquiryCommand
        {
            get
            {
                return new Command(async() =>
                {

                    if ((IsFilterByBusInformation && !string.IsNullOrEmpty(BillInfoNumber) && selectedPort != null && SelectedCarrier != null) ||
                    (IsFilterByDeclarationInformation && DeclarationNumber != 0&& DeclarationNumber!=null && SelectedPort != null && SelectedDeclarationType != null)
                    )
                    {
                        if (IsFilterByBusInformation)
                        {
                            await GetDeclarationsByBillInformation();
                        }
                    else
                        {

                            await GetDeclarationsByInformation();
                            //GetDeclarationFees(DeclarationNumber, selectedPort.port_cd, HijriDateToBeDisplayed, SelectedDeclarationType.Key);
                            //GetDeclarationStatmentItems(SelectedDeclarationType.Key);
                        }
                    }
                    else
                    {
                        //  await _dialogService.ShowMessage(AppResources.InquiryDataRequiredAttentionMsg, "");
                        MessageTxt = AppResources.InquiryDataRequiredAttentionMsg;
                        IsShowMsgView = true;
                    }
                });
            }

        }

        public ICommand StatmentDetailsCommand
        {
            get
            {
                return new Command<StatmentItems>(async(e) =>
                {
                    SelectedStatmentItems = e;
                    IsStatmentDetailsVisible = true;
                    IsDetailsVisible = false;
                });
            }

        }



        public ICommand BackFromDetailsCommand
        {
            get
            {
                return new Command(() =>
                {

                    IsMainPage = true;
                    IsDetailsVisible = false;
                    IsStatmentDetailsVisible = false;

                    StatmentItemsLst = null;
                    DeclarionFeesLst = null;
                    DeclarionByInformationInquireLst = null;
                    ResetData();
                });
            }
        }

      

        private void ResetData()
        {
            SelectedCarrier = null;
            SelectedDeclarationType = null;
            SelectedPort = null;
            DeclarationNumber = null;
            BillInfoNumber = null;
            ResetDate();
        }

        public ICommand BackToDetailsCommand
        {
            get
            {
                return new Command(() =>
                {
                    SelectedStatmentItems = null;
                    IsStatmentDetailsVisible = false;
                    IsDetailsVisible = true;
                    IsFeesDescriptionVisible = false;
                });
            }
        }


        public ICommand LoadIntialLookupsCommand
        {
            get
            {
                return new Command(() =>
                {
                    GetPorts();
                    GetDeclarationTypes();
                   GetCarriers();
                });
            }
        }

        public ICommand PortSelectionChangedCommand
        {
            get
            {
                return new Command<object>((e) =>
                {

                    var type = e.GetType();
                    
                    if (type==typeof(CustomPort))
                    {
                        SelectedPort = e as CustomPort;
                        Ports = PortsStaticLst;
                        //SelectedCarrier = null;
                    }
                   else if (type == typeof(DeclarationType))
                    {
                        SelectedDeclarationType = e as DeclarationType;
                        DeclarationTypes = DeclartionTpesStaticLst;
                    }
                  else  if (type == typeof(Carrier))
                    {
                        SelectedCarrier = e as Carrier;
                        Carriers = CarriersStaticLst;
                    }
                    IsPortsPickerSearch=IsCarrierOpened=IsDeclarationTypeOpened = IsPickerOpened = false;
                    SearchTxt="";
                });
            }
        }
 public ICommand CarrierSelectionChangedCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedCarrier!=null)
                    {
                        IsCarrierOpened = false;
                    }
                });
            }
        }

        public ICommand DeclarationTypeSelectionChangedCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedDeclarationType != null)
                    {
                        IsDeclarationTypeOpened = false;
                    }
                });
            }
        }
  public ICommand SelectedDeclarationTypeDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (TodayDateinHijri != null&&TodayDateinHijri.Count>0)
                    {
                        /* string month = TodayDateinHijri[1].ToString();
                         string day = TodayDateinHijri[0].ToString();
                         string year = TodayDateinHijri[2].ToString();
                         HijriDateToBeDisplayed = day + "/" + month + "/" + year;
                         */
                        string month = TodayDateinHijri[1].ToString();
                        string day; string year;
                        if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                        {
                            day = TodayDateinHijri[0].ToString();
                            year = TodayDateinHijri[2].ToString();
                        }
                        else
                        {
                            day = TodayDateinHijri[2].ToString();
                            year = TodayDateinHijri[0].ToString();
                        }
                        HijriDateToBeDisplayed = day + "/" + month + "/" + year;

                    }
                });
            }
        }


        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsPortsPickerSearch)
                    {
                        IsPortsPickerSearch = IsDeclarationTypeOpened = IsCarrierOpened= IsPickerOpened = false;
                        SearchTxt = "";
                        return;
                    }
                    _navigationService.GoBack();
                    ResetData();
                });
            }
        }
  
        public ICommand VerifyOTPCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        IsLoading = true;
                        if (IsOtpValid)
                        {

                            otpTimer.Stop();
                            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                            if (EnteredOTP == Preferences.Get("OTPValue", ""))
                            {
                                await LoadInquiryDetails();
                                ClearOTPData();
                            }
                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidOTP;
                            }
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.InvalidOTP;
                        }
                    }
                    catch (Exception ex)
                    {
                       
                    }
                    finally { IsLoading = false; }
                 
                    
                });
            }
        }


        public ICommand GoToNextEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e!=null)
                    {
                  
                        var entry = e as BorderlessEntry;

                        switch (entry.ClassId)
                        {
                            case "2":
                                if (!string.IsNullOrEmpty(OTPFirstDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "3":
                                if (!string.IsNullOrEmpty(OTPSecondDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "4":
                                if (!string.IsNullOrEmpty(OTPThirdDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            default:
                                break;
                        }
                       
                        
                    }
                });
            }
        }

        public ICommand BackFromOtpCommand
        {

            get
            {
                return new Command(() =>
                {
                    IsOTPView= IsOtpValid = false;
                    IsMainPage = true;
                    ClearOTPData();
                });
            }
        }

        public string Phone { get; set; }

        public ICommand ResendOtpCommand
        {

            get
            {
                return new Command(async() =>
                {
                    
                    await SendOtpSMS(Phone);
                });
            }
        }

        public async Task SendOtpSMS(string PhoneNo)
        {
            try
            {
                //PhoneNo = "0551844232";
                Phone = PhoneNo;
                IsLoading = true;
                string otp = OTPHelper.Generate();
                Preferences.Set("OTPValue", otp);
                Preferences.Set("MobileNo", PhoneNo);
                var data = await _commonServices.SendOtpSms(PhoneNo,$"{AppResources.OTPMsgBody}{otp}");
                IsOtpValid = true;
                OTPSentOnThisMobileNumber = AppResources.MobileNumber + " xxxxxxx" + Phone.Substring(7, 3);
                ResendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
                IsResendCodeEnabled = false;
                StartOTPTimer();
                if (data.Item2)
                {
                   
                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }

       public void ClearOTPData()
        {
            Preferences.Remove("OTPValue");
            Preferences.Remove("MobileNo");
            OTPSentOnThisMobileNumber= OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = EnteredOTP = "";
            IsResendCodeEnabled= IsOtpValid = false;
            ResendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];

        }

        private string _OTPSentOnThisMobileNumber;
        public string OTPSentOnThisMobileNumber
        {
            get
            {
                return _OTPSentOnThisMobileNumber;
            }
            set
            {
                if (_OTPSentOnThisMobileNumber == value) return;

                _OTPSentOnThisMobileNumber = value;
                RaisePropertyChanged();
            }
        }

        private string _SearchTxt;
        public string SearchTxt
        {
            get
            {
                return _SearchTxt;
            }
            set
            {
                if (_SearchTxt == value) return;

                _SearchTxt = value;
                RaisePropertyChanged();
            }
        }

        private bool isResendCodeEnabled;
        public bool IsResendCodeEnabled
        {
            get
            {
                return isResendCodeEnabled;
            }
            set
            {
                if (isResendCodeEnabled == value) return;

                isResendCodeEnabled = value;
                RaisePropertyChanged();
            }
        }


        string oTPFirstDigit;
        public string OTPFirstDigit { get { return oTPFirstDigit; } set { oTPFirstDigit = value; RaisePropertyChanged(); } }

        string oTPSecondDigit;
        public string OTPSecondDigit { get { return oTPSecondDigit; } set { oTPSecondDigit = value; RaisePropertyChanged(); } }

        string oTPThirdDigit;
        public string OTPThirdDigit { get { return oTPThirdDigit; } set { oTPThirdDigit = value; RaisePropertyChanged(); } }

        string oTPFourthDigit;
        public string OTPFourthDigit { get { return oTPFourthDigit; } set { oTPFourthDigit = value; RaisePropertyChanged(); } }

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                RaisePropertyChanged();
            }
        }

        private Color _resendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
        public Color ResendOTPTextColor
        {
            get
            {
                return _resendOTPTextColor;
            }
            set
            {
                if (_resendOTPTextColor == value) return;

                _resendOTPTextColor = value;
                RaisePropertyChanged();
            }
        }

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public string EnteredOTP = string.Empty;
        bool IsOtpValid;
        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;

            otpTimer.Enabled = true;
        }

        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            if (countDownSeconds <= 9&&countDownSeconds>0)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else if (countDownSeconds > 60)
            {
                int countDownSecondsL = countDownSeconds - 60;
                LblCountDownTimer = "1:" + countDownSecondsL.ToString();

                if (countDownSecondsL <= 9)
                    LblCountDownTimer = "1:0" + countDownSecondsL.ToString();
            }
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();

            // Stop timer
            if (countDownSeconds == 0)
            {
                otpTimer.Elapsed -= OnCountDownTimedOTPEvent;
                otpTimer.Stop();
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];
                IsOtpValid = false;
                IsResendCodeEnabled = true;
            }
        }

        public ICommand SearchInPortsCommand
        {

            get
            {
                return new Command (() =>
                {
                    try
                    {

                    if (IsPickerOpened)
                    {
                    var res = PortsStaticLst.Where(c => c.Name.Contains(SearchTxt));
                    Ports = new ObservableCollection<CustomPort>(res);
                    }
                   /* else
                    {
                        Ports = new ObservableCollection<CustomPort>(PortsStaticLst);
                    }*/
                    if (!String.IsNullOrEmpty(SearchTxt) && IsDeclarationTypeOpened)
                    {
                        var res = DeclartionTpesStaticLst.Where(c => c.Value.Contains(SearchTxt));
                        DeclarationTypes = new ObservableCollection<DeclarationType>(res);
                    }
                   /* else
                    {
                        DeclarationTypes = DeclartionTpesStaticLst;
                    }*/
                    if (!String.IsNullOrEmpty(SearchTxt) && IsCarrierOpened)
                    {
                        var res = CarriersStaticLst.Where(c => c.carr_name.Contains(SearchTxt));
                        Carriers = new ObservableCollection<Carrier>(res);
                    }
                    /*else
                    {
                        Carriers = CarriersStaticLst;
                    }*/

                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }


    }
}
