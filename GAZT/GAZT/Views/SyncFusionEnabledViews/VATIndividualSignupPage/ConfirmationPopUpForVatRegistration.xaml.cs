using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationPopUpForVatRegistration : PopupPage
    {
         string _FromWhere = string.Empty;
        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public ConfirmationPopUpForVatRegistration(string ConfirmationText,string FromWhere)
        {
            InitializeComponent();
            _FromWhere =  FromWhere;
            confirmationText.Text = ConfirmationText;
            SetLTR();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void OnOkayButtonClicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_FromWhere))
            {
                if(_FromWhere== "FileAttachmentPopUpPageView")
                {
                    MessagingCenter.Send<Object, string>(this, "YesPressedToDeleteAttachment", "Yes");
                    OnSelect?.Invoke("Yes");
                    PopupNavigation.Instance.PopAsync();
                }
                else if(_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<Object, string>(this, "YesPressedToDeleteFinancialAttachment", "Yes");
                    OnSelect?.Invoke("Yes");
                    PopupNavigation.Instance.PopAsync();
                }
            }
        

        }

        private void OnCancelClicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(_FromWhere))
            {
                if (_FromWhere == "FileAttachmentPopUpPageView")
                {
                    MessagingCenter.Send<Object, string>(this, "NoPressedToDeleteAttachment", "No");
                    OnSelect?.Invoke("No");
                    PopupNavigation.Instance.PopAsync();
                }
                else if (_FromWhere == "FinancialDetailAttachmentPopupPageView")
                {
                    MessagingCenter.Send<Object, string>(this, "NoPressedToDeleteFinancialAttachment", "No");
                    OnSelect?.Invoke("No");
                    PopupNavigation.Instance.PopAsync();
                }
            }

            

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}