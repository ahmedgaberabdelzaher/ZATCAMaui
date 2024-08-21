using System.Collections.ObjectModel;
using System.Windows.Input;
using Mopups.Animations;
using Mopups.Enums;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.TINOutletDeregister;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;
using static ZATCAMAUI.Models.TINOutletDeregister.TinOutletPrevousRequestsModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;

public class TINOutletDeregistrationViewModel : BaseViewModel
{

    public TinOutletPrevousRequestsModel tinOutletPrevousRequestsModel;

    public ICommand CreateNewRequestTapped { get; set; }
    public ICommand CancelRequestTapped { get; set; }
    public ICommand DeleteRequestTapped { get; set; }

    private Command itemTappedCommand;
    public Command ItemTappedCommand
    {
        get { return itemTappedCommand; }
        protected set { itemTappedCommand = value; }
    }


    public string _searchText = "";
    /// <summary>
    /// holds the user entered text in search field.
    /// </summary>    
    public string SearchText
    {
        get
        {
            return _searchText;
        }
        set
        {
            _searchText = value;

            OnPropertyChanged("SearchText");
        }
    }
    private bool _isSearchButtonVisible = true;

    public bool IsSearchButtonVisible
    {
        get
        {
            return _isSearchButtonVisible;
        }

        set
        {
            if (_isSearchButtonVisible == value) return;

            _isSearchButtonVisible = value;
            OnPropertyChanged("IsSearchButtonVisible");
        }
    }

    private bool _isCloseButtonVisible = false;

    public bool IsCloseButtonVisible
    {
        get
        {
            return _isCloseButtonVisible;
        }

        set
        {
            if (_isCloseButtonVisible == value) return;

            _isCloseButtonVisible = value;
            OnPropertyChanged("IsCloseButtonVisible");
        }
    }

    private ObservableCollection<PreviousRequests> _previousRequestList = new ObservableCollection<PreviousRequests>();
    public ObservableCollection<PreviousRequests> PreviousRequestList
    {
        get
        {
            return _previousRequestList;
        }
        set
        {
            if (_previousRequestList == value) return;

            _previousRequestList = value;

            OnPropertyChanged("PreviousRequestList");
        }
    }


    private ObservableCollection<PreviousRequests> _copiedPreviousRequestList = new ObservableCollection<PreviousRequests>();
    public ObservableCollection<PreviousRequests> CopiedPreviousRequestList
    {
        get
        {
            return _copiedPreviousRequestList;
        }
        set
        {
            if (_copiedPreviousRequestList == value) return;

            _copiedPreviousRequestList = value;

            OnPropertyChanged("CopiedPreviousRequestList");
        }
    }


    public TINOutletDeregistrationViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
    {
        
        CreateNewRequestTapped = new Command(() => {
            PreviousRequests previousRequests = new PreviousRequests();
            NavigatingtoRequestPageView(previousRequests);
        });


        CancelRequestTapped = new Command(CancelRequest);
        DeleteRequestTapped = new Command(DeleteRequest);


        itemTappedCommand = new Command(OnItemTapped);

    }

    private  void CancelRequest(object item)
    {
        try
        {
            // NOTE : from mobile we are not allowing user to create/Delete/Cancel/ Open request for Outlet De-Registrations.
            var selectedItem = item as PreviousRequests;
            if (selectedItem.TxnTp.Equals("DEREG_O"))
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(AppResources.CancelWarningMsg, AppResources.Information, AppResources.ZYes, AppResources.ZNo, (async (bool isConfirmed) =>
                {
                    if (isConfirmed == true)
                    {
                        IsLoading = true;

                        bool success = await TINDeregistrationWebServiceManager.PostCancelExistingRequest(selectedItem.Fbnum, selectedItem.Status1, selectedItem.Status2, 1);
                        IsLoading = false;

                        if (success)
                        {

                            await _dialogService.ShowMessage(AppResources.RequestCancelMsg, AppResources.Information);

                            await GetTinOutletDeregisteredRequests();
                        }
                    }
                }));
            });
        }
        catch (Exception)
        {
            IsLoading = false;
        }
    }


    private  void DeleteRequest(object item)
    {
        // NOTE : from mobile we are not allowing user to create/Delete/Cancel/ Open request for Outlet De-Registrations.
        try
        {
            // NOTE : from mobile we are not allowing user to create/Delete/Cancel/ Open request for Outlet De-Registrations.
            var selectedItem = item as PreviousRequests;
            if (selectedItem.TxnTp.Equals("DEREG_O"))
            {
                return;
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(AppResources.DeleteWarningMsg, AppResources.Information, AppResources.ZYes, AppResources.ZNo, (async (bool isConfirmed) =>
                {
                    if (isConfirmed == true)
                    {
                        IsLoading = true;
                        var selectedItem = item as PreviousRequests;
                        bool success = await TINDeregistrationWebServiceManager.PostCancelExistingRequest(selectedItem.Fbnum, selectedItem.Status1, selectedItem.Status2, 2);
                        IsLoading = false;

                        if (success)
                        {
                            await GetTinOutletDeregisteredRequests();
                        }
                    }
                }));
            });



        }
        catch (Exception)
        {
            IsLoading = false;
        }
    }

    public  void OnItemTapped(object obj)
    {
        var previousRequests = obj as PreviousRequests;
        int reqCode = 0;
        if (previousRequests.TxnTp.Equals("DEREG_T"))
        {
            reqCode = 1;
        }
        else if (previousRequests.TxnTp.Equals("DEREG_O"))
        {
            // NOTE : for mobile we are not allowing user to create or View Outlet De-Registrations.
            // reqCode = 2;
            return;
        }
        else
        {
            //PErmit
            reqCode = 3;
        }


        NavigatingtoRequestPageView(previousRequests);
    }

    public  void NavigatingtoRequestPageView(PreviousRequests requestCode)
    {
        _navigationService.NavigateTo(App.TinOutletDeRegRequestPageView, requestCode);
    }

    [Obsolete]
    public async void CreateNewRequest()
    {
        try
        {
            var pr = new TinOutletPopupPage();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };

            pr.Animation = scaleAnimation;
            await MopupService.Instance.PushAsync(pr);

        }

        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            });
        }
    }

    public async Task GetTinOutletDeregisteredRequests()
    {
        

        try
        {
            IsLoading = true;
            Thread.Sleep(1000);
            if (CopiedPreviousRequestList != null)
            {
                CopiedPreviousRequestList.Clear();
            }

            tinOutletPrevousRequestsModel = await TINDeregistrationWebServiceManager.GetTinOutletDeRegisterPreviousRequests();

            if (tinOutletPrevousRequestsModel != null && tinOutletPrevousRequestsModel.D != null && tinOutletPrevousRequestsModel.D.WIItemSet.Count > 0)
            {
                PreviousRequestList = new ObservableCollection<TinOutletPrevousRequestsModel.PreviousRequests>(tinOutletPrevousRequestsModel.D.WIItemSet);
                CopiedPreviousRequestList.Clear();
                CopiedPreviousRequestList = PreviousRequestList;


            }
            IsLoading = false;
        }
        catch (Exception)
        {
            IsLoading = false;
        }
    }
    public void FilterWithReferenceNumber()
    {
        CopiedPreviousRequestList = new ObservableCollection<PreviousRequests>(PreviousRequestList.Where(searchedObjects => searchedObjects.Fbnum.Contains(SearchText)));
    }

}
