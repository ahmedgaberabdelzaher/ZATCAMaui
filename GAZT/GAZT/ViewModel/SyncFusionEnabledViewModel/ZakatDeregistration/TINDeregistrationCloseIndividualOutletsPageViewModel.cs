using System;
using System.Collections.ObjectModel;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class TINDeregistrationCloseIndividualOutletsPageViewModel: ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public string SelectedReason = string.Empty;


        private bool _isReasonViewEnabled = true;
        public bool IsReasonViewEnabled
        {
            get
            {
                return _isReasonViewEnabled;
            }
            set
            {
                _isReasonViewEnabled = value;
                RaisePropertyChanged("IsReasonViewEnabled");
            }
        }
        private FieldValidations _tinText { get; set; }
        public FieldValidations TinText
        {
            get
            {
                return _tinText;
            }
            set
            {
                _tinText = value;
                RaisePropertyChanged("TinText");
            }
        }

        private FieldValidations _idTypeText { get; set; }
        public FieldValidations IdTypeText
        {
            get
            {
                return _idTypeText;
            }
            set
            {
                _idTypeText = value;
                RaisePropertyChanged("IdTypeText");
            }
        }

        private FieldValidations _idNumberText { get; set; }
        public FieldValidations IdNumberText
        {
            get
            {
                return _idNumberText;
            }
            set
            {
                _idNumberText = value;
                RaisePropertyChanged("IdNumberText");
            }
        }

        private FieldValidations _dobText { get; set; }
        public FieldValidations DobText
        {
            get
            {
                return _dobText;
            }
            set
            {
                _dobText = value;
                RaisePropertyChanged("DobText");
            }
        }

        private FieldValidations _firstNameText { get; set; }
        public FieldValidations FirstNameText
        {
            get
            {
                return _firstNameText;
            }
            set
            {
                _firstNameText = value;
                RaisePropertyChanged("FirstNameText");
            }
        }

        private FieldValidations _surnameText { get; set; }
        public FieldValidations SurnameText
        {
            get
            {
                return _surnameText;
            }
            set
            {
                _surnameText = value;
                RaisePropertyChanged("SurnameText");
            }
        }

        private FieldValidations _fathersNameText { get; set; }
        public FieldValidations FathersNameText
        {
            get
            {
                return _fathersNameText;
            }
            set
            {
                _fathersNameText = value;
                RaisePropertyChanged("FathersNameText");
            }
        }

        private FieldValidations _grandFathersNameText { get; set; }
        public FieldValidations GrandFathersNameText
        {
            get
            {
                return _grandFathersNameText;
            }
            set
            {
                _grandFathersNameText = value;
                RaisePropertyChanged("GrandFathersNameText");
            }
        }

        private FieldValidations _familyNameText { get; set; }
        public FieldValidations FamilyNameText
        {
            get
            {
                return _familyNameText;
            }
            set
            {
                _familyNameText = value;
                RaisePropertyChanged("FamilyNameText");
            }
        }

        private FieldValidations _name1Text { get; set; }
        public FieldValidations Name1Text
        {
            get
            {
                return _name1Text;
            }
            set
            {
                _name1Text = value;
                RaisePropertyChanged("Name1Text");
            }
        }

        private FieldValidations _name2Text { get; set; }
        public FieldValidations Name2Text
        {
            get
            {
                return _name2Text;
            }
            set
            {
                _name2Text = value;
                RaisePropertyChanged("Name2Text");
            }
        }
        private int _selectedOutletOptionIndex;
        public int SelectedOutletOptionIndex
        {
            get
            {
                return _selectedOutletOptionIndex;
            }
            set
            {
                _selectedOutletOptionIndex = value;
                RaisePropertyChanged("SelectedOutletOptionIndex");
            }
        }

  

        public ObservableCollection<TINDeregistrationModel> c { get; set; }

        public ObservableCollection<TINDeregistrationModel> outletDecisionOptions { get; set; }
        public ObservableCollection<TINDeregistrationModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }
        public TINDeregistrationCloseIndividualOutletsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

                //            CloseBtnTapped = new Command(async () =>
                //            {
                //    _navigationService.GoBack();
                //});         

                           

                TinText = new FieldValidations();
                IdTypeText = new FieldValidations();
                IdNumberText = new FieldValidations();
                DobText = new FieldValidations();
                FirstNameText = new FieldValidations();
                SurnameText = new FieldValidations();
                FathersNameText = new FieldValidations();
                FamilyNameText = new FieldValidations();
                GrandFathersNameText = new FieldValidations();



                AddOutletDecisionOptions();
          
        }


        public void AddOutletDecisionOptions()
        {
            if (OutletDecisionOptions == null)
                OutletDecisionOptions = new ObservableCollection<TINDeregistrationModel>();

            // OutletDecisionOptions.Clear();

            //if (SelectedReason == AppResources.TinDeregistrationReasonBankruptcy || SelectedReason == AppResources.TinDeregistrationReasonDeath
            //    || SelectedReason == AppResources.TinDeregistrationReasonLiquidation)
            //{
            OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseAllOutlets,
                    ActiveOutletDecisionOptionsIsSelected = true,
                    OutletOptionIndex = "1"
                });
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
                    ActiveOutletDecisionOptionsIsSelected = false,
                    OutletOptionIndex = "2"
                });
                OutletDecisionOptions.Add(new TINDeregistrationModel
                {
                    ActiveOutletDecisionOptions = AppResources.TinDeregistrationCloseOutletsIndividually,
                    ActiveOutletDecisionOptionsIsSelected = false,
                    OutletOptionIndex = "3"
                });
            //}
            //else
            //{
            //    OutletDecisionOptions.Add(new TINDeregistrationModel
            //    {
            //        ActiveOutletDecisionOptions = AppResources.TinDeregistrationTransferAllOutletsToSingle,
            //        ActiveOutletDecisionOptionsIsSelected = false
            //    });
            //}
        }

    }
}
