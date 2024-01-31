using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.ZakatForm5
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatForm5PageView : ContentPage
    {
        ZakatForm5PageViewModel viewModel;

        public ZakatForm5PageView(string Fbguid)
        {


            InitializeComponent();
            viewModel = App.Locator.ZakatForm5PageView;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            viewModel.Fbguid = Fbguid;
            _ = viewModel.LoadZakatForm5Data();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;

            App.IsComingFromSleepMode = false;
       

            Task.Run(() =>
            {
               
                if (viewModel != null)
                {
                    
                    viewModel.NextText = AppResources.ZZNext;
                    viewModel.setCurrentTab();
                }
            });

        }
        private async Task LoadData()
        {
            try
            {



            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }
        public void IntialiseAsync()
        {
            
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
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
                    Cabsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Cabsbtn.TextColor = Colors.Green;
                    break;
                case 2:
                    ProfessionalBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    ProfessionalBtn.TextColor = Colors.Green;
                    break;
                case 3:
                    SellBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    SellBtn.TextColor = Colors.Green;
                    break;
                case 4:
                    LabourBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    LabourBtn.TextColor = Colors.Green;
                    break;
                case 5:
                    IndustryBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    IndustryBtn.TextColor = Colors.Green;
                    break;
                case 6:
                    ContractingBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    ContractingBtn.TextColor = Colors.Green;
                    break;
                case 7:
                    InvestBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    InvestBtn.TextColor = Colors.Green;
                    break;
                case 8:
                    Hotelsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Hotelsbtn.TextColor = Colors.Green;
                    break;
                case 9:
                    EducationBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    EducationBtn.TextColor = Colors.Green;
                    break;
                case 10:
                    Poultrybtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Poultrybtn.TextColor = Colors.Green;
                    break;
                case 11:
                    Carsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Carsbtn.TextColor = Colors.Green;
                    break;
                case 12:
                    Mineralsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Mineralsbtn.TextColor = Colors.Green;
                    break;
                case 13:
                    Additionalbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
                    Additionalbtn.TextColor = Colors.Green;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = Colors.Green;

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


            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = Colors.Green;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = Colors.Green;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = Colors.Green;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = Colors.Green;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color) Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color) Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = Colors.Green;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;

            Mineralsbtn.TextColor = (Color)Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Application.Current.Resources["Primary"]; ;
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


            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;


            Cabsbtn.TextColor = (Color)Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color) Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color) Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = Colors.Green;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = Colors.Green;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = Colors.Green;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;

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
            viewModel.isBuyVisible = false;
            viewModel.isProfessionalVisible = false;
            viewModel.isCabVisible = false;

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = Colors.Green;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = Colors.Green;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
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

            Cabsbtn.Background = Colors.White;
            ProfessionalBtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;

            Cabsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ProfessionalBtn.TextColor = Colors.Green;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;

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

            Cabsbtn.Background = (Color)Application.Current.Resources["BackgroundGray"];
            ProfessionalBtn.Background = Colors.White;
            SellBtn.Background = Colors.White;
            LabourBtn.Background = Colors.White;
            IndustryBtn.Background = Colors.White;
            ContractingBtn.Background = Colors.White;
            InvestBtn.Background = Colors.White;
            Hotelsbtn.Background = Colors.White;
            EducationBtn.Background = Colors.White;
            Poultrybtn.Background = Colors.White;
            Carsbtn.Background = Colors.White;
            Mineralsbtn.Background = Colors.White;
            Additionalbtn.Background = Colors.White;


            Cabsbtn.TextColor = Colors.Green;
            ProfessionalBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            SellBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            LabourBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            IndustryBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            ContractingBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            InvestBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Hotelsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            EducationBtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Poultrybtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Carsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Mineralsbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;
            Additionalbtn.TextColor = (Color)Microsoft.Maui.Controls.Application.Current.Resources["Primary"]; ;

        }
    }
}