using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.CustomServices.BalaghModels;
using GalaSoft.MvvmLight.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.SubmitReport
{
    public class SubmitReportViewModel : BaseViewModel
    {
        string fullName;
        public string FullName { get { return fullName; } set { fullName = value; RaisePropertyChanged(); } }

        string email;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }

        string phone;
        public string PhoneNumber { get { return phone; } set { phone = value; RaisePropertyChanged(); } }

        string location;
        public string Location { get { return location; } set { location = value; RaisePropertyChanged(); } }

        string region;
        public string Region { get { return region; } set { region = value; RaisePropertyChanged(); } }

        string city;
        public string City { get { return city; } set { city = value; RaisePropertyChanged(); } }

        string district;
        public string District { get { return district; } set { district = value; RaisePropertyChanged(); } }

        string street;
        public string Street { get { return street; } set { street = value; RaisePropertyChanged(); } }

        string merchant;
        public string Merchant { get { return merchant; } set { merchant = value; RaisePropertyChanged(); } }

        string editor;
        public string Editor { get { return editor; } set { editor = value; RaisePropertyChanged(); } }

        bool isTherePDFUploaded;
        public bool IsTherePDFUploaded { get { return isTherePDFUploaded; } set { isTherePDFUploaded = value; RaisePropertyChanged(); } }

        bool isShowMapView;
        public bool IsShowMapView { get { return isShowMapView; } set { isShowMapView = value; RaisePropertyChanged(); } }

        bool isRewardChecked;
        public bool IsRewardChecked { get { return isRewardChecked; } set { isRewardChecked = value; RaisePropertyChanged(); } }

        ObservableCollection<Ticketfile> reportUloadedFiles = new ObservableCollection<Ticketfile>();
        public ObservableCollection<Ticketfile> ReportUloadedFiles { get { return reportUloadedFiles; } set { reportUloadedFiles = value; RaisePropertyChanged(); } }

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

        public ICommand ShowMapCommand
        {
            get
            {
                return new Command(() => { IsShowMapView = true;  });
                
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

                        if(ReportUloadedFiles.Count == 0) IsTherePDFUploaded = false;
                    }
                });
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

