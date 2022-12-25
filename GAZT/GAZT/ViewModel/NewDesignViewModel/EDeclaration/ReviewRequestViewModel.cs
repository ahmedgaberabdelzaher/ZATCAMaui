using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using EGAZT.Controls;
using System.Linq;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
	public class ReviewRequestViewModel:BaseViewModel
	{
        ObservableCollection<BottomSheetModel> _InquireList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> InquireList { get { return _InquireList; } set { _InquireList = value; RaisePropertyChanged(); } }

        TravelerDeclarationResponse _Inquire = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse Inquire { get { return _Inquire; } set { _Inquire = value; RaisePropertyChanged(); } }

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command( _ =>
                {
                    if(Inquire.IsNotPaid)
                        _navigationService.NavigateTo("EDeclarationPaymentPage",Inquire);
                });
            }
        }
        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command( _ =>
                {
                    try
                    {
                        var date = DateTime.Now;
                        Inquire = App.Locator.StateManager.GetItem("inquireDecleration") as TravelerDeclarationResponse;
                        if (Inquire != null)
                        {
                            DateTime.TryParse(Inquire.travelDate.ToString(), out date);
                            Inquire.TravelDateString = date.ToString("dd/MM/yyyy");
                            Inquire.totalFees = Math.Round(Inquire.totalFees, 2);
                            Inquire.tobacco?.ForEach(t => { InquireList.Add(new BottomSheetModel { Name = t.Name, Id = $"(x {t.count.ToString()})" }); });
                            Inquire.product?.ForEach(p => { InquireList.Add(new BottomSheetModel { Name = p.Name, Id = $"(x {p.count.ToString()})" }); });
                            Inquire.currency?.ForEach(c => { InquireList.Add(new BottomSheetModel { Name = c.Name }); });
                            Inquire.restricted?.ForEach(r => { InquireList.Add(new BottomSheetModel { Name = r.Name,Id = $"(x {r.count.ToString()})" }); });
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                   
                   
                });
            }
        }


        public ReviewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

