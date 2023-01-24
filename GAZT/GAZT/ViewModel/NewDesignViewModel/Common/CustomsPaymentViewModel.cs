using System;
using System.Windows.Input;
using EGAZT.AppConfigurations;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    public class CustomsPaymentViewModel:BaseViewModel
    {
        string pageURL;
        public string PageURL { get { return pageURL; } set { pageURL = value; RaisePropertyChanged(); } }

        IE_DeclerationServices _iE_DeclerationServices;

        public CustomsPaymentViewModel(INavigationService navigationServices, IDialogService dialogService, IE_DeclerationServices e_DeclerationServices) : base(navigationServices, dialogService)
        {
            _iE_DeclerationServices = e_DeclerationServices;

        }

        public ICommand GoTransactionPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        var result = await _iE_DeclerationServices?.GetTransactionId();

                        if (result.Item2)
                        {
                            var id = result.Item1;
                            var lang = App.IsArabic ? "ar" : "en";

                            PageURL = PageSettings.GetCustomsPaymentUrl() + id+"?local="+lang+"&mobile=1";
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

                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
            }
        }

    }
}

