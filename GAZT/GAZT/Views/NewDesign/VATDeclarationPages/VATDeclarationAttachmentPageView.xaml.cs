using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    public partial class VATDeclarationAttachmentPageView : ContentPage
    {
        VATDeclarationAttachmentPageViewModel viewModel;
        public VATDeclarationAttachmentPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATDeclarationAttachmentPageView;
            this.BindingContext = viewModel;
        }
    }
}
