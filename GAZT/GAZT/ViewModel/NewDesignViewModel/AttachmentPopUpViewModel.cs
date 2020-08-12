using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Plugin.FilePicker;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class AttachmentPopUpViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //============================start===================================================
        public ICommand OnAttachmentClicked { get; set; }
        public ZakatReturnDetailsD ZakatReturnDetail;
        byte[] attachment;

        #region Property


        private ObservableCollection<ZakatAttachment> _zakatReturnAttachmentsList;
        public ObservableCollection<ZakatAttachment> ZakatReturnAttachmentsList
        {
            get
            {
                return _zakatReturnAttachmentsList;
            }
            set
            {
                _zakatReturnAttachmentsList = value;
                //if(ZakatReturnAttachmentsList != null)
                //{
                //    SetSaveButtonVisibility();
                //}
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
                   
                }
                RaisePropertyChanged("AttachmentName");
            }
        }
        #endregion

        #region Constructor
        public AttachmentPopUpViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnAttachmentClicked = new Xamarin.Forms.Command(() =>
            {

            });
        }
        #endregion

        #region Method

        public void OnPageLoad()
        {
            ZakatAttachment ZakatAttachment1 = new ZakatAttachment();
            ZakatAttachment1.Filename = "ABC.pdf";
            ZakatAttachment ZakatAttachment2 = new ZakatAttachment();
            ZakatAttachment2.Filename = "XYZ.pdf";
            ObservableCollection<ZakatAttachment> list = new ObservableCollection<ZakatAttachment>();
            list.Add(ZakatAttachment1);
            list.Add(ZakatAttachment2);
            ZakatReturnAttachmentsList = list;
        }
        public async Task AddAttachment()
        {
            try
            {
                string[] filetypes;

                filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForZakat();

                var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                attachment = fileData.DataArray;
                await Task.Run(() =>
                {
                   // IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        AttachmentName = fileData.FileName;
                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = AttachmentName.Split('.')[1];
                            string ContentType = UtilityManager.GetContentType(Extention);
                            bool isFileAlreayUploaded = IsFileAlreadyAttached(AttachmentName);
                            decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                            {
                                if (!isFileAlreayUploaded)
                                {
                                    if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls")
                                    {
                                        if (attachment.Length < 5242880)
                                        {
                                            if (ZakatReturnAttachmentsList.Count < 5)
                                            {
                                                AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(attachment, AttachmentName, ZakatReturnDetail.ReturnId, "Z12L", ContentType);
                                                PopToRootPage();
                                                if (_attachment != null && _attachment.d != null)
                                                {
                                                    AttachmentName = string.Empty;
                                                    EstimateZakatAttachment _estimateZakatAttachment = new EstimateZakatAttachment();
                                                    _estimateZakatAttachment.Doguid = _attachment.d.Doguid;
                                                    _estimateZakatAttachment.Seqno = string.Empty;
                                                    _estimateZakatAttachment.SchGuid = string.Empty;
                                                    _estimateZakatAttachment.AttBy = string.Empty;// DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’");// string.Empty;
                                                    _estimateZakatAttachment.FileExtn = string.Empty;
                                                    _estimateZakatAttachment.ByPusr = string.Empty;
                                                    _estimateZakatAttachment.OutletRef = string.Empty;
                                                    _estimateZakatAttachment.Filename = _attachment.d.Filename;
                                                    _estimateZakatAttachment.RetGuid = _attachment.d.RetGuid;
                                                    _estimateZakatAttachment.Dotyp = "FZ01";
                                                    _estimateZakatAttachment.Mimetype = string.Empty;
                                                    _estimateZakatAttachment.DocUrl = _attachment.d.DocUrl;
                                                    _estimateZakatAttachment.DataVersion = string.Empty;
                                                    DateTime currentDate = DateTime.Now.ToLocalTime();
                                                    long ticks = currentDate.Ticks;
                                                    //_estimateZakatAttachment.UploadedDate = currentDate.ToString();
                                                    TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                                                    string unixTime = span.TotalSeconds.ToString("N0");
                                                    unixTime = unixTime.Replace(",", "");
                                                    _estimateZakatAttachment.Erfdt = "" + "/Date(" + unixTime + ")/";// need to
                                                                                                                     //_estimateZakatAttachment.Erfdt = "/Date(" + unixTime + ")/";// need to
                                                    //SelectedSalesDetails.estimateZakatAttachment.Add(_estimateZakatAttachment);
                                                    //ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SelectedSalesDetails.estimateZakatAttachment);
                                                    //IsValueChanged();// 1584987294.32348//1584987210.06955
                                                                     // ZakatReturnAttachmentsList.Add(_estimateZakatAttachment);
                                                }
                                                else
                                                {
                                                    Device.BeginInvokeOnMainThread(async () =>
                                                    {
                                                        AttachmentName = string.Empty;
                                                        await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                        //IsLoading = false;
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    AttachmentName = string.Empty;
                                                    await _dialogService.ShowMessage(AppResources.ZZYoucannotuploadmorethan5attachment, AppResources.Alerts);
                                                    //IsLoading = false;
                                                });
                                            }
                                        }
                                        else
                                        {
                                            Device.BeginInvokeOnMainThread(async () =>
                                            {
                                                AttachmentName = string.Empty;
                                                await _dialogService.ShowMessage(AppResources.ZZFilesizemustbelessthan5MB, AppResources.Alerts);
                                                //IsLoading = false;
                                            });
                                        }
                                    }
                                    else
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            AttachmentName = string.Empty;
                                            await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                            //IsLoading = false;
                                        });
                                    }
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await _dialogService.ShowMessage(AppResources.ZZFileWithTheSameNameAlreadyExists, AppResources.Alerts);
                                        //IsLoading = false;
                                        AttachmentName = string.Empty;
                                    });
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                //IsLoading = false;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                });


                            }

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                AttachmentName = string.Empty;
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                //IsLoading = false;
                            });
                        }
                    }
                    catch (InternetException ex)
                    {
                        //IsLoading = false;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    //IsLoading = false;
                });
            }
            catch (Exception ex)
            {
            }

        }

        public void DeleteAttachment()
        {

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

        private bool IsFileAlreadyAttached(string FileName)
        {
            //bool isFileAlreadyAttached = false;
            //if (SelectedSalesDetails.estimateZakatAttachment != null)
            //{
            //    for (int i = 0; i < SelectedSalesDetails.estimateZakatAttachment.Count; i++)
            //    {
            //        if (SelectedSalesDetails.estimateZakatAttachment[i].Filename.Equals(FileName))
            //            isFileAlreadyAttached = true;
            //        else
            //            isFileAlreadyAttached = false;
            //        if (isFileAlreadyAttached)
            //            break;
            //    }
            //}
            //return isFileAlreadyAttached;
            return false;
        }

        public async Task DeleteSelectedAttachment(string filename, string dougUD)
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
                            //    SelectedSalesDetails.estimateZakatAttachment.RemoveAt(i);
                            }
                        }
                        //IsValueChanged();
                        //SetSaveButtonVisibility();
                    }
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
