using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttachmentPageView : ContentPage
    {

        #region Variable
        AttachmentPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor
        public AttachmentPageView(VATDeclaration vATDeclaration)
        {



            InitializeComponent();
            try
            {
                viewModel = App.Locator.AttachmentPageView;
                this.BindingContext = viewModel;

                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;


                    if (viewModel.VATDeclarationData.d.ATTACHSet.results.Count != 0)
                    {
                        ObservableCollection<Attachment> myCollection = new ObservableCollection<Attachment>(viewModel.VATDeclarationData.d.ATTACHSet.results as List<Attachment>);

                        viewModel.VatAttachmentsList = myCollection;
                    }

                }


            }
            catch (Exception e)
            {

            }
        }
        #endregion

        #region Method

        private void OnDeleteAttachmentClicked(object sender, EventArgs e)
        {
            Image arrowImage = sender as Image;
            Attachment attachment = (Attachment)arrowImage.BindingContext;
            var results = WebServiceManager.GAZTDeleteVATDeclarationAttachment(attachment.Filename, viewModel.VATDeclarationData.d.ReturnIdz);
        }

        #endregion



    }
}