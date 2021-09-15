using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM;
using EGAZT.Views.NewDesign.GenericPickers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using static EGAZT.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM.FilterVatEffectiveDatePageViewModel;

namespace EGAZT.Views.NewDesign.UpdateVatEffectiveDate
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterVatEffectiveDatePageView : ContentPage
    {
        FilterVatEffectiveDatePageViewModel viewModel;
        public FilterVatEffectiveDatePageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FilterVatEffectiveDatePageView;
            ChangeAeroIcon();
            SetLTR();
            this.BindingContext = viewModel;
            //viewModel.FilterList.Clear();

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {

                VatEffectDateFilterModel filter1 = new VatEffectDateFilterModel();
                VatEffectDateFilterModel filter2 = new VatEffectDateFilterModel();

                viewModel.PickerModel = arg;
                var selectedType = string.Empty;
                string SelectedIDTypeValue = string.Empty;
                if (arg.PickerId == "DateSortTypePicker")
                {
                    viewModel.SelectedDateSortText = arg.SelectedValue;
                    /*if (viewModel.FilterList.Count > 0)
                    {*/
                        var dateTypeMatch = viewModel.FilterList.RemoveAll(selectedValue => (selectedValue.filterId == 1));
                        filter1.filterId = 1;
                        filter1.filterType = "DateType";
                        filter1.filterName = arg.SelectedValue;
                        viewModel.FilterList.Add(filter1);
                        /* if (dateTypeMatch != null)
                        {
                            *//*filter1.filterId = 1;
                            filter1.filterType = "DateType";
                            filter1.filterName = arg.SelectedValue;
                            viewModel.FilterList.Add(filter1);*//*
                            for (int i = 0; i < viewModel.FilterList.Count; i++)
                            {
                                if (viewModel.FilterList[i].filterId == 1)
                                {
                                    viewModel.FilterList[i].filterId = 1;
                                    viewModel.FilterList[i].filterType = "DateType";
                                    viewModel.FilterList[i].filterName = arg.SelectedValue;
                                }
                                else
                                {
                                    filter1.filterId = 1;
                                    filter1.filterType = "DateType";
                                    filter1.filterName = arg.SelectedValue;
                                    viewModel.SelectedDateSortText = arg.SelectedValue;
                                    viewModel.FilterList.Add(filter1);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < viewModel.FilterList.Count; i++)
                            {
                                if (viewModel.FilterList[i].filterId == 1)
                                {
                                    viewModel.FilterList[i].filterId = 1;
                                    viewModel.FilterList[i].filterType = "DateType";
                                    viewModel.FilterList[i].filterName = arg.SelectedValue;
                                }
                                else
                                {
                                    filter1.filterId = 1;
                                    filter1.filterType = "DateType";
                                    filter1.filterName = arg.SelectedValue;
                                    viewModel.SelectedDateSortText = arg.SelectedValue;
                                    viewModel.FilterList.Add(filter1);
                                }
                            }

                        }*/
                    //}
                    /*else
                    {
                        filter1.filterId = 1;
                        filter1.filterType = "DateType";
                        filter1.filterName = arg.SelectedValue;
                        viewModel.SelectedDateSortText = arg.SelectedValue;
                        viewModel.FilterList.Add(filter1);
                    }*/
                }
                else if (arg.PickerId == "UpdatedBySortTypePicker")
                {
                    viewModel.SelectedUpdatedSortText = arg.SelectedValue;
                    /*if (viewModel.FilterList.Count > 0)
                    {*/
                        var updateByTypeMatch = viewModel.FilterList.RemoveAll(selectedValue => (selectedValue.filterId == 2));
                        filter2.filterId = 2;
                        filter2.filterType = "UpdatedByType";
                        filter2.filterName = arg.SelectedValue;
                        viewModel.FilterList.Add(filter2);
                        /* if (updateByTypeMatch != null)
                        {*/
                        /* filter2.filterId = 2;
                         filter2.filterType = "UpdatedByType";
                         filter2.filterName = arg.SelectedValue;
                         viewModel.SelectedUpdatedSortText = arg.SelectedValue;
                         viewModel.FilterList.Add(filter2);*/

                        /*for (int i = 0; i < viewModel.FilterList.Count; i++)
                            {
                                if (viewModel.FilterList[i].filterId == 2)
                                {
                                    //viewModel.FilterList = new List<VatEffectDateFilterModel>(viewModel.FilterList.Where(filteredObjects => filteredObjects.filterId == 2));

                                    viewModel.FilterList[i].filterId = 2;
                                    viewModel.FilterList[i].filterType = "UpdatedByType";
                                    viewModel.FilterList[i].filterName = arg.SelectedValue;
                                }
                            }*/

                        //}
                       /* else
                        {
                            for (int i = 0; i < viewModel.FilterList.Count; i++)
                            {
                                if (viewModel.FilterList[i].filterId == 2)
                                {
                                    //viewModel.FilterList = new List<VatEffectDateFilterModel>(viewModel.FilterList.Where(filteredObjects => filteredObjects.filterId == 2));

                                    viewModel.FilterList[i].filterId = 2;
                                    viewModel.FilterList[i].filterType = "UpdatedByType";
                                    viewModel.FilterList[i].filterName = arg.SelectedValue;
                                }
                            }
                        }*/
                        /* List<VatEffectDateFilterModel> filteredList;
                         filteredList = new List<VatEffectDateFilterModel>(viewModel.FilterList.Where(filteredObjects => filteredObjects.filterId == 2));
                         if (filteredList != null && filteredList.Count > 0)
                         {
                             filteredList[0].filterId = 2;
                             filteredList[0].filterType = "UpdatedByType";
                             filteredList[0].filterName = arg.SelectedValue;*/
                        /*for (int i = 0; i < viewModel.FilterList.Count; i++)
                        {
                            if (viewModel.FilterList[i].filterId == 2)
                            {
                                //viewModel.FilterList = new List<VatEffectDateFilterModel>(viewModel.FilterList.Where(filteredObjects => filteredObjects.filterId == 2));

                                viewModel.FilterList[i].filterId = 2;
                                viewModel.FilterList[i].filterType = "UpdatedByType";
                                viewModel.FilterList[i].filterName = arg.SelectedValue;
                            }
                        }*/
                        //filter2.filterId = 2;
                        //filter2.filterType = "UpdatedByType";
                        //filter2.filterName = arg.SelectedValue;
                     
                        // viewModel.FilterList.Add(filter2);
                        //}
                   // }
                  /*  else
                    {
                        filter2.filterId = 2;
                        filter2.filterType = "UpdatedByType";
                        filter2.filterName = arg.SelectedValue;
                        viewModel.SelectedUpdatedSortText = arg.SelectedValue;
                        viewModel.FilterList.Add(filter2);
                    }*/
                }
            });
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");

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

        void SortAscending_Tapped(System.Object sender, System.EventArgs e)
        {
        }

        void SortDescending_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}