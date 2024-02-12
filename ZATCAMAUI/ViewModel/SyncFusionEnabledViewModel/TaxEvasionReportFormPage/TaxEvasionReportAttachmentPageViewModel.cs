using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage
{
    public class TaxEvasionReportAttachmentPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand SubmitReportClicked { get; set; }

        public ICommand OnAttachmentClick { get; set; }
        public static decimal AttachmentUploadedSize;
        byte[] attachment;
        public decimal _attachmentSize = 0;
        public decimal AttachmentSize
        {
            get
            {
                return _attachmentSize;
            }
            set
            {
                _attachmentSize = value;
                RaisePropertyChanged("AttachmentSize");
            }
        }
        public decimal _totalAttachmentSize = 0;
        public decimal TotalAttachmentSize
        {
            get
            {
                return _totalAttachmentSize;
            }
            set
            {
                _totalAttachmentSize = value;
                RaisePropertyChanged("TotalAttachmentSize");
            }
        }
        private bool _isLoading = false;
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

        private SignUpModelRootObject _signUpModelRootObjectM = null;
        public SignUpModelRootObject SignUpModelRootObjectM
        {
            get
            {
                return _signUpModelRootObjectM;
            }
            set
            {
                _signUpModelRootObjectM = value;
                RaisePropertyChanged("SignUpModelRootObjectM");
            }
        }
        private bool _isSubmitButtonEnable = false;
        public bool IsSubmitButtonEnable
        {
            get
            {
                return _isSubmitButtonEnable;
            }
            set
            {
                _isSubmitButtonEnable = value;
                RaisePropertyChanged("IsSubmitButtonEnable");
            }
        }

        private TaxEvasionReportDetails _selectedtaxEList = null;
        public TaxEvasionReportDetails selectedtaxEList
        {
            get
            {
                return _selectedtaxEList;
            }
            set
            {
                _selectedtaxEList = value;
                RaisePropertyChanged("selectedtaxEList");
            }
        }
        private TaxEvasionReportDetails _TaxEvasionReportTobeUsedToSubmit;
        public TaxEvasionReportDetails TaxEvasionReportTobeUsedToSubmit
        {
            get
            {
                return _TaxEvasionReportTobeUsedToSubmit;
            }
            set
            {
                _TaxEvasionReportTobeUsedToSubmit = value;
                if (_TaxEvasionReportTobeUsedToSubmit != null)
                {
                }
                RaisePropertyChanged("TaxEvasionReportTobeUsedToSubmit");
            }
        }
        private UploadedDocumentsList _uploadedDocumentsList = null;
        public UploadedDocumentsList UploadedDocumentsList
        {
            get
            {
                return _uploadedDocumentsList;
            }
            set
            {
                _uploadedDocumentsList = value;
                RaisePropertyChanged("UploadedDocumentsList");
            }
        }
        public int _attachmentCount = 0;
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                _attachmentCount = value;
                RaisePropertyChanged("AttachmentCount");
            }
        }
        private ObservableCollection<UploadedDocumentsList> _uploadedDocumentsListObj = new ObservableCollection<UploadedDocumentsList>();
        public ObservableCollection<UploadedDocumentsList> UploadedDocumentsListObj
        {
            get
            {
                return _uploadedDocumentsListObj;
            }
            set
            {
                _uploadedDocumentsListObj = value;
                RaisePropertyChanged("UploadedDocumentsListObj");
            }
        }
        private double _latitude = 00.00;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                RaisePropertyChanged("Latitude");
            }
        }
        private double _longitude = 00.00;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                RaisePropertyChanged("Longitude");
            }
        }
        private string _attachmentName = string.Empty;
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                RaisePropertyChanged("AttachmentName");
            }
        }
        public TaxEvasionReportAttachmentPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

            SubmitReportClicked = new Command(async () =>
            {

                await Task.Run(() =>
                {
                    App.DisplayProgressView();
                });

                await SubmitCreatedReport();
            });
            OnAttachmentClick = new Command(async () =>
            {
                await AddAttachment();
            });
        }

        public async Task SubmitCreatedReport()
        {
            try
            {
                List<UploadedDocumentsList> newList = UploadedDocumentsListObj.ToList();

                TaxEvasionReportTobeUsedToSubmit.Latitude = _latitude.ToString();
                TaxEvasionReportTobeUsedToSubmit.Longitude = _longitude.ToString();

                TaxEvasionCreateReportResponseModel response = new TaxEvasionCreateReportResponseModel();
                response = await TaxEvasionWebServiceManager.GAZTTaxEvasionCreateReport(TaxEvasionReportTobeUsedToSubmit, newList);

                if (response != null && response.Status == true)
                {
                    //ZTEReportReportSuccessResponsep1
                    var resmessage = AppResources.ZTEReportReportSuccessResponsep1;
                    var newrm = resmessage.Replace("Report Number", response.Data.TicketId);
                    var newReplacedMsg = newrm.Replace("5", "10");

                    await _dialogService.ShowMessage(newReplacedMsg, AppResources.ZZZSubmittedReport);
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage);
                    var _lastPage2 = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage2);
                    //Go back 

                    _navigation.PopAsync();
                    //_navigationService.NavigateTo(App.TaxEvasionReportListPageView);
                }
                else
                {//ZTEReportReportSuccessResponsep2
                    _dialogService.ShowMessage(AppResources.ZTEReportReportSuccessResponsep2, " ");
                }
                await Task.Run(() =>
                {
                    App.HideProgressView();
                });
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     await Task.Run(() =>
                     {
                         IsLoading = false;
                     });

                     _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                     //viewModel._navigationService.GoBack();
                 });
            }
            catch (Exception)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     await Task.Run(() =>
                     {
                         IsLoading = false;
                     });

                     await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                 });
            }
        }
        public async Task AddAttachment()
        {
            if (AttachmentCount < 3)
            {
                string[] filetypes;

                filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);

                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                attachment = UtilityManager.ReadFully(stream as Stream);
                if (fileData != null)
                {

                    string base64String = Convert.ToBase64String(attachment, 0, attachment.Length);
                    AttachmentName = fileData.FileName;


                    if (fileData.FileName.Contains("."))
                    {
                        string[] ExtensionArray = fileData.FileName.Split('.');
                        string Extention = ExtensionArray.Last();
                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                        {
                            if (TotalAttachmentSize <= 30)
                            {
                                AttachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 2);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);
                                if (Convert.ToDecimal(AttachmentSize) <= 10)
                                {
                                    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                    {
                                        bool isAttachmentexixt = false;

                                        try
                                        {
                                            UploadedDocumentsList a = new UploadedDocumentsList();
                                            a.FileNameWithExtension = AttachmentName;
                                            a.DocBinaryInBase64 = attachment;

                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                            a.MimeType = attachmentType;
                                            foreach (UploadedDocumentsList ItemA in UploadedDocumentsListObj)
                                            {
                                                if (AttachmentName == ItemA.FileNameWithExtension)
                                                {
                                                    isAttachmentexixt = true;
                                                }
                                            }
                                            if (isAttachmentexixt == false)
                                            {
                                                UploadedDocumentsListObj.Add(a);
                                                AttachmentCount++;
                                                AttachmentName = string.Empty;
                                            }
                                            else
                                            {
                                                AttachmentName = string.Empty;
                                                _dialogService.ShowMessage(AppResources.ZZGeneralMessage_FileWithTheSameNameAlreadyExists, AppResources.Information);
                                            }

                                        }
                                        catch (Exception)
                                        {


                                        }
                                    }
                                    else
                                    {
                                        AttachmentName = string.Empty;
                                        _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    }
                                }
                            }
                        }
                        else
                        {
                            _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        }
                    }
                }
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     var _navigation = Application.Current.MainPage.Navigation;
                     await _navigation.PopToRootAsync();
                 });
            }
        }
    }
}

