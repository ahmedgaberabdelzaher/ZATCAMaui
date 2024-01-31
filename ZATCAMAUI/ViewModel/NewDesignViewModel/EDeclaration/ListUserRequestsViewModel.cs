using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ListUserRequestsViewModel : BaseEDeclarationViewModel
    {
        ObservableCollection<TravelerDeclarationResponse> _InquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>();
        public ObservableCollection<TravelerDeclarationResponse> InquireListOfUser { get { return _InquireListOfUser; } set { _InquireListOfUser = value; RaisePropertyChanged(); } }

        ObservableCollection<TravelerDeclarationResponse> _TempInquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>();
        public ObservableCollection<TravelerDeclarationResponse> TempInquireListOfUser { get { return _TempInquireListOfUser; } set { _TempInquireListOfUser = value; RaisePropertyChanged(); } }

        string _PreviousRequests;
        public string PreviousRequests { get { return _PreviousRequests; } set { _PreviousRequests = value; RaisePropertyChanged(); } }

        string searchInput;
        public string SearchInput { get { return searchInput; } set { searchInput = value; RaisePropertyChanged(); } }


        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        var travelId = App.Locator.StateManager.GetItem("TravelId") as string;
                        var result = await DeclerationServices?.GetListInquireDecleration(travelId);

                        if (result?.Item1?.header?.status.code == "I000000")
                        {
                            InquireListOfUser = result?.Item1?.data;

                            if (InquireListOfUser != null)
                            {
                                TempInquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>(InquireListOfUser);
                                PreviousRequests = string.Format(AppResources.PreviousRequests, InquireListOfUser.Count);
                                foreach (var item in InquireListOfUser)
                                {
                                    item.TravelDateString = DateTimeHelper.DateTimeFormater(item.travelDate);
                                }

                            }
                            IsLoading = false;
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
                            IsLoading = false;

                        }

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }


                });
            }
        }

        public ICommand SearchCommand
        {

            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(SearchInput.ToLower()))
                            InquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>(TempInquireListOfUser);
                        else
                        {
                            var result = TempInquireListOfUser.Where(s => s.ReferenceID.ToLower().Contains(SearchInput.ToLower()));
                            InquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>(result);
                        }
                    }
                    catch (Exception)
                    {
                        SearchInput = string.Empty;
                    }

                });
            }
        }

        public ICommand SelectedItemCommand
        {

            get
            {
                return new Command<object>(async (item) =>
                {
                    try
                    {
                        var selected = item as TravelerDeclarationResponse;

                        if (selected != null)
                        {
                            IsLoading = true;
                            var result = await DeclerationServices?.GetInquireDecleration(selected.ReferenceID, selected.travelID);

                            if (result?.Item1?.header?.status.code == "I000000")
                            {
                                var inquireDecleration = result?.Item1?.data?.travelerDeclaration;
                                if (inquireDecleration != null)
                                {
                                    App.Locator.StateManager.SetItem("inquireDeclaration", inquireDecleration);
                                    _navigationService.NavigateTo("ReviewRequestPage");

                                }
                                IsLoading = false;
                            }
                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.RequestTimeoutDescription;
                                IsLoading = false;

                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        private void ResetData()
        {
            SearchInput = string.Empty;
            PreviousRequests = string.Empty;
            InquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>();
            TempInquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>();
        }
        public void BackMethod()
        {
            ResetData();
            _navigationService.GoBack();
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    BackMethod();

                });
            }
        }

        public ListUserRequestsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

