using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class TaxEvasionReportTypePageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                RaisePropertyChanged("TaxEvasionListobj");
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
                RaisePropertyChanged("ReportTypes");
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
                        //_selectedReportTypeListItem.IsTypeSelected = true;

                        //ObservableCollection<TaxEvasionCategoriesDataModel> tempReportType = ReportTypes;

                        //foreach (TaxEvasionCategoriesDataModel taxEvasionCategoriesDataModel in tempReportType)
                        //{
                        //    if(taxEvasionCategoriesDataModel.Id == _selectedReportTypeListItem.Id)
                        //    {
                        //        taxEvasionCategoriesDataModel.IsTypeSelected = true;
                        //    }
                        //    else
                        //    {
                        //        taxEvasionCategoriesDataModel.IsTypeSelected = false;
                        //    }
                        //}

                        //ReportTypes = tempReportType;
                        //passSelectedTaxEvasionItem(_selectedTaxEvasionListItem);
                    }

                    RaisePropertyChanged("SelectedReportTypeListItem");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                }
            }
        }
        private Color _nextbuttonDisableColor =  (Color)Application.Current.Resources["Primary"];
        public Color NextbuttonDisableColor
        {
            get
            {
                return _nextbuttonDisableColor;
            }
            set
            {
                _nextbuttonDisableColor = value;
                RaisePropertyChanged("NextbuttonDisableColor");
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
                RaisePropertyChanged("IsnextbuttonEnable");
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
                RaisePropertyChanged("MobileNumber");
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
                RaisePropertyChanged("CategorySelected_Index");
            }
        }
        //IsLoading
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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

                    if(rootObject.Data != null)
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
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

        public TaxEvasionReportTypePageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            try
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
                BackButtonClicked = new Xamarin.Forms.Command(() =>
                {
                    if (!IsLoading)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.GoBack();
                        });
                    }
                });

                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                //});

                this.OnNextClicked = new Command(this.OnNextButtonClicked);

                //OnNextClicked = new Command(async () =>
                //{
                //    if (IsnextbuttonEnable == true)
                //    {
                //        try
                //        {
                //            await navigateToFormPage();
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    Device.BeginInvokeOnMainThread(() =>
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
    }
}
