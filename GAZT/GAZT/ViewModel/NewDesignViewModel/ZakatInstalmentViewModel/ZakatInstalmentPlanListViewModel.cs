using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.VATInstalmentModels;
using EGAZT.Models.ZakatInstalationModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region commands
        public ICommand ReqInstalmentBtnTapped { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand CloseClick { get; set; }

        #endregion

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(async () =>
            {
                if (IsZakatLandingPageVisible)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (CreateZakatInstalmentBtnVisible)
                {
                    EnableZakatLandingPage();
                }
                else if (IsZakatPlanViewVisible)
                {
                    EnableCreateZakatInstalment();
                }
                else if (IsRevokZakatInstalmentVisible)
                {
                    EnableZakatLandingPage();
                }


                CloseClick = new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                });

            });

            EnableZakatLandingPage();

            ReqInstalmentBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            });

        }


        #region Views Enabling
        public void EnableZakatLandingPage()
        {
            AddOutletDecisionOptions();

            IsZakatLandingPageVisible = true;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatPlanViewVisible = false;
            IsRevokZakatInstalmentVisible = false;


        }
        public void EnableCreateZakatInstalment()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = true;
            IsZakatPlanViewVisible = false;
            IsRevokZakatInstalmentVisible = false;
        }
        public void EnableIsZakatPlanVIew()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatPlanViewVisible = true;
            IsRevokZakatInstalmentVisible = false;
        }
        public void EnableRevokZakatInstalment()
        {
            IsZakatLandingPageVisible = false;
            CreateZakatInstalmentBtnVisible = false;
            IsZakatPlanViewVisible = false;
            IsRevokZakatInstalmentVisible = true;
        }

        #endregion

        #region Observables

        private bool _isZakatLandingPageVisible = true;
        public bool IsZakatLandingPageVisible
        {
            get
            {
                return _isZakatLandingPageVisible;
            }
            set
            {
                _isZakatLandingPageVisible = value;
                RaisePropertyChanged("IsZakatLandingPageVisible");
            }
        }

        private bool _createZakatInstalmentBtnVisible = false;
        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _createZakatInstalmentBtnVisible;
            }
            set
            {
                _createZakatInstalmentBtnVisible = value;
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }

        private bool _isZakatPlanViewVisible = false;
        public bool IsZakatPlanViewVisible
        {
            get
            {
                return _isZakatPlanViewVisible;
            }
            set
            {
                _isZakatPlanViewVisible = value;
                RaisePropertyChanged("IsZakatPlanViewVisible");
            }
        }

        private bool _isRevokZakatInstalmentVisible = true;
        public bool IsRevokZakatInstalmentVisible
        {
            get
            {
                return _isRevokZakatInstalmentVisible;
            }
            set
            {
                _isRevokZakatInstalmentVisible = value;
                RaisePropertyChanged("IsRevokZakatInstalmentVisible");
            }
        }

        public ObservableCollection<InstalmentPlanModel> outletDecisionOptions { get; set; }
        public ObservableCollection<InstalmentPlanModel> OutletDecisionOptions
        {
            get
            {
                return outletDecisionOptions;
            }

            set
            {
                if (outletDecisionOptions == value)
                {
                    return;
                }

                outletDecisionOptions = value;
                RaisePropertyChanged("OutletDecisionOptions");
            }
        }

        #endregion

        #region Lists

        public void AddOutletDecisionOptions()
        {

            var outletDecisionOptions = new ObservableCollection<InstalmentPlanModel>();
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanCreate_Display,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            outletDecisionOptions.Add(new InstalmentPlanModel
            {
                ActiveOutletDecisionOptions = AppResources.ZakatInstalmentPlanRevokeZAKAT,
                ActiveOutletDecisionOptionsIsSelected = false
            });
            OutletDecisionOptions = outletDecisionOptions;
        }

        #endregion

        //public ZakatInstalmentPlanListModel instalmentListModel { get; set; }
        //public ZakatInstalmentPlanListModel InstalmentListModel
        //{
        //    get
        //    {
        //        return instalmentListModel;
        //    }

        //    set
        //    {
        //        if (instalmentListModel == value)
        //        {
        //            return;
        //        }

        //        instalmentListModel = value;
        //        RaisePropertyChanged("InstalmentListModel");
        //    }
        //}


    }
}
