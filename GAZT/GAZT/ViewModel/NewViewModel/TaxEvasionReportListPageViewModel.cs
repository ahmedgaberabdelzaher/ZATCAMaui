using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class TaxEvasionReportListPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCloseClicked_Tapped { get; set; }
        public ICommand OnOpenClicked_Tapped { get; set; }
       
        private bool _setNoDataLabelVisibility = false;//SelectedTaxEvasionListItem
        private TaxEvasionReportList _selectedTaxEvasionListItem;
        public TaxEvasionReportList SelectedTaxEvasionListItem
        {
            get
            {
                return _selectedTaxEvasionListItem;

            }
            set
            {
                try
                {
                    _selectedTaxEvasionListItem = value;
                    if (_selectedTaxEvasionListItem != null)
                    {
                        passSelectedTaxEvasionItem();
                    }

                    RaisePropertyChanged("SelectedTaxEvasionListItem");
                }
                catch(Exception ex)
                {

                }
            }
        }


        public bool SetNoDataLabelVisibility
        {
            get
            {
                return _setNoDataLabelVisibility;
            }
            set
            {
                _setNoDataLabelVisibility = value;
                RaisePropertyChanged("SetNoDataLabelVisibility");
            }
        }
        private List<TaxEvasionReportList> _taxEvasionReportList;
        public List<TaxEvasionReportList> TERListReportbymobno
        {
            get
            {
                return _taxEvasionReportList;

            }
            set
            {
                _taxEvasionReportList = value;

                RaisePropertyChanged("TERListReportbymobno");
            }
        }
        private List<TaxEvasionReportList> _tERListReportbymobnoDummy;
        public List<TaxEvasionReportList> TERListReportbymobnoDummy
        {
            get
            {
                return _tERListReportbymobnoDummy;

            }
            set
            {
                _tERListReportbymobnoDummy = value;

                RaisePropertyChanged("TERListReportbymobnoDummy");
            }
        }





        public TaxEvasionReportListPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnCloseClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {
                    TERListReportbymobno.Clear();
                      TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "3") ).ToList();
                }
                catch (Exception ex)
                {
                }
            });

            //OnVATCertificateClicked = new RelayCommand(async () =>
            //{
            //    try
            //    {
            //        if (allCertificate != null)
            //        {
            //            if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
            //            {
            //                CertificateType = AppResources.VATCertificates;
            //                SetCertificateListViewVisibility();
            //                CertificateList = allCertificate.VATSet.results;
            //            }
            //            else
            //            {
            //                CertificateType = String.Empty;
            //                SetNoDataLabelViewVisibility();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //});





            OnOpenClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {
                    TERListReportbymobno.Clear();
                    TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "0")|| (x.ReportStatus == "2")).ToList();
                }
                catch (Exception ex)
                {
                }
            });







        }

        public void passSelectedTaxEvasionItem()
        {
            try
            {
                _navigationService.NavigateTo(App.TaxEvasionReportFormPageView, SelectedTaxEvasionListItem);
                SelectedTaxEvasionListItem = null;
            }
            catch(Exception ex)
            {
               
            }

        }



        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {

                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });


            }
        }

        public void OnPageLoad()
        {
            try
            {

                ReportRetriveByMobNoRootObject rlist = new ReportRetriveByMobNoRootObject();
                rlist = WebServiceManager.GAZTTESReportByMobNo("0565154482");

                PopToRootPage();



                if (rlist != null)
                {
                    if (rlist.TaxEvasionReportList != null && rlist.TaxEvasionReportList.Count > 0)
                    {
                        //CertificateType = AppResources.ZakatCertificates;
                        //SetCertificateListViewVisibility();
                        TERListReportbymobnoDummy = rlist.TaxEvasionReportList;

                        TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "0") || (x.ReportStatus == "2")).ToList();



                    }
                    else
                    {

                        SetNoDataLabelViewVisibility();
                    }

                }
                else
                {
                    SetNoDataLabelVisibility = true;

                }
            }
            catch(Exception ex)
            {
                SetNoDataLabelVisibility = true;
                _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Alerts); }
        }
        private void SetNoDataLabelViewVisibility()
        {
            //IsCertificateAvailable = false;
            SetNoDataLabelVisibility = true;
        }
        private void SetCertificateListViewVisibility()
        {
            //IsCertificateAvailable = true;
            SetNoDataLabelVisibility = false;
        }













    }
            } 
