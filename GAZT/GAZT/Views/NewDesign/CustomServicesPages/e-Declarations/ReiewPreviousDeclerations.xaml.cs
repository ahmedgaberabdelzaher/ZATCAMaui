using System;
using System.Collections.Generic;
using EGAZT.Helper;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class ReiewPreviousDeclerations : BaseContentPage
    {
        E_DeclerationViewModel viewModel;
       
        public ReiewPreviousDeclerations()
        {
            viewModel = App.Locator.eDeclerationViewModel;
           
            BindingContext = viewModel;
            InitializeComponent();
        }

        async void WebView_Navigating(System.Object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
         // await  DownlOadFile("https://soga.fasah.sa/api/attachment/v1/download?fileUri=ru4ldgUPCt6Oc3M9RO3ejmOdI4BeDCKiFLVSxny%2FtuXK%2BsZ5XM6aOC%2BobOKC%2FtCPWz%2B5ERM%2BsmPh0pHCSMM060MeTmQnrh1aL%2FyUnT%2BIj4XV9%2FzYbCNMsu4JK9wLJuth");
           /* if (e.Url.Contains("file"))
            {
                viewModel.IsLoading = true;
              await viewModel.DownlOadFile(e.Url);
            }*/
            
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
