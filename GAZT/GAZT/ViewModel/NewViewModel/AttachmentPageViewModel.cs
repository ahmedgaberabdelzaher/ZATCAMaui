using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class AttachmentPageViewModel : ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAttachmentClick { get; set; }
        byte[] attachment;

        #endregion

        #region Property

        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }

        private string _attachmentName = "Attachments";
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

                RaisePropertyChanged("AttachmentName");

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
        private ObservableCollection<Attachment> _vatAttachmentsList;
        public ObservableCollection<Attachment> VatAttachmentsList
        {
            get
            {
                return _vatAttachmentsList;
            }
            set
            {
                _vatAttachmentsList = value;
                RaisePropertyChanged("VatAttachmentsList");
            }
        }

        #endregion

        #region Constructor
        public AttachmentPageViewModel(INavigationService navigationService, IDialogService dialogService)
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



            OnAttachmentClick = new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    if (AttachmentCount <= 40)
                    {
                        var fileData = await CrossFilePicker.Current.PickFile();
                        attachment = fileData.DataArray;
                        AttachmentName = fileData.FileName;

                        AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveVATDeclarationAttachment(attachment, AttachmentName, VATDeclarationData.d.ReturnIdz, "VTA0");
                        if (_attachment != null && _attachment.d != null)
                        {
                            AttachmentSize =Math.Round((Convert.ToDecimal(attachment.Length) / 1024),2).ToString();
                            if (Convert.ToInt32(AttachmentSize) <= 20)
                            {
                                VATDeclarationData.d.ATTACHSet.results.Add(_attachment.d);
                                ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(VATDeclarationData.d.ATTACHSet.results as List<Attachment>);
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    VatAttachmentsList = myCollection;
                                });
                                VatAttachmentsList = myCollection;
                                AttachmentCount++;
                            }
                            else
                            {
                                _dialogService.ShowMessage(AppResources.ZFilesizeshouldnotbemorethan20MB, AppResources.Information);

                            }
                        }
                       
                    }
                    else
                    {
                        _dialogService.ShowMessage(AppResources.ZMaximumnoofallowedattachmentsare40, AppResources.Information);
                    }
                }
                catch (Exception ex)
                {


                }
            

            });

        }
        #endregion

        #region Method
        #endregion
    }
}
