using System.Collections.ObjectModel;
using System.Windows.Input;


using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage
{
    public class TaxEvasionReportTypePageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand BackButtonClicked { get; set; }
        // public ICommand OnNextClicked { get; set; }
        public ICommand OnNextClicked { get; set; }
        #endregion
        private TaxEvasionReportDetails _taxEvasionListobj = null;
        public TaxEvasionReportDetails TaxEvasionListobj
        {
            get
            {
                return _taxEvasionListobj;
            }
            set
            {
                _taxEvasionListobj = value;
                //if (_selectedTaxEvasionListItem != null)
                //{ passSelectedTaxEvasionItem(); }
                OnPropertyChanged("TaxEvasionListobj");
            }
        }

        private ObservableCollection<TaxEvasionCategoriesDataModel> _reportTypes = null;
        public ObservableCollection<TaxEvasionCategoriesDataModel> ReportTypes
        {
            get
            {
                return _reportTypes;
            }
            set
            {
                _reportTypes = value;

                //if (_selectedTaxEvasionListItem != null)
                //{ passSelectedTaxEvasionItem(); }
                OnPropertyChanged("ReportTypes");
            }
        }

        private TaxEvasionCategoriesDataModel _selectedReportTypeListItem;
        public TaxEvasionCategoriesDataModel SelectedReportTypeListItem
        {
            get
            {
                return _selectedReportTypeListItem;
            }
            set
            {
                try
                {
                    _selectedReportTypeListItem = value;

                    if (_selectedReportTypeListItem != null)
                    {
                       
                    }

                    OnPropertyChanged("SelectedReportTypeListItem");
                }
                catch (Exception)
                {


                }
            }
        }
        private Color _nextbuttonDisableColor = (Color)Application.Current.Resources["Primary"];
        public Color NextbuttonDisableColor
        {
            get
            {
                return _nextbuttonDisableColor;
            }
            set
            {
                _nextbuttonDisableColor = value;
                OnPropertyChanged("NextbuttonDisableColor");
            }
        }
        private bool _isnextbuttonEnable = false;
        public bool IsnextbuttonEnable
        {
            get
            {
                return _isnextbuttonEnable;
            }
            set
            {
                _isnextbuttonEnable = value;
                OnPropertyChanged("IsnextbuttonEnable");
            }
        }
        //MobileNumber
        private string _mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                OnPropertyChanged("MobileNumber");
            }
        }
        private string _categorySelected_Index = "0";
        public string CategorySelected_Index
        {
            get
            {
                return _categorySelected_Index;
            }
            set
            {
                _categorySelected_Index = value;
                OnPropertyChanged("CategorySelected_Index");
            }
        }
        

        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                TaxEvasionCategoriesModel rootObject = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetCategories();
                PopToRootPage();
                if (rootObject != null)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    if (rootObject.Data != null)
                    {
                        ReportTypes = new ObservableCollection<TaxEvasionCategoriesDataModel>();
                        foreach (TaxEvasionCategoriesDataModel taxEvasionCategoriesDataModel in rootObject.Data)
                        {
                            ReportTypes.Add(taxEvasionCategoriesDataModel);
                        }
                    }
                    else
                    {
                        _navigationService.GoBack();
                    }
                }
                else
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public TaxEvasionReportTypePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            try
            {
                BackButtonClicked = new Command(() =>
                {
                    if (!IsLoading)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.GoBack();
                        });
                    }
                });

                OnNextClicked = new Command(OnNextButtonClicked);

             
            }
            catch (Exception)
            {


            }
        }

        public void OnNextButtonClicked()
        {
            if (IsnextbuttonEnable == true)
            {
                try
                {
                    navigateToFormPage();
                }
                catch (Exception)
                {


                }
            }
        }

        public void navigateToFormPage()
        {
            Task.Run(() =>
            {
                IsLoading = true;
            });
            Task.Run(() =>
            {
                TaxEvasionListobj = new TaxEvasionReportDetails();
                TaxEvasionListobj.Category = SelectedReportTypeListItem.Id.ToString();
                TaxEvasionListobj.CategoryTitle = SelectedReportTypeListItem.Title;

                if (!string.IsNullOrEmpty(MobileNumber))
                {
                    TaxEvasionListobj.PhoneNumber = MobileNumber;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = false;
                        _navigationService.NavigateTo(App.TaxEvasionReportFormPageView, TaxEvasionListobj);
                    });
                }
            });
        }

        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}
