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

        string paymentCode;
        public string PaymentCode { get { return paymentCode; } set { paymentCode = value; RaisePropertyChanged(); } }

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

                        var lang = App.IsArabic ? "ar" : "en";
                        if (!string.IsNullOrWhiteSpace(PageSettings.Target_Environment) && PageSettings.Target_Environment.Equals("Prod"))
                        {

                            // Sample payment prod codes
                            //"9c602810-f4d5-4838-8103-0ad1323cfcc4";
                            //"9cf1e0f2-e49f-4447-8a65-267797ce1720"

                            PageURL = $"{PageSettings.GetCustomsPaymentUrl()}{PaymentCode}?local={lang}&mobile=1";
                            return;
                        }

                        // For STG 
                        IsLoading = true;

                        var result = await _iE_DeclerationServices?.GetTransactionId();

                        if (result.Item2)
                        {
                            var id = result.Item1;
                            PageURL = $"{PageSettings.GetCustomsPaymentUrl()}{id}?local={lang}&mobile=1";
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

