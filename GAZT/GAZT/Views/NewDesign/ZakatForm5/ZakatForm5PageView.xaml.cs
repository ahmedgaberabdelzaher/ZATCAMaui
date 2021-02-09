using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
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
using Xamarin.Forms.Markup;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatForm5PageView : ContentPage
    {
        ZakatForm5PageViewModel viewModel;

        public ZakatForm5PageView(string Fbguid)
        {


            InitializeComponent();
            viewModel = App.Locator.ZakatForm5PageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.Fbguid = Fbguid;
            //viewModel.z = true;
            //viewModel.IsNoDataLabelVisible = false;
            _ = viewModel.LoadZakatForm5Data();

           // IntialiseAsync();
          //  ZakatEstimationList.IsVisible = viewModel.IsZakatEstListVisible;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            this.Padding = safeInsets;

            App.IsComingFromSleepMode = false;
            if (viewModel != null)
            {
            //    viewModel.IsLoading = false;
               
            }

            Task.Run( () =>
            {
               // await LoadData();

                if (viewModel != null)
                {
                   // viewModel.IsLoading = false;
                    viewModel.NextText = AppResources.ZZNext;
                    viewModel.setCurrentTab();
                }
            });

            //if (App.IsArabic)
            //{
            //    RefrenceNumberInEnglish.IsVisible = false;
            //    RefrenceNumberInArabic.IsVisible = true;
            //}
            //else
            //{
            //    RefrenceNumberInEnglish.IsVisible = true;
            //    RefrenceNumberInArabic.IsVisible = false;
            //}
        }
        private async Task LoadData()
        {
            try
            {
               
              
                //Device.BeginInvokeOnMainThread(() => {

                //    ////App.HideProgressView();
                //    //});
                //    await Task.Run(() =>
                //    {
                //        viewModel.IsLoading = false;
                //    });

                //}
              
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
                // App.HideProgressView();
            }
        }
        public void IntialiseAsync()
        {
            //try
            //{
            //    await viewModel.LoadZakatForm5Data();
            //    if (viewModel.ZakatForm5DataResult != null )
            //    {
            //       // BPicker.SelectedIndex = 14;
            //    }
            //}
            //catch (Exception e)
            //{
            //}


         //  LoadZakatForm5Data();
        }

        private void SetLTR()
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
        public void ChangeAeroIcon()
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

        private void OnInfoTapped(object sender, EventArgs e)
        {
           // PopupNavigation.Instance.PushAsync(new DummyPopUp());
        }


        public void SetButtonBackGroundColor(int BTNno)
        {
            switch (BTNno)
            {
                case 1:
                    Cabsbtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Cabsbtn.TextColor = Color.Green;
                    break;
                case 2:
                    ProfessionalBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    ProfessionalBtn.TextColor = Color.Green;
                    break;
                case 3:
                    SellBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    SellBtn.TextColor = Color.Green;
                    break;
                case 4:
                    LabourBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    LabourBtn.TextColor = Color.Green;
                    break;
                case 5:
                    IndustryBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    IndustryBtn.TextColor = Color.Green;
                    break;
                case 6:
                    ContractingBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    ContractingBtn.TextColor = Color.Green;
                    break;
                case 7:
                    InvestBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    InvestBtn.TextColor = Color.Green;
                    break;
                case 8:
                    Hotelsbtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Hotelsbtn.TextColor = Color.Green;
                    break;
                case 9:
                    EducationBtn.BackgroundColor = Color.FromHex("#E5EFED");
                    EducationBtn.TextColor = Color.Green;
                    break;
                case 10:
                    Poultrybtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Poultrybtn.TextColor = Color.Green;
                    break;
                case 11:
                    Carsbtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Carsbtn.TextColor = Color.Green;
                    break;
                case 12:
                    Mineralsbtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Mineralsbtn.TextColor = Color.Green;
                    break;
                case 13:
                    Additionalbtn.BackgroundColor = Color.FromHex("#E5EFED");
                    Additionalbtn.TextColor = Color.Green;
                    break;
            }
        }

        private void Additional_Clicked(object sender, EventArgs e)
        {
            viewModel.isAdditionalVisible = true;
 //           Additionalbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];
 //Cabsbtn.Style=SellBtn.Style=ProfessionalBtn.Style=LabourBtn.Style=IndustryBtn.Style=ContractingBtn.Style=Mineralsbtn.Style=InvestBtn.Style=Hotelsbtn.Style=EducationBtn.Style=Poultrybtn.Style=Carsbtn.Style= (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isMineralVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.FromHex("#E5EFED");

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Green;

        }

        private void Minerals_Clicked(object sender, EventArgs e)
        {
            viewModel.isMineralVisible = true;
           // Mineralsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

          // Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isAdditionalVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;


            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.FromHex("#E5EFED");
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Green;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Cars_Clicked(object sender, EventArgs e)
        {
            viewModel.isCarVisible = true;
          //  Carsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

         //   Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.FromHex("#E5EFED");
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Green;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Poultry_Clicked(object sender, EventArgs e)
        {
            viewModel.isPoultryVisible = true;
           // Poultrybtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

          //  Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.FromHex("#E5EFED");
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Green;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Education_Clicked(object sender, EventArgs e)
        {
            viewModel.isEducationVisible = true;
          //  EducationBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

         //   Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.FromHex("#E5EFED");
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Green;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Hotels_Clicked(object sender, EventArgs e)
        {
            viewModel.isHotelVisible = true;
           // Hotelsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

          //  Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.FromHex("#E5EFED");
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Green;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Invest_Clicked(object sender, EventArgs e)
        {
            viewModel.isRealEstateVisible = true;
          //  InvestBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

           // Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;


            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.FromHex("#E5EFED");
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;


            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Green;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Contracting_Clicked(object sender, EventArgs e)
        {
            viewModel.isContractingVisible = true;
          //  ContractingBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

          //  Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];


            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.FromHex("#E5EFED");
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Green;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Industry_Clicked(object sender, EventArgs e)
        {
            viewModel.isIndustryVisible = true;
          //  IndustryBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

           // Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isContractingVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.FromHex("#E5EFED");
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Green;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;

        }

        private void Labour_Clicked(object sender, EventArgs e)
        {
            viewModel.isLabourOccupancyVisible = true;
           // LabourBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

//Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];


            viewModel.isIndustryVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isBuyVisible = false ;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.FromHex("#E5EFED");
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Green;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            viewModel.isBuyVisible = true;
          //  SellBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

          //  Cabsbtn.Style = LabourBtn.Style = ProfessionalBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isLabourOccupancyVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.FromHex("#E5EFED");
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Green;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;
        }

        private void Professional_Clicked(object sender, EventArgs e)
        {
            viewModel.isProfessionalVisible = true;
           // ProfessionalBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

          //  Cabsbtn.Style = SellBtn.Style = SellBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isBuyVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.BackgroundColor = Color.White;
            ProfessionalBtn.BackgroundColor = Color.FromHex("#E5EFED");
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = Color.Black;
            ProfessionalBtn.TextColor = Color.Green;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;

        }

        private void Cabs_Clicked(object sender, EventArgs e)
        {
            viewModel.isCabVisible = true;
//Cabsbtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

      //      ProfessionalBtn.Style = SellBtn.Style = SellBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

            viewModel.isProfessionalVisible = false;
            viewModel.isBuyVisible = false;
            viewModel.isLabourOccupancyVisible = false;
            viewModel.isIndustryVisible = false;
            viewModel.isContractingVisible = false;
            viewModel.isRealEstateVisible = false;
            viewModel.isHotelVisible = false;
            viewModel.isEducationVisible = false;
            viewModel.isPoultryVisible = false;
            viewModel.isCarVisible = false;
            viewModel.isMineralVisible = false;
            viewModel.isAdditionalVisible = false;

            Cabsbtn.BackgroundColor = Color.FromHex("#E5EFED");
            ProfessionalBtn.BackgroundColor = Color.White;
            SellBtn.BackgroundColor = Color.White;
            LabourBtn.BackgroundColor = Color.White;
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
           Additionalbtn.BackgroundColor = Color.White;


            Cabsbtn.TextColor = Color.Green;
            ProfessionalBtn.TextColor = Color.Black;
            SellBtn.TextColor = Color.Black;
            LabourBtn.TextColor = Color.Black;
            IndustryBtn.TextColor = Color.Black;
            ContractingBtn.TextColor = Color.Black;
            InvestBtn.TextColor = Color.Black;
            Hotelsbtn.TextColor = Color.Black;
            EducationBtn.TextColor = Color.Black;
            Poultrybtn.TextColor = Color.Black;
            Carsbtn.TextColor = Color.Black;
            Mineralsbtn.TextColor = Color.Black;
            Additionalbtn.TextColor = Color.Black;

        }
    }
}