
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
            BindingContext = viewModel;
            viewModel.Fbguid = Fbguid;

        }

        private void Additional_Clicked(object sender, EventArgs e)
        {
            viewModel.isAdditionalVisible = true;
            

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