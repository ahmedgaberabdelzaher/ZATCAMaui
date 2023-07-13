using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EDeclaration.QuestionsViews
{	
	public partial class ChooseQuestionsPage : ContentPage
	{
        BaseProductDeclarationViewModel viewModel;
        public ChooseQuestionsPage ()
		{
			InitializeComponent ();
            viewModel = App.Locator.ProductDeclarationViewModel;
            BindingContext = viewModel;
            viewModel.IsArrivingPlaneSelected = viewModel.SubmitModel.travelerDeclaration.travelingType == 2 ? false : true;
            viewModel.HeaderTitle = viewModel.IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
            viewModel.CardData = new ObservableCollection<EDeclerationCardModel>();
            viewModel.selectedQuestionList = new List<int>();
            viewModel.questionIndex = 0;
        }

        protected override void OnAppearing()
        {
            var userType = App.Locator.StateManager.GetItem("IsLoggedIn") ;
            var fullName = (string)App.Locator.StateManager.GetItem("FullName");
            
            if (userType != null )
            {
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = (bool)userType;
                viewModel.SubmitModel.travelerDeclaration.FullName = viewModel.SubmitModel.travelerDeclaration.Isvisitor ? null : fullName;

            }
                

            if (!viewModel.IsArrivingPlaneSelected)
            {
                viewModel.QuestionList = new ObservableCollection<QuestionModel>
                {
                    new QuestionModel{QuestionName = AppResources.EDeclerationCurrencyQuestion ,QuestionId =3}
                };
            }
            else
            {
                viewModel.QuestionList = new ObservableCollection<QuestionModel>
                 {
                     new QuestionModel{QuestionName = AppResources.EdeclerationTobbacoQuestion ,QuestionId =1},
                     new QuestionModel{QuestionName = AppResources.EDeclerationRestrictedQuestion ,QuestionId =2},
                     new QuestionModel{QuestionName = AppResources.EDeclerationCurrencyQuestion ,QuestionId =3},
                     new QuestionModel{QuestionName = AppResources.EdeclerationProductQuestion ,QuestionId =4}
                 };
            }
        }
    }
}

