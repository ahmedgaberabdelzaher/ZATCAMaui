
using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Core;
using System.Collections.ObjectModel;
using System.Text;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;

namespace ZATCAMAUI.Views.NewDesign.ZakatDeregistration
{

    public partial class TINDeregistrationCloseIndividualOutletsPageView : PopupPage
    {
        TINDeregistrationPageViewModel viewModel;

        public TINDeregistrationCloseIndividualOutletsPageView(string reasonDesc, TINDeregistrationPageViewModel tINDeregistrationPageViewModel)
        {
            InitializeComponent();
            try
            {
                tINDeregistrationPageViewModel.IsMultiplePermitsVisible = false;
                viewModel = tINDeregistrationPageViewModel;
                this.BindingContext = viewModel;
                viewModel.PopulateUI();

            }
            catch (Exception)
            {
                return;
            }
        }

        private void CloseDeregDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsHijriCal)
            {

                string month = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[1].ToString();
                string day = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[0].ToString();
                string year = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.SingleDeregistrationDate = date;


            }
            else
            {
                var selectedItem = CloseDeregDatePicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = year + "/" + month + "/" + day;

                viewModel.SingleDeregistrationDate = date;


            }

        }

        private void ClosePermitDeregDatePicker_Unfocused(object sender, FocusEventArgs e)
        {


            if (viewModel.IsHijriCal)
            {

                string month = (ClosePermitDOBPickerHijri.SelectedItem as IList<object>)[1].ToString();
                string day = (ClosePermitDOBPickerHijri.SelectedItem as IList<object>)[0].ToString();
                string year = (ClosePermitDOBPickerHijri.SelectedItem as IList<object>)[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.SingleOutletDeregistrationDate = Convert.ToDateTime(date);


            }
            else
            {
                var selectedItem = ClosePermitDOBPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = year + "/" + month + "/" + day;

                viewModel.SingleOutletDeregistrationDate = Convert.ToDateTime(date);


            }
        }
        private void ClosePermitDOBDatePicker_Unfocused(object sender, FocusEventArgs e)
        {


            if (viewModel.IsDOBHijriCal)
            {

                string month = (ClosePermitDeregDatePickerHijri.SelectedItem as IList<object>)[1].ToString();
                string day = (ClosePermitDeregDatePickerHijri.SelectedItem as IList<object>)[0].ToString();
                string year = (ClosePermitDeregDatePickerHijri.SelectedItem as IList<object>)[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.PermitDob = Convert.ToDateTime(date);


            }
            else
            {
                var selectedItem = ClosePermitDeregDatePicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                string date = year + "/" + month + "/" + day;

                viewModel.PermitDob = Convert.ToDateTime(date);


            }
        }
        public void OnDateEntryFocussed(object sender, EventArgs args)
        {
            if (viewModel.IsHijriCal)
            {
                CloseDeregDatePickerHijri.IsOpen = true;
            }
            else
            {
                CloseDeregDatePicker.IsOpen = true;
            }

        }
        private void OnDOBClicked(object sender, EventArgs e)
        {

            if (!viewModel.IsHijriCal)
            {
                CloseDeregDatePicker.IsOpen = true;
            }
            else
            {
                CloseDeregDatePickerHijri.IsOpen = true;
            }
        }

        private void HijriCalSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (CloseDeregDatePickerHijri.SelectedItem != null && (CloseDeregDatePickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[1].ToString();
                        string day = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[0].ToString();
                        string year = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[2].ToString();
                        viewModel.SingleDeregistrationDate = viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerCloseAllDeregDateDisplay = viewModel.PkrDBO; //DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");



                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerCloseAllDeregDateDisplay = string.Empty;

                    }

                }
                else
                {
                    if (CloseDeregDatePicker.SelectedItem != null && (CloseDeregDatePicker.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = (CloseDeregDatePicker.SelectedItem as IList<object>)[1].ToString();
                        string day = (CloseDeregDatePicker.SelectedItem as IList<object>)[0].ToString();
                        string year = (CloseDeregDatePicker.SelectedItem as IList<object>)[2].ToString();
                        viewModel.SingleDeregistrationDate = viewModel.PkrDBO = year + "/" + month + "/" + day;
                        //viewModel.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerCloseAllDeregDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");
                    }
                    else
                    {
                        viewModel.PkrDBO = string.Empty;
                        viewModel.PickerCloseAllDeregDateDisplay = string.Empty;

                    }
                }


            }
            catch (Exception)
            {


            }
        }

        private void DeregDateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel != null && !string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                FrmDBO.HasError = false;
            }

        }
        private void CloseDeregDatePicker_Closed(object sender, EventArgs e)
        {
            // bool isHIjri;

            try
            {
                if (viewModel.IsHijriCal)
                {
                    if (CloseDeregDatePickerHijri.SelectedItem != null && (CloseDeregDatePickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[1].ToString();
                        string day = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[0].ToString();
                        string year = (CloseDeregDatePickerHijri.SelectedItem as IList<object>)[2].ToString();
                        var date = year + "/" + month + "/" + day;


                        viewModel.SingleDeregistrationDate = viewModel.PkrDBO = date;
                        // viewModel.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                        viewModel.PickerCloseAllDeregDateDisplay = date;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    // isHIjri = true;
                }
                else
                {
                    if (CloseDeregDatePicker.SelectedItem != null)
                    {
                        string month = (CloseDeregDatePicker.SelectedItem as IList<object>)[1].ToString();
                        string day = (CloseDeregDatePicker.SelectedItem as IList<object>)[0].ToString();
                        string year = (CloseDeregDatePicker.SelectedItem as IList<object>)[2].ToString();
                        var date = year + "/" + month + "/" + day;


                        viewModel.SingleDeregistrationDate = viewModel.PkrDBO = date;
                        //  viewModel.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);

                        viewModel.PickerCloseAllDeregDateDisplay = viewModel.PkrDBO; //DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                    }
                    // isHIjri = false;
                }


            }
            catch (Exception)
            {


            }

        }



        private void HijriCal2Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel != null)
                {
                    if (viewModel.IsHijriCal)
                    {
                        if (TransferDeregDatePickerHijri.SelectedItem != null && (TransferDeregDatePickerHijri.SelectedItem as IList<object>).Count == 3)
                        {
                            string month = (TransferDeregDatePickerHijri.SelectedItem as IList<object>)[1].ToString();
                            string day = (TransferDeregDatePickerHijri.SelectedItem as IList<object>)[0].ToString();
                            string year = (TransferDeregDatePickerHijri.SelectedItem as IList<object>)[2].ToString();
                            viewModel.SingleDeregistrationDate = viewModel.PkrDBO = year + "/" + month + "/" + day;
                            // viewModel.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                            viewModel.PickerCloseAllDeregDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                        }
                        else
                        {
                            viewModel.PkrDBO = string.Empty;
                            viewModel.PickerCloseAllDeregDateDisplay = string.Empty;

                        }

                    }
                    else
                    {
                        if (TransferDeregDatePicker.SelectedItem != null && (TransferDeregDatePicker.SelectedItem as IList<object>).Count == 3)
                        {
                            string month = (TransferDeregDatePicker.SelectedItem as IList<object>)[1].ToString();
                            string day = (TransferDeregDatePicker.SelectedItem as IList<object>)[0].ToString();
                            string year = (TransferDeregDatePicker.SelectedItem as IList<object>)[2].ToString();
                            viewModel.SingleDeregistrationDate = viewModel.PkrDBO = year + "/" + month + "/" + day;
                            // viewModel.SingleDeregistrationDate = Convert.ToDateTime(viewModel.PkrDBO);
                            viewModel.PickerCloseAllDeregDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                        }
                        else
                        {
                            viewModel.PkrDBO = string.Empty;
                            viewModel.PickerCloseAllDeregDateDisplay = string.Empty;

                        }
                    }

                }


            }
            catch (Exception)
            {


            }
            finally
            {
                if (viewModel != null)
                {
                    MainThread.BeginInvokeOnMainThread(() => HijriCalSwitch3.IsToggled = viewModel.IsHijriCal);
                }
            }
        }
        private void HijriCal3Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (viewModel != null)
                {
                    if (viewModel.IsDOBHijriCal)
                    {
                        if (TransferDOBPickerHijri.SelectedItem != null && (TransferDOBPickerHijri.SelectedItem as IList<object>).Count == 3)
                        {
                            string month = (TransferDOBPickerHijri.SelectedItem as IList<object>)[1].ToString();
                            string day = (TransferDOBPickerHijri.SelectedItem as IList<object>)[0].ToString();
                            string year = (TransferDOBPickerHijri.SelectedItem as IList<object>)[2].ToString();
                            viewModel.SelectedDob = viewModel.PkrDBO = year + "/" + month + "/" + day;
                            //viewModel.SelectedDob = day + "/" + month + "/" + year;
                            viewModel.TransferPickerDOBDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");


                        }
                        else
                        {
                            viewModel.PkrDBO = string.Empty;
                            viewModel.TransferPickerDOBDateDisplay = string.Empty;

                        }

                    }
                    else
                    {
                        if (TransferDOBPicker.SelectedItem != null && (TransferDOBPicker.SelectedItem as IList<object>).Count == 3)
                        {
                            string month = (TransferDOBPicker.SelectedItem as IList<object>)[1].ToString();
                            string day = (TransferDOBPicker.SelectedItem as IList<object>)[0].ToString();
                            string year = (TransferDOBPicker.SelectedItem as IList<object>)[2].ToString();
                            viewModel.SelectedDob = viewModel.PkrDBO = year + "/" + month + "/" + day;
                            //viewModel.SelectedDob = day + "/" + month + "/" + year;
                            viewModel.TransferPickerDOBDateDisplay = viewModel.PkrDBO;//DateTime.Parse(viewModel.PkrDBO).Date.ToString("dd MMM yyyy");

                        }
                        else
                        {
                            viewModel.PkrDBO = string.Empty;
                            viewModel.PickerDOBDateDisplay = string.Empty;

                        }
                    }

                }

            }
            catch (Exception)
            {


            }
            finally
            {
                if (viewModel != null)
                {
                    MainThread.BeginInvokeOnMainThread(() => HijriCalSwitch1.IsToggled = viewModel.IsDOBHijriCal);
                }
            }
        }

        private void ClosePermitDeregDatePicker_Closed(object sender, EventArgs e)
        {
            try
            {

                var permitType = viewModel.SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitNoTb == viewModel.selectedCalPermitNo);
                if (permitType == null) return;
                var isHijiri = permitType.IsHijiri;
                object tempCal = null;
                if (isHijiri)
                {
                    viewModel.IsPermitHijriCal = true;

                    if (ClosePermitDeregDatePickerHijri.SelectedItem != null && (ClosePermitDeregDatePickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = ClosePermitDeregDatePickerHijri.SelectedItem;
                    }
                }
                else
                {
                    viewModel.IsPermitHijriCal = false;

                    if (CloseDeregDatePicker.SelectedItem != null && (CloseDeregDatePicker.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = CloseDeregDatePicker.SelectedItem;
                    }
                }
                if (tempCal == null) return;
                string month = (tempCal as IList<object>)[1].ToString();
                string day = (tempCal as IList<object>)[0].ToString();
                string year = (tempCal as IList<object>)[2].ToString();
                permitType.APermitDeregDisplayDate = year + "/" + month + "/" + day;
                if (isHijiri)
                {

                    string date = UtilityManager.HijriToGreg(permitType.APermitDeregDisplayDate);
                    permitType.APermitEffDtTb = viewModel.ConvertDateFormat(date);
                }
                else
                {
                    permitType.APermitEffDtTb = viewModel.ConvertDateFormat(permitType.APermitDeregDisplayDate);

                }
                viewModel.SelectedOutletForCloseTranser.PermitTypes = viewModel.SelectedOutletForCloseTranser.PermitTypes.Select(x =>
                {
                    if (x.APermitNoTb == viewModel.selectedCalPermitNo)
                    {
                        x.APermitDeregDisplayDate = permitType.APermitDeregDisplayDate;
                        x.APermitEffDtTb = permitType.APermitEffDtTb;
                    }
                    return x;
                }
               ).ToList();
            }
            catch (Exception)
            {


            }

        }
        private void HijriCal4Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                var permitType = viewModel.SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitNoTb == viewModel.selectedCalPermitNo);
                if (permitType == null) return;
                var isHijiri = permitType.IsHijiri;

                object tempCal = null;
                if (isHijiri)
                {
                    if (ClosePermitDeregDatePickerHijri.SelectedItem != null && (ClosePermitDeregDatePickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = ClosePermitDeregDatePickerHijri.SelectedItem;
                    }
                }
                else
                {
                    if (CloseDeregDatePicker.SelectedItem != null && (CloseDeregDatePicker.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = CloseDeregDatePicker.SelectedItem;
                    }
                }
                if (tempCal == null) return;
                string month = (tempCal as IList<object>)[1].ToString();
                string day = (tempCal as IList<object>)[0].ToString();
                string year = (tempCal as IList<object>)[2].ToString();
                permitType.APermitDeregDisplayDate = year + "/" + month + "/" + day;

                if (isHijiri)
                {

                    string date = UtilityManager.HijriToGreg(permitType.APermitDeregDisplayDate);
                    permitType.APermitEffDtTb = viewModel.ConvertDateFormat(date);
                }
                else
                {
                    permitType.APermitEffDtTb = viewModel.ConvertDateFormat(permitType.APermitDeregDisplayDate);

                }
                viewModel.SelectedOutletForCloseTranser.PermitTypes = viewModel.SelectedOutletForCloseTranser.PermitTypes.Select(x =>
                {
                    if (x.APermitNoTb == viewModel.selectedCalPermitNo)
                    {
                        x.APermitDeregDisplayDate = permitType.APermitDeregDisplayDate;
                        x.APermitEffDtTb = permitType.APermitEffDtTb;
                    }
                    return x;
                }
               ).ToList();

            }
            catch (Exception)
            {


            }
        }


        private void ClosePermitDOBPicker_Closed(object sender, EventArgs e)
        {
            try
            {

                var permitType = viewModel.SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitNoTb == viewModel.selectedCalPermitNo);
                if (permitType == null) return;
                var isDOBHijiri = permitType.IsDOBHijiri;

                object tempCal = null;
                if (isDOBHijiri)
                {
                    if (ClosePermitDOBPickerHijri.SelectedItem != null && (ClosePermitDOBPickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = ClosePermitDOBPickerHijri.SelectedItem;
                    }
                }
                else
                {
                    if (ClosePermitDOBPicker.SelectedItem != null && (ClosePermitDOBPicker.SelectedItem as IList<object>).Count == 3)
                    {
                        tempCal = ClosePermitDOBPicker.SelectedItem;
                    }
                }
                if (tempCal == null) return;
                string month = (tempCal as IList<object>)[1].ToString();
                string day = (tempCal as IList<object>)[0].ToString();
                string year = (tempCal as IList<object>)[2].ToString();
                permitType.APermitDeregDisplayDobDate = year + "/" + month + "/" + day;
                if (isDOBHijiri)
                {

                    string date = UtilityManager.HijriToGreg(permitType.APermitDeregDisplayDobDate);
                    permitType.APermitDobTb = viewModel.ConvertDateFormat(date);
                }
                else
                {
                    permitType.APermitDobTb = viewModel.ConvertDateFormat(permitType.APermitDeregDisplayDobDate);

                }
                //permitType.APermitDobTb = viewModel.ConvertDateFormat(permitType.APermitDeregDisplayDobDate);
                viewModel.SelectedOutletForCloseTranser.PermitTypes = viewModel.SelectedOutletForCloseTranser.PermitTypes.Select(x =>
                {
                    if (x.APermitNoTb == viewModel.selectedCalPermitNo)
                    {
                        x.APermitDeregDisplayDobDate = permitType.APermitDeregDisplayDobDate;
                        x.APermitDobTb = permitType.APermitEffDtTb;
                    }
                    return x;
                }
               ).ToList();
            }
            catch (Exception)
            {


            }

        }

        private void TransferDOBPicker_Closed(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.IsDOBHijriCal)
                {
                    if (TransferDOBPickerHijri.SelectedItem != null && (TransferDOBPickerHijri.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = (TransferDOBPickerHijri.SelectedItem as IList<object>)[1].ToString();
                        string day = (TransferDOBPickerHijri.SelectedItem as IList<object>)[0].ToString();
                        string year = (TransferDOBPickerHijri.SelectedItem as IList<object>)[2].ToString();
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && viewModel.SingleDeregistrationDate != null && DateTime.Parse(date) > DateTime.Parse(viewModel.SingleDeregistrationDate))
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            return;
                        }
                        viewModel.PkrDBO = date;
                        viewModel.SelectedDob = date;
                        viewModel.TransferPickerDOBDateDisplay = viewModel.PkrDBO;

                        viewModel.ValidateIDNumber();

                    }
                }
                else
                {
                    if (TransferDOBPicker.SelectedItem != null && (TransferDOBPicker.SelectedItem as IList<object>).Count == 3)
                    {
                        string month = (TransferDOBPicker.SelectedItem as IList<object>)[1].ToString();
                        string day = (TransferDOBPicker.SelectedItem as IList<object>)[0].ToString();
                        string year = (TransferDOBPicker.SelectedItem as IList<object>)[2].ToString();
                        var date = year + "/" + month + "/" + day;
                        if (!string.IsNullOrEmpty(date) && viewModel.SingleDeregistrationDate != null && DateTime.Parse(date) > DateTime.Parse(viewModel.SingleDeregistrationDate))
                        {
                            viewModel._dialogService.ShowMessage(AppResources.TINDeregDateDOBValidation, AppResources.Information);
                            return;
                        }
                        viewModel.PkrDBO = date;
                        viewModel.SelectedDob = date;
                        viewModel.TransferPickerDOBDateDisplay = viewModel.PkrDBO;
                        viewModel.ValidateIDNumber();

                    }
                }


            }
            catch (Exception)
            {


            }

        }


        private void TransferDOBDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            //ValidateIDNumber();

            if (viewModel.IsDOBHijriCal)
            {
                string month = (TransferDOBPickerHijri.SelectedItem as IList<object>)[1].ToString();
                string day = (TransferDOBPickerHijri.SelectedItem as IList<object>)[0].ToString();
                string year = (TransferDOBPickerHijri.SelectedItem as IList<object>)[2].ToString();
                string date = UtilityManager.HijriToGreg(year + "/" + month + "/" + day);
                viewModel.SelectedDob = date;
            }
            else
            {
                string month = (TransferDOBPicker.SelectedItem as IList<object>)[1].ToString();
                string day = (TransferDOBPicker.SelectedItem as IList<object>)[0].ToString();
                string year = (TransferDOBPicker.SelectedItem as IList<object>)[2].ToString();
                string date = year + "/" + month + "/" + day;
                viewModel.SelectedDob = date;
            }
        }

        private void TransferDOBDateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.PkrDBO))
            {
                FrmDBO.HasError = false;
            }

        }
        public void OnTransferDOBDateEntryFocussed(object sender, EventArgs args)
        {
            if (viewModel.IsDOBHijriCal)
            {
                TransferDOBPickerHijri.IsOpen = true;
            }
            else
            {
                TransferDOBPicker.IsOpen = true;
            }

        }
        private void OnTransferIDDOBClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idNumber.Text))
            {
                idNumber.Focus();
                return;
            }
            if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID && viewModel.DobText.IsEditable == false)
            {
                return;
            }
            if (!viewModel.IsDOBHijriCal)
            {
                TransferDOBPicker.IsOpen = true;
            }
            else
            {
                TransferDOBPickerHijri.IsOpen = true;
            }
        }
        void GetSelectedDataTemplate()
        {
            if (viewModel.SelectedPermitOutletOptionIndex == 2)
            {
                viewModel.outletEditIsVisible = true;
                //  viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseorTransferAllOutlets;
                foreach (var items in viewModel.SelectedOutletForCloseTranser.PermitTypes)
                {
                    items.APermitDregRsnTb = "1";
                    items.APermitDisplayReason = AppResources.TinDeregistrationClosed;
                    items.ReasonDescription = AppResources.TinDeregistrationClosed;
                    items.APermitDeregDisplayDate = string.Empty;
                    items.APermitIdNoTb = string.Empty;
                    items.APermitDeregDisplayDobDate = string.Empty;
                }
            }
            else if (viewModel.SelectedPermitOutletOptionIndex == 1)
            {
                viewModel.outletEditIsVisible = false;
                //viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxTransferAllOutlets;
                viewModel.SingleDeregistrationDate = string.Empty;
                viewModel.PickerCloseAllDeregDateDisplay = string.Empty;
                viewModel.SelectedIdNumber = string.Empty;
                viewModel.SelectedIdtype = string.Empty;
                viewModel.SelectedDob = string.Empty;
                viewModel.TINNumber = string.Empty;
                viewModel.IDTypeDataModel = new VATSignUpD();
            }
            else
            {
                viewModel.outletEditIsVisible = false;
                // viewModel.OutletCheckboxTitle = AppResources.TinDeregistrationOutletCheckboxCloseAllOutlets;
                viewModel.SingleDeregistrationDate = string.Empty;
                viewModel.PickerCloseAllDeregDateDisplay = string.Empty;
            }

            int index = Convert.ToInt16(viewModel.SelectedPermitOutletOptionIndex);
            viewModel.IsPermitOption1Visible = index == 0 ? true : false;
            viewModel.IsPermitOption2Visible = index == 1 ? true : false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetDate();

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {
                viewModel.PickerModel = arg;
            });


            MessagingCenter.Subscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption", (arg) =>
            {
                viewModel.GetSelectedDataTemplate();
            });
        }
        private void SetDate()
        {
            viewModel.SetDefaultDate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel = null;

            MessagingCenter.Unsubscribe<TINDeregistrationPageViewModel>(this, "SelectedOutletDecisionOption");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            GC.Collect(1);
        }
        private void IDNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel?.SelectedIdNumber))
            {
                viewModel.FrameIDError = false;
            }
        }

        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            try
            {
                TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
                viewModel.SelectedPermitOutletOptionIndex = viewModel.PermitOutletDecisionOptions.IndexOf(selectedItem);

                if (viewModel.SelectedPermitOutletOptionIndex == 2)
                {
                    if (viewModel.SelectedOutletForCloseTranser != null)
                    {
                        if (viewModel.SelectedOutletForCloseTranser.PermitTypes != null)
                        {
                            if (viewModel.SelectedOutletForCloseTranser.PermitTypes.Count > 0)
                            {
                                viewModel.IsNodataAvailableVisible = false;

                                viewModel.IsMultiplePermitsVisible = true;
                            }
                            else
                            {
                                viewModel.IsNodataAvailableVisible = true;

                            }
                        }
                        else
                        {
                            viewModel.IsNodataAvailableVisible = true;
                        }
                    }
                }
                else
                {
                    viewModel.IsNodataAvailableVisible = false;
                    viewModel.IsMultiplePermitsVisible = false;
                    if (viewModel.SelectedPermitOutletOptionIndex == 1)
                    {
                        viewModel.FirstNameLbl = AppResources.ZZZVATRFirstName;
                        viewModel.SurnameNameLbl = AppResources.TinDeregistrationSurName;
                    }
                }
                viewModel.GetSelectedDataTemplate();
            }
            catch (Exception)
            {


                // throw;
            }
        }

        private void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (viewModel != null && !string.IsNullOrEmpty(viewModel.SelectedIdtype))
                {
                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (viewModel.SelectedIdNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }
                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.SelectedDob))
                                {
                                    viewModel.ValidateIDNumberForIndiviualOutlets();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));


                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            if (viewModel.SelectedIdNumber.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }
                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel.SelectedIdNumber = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.SelectedDob))
                                {
                                    viewModel.ValidateIDNumberForIndiviualOutlets();
                                }
                            }
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationGCCID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else if (!(viewModel.SelectedIdNumber.Length <= 15 && viewModel.SelectedIdNumber.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                            //EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumberForIndiviualOutlets();
                        }
                    }

                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationCompanyID)
                    {
                        if (viewModel.SelectedIdNumber.Substring(0, 1) != "7")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.TinDeregistrationCompanyIDCheck;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else if (viewModel.SelectedIdNumber.Length > 10)
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.CompanyIDlengthis10digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.SelectedIdNumber = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumberForIndiviualOutlets();
                        }
                    }
                }
                else
                {
                    viewModel.FrameIDError = true;
                }
            }
            catch (Exception)
            {


            }
        }

        async void BorderlessEntryPermittype_Unfocused(object sender, FocusEventArgs e)
        {

            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();

            var cell = outletsListView.Children.FirstOrDefault();
            var entry = sender as GAZTBorderlessEntry;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                if (entry.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    entry.Focus();
                }
                if (entry.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
                }
                if (Messages.Length > 0)
                {
                    popUp.Message = Messages.ToString();
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    entry.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameTinError = false;
                    await viewModel.ValidateIdNumberForPermitTypes(entry.Text);
                }
            }
            else
            {
                viewModel.FrameTinError = true;
                Messages.Append(AppResources.AccountUnlockedCompleteRequiedFields);

                popUp.Message = Messages.ToString();
                popUp.IsLinkAvailable = false;

                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }

                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                entry.Text = string.Empty;
            }
        }

        async void BorderlessTINEntry_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTIN.Focus();
                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
                }
                if (Messages.Length > 0)
                {
                    popUp.Message = Messages.ToString();
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }

                    await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    viewModel.FrameTinError = false;
                    await viewModel.ValidateIdNumberFromApi(EntryTIN.Text);
                }
            }
            else
            {
                viewModel.FrameTinError = true;
                Messages.Append(AppResources.AccountUnlockedCompleteRequiedFields);

                popUp.Message = Messages.ToString();
                popUp.IsLinkAvailable = false;

                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }

                await MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                EntryTIN.Text = string.Empty;
            }
        }

        async void TapGestureRecognizerSingleDeregDate_Tapped(object sender, EventArgs e)
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregOutletSingleDatePicker";
                datepickermessagecenter();
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));


            }
            catch (InternetException)
            {
            }
        }

        private void datepickermessagecenter()
        {
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (senderDate, arg) =>
            {
                if (arg.PickerId == "DeregDatePicker")
                {
                    viewModel.DeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "DeregPermitOutletDatePicker")
                {
                    viewModel.SingleOutletDeregistrationDate = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "PermitTypeDobPickerDateTypePicker")
                {
                    viewModel.PermitDob = Convert.ToDateTime(arg.SelectedValue);
                }
                if (arg.PickerId == "_DOBDateTypePicker")
                {
                    viewModel.SelectedDob = arg.SelectedValue;
                    viewModel.PkrDBO = arg.SelectedValue;
                    if (viewModel.SelectedIdtype == AppResources.TinDeregistrationNationalID || viewModel.SelectedIdtype == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (!string.IsNullOrEmpty(viewModel.SelectedIdNumber))
                        {
                            //await Task.Delay(700);
                            viewModel.ValidateIDNumberForIndiviualOutlets();
                        }
                    }
                }

                if (arg.PickerId == "DeregOutletSingleDatePicker")
                {
                    viewModel.SingleDeregistrationDate = arg.SelectedValue;//Convert.ToDateTime(arg.SelectedValue).ToString("dd/MM/yyyy");
                }

                MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            });
        }

        async void TapGestureRecognizerSelectSingleOutleDate_Tapped(object sender, EventArgs e)
        {
            try
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = AppResources.TinDeregistrationDate;
                genericDatePickerModel.PickerId = "DeregPermitOutletDatePicker";

                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeDeRegisrtationReasonDateTapped.Execute(parameterVal);
                }
                datepickermessagecenter();
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));

            }
            catch (InternetException)
            {
            }
        }

        async void TapGestureRecognizerSelectPermitType_Tapped(object sender, EventArgs e)
        {
            try
            {
                List<string> reasonData = new List<string>();
                reasonData.Add(AppResources.TinDeregistrationClosed);
                reasonData.Add(AppResources.TinDeregistrationTransfer);

                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeReasonTapped.Execute(parameterVal);
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = reasonData;
                genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "permitTypeReasonPicker";

                await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
            catch (Exception)
            {



            }
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            viewModel.OnIdTypeClicked();
        }

        async void IndiviualTinRegistrationDOBTapped(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idNumber.Text))
            {
                idNumber.Focus();
                return;
            }
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "_DOBDateTypePicker";
            try
            {
                datepickermessagecenter();
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException)
            {

            }
        }

        async void OnIndiviualOutletTinRegistrationDOBTapped(object sender, EventArgs e)
        {
            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = AppResources.VatDeregDOBDatePickerTitle;
            genericDatePickerModel.PickerId = "PermitTypeDobPickerDateTypePicker";
            try
            {
                if ((e as TappedEventArgs).Parameter != null)
                {
                    var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                    viewModel.OnOutletPermitTypeDeRegisrtationReasonDateTapped.Execute(parameterVal);
                }
                datepickermessagecenter();
                await MopupService.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel));
            }
            catch (GAZTUnlockAccountException)
            {

            }

        }

        void PermitIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var permit = ((GAZTBorderlessEntry)sender).ReturnCommandParameter as PermitSetResult;
                var permitIDNum = sender as GAZTBorderlessEntry;
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();

                var selectedPermit = viewModel?.SelectedOutletForCloseTranser.PermitTypes.FirstOrDefault(x => x.APermitNoTb == permit.APermitNoTb || x.APermitNoTb == permit.APermitNoTb);

                if (!string.IsNullOrEmpty(selectedPermit?.PermitIdTypeName))
                {
                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationNationalID)
                    {
                        if (selectedPermit?.APermitIdNoTb.Substring(0, 1) != "1")
                        {
                            popUp.Message = AppResources.ZZNationalIDstartswith1;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            //viewModel.FrameIDError = true;
                            permitIDNum.Text = string.Empty;
                            //ZZPleaseenteravalidNationalID
                        }
                        else
                        {
                            if (selectedPermit?.APermitIdNoTb.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                            }
                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }

                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                //viewModel.FrameIDError = true;
                                permitIDNum.Text = string.Empty;
                            }
                            else
                            {
                                //viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(selectedPermit?.APermitDeregDisplayDobDate))
                                {
                                    viewModel.ValidateIDNumberForIndiviualPermit(permit);
                                }
                            }
                        }
                    }

                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationIQAMANumber)
                    {
                        if (selectedPermit?.APermitIdNoTb.Substring(0, 1) != "2")
                        {
                            popUp.Message = AppResources.ZZIqamaIDstartswith2;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            //viewModel.FrameIDError = true;
                            permitIDNum.Text = string.Empty;
                        }
                        else
                        {
                            if (selectedPermit?.APermitIdNoTb.Length != 10)
                            {
                                if (Messages.Length > 0)
                                {
                                    Messages.Append(Environment.NewLine);
                                }
                                Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                            }

                            if (Messages.Length > 0)
                            {
                                popUp.Message = Messages.ToString();
                                popUp.IsLinkAvailable = false;
                                if (App.IsArabic)
                                {
                                    popUp.FlowDirections = "RightToLeft";
                                    popUp.isFontSet = true;
                                }
                                else
                                {
                                    popUp.FlowDirections = "LeftToRight";
                                }
                                MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                                permitIDNum.Text = string.Empty;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(selectedPermit.APermitDeregDisplayDobDate))
                                {
                                    viewModel.ValidateIDNumberForIndiviualPermit(permit);
                                }
                            }
                        }
                    }

                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationGCCID)
                    {
                        if (selectedPermit?.APermitIdNoTb.Substring(0, 1) == "0")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            permitIDNum.Text = string.Empty;
                        }
                        else if (!(selectedPermit?.APermitIdNoTb.Length <= 15 && selectedPermit?.APermitIdNoTb.Length >= 7))
                        {
                            popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            permitIDNum.Text = string.Empty;
                            //EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            //viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumberForIndiviualPermit(permit);
                        }
                    }

                    if (selectedPermit?.PermitIdTypeName == AppResources.TinDeregistrationCompanyID)
                    {
                        if (selectedPermit?.APermitIdNoTb.Substring(0, 1) != "7")
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.TinDeregistrationCompanyIDCheck;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            permitIDNum.Text = string.Empty;
                        }
                        else if (selectedPermit.APermitIdNoTb.Length > 10)
                        {
                            //Have to change to neww error message
                            popUp.Message = AppResources.CompanyIDlengthis10digit;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                            permitIDNum.Text = string.Empty;
                        }
                        else
                        {
                            //viewModel.FrameIDError = false;
                            viewModel.ValidateIDNumberForIndiviualPermit(permit);
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
        }


        void Button_Clicked(object sender, EventArgs e)
        {
            var permit = new PermitSetResult();
            if (sender is GAZTBorderlessEntry borderlessEntry)
            {
                 permit = borderlessEntry.BindingContext as PermitSetResult;
            }
            else if (sender is SfTextInputLayout textInputLayout)
            {
                 permit = textInputLayout.BindingContext as PermitSetResult;
            }
            viewModel.selectedCalPermitNo = permit.APermitNoTb;
            MainThread.BeginInvokeOnMainThread(() =>
             {
                 if (permit.IsHijiri)
                 {
                     ClosePermitDeregDatePickerHijri.IsOpen = true;
                 }
                 else
                 {
                     ClosePermitDeregDatePicker.IsOpen = true;
                 }
             });
        }

        void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if ((e as TappedEventArgs).Parameter != null)
            {
                var parameterVal = (e as TappedEventArgs).Parameter.ToString();
                viewModel.OnOutletPermitTypeDeRegisrtationReasonDateTapped.Execute(parameterVal);
            }

        }
        void OnClosePermitDOBDateClicked(object sender, EventArgs e)
        {
            viewModel.DobText.IsEditable = true;
            var permit = ((Button)sender).CommandParameter as PermitSetResult;
            viewModel.selectedCalPermitNo = permit.APermitNoTb;
            if (permit != null && permit != null && !string.IsNullOrEmpty(permit.aPermitIdTypeTb))
            {
                //if (permit.PermitIdTypeName.Equals(AppResources.TinDeregistrationNationalID) || permit.PermitIdTypeName.Equals(AppResources.TinDeregistrationIQAMANumber)) // DOB enable only in case of Natioanl Id and Iquama Id
                //{
                MainThread.BeginInvokeOnMainThread(() =>
                 {
                     if (permit.IsDOBHijiri)
                     {
                         ClosePermitDOBPickerHijri.IsOpen = true;
                     }
                     else
                     {
                         ClosePermitDOBPicker.IsOpen = true;
                     }
                 });
                //}
            }
            else if (viewModel.PickerModel != null)
            {
                //if (viewModel.PickerModel.SelectedValue.Equals(AppResources.TinDeregistrationNationalID) || viewModel.PickerModel.SelectedValue.Equals(AppResources.TinDeregistrationIQAMANumber)) // DOB enable only in case of Natioanl Id and Iquama Id
                //{
                MainThread.BeginInvokeOnMainThread(() =>
                 {
                     if (permit.IsDOBHijiri)
                     {
                         ClosePermitDOBPickerHijri.IsOpen = true;
                     }
                     else
                     {
                         ClosePermitDOBPicker.IsOpen = true;
                     }
                 });
                //}
            }
        }

        public void SetLayoutVisibilityOnPageAppearing(TINDeregistrationModel selectedItem)
        {
            //  TINDeregistrationModel selectedItem = e.AddedItems[0] as TINDeregistrationModel;
            viewModel.SelectedPermitOutletOptionIndex = viewModel.PermitOutletDecisionOptions.IndexOf(selectedItem);

            if (viewModel.SelectedPermitOutletOptionIndex == 2)
            {
                if (viewModel.SelectedOutletForCloseTranser != null)
                {
                    if (viewModel.SelectedOutletForCloseTranser.PermitTypes != null)
                    {
                        if (viewModel.SelectedOutletForCloseTranser.PermitTypes.Count > 0)
                        {
                            viewModel.IsNodataAvailableVisible = false;

                            viewModel.IsMultiplePermitsVisible = true;
                        }
                        else
                        {
                            viewModel.IsNodataAvailableVisible = true;

                        }
                    }
                    else
                    {
                        viewModel.IsNodataAvailableVisible = true;
                    }
                }
            }
            else
            {
                viewModel.IsNodataAvailableVisible = false;
                viewModel.IsMultiplePermitsVisible = false;
                if (viewModel.SelectedPermitOutletOptionIndex == 1)
                {
                    viewModel.FirstNameLbl = AppResources.ZZZVATRFirstName;
                    viewModel.SurnameNameLbl = AppResources.TinDeregistrationSurName;
                }
            }

        }

    }
}
