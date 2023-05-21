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
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
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
                    Cabsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    Cabsbtn.TextColor = Color.Green;
                    break;
                case 2:
                    ProfessionalBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    ProfessionalBtn.TextColor = Color.Green;
                    break;
                case 3:
                    SellBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    SellBtn.TextColor = Color.Green;
                    break;
                case 4:
                    LabourBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    LabourBtn.TextColor = Color.Green;
                    break;
                case 5:
                    IndustryBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    IndustryBtn.TextColor = Color.Green;
                    break;
                case 6:
                    ContractingBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    ContractingBtn.TextColor = Color.Green;
                    break;
                case 7:
                    InvestBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    InvestBtn.TextColor = Color.Green;
                    break;
                case 8:
                    Hotelsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    Hotelsbtn.TextColor = Color.Green;
                    break;
                case 9:
                    EducationBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    EducationBtn.TextColor = Color.Green;
                    break;
                case 10:
                    Poultrybtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    Poultrybtn.TextColor = Color.Green;
                    break;
                case 11:
                    Carsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    Carsbtn.TextColor = Color.Green;
                    break;
                case 12:
                    Mineralsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
                    Mineralsbtn.TextColor = Color.Green;
                    break;
                case 13:
                    Additionalbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
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
            Additionalbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            Mineralsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = Color.Green;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            Carsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = Color.Green;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            Poultrybtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = Color.Green;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            EducationBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = Color.Green;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            Hotelsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = Color.Green;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            InvestBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;


            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = Color.Green;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            ContractingBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = Color.Green;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            IndustryBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = Color.Green;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;

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
            LabourBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
            IndustryBtn.BackgroundColor = Color.White;
            ContractingBtn.BackgroundColor = Color.White;
            InvestBtn.BackgroundColor = Color.White;
            Hotelsbtn.BackgroundColor = Color.White;
            EducationBtn.BackgroundColor = Color.White;
            Poultrybtn.BackgroundColor = Color.White;
            Carsbtn.BackgroundColor = Color.White;
            Mineralsbtn.BackgroundColor = Color.White;
            Additionalbtn.BackgroundColor = Color.White;

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = Color.Green;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            SellBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
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

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = Color.Green;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;
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
            ProfessionalBtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
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

            Cabsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ProfessionalBtn.TextColor = Color.Green;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;

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

            Cabsbtn.BackgroundColor =  (Color)Application.Current.Resources["BackgroundGray"];
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
            ProfessionalBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            SellBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            LabourBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            IndustryBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            ContractingBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            InvestBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Hotelsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            EducationBtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Poultrybtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Carsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Mineralsbtn.TextColor = (Color)App.Current.Resources["Primary"];;
            Additionalbtn.TextColor = (Color)App.Current.Resources["Primary"];;

        }
    }
}