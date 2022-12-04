using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Services.Interface;
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

                        var topacoTypes = await DeclerationServices.GetTobacoTypes();
                        var result = topacoTypes?.Item1.data.Select(c => new BottomSheetModel() { Id = c.typeID, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
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

        public ICommand OpenTobacoItemssCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;

                        var topacoTypes = await DeclerationServices.GetTobacoTypes();
                        var result = topacoTypes?.Item1.data.Select(c => new BottomSheetModel() { Id = c.typeID, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
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
        public ProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices DeclerationServices) : base(navigationService, dialogService,DeclerationServices)
        {

        }
    }
}

