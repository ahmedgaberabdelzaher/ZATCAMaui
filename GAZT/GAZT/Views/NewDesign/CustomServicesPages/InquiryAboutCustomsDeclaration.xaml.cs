using System;
using System.Collections.Generic;
using EGAZT.Models.CustomServices;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.CustomServicesPages
{
    public partial class InquiryAboutCustomsDeclaration : ContentPage
    {
        InquiryAboutCustomsDeclarationViewModel viewModel;
        public InquiryAboutCustomsDeclaration()
        {
            //var prism = PrismApplicationBase.Current.Container;
            // var viewModel2 = (Application.Current as App).Container.Resolve<InquiryAboutCustomsDeclarationViewModel>();
            //var viewModel2=prism.Resolve<InquiryAboutCustomsDeclarationViewModel>();
            viewModel= App.Locator.InquiryAboutCustomsDeclarationViewModel;
             BindingContext = viewModel;
            SetLTR();
           // SetPickerFont();
            InitializeComponent();

        }

     
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;


            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;


                // Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }



    }
}
