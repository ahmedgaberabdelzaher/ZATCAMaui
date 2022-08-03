using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.CustomServices.BalaghModels;
using EGAZT.Models.SubmitReportModel;
using GalaSoft.MvvmLight.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.SubmitReport
{
    public class SubmitReportViewModel : BaseViewModel
    {
        SubmitReportModel submitReport;
        public SubmitReportModel SubmitReport { get { return submitReport; } set { submitReport = value; RaisePropertyChanged(); } }

        bool isTherePDFUploaded;
        public bool IsTherePDFUploaded { get { return isTherePDFUploaded; } set { isTherePDFUploaded = value; RaisePropertyChanged(); } }

        bool isShowMapView;
        public bool IsShowMapView { get { return isShowMapView; } set { isShowMapView = value; RaisePropertyChanged(); } }

        bool isRewardChecked;
        public bool IsRewardChecked { get { return isRewardChecked; } set { isRewardChecked = value; RaisePropertyChanged(); } }

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }


        ObservableCollection<Ticketfile> reportUloadedFiles = new ObservableCollection<Ticketfile>();
        public ObservableCollection<Ticketfile> ReportUloadedFiles { get { return reportUloadedFiles; } set { reportUloadedFiles = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>()
        {
            new BottomSheetModel {Id= null, Name = AppResources.VATCertificates},
            new BottomSheetModel {Id= "V2", Name =AppResources.ExciseCertificates},
            new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
            new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
            new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
            new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
            new BottomSheetModel {Id= "V8", Name = AppResources.Einvoice},
        };
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }


        public ICommand SendReportCommand { get; set; }

        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" });


                });
            }
        }

        public ICommand CloseMapCommand
        {
            get
            {
                return new Command(() => { IsShowMapView = false; });

            }
        }
        public ICommand ClosBottomSheetCommand
        {
            get
            {
                return new Command(() => { IsShowBottomSheet = false; });

            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new Command(() => { IsShowMapView = true; });

            }
        }

        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<Ticketfile>((e) =>
                {

                    if (e != null && ReportUloadedFiles != null && ReportUloadedFiles.Count > 0)
                    {
                        ReportUloadedFiles.Remove(e);

                        if (ReportUloadedFiles.Count == 0) IsTherePDFUploaded = false;
                    }
                });
            }
        }
        public ICommand SelectedItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    SubmitReport.ReportType = e.Name;
                    SubmitReport.ReportCategory = e.Name;
                    IsShowBottomSheet = false;

                });
            }
        }
        public ICommand OpenReportTypeCommand
        {
            get
            {
                return new Command(() =>
                {
                    BottomSheetList = new ObservableCollection<BottomSheetModel>();
                    IsShowBottomSheet = true;
                });

            }
        }
        public ICommand OpenReportCategoryCommand
        {
            get
            {
                return new Command(() => { IsShowBottomSheet = true; });

            }
        }

        public SubmitReportViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        private async Task PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                    {

                        var lenght = new FileInfo(result.FullPath).Length;
                        double LenghtInKb = lenght / 1024;
                        double LenInMb = LenghtInKb / 1024;
                        double size = LenInMb;
                        double filesize = size;

                        if (filesize > 5)
                        {
                            MessageTxt = AppResources.MaximumFileSizeMsg;
                            IsShowMsgView = true;
                        }
                        else if (ReportUloadedFiles != null && ReportUloadedFiles.Count >= 0 && ReportUloadedFiles.Count <= 5)
                        {
                            var stream = await result.OpenReadAsync();
                            //var file = new File("profilePicture", "img.png", new StreamContent(mediaFile.GetStream()));
                            string content = ConvertToBase64(stream);
                            Ticketfile ticketfile = new Ticketfile();
                            ticketfile.filecontent = content;
                            ticketfile.filename = result.FileName;
                            ticketfile.FileSize = Math.Round(size, 2);
                            ticketfile.Id = result.FileName + System.DateTime.Now.Ticks;
                            ReportUloadedFiles.Add(ticketfile);
                            IsTherePDFUploaded = true;

                        }
                        else
                        {
                            MessageTxt = AppResources.TINDeregAttachmentsTitleOne;
                            IsShowMsgView = true;
                        }
                    }
                    else
                    {
                        MessageTxt = AppResources.BalaghSupportedFileMsg;
                        IsShowMsgView = true;
                    }

                }
            }
            catch (Exception ex)
            {
            }


        }

        private string ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }

    }
}

