using System.Collections.ObjectModel;
using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class AttachmentPopUpViewModel : BaseViewModel
    {
        public ICommand OnAttachmentClicked { get; set; }
        public ICommand OnSaveButtonClick { get; set; }
        public ZakatReturnDetailsD ZakatReturnDetail;
        public static List<SalesDetails> SalesDetailList = new List<SalesDetails>();
        byte[] attachment;
        public int SelectedSalesTypeIndex;
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
                OnPropertyChanged("ZakatReturnAttachmentsList");
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
                OnPropertyChanged("AttachmentName");
            }
        }


        private string _objectionReason = "";
        public string ObjectionReason
        {
            get
            {
                return _objectionReason;
            }
            set
            {
                _objectionReason = value;
                //if(!string.IsNullOrEmpty(ObjectionReason))
                //{
                //    SalesDetailList[SelectedSalesTypeIndex].ChangeReason = ObjectionReason;
                //}


                OnPropertyChanged("ObjectionReason");
            }
        }



        #endregion

        #region Constructor
        public AttachmentPopUpViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {


            OnSaveButtonClick = new Command(() =>
            {
                if (!string.IsNullOrEmpty(ObjectionReason))
                {
                    SalesDetailList[SelectedSalesTypeIndex].ChangeReason = ObjectionReason;

                }
                else
                {

                }
            });


        }
        #endregion

        #region Method

        public void OnPageLoad()
        {
            if (ZAKATReturnDetailsView.IsGoingFirstTimeOnAttachmentPage)
            {
                ZakatReturnAttachmentsList = new ObservableCollection<ZakatAttachment>();
                GetSalesTypeObjectList();
                ZAKATReturnDetailsView.IsGoingFirstTimeOnAttachmentPage = false;
            }


            SelectedSalesTypeIndex = GetSelectedSalesTypeIndex();
            if (SalesDetailList[SelectedSalesTypeIndex] != null)
            {
                ObjectionReason = SalesDetailList[SelectedSalesTypeIndex].ChangeReason;

                if (SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment.Count > 0)
                {
                    ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment);
                }
                else
                {
                    ZakatReturnAttachmentsList = null;
                }
            }
            else
            {
                ObjectionReason = string.Empty;
                ZakatReturnAttachmentsList = null;
            }

        }
        public async Task AddAttachment()
        {
            try
            {
                string[] filetypes;

                filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForZakat();
                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                //var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                attachment = UtilityManager.ReadFully(stream as Stream);
                //attachment = fileData.DataArray;
                IsLoading = true;
                await Task.Run(async () =>
                {
                    try
                    {
                        AttachmentName = fileData.FileName;
                        if (fileData.FileName.Contains("."))
                        {
                            //string Extention = AttachmentName.Split('.')[1];
                            string[] ExtentionArray = AttachmentName.Split('.');
                            string Extention = ExtentionArray.Last();
                            string ContentType = UtilityManager.GetContentType(Extention);
                            bool isFileAlreayUploaded = IsFileAlreadyAttached(AttachmentName);
                            decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachment.Length) / 1048576.0), 4);

                            if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                            {
                                if (!isFileAlreayUploaded)
                                {
                                    if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls")
                                    {
                                        if (attachment.Length < 5242880)
                                        {
                                            AttachmentRootOject _attachment = null;
                                            if (ZAKATReturnDetailsView.salesType.Equals("RealEstateValue"))
                                            {
                                                _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(stream, AttachmentName, ZakatReturnDetail.ReturnId, "Z12R", ContentType);
                                            }
                                            else
                                            {
                                                _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(stream, AttachmentName, ZakatReturnDetail.ReturnId, "Z12L", ContentType);
                                            }

                                            // PopToRootPage();
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
                                                TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                                                string unixTime = span.TotalSeconds.ToString("N0");
                                                unixTime = unixTime.Replace(",", "");
                                                _estimateZakatAttachment.Erfdt = "" + "/Date(" + unixTime + ")/";// need to
                                                                                                                 //_estimateZakatAttachment.Erfdt = "/Date(" + unixTime + ")/";// need to
                                                SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment.Add(_estimateZakatAttachment);
                                                ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment);


                                                //IsValueChanged();// 1584987294.32348//1584987210.06955
                                                // ZakatReturnAttachmentsList.Add(_estimateZakatAttachment);
                                            }
                                            else
                                            {
                                                MainThread.BeginInvokeOnMainThread(async () =>
                                                {
                                                    AttachmentName = string.Empty;
                                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                                    //   await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                                    IsLoading = false;
                                                });
                                            }

                                        }
                                        else
                                        {
                                            MainThread.BeginInvokeOnMainThread(async () =>
                                            {
                                                AttachmentName = string.Empty;
                                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFilesizemustbelessthan5MB));

                                                //  await _dialogService.ShowMessage(AppResources.ZZFilesizemustbelessthan5MB, AppResources.Alerts);
                                                IsLoading = false;
                                            });
                                        }
                                    }
                                    else
                                    {
                                        MainThread.BeginInvokeOnMainThread(async () =>
                                        {
                                            AttachmentName = string.Empty;
                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                            //   await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                            IsLoading = false;
                                        });
                                    }
                                }
                                else
                                {
                                    MainThread.BeginInvokeOnMainThread(async () =>
                                    {
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));

                                        //await _dialogService.ShowMessage(AppResources.ZZFileWithTheSameNameAlreadyExists, AppResources.Alerts);
                                        IsLoading = false;
                                        AttachmentName = string.Empty;
                                    });
                                }
                            }
                            else
                            {
                                AttachmentName = string.Empty;
                                IsLoading = false;
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                    // await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                });


                            }

                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                AttachmentName = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));

                                //  await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                                IsLoading = false;
                            });
                        }
                    }
                    catch (InternetException ex)
                    {
                        IsLoading = false;
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        });
                    }
                });
                IsLoading = false;
            }
            catch (Exception)
            {
            }

        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

        private bool IsFileAlreadyAttached(string FileName)
        {
            bool isFileAlreadyAttached = false;
            if (ZakatReturnAttachmentsList != null && ZakatReturnAttachmentsList.Count > 0)
            {
                for (int i = 0; i < ZakatReturnAttachmentsList.Count; i++)
                {
                    if (ZakatReturnAttachmentsList[i].Filename.Equals(FileName))
                        isFileAlreadyAttached = true;
                    else
                        isFileAlreadyAttached = false;
                    if (isFileAlreadyAttached)
                        break;
                }
            }
            return isFileAlreadyAttached;
        }

        public async Task DeleteSelectedAttachment(string filename, string dougUD)
        {
            IsLoading = true;
            try
            {
                string res = await WebServiceManager.GAZTDeleteEstimatedZAKATRAttachment(filename, dougUD);
                await PopToRootPage();
                if (res.Equals("X") && ZakatReturnAttachmentsList.Count > 0)
                {
                    for (int i = 0; i < ZakatReturnAttachmentsList.Count; i++)
                    {
                        if (ZakatReturnAttachmentsList[i].Doguid.Equals(dougUD))
                        {
                            SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment.RemoveAt(i);
                            ZakatReturnAttachmentsList.RemoveAt(i);
                        }
                    }
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            try
            {
                string res = await WebServiceManager.GAZTDeleteEstimatedZAKATRAttachment(filename, dougUD);
                await PopToRootPage();
                if (res.Equals("X") && ZakatReturnAttachmentsList.Count > 0)
                {
                    for (int i = 0; i < ZakatReturnAttachmentsList.Count; i++)
                    {
                        if (ZakatReturnAttachmentsList[i].Doguid.Equals(dougUD))
                        {
                            SalesDetailList[SelectedSalesTypeIndex].estimateZakatAttachment.RemoveAt(i);
                            ZakatReturnAttachmentsList.RemoveAt(i);
                        }
                    }
                }
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            IsLoading = false;
        }


        public async Task ClearAllAttachment(string filename, string dougUD)
        {
            IsLoading = true;

            try
            {
                string res = await WebServiceManager.GAZTDeleteEstimatedZAKATRAttachment(filename, dougUD);
                await PopToRootPage();

            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            IsLoading = false;
        }


        private ObservableCollection<ZakatAttachment> CloneAttachmmentListInLocalList(ObservableCollection<EstimateZakatAttachment> estimateZakatAttachment)
        {
            ObservableCollection<ZakatAttachment> _estimateZakatAttachment = new ObservableCollection<ZakatAttachment>();
            foreach (EstimateZakatAttachment obj in estimateZakatAttachment)
            {
                ZakatAttachment _zakatAttachment = new ZakatAttachment();
                try
                {
                    //  public Metadata3 __metadata { get; set; }
                    _zakatAttachment.RetGuid = obj.RetGuid;
                    _zakatAttachment.Seqno = obj.Seqno;
                    _zakatAttachment.Dotyp = obj.Dotyp;
                    _zakatAttachment.Doguid = obj.Doguid;
                    _zakatAttachment.AttBy = obj.AttBy;
                    _zakatAttachment.Filename = obj.Filename;
                    _zakatAttachment.FileExtn = obj.Filename.Split('.')[1];
                    _zakatAttachment.FileImage = UtilityManager.GetFileImage(_zakatAttachment.FileExtn);

                    _zakatAttachment.Mimetype = obj.Mimetype;
                    _zakatAttachment.ByPusr = obj.ByPusr;
                    _zakatAttachment.Erfdt = obj.Erfdt;
                    _zakatAttachment.DataVersion = obj.DataVersion;

                    _zakatAttachment.DocUrl = obj.DocUrl;
                    _zakatAttachment.OutletRef = obj.OutletRef;
                    string unixDate = GetUnixDate(_zakatAttachment.Erfdt);
                    double unixTime = Convert.ToDouble(unixDate);
                    DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
                    DateTime dt = new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
                    TimeZone localZone = TimeZone.CurrentTimeZone;
                    string standardName = localZone.DaylightName;
                    _zakatAttachment.UploadededDateToShow = DateTime.Now.ToLocalTime().ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘UTC’ ‘zzz’");
                    string uploadedDate = _zakatAttachment.UploadededDateToShow;
                    uploadedDate = uploadedDate.Replace("’", "");
                    uploadedDate = uploadedDate.Replace("‘", "");
                    uploadedDate = uploadedDate.Replace("UTC", "GMT");
                    _zakatAttachment.UploadededDateToShow = uploadedDate;
                    _estimateZakatAttachment.Add(_zakatAttachment);
                }
                catch (Exception)
                {
                }
            }
            return _estimateZakatAttachment;
        }

        private string GetUnixDate(string _erfdt)
        {
            int startIndex = 6;
            int lengthOfCharacter = _erfdt.Length - 8;
            string unixDateTime = _erfdt.Substring(startIndex, lengthOfCharacter);
            return unixDateTime;
        }
        // Creating a list object to contain the Sales Type object
        public void GetSalesTypeObjectList()
        {
            List<SalesDetails> salesDetailList = new List<SalesDetails>();
            SalesDetails toatalVATSales = new SalesDetails();
            SalesDetails averageNumberOfLabour = new SalesDetails();
            SalesDetails importValue = new SalesDetails();
            SalesDetails importFromPointOfSales = new SalesDetails();
            SalesDetails contactFromETIMADSystem = new SalesDetails();
            SalesDetails exportValue = new SalesDetails();
            SalesDetails purchaseValue = new SalesDetails();
            SalesDetails capitalAmount = new SalesDetails();
            SalesDetails realEstate = new SalesDetails();

            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment1 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment2 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment3 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment4 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment5 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment6 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment7 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment8 = new ObservableCollection<EstimateZakatAttachment>();
            ObservableCollection<EstimateZakatAttachment> _estimateZakatAttachment9 = new ObservableCollection<EstimateZakatAttachment>();
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment1;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment2;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment3;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment4;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment5;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment6;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment7;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment8;
            toatalVATSales.estimateZakatAttachment = _estimateZakatAttachment9;


            salesDetailList.Add(toatalVATSales);
            salesDetailList.Add(averageNumberOfLabour);
            salesDetailList.Add(importValue);
            salesDetailList.Add(importFromPointOfSales);
            salesDetailList.Add(contactFromETIMADSystem);
            salesDetailList.Add(exportValue);
            salesDetailList.Add(purchaseValue);
            salesDetailList.Add(capitalAmount);
            salesDetailList.Add(realEstate);
            SalesDetailList = salesDetailList;

        }

        private int GetSelectedSalesTypeIndex()
        {
            if (ZAKATReturnDetailsView.salesType.Equals("TotalVATSales"))
            {
                return 0;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("AverageNumberLabour"))
            {
                return 1;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("ImportValue"))
            {
                return 2;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("ImportFromPointOfSales"))
            {
                return 3;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("ContactFromETIMADSystem"))
            {
                return 4;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("ExportValue"))
            {
                return 5;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("PurchaseValue"))
            {
                return 6;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("CapitalAmount"))
            {
                return 7;
            }
            else if (ZAKATReturnDetailsView.salesType.Equals("RealEstateValue"))
            {
                return 8;
            }
            else
            {
                return 0;
            }
        }

        public void ClearData()
        {
            ObjectionReason = string.Empty;
            if (ZakatReturnAttachmentsList != null && ZakatReturnAttachmentsList.Count > 0)
            {
                ZakatReturnAttachmentsList.Clear();
            }

        }

        #endregion

    }
}
