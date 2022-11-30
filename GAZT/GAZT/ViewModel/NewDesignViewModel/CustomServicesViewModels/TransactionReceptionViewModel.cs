using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Models.CustomServices.Tawreed;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
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

        string crNo="null";
        public string CRNo { get { return crNo; } set { crNo = value; RaisePropertyChanged(); } }


        public int IamRegisteredUserID { get; set; } = 123;
        public string MobileNo { get; set; } ="0551844232";

        public ICommand SelectUserTypeCommand
        {
            get
            {
                return new Command<string>((selectedType) =>
                {
                    IsEntity = selectedType == "2" ? true : false;
                    UserType = selectedType;
                });
            }
        }


        public ICommand DisplayAddNewCRViewCommand
        {
            get
            {
                return new Command<bool>((flag) =>
                {
                    IsAddNewCR = flag;
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
                        if (!string.IsNullOrWhiteSpace(Description)&& !string.IsNullOrWhiteSpace(Subject)&& !string.IsNullOrWhiteSpace(Email)&&UploadedFiles!=null&&UploadedFiles.Count>0)
                    {
                        var model = new TawreedSubmitFormModel()
                        {
                            departmentTypeId = int.Parse(UserType),
                            description = Description,
                            email = Email,
                            CrNumber=CRNo,
                            referenceNumber="e",
                            mobileNumber=MobileNo,
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
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" },1);
                });
            }
        }
        ITwareedServices _twareedServices;

        public TransactionReceptionViewModel(INavigationService navigationServices, IDialogService dialogService, ITwareedServices twareedServices) : base(navigationServices, dialogService)
        {
            _twareedServices = twareedServices;
        }


    }
}

