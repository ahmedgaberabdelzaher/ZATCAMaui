using EGAZT.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using EGAZT.Views.NewDesign.AccountStatements;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.Models.IBanManagementListModel;

namespace EGAZT.Views.NewDesign.IBanAccountsManagements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class BankAccountManagementPageView : ContentPage
    {
        private BankAccountManagementPageViewModel _viewModel;
        public BankAccountManagementPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();

            SetLTR();

            _viewModel = App.Locator.BankAccountManagementPageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            //Check for Large Tax payer or not
            await _viewModel.LoadAllIBanAccounts();

            MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
            {
                await PopupNavigation.Instance.PopAsync();
                string message = arg;
               
            });
         }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            try
            {
                if (App.IsArabic)
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
                }
                else
                {
                    Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
                }

            }
            catch (Exception)
            {

            }

        }

      


        private  async void IBanAccountsListItemTapped(object sender, ItemSelectionChangedEventArgs e)
        {
            var textToDisplayInButton = string.Empty;
            var selectedLv = sender as SfListView;
            IbanListSetResult selectedItem = (IbanListSetResult)selectedLv.SelectedItem;
            //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("" + selectedItem.Bkext));
            if(selectedItem.VisibleUpdate!=null)
            {
                if(selectedItem.VisibleUpdate=="")
                {
                   // Display ActionSheet Radio buttons 
                   if(selectedItem.ActiveIban=="X")
                    {
                        textToDisplayInButton = "DEACTIVATE";
                    }else if(selectedItem.ActiveIban=="")
                    {
                        textToDisplayInButton = "ACTIVATE";
                    }
                }
                else
                {
                    //IsEnabled Update or disble update button
                    if(selectedItem.EnableUpdate=="X")
                    {
                        textToDisplayInButton = "UpdatedEnabled";
                    }
                    else if(selectedItem.EnableUpdate == "")
                    {
                        textToDisplayInButton = "UpdateDisabled";
                    }
                }
                var listOfActionButtonsApplicable = new List<string>();
                listOfActionButtonsApplicable.Add(textToDisplayInButton);
                await PopupNavigation.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(listOfActionButtonsApplicable));

            }
            var view = sender as SfListView;
            view.SelectedItem = null;
        }
    }
}