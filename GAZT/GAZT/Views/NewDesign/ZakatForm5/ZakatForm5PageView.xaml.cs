using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatForm5PageView : ContentPage
    {
        ZakatForm5PageViewModel viewModel;

        public ZakatForm5PageView(string Fbguid)
        {


            InitializeComponent();
            viewModel = App.Locator.ZakatForm5PageView;
            this.BindingContext = viewModel;
            viewModel.Fbguid = Fbguid;
            //viewModel.z = true;
            //viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.IsComingFromSleepMode = false;
            if (viewModel != null)
            {
                viewModel.IsLoading = false;
                
            }

            Task.Run(async () =>
            {
                await LoadData();

                if (viewModel != null)
                    viewModel.IsLoading = false;
            });
        }
        private async Task LoadData()
        {
            try
            {
                App.DisplayProgressView();
                await viewModel.LoadZakatForm5Data();
                Device.BeginInvokeOnMainThread(() => {
                   
                    App.HideProgressView();
                    });

               
            }
            catch (Exception ex)
            {
                App.HideProgressView();
            }
        }
        public async Task IntialiseAsync()
        {
            try
            {
                await viewModel.LoadZakatForm5Data();
                if (viewModel.ZakatForm5DataResult != null )
                {
                   // BPicker.SelectedIndex = 14;
                }
            }
            catch (Exception e)
            {
            }
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        private void OnInfoTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new DummyPopUp());
        }

        private void Additional_Clicked(object sender, EventArgs e)
        {
            viewModel.isAdditionalVisible = true;
            Additionalbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];
 Cabsbtn.Style=SellBtn.Style=ProfessionalBtn.Style=LabourBtn.Style=IndustryBtn.Style=ContractingBtn.Style=Mineralsbtn.Style=InvestBtn.Style=Hotelsbtn.Style=EducationBtn.Style=Poultrybtn.Style=Carsbtn.Style= (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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

        }

        private void Minerals_Clicked(object sender, EventArgs e)
        {
            viewModel.isMineralVisible = true;
            Mineralsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Cars_Clicked(object sender, EventArgs e)
        {
            viewModel.isCarVisible = true;
            Carsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Poultry_Clicked(object sender, EventArgs e)
        {
            viewModel.isPoultryVisible = true;
            Poultrybtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = EducationBtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Education_Clicked(object sender, EventArgs e)
        {
            viewModel.isEducationVisible = true;
            EducationBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = Hotelsbtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Hotels_Clicked(object sender, EventArgs e)
        {
            viewModel.isHotelVisible = true;
            Hotelsbtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = InvestBtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Invest_Clicked(object sender, EventArgs e)
        {
            viewModel.isRealEstateVisible = true;
            InvestBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = ContractingBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Contracting_Clicked(object sender, EventArgs e)
        {
            viewModel.isContractingVisible = true;
            ContractingBtn.Style = (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = IndustryBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];


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
        }

        private void Industry_Clicked(object sender, EventArgs e)
        {
            viewModel.isIndustryVisible = true;
            IndustryBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = LabourBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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

        }

        private void Labour_Clicked(object sender, EventArgs e)
        {
            viewModel.isLabourOccupancyVisible = true;
            LabourBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = ProfessionalBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];


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
        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            viewModel.isBuyVisible = true;
            SellBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = LabourBtn.Style = ProfessionalBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Professional_Clicked(object sender, EventArgs e)
        {
            viewModel.isProfessionalVisible = true;
            ProfessionalBtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

            Cabsbtn.Style = SellBtn.Style = SellBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }

        private void Cabs_Clicked(object sender, EventArgs e)
        {
            viewModel.isCabVisible = true;
            Cabsbtn.Style= (Style)Application.Current.Resources["SelectedBtn"];

            ProfessionalBtn.Style = SellBtn.Style = SellBtn.Style = IndustryBtn.Style = ContractingBtn.Style = InvestBtn.Style = Additionalbtn.Style = Hotelsbtn.Style = EducationBtn.Style = Poultrybtn.Style = Mineralsbtn.Style = Carsbtn.Style = (Style)Application.Current.Resources["BackgroundWhiteBtn"];

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
        }
    }
}