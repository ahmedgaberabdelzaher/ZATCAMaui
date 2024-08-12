using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.TPProfile;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using static ZATCAMAUI.Models.ErrorMessage;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class UpdateManagerViewModel : BaseViewModel
    {

        #region Properties

        private bool showListview = false;
        public bool ShowListview
        {
            get
            {
                return showListview;
            }
            set
            {
                if (showListview == value) return;
                showListview = value;
                OnPropertyChanged(nameof(ShowListview));
            }
        }

        private bool showNoData = false;
        public bool ShowNoData
        {
            get
            {
                return showNoData;
            }
            set
            {
                if (showNoData == value) return;
                showNoData = value;
                OnPropertyChanged(nameof(ShowNoData));
            }
        }


        private UpdateManagerModel _changeManagerModel;
        public UpdateManagerModel ChangeManagerModel
        {
            get
            {
                return _changeManagerModel;
            }
            set
            {
                if (_changeManagerModel == value) return;
                _changeManagerModel = value;
                OnPropertyChanged(nameof( ChangeManagerModel));
            }
        }

        public ObservableCollection<ManagerList> _mgrList = new ObservableCollection<ManagerList>();
        public ObservableCollection<ManagerList> MgrList
        {
            get { return _mgrList; }

            set
            {
                if (_mgrList == value)
                {
                    return;
                }

                _mgrList = value;
                OnPropertyChanged("MgrList");
            }
        }
        #endregion

        public UpdateManagerViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService,dialogService)
        {
        }

        public async Task LoadManagerDetails()
        {
            IsLoading = true;
            var data = await WebServiceManager.GetTPManagerDetails();
            IsLoading = false;
            MgrList.Clear();
            if (data.Item1 != null)
            {
                if (data.Item1.IsSuccessStatusCode)
                {
                    if (data.Item2 != null)
                    {
                        ChangeManagerModel = JsonConvert.DeserializeObject<UpdateManagerModel>(data.Item2);
                        if(ChangeManagerModel.D != null)
                        {

                            foreach(ManagerList item in ChangeManagerModel.D.Results)
                            {
                                ManagerList newItem = new ManagerList();
                                item.Gpart = App.TP.TIN;
                                newItem = item;
                                if (item.BirthDt != null)
                                {
                                    newItem.BirthDt = Convert.ToDateTime(item.BirthDt.ToString()).ToShortDateString();
                                }
                                if (item.Editfg.Equals("Y"))
                                {
                                    newItem.EnableIDNumber = item.Mgrid.Length > 0 ? false : true;
                                    newItem.EnableManagerName = item.Mgrnm.Length > 0 ? false : true;
                                    newItem.EnableBirthdate = item.BirthDt.ToString().Length > 0 ? false : true;
                                    newItem.EnableMobNumber = item.MobNo.Length > 0 ? false : true;
                                    newItem.EnableEmail = item.Email.Length > 0 ? false : true;
                                }
                                else
                                {
                                    newItem.EnableIDNumber = false;
                                    newItem.EnableManagerName = false;
                                    newItem.EnableBirthdate = false;
                                    newItem.EnableMobNumber = false;
                                    newItem.EnableEmail = false;
                                }
                                MgrList.Add(newItem);
                            }
                        }
                        if(MgrList.Count > 0)
                        {
                            ShowListview = true;
                            ShowNoData = false;
                        }
                        else
                        {
                            ShowListview = false;
                            ShowNoData = true;
                        }
                    }
                }
                else
                {
                    await ShowErrorWithQuitAsync(data.Item2);
                }
            }
        }
        private async Task ShowErrorWithQuitAsync(string item2)
        {
            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(item2);
            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
            {
                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WithReplacedString));
            }
        }
        private async Task PleaseFillAllMandatory()
        {
            await _dialogService.ShowMessage(AppResources.ZZPleasefillallthemandatoryfields, AppResources.ZError);
            return;
        }

        internal async 
        Task
PrepareDataForSubmit()
        {
            try
            {
                if (MgrList.Count > 0)
                {
                    foreach (var item in MgrList)
                    {
                        if (item.Editfg.Equals("Y"))
                        {
                            if (item.EnableIDNumber)
                            {
                                if (item.Mgrid.Length == 0)
                                {
                                    await PleaseFillAllMandatory();
                                    return;
                                }
                            }

                            if (item.EnableManagerName)
                            {
                                if (item.Mgrnm.Length == 0)
                                {
                                    await PleaseFillAllMandatory();
                                    return;
                                }
                            }

                            if (item.EnableBirthdate)
                            {
                                if (item.BirthDt == null)
                                {
                                    await PleaseFillAllMandatory();
                                    return;
                                }
                            }
                            if (item.EnableMobNumber)
                            {
                                if (item.MobNo == null)
                                {
                                    await PleaseFillAllMandatory();
                                    return;
                                }
                            }
                            if (item.EnableEmail)
                            {
                                if (item.Email == null)
                                {
                                    await PleaseFillAllMandatory();
                                    return;
                                }
                            }
                        }
                    }

                    ManagerDetailsPayload obj = new ManagerDetailsPayload();
                    obj.Operation = "SM";
                    obj.Taxpayer = App.TP.Tin;
                    obj.ManagerDetailsSet = new System.Collections.Generic.List<ManagerDetailsSet>();
                    foreach (var item in MgrList)
                    {
                        ManagerDetailsSet newItem = new ManagerDetailsSet();
                        newItem.BirthDt = UtilityManager.ConvertDateFormat(item.BirthDt);
                        newItem.Editfg = item.Editfg;
                        newItem.Email = item.Email;
                        newItem.Gpart = item.Gpart;
                        newItem.Mgrid = item.Mgrid;
                        newItem.Mgrnm = item.Mgrnm;
                        newItem.MobNo = item.MobNo;
                        obj.ManagerDetailsSet.Add(newItem);
                    }
                    IsLoading = true;
                    var data = await WebServiceManager.SaveManagersList(obj);
                    IsLoading = false;
                    if (data.Item1 != null)
                    {
                        if (data.Item1.IsSuccessStatusCode)
                        {
                            if (data.Item2 != null)
                            {
                                var response = JsonConvert.DeserializeObject<UpdateManagerModel>(data.Item2);
                                await _dialogService.ShowMessage(AppResources.DataSaved, AppResources.Information);
                                _navigationService.GoBack();
                            }
                        }
                        else
                        {
                            await ShowErrorWithQuitAsync(data.Item2);
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {
                IsLoading = false;
            }
        }
    }
}


