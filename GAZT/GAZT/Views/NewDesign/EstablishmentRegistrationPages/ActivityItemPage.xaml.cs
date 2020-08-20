using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class ActivityItemPage : ContentPage
    {
        public ActivityItemPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.ActivityItemPage;
            SetLTR();
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
