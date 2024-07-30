
using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.VATAmendReactivationPages
{
    public partial class VATAmendReactivationMoreOptionPopUp : PopupPage
    {
        public delegate void OnSaveAsDraftSelectDelegate(string item);
        public OnSaveAsDraftSelectDelegate OnItemSelect { get; set; } = null;
        List<string> OptionList = new List<string>();
        public VATAmendReactivationMoreOptionPopUp(List<string> OptionList)
        {
            InitializeComponent();
            this.OptionList = OptionList;
        }

        protected override void OnAppearing()
        {
            List<Option> list = new List<Option>();
            base.OnAppearing();
            foreach (string option in OptionList)
            {
                Option OptionObj = new Option();
                OptionObj.option = option;
                list.Add(OptionObj);
            }

            optionList.ItemsSource = list;

        }

        private async void OptionItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedOption = e.Item as Option;
                await MopupService.Instance.PopAsync();
                OnItemSelect?.Invoke(selectedOption.option);
            }
            catch (Exception)
            {


            }
        }

    }


    public class Option
    {
        public string option { get; set; }
    }
}
