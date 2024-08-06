using System.Collections.ObjectModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EscalatedCasesGSTCPageViewModel;
using static ZATCAMAUI.Models.EscalatedGstcModel;

namespace ZATCAMAUI.Views.NewDesign.EscalatedCasesGSTC;

public partial class EscalatedCasesGSTCPageView : ContentPage
{
    private EscalatedCasesGSTCPageViewModel viewModel;

    public EscalatedCasesGSTCPageView(ObservableCollection<CaseDetailsResultSet> CaseDetailedListViewData)
    {
        try
        {
            InitializeComponent();
            viewModel = App.Locator.EscalatedCasesGSTCPageView;
            this.BindingContext = viewModel;

            viewModel.CaseDetailedListViewData = CaseDetailedListViewData;
            viewModel.CopiedCaseDetailedListViewData = CaseDetailedListViewData;

            if (viewModel.CaseDetailedListViewData.Count > 0)
            {
                viewModel.IsListVisible = true;
            }
            else
            {
                viewModel.IsListVisible = false;
            }
        }
        catch (Exception)
        {

        }


    }

    private void searchButtonTapped(object sender, EventArgs e)
    {
        viewModel.IsSearchButtonVisible = false;
        viewModel.IsCloseButtonVisible = true;
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            searchBar = (sender as SearchBar);
        
            viewModel.SearchText = searchBar.Text;
            viewModel.FilterWithReferenceNumber();
        }
        catch (Exception)
        {

        }
    }

    private void CloseSearchButton_Tapped(object sender, EventArgs e)
    {
        viewModel.IsSearchButtonVisible = true;
        viewModel.IsCloseButtonVisible = false;
        viewModel.SearchText = "";

        viewModel.CopiedCaseDetailedListViewData = viewModel.CaseDetailedListViewData;
        if (searchBar != null)
        {
            searchBar.Text = "";
        }
    }
}
