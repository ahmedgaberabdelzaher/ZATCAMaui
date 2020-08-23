using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class RegistrationSuccessfulPage : ContentPage
    {
        public RegistrationSuccessfulPage()
        {
            InitializeComponent();
            SetLTR();
            BindingContext = App.Locator.RegistrationSuccessfulPage;
        }
        private void SetLTR()
        {

            //if (!App.IsArabic)
            //{
            this.FlowDirection = FlowDirection.LeftToRight;
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.RightToLeft;
            //}
        }
    }
}
