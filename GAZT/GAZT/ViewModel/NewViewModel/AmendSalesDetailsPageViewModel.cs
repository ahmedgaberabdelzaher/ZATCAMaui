using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class AmendSalesDetailsPageViewModel: ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static SalesDetails SelectedSalesDetails = new SalesDetails();
        //  public ICommand OnBillsButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnZakatReturnDataUpdateClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnDeleteAttachmentClickedTapped { get; set; }

        
        public RootObject rootObject { get; set; }
        
        byte[] attachment;

        #endregion

        #region Property
        private ObservableCollection<EstimateZakatAttachment> _zakatReturnAttachmentsList;
        public ObservableCollection<EstimateZakatAttachment> ZakatReturnAttachmentsList
        {
            get
            {
                return _zakatReturnAttachmentsList;
            }
            set
            {
                _zakatReturnAttachmentsList = value;
                RaisePropertyChanged("ZakatReturnAttachmentsList");
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

        private string _newValue = "";
        public string NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                _newValue = value;
                if(NewValue != null)
                {
                    SelectedSalesDetails.NewValue = NewValue;
                }
                RaisePropertyChanged("NewValue");
            }
        }

        private string _oldValue = "";
        public string OldValue
        {
            get
            {
                return _oldValue;
            }
            set
            {
                _oldValue = value;
                if (OldValue != null)
                {
                    SelectedSalesDetails.OldValue = _oldValue;
                }
                RaisePropertyChanged("OldValue");
            }
        }

        
        private string _changeReason = "";
        public string ChangeReason
        {
            get
            {
                return _changeReason;
            }
            set
            {
                _changeReason = value;
                if (ChangeReason != null)
                {
                    SelectedSalesDetails.ChangeReason = ChangeReason;
                    if (SelectedSalesDetails.IsReasonRequird)
                    {
                        SelectedSalesDetails.IsReasonRequird = false;
                    }                   
                }
                RaisePropertyChanged("ChangeReason");
            }
        }

        

        private string _attachmentName = "";
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                _attachmentName = value;
                if (AttachmentName != null)
                {
                    SelectedSalesDetails.AttchamentName = AttachmentName;
                    if(SelectedSalesDetails.IsAttachmentRequired)
                    {
                        SelectedSalesDetails.IsAttachmentRequired = false;
                    }
                }
                RaisePropertyChanged("AttachmentName");
            }
        }

        private string _attachmentNumber = "";
        public string AttachmentNumber
        {
            get
            {
                return _attachmentNumber;
            }
            set
            {
                _attachmentNumber = value;
                if (_attachmentNumber != null)
                {
                    SelectedSalesDetails.AttchamentNumber = AttachmentNumber;
                }
                RaisePropertyChanged("AttachmentNumber");
            }
        }

        private string _salesType = "";
        public string SalesType
        {
            get
            {
                return _salesType;
            }
            set
            {
                _salesType = value;
                if (_attachmentNumber != null)
                RaisePropertyChanged("SalesType");
            }
        }
        
        #endregion

        #region Constructor
        public AmendSalesDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;



            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }


            OnZakatReturnDataUpdateClicked = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    _navigationService.GoBack();
                    // _navigationService.NavigateTo(App.ZakatBillDetailsPageView);
                 
                }
                catch(Exception ex)
                {

                }
            });

            //OnDeleteAttachmentClickedTapped = new Xamarin.Forms.Command(async () =>
            //{
            //    try
            //    {
            //        DeleteSelectedAttachment();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //});
            

            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    var fileData = await CrossFilePicker.Current.PickFile();
                    attachment = fileData.DataArray;
                    AttachmentName = fileData.FileName;
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await Task.Run(async() =>
                    {
                    try
                    {
                            if(attachment.Length < 5242880)
                            {
                                if(ZakatReturnAttachmentsList.Count < 5)
                                {
                                    AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(attachment, AttachmentName, SalesDetailsPageViewModel.RetGuid, "Z12L");
                                    PopToRootPage();
                                    if (_attachment != null && _attachment.d != null)
                                    {
                                        EstimateZakatAttachment _estimateZakatAttachment = new EstimateZakatAttachment();
                                        _estimateZakatAttachment.Doguid = _attachment.d.Doguid;
                                        _estimateZakatAttachment.Seqno = "";
                                        _estimateZakatAttachment.SchGuid = "";
                                        _estimateZakatAttachment.AttBy = "";
                                        _estimateZakatAttachment.FileExtn = "";
                                        _estimateZakatAttachment.ByPusr = "";
                                        _estimateZakatAttachment.OutletRef = "";
                                        _estimateZakatAttachment.Filename = _attachment.d.Filename;
                                        _estimateZakatAttachment.RetGuid = _attachment.d.RetGuid;
                                        _estimateZakatAttachment.Dotyp = "FZ01";
                                        _estimateZakatAttachment.Mimetype = "";
                                        _estimateZakatAttachment.DocUrl = _attachment.d.DocUrl;
                                        _estimateZakatAttachment.DataVersion = "";
                                        DateTime currentDate = DateTime.Now;
                                        _estimateZakatAttachment.Erfdt = "/Date(1546300800000)/";// need to
                                        SelectedSalesDetails.estimateZakatAttachment.Add(_estimateZakatAttachment);

                                        // ZakatReturnAttachmentsList.Add(_estimateZakatAttachment);
                                    }
                                }
                                else
                                {
                                    await _dialogService.ShowMessage("You can not upload more than 5 attachment", AppResources.Alerts);
                                }
                            }
                            else
                            {
                                await _dialogService.ShowMessage("File size must be less than 5 MB", AppResources.Alerts);
                            }

                        }
                        catch (InternetException ex)
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        }
                    });
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                   

                }
                catch (Exception ex)
                {


                }

            });
        }
        #endregion

        #region Method
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
        public void ClearData()
        {
            //SalesType = string.Empty;
            //OldValue = string.Empty;
            //NewValue = string.Empty;
            //ChangeReason = string.Empty;
            //ZakatReturnAttachmentsList = new ObservableCollection<EstimateZakatAttachment>();
        }
        public void OnLoad()
        {
            SalesType = SelectedSalesDetails.SalesType;
            OldValue = SelectedSalesDetails.InformationFromPartie;
            NewValue =SelectedSalesDetails.NewValue;
            ChangeReason = SelectedSalesDetails.ChangeReason;
            ZakatReturnAttachmentsList = SelectedSalesDetails.estimateZakatAttachment;
            // int k = 5;
            //ObservableCollection<EstimateZakatAttachment> ZakatAttachment = new ObservableCollection<EstimateZakatAttachment>();
            //ZakatReturnAttachmentsList = new List<SalesDetailsAttachments>();
            //for (k = 0; k < 6; k++)
            //{
            //    SalesDetailsAttachments m = new SalesDetailsAttachments();
            //    m.Id = "0001";
            //    m.DocumentName = "Test-Document.pdf";
            //    m.Size = "20.00";

            //    ZakatAttachment.Add(m);
            //}
           // ZakatReturnAttachmentsList = ZakatAttachment;
        }

        public async Task  DeleteSelectedAttachment(string filename, string dougUD)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(() =>
            {
                try
                {
                    string res = WebServiceManager.GAZTDeleteEstimatedZAKATRAttachment(filename, dougUD);
                    PopToRootPage();
                    if (res.Equals("X") && ZakatReturnAttachmentsList.Count > 0)
                    {
                        for (int i = 0; i < ZakatReturnAttachmentsList.Count; i++)
                        {
                            if (ZakatReturnAttachmentsList[i].Doguid.Equals(dougUD))
                            {
                                ZakatReturnAttachmentsList.RemoveAt(i);
                            }
                        }
                    };
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });
           
        }
        #endregion
    }
}
