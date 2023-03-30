using System;
using System.Collections.Generic;
using EGAZT.Helper;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;
using EGAZT.AppConfigurations;

namespace EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class ReiewPreviousDeclerations : BaseContentPage
    {
        E_DeclerationViewModel viewModel;
       
        public ReiewPreviousDeclerations(string token="")
        {
            viewModel = App.Locator.eDeclerationViewModel;
           
            BindingContext = viewModel;
            string source = "Bearer%20eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ5YXNzODk1NyIsIkdST1VQUyAiOiIiLCJHUk9VUFMiOiIiLCJJU19TU08gIjpmYWxzZSwiU1NPX1RPS0VOICI6IiIsIklTX1NTTyI6ZmFsc2UsIlNTT19UT0tFTiI6IiIsIkNMSUVOVF9OQU1FIjoiVEVNUF9UT0tFTiIsImlzcyI6IkZBU0FIIiwiYXVkIjoiRkFTQUggQXBwbGljYXRpb24iLCJleHAiOjE2ODAxMDE5OTZ9.fCyBlJ07q9U7MSg2m7mTojIydJgDCJ50pWtioAzL0SgmgcROsnDuW03YrgoBg_MU7FfOU1MXkox4xqE9WiLm2w";
            // webc.Source = source;
            InitializeComponent();
            var url = PageSettings.FasahRedirectUrl+token;

            //   webc.Source = "https://soga.fasah.sa/ar/redirection/1.0/?s=Brokers_optionality&t=Bearer%20eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ5YXNzODk1NyIsIkdST1VQUyAiOiJJTVBSVCxUcmFkZXIiLCJHUk9VUFMiOiJJTVBSVCxUcmFkZXIiLCJJU19TU08gIjpmYWxzZSwiU1NPX1RPS0VOICI6IiIsIklTX1NTTyI6ZmFsc2UsIlNTT19UT0tFTiI6IiIsIkNMSUVOVF9OQU1FIjoiRkFTQUgiLCJpc3MiOiJGQVNBSCIsImF1ZCI6IkZBU0FIIEFwcGxpY2F0aW9uIiwiZXhwIjoxNjgxOTk5NjU2fQ.j2rv8UWVUK2x5luvTCmrbDc7t8aTPsnP4HN33rXKANWDTDOyyJfQJQw328tXEs_TcmbZ3SlAydeM9FeV6jZhyA";
            webc.Source = url;
        }

        async void WebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
         // await  DownlOadFile("https://soga.fasah.sa/api/attachment/v1/download?fileUri=ru4ldgUPCt6Oc3M9RO3ejmOdI4BeDCKiFLVSxny%2FtuXK%2BsZ5XM6aOC%2BobOKC%2FtCPWz%2B5ERM%2BsmPh0pHCSMM060MeTmQnrh1aL%2FyUnT%2BIj4XV9%2FzYbCNMsu4JK9wLJuth");
            if (e.Url.Contains("file"))
            {
                viewModel.IsLoading = true;
              await viewModel.DownlOadFile(e.Url);
            }

            if (e.Url== "https://soga.fasah.sa/ar/login/1.0/")
            {
                 viewModel._navigationService.GoBack();
            }
            
        }
        private async Task DownlOadFile(string url)
        {
            DownloadFile downloadFile = new DownloadFile();
            var file = url.Split('.');
            var extntion= file[file.Length-1];
            await downloadFile.DownloadFileAsync($"{url}",extntion, null);
        }
    }
}
