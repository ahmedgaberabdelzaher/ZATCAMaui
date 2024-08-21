using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM
{

    public class RelationShipManagerInfoPageViewModel : BaseViewModel
    {

        RMContactDetailsBaseModel RmContactsdetailsBaseModel;
        public RelationShipManagerInfoPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            ShareOpenionClicked = new Command(async () =>
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.GOTOPORTALFORSURVEY));
            });
        }

       
        public ICommand ShareOpenionClicked { get; set; }
       
       /* RM Details*/
        private string _RMName = string.Empty;
        public string getRMName
        {
            get
            {
                return _RMName;
            }
            set
            {
                if (_RMName == value) return;

                _RMName = value;
                OnPropertyChanged("getRMName");
            }
        }

        private string _RMEmail = string.Empty;
        public string getRMEmail
        {
            get
            {
                return _RMEmail;
            }
            set
            {
                if (_RMEmail == value) return;

                _RMEmail = value;
                OnPropertyChanged("getRMEmail");
            }
        }

        private string _RMMobile = string.Empty;
        public string getRMMobile
        {
            get
            {
                return _RMMobile;
            }
            set
            {
                if (_RMMobile == value) return;

                _RMMobile = value;
                OnPropertyChanged("getRMMobile");
            }
        }


        /* Sup Details*/
        private string _SupName = string.Empty;
        public string getSupName
        {
            get
            {
                return _SupName;
            }
            set
            {
                if (_SupName == value) return;

                _SupName = value;
                OnPropertyChanged("getSupName");
            }
        }

        private string _SupEmail = string.Empty;
        public string getSupEmail
        {
            get
            {
                return _SupEmail;
            }
            set
            {
                if (_SupEmail == value) return;

                _SupEmail = value;
                OnPropertyChanged("getSupEmail");
            }
        }

        private string _SupMobile = string.Empty;
        public string getSupMobile
        {
            get
            {
                return _SupMobile;
            }
            set
            {
                if (_SupMobile == value) return;

                _SupMobile = value;
                OnPropertyChanged("getSupMobile");
            }
        }

        public async Task GetRmContactDetailsOnPageLoad()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Run(async () =>
            {
                 //RmContactsdetailsBaseModel = await RMContactDetailsWebServiceManager.GetGAZTRMContactDetails(App.LoginDataRetrieved.TIN);

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (RmContactsdetailsBaseModel != null && RmContactsdetailsBaseModel.d != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.RmName))
                            getRMName = RmContactsdetailsBaseModel.d.RmName; 
                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.RmMobile))
                            getRMMobile = RmContactsdetailsBaseModel.d.RmMobile;
                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.RmEmail))
                            getRMEmail = RmContactsdetailsBaseModel.d.RmEmail;

                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.SupName))
                            getSupName = RmContactsdetailsBaseModel.d.SupName;
                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.SupMobile))
                            getSupMobile = RmContactsdetailsBaseModel.d.SupMobile;
                        if (!string.IsNullOrEmpty(RmContactsdetailsBaseModel.d.SupEmail))
                            getSupEmail = RmContactsdetailsBaseModel.d.SupEmail;

                        IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        
                        
                        IsLoading = false;
                    }
                }
                IsLoading = false;
            });
        }

        public async Task getGaztAuthorityDetails()
        {
            throw new NotImplementedException();
        }
    }
}
