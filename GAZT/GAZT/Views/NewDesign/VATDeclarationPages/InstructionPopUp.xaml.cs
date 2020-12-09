using Rg.Plugins.Popup.Pages;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionPopUp : PopupPage
    {
        public InstructionPopUp()
        {
            InitializeComponent();
            SetLTR();
        }

        private void SetLTR()
        {
            try
            {
                if (App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
                else
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
                }
            }
            catch (Exception gec)
            {
            }
        }

        private void OnClickedFAQ(object sender, EventArgs e)
        {
            string FaqUrl = string.Empty;
            if (App.IsArabic)
            {
                FaqUrl = "https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx";
            }
            else
            {
                FaqUrl = "https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx";
            }
            Device.OpenUri(new Uri(FaqUrl));
        }
    }
}