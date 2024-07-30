
using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatDeregistration
{
    public partial class TINDeRegistrationPopUp : PopupPage
    {
        public delegate void OnSaveAsDraftSelectDelegate();
        public OnSaveAsDraftSelectDelegate OnItemSelect { get; set; } = null;

        public TINDeRegistrationPopUp()
        {
            InitializeComponent();
        }

        private async void OnSaveAsDraftClicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
            OnItemSelect?.Invoke();
        }

    }
}
