using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages
{
    public partial class ZAKATReturnDetailsView : ContentPage
    {
        ZAKATReturnDetailsViewModel viewModel;
        public ZAKATReturnDetailsView(string fbguid)
        {
            InitializeComponent();
            viewModel = App.Locator.ZAKATReturnDetailsView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            viewModel.Fbguid = fbguid;
            SetLTR();

          viewModel.OnPageLoad(fbguid);

        }

        protected override void OnAppearing()
        {

        //date.Text = viewModel.Abrzu;
        }
        
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
           this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async void OnReleaseBillsButtonClicked(object sender, EventArgs e)
        {
            if (viewModel.ZakatReturnDetails.d.Statusz.Equals("E0001") || viewModel.ZakatReturnDetails.d.Statusz.Equals("IP011"))
            {
                if (App.IsArabic)
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZCancel, AppResources.ZZZOkayText);
                    if (!result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
                else
                {
                    var result = await this.DisplayAlert(AppResources.ZZConfirmation, AppResources.ZZDoyouwanttoreleasethedeclaration, AppResources.ZZZOkayText, AppResources.ZZCancel);
                    if (result)
                    {
                        await viewModel.OnReleaseOrBillsClicked();
                    }

                }
            }
            else
            {
                await viewModel.OnReleaseOrBillsClicked();
            }

           

        }
        private void OnEditClicked(object sender, EventArgs e)
        {

            viewModel.isLabelVisible = false;
            viewModel.isEditVisible = true;
            viewModel.IsEditTextVisible = true;
            viewModel.SetEditImage();
        }

        private void OnInfoClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentPopUp(viewModel.ZakatReturnDetail));
        }
    }
}
