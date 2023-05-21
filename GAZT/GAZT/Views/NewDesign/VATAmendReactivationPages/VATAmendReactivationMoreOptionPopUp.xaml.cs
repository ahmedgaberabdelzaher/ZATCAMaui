using System;
using System.Collections.Generic;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.VATAmendReactivationPages
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
            foreach(string option in OptionList)
            {
                Option OptionObj = new Option();
                OptionObj.option = option;
                list.Add(OptionObj);
            }

            optionList.ItemsSource = list;

        }

        private async void OptionItemTapped(object sender, ItemTappedEventArgs e)
        {
            try { 
            var selectedOption = e.Item as Option;
            await PopupNavigation.Instance.PopAsync();
            OnItemSelect?.Invoke(selectedOption.option);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        }

    
    public class Option
    {
        public string option { get; set; }
    }
}
