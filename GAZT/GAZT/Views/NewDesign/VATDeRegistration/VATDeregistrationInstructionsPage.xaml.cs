using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.VATDeRegistration
{
    [Preserve(AllMembers = true)]
    public partial class VATDeregistrationInstructionsPage : PopupPage
    {
        VATDeRegistrationInstructionsPageViewModel viewModel;
        public VATDeregistrationInstructionsPage(bool InstructionChecked)
        {
            InitializeComponent();

            
            //Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];

            viewModel = App.Locator.VATDeregistrationInstructionsPage;
            this.BindingContext = viewModel;
            SetLTR();
            viewModel.IsInstructionChecked = InstructionChecked;
            viewModel.isInstructionCheckedEnable = !InstructionChecked;
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

            }
        }
        protected override void OnAppearing()
        {
            //MessagingCenter.Subscribe<VATDeRegistrationInstructionsPageViewModel, bool>(this, "IsInstructionChecked", (obj, res) =>
            //{
            //    if (res)
            //        Resources["IsInstructionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
            //    else
            //        Resources["IsInstructionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
            //});
        }
    }
}
