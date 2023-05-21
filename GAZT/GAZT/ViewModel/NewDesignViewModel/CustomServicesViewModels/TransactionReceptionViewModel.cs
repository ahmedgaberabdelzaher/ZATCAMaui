using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.CustomServices.Tawreed;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class TransactionReceptionViewModel: BaseViewModel
    {
        string userType = "1";
        public string UserType { get { return userType; } set { userType = value; RaisePropertyChanged(); } }

         bool isSuccessView=false;
        public bool IsSuccessView { get { return isSuccessView; } set { isSuccessView = value; RaisePropertyChanged(); } }

        bool isEntity = false;
        public bool IsEntity { get { return isEntity; } set { isEntity = value; RaisePropertyChanged(); } }

        bool isAddNewCR ;
        public bool IsAddNewCR { get { return isAddNewCR; } set { isAddNewCR = value; RaisePropertyChanged(); } }


        string email ;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }

        string subject;
        public string Subject { get { return subject; } set { subject = value; RaisePropertyChanged(); } }

        string description;
        public string Description { get { return description; } set { description = value; RaisePropertyChanged(); } }

        string crNo="";
        public string CRNo { get { return crNo; } set { crNo = value; RaisePropertyChanged(); } }

        string selectedCRNo = "";
        public string SelectedCRNo { get { return selectedCRNo; } set { selectedCRNo = value; RaisePropertyChanged(); } }

        bool isOpenAddNewCr;
        public bool IsOpenAddNewCr { get { return isOpenAddNewCr; } set { isOpenAddNewCr = value; RaisePropertyChanged(); } }


        ObservableCollection<UserCRResponseModel> cRLst;
        public ObservableCollection<UserCRResponseModel> CRLst { get { return cRLst; } set { cRLst = value; RaisePropertyChanged(); } }


        public static ObservableCollection<UserCRResponseModel> CRCashedList;
        public static bool isCRDataFetched = false;

        public int IamRegisteredUserID { get; set; } = 1837784;
        public string MobileNo { get; set; } ="0551844232";

        public string NationalId { get; set; } = "1068253721";

        public ICommand SelectUserTypeCommand
        {
            get
            {
                return new Command<string>((selectedType) =>
                {
                    IsEntity = selectedType == "2" ? true : false;
                    UserType = selectedType;
                    IsAddNewCR = false;
                });
            }
        }


        public ICommand DisplayAddNewCRViewCommand
        {
            get
            {
                return new Command<string>(async(e) =>
                {
                    IsAddNewCR = e=="1"?true:false;
                    if (!IsAddNewCR)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        CRNo = "";
                    }
                    else
                    {
                        NewCrPopupView poupWindow = new NewCrPopupView();
                        await PopupNavigation.Instance.PushAsync(poupWindow);
                    }
                });
            }
        }

        public ICommand SendTransactionCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        IsLoading = true;
                        Regex EmailRgx = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

                        if (!string.IsNullOrWhiteSpace(Description)&& !string.IsNullOrWhiteSpace(Subject)&& !string.IsNullOrWhiteSpace(Email))
                    {
                     if (!EmailRgx.IsMatch(Email.ToLower()))
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidEmailFormat;
                                return;
                            }
                            if (UploadedFiles == null || UploadedFiles.Count <=0)
                            {
                                MessageTxt = AppResources.NoFileChoosen;
                                IsShowMsgView = true;
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(SelectedCRNo)&&IsEntity)
                            {
                                MessageTxt = AppResources.RequiredData;
                                IsShowMsgView = true;
                                return;

                            }
                                                 var model = new TawreedSubmitFormModel()
                        {
                            departmentTypeId = int.Parse(UserType),
                            description = Description,
                            email = Email,
                            CrNumber=String.IsNullOrWhiteSpace(SelectedCRNo)?"null": SelectedCRNo,
                            referenceNumber="e",
                            mobileNumber=MobileNo,
                          //  mobileNumber="+966590768641",
                            subject = Subject,
                            attachement = new Attachement()
                            {
                                fileContent = UploadedFiles.FirstOrDefault().fileBase64,
                                fileName = UploadedFiles.FirstOrDefault().fileFullName

                            },
                            IamRegisteredUserID= IamRegisteredUserID
                        };
                        var response =await _twareedServices.TawreedSubmitForm(model);
                        if (response.IsSuccessStatusCode)
                        {
                            var content =await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<SubmitFormResponse>(content);
                            if (result.header.status.code== "I000000")
                            {
                                var refNo =result.result.referenceNumber;
                                _navigationService.NavigateTo("/SuccessView",refNo.ToString());
                                    clearData();
                            }
                     else
                            {
                                MessageTxt = AppResources.ServerError;
                                IsShowMsgView = true;
                            }

                        }
                        else
                        {
                            MessageTxt = AppResources.RequestTimeoutDescription;
                            IsShowMsgView = true;
                        }
                    }
                        else
                        {
                            MessageTxt = AppResources.RequiredData;
                            IsShowMsgView = true;
                        }
                      
                    }
                    catch (Exception ex)
                    {

                    }
                    finally { IsLoading = false; }
                });
            }
        }


        public ICommand AddNewCRCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;

                        if (!string.IsNullOrWhiteSpace(CRNo))
                        {
                            await PopupNavigation.Instance.PopAsync(true);
                            var model = new AddNewCrBody()
                            {
                                 crNumber=CRNo,
                                  idNumber=NationalId,
                                   registeredUserID=IamRegisteredUserID
                            };
                            var response = await _twareedServices.TawreedAddNewCr(model);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<SubmitFormResponse>(content);
                                if (result.header.status.code == "I000000")
                                {
                                   // await PopupNavigation.Instance.PopAsync(true);
                                    isCRDataFetched = false;
                                    IsOpenAddNewCr = false;
                                    CRNo = "";
                                    MessageTxt = AppResources.CRNoAddedSuccess;
                                    IsShowMsgView = true;
                                    return;
                                }
                                else
                                {
                                    if (result.header.moreInformation!=null&& result.header.moreInformation.Errordetails!= null && result.header.moreInformation.Errordetails.Count > 0)
                                    {
                                        MessageTxt = result.header.moreInformation.Errordetails[0];
                                        IsShowMsgView = true;
                                        
                                        return;
                                    }
                                    MessageTxt = AppResources.RequestTimeoutDescription;
                                    IsShowMsgView = true;
                                }

                            }
                            else
                            {
                                MessageTxt = AppResources.RequestTimeoutDescription;
                                IsShowMsgView = true;
                                IsLoading = false;
                            }
                        }
                        else
                        {
                            MessageTxt = AppResources.RequiredData;
                            IsShowMsgView = true;
                            IsLoading = false;
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    finally {
                        IsLoading = false;
                        CRNo = "";
                    }
                });
            }
        }

        public ICommand GetCurrentUserCRCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!isCRDataFetched)
                    {
                     await GetCurrentUserCR();
                    }
                    else
                    {
                       
                        CRLst = CRCashedList;
                        IsShowBottomSheet = true;
                    }
             
                });
            }
        }

        private async Task GetCurrentUserCR()
        {
            try
            {
                IsLoading = true;
                var response = await _twareedServices.GetUserCRs(IamRegisteredUserID);
                if (response.Item2)
                {
                    if (response.Item1.header.status.code == "I000000")
                    {
                        var data = response.Item1.data;

                       CRCashedList = data;
                        var result = CRCashedList.Select(c => new BottomSheetModel() { Id = "1", Name = c.Name==""?c.crType:c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
                        TempBottomSheetList = BottomSheetList;
                        isCRDataFetched = true;
                    }
                    else
                    {
                        if (response.Item1?.header.moreInformation != null && response.Item1?.header.moreInformation.Errordetails != null && response.Item1?.header.moreInformation.Errordetails.Count > 0)
                        {
                            MessageTxt = response.Item1.header.moreInformation.Errordetails[0];
                            IsShowMsgView = true;

                            return;
                        }
                        MessageTxt = AppResources.RequestTimeoutDescription;
                        IsShowMsgView = true;
                    }

                }
                else
                {
                    MessageTxt = AppResources.RequestTimeoutDescription;
                    IsShowMsgView = true;
                }

            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    try
                    {
                      //  IsLoading = true;
                        SelectedCRNo = e.Name;
                        IsShowBottomSheet = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }


                });
            }
        }

        public void clearData()
        {
            Email = Subject = Description =CRNo= SelectedCRNo = "";
            UploadedFiles = new ObservableCollection<ReportFileModel>();
            UserType = "1"; 
            IsEntity = false; isCRDataFetched = false;
            IsShowBottomSheet=IsShowMsgView = false;
            IsAddNewCR = false;
        }

        public override ICommand BackCommand {

            get
            {
                return new Command(() => {

                    if (IsShowBottomSheet)
                    {
                        IsShowBottomSheet = false;
                        return;
                    }
                    _navigationService.GoBack();
                    clearData();
                });
            }
        }

        public ICommand BackToHomeCommand
        {
            get
            {
                return new Command(() =>
                {
                   
                    _navigationService.NavigateTo("/Home","0");
                });
            }
        }
       
        public ICommand BackToCustomServicesCommand
        {
            get
            {
                return new Command( () =>
                {
                    _navigationService.NavigateTo("/CusromServiceMenu");
                });
            }
        }

        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" }, AppResources.PDFFileHint, AppResources.NumberofAttachments);
                });
            }
        }
        ITwareedServices _twareedServices;

        public TransactionReceptionViewModel(INavigationService navigationServices, IDialogService dialogService, ITwareedServices twareedServices) : base(navigationServices, dialogService)
        {
            _twareedServices = twareedServices;
        }

        ObservableCollection<BottomSheetModel> bottomSheetList;
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> tempBottomSheetList;
        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get { return tempBottomSheetList; } set { tempBottomSheetList = value; RaisePropertyChanged(); } }

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        public void SetUserData(string token)
        {
            var data = GetTokenData(token);
            if (data!=null)
            {
            IDictionary<string, object> iamLoginPayloadData = data as IDictionary<string, object>;
            // IamLoginPayloadData.TryGetValue("FirstName",out SubmitModel.travelerDeclaration.firstName);
            NationalId = iamLoginPayloadData["NationlId"].ToString();
            MobileNo ="+966"+ iamLoginPayloadData["Mobile"].ToString();
            IamRegisteredUserID = int.Parse(iamLoginPayloadData["Id"].ToString());
            }
        }


    }
}

