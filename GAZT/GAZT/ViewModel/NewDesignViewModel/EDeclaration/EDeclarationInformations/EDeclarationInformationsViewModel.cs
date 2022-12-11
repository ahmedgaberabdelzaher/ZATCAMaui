using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using EGAZT.Controls;
using GAZT;
using System.Linq;
using System.Collections.Generic;
using EGAZT.Services.Interface;
using System.Linq.Expressions;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel : BaseEDeclarationViewModel
    {
        #region Properties

        private bool isItsSourceSelected;
        private bool isNationalitySelected;
        private bool isComingGoingSelected;
        private bool isPortSelected;
        private bool isTravelPurposeSelected;

        #endregion


        #region Commands
        
        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {
                        var entry = e as BorderlessEntry;
                        var value = entry.Text.ToLower();
                        if (string.IsNullOrWhiteSpace(value))
                            BottomSheetList = TempBottomSheetList;
                        else
                        {
                            var result = BottomSheetList.Where(s => s.Name.Contains(value)).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        }
                    }
                });
            }
        }

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>(async (e) =>
                {
                    try
                    {
                        IsLoading = true;

                        //if (isNationalitySelected)
                        //{

                        //    SubmitReport.ReportTypeName = e.Name;
                        //    SubmitReport.ReportTaxType = e.Id;
                        //    ReportCategory = await this._submitReportServices.GetReportCategories(SubmitReport?.ReportTaxType);
                        //    var result = ReportCategory?.Select(c => new BottomSheetModel() { Id = c.Id, Name = c.Title }).ToList() ?? new List<BottomSheetModel>();
                        //    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        //    isNationalitySelected = false;
                        //}
                        //else if (isItsSourceSelected)
                        //{
                        //    SubmitReport.ReportCategoryName = e.Name;
                        //    SubmitReport.ReportCategory = e.Id;
                        //    isItsSourceSelected = false;
                        //    SubmitReport.MissedFieldName = string.Empty;
                        //    SubmitReport.MissedField = string.Empty;
                        //    IsMissingFieldShowen = !string.IsNullOrWhiteSpace(SubmitReport.ReportCategoryName) && SubmitReport.ReportCategory.ToLower().Equals("v36") ? true : false;
                        //}

                        //IsShowBottomSheet = false;
                        //HeaderTitle = AppResources.Submitareport;
                        //SearchText = string.Empty;
                        //TempBottomSheetList = BottomSheetList;
                        //IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }


                });
            }
        }

        

        public ICommand BackToHomeCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/Home", "0");

                });
            }
        }

        #endregion
        public EDeclarationInformationsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

