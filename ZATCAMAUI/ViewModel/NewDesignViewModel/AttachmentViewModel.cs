using System;
using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class AttachmentViewModel : BaseViewModel
    {
        private ObservableCollection<Attachment> _attachmentsListViewData;
        public ObservableCollection<Attachment> AttachmentsListViewData
        {
            get { return _attachmentsListViewData; }
            set
            {
                _attachmentsListViewData = value;
                OnPropertyChanged(nameof(AttachmentsListViewData));
            }
        }

        public AttachmentViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService,dialogService)
        {

        }
    }
}

