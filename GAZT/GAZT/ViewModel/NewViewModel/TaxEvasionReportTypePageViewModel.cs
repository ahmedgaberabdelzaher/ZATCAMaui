using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class TaxEvasionReportTypePageViewModel : ViewModelBase
    {
        #region Variable
       
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand OnNextClicked { get; set; }
        public ICommand OnBClicked { get;  set; }

        #endregion
        private TaxEvasionReport _taxEvasionListobj = null;
        public TaxEvasionReport TaxEvasionListobj
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


        private bool _isimgVisiblec1 = false;
        public bool IsimgVisiblec1
        {
            get
            {
                return _isimgVisiblec1;
            }
            set
            {
                _isimgVisiblec1 = value;
                RaisePropertyChanged("IsimgVisiblec1");
            }
        }

        private bool _isimgVisiblec2 = false;
        public bool IsimgVisiblec2
        {
            get
            {
                return _isimgVisiblec2;
            }
            set
            {
                _isimgVisiblec2 = value;
                RaisePropertyChanged("IsimgVisiblec2");
            }
        }
        private bool _isimgVisiblec3 = false;
        public bool IsimgVisiblec3
        {
            get
            {
                return _isimgVisiblec3;
            }
            set
            {
                _isimgVisiblec3 = value;
                RaisePropertyChanged("IsimgVisiblec3");
            }
        }
        private bool _isimgVisiblec4 = false;
        public bool IsimgVisiblec4
        {
            get
            {
                return _isimgVisiblec4;
            }
            set
            {
                _isimgVisiblec4 = value;
                RaisePropertyChanged("IsimgVisiblec4");
            }
        }
        private bool _isimgVisiblec5 = false;
        public bool IsimgVisiblec5
        {
            get
            {
                return _isimgVisiblec5;
            }
            set
            {
                _isimgVisiblec5 = value;
                RaisePropertyChanged("IsimgVisiblec5");
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
                    _navigationService.GoBack();
                });

                OnBClicked = new Command(() =>
                {
                    CategorySelected_Index = "0";


                    try
                    {
                        if (IsimgVisiblec1 == true)
                        {
                            CategorySelected_Index = "1";
                        }
                        else if (IsimgVisiblec2 == true)
                        {
                            CategorySelected_Index = "2";
                        }
                        else if (IsimgVisiblec3 == true)
                        {
                            CategorySelected_Index = "3";
                        }
                        else if (IsimgVisiblec4 == true)
                        {
                            CategorySelected_Index = "4";
                        }
                        else if (IsimgVisiblec5 == true)
                        {
                            CategorySelected_Index = "5";
                        }

                        if (CategorySelected_Index != "0")
                        {
                            TaxEvasionListobj.ViolationType = CategorySelected_Index;

                            //TaxEvasionReportList tax = new TaxEvasionReportList();

                              _navigationService.NavigateTo(App.TaxEvasionReportFormPageView, TaxEvasionListobj);
                            
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
            catch(Exception ex)
            {

            }
        

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
