using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.CustomServices.BalaghModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class ReportFinancialViolationViewModel : CommonViewModel
    {

        ObservableCollection<Balagh> balaghTypes { get; set; }

        public ObservableCollection<Balagh> BalaghTypes
        {
            get { return balaghTypes; }

            set
            {

                balaghTypes = value;
                RaisePropertyChanged();
            }
        }

        Balagh selectedBalaghType { get; set; } = null;

        public Balagh SelectedBalaghType
        {
            get { return selectedBalaghType; }

            set
            {

                selectedBalaghType = value;
                RaisePropertyChanged();
            }
        }

        string name { get; set; } = null;

        public string Name
        {
            get { return name; }

            set
            {

                name = value;
                RaisePropertyChanged();
            }
        }


        string phone { get; set; } = null;

        public string Phone
        {
            get { return phone; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.StartsWith("0") && value.Length == 10)
                    {
                        PhoneHasError = false;
                    }
                    else
                    {
                        PhoneHasError = true;
                    }
                }
                else { PhoneHasError = false; }
                phone = value;
                RaisePropertyChanged();
            }
        }

        bool mailHasError { get; set; }

        public bool MailHasError
        {
            get { return mailHasError; }

            set
            {

                mailHasError = value;
                RaisePropertyChanged();
            }
        }

        bool phoneHasError { get; set; }

        public bool PhoneHasError
        {
            get { return phoneHasError; }

            set
            {

                phoneHasError = value;
                RaisePropertyChanged();
            }
        }


        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        string mail { get; set; } = null;

        public string Mail
        {
            get { return mail; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    IsValidMail = Regex.IsMatch(value, emailRegex, RegexOptions.IgnoreCase);
                    MailHasError = !IsValidMail;
                }
                else
                {
                    MailHasError = false;
                }
                mail = value;
                RaisePropertyChanged();
            }
        }

        bool balaghTxtHasError { get; set; } = true;

        public bool BalaghTxtHasError
        {
            get { return balaghTxtHasError; }

            set
            {

                balaghTxtHasError = value;
                RaisePropertyChanged();
            }
        }

        bool balaghOtherTypeTxtHasError { get; set; }

        public bool BalaghOtherTypeTxtHasError
        {
            get { return balaghOtherTypeTxtHasError; }

            set
            {

                balaghOtherTypeTxtHasError = value;
                RaisePropertyChanged();
            }
        }



        string balaghTxt { get; set; }

        public string Balaghtx
        {
            get { return balaghTxt; }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    BalaghTxtHasError = false;
                }
                else
                {
                    BalaghTxtHasError = true;

                }
                balaghTxt = value;
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


        bool isShowSuccessView { get; set; }

        public bool IsShowSuccessView
        {
            get { return isShowSuccessView; }

            set
            {
                isShowSuccessView = value;
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

        string balaghTypeOtherTxt { get; set; }

        public string BalaghTypeOtherTxt
        {
            get { return balaghTypeOtherTxt; }

            set
            {
                if (IsBalaghTypeOOther)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        BalaghOtherTypeTxtHasError = true;
                    }
                    else
                    {
                        BalaghOtherTypeTxtHasError = false
                            ;
                    }
                }
                else
                {
                    BalaghOtherTypeTxtHasError = false;
                }
                balaghTypeOtherTxt = value;
                RaisePropertyChanged();
            }
        }

        string portOtherTxt { get; set; }

        public string PortOtherTxt
        {
            get { return portOtherTxt; }

            set
            {
                portOtherTxt = value;
                RaisePropertyChanged();
            }
        }

        bool isBalaghTypeOOther { get; set; }

        public bool IsBalaghTypeOOther
        {
            get { return isBalaghTypeOOther; }

            set
            {
                isBalaghTypeOOther = value;
                RaisePropertyChanged();
            }
        }

        bool isPortOther { get; set; }

        public bool IsPortOther
        {
            get { return isPortOther; }

            set
            {
                isPortOther = value;
                RaisePropertyChanged();
            }
        }


        string balaghNoTxt { get; set; }

        public string BalaghNoTxt
        {
            get { return balaghNoTxt; }

            set
            {
                balaghNoTxt = value;
                RaisePropertyChanged();
            }
        }

        DateTime balaghDate { get; set; }

        public DateTime DateTime
        {
            get { return DateTime.Now; }

            set
            {
                balaghDate = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<Ticketfile> ticketFiles;

        public ObservableCollection<Ticketfile> TicketFiles
        {
            get { return ticketFiles; }

            set
            {

                ticketFiles = value;
                RaisePropertyChanged();
            }
        }


        IBalaghServices _balaghServices;
        ICustomInquiryService _inquiryService;

        public ReportFinancialViolationViewModel(INavigationService navigationServices, IDialogService dialogService, IBalaghServices balaghServices, ICustomInquiryService inquiryService, ICommonServices commonService) : base(navigationServices, dialogService, commonService)
        {
            _inquiryService = inquiryService;
            _balaghServices = balaghServices;
            TicketFiles = new ObservableCollection<Ticketfile>();
        }

        public ICommand LoadIntialLookupsCommand
        {
            get
            {
                return new Command(() =>
                {
                    /*Task.WhenAny
                    (GetPorts(),GetBalaghTypes());*/
                    GetBalaghTypes();
                    GetPorts();


                });
            }
        }

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

        public ICommand SendTicketCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SendBalaghTicket();


                });
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return new Command(() =>
                {
                    ClearData();

                });
            }
        }

        private void ClearData()
        {
            Name = Phone = Mail = Balaghtx = null;
            SelectedBalaghType = null;
            SelectedPort = null;
            IsPolicyChecked = false;
            IsBalaghTypeOOther = false; BalaghTypeOtherTxt = "";
            TicketFiles = new ObservableCollection<Ticketfile>();
        }

        public string ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }
        async Task PickAndShow(PickOptions options)
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
                        //var filesiz = new FileInfo(Text).Length;
                        var lenght = new FileInfo(result.FullPath).Length;
                        double LenghtInKb = lenght / 1024;
                        double LenInMb = LenghtInKb / 1024;
                        double size = LenInMb;


                        double filesize = size;

                        if (TicketFiles != null && TicketFiles.Count > 0)
                        {
                            if (TicketFiles.Count < 5)
                            {
                                filesize += TicketFiles.Select(c => c.FileSize).Sum();
                            }
                            else
                            {
                                MessageTxt = AppResources.TINDeregAttachmentsTitleOne;
                                isShowMsgView = true;

                                return;
                            }

                        }
                        if (filesize > 2)
                        {
                            MessageTxt = AppResources.MaximumFileSizeMsg;
                            isShowMsgView = true;
                            return;
                        }
                        else
                        {
                            var stream = await result.OpenReadAsync();
                            string content = ConvertToBase64(stream);
                            Ticketfile ticketfile = new Ticketfile();
                            ticketfile.filecontent = content;
                            ticketfile.filename = result.FileName;
                            ticketfile.FileSize = Math.Round(size, 2);
                            ticketfile.Id = result.FileName + DateTime.Now.Ticks;
                            TicketFiles.Add(ticketfile);
                        }
                    }
                    else
                    {
                        MessageTxt = AppResources.BalaghSupportedFileMsg;
                        IsShowMsgView = true;
                    }

                }

                //return result;
            }
            catch (Exception)
            {
                // The user canceled or something went wrong
            }


        }

        public async Task GetBalaghTypes()
        {
            try
            {
                IsLoading = true;
                var data = await _balaghServices.GetBalaghTypes();
                if (data.Item2)
                {
                    BalaghTypes = data.Item1.data;
                }
            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        bool isMainView = true;
        public bool IsMainView { get { return isMainView; } set { isMainView = value; RaisePropertyChanged(); } }

        bool isValidMail;
        public bool IsValidMail { get { return isValidMail; } set { isValidMail = value; RaisePropertyChanged(); } }

        bool isPolicyChecked = false;
        public bool IsPolicyChecked { get { return isPolicyChecked; } set { isPolicyChecked = value; RaisePropertyChanged(); } }

        public async Task SendBalaghTicket()
        {
            try
            {
                IsLoading = true;
                if (IsPolicyChecked)
                {


                    if (!MailHasError && SelectedPort != null && (SelectedBalaghType != null && IsBalaghTypeOOther == false || IsBalaghTypeOOther && !string.IsNullOrEmpty(BalaghTypeOtherTxt)) && !string.IsNullOrEmpty(Balaghtx))
                    {


                        BalaghTicket balaghTicket = new BalaghTicket()
                        {
                            balaghplaceid = SelectedPort.port_cd,
                            balaghtypeid = SelectedBalaghType.id,
                            email = Mail,
                            mobile = Phone,
                            name = Name,
                            ticketfiles = TicketFiles,
                            ticketcontent = Balaghtx,
                            otherbalaghtype = BalaghTypeOtherTxt
                        };

                        var data = await _balaghServices.AddNewBalaghTicket(balaghTicket);
                        if (data.IsSuccessStatusCode)
                        {
                            var contet = await data.Content.ReadAsStringAsync();
                            var res = JsonConvert.DeserializeObject<BalaghTicketResponse>(contet);
                            if (res.issuccess)
                            {
                                IsMainView = false;
                                IsShowSuccessView = true;
                                BalaghNoTxt = res.data;
                            }
                        }
                    }
                    else
                    {
                        if (IsBalaghTypeOOther && string.IsNullOrEmpty(BalaghTypeOtherTxt))
                        {
                            BalaghOtherTypeTxtHasError = true;
                        }
                        MessageTxt = AppResources.InquiryDataRequiredAttentionMsg;
                        IsShowMsgView = true;
                    }
                }
                else
                {
                    MessageTxt = AppResources.PolicyValidationMsg;
                    IsShowMsgView = true;
                }

            }
            catch (Exception)
            {

            }
            finally { IsLoading = false; }
        }

        public ICommand SelectPortCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (Ports != null && Ports.Count > 0)
                    {
                        var res = await ActionSheet.ShowActionSheet("", AppResources.CancelText, null, Ports.Select(c => c.Name).ToArray());
                        if (!string.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            /* if (res == "اخرى")
                             {
                                 //IsBalaghTypeOOther = true;
                                 return;
                             }*/
                            SelectedPort = Ports.First(c => c.Name == res);
                        }
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                });
            }
        }

        public ICommand SelectBalaghTypeCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (BalaghTypes != null && BalaghTypes.Count > 0)
                    {
                        var res = await ActionSheet.ShowActionSheet(null, AppResources.CancelText, null, BalaghTypes.Select(c => c.Name).ToArray());
                        if (!string.IsNullOrEmpty(res) && res != AppResources.CancelText)
                        {
                            if (res == "اخرى" || res == "Others")
                            {
                                IsBalaghTypeOOther = true;
                                //return;
                            }
                            else
                            {
                                IsBalaghTypeOOther = false;

                            }
                            SelectedBalaghType = BalaghTypes.First(c => c.Name == res);
                        }
                    }
                    else
                    {
                        MessageTxt = AppResources.NoDataFound;
                        IsShowMsgView = true;
                    }
                });
            }
        }

        public ICommand PolicyCheckedCommand
        {
            get
            {
                return new Command(() =>
                {

                    IsPolicyChecked = !IsPolicyChecked;
                });
            }
        }

        public ICommand DeleteAttatchementCommand
        {
            get
            {
                return new Command<Ticketfile>((e) =>
                {

                    if (e != null && TicketFiles != null && TicketFiles.Count > 0)
                    {
                        TicketFiles.Remove(e);
                    }
                });
            }
        }
        public ICommand ViewPrivacyandPolicyCommand
        {
            get
            {
                return new Command(() =>
                {


                    var pdfLink = "http://172.50.15.39:8080/files/PrivaceAndPolicyEn.pdf";
                    if (App.IsArabic)
                    {

                        pdfLink = "http://172.50.15.39:8080/files/PrivaceAndPolicyAr.pdf";
                        //pdfLink = "https://eservices.zatca.gov.sa/sites/sc/ar/reportsandcomplaints/Documents/%D8%B3%D9%8A%D8%A7%D8%B3%D8%A9%20%D8%A8%D9%84%D8%A7%D8%BA%2010-12-2018.pdf";
                    }
                    _navigationService.NavigateTo(App.PdfView, pdfLink);

                    //  Xamarin.Essentials.Launcher.OpenAsync(pdfLink);

                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowSuccessView = false;
                    IsMainView = true;
                    ClearData();
                    _navigationService.GoBack();

                });
            }
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

    }
}
