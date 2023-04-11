using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Helper;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Services.Classes;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using System.Linq;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ListUserRequestsViewModel : BaseEDeclarationViewModel
    {
        ObservableCollection<TravelerDeclarationResponse> _InquireListOfUser = new ObservableCollection<TravelerDeclarationResponse>();
        public ObservableCollection<TravelerDeclarationResponse> InquireListOfUser { get { return _InquireListOfUser; } set { _InquireListOfUser = value; RaisePropertyChanged(); } }

        string _PreviousRequests;
        public string PreviousRequests { get { return _PreviousRequests; } set { _PreviousRequests = value; RaisePropertyChanged(); } }


        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command( async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        var travelId= App.Locator.StateManager.GetItem("TravelId") as string;
                        var result = await DeclerationServices?.GetListInquireDecleration(travelId);

                        if (result?.Item1?.header?.status.code == "I000000")
                        {
                            InquireListOfUser = result?.Item1?.data;

                            if(InquireListOfUser !=null )
                            {
                                PreviousRequests = String.Format(AppResources.PreviousRequests, InquireListOfUser.Count);
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
                    catch (Exception ex)
                    {
                        IsLoading = false;
                    }


                });
            }
        }

        private void ResetData()
        {
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

        public ListUserRequestsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices):base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

