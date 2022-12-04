using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Controls;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ProductDeclarationViewModel: BaseEDeclarationViewModel
    {
        #region Properties
        //bool isArrivingPlaneSelected = true;
        //public bool Te { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }
        #endregion


        #region Commands
        //public ICommand Tes
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {

        //        });
        //    }
        //}
        public ICommand OpenTobacoTypesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;

                        var reportType = await this._submitReportServices.GetReportType();
                        var result = reportType?.reportTaxTypeList?.Select(c => new BottomSheetModel() { Id = c.reportTaxTypeCode, Name = c.reportTaxTypeName }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ReportType;
                        TempBottomSheetList = BottomSheetList;
                        IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                  
                });

            }
        }

        #endregion

        public ProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

