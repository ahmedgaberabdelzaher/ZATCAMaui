using Mopups.Pages;
using Mopups.Services;
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
        }
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
        }
        async void AddressList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
                await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {


            }
        }
    }
}
