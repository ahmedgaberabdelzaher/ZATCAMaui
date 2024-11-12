
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{
    public class ActivitiesPopUpViewModel : BaseViewModel
    {

        public static event EventHandler<List<NregMulSet>> DataSent;


        public ICommand OnMainGroupSelectButtonClick { get; set; }
        public ICommand OnSubGroupSelectButtonClick { get; set; }
        public ICommand OnAcitivitySelectButtonClick { get; set; }
        public ICommand OnNextButtonClick { get; set; }
        public ICommand onAddButtonClick { get; set; }
        public ICommand GoBackClick { get; set; }


        private bool isValid = true;

        public ObservableCollection<NregMulSet> _AllActivitiesList { get; set; }

        public ObservableCollection<NregMulSet> AllActivitiesList
        {
            get { return _AllActivitiesList; }

            set
            {
                if (_AllActivitiesList == value)
                {
                    return;
                }

                _AllActivitiesList = value;
                OnPropertyChanged("AllActivitiesList");
            }
        }

        public List<NregMulSet> _NregMulsetList { get; set; }

        public List<NregMulSet> NregMulsetList
        {
            get { return _NregMulsetList; }

            set
            {
                if (_NregMulsetList == value)
                {
                    return;
                }

                _NregMulsetList = value;
                OnPropertyChanged("NregMulsetList");
            }
        }
        public ActivitySetsList _ActivitySetsList { get; set; }

        public ActivitySetsList ActivitySetsList
        {
            get { return _ActivitySetsList; }

            set
            {
                if (_ActivitySetsList == value)
                {
                    return;
                }

                _ActivitySetsList = value;
                OnPropertyChanged("ActivitySetsList");
            }
        }

        private ActGroupSet _cRMainGroup = null;
        public ActGroupSet CRMainGroup
        {
            get => _cRMainGroup;
            set
            {
                if (_cRMainGroup == value) return;

                if (value != null)
                {
                    _cRMainGroup = value;
                    OnPropertyChanged(nameof(CRMainGroup));
                }
            }
        }
        private ActGroupSet _cRSubGroup = null;
        public ActGroupSet CRSubGroup
        {
            get => _cRSubGroup;
            set
            {
                if (_cRSubGroup == value) return;

                _cRSubGroup = value;
                OnPropertyChanged(nameof(CRSubGroup));
            }
        }
        private ActGroupSet _cRAcitivity;
        public ActGroupSet CRAcitivity
        {
            get => _cRAcitivity;
            set
            {
                if (_cRAcitivity == value) return;

                _cRAcitivity = value;
                OnPropertyChanged(nameof(CRAcitivity));
            }
        }

        public string _MainGroup { get; set; }

        public string MainGroup
        {
            get { return _MainGroup; }

            set
            {
                if (_MainGroup == value)
                {
                    return;
                }

                _MainGroup = value;
                OnPropertyChanged("MainGroup");
            }
        }
        public string _SubGroup { get; set; }

        public string SubGroup
        {
            get { return _SubGroup; }

            set
            {
                if (_SubGroup == value)
                {
                    return;
                }

                _SubGroup = value;
                OnPropertyChanged("SubGroup");
            }
        }

        public string _Activity { get; set; }

        public string Activity
        {
            get { return _Activity; }

            set
            {
                if (_Activity == value)
                {
                    return;
                }

                _Activity = value;
                OnPropertyChanged("Activity");
            }
        }

        public bool _isEditable { get; set; }

        public bool isEditable
        {
            get { return _isEditable; }

            set
            {
                if (_isEditable == value)
                {
                    return;
                }

                _isEditable = value;
                OnPropertyChanged("isEditable");
            }
        }
        public bool _isNoData { get; set; }

        public bool isNoData
        {
            get { return _isNoData; }

            set
            {
                if (_isNoData == value)
                {
                    return;
                }

                _isNoData = value;
                OnPropertyChanged("isNoData");
            }
        }

        public bool _isListVisible { get; set; }

        public bool isListVisible
        {
            get { return _isListVisible; }

            set
            {
                if (_isListVisible == value)
                {
                    return;
                }

                _isListVisible = value;
                OnPropertyChanged("isListVisible");
            }
        }


        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get
            {
                return _pickerModel;
            }
            set
            {
                if (_pickerModel == value) return;
                _pickerModel = value;
                try
                {
                    if (PickerModel != null && !string.IsNullOrEmpty(PickerModel.SelectedValue))
                    {

                        if (PickerModel.PickerId == "MainGroupPicker")
                        {
                        }

                        if (PickerModel.PickerId == "SubGroupPicker")
                        {
                        }
                        if (PickerModel.PickerId == "ActivityPicker")
                        {
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());

                }

                OnPropertyChanged("PickerModel");
            }
        }
        public ActivitiesPopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            AllActivitiesList = new ObservableCollection<NregMulSet>();

           
            OnNextButtonClick = new Command(() =>
            {
                ValidateActvityDetialFields();
            });


            onAddButtonClick = new Command(() => { addEmptyItem(); });

            GoBackClick = new Command(() =>
            {
                MopupService.Instance.PopAsync();

            });


            OnMainGroupSelectButtonClick = new Command((listItem) =>
            {
                if (isEditable == false)
                {
                    return;
                }
                int itemPosition = 0;
                var newItem = listItem as NregMulSet;
                if (newItem != null)
                {
                    itemPosition = AllActivitiesList.IndexOf(newItem);
                }
                var dropDownData = ActivitySetsList?.act_groupSet;
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        CRMainGroup = item as ActGroupSet;
                        CRSubGroup = null;
                        CRAcitivity = null;

                        //update list record with proper activities
                        AllActivitiesList[itemPosition].ActMgrp = CRMainGroup.Text;
                        AllActivitiesList[itemPosition].ActMgrpCode = CRMainGroup.IndSector;



                    }
                    catch (Exception )
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);

            });
            OnSubGroupSelectButtonClick = new Command((listItem) =>
            {
                if (isEditable == false)
                {
                    return;
                }
                int itemPosition = 0;
                var newItem = listItem as NregMulSet;
                if (newItem != null)
                {
                    itemPosition = AllActivitiesList.IndexOf(newItem);
                }
                var dropDownData = new List<ActGroupSet>();
                if (ActivitySetsList?.act_subgroupSet != null && CRMainGroup?.IndSector != null)
                {
                    dropDownData = ActivitySetsList?.act_subgroupSet?.Where(i => i.IndSector.StartsWith(CRMainGroup?.IndSector) == true).ToList();
                    ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);

                    poupWindow.OnItemSelect = (item) =>
                    {
                        try
                        {
                            CRSubGroup = item as ActGroupSet;
                            CRAcitivity = null;
                            AllActivitiesList[itemPosition].ActSgrp = CRSubGroup.Text;
                            AllActivitiesList[itemPosition].ActSgrpCode = CRSubGroup.IndSector;
                        }
                        catch (Exception )
                        {
                        }
                    };
                    MopupService.Instance.PushAsync(poupWindow);
                }
            });
            OnAcitivitySelectButtonClick = new Command((listItem) =>
            {
                if (isEditable == false)
                {
                    return;
                }
                int itemPosition = 0;
                var newItem = listItem as NregMulSet;
                if (newItem != null)
                {
                    itemPosition = AllActivitiesList.IndexOf(newItem);
                }

                if (ActivitySetsList?.activitySet != null && CRSubGroup?.IndSector != null)
                {
                    var dropDownData = new List<ActGroupSet>();
                    dropDownData = ActivitySetsList?.activitySet?.Where(i => i.IndSector.StartsWith(CRSubGroup?.IndSector)).ToList();

                    Tuple<ObservableCollection<NregMulSet>, bool> CheckDuplicates = null;

                    ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                    poupWindow.OnItemSelect = (item) =>
                    {
                        try
                        {
                            CRAcitivity = item as ActGroupSet;
                            CRMainGroup = ActivitySetsList.act_groupSet.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 2))).FirstOrDefault();
                            CRSubGroup = ActivitySetsList.act_subgroupSet.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 4))).FirstOrDefault();
                            AllActivitiesList[itemPosition].Activity = CRAcitivity.Text;
                            AllActivitiesList[itemPosition].ActivityCode = CRAcitivity.IndSector;

                            CheckDuplicates = RemoveDuplicates(AllActivitiesList, obj => $"{obj.Idnumber}-{obj.ActMgrp}-{obj.ActSgrp}-{obj.Activity}");
                        }
                        catch (Exception )
                        {
                        }
                    };
                    poupWindow.Closed += (sender, args) =>
                    {
                        if (CheckDuplicates != null && CheckDuplicates?.Item2 == true)
                        {
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.EstGotDuplicates));
                            //Clearing the data
                            AllActivitiesList.RemoveAt(itemPosition);
                            addEmptyItem();
                            CheckDuplicates = null;
                        }
                    };

                    MopupService.Instance.PushAsync(poupWindow);

                }
            });

        }

        public static Tuple<ObservableCollection<T>, bool> RemoveDuplicates<T>(ObservableCollection<T> collection, Func<T, string> keySelector)
        {
            var uniqueItems = new HashSet<string>();
            bool duplicatesFound = false;

            var uniqueList = new ObservableCollection<T>(collection.Where(item => uniqueItems.Add(keySelector(item)) || (duplicatesFound = true)));

            return Tuple.Create(uniqueList, duplicatesFound);
        }

        public void DeleteListItem(int itemPosition)
        {
            AllActivitiesList.RemoveAt(itemPosition);
            if (AllActivitiesList.Count == 0)
            {
                isListVisible = false;
                isNoData = true;
            }
        }

        public void addEmptyItem()
        {
            isListVisible = true;
            isNoData = false;
            AllActivitiesList.Add(new NregMulSet
            {
                Activity = "",
                ActSgrp = "",
                Idnumber = "",
                ActMgrp = "",
            });
        }

        private void ValidateActvityDetialFields()
        {
            isValid = true;
            for (int i = 0; i < AllActivitiesList.Count; i++)
            {
                if (string.IsNullOrEmpty(AllActivitiesList[i].ActMgrp))
                {
                    isValid = false;
                    break;

                }
                else if (string.IsNullOrEmpty(AllActivitiesList[i].ActSgrp))
                {
                    isValid = false;
                    break;
                }
                else if (string.IsNullOrEmpty(AllActivitiesList[i].Activity))
                {
                    isValid = false;
                    break;
                }
            }

            if (isEditable == true)
            {
                if (isValid)
                {
                    NavigateDataBetweenScreens();
                }
                else
                {
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                }
            }
            else
            {
                NavigateDataBetweenScreens();
            }
        }


        private void NavigateDataBetweenScreens()
        {
            DataSent?.Invoke(this, AllActivitiesList.ToList());
             MopupService.Instance.PopAsync(true);
        }
    }
}
