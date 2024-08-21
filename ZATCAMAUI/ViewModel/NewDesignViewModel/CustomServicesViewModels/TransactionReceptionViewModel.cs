using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.CustomServices.Tawreed;
using ZATCAMAUI.Models.NativeNafath;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class TransactionReceptionViewModel : BaseViewModel
    {
        string userType = "1";
        public string UserType { get { return userType; } set { userType = value; OnPropertyChanged(); } }

        bool isSuccessView = false;
        public bool IsSuccessView { get { return isSuccessView; } set { isSuccessView = value; OnPropertyChanged(); } }

        bool isEntity = false;
        public bool IsEntity { get { return isEntity; } set { isEntity = value; OnPropertyChanged(); } }

        bool isAddNewCR;
        public bool IsAddNewCR { get { return isAddNewCR; } set { isAddNewCR = value; OnPropertyChanged(); } }


        string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }

        string subject;
        public string Subject { get { return subject; } set { subject = value; OnPropertyChanged(); } }

        string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(); } }

        string crNo = "";
        public string CRNo { get { return crNo; } set { crNo = value; OnPropertyChanged(); } }

        string selectedCRNo = "";
        public string SelectedCRNo { get { return selectedCRNo; } set { selectedCRNo = value; OnPropertyChanged(); } }

        bool isOpenAddNewCr;
        public bool IsOpenAddNewCr { get { return isOpenAddNewCr; } set { isOpenAddNewCr = value; OnPropertyChanged(); } }

        int buildingNo;
        public int BuildingNo { get { return buildingNo; } set { buildingNo = value; OnPropertyChanged(); } }

        string streetName;
        public string StreetName { get { return streetName; } set { streetName = value; OnPropertyChanged(); } }

        string districtName;
        public string DistrictName { get { return districtName; } set { districtName = value; OnPropertyChanged(); } }

        string cityName;
        public string CityName { get { return cityName; } set { cityName = value; OnPropertyChanged(); } }

        string postalCode;
        public string PostalCode { get { return postalCode; } set { postalCode = value; OnPropertyChanged(); } }


        string seconderyNo;
        public string SeconderyNo { get { return seconderyNo; } set { seconderyNo = value; OnPropertyChanged(); } }

        string registrationNo;
        public string RegistrationNo { get { return registrationNo; } set { registrationNo = value; OnPropertyChanged(); } }

        long tIN;
        public long TIN { get { return tIN; } set { tIN = value; OnPropertyChanged(); } }

        bool isTinNoVisible;
        public bool IsTinNoVisible { get { return isTinNoVisible; } set { isTinNoVisible = value; OnPropertyChanged(); } }



        ObservableCollection<UserCRResponseModel> cRLst;
        public ObservableCollection<UserCRResponseModel> CRLst { get { return cRLst; } set { cRLst = value; OnPropertyChanged(); } }


        ObservableCollection<ReportFileModel> transactionUploadedFiles = new ObservableCollection<ReportFileModel>();
        public ObservableCollection<ReportFileModel> TransactionUploadedFiles { get { return transactionUploadedFiles; } set { transactionUploadedFiles = value; OnPropertyChanged(); } }

        public static ObservableCollection<UserCRResponseModel> CRCashedList;
        public static bool isCRDataFetched = false;

        public int IamRegisteredUserID { get; set; } = 1837784;
        public string MobileNo { get; set; } = "0551844232";

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
                    if (!IsEntity)
                    {
                        IsTinNoVisible = false;
                    }

                });
            }
        }


        public ICommand DisplayAddNewCRViewCommand
        {
            get
            {
                return new Command<string>(async (e) =>
                {
                    // Don't remove try & catch as there is an Known issue in Syncfusion text-input
                    // Can't dispose empty object 
                    try
                    {
                        IsAddNewCR = e == "1" ? true : false;
                        if (!IsAddNewCR)
                        {
                            await MopupService.Instance.PopAsync(true);
                            CRNo = "";
                        }
                        else
                        {
                            NewCrPopupView poupWindow = new NewCrPopupView();
                            await MopupService.Instance.PushAsync(poupWindow);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    
                });
            }
        }

        public ICommand SendTransactionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {

                        IsLoading = true;
                        if (!int.TryParse(BuildingNo.ToString(), out int value) || BuildingNo.ToString().Length != 4)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.BuildingNoValidationMsg;
                            return;
                        }
                        if (!int.TryParse(postalCode, out int e) || postalCode.Length != 5)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.PostalValidationMsg;
                            return;
                        }
                        if (!int.TryParse(SeconderyNo, out int a) || SeconderyNo.Length != 4)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.additionalNoValidationMsg;
                            return;
                        }
                        Regex EmailRgx = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

                        if (!string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Subject) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(CityName) && !string.IsNullOrWhiteSpace(StreetName))
                        {
                            if (!EmailRgx.IsMatch(Email.ToLower()))
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidEmailFormat;
                                return;
                            }
                            if (TransactionUploadedFiles == null || TransactionUploadedFiles.Count <= 0)
                            {
                                MessageTxt = AppResources.NoFileChoosen;
                                IsShowMsgView = true;
                                return;
                            }
                            if (String.IsNullOrWhiteSpace(SelectedCRNo) && IsEntity)
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
                                CrNumber = String.IsNullOrWhiteSpace(SelectedCRNo) ? "null" : SelectedCRNo,
                                referenceNumber = "e",
                                mobileNumber = MobileNo,
                                //  mobileNumber="+966590768641",
                                subject = Subject,
                                attachement = new Attachement()
                                {
                                    fileContent = TransactionUploadedFiles.FirstOrDefault().fileBase64,
                                    fileName = TransactionUploadedFiles.FirstOrDefault().fileFullName

                                },
                                iamRegisteredUserID = IamRegisteredUserID,
                                buildingNumber = BuildingNo,
                                streetNumber = StreetName,
                                districtNumber = DistrictName,
                                cityName = CityName,
                                additionalNumber = int.Parse(SeconderyNo),
                                postCode = int.Parse(PostalCode),
                                TIN = TIN
                            };
                            var response = await _twareedServices.TawreedSubmitForm(model);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<SubmitFormResponse>(content);
                                if (result.header.status.code == "I000000")
                                {
                                    var refNo = result.result.referenceNumber;
                                    _navigationService.NavigateTo("/SuccessView", refNo.ToString());
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
                    catch (Exception)
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

                        if (!string.IsNullOrWhiteSpace(CRNo) && CRNo.Length == 10)
                        {
                            await MopupService.Instance.PopAsync(true);
                            var model = new AddNewCrBody()
                            {
                                crNumber = CRNo,
                                idNumber = NationalId,
                                registeredUserID = IamRegisteredUserID
                            };
                            var response = await _twareedServices.TawreedAddNewCr(model);
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<SubmitFormResponse>(content);
                                if (result.header.status.code == "I000000")
                                {
                                    // await MopupService.Instance.PopAsync(true);
                                    isCRDataFetched = false;
                                    IsOpenAddNewCr = false;
                                    CRNo = "";
                                    MessageTxt = AppResources.CRNoAddedSuccess;
                                    IsShowMsgView = true;
                                    return;
                                }
                                else
                                {
                                    if (result.header.moreInformation != null && result.header.moreInformation.errorDetails != null && result.header.moreInformation.errorDetails.Count > 0)
                                    {
                                        MessageTxt = result.header.moreInformation.errorDetails[0].message;
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
                            await MopupService.Instance.PopAsync(true);
                            MessageTxt = AppResources.RequiredData;
                            IsShowMsgView = true;
                            IsLoading = false;
                        }

                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
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
                        var result = CRCashedList.Select(c => new BottomSheetModel() { Id = c.crType, Name = c.Name == "" ? c.crType : c.Name }).ToList() ?? new List<BottomSheetModel>();
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
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }
        private async Task GetCurrentUserCRTinNO(string crNo)
        {
            try
            {
                IsLoading = true;
                TIN = 0;
                IsTinNoVisible = false;
                var response = await _twareedServices.GetCurrentCRTiNo(crNo);
                if (response.Item2)
                {
                    if (response.Item1.header.status.code == "I000000")
                    {
                        var data = response.Item1.data;

                        if (data.taxpayers != null && data.taxpayers.Count > 0)
                        {
                            var act = data.taxpayers.FirstOrDefault(c => c.idStatus == "ACTIVE");
                            if (act != null)
                            {
                                if (!String.IsNullOrEmpty(act.TINNumber))
                                {
                                    TIN = long.Parse(act.TINNumber);
                                    IsTinNoVisible = true;
                                }
                            }
                        }

                    }
                    else
                    {

                        if (response.Item1?.header.status.code == "E200405")
                        {
                            MessageTxt = response.Item1.header.status.description;
                            IsShowMsgView = true;

                            return;
                        }
                        if (response.Item1.header.moreInformation != null && response.Item1.header.moreInformation.Errordetails != null && response.Item1.header.moreInformation.Errordetails.Count > 0)
                        {
                            MessageTxt = response.Item1.header.moreInformation.Errordetails[0];
                            IsShowMsgView = true;

                            return;
                        }
                        //   MessageTxt = AppResources.RequestTimeoutDescription;
                        //   IsShowMsgView = true;
                    }

                }
                else
                {
                    // MessageTxt = AppResources.RequestTimeoutDescription;
                    // IsShowMsgView = true;
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
                return new Command<BottomSheetModel>(async (e) =>
                {
                    try
                    {
                        //  IsLoading = true;
                        SelectedCRNo = e.Name;
                        await GetCurrentUserCRTinNO(e.Id);
                        IsShowBottomSheet = false;
                    }
                    catch (Exception)
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
            Email = Subject = Description = CRNo = SelectedCRNo = StreetName = CityName = DistrictName = PostalCode = SeconderyNo = "";
            TIN = 0;
            BuildingNo = 0;
            TransactionUploadedFiles = new ObservableCollection<ReportFileModel>();
            UserType = "1";
            IsEntity = false; isCRDataFetched = false;
            IsShowBottomSheet = IsShowMsgView = false;
            IsTinNoVisible = IsAddNewCR = false;

        }

        public override ICommand BackCommand
        {

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

                    _navigationService.NavigateTo("/Home", "0");
                });
            }
        }

        public ICommand BackToCustomServicesCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/CusromServiceMenu");
                });
            }
        }
        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<ReportFileModel>((file) =>
                {

                    if (file != null && TransactionUploadedFiles != null && TransactionUploadedFiles.Count > 0)
                    {
                        TransactionUploadedFiles.Remove(file);

                    }
                });
            }
        }
        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    TransactionUploadedFiles = await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" }, TransactionUploadedFiles, AppResources.PDFFileHint, AppResources.NumberofAttachments);
                });
            }
        }
        ITwareedServices _twareedServices;

        public TransactionReceptionViewModel(INavigationService navigationServices, IDialogService dialogService, ITwareedServices twareedServices) : base(navigationServices, dialogService)
        {
            _twareedServices = twareedServices;
        }

        ObservableCollection<BottomSheetModel> bottomSheetList;
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; OnPropertyChanged(); } }

        ObservableCollection<BottomSheetModel> tempBottomSheetList;
        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get { return tempBottomSheetList; } set { tempBottomSheetList = value; OnPropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; OnPropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged(); } }

        public void SetUserData(object payload)
        {

            if (payload != null)
            {
                var data = payload as CustomsIamUser;
                NationalId = data.nationalId.ToString();
                MobileNo = "+966" + data.mobileNumber.ToString();
                IamRegisteredUserID = data.id;
            }
        }


    }


}

