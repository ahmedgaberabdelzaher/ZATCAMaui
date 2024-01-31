using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using System.Collections;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{

    public partial class AddressPickerPopPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;

        public AddressPickerPopPage(IEnumerable addressess)
        {
            InitializeComponent();
            addressList.ItemsSource = addressess;
            SetLTR();
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
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        async void AddressList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {


            }
        }
    }
}
